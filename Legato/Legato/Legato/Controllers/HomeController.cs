using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Models;
using Legato.Helpers;

namespace Legato.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
    {
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        [Url("")]
        public virtual ActionResult Index()
        {
            ViewBag.PageType = "PresentationPage"; // see top of master layout page

            var dbTypes = (from t in Current.DB.InstrumentTypes
                           select new { Id = t.TypeID, Name = t.Name }).ToList();
            dbTypes.Add(new { Id = 0, Name = "All Instruments" });
            ViewBag.Types = new SelectList(dbTypes.ToArray(), "Id", "Name");

            return View();
        }

        [Url("About")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult About()
        {
            return View();
        }

        [Url("p/{pid}")]
        [CustomCache(NoCachingForAuthenticatedUsers = false, Duration = 7200 * 40, VaryByParam = "pid")]
        public virtual ActionResult PostRedirect(int pid)
        {
            var db = Current.DB;
            var globalpost = db.GlobalPostIDs.Where(p=>p.GlobalPostID1==pid).SingleOrDefault();
            if(globalpost==null)
            {
                return RedirectToAction("NotFound","Error");
            }

            switch(globalpost.PostCategory)
            {
                case "ins":
                    return RedirectToAction("Individual","Instruments", new{ instrumentID = globalpost.SpecificPostID});
                    break;
                case "ins.rev":
                    return RedirectToAction("IndividualReview","Instruments", new{ reviewID = globalpost.SpecificPostID});
                    break;
                case "i.r.r":
                    var reviewr = db.InstrumentReviewRevisions.Where(r=>r.RevisionID==globalpost.SpecificPostID).SingleOrDefault();
                    if(reviewr==null)
                    {
                        return RedirectToAction("NotFound","Error");
                    }
                    return RedirectToAction("Timeline","Instruments", new{ reviewID = reviewr.ReviewID});
                    break;
            }

            return RedirectToAction("NotFound","Error");
        }

        [Url("BuildNum")]
        public virtual ActionResult BuildNum()
        {
            return Content(Current.RevNumber());
        }
    }
}
