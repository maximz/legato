using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Text.RegularExpressions;
using System.Web.Hosting;
using Legato.ViewModels;
using Lucene.Net.Spatial.GeoHash;
using Lucene.Net.Spatial.Geometry;
using Lucene.Net.Spatial.Tier;
using Lucene.Net.Spatial.Tier.Projectors;
using Lucene.Net.Search.Function;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace Legato.Models.Search
{
	/// <summary>
	/// Provides spatial searching tasks using a second Lucene.net search index.
	/// </summary>
	public class SpatialSearchManager
	{
		//private static string _indexPath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\search";
		private static string _indexPath = HostingEnvironment.MapPath("~/search/spatial");
		private static Regex _removeTagsRegex = new Regex("<(.|\n)*?>");

		/// <summary>
		/// Gets the current <see cref="SearchManager"/> for the application.
		/// </summary>
		public static SpatialSearchManager Current
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
			internal static readonly SpatialSearchManager Current = new SpatialSearchManager();

			static Nested()
			{
			}
		}

		private readonly List<CartesianTierPlotter> _ctps = new List<CartesianTierPlotter>();
		private readonly IProjector _projector = new SinusoidalProjector();
		private const string LatField = "lat";
		private const string LngField = "lng";
		
		
		private void SetUpPlotter(int @base, int top)
		{

			for (; @base <= top; @base++)
			{
				_ctps.Add(new CartesianTierPlotter(@base, _projector, CartesianTierPlotter.DefaltFieldPrefix));
			}
		}

		/// <summary>
		/// Searches the lucene index with the search text.
		/// </summary>
		/// <param name="searchText">The text to search with.</param>
		/// <remarks>Syntax reference: http://lucene.apache.org/java/2_3_2/queryparsersyntax.html#Wildcard</remarks>
		/// <exception cref="SearchException">An error occured searching the lucene.net index.</exception>
		public SpatialSearchResultsModel SearchIndex(double lat, double @long, double miles)
		{
			// This check is for the benefit of the CI builds
			if (!Directory.Exists(_indexPath))
				CreateIndex();

			var model = new SpatialSearchResultsModel();

			StandardAnalyzer analyzer = new StandardAnalyzer();
			try
			{
				IndexSearcher searcher = new IndexSearcher(_indexPath, true);
				// Build query
				var dq = new DistanceQueryBuilder(lat, @long, miles, LatField, LngField, CartesianTierPlotter.DefaltFieldPrefix, true);

				//create a term query to search against all documents
			Query tq = new TermQuery(new Term("metafile", "doc"));

			var dsort = new DistanceFieldComparatorSource(dq.DistanceFilter);
			Sort sort = new Sort(new SortField("foo", dsort, false));

			// Perform the search, using the term query, the distance filter, and the
			// distance sort
			TopDocs hits = searcher.Search(tq, dq.Filter, 1000, sort);
			int results = hits.totalHits;
			ScoreDoc[] scoreDocs = hits.scoreDocs;

			// Get a list of distances
			Dictionary<int, Double> distances = dq.DistanceFilter.Distances;
			
			model.QueryLat = lat;
			model.QueryLong = @long;
			model.QueryMiles = miles;
			
			//double lastDistance = 0;
			for (int i = 0; i < results; i++)
			{
				Document d = searcher.Doc(scoreDocs[i].doc);

				String name = d.Get("name");
				double rsLat = NumericUtils.PrefixCodedToDouble(d.Get(LatField));
				double rsLng = NumericUtils.PrefixCodedToDouble(d.Get(LngField));
				int id = int.Parse(d.Get("PostID"));
				Double geo_distance = distances[scoreDocs[i].doc];

				double distance = DistanceUtils.GetInstance().GetDistanceMi(lat, @long, rsLat, rsLng);
				double llm = DistanceUtils.GetInstance().GetLLMDistance(lat, @long, rsLat, rsLng);
				
				model.Results.Add(new SpatialResult() { title = name, instrumentID = id, dist = distance, lat = rsLat, lng = rsLng, resultNum = i+1});

				//lastDistance = geo_distance;
			}
			}
			catch(Exception ex)
			{
				throw new SearchException(ex, "An error occurred while searching the index");
			}
			
			return model;
			
		}
		
		public void AddPoint(int id, string name, double lat, double lng)
		{
			IndexWriter writer = new IndexWriter(_indexPath, new WhitespaceAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);
			Document doc = new Document();

			doc.Add(new Field("name", name, Field.Store.YES, Field.Index.ANALYZED));
			doc.Add(new Field("PostID", Convert.ToString(id), Field.Store.YES, Field.Index.UN_TOKENIZED));

			// convert the lat / long to lucene fields
			doc.Add(new Field(LatField, NumericUtils.DoubleToPrefixCoded(lat), Field.Store.YES, Field.Index.NOT_ANALYZED));
			doc.Add(new Field(LngField, NumericUtils.DoubleToPrefixCoded(lng), Field.Store.YES, Field.Index.NOT_ANALYZED));

			// add a default meta field to make searching all documents easy 
			doc.Add(new Field("metafile", "doc", Field.Store.YES, Field.Index.ANALYZED));

			int ctpsize = _ctps.Count;
			for (int i = 0; i < ctpsize; i++)
			{
				CartesianTierPlotter ctp = _ctps[i];
				var boxId = ctp.GetTierBoxId(lat, lng);
				doc.Add(new Field(ctp.GetTierFieldName(),
								  NumericUtils.DoubleToPrefixCoded(boxId),
								  Field.Store.YES,
								  Field.Index.NOT_ANALYZED_NO_NORMS));
			}
			writer.AddDocument(doc);

		}
		
		/// <summary>
		/// Changes the index - removes the old version of the post from the index and adds the new version.
		/// </summary>
		/// <param name="toChange">To change.</param>
		public void Update(Instrument toChange)
		{
			EnsureDirectoryExists();
			Delete(toChange);
			Add(toChange);
		}

public void Add(Instrument toAdd)
{
toAdd.FillProperties();
AddPoint(toAdd.InstrumentID, toAdd.Title, toAdd.Lat, toAdd.Long);
}
		
		/// <summary>
		/// Deletes from index.
		/// </summary>
		/// <param name="toDelete">To delete.</param>
		public void Delete(Instrument toDelete)
		{
			try
			{
			WhitespaceAnalyzer analyzer = new WhitespaceAnalyzer();
			IndexWriter writer = new IndexWriter(_indexPath, analyzer, false);
			writer.DeleteDocuments(new Term("PostID", Convert.ToString(toDelete.InstrumentID)));
			
							writer.Optimize();
				writer.Close();

			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occurred while deleting page '{0}' from the search index", toDelete.Title);
			}
		}


		/// <summary>
		/// Creates the initial search index based on all pages in the system.
		/// </summary>
		/// <exception cref="SearchException">An error occurred with the lucene.net IndexWriter while adding the page to the index.</exception>
		public void CreateIndex()
		{
			EnsureDirectoryExists();

			try
			{
				WhitespaceAnalyzer analyzer = new WhitespaceAnalyzer();
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
						Add(p);
					}

				writer.Optimize();
				writer.Close();
			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occurred while creating the search index");
			}
		}
		
		public void OptimizeIndex()
		{
			EnsureDirectoryExists();
			
			try
			{
				WhitespaceAnalyzer analyzer = new WhitespaceAnalyzer();
				IndexWriter writer = new IndexWriter(_indexPath, analyzer, true);
				writer.Optimize();
				writer.Close();
			}
			catch (Exception ex)
			{
				throw new SearchException(ex, "An error occurred while creating the search index");
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
				throw new SearchException(ex, "An error occurred while creating the search directory '{0}'", _indexPath);
			}
		}

	}
	
	
	public class SpatialSearchResultsModel
	{
		public double QueryLat
		{
			get;set;
		}
		public double QueryLong
		{
			get;set;
		}
		public double QueryMiles
		{
			get;set;
		}
		public List<SpatialResult> Results
		{
			get;set;
		}
	}
	public class SpatialResult
	{
		public string title
		{
		get;set;
		}
		public int instrumentID
		{
		get;set;
		}
		public double dist
		{
		get;set;
		}
		public double lat
		{
		get;set;
		}
		public double lng
		{
		get;set;
		}
		public int resultNum
		{
		get;set;
		}
	}
}