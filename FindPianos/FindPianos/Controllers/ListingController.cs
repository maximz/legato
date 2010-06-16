using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;
using System.Globalization;
using RiaLibrary.Web;

namespace FindPianos.Controllers
{
    [HandleError]
    public class ListingController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult Read(long id)
        {
            using (var data = new PianoDataContext())
            {
                var listing = data.PianoListings.Where(l => l.PianoID == id).SingleOrDefault();
                var reviews = data.PianoReviews.Where(r => r.PianoListingID == id).ToList();
                foreach (var r in reviews)
                {
                    r.Comments = data.PianoReviewComments.Where(c => c.PianoReviewID == r.PianoReviewID).ToList();
                    r.Revisions = data.PianoReviewRevisions.Where(rev => rev.PianoReviewID == r.PianoReviewID).OrderByDescending(rev => rev.RevisionNumberOfReview).ToList();
                }
                ViewData["listing"] = listing;
                ViewData["reviews"] = reviews;

                return View();
            }
        }
        [Url("/Search")]
        public ActionResult List()
        {
            return View();
        }
        [Url("/Search")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List(SearchForm s)
        {
            //validate


            //execute search
            

            using (var db = new PianoDataContext())
            {
                ViewData["listings"] = db.ProcessSearchForm(s);
            }
            return View();
        }
        [Url("/Listing/Create")]
        public ActionResult Submit()
        {
            return View();
        }
        [Url("/Listing/Create")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Submit(PianoReview r)
        {
            //try
            //{
            //    //TODO: assign authenticated user's info
            //    //UpdateModel(r);
            //    using (var db = new DUDataContext())
            //    {
            //        db.Revisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
            //        if (!r.IsValid)
            //        {
            //            throw new Exception(); //just in case insertonsubmit doesn't throw exception correctly
            //        }
            //        db.SubmitChanges();
            //    }
            //    return RedirectToAction("Details", new { id = r.RevID }); //shows details for that submission thread, with only one revision!

            //}
            //catch
            //{
            //    foreach (RuleViolation rv in r.GetRuleViolations())
            //    {
            //        ModelState.AddModelError(rv.PropertyName, rv.ErrorMessage);
            //        ModelState.SetModelValue(rv.PropertyName, new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
            //    }
            //    return View();
            //}
            return View();
        }
        [Url("/Listing/Edit/{reviewId}")]
        public ActionResult Edit(long reviewId)
        {
            using(var db = new PianoDataContext())
            {
                ViewData["revisions"] = db.PianoReviewRevisions.Where(r => r.PianoReviewID == reviewId).ToList();
                return View();
            }
        }
        [Url("/Listing/Edit/{reviewId}")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(long reviewId, [Bind(Exclude="PianoReviewRevisionID, PianoReviewID, DateOfRevision")]PianoReviewRevision r)
        {
            try
            {
                //TODO: assign authenticated user's info
                r.PianoReviewID = reviewId;
                r.DateOfRevision = DateTime.UtcNow;
                using (var db = new PianoDataContext())
                {
                    r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                                                where rev.PianoReviewID == reviewId
                                                select rev.RevisionNumberOfReview).Max() + 1;
                    db.PianoReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    if (!r.IsValid)
                    {
                        throw new Exception(); //just in case insertonsubmit doesn't throw exception correctly
                    }
                    db.SubmitChanges();
                }
                return RedirectToAction("Read", new { id = r.PianoReview.PianoListingID }); //shows details for that submission thread, with only one revision!

            }
            catch
            {
                foreach (RuleViolation rv in r.GetRuleViolations())
                {
                    ModelState.AddModelError(rv.PropertyName, rv.ErrorMessage);
                    ModelState.SetModelValue(rv.PropertyName, new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
                }
                return View();
            }
        }


    }
}
