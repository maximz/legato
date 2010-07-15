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
    public class DiscussController : Controller
    {
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        #region Read Threads and Posts
        [Url("Discuss/Thread/{threadID}")]
        [OutputCache(Duration = 7200, VaryByParam = "threadID")]
        public ActionResult ReadThread(long threadID)
        {
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var thread = data.DiscussThreads.Where(t => t.ThreadID == threadID).Single();
                    //thread.FillProperties();
                    var posts = data.DiscussPosts.Where(p => p.ThreadID == threadID).OrderBy(p=>p.DateOfSubmission).ToList();
                    //foreach (var p in posts)
                    //{
                    //    p.FillProperties();
                    //}
                    var model = new DiscussThreadViewModel
                    {
                        Thread = thread,
                        Posts = posts
                    };
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
        }
        
        [Url("Discuss/Thread/{threadID}/{postID}")]
        [OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult IndividualReview(long threadID, long postID)
        {
            //TODO - how do we automatically page and scroll to a specific place?
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var post = data.DiscussPosts.Where(p => p.PostID == postID).Single();
                    //post.FillProperties();

                    var thread = post.Thread;
                    //thread.FillProperties();

                    //Get the page number that the post is on
                    //Get posts for that page, return them

                    var model = new DiscussThreadViewModel
                    {
                        Thread = thread,
                        Posts = new List<Review>()
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

        #region Individual Post timeline- and revision-listing method
        [Url("Discuss/Post/Timeline/{postID}")]
        [OutputCache(Duration = 7200, VaryByParam = "postID")]
        public ActionResult PostTimeline(long postID)
        {
            try
            {
                using (var data = new LegatoDataContext())
                {
                    var post = data.DiscussPosts.Where(p => p.PostID == postID).Single();
                    post.Revisions = data.DiscussPostRevisions.Where(rev => rev.PostID == postID).OrderByDescending(rev => rev.EditNumber).ToList();
                    return View(post);
                }
            }
            catch
            {
                return RedirectToAction("NotFound","Error");
            }
        }
        #endregion

        #region Searching Methods
        [Url("Search/Discuss")][OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult List()
        {
            return View();
        }
        [Url("Discuss/EnumerateBox")]
        [HttpPost][OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult AjaxSearchMapFill(decimal lat1, decimal long1, decimal lat2, decimal long2)
        {
            using (var db = new LegatoDataContext())
            {
                var results = DiscussPost.ProcessAjaxMapSearch(new BoundingBox()
            {
                extent1 = new LatLong() { latitude = lat1, longitude = long1 },
                extent2 = new LatLong() { latitude = lat2, longitude = long2 }
            });
                return Json(results);
            }

        }
        #endregion

        #region Submission and Editing methods
        [Url("Discuss/Board/{boardID}/Create")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name="DiscussSubmitGET", Seconds=600)]
        public ActionResult Submit(long boardID)
        {
            var model = new DiscussSubmitViewModel()
            {
                BoardID=boardID
            };
            return View(model);
        }
        [Url("Discuss/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name="DiscussSubmitPOST", Seconds=600)]
        public ActionResult Submit(DiscussSubmitViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;

                    //THREAD:
                    var thread = new DiscussThread();

                    //listing.StreetAddress = model.Listing.StreetAddress;
                    //listing.Lat = model.Listing.Lat;
                    //listing.Long = model.Listing.Long;
                    //listing.InstrumentBrand = model.Listing.Equipment.Brand.Trim();
                    //if(model.Listing.Equipment.Model.IsNullOrEmpty())
                        //listing.InstrumentModel = null;
                    //else
                        //listing.InstrumentModel = model.Listing.Equipment.Model.Trim();


                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    thread.OriginalSubmitterUserID = userGuid;
                    thread.DateOfSubmission = time;
                    thread.Title = model.Title;

                    db.DiscussThreads.InsertOnSubmit(thread);
                    db.SubmitChanges();

                    //POST:
                    var post = new DiscussPost();
                    post.Thread = thread;
                    db.DiscussPosts.InsertOnSubmit(post);
                    db.SubmitChanges();

                    //POST REVISION:
                    var r = new DiscussPostRevision();
                    r.Markdown = model.Markdown;
                    r.HTML = HtmlUtilities.Safe(Markdown.Convert(model.Markdown));
                    r.Address = model.Address;
                    r.Lat = model.Lat;
                    r.Long = model.Long;
                    r.DateOfRevision = time;
                    r.Post = post;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.EditNumber = 1;
                    db.DiscussPostRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();
                    
                    return RedirectToAction("Read", new { id = thread.ThreadID }); //shows details for that submission thread, with only one revision!
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        [Url("Discuss/Reply")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [RateLimit(Name = "DiscussReplyToThreadPOST", Seconds = 600)]
        public ActionResult Reply(DiscussReplyViewModel model)
        {

        }
        [HttpPost]
        [Url("Discuss/Reply/ToPost")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [RateLimit(Name = "DiscussReplyToPostPOST", Seconds = 600)]
        public ActionResult ReplyToPost(DiscussReplyViewModel model)
        {

        }
        [Url("Discuss/Edit/{postID}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name = "DiscussEditGET", Seconds = 600)]
        public ActionResult Edit(long postID)
        {
            //edit an individual review
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var query = db.DiscussPostRevisions.Where(r => r.PostID == postID).OrderByDescending(r => r.EditNuumber);
                    var revision = query.First();
                    var submitterGuid = query.Last().SubmitterUserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }
                    var firstPost = db.DiscussPosts.Where(p => p.ThreadID == revision.Post.ThreadID).Where(p=>p.PostNumberInThread==1).Single();
                    var hours = db.VenueHours.Where(h => h.ReviewRevisionID == revision.ReviewRevisionID).ToList();
                    var revisionmodel = new RevisionSubmissionViewModel()
                    {
                        DateOfLastUsage = revision.DateOfLastUsageOfPianoBySubmitter,
                        Message = revision.Message,
                        PricePerHour = revision.PricePerHourInUSD,
                        RatingOverall = revision.RatingOverall,
                        RatingPlayingCapability = (int)revision.RatingPlayingCapability,
                        RatingToneQuality = (int)revision.RatingToneQuality,
                        RatingTuning = (int)revision.RatingTuning,
                        ReviewId = (int)revision.ReviewID,
                        VenueName = revision.VenueName
                    };
                    var hourmodel = new List<VenueHourViewModel>();
                    foreach (var h in hours)
                    {
                        var item = new VenueHourViewModel();
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
                    var listingmodel = new ReadListingViewModel()
                    {
                        Listing = listing,
                        Reviews = null
                    };

                    var model = new EditViewModel()
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
        [Url("Review/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name = "DiscussEditPOST", Seconds = 600)]
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
                        var submitterGuid = db.ReviewRevisions.Where(revisionforcheck => revisionforcheck.ReviewID == model.ReviewRevision.ReviewId).First().SubmitterUserID;
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

    }
}
