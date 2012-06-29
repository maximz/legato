using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legato.Models;
using Lucene.Net.Documents;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net;
using Lucene.Net.QueryParsers;
using Lucene.Net.Highlight;
using Legato;
//using Roadkill.Core.Converters;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using Legato.ViewModels;

namespace Legato.Models.Search
{
	/// <summary>
	/// Provides searching tasks using a Lucene.net search index.
	/// </summary>
	public class SearchManager
	{
		//private static string _indexPath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\search";
		private static string _indexPath = HostingEnvironment.MapPath("~/search/lucene");
		private static Regex _removeTagsRegex = new Regex("<(.|\n)*?>");

		/// <summary>
		/// Gets the current <see cref="SearchManager"/> for the application.
		/// </summary>
		public static SearchManager Current
		{
			get
			{
				return Nested.Current;
			}
		}

		/// <summary>
		/// Singleton implementation.
		/// </summary>
		class Nested
		{
			internal static readonly SearchManager Current = new SearchManager();

			static Nested()
			{
			}
		}

		/// <summary>
		/// Searches the lucene index with the search text.
		/// </summary>
		/// <param name="searchText">The text to search with.</param>
		/// <remarks>Syntax reference: http://lucene.apache.org/java/2_3_2/queryparsersyntax.html#Wildcard</remarks>
		/// <exception cref="SearchException">An error occured searching the lucene.net index.</exception>
		public SearchResultsModel SearchIndex(string searchText)
		{
			// This check is for the benefit of the CI builds
			if (!Directory.Exists(_indexPath))
				CreateIndex();

			var model = new SearchResultsModel();

			StandardAnalyzer analyzer = new StandardAnalyzer();
			try
			{
				IndexSearcher searcher = new IndexSearcher(_indexPath);
				// Build query
				var parser = new MultiFieldQueryParser(new string[] { "Text", "Title" }, analyzer);
				var searchQuery = parser.Parse(searchText);

				// Execute search
				var hits = searcher.Search(searchQuery);

				// Display results
				var results = new List<Result>();
				for (int i = 0; i < hits.Length(); i++)
				{
					results.Add(new Result()
					{
						doc = hits.Doc(i),
						Score = hits.Score(i)
					});
				}

				//Highlight the parts that are matched:
				var formatter = new SimpleHTMLFormatter("<span style='background:yellow;font-weight:bold;'>", "</span>");
				var fragmenter = new SimpleFragmenter(400);
				var scorer = new QueryScorer(searchQuery);
				var highlighter = new Highlighter(formatter, scorer);
				highlighter.SetTextFragmenter(fragmenter);
				var finalResults = new List<DisplayedResult>();

				var db = Legato.Current.DB;
				foreach (var result in results)
				{
					var stream = analyzer.TokenStream("", new StringReader(result.doc.Get("RawText")));
					var highlighted = highlighter.GetBestFragments(stream, result.doc.Get("RawText"), 1, "...").Replace("'", "''");
					if (highlighted == "") // sometimes the highlighter fails to emit text...
					{
						highlighted = result.doc.Get("RawText").Replace("'", "''");
					}
					if (highlighted.Length > 1000)
					{
						highlighted = highlighted.Substring(0, 1000);
					}

					int postID;
					if (!int.TryParse(result.doc.Get("GlobalPostID"), out postID)) // If GlobalPostID is null or not a number, this isn't a valid search entry, so we skip it.
					{
						continue;
					}
					var post = db.GlobalPostIDs.Where(p => p.GlobalPostID1 == postID).SingleOrDefault();
					if (post == null) continue;
					// TODO: privacy checks?
					post.FillProperties();

					finalResults.Add(new DisplayedResult()
					{
						ResultPost = post,
						Score = result.Score,
						HighlightedHTML = highlighted
					});
				}

				model = new SearchResultsModel()
				{
					Results = finalResults.OrderByDescending(r => r.Score),
					Query = searchText.Trim()
				};
			}
			catch(Exception ex)
			{
				throw new SearchException(ex, "An error occured while searching the index");
			}
			
			return model;
			
			/*MultiFieldQueryParser parser = new MultiFieldQueryParser(new string[] { "content", "title" }, analyzer);

			Query query = null;
			try
			{
				query = parser.Parse(searchText);
			}
			catch (Lucene.Net.QueryParsers.ParseException)
			{
				// Catch syntax errors in the search and remove them.
				searchText = QueryParser.Escape(searchText);
				query = parser.Parse(searchText);
			}

			if (query != null)
			{
				try
				{
					IndexSearcher searcher = new IndexSearcher(_indexPath);
					Hits hits = searcher.Search(query);

					for (int i = 0; i < hits.Length(); i++)
					{
						Document document = hits.Doc(i);

						DateTime createdOn = DateTime.Now;
						if (!DateTime.TryParse(document.GetField("createdon").StringValue(), out createdOn))
							createdOn = DateTime.Now;

						SearchResult result = new SearchResult()
						{
							Id = int.Parse(document.GetField("id").StringValue()),
							Title = document.GetField("title").StringValue(),
							ContentSummary = document.GetField("contentsummary").StringValue(),
							Tags = document.GetField("tags").StringValue(),
							CreatedBy = document.GetField("createdby").StringValue(),
							CreatedOn = createdOn,
							ContentLength = int.Parse(document.GetField("contentlength").StringValue()),
							Score = hits.Score(i)
						};

						list.Add(result);
					}
				}
				catch (Exception ex)
				{
					throw new SearchException(ex, "An error occured while searching the index");
				}
			} 

			return list; */
		}
		
