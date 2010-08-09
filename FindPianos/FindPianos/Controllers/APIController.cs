using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;

namespace FindPianos.Controllers
{
    [HandleError]
    [OutputCache(Duration = 7200, VaryByParam = "*")]
    public class APIController : CustomControllerBase
    {
        [OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult Index()
        {
            return Content("NotImplemented");
        }
        [OutputCache(Duration = 7200, VaryByParam = "*")][HttpPost]
        public ActionResult GetListings(BoundingBox bounds, DateTime? startDateSubmission, DateTime? endDateSubmission, int? stars, int? pagenumber)
        {
            return Content("NotImplemented");
        }
        [OutputCache(Duration = 7200, VaryByParam = "*")][HttpPost]
        public ActionResult GetUsers(string nameContains, DateTime? startRegistration, DateTime? endRegistration, DateTime? startLastPost, DateTime? endLastPost, int? pagenumber)
        {
            return Content("NotImplemented");
        }
    }
}
