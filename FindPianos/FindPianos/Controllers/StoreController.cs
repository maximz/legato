using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;
using System.Globalization;
using RiaLibrary.Web;
using System.Web.Security;
using FindPianos.Helpers;
using System.Net;
using FindPianos.ViewModels;
using System.Web.Routing;

namespace FindPianos.Controllers
{
    [HandleError]
    public class StoreController : CustomControllerBase
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (ViewData["CurrentMenuItem"].ToString().IsNullOrEmpty())
            {
                ViewData["CurrentMenuItem"] = "Stores";
            }
        }
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "None")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        #region Read Listings and Reviews
        [Url("Stores/View/{listingId}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "listingId")]
        public ActionResult Read(long listingId)
        {
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var listing = data.StoreListings.Where(l => l.StoreListingID== listingId).Single();
                    listing.FillProperties();
                    var reviews = data.StoreReviews.Where(r => r.StoreID == listingId).ToList();
                    foreach (var r in reviews)
                    {
                        r.FillProperties();
                    }
                    var model = new ReadStoreListingViewModel
                    {
                        Listing = listing,
                        Reviews = reviews
                    };
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
        }
        
        [Url("Stores/Review/View/{reviewId}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "reviewId")]
        public ActionResult IndividualReview(long reviewId)
        {
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var review = data.StoreReviews.Where(r => r.ReviewID == reviewId).Single();
                    review.FillProperties();
                    var listing = review.StoreListing;
                    listing.FillProperties();

                    var model = new ReadStoreListingViewModel()
                    {
                        Listing = listing,
                        Reviews = new List<StoreReview>()
                    };
                    model.Reviews.Add(review);
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
        }
        #endregion

        #region Individual Review timeline- and revision-listing method
        [Url("Stores/Review/Timeline/{reviewId}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "reviewId")]
        public ActionResult ReviewTimeline(long reviewId)
        {
            try
            {
                using (var data = new LegatoDataContext())
                {
                    var review = data.StoreReviews.Where(r => r.ReviewID == reviewId).Single();
                    review.Revisions = data.StoreReviewRevisions.Where(rev => rev.ReviewID == review.ReviewID).OrderByDescending(rev => rev.EditNumber).ToList();
                    return View(review);
                }
            }
            catch
            {
                return RedirectToAction("NotFound","Error");
            }
        }
        #endregion

        #region Searching Methods
        [Url("Search/Stores")][CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "None")]
        public ActionResult List()
        {
            return View();
        }
        [Url("Stores/EnumerateBox")]
        [HttpPost][VerifyReferrer][CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "*")]
        public ActionResult AjaxSearchMapFill(decimal lat1, decimal long1, decimal lat2, decimal long2)
        {
            var results = StoreListing.ProcessAjaxMapSearch(new BoundingBox()
            {
                extent1 = new LatLong() { latitude = lat1, longitude = long1 },
                extent2 = new LatLong() { latitude = lat2, longitude = long2 }
            });
            return Json(results);
        }
        #endregion

        #region Submission and Editing methods
        [Url("Stores/Create")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name="StoreSubmitGET", Seconds=600)]
        public ActionResult Submit()
        {
            return View(new StoreSubmitViewModel());
        }
        [Url("Stores/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [RateLimit(Name="StoreSubmitPOST", Seconds=600)]
        public ActionResult Submit(StoreSubmitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //View info:
            //http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx = pianovenuehours binding
            //as there are multiple parameters, we'll just have to have multiple <form>s (one per parameter/object) in the View
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;

                    //LISTING:
                    var listing = new StoreListing();

                    listing.Address = model.Listing.StreetAddress;
                    listing.Lat = model.Listing.Lat;
                    listing.Long = model.Listing.Long;

                    listing.Name = model.Listing.Name;
                    listing.Description = model.Listing.Description;

                    listing.IsSubmitterAffiliatedWithStore = model.Listing.IsSubmitterAffiliatedWithStore;

                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    listing.SubmitterUserID = userGuid;
                    listing.DateOfSubmission = time;

                    db.StoreListings.InsertOnSubmit(listing);
                    db.SubmitChanges();

                    //REVIEW:
                    var review = new StoreReview();
                    review.StoreListing = listing;
                    db.StoreReviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    //REVISION:
                    var r = new StoreReviewRevision();
                    r.DateOfLastVisit = model.ReviewRevision.DateOfLastVisit;
                    r.DateOfLastPurchase = model.ReviewRevision.DateOfLastPurchase;
                    r.Message = model.ReviewRevision.Message;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingProductQuality= model.ReviewRevision.RatingProductQuality;
                    r.RatingService = model.ReviewRevision.RatingService;
                    r.RatingEnvironment = model.ReviewRevision.RatingEnvironment;
                    r.RevisionDate = time;
                    r.StoreReview = review;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.EditNumber = 1;
                    db.StoreReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new StoreVenueHour();
                        submit.DayOfWeek = hour.DayOfWeekId;
                        if(!hour.Closed)
                        {
                            submit.StartTime = hour.StartTime;
                            submit.EndTime = hour.EndTime;
                        }
                        else
                        {
                            submit.StartTime = null;
                            submit.EndTime = null;
                        }
                        submit.StoreReviewRevision = r;
                        db.StoreVenueHours.InsertOnSubmit(submit);
                    }
                    db.SubmitChanges();
                    return RedirectToAction("Read", new { id = listing.StoreListingID }); //shows details for that submission thread, with only one revision!
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpPost][VerifyReferrer]
        [Url("Stores/Review")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [RateLimit(Name = "StoresCreateReviewPOST", Seconds = 600)]
        public ActionResult Reply(StoreReplyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("InternalServerError", "Error");
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc

                    var listing = db.StoreListings.Where(l => l.StoreListingID == model.ListingID).SingleOrDefault();
                    if (listing == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //REVIEW:
                    var review = new StoreReview();
                    review.StoreListing = listing;
                    db.StoreReviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    //REVISION:
                    var r = new StoreReviewRevision();
                    r.DateOfLastVisit = model.ReviewRevision.DateOfLastVisit;
                    r.DateOfLastPurchase = model.ReviewRevision.DateOfLastPurchase;
                    r.Message = model.ReviewRevision.Message;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingProductQuality = model.ReviewRevision.RatingProductQuality;
                    r.RatingService = model.ReviewRevision.RatingService;
                    r.RatingEnvironment = model.ReviewRevision.RatingEnvironment;
                    r.RevisionDate = time;
                    r.StoreReview = review;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.EditNumber = 1;
                    db.StoreReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new StoreVenueHour();
                        submit.DayOfWeek = hour.DayOfWeekId;
                        if (!hour.Closed)
                        {
                            submit.StartTime = hour.StartTime;
                            submit.EndTime = hour.EndTime;
                        }
                        else
                        {
                            submit.StartTime = null;
                            submit.EndTime = null;
                        }
                        submit.StoreReviewRevision = r;
                        db.StoreVenueHours.InsertOnSubmit(submit);
                    }
                    db.SubmitChanges();

                    return RedirectToAction("IndividualReview", new
                    {
                        reviewId = review.ReviewID
                    });
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [Url("Stores/Review/Edit/{reviewId}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name = "StoreReviewEditGET", Seconds = 600)]
        public ActionResult EditReview(long reviewId)
        {
            //edit an individual review
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var query = db.StoreReviewRevisions.Where(r => r.ReviewID == reviewId).OrderByDescending(r => r.EditNumber);
                    var revision = query.First();
                    var submitterGuid = query.Last().UserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }
                    var listing = db.StoreListings.Where(l => l.StoreListingID == revision.StoreReview.StoreID).Single();
                    var hours = db.StoreVenueHours.Where(h => h.StoreReviewRevisionID == revision.StoreReviewRevisionID).ToList();
                    var revisionmodel = new StoreRevisionSubmissionViewModel()
                    {
                        DateOfLastVisit = revision.DateOfLastVisit,
                        DateOfLastPurchase = revision.DateOfLastPurchase,
                        Message = revision.Message,
                        RatingOverall = revision.RatingOverall,
                        RatingProductQuality= (int)revision.RatingProductQuality,
                        RatingEnvironment= (int)revision.RatingEnvironment,
                        RatingService = (int)revision.RatingService,
                        ReviewId = (int)revision.ReviewID
                    };
                    var hourmodel = new List<StoreHourViewModel>();
                    foreach (var h in hours)
                    {
                        var item = new StoreHourViewModel();
                        item.DayOfWeekId = h.WeekDay.WeekDayID;
                        item.DayOfWeekName = h.WeekDay.WeekDayName;
                        if (h.StartTime == null || !h.StartTime.HasValue || h.StartTime == DateTime.MinValue) //we only need to check one of them, because if one's null the other one is, too
                        {
                            item.StartTime = DateTime.MinValue;
                            item.EndTime = DateTime.MinValue;
                            item.Closed = true;
                        }
                        else
                        {
                            item.Closed = false;
                            item.StartTime = (DateTime)h.StartTime;
                            item.EndTime = (DateTime)h.EndTime;
                        }
                        hourmodel.Add(item);
                    }
                    var listingmodel = new ReadStoreListingViewModel()
                    {
                        Listing = listing,
                        Reviews = null
                    };

                    var model = new StoreEditViewModel()
                    {
                        ReviewRevision = revisionmodel,
                        Hours = hourmodel,
                        Listing = listingmodel
                    };
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("NotFound", "Error");
            }
        }
        [Url("Stores/Review/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [RateLimit(Name = "StoreReviewEditPOST", Seconds = 600)]
        public ActionResult EditReview(StoreEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    try
                    {
                        //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                        var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                        var submitterGuid = db.ReviewRevisions.Where(revisionforcheck => revisionforcheck.ReviewID == model.ReviewRevision.ReviewId).OrderBy(revisionforcheck => revisionforcheck.RevisionNumberOfReview).First().SubmitterUserID;
                        if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                        {
                            return RedirectToAction("Forbidden", "Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                   
                    var time = DateTime.Now;

                    //REVISION:
                    var r = new StoreReviewRevision();
                    r.DateOfLastPurchase = model.ReviewRevision.DateOfLastPurchase;
                    r.DateOfLastVisit = model.ReviewRevision.DateOfLastVisit;
                    r.Message = model.ReviewRevision.Message;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingEnvironment = model.ReviewRevision.RatingEnvironment;
                    r.RatingProductQuality= model.ReviewRevision.RatingProductQuality;
                    r.RatingService = model.ReviewRevision.RatingService;
                    r.RevisionDate = time;
                    r.ReviewID = model.ReviewRevision.ReviewId;
                    r.EditNumber = (from rev in db.StoreReviewRevisions
                                    where rev.ReviewID == model.ReviewRevision.ReviewId
                                    select rev.EditNumber).Max() + 1;
                    db.StoreReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new StoreVenueHour();
                        submit.DayOfWeek = hour.DayOfWeekId;
                        if (!hour.Closed)
                        {
                            submit.StartTime = hour.StartTime;
                            submit.EndTime = hour.EndTime;
                        }
                        else
                        {
                            submit.StartTime = null;
                            submit.EndTime = null;
                        }
                        submit.StoreReviewRevision = r;
                        db.StoreVenueHours.InsertOnSubmit(submit);
                    }
                    db.SubmitChanges();
                    return RedirectToAction("ReviewTimeline", new { reviewId = model.ReviewRevision.ReviewId});
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion
        #region AJAX: Flag Listings and Reviews
        [RateLimit(Name="StoreFlagListingPOST", Seconds=120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [Url("Stores/Listing/Flag")]
        public ActionResult AjaxFlagListing(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.StoreListings.Where(l => l.StoreListingID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new StoreListingFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ListingID = idOfPost;
                    db.StoreListingFlags.InsertOnSubmit(flag);
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
        [RateLimit(Name = "StoreFlagReviewPOST", Seconds = 120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [Url("Stores/Review/Flag")]
        public ActionResult AjaxFlagReview(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.StoreReviews.Where(l => l.ReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new StoreReviewFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ReviewID = idOfPost;
                    db.StoreReviewFlags.InsertOnSubmit(flag);
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
        [RateLimit(Name = "StoreListingCommentPOST", Seconds = 120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [Url("Stores/Listing/Comment")]
        public ActionResult AjaxCommentListing(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.StoreListings.Where(l => l.StoreListingID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new StoreListingComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.ListingID = idOfPost;
                    db.StoreListingComments.InsertOnSubmit(comment);
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

        [RateLimit(Name = "StoreReviewCommentPOST", Seconds = 120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [Url("Stores/Review/Comment")]
        public ActionResult AjaxCommentReview(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.StoreReviews.Where(l => l.ReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new StoreReviewComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.ReviewID = idOfPost;
                    db.StoreReviewComments.InsertOnSubmit(comment);
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
