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
using FindPianos.Helpers;
using System.Net;

namespace FindPianos.Controllers
{
    [HandleError]
    public class ListingController : Controller
    {
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        #region Read Listings and Reviews
        [Url("Listing/View/{listingId}")]
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult Read(long listingId)
        {
            using (var data = new PianoDataContext())
            {
                try
                {
                    var listing = data.PianoListings.Where(l => l.PianoID == listingId).Single();
                    listing.FillProperties();
                    var reviews = data.PianoReviews.Where(r => r.PianoListingID == listingId).ToList();
                    foreach (var r in reviews)
                    {
                        r.FillProperties();
                    }
                    ViewData["listing"] = listing;
                    ViewData["reviews"] = reviews;
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
                return View();
            }
        }
        
        [Url("Review/View/{reviewId}")]
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult IndividualReview(long reviewId)
        {
            using (var data = new PianoDataContext())
            {
                try
                {
                    var listing = data.PianoListings.Where(l => l.PianoID == reviewId).Single();
                    listing.FillProperties();
                    var review = data.PianoReviews.Where(r => r.PianoListingID == reviewId).Single();
                    review.FillProperties();
                    ViewData["listing"] = listing;
                    ViewData["review"] = review;
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
                return View();
            }
        }
        #endregion

        #region Individual Review timeline- and revision-listing method
        [Url("Review/Timeline/{reviewId}")]
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult ReviewTimeline(long reviewId)
        {
            try
            {
                using (var data = new PianoDataContext())
                {
                    var review = data.PianoReviews.Where(r => r.PianoReviewID == reviewId).Single();
                    review.Revisions = data.PianoReviewRevisions.Where(rev => rev.PianoReviewID == review.PianoReviewID).OrderByDescending(rev => rev.RevisionNumberOfReview).ToList();
                    ViewData["review"] = review;
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("NotFound","Error");
            }
        }
        #endregion

        #region Searching Methods
        [Url("Search")][OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult List()
        {
            return View();
        }
        [Url("Search/EnumerateBox")]
        [HttpPost][OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult AjaxSearchMapFill(decimal lat1, decimal long1, decimal lat2, decimal long2)
        {
            using (var db = new PianoDataContext())
            {
                var results = db.ProcessAjaxMapSearch(new BoundingBox()
            {
                extent1 = new LatLong() { latitude = lat1, longitude = long1 },
                extent2 = new LatLong() { latitude = lat2, longitude = long2 }
            });
                return Json(results);
            }

        }
        //[Url("Search")]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult List(SearchForm s)
        //{
        //    //validate


        //    //execute search
            

        //    using (var db = new PianoDataContext())
        //    {
        //        ViewData["listings"] = db.ProcessSearchForm(s);
        //    }
        //    return View();
        //}

        #endregion

        #region Submission and Editing methods
        [Url("Listing/Create")]
        [HttpGet]
        [AwesomeAuthorize(AuthorizeSuspended=false)]
        [RateLimit(Name="ListingSubmitGET", Seconds=600)]
        public ActionResult Submit()
        {
            return View();
        }
        [Url("Listing/Create")]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [RateLimit(Name="ListingSubmitPOST", Seconds=600)]
        public ActionResult Submit([Bind(Exclude = "PianoReviewRevisionID, PianoReviewID, DateOfRevision, RevisionNumberOfReview")]PianoReviewRevision r, [Bind(Exclude="PianoID, Lat, Long, OriginalSubmitterUserID, DateOfSubmission")]PianoListing listing, [Bind(Exclude="ReviewRevisionID,VenueHoursID")]ICollection<PianoVenueHour> hours)
        {
            //View info:
            //http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx = pianovenuehours binding
            //as there are multiple parameters, we'll just have to have multiple <form>s (one per parameter/object) in the View
            try
            {
                using (var db = new PianoDataContext())
                {
                    var time = DateTime.Now;

                    //LISTING:
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
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
        [Url("Review/Edit/{reviewId}")]
        [HttpGet]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [RateLimit(Name = "ListingEditGET", Seconds = 600)]
        public ActionResult Edit(long reviewId)
        {
            using(var db = new PianoDataContext())
            {
                //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                var revisions = db.PianoReviewRevisions.Where(r => r.PianoReviewID == reviewId).ToList();
                var submitterGuid = revisions[0].SubmitterUserID;
                if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewData["revisions"] = revisions;
                return View();
            }
        }
        [Url("Review/Edit")]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [RateLimit(Name = "ListingEditPOST", Seconds = 600)]
        public ActionResult Edit(long reviewId, [Bind(Exclude = "PianoReviewRevisionID, PianoReviewID, DateOfRevision, RevisionNumberOfReview")]PianoReviewRevision r, [Bind(Exclude = "ReviewRevisionID,VenueHoursID")]ICollection<PianoVenueHour> hours)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var revisions = db.PianoReviewRevisions.Where(revisionforcheck => revisionforcheck.PianoReviewID == reviewId).Take(1).ToList();
                    var submitterGuid = revisions[0].SubmitterUserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
                    var time = DateTime.Now;

                    //REVISION:
                    r.DateOfRevision = time;
                    r.PianoReviewID = reviewId;
                    r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                                                where rev.PianoReviewID == reviewId
                                                select rev.RevisionNumberOfReview).Max() + 1;
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
        #endregion
        #region AJAX: Flag Listings and Reviews
        [RateLimit(Name="ListingFlagListingPOST", Seconds=120)]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [Url("Listing/Flag")]
        public ActionResult AjaxFlagListing(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.PianoListings.Where(l => l.PianoID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new PianoListingFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ListingID = idOfPost;
                    db.PianoListingFlags.InsertOnSubmit(flag);
                    db.SubmitChanges();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new EmptyResult();
                }
            }
            catch
            {
                /* on jQuery side:
                  
                    if error = code 500, we have reached here.
                 * 
                    if error = code 409 (conflict), user has failed rate limit check.*/
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new EmptyResult();
            }

        }
        [RateLimit(Name = "ListingFlagReviewPOST", Seconds = 120)]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [Url("Review/Flag")]
        public ActionResult AjaxFlagReview(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.PianoReviews.Where(l => l.PianoReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new PianoReviewFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ReviewID = idOfPost;
                    db.PianoReviewFlags.InsertOnSubmit(flag);
                    db.SubmitChanges();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new EmptyResult();
                }
            }
            catch
            {
                /* on jQuery side:
                  
                    if error = code 500, we have reached here.
                 * 
                    if error = code 409 (conflict), user has failed rate limit check.*/
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new EmptyResult();
            }

        }
        #endregion

        #region AJAX: Comment on Listings and Reviews
        [RateLimit(Name = "ListingCommentListingPOST", Seconds = 120)]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [Url("Listing/Comment")]
        public ActionResult AjaxCommentListing(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.PianoListings.Where(l => l.PianoID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new PianoListingComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.PianoListingID = idOfPost;
                    db.PianoListingComments.InsertOnSubmit(comment);
                    db.SubmitChanges();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new EmptyResult();
                }
            }
            catch
            {
                /* on jQuery side:
                  
                    if error = code 500, we have reached here.
                 * 
                    if error = code 409 (conflict), user has failed rate limit check.*/
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new EmptyResult();
            }

        }

        [RateLimit(Name = "ListingCommentReviewPOST", Seconds = 120)]
        [AwesomeAuthorize(AuthorizeSuspended = false)]
        [HttpPost]
        [Url("Review/Comment")]
        public ActionResult AjaxCommentReview(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.PianoReviews.Where(l => l.PianoReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new PianoReviewComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.PianoReviewID = idOfPost;
                    db.PianoReviewComments.InsertOnSubmit(comment);
                    db.SubmitChanges();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new EmptyResult();
                }
            }
            catch
            {
                /* on jQuery side:
                  
                    if error = code 500, we have reached here.
                 * 
                    if error = code 409 (conflict), user has failed rate limit check.*/
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new EmptyResult();
            }

        }

        #endregion
    }
}
