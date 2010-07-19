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
        [OutputCache(Duration = 7200, VaryByParam = "listingId")]
        public ActionResult Read(long listingId)
        {
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var listing = data.Listings.Where(l => l.ListingID == listingId).Single();
                    listing.FillProperties();
                    var reviews = data.Reviews.Where(r => r.ListingID == listingId).ToList();
                    foreach (var r in reviews)
                    {
                        r.FillProperties();
                    }
                    var model = new ReadListingViewModel
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
        
        [Url("Review/View/{reviewId}")]
        [OutputCache(Duration = 7200, VaryByParam = "reviewId")]
        public ActionResult IndividualReview(long reviewId)
        {
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var review = data.Reviews.Where(r => r.ReviewID == reviewId).Single();
                    review.FillProperties();

                    var listing = review.Listing;
                    listing.FillProperties();

                    var model = new ReadListingViewModel()
                    {
                        Listing = listing,
                        Reviews = new List<Review>()
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
        [Url("Review/Timeline/{reviewId}")]
        [OutputCache(Duration = 7200, VaryByParam = "reviewId")]
        public ActionResult ReviewTimeline(long reviewId)
        {
            try
            {
                using (var data = new LegatoDataContext())
                {
                    var review = data.Reviews.Where(r => r.ReviewID == reviewId).Single();
                    review.Revisions = data.ReviewRevisions.Where(rev => rev.ReviewID == review.ReviewID).OrderByDescending(rev => rev.RevisionNumberOfReview).ToList();
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
        [Url("Search")][OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult List()
        {
            return View();
        }
        [Url("Search/EnumerateBox")]
        [HttpPost][OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult AjaxSearchMapFill(decimal lat1, decimal long1, decimal lat2, decimal long2)
        {
            using (var db = new LegatoDataContext())
            {
                var results = db.ProcessAjaxMapSearch(new BoundingBox()
            {
                extent1 = new LatLong() { latitude = lat1, longitude = long1 },
                extent2 = new LatLong() { latitude = lat2, longitude = long2 }
            });
                return Json(results);
            }

        }
        #endregion

        #region Submission and Editing methods
        [Url("Listing/Create")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name="ListingSubmitGET", Seconds=600)]
        public ActionResult Submit()
        {
            //TODO: load styles and types into the model; or rather, don't. that will be Ajax.
            return View(new SubmitViewModel());
        }
        [Url("Listing/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name="ListingSubmitPOST", Seconds=600)]
        public ActionResult Submit(SubmitViewModel model)
        {
            //View info:
            //http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx = pianovenuehours binding
            //as there are multiple parameters, we'll just have to have multiple <form>s (one per parameter/object) in the View
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;

                    //LISTING:
                    var listing = new Listing();

                    listing.StreetAddress = model.Listing.StreetAddress;
                    listing.Lat = model.Listing.Lat;
                    listing.Long = model.Listing.Long;
                    listing.InstrumentBrand = model.Listing.Equipment.Brand.Trim();
                    if(model.Listing.Equipment.Model.IsNullOrEmpty())
                        listing.InstrumentModel = null;
                    else
                        listing.InstrumentModel = model.Listing.Equipment.Model.Trim();

                    /*Matching instrument and style:
                     * 1. take instrument name, find match in Instruments table
                     * 2. apply SelectedIndex of type to dropdownlist, extract name from the list
                     * 3. Match name to a record in InstrumentTypes with InstrumentID from step 1 and Name from step 2
                     * 4. Apply ID of record in #3 to Listing
                     * 5. Same for Styles
                     * that's how we do it! */
                    var instrument = db.Instruments.Where(i => i.Name == model.Listing.InstrumentName).SingleOrDefault();
                    if(instrument==null)
                    {
                        ModelState.AddModelError("InstrumentName", "No such instrument exists.");
                        return View();
                    }
                    var style = model.Listing.Equipment.Styles.ElementAtOrDefault(model.Listing.Equipment.SelectedStyle);
                    if(style==null)
                    {
                        ModelState.AddModelError("Style", "No such style exists.");
                        return View();
                    }
                    var modelStyle = db.InstrumentStyles.Where(s => s.InstrumentID == instrument.InstrumentID && s.StyleName == style.Value).SingleOrDefault(); //TODO: is it style.Value or style.Text?
                    if(modelStyle==null)
                    {
                        ModelState.AddModelError("Style", "No such style exists.");
                        return View();
                    }
                    listing.InstrumentStyleID = modelStyle.StyleID;
                    style = null;
                    modelStyle = null;
                    var type = model.Listing.Equipment.Types.ElementAtOrDefault(model.Listing.Equipment.SelectedType);
                    if (type == null)
                    {
                        ModelState.AddModelError("Type", "No such type exists.");
                        return View();
                    }
                    var modelType = db.InstrumentTypes.Where(t => t.InstrumentID == instrument.InstrumentID && t.TypeName == type.Value).SingleOrDefault(); //TODO: is it type.Value or type.Text?
                    if (modelType == null)
                    {
                        ModelState.AddModelError("Type", "No such type exists.");
                        return View();
                    }
                    listing.InstrumentTypeID = modelType.TypeID;
                    type = null;
                    modelType = null;

                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    listing.OriginalSubmitterUserID = userGuid;
                    listing.DateOfSubmission = time;

                    db.Listings.InsertOnSubmit(listing);
                    db.SubmitChanges();

                    //REVIEW:
                    var review = new Review();
                    review.Listing = listing;
                    db.Reviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    //REVISION:
                    var r = new ReviewRevision();
                    r.DateOfLastUsageOfPianoBySubmitter = model.ReviewRevision.DateOfLastUsage;
                    r.Message = model.ReviewRevision.Message;
                    r.PricePerHourInUSD = model.ReviewRevision.PricePerHour;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingPlayingCapability = model.ReviewRevision.RatingPlayingCapability;
                    r.RatingToneQuality = model.ReviewRevision.RatingToneQuality;
                    r.RatingTuning = model.ReviewRevision.RatingTuning;
                    r.VenueName = model.ReviewRevision.VenueName;
                    r.DateOfRevision = time;
                    r.Review = review;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.RevisionNumberOfReview = 1;
                    db.ReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new VenueHour();
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
                        submit.ReviewRevision = r;
                        db.VenueHours.InsertOnSubmit(submit);
                    }
                    db.SubmitChanges();
                    return RedirectToAction("Read", new { id = listing.ListingID}); //shows details for that submission thread, with only one revision!
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpPost]
        [Url("Review/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [RateLimit(Name = "ListingReplyPOST", Seconds = 600)]
        public ActionResult Reply(ReplyViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc

                    var listing = db.Listings.Where(l => l.ListingID == model.ListingID).SingleOrDefault();
                    if(listing==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //REVIEW:
                    var review = new Review();
                    review.Listing = listing;
                    db.Reviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    //REVISION:
                    var r = new ReviewRevision();
                    r.DateOfLastUsageOfPianoBySubmitter = model.ReviewRevision.DateOfLastUsage;
                    r.Message = model.ReviewRevision.Message;
                    r.PricePerHourInUSD = model.ReviewRevision.PricePerHour;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingPlayingCapability = model.ReviewRevision.RatingPlayingCapability;
                    r.RatingToneQuality = model.ReviewRevision.RatingToneQuality;
                    r.RatingTuning = model.ReviewRevision.RatingTuning;
                    r.VenueName = model.ReviewRevision.VenueName;
                    r.DateOfRevision = time;
                    r.Review = review;
                    r.RevisionNumberOfReview = 1;
                    db.ReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new VenueHour();
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
                        submit.ReviewRevision = r;
                        db.VenueHours.InsertOnSubmit(submit);
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
        [Url("Review/Edit/{reviewId}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name = "ListingEditGET", Seconds = 600)]
        public ActionResult Edit(long reviewId)
        {
            //edit an individual review
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var query = db.ReviewRevisions.Where(r => r.ReviewID == reviewId).OrderByDescending(r => r.RevisionNumberOfReview);
                    var revision = query.First();
                    var submitterGuid = query.Last().SubmitterUserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }
                    var listing = db.Listings.Where(l => l.ListingID == revision.Review.ListingID).Single();
                    var hours = db.VenueHours.Where(h => h.ReviewRevisionID == revision.ReviewRevisionID).ToList();
                    var revisionmodel = new RevisionSubmissionViewModel()
                    {
                        DateOfLastUsage=revision.DateOfLastUsageOfPianoBySubmitter,
                        Message=revision.Message,
                        PricePerHour=revision.PricePerHourInUSD,
                        RatingOverall=revision.RatingOverall,
                        RatingPlayingCapability=(int)revision.RatingPlayingCapability,
                        RatingToneQuality=(int)revision.RatingToneQuality,
                        RatingTuning=(int)revision.RatingTuning,
                        ReviewId=(int)revision.ReviewID,
                        VenueName=revision.VenueName
                    };
                    var hourmodel = new List<VenueHourViewModel>();
                    foreach(var h in hours)
                    {
                        var item = new VenueHourViewModel();
                        item.DayOfWeekId = h.WeekDay.WeekDayID;
                        item.DayOfWeekName = h.WeekDay.WeekDayName;
                        if(h.StartTime==null||!h.StartTime.HasValue||h.StartTime==DateTime.MinValue) //we only need to check one of them, because if one's null the other one is, too
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
                    var listingmodel = new ReadListingViewModel()
                    {
                        Listing=listing,
                        Reviews=null
                    };

                    var model = new EditViewModel()
                    {
                        ReviewRevision=revisionmodel,
                        Hours=hourmodel,
                        Listing=listingmodel
                    };
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("NotFound", "Error");
            }
        }
        [Url("Review/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name = "ListingEditPOST", Seconds = 600)]
        public ActionResult Edit(EditViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    try
                    {
                        //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                        var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                        var submitterGuid = db.ReviewRevisions.Where(revisionforcheck => revisionforcheck.ReviewID == model.ReviewRevision.ReviewId).OrderBy(revisionforcheck=>revisionforcheck.RevisionNumberOfReview).First().SubmitterUserID;
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
                    var r = new ReviewRevision();
                    r.DateOfLastUsageOfPianoBySubmitter = model.ReviewRevision.DateOfLastUsage;
                    r.Message = model.ReviewRevision.Message;
                    r.PricePerHourInUSD = model.ReviewRevision.PricePerHour;
                    r.RatingOverall = model.ReviewRevision.RatingOverall;
                    r.RatingPlayingCapability = model.ReviewRevision.RatingPlayingCapability;
                    r.RatingToneQuality = model.ReviewRevision.RatingToneQuality;
                    r.RatingTuning = model.ReviewRevision.RatingTuning;
                    r.VenueName = model.ReviewRevision.VenueName;
                    r.DateOfRevision = time;
                    r.ReviewID = model.ReviewRevision.ReviewId;
                    r.RevisionNumberOfReview = (from rev in db.ReviewRevisions
                                                where rev.ReviewID == model.ReviewRevision.ReviewId
                                                select rev.RevisionNumberOfReview).Max() + 1;
                    db.ReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new VenueHour();
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
                        submit.ReviewRevision = r;
                        db.VenueHours.InsertOnSubmit(submit);
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
        [RateLimit(Name="ListingFlagListingPOST", Seconds=120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [Url("Listing/Flag")]
        public ActionResult AjaxFlagListing(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.Listings.Where(l => l.ListingID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new ListingFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ListingID = idOfPost;
                    db.ListingFlags.InsertOnSubmit(flag);
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
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [Url("Review/Flag")]
        public ActionResult AjaxFlagReview(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.Reviews.Where(l => l.ReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new ReviewFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.ReviewID = idOfPost;
                    db.ReviewFlags.InsertOnSubmit(flag);
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
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [Url("Listing/Comment")]
        public ActionResult AjaxCommentListing(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.Listings.Where(l => l.ListingID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new ListingComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.ListingID = idOfPost;
                    db.ListingComments.InsertOnSubmit(comment);
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
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [Url("Review/Comment")]
        public ActionResult AjaxCommentReview(long idOfPost, string commentText)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given review exists before creating a possibly-useless record
                    if (db.Reviews.Where(l => l.ReviewID == idOfPost).Count() != 1)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var comment = new ReviewComment();
                    comment.DateOfSubmission = DateTime.Now;
                    comment.AuthorUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    comment.MessageText = commentText;
                    comment.ReviewID = idOfPost;
                    db.ReviewComments.InsertOnSubmit(comment);
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
