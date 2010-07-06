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
                    var listing = data.Listings.Where(l => l.ListingID == reviewId).Single();
                    listing.FillProperties();
                    var review = data.Reviews.Where(r => r.ListingID == reviewId).Single();
                    review.FillProperties();
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
            //TODO: load styles and types into the model; or rather, don't. that will be ajax.
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
                    try
                    {
                        var addresses = Geocoder.CallGeoWS(listing.StreetAddress);
                        if (addresses.Status == "ZERO_RESULTS")
                            return RedirectToAction("InternalServerError", "Error");
                        else
                        {
                            listing.Lat = (decimal)addresses.Results[0].Geometry.Location.Lat;
                            listing.Long = (decimal)addresses.Results[0].Geometry.Location.Lng;
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("Address", "Sorry, but we couldn't find this location. Are you sure it's correct?");
                        ModelState.SetModelValue("Address", new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
                        return View();
                    }
                    db.Listings.InsertOnSubmit(listing);
                    db.SubmitChanges();

                    //REVIEW:
                    var review = new PianoReview();
                    review.Listing = listing;
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
                return RedirectToAction("Read", new { id = r.PianoReview.ListingID }); //shows details for that submission thread, with only one revision!

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
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name = "ListingEditGET", Seconds = 600)]
        public ActionResult Edit(long reviewId)
        {
            using(var db = new LegatoDataContext())
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
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name = "ListingEditPOST", Seconds = 600)]
        public ActionResult Edit(long reviewId, [Bind(Exclude = "PianoReviewRevisionID, PianoReviewID, DateOfRevision, RevisionNumberOfReview")]PianoReviewRevision r, [Bind(Exclude = "ReviewRevisionID,VenueHoursID")]ICollection<PianoVenueHour> hours)
        {
            try
            {
                using (var db = new LegatoDataContext())
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
                return RedirectToAction("Read", new { id = r.PianoReview.ListingID }); //shows details for that submission thread, with only one revision!

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
