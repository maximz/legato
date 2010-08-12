using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using FindPianos.Helpers;

namespace FindPianos.Controllers
{
    public class SiteMetadataController : CustomControllerBase
    {
        [Url("sitemap.xml")]
        [CustomCache(AllowOnlyValidSearchEngines=true,Duration=120)] //2 minutes
        public ActionResult SiteMap()
        {
            throw new NotImplementedException();
        }
        [Url("robots.txt")]
        [CustomCache(AllowOnlyValidSearchEngines = false, Duration = 432000)] //5 days
        public ActionResult Robots()
        {
            return File(Server.MapPath("robots.txt.config"),"text/plain");
        }

    }
}
