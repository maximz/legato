using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using FindPianos.Helpers;
using System.Text;
using FindPianos.Models;

namespace FindPianos.Controllers
{
    public class SiteMetadataController : CustomControllerBase
    {
        [Url("sitemap.xml")]
        [CustomCache(AllowOnlyValidSearchEngines=true,Duration=480)] //8 minutes
        public ActionResult SiteMap()
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            
            //Max size: 10MB and 50000 URLs

            //Add URLs
            sb.Append(AddToSitemap(string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, Url.Content("~")), DateTime.Now, "daily", "0.6"));

            using (var db = new LegatoDataContext())
            {
                var discuss = db.DiscussThreads.OrderByDescending(t => t.LatestActivity).Take(5000);
                foreach (var t in discuss)
                {
                    sb.Append(AddToSitemap()) //TODO
                }
            }
            
            sb.Append("</urlset>");

            return Content(sb.ToString(), "application/xml", Encoding.UTF8);

            //daily change frequency, 0.6 priority for all content
        }
        [Url("robots.txt")]
        [CustomCache(AllowOnlyValidSearchEngines = false, Duration = 432000)] //5 days
        public ActionResult Robots()
        {
            return File(Server.MapPath("robots.txt.config"),"text/plain");
        }


        private string AddToSitemap(string url, DateTime LastModification, string ChangeFrequency,string priority)
        {
            return string.Format(@"<url>
<loc>{0}</loc>
<lastmod>{1}</lastmod>
<changefreq>{2}</changefreq>
<priority>{3}</priority>
</url>", Url.Encode(url), LastModification.ToString("YYYY-MM-dd"), ChangeFrequency, priority);
        }
    }
}
