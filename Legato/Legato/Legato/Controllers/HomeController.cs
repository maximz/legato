﻿using System;
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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Url("p/{pid}")]
        [CustomCache(NoCachingForAuthenticatedUsers = false, Duration = 7200 * 40, VaryByParam = "pid")]
        public ActionResult PostRedirect(int pid)
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
                    return RedirectToAction("Timeline","Instruments", new{ reviewID = globalpost.SpecificPostID});
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
    }
}