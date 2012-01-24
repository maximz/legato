using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Helpers;
using Lucene.Net;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Legato.Models;
using Lucene.Net.Documents;
using System.IO;
using Lucene.Net.QueryParsers;
using Lucene.Net.Highlight;
using System.Web.Hosting;
using System.Text;
using Legato.ViewModels;
using System.Web.Security;
using MvcReCaptcha;
using System.Configuration;
using System.Web.Routing;
using Legato.Models.Search;

namespace Legato.Controllers
{
    /// <summary>
    /// Handles searching operations.
    /// </summary>
    public partial class SearchController : CustomControllerBase
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (ViewData["CurrentMenuItem"] == null || ViewData["CurrentMenuItem"].ToString().IsNullOrEmpty())
            {
                ViewData["CurrentMenuItem"] = "Search";
            }
        }

        // TODO: PAGINATION!
        /// <summary>
        /// Displays a search form.
        /// </summary>
        /// <returns></returns>
        [Url("search")]
        [CustomCache(Duration = 3600, NoCachingForAuthenticatedUsers = true)]
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Processes search form and redirects to search action.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        [Url("search/execute")]
        [HttpPost]
        public virtual ActionResult IndexPost(string query, string tags)
        {
            if (tags.HasValue() && query.HasValue() && tags != "in these tags (optional)" && query != "search query/term") // not going to happen, because tags are currently disabled
            {
                throw new NotImplementedException("Tags are currently disabled.");
                var sb = new StringBuilder();
                foreach (var tag in tags.Trim().Split(' '))
                {
                    sb.Append("+" + tag);
                }
                sb.Remove(0, 1); //Removes first +
                return RedirectToAction("TagSearch", new
                {
                    query = query.Trim(),
                    tags = sb.ToString().Trim()
                });
            }
            else if (query.HasValue() && query != "search query/term")
            {
                return RedirectToAction("Search", new { query = query.Trim() });
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        [Url("search/{query}")]
        [CustomCache(Duration = 300, NoCachingForAuthenticatedUsers = true, VaryByParam = "query")]
        public virtual ActionResult Search(string query)
        {
            try
            {
                if (ConfigurationManager.AppSettings["IsSearchActivated"] == "false")
                {
                    return View("Deactivated");
                }
                if (query.IsNullOrEmpty())
                {
                    return RedirectToAction("Index");
                }

                // No caching for now. Why? Because if the first request is a moderator/admin request, then all the following requests will see hidden posts!
                // TODO: caching

                //var cachedObject = Current.GetCachedObject("Search.Regular."+query.Trim());
                //if (cachedObject != null)
                //{
                //    return View(cachedObject as SearchResultsModel);
                //}

                var model = SearchManager.Current.SearchIndex(query);

                //Current.SetCachedObject("Search.Regular." + query.Trim(), model, 7200);
                return View(model);
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        /// <summary>
        /// Executes search across one or more tags. Tags should be delimited by +.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        [Url("search/{query}/{tags}")]
        [CustomCache(Duration = 300, NoCachingForAuthenticatedUsers = true, VaryByParam = "query")]
        public virtual ActionResult TagSearch(string query, string tags)
        {
            throw new NotImplementedException("Tags are currently disabled.");
            try
            {
                //InitWriter();
                //if (ConfigurationManager.AppSettings["IsSearchActivated"] == "false")
                //{
                //    return View("Deactivated");
                //}
                //var tagList = new List<string>();

                //if (tags.IsNullOrEmpty() && query.HasValue())
                //{
                //    return RedirectToAction("Search", new
                //    {
                //        query = query
                //    });
                //}
                //else if (query.IsNullOrEmpty() || tags.IsNullOrEmpty())
                //{
                //    return RedirectToAction("Index");
                //}

                //var reader = writer.GetReader(); // Get reader from writer
                //var searcher = new IndexSearcher(reader); // Build IndexSearcher

                //// Build query
                //var parser = new MultiFieldQueryParser(new string[] { "Text", "Title" }, analyzer);
                //var searchQuery = parser.Parse(query);

                //var filterQuery = new BooleanQuery();
                //foreach (var tag in tags.Trim().Split('+'))
                //{
                //    filterQuery.Add(new TermQuery(new Term("RawTags", "<" + tag.Trim() + ">")), BooleanClause.Occur.MUST);
                //    tagList.Add(tag.Trim());
                //}
                //var filter = new QueryFilter(filterQuery);

                //// Execute search
                //var hits = searcher.Search(searchQuery, filter);

                //// Display results
                //var results = new List<Result>();
                //for (int i = 0; i < hits.Length(); i++)
                //{
                //    results.Add(new Result()
                //    {
                //        doc = hits.Doc(i),
                //        Score = hits.Score(i)
                //    });
                //}

                ////Highlight the parts that are matched:
                //var formatter = new SimpleHTMLFormatter("<span style='background:yellow;font-weight:bold;'>", "</span>");
                //var fragmenter = new SimpleFragmenter(400);
                //var scorer = new QueryScorer(searchQuery);
                //var highlighter = new Highlighter(formatter, scorer);
                //highlighter.SetTextFragmenter(fragmenter);
                //var finalResults = new List<DisplayedResult>();

                //var db = Current.DB;
                //foreach (var result in results)
                //{
                //    var stream = analyzer.TokenStream("", new StringReader(result.doc.Get("RawText")));
                //    var highlighted = highlighter.GetBestFragments(stream, result.doc.Get("RawText"), 1, "...").Replace("'", "''");
                //    if (highlighted == "") // sometimes the highlighter fails to emit text...
                //    {
                //        highlighted = result.doc.Get("RawText").Replace("'", "''");
                //    }
                //    if (highlighted.Length > 1000)
                //    {
                //        highlighted = highlighted.Substring(0, 1000);
                //    }

                //    var post = db.Posts.Where(p => p.PostID == int.Parse(result.doc.Get("PostID"))).SingleOrDefault();
                //    if (post == null) continue;
                //    if (!(post.DateOfPublish <= DateTime.Now) && !(User.Identity.IsAuthenticated && ((Guid)Membership.GetUser().ProviderUserKey == post.AuthorID || User.IsInRole("Moderator") || User.IsInRole("Administrator")))) continue; // if post is private, skip it

                //    finalResults.Add(new DisplayedResult()
                //    {
                //        ResultPost = post,
                //        Score = result.Score,
                //        HighlightedHTML = highlighted
                //    });
                //}
                //// Dispose of objects
                //searcher = null;
                //reader.Close();
                //reader = null;

                //return View(new TagResultsModel()
                //{
                //    Results = finalResults.OrderByDescending(r => r.Score),
                //    Tags = tagList,
                //    Query = query.Trim()
                //});
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        #region Administrative Methods
        [Url("admin/search/regenerate")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator+","+RoleNames.Administrator)]
        [HttpGet]
        public virtual ActionResult RegenerateIndex()
        {
            return View();
        }
        [CaptchaValidator]
        [HttpPost]
        [VerifyReferrer]
        [Url("admin/search/regenerate")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        public virtual ActionResult RegenerateIndex(bool captchaValid)
        {
            try
            {
                if (captchaValid)
                {
                    // lock searching
                    ConfigurationManager.AppSettings["IsSearchActivated"] = "false";

                    SearchManager.Current.CreateIndex();

                    // unlock searching
                    ConfigurationManager.AppSettings["IsSearchActivated"] = "true";
                }
                else
                {
                    ModelState.AddModelError("CAPTCHA", "Please re-enter the verification word.");
                    return View();
                }
                return Content("Index successfully regenerated!");
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [Url("admin/search/optimize")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        [HttpGet]
        public virtual ActionResult OptimizeIndex()
        {
            return View();
        }
        [CaptchaValidator]
        [HttpPost]
        [VerifyReferrer]
        [Url("admin/search/optimize")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        public virtual ActionResult OptimizeIndex(bool captchaValid)
        {
            try
            {
                if (captchaValid)
                {
                    // lock searching
                    ConfigurationManager.AppSettings["IsSearchActivated"] = "false";

                    SearchManager.Current.OptimizeIndex();

                    // unlock searching
                    ConfigurationManager.AppSettings["IsSearchActivated"] = "true";
                }
                else
                {
                    ModelState.AddModelError("CAPTCHA", "Please re-enter the verification word.");
                    return View();
                }
                return Content("Index successfully optimized!");
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        #endregion

        #region Spatial Administrative Methods
        [Url("admin/spatial/regenerate")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        [HttpGet]
        public virtual ActionResult SpatialRegenerateIndex()
        {
            return View();
        }
        [CaptchaValidator]
        [HttpPost]
        [VerifyReferrer]
        [Url("admin/spatial/regenerate")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        public virtual ActionResult SpatialRegenerateIndex(bool captchaValid)
        {
            try
            {
                if (captchaValid)
                {
                    SpatialSearchManager.Current.CreateIndex();
                }
                else
                {
                    ModelState.AddModelError("CAPTCHA", "Please re-enter the verification word.");
                    return View();
                }
                return Content("Index successfully regenerated!");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [Url("admin/spatial/optimize")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        [HttpGet]
        public virtual ActionResult SpatialOptimizeIndex()
        {
            return View();
        }
        [CaptchaValidator]
        [HttpPost]
        [VerifyReferrer]
        [Url("admin/spatial/optimize")]
        [CustomAuthorization(AuthorizeEmailNotConfirmed = false, AuthorizeSuspended = false, AuthorizedRoles = RoleNames.Moderator + "," + RoleNames.Administrator)]
        public virtual ActionResult SpatialOptimizeIndex(bool captchaValid)
        {
            try
            {
                if (captchaValid)
                {
                    SpatialSearchManager.Current.OptimizeIndex();
                }
                else
                {
                    ModelState.AddModelError("CAPTCHA", "Please re-enter the verification word.");
                    return View();
                }
                return Content("Index successfully optimized!");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        #endregion

        // In the index, we have two tokenized fields to search across - Title and Text. Thus, we have to use a special QueryParser: http://stackoverflow.com/questions/468405/lucene-net-how-to-incorporate-multiple-fields-in-queryparser/2036898#2036898


        // this should only happen when the application's exiting, because according to http://stackoverflow.com/questions/3865183/lucene-open-a-closed-indexwriter/3865564#3865564, we should only close rarely.
        /*private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }
                try
                {
                    writer.Close();
                    writer = null;
                }
                catch
                {

                }
                // Release unmanaged resources.
                // Set large fields to null.
                // Call Dispose on your base class.
                disposed = true;
            }
            base.Dispose(disposing);
        } */


    }

}
