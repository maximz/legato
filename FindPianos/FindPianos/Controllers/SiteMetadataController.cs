using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;

namespace FindPianos.Controllers
{
    public class SiteMetadataController : Controller
    {
        [Url("sitemap.xml")]
        public ActionResult SiteMap()
        {
            throw new NotImplementedException();
        }
        [Url("robots.txt")]
        public ActionResult Robots()
        {
            return File(Server.MapPath("robots.txt.config"),"text/plain");
        }

    }
}
