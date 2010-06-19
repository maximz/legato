using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;

namespace FindPianos.Controllers
{
    [HandleError]
    [OutputCache(Duration = 7200, VaryByParam = "None")]
    public class APIController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult GetListings(BoundingBox bounds, DateTime? startDateSubmission, DateTime? endDateSubmission, int? stars, int? pagenumber)
        {
            return View();
        }

        public ActionResult GetUsers(string nameContains, DateTime? startRegistration, DateTime? endRegistration, DateTime? startLastPost, DateTime? endLastPost, int? pagenumber)
        {
            return View();
        }
    }
}
