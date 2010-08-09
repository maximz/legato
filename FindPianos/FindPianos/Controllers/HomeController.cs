using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FindPianos.Helpers;

namespace FindPianos.Controllers
{
    [HandleError]
    [OutputCache(Duration = 7200, VaryByParam = "None")]
    public class HomeController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            if (ViewData["CurrentMenuItem"].ToString().IsNullOrEmpty())
            {
                ViewData["CurrentMenuItem"] = "Home";
            }
        }
        public ActionResult Index()
        {
            //ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
