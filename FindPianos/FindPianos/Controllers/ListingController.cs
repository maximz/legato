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
        public ActionResult Submit([Bind(Exclude = "PianoReviewRevisionID, PianoReviewID, DateOfRevision, RevisionNumberOfReview, VenueID")]PianoReviewRevision r, [Bind(Exclude="PianoID, Lat, Long, OriginalSubmitterUserID, DateOfSubmission")]PianoListing listing, [Bind(Exclude="VenueID")]PianoVenue v, [Bind(Exclude="VenueID,VenueHoursID")]ICollection<PianoVenueHour> hours)
        {
            //View info:
            //http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx = pianovenuehours binding
            //as there are multiple parameters, we'll just have to have multiple <form>s (one per parameter/object) in the View

            try
            {
                using (var db = new PianoDataContext())
                {
                    var time = DateTime.UtcNow;

                    //LISTING:
                    //TODO: assign authenticated user's info
                    listing.DateOfSubmission = time;
                    //TODO: geocode StreetAddress into Lat and Long; if failure, don't fill those properties, then check for existence of them in Validation before/during Submit to DB, create rule violation for street address if not available, break and return View right away.
                    db.PianoListings.InsertOnSubmit(listing);
                    db.SubmitChanges();

                    //REVIEW:
                    var review = new PianoReview();
                    review.PianoListing = listing;
                    db.PianoReviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    //REVISION:
                    r.DateOfRevision = time;
                    r.PianoReview = review;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.RevisionNumberOfReview = 1;
                    //TODO: VENUE!!!
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