		/// <summary>
		/// Adds to index.
		/// </summary>
		/// <param name="toAdd">To add.</param>
		public void Add(object toAdd)
		{
			Add(toAdd, true);
		}
		/// <summary>
		/// Adds to index.
		/// </summary>
		/// <param name="toAdd">To add.</param>
		/// <param name="finalTransaction">If this is the final transaction, optimatization and closing methods are called.</param>
		public void Add(dynamic toAdd, bool finalTransaction)
		{
			try
			{
				EnsureDirectoryExists();

				StandardAnalyzer analyzer = new StandardAnalyzer();
				IndexWriter writer = new IndexWriter(_indexPath, analyzer, false);

				var doc = Types.DynamicToDocument(toAdd);
				writer.AddDocument(doc);
				
				writer.Optimize();
				writer.Close();
			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occured while adding page '{0}' to the search index", toAdd.Title);
			}
		}
		
		/// <summary>
		/// Changes the index - removes the old version of the post from the index and adds the new version.
		/// </summary>
		/// <param name="toChange">To change.</param>
		public void Update(dynamic toChange)
		{
			EnsureDirectoryExists();
			Delete(toChange, false);
			Add(toChange, true);
		}
		
		/// <summary>
		/// Deletes from index.
		/// </summary>
		/// <param name="toDelete">To delete.</param>
		public void Delete(dynamic toDelete)
		{
			Delete(toDelete, true);
		}
		/// <summary>
		/// Deletes from index.
		/// </summary>
		/// <param name="toDelete">To delete.</param>
		public void Delete(dynamic toDelete, bool finalTransaction)
		{
			try
			{
			StandardAnalyzer analyzer = new StandardAnalyzer();
			IndexWriter writer = new IndexWriter(_indexPath, analyzer, false);
			writer.DeleteDocuments(new Term("PostID", Convert.ToString(Types.DynamicToDocumentId(toDelete))));
			
							writer.Optimize();
				writer.Close();

			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occured while deleting page '{0}' from the search index", toDelete.Title);
			}
		}


		/// <summary>
		/// Creates the initial search index based on all pages in the system.
		/// </summary>
		/// <exception cref="SearchException">An error occured with the lucene.net IndexWriter while adding the page to the index.</exception>
		public void CreateIndex()
		{
			EnsureDirectoryExists();

			try
			{
				StandardAnalyzer analyzer = new StandardAnalyzer();
				IndexWriter writer = new IndexWriter(_indexPath, analyzer, true);
				
				// Step 1: delete everything from index!
					try
					{
						writer.DeleteAll();
					}
					catch
					{
						// if the index doesn't exist yet, this will probably fail
					}

					// Step 2: add everything into index!
					var db = Legato.Current.DB;
					foreach (var p in db.Instruments)
					{
						writer.AddDocument(Types.DynamicToDocument(p));
					}
					foreach (var p in db.InstrumentReviews)
					{
						writer.AddDocument(Types.DynamicToDocument(p));
					}

				writer.Optimize();
				writer.Close();
			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occured while creating the search index");
			}
		}
		
		public void OptimizeIndex()
		{
			EnsureDirectoryExists();
			
			try
			{
				StandardAnalyzer analyzer = new StandardAnalyzer();
				IndexWriter writer = new IndexWriter(_indexPath, analyzer, true);
				writer.Optimize();
				writer.Close();
			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occured while creating the search index");
			}
		}

		private void EnsureDirectoryExists()
		{
			try
			{
				if (!Directory.Exists(_indexPath))
					Directory.CreateDirectory(_indexPath);
			}
			catch (IOException ex)
			{
				throw new SearchException(ex, "An error occured while creating the search directory '{0}'", _indexPath);
			}
		}

	}
	
		/// <summary>
	/// A result.
	/// </summary>
	public class Result
	{
		public Document doc
		{
			get;
			set;
		}
		public double Score
		{
			get;
			set;
		}
	}
	/// <summary>
	/// A displayed result.
	/// </summary>
	public class DisplayedResult
	{
		public GlobalPostID ResultPost
		{
			get;
			set;
		}
		public double Score
		{
			get;
			set;
		}
		public string HighlightedHTML
		{
			get;
			set;
		}
	}
}
