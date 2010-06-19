using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;
using System.Globalization;
using RiaLibrary.Web;
using GeoCoding;
using GeoCoding.Google;
using System.Web.Security;

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
        public ActionResult Submit([Bind(Exclude = "PianoReviewRevisionID, PianoReviewID, DateOfRevision, RevisionNumberOfReview")]PianoReviewRevision r, [Bind(Exclude="PianoID, Lat, Long, OriginalSubmitterUserID, DateOfSubmission")]PianoListing listing, [Bind(Exclude="ReviewRevisionID,VenueHoursID")]ICollection<PianoVenueHour> hours)
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
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    //TODO: add GUID to cache!!! Cache.Add(User.Identity.Name, userGuid);
                    listing.OriginalSubmitterUserID = userGuid;
                    listing.DateOfSubmission = time;
                    try
                    {
                        IGeoCoder geocode = new GoogleGeoCoder("ABQIAAAAbyfszEVR0VTKZImYRp5b6BS9l0G0i7V22ZGVaQxYRD7DXNsCeRQYuExgpEMwCaudHBGK5MIz8RlXCg"); //key for maximzaslavsky.com
                        var addresses = geocode.GeoCode(listing.StreetAddress);
                        if (addresses.Length < 1)
                            throw new ApplicationException();
                        else
                        {
                            listing.Lat = (decimal)addresses[0].Coordinates.Latitude;
                            listing.Long = (decimal)addresses[0].Coordinates.Longitude;
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("Address", "Sorry, but we couldn't find this location. Are you sure it's correct?");
                        ModelState.SetModelValue("Address", new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
                        return View();
                    }
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
                    db.PianoReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    if (!r.IsValid)
                    {
                        throw new Exception(); //just in case insertonsubmit doesn't throw exception correctly
                    }
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (PianoVenueHour hour in hours)
                    {
                        hour.PianoReviewRevision = r;
                        //TODO: How will DayOfWeek be binded???
                        db.PianoVenueHours.InsertOnSubmit(hour);
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
