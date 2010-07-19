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
        int ThreadsPerPage = 30;
        int PostsPerPage = 20;
        [OutputCache(Duration = 7200, VaryByParam = "None")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        #region Read Boards, Threads, and Posts

        /// <summary>
        /// Reads the board.
        /// </summary>
        /// <param name="boardID">The board ID.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [Url("Discuss/{boardID}/{slug?}")]
        [OutputCache(Duration=7200, VaryByParam="boardID;page")]
        public ActionResult ReadBoard(long boardID, int? page)
        {
            var href = "Discuss/" + boardID;
            using(var db = new LegatoDataContext())
            {
                try
                {
                    var board = db.DiscussBoards.Where(b => b.BoardID == boardID).SingleOrDefault();
                    if(board==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    var data = db.DiscussThreads.Where(t => t.BoardID == boardID).OrderByDescending(t => t.LatestActivity).Skip((page.GetValueOrDefault(1) - 1) * ThreadsPerPage).Take(ThreadsPerPage).ToPagedList(page??1,ThreadsPerPage);
                    var totalPosts = db.DiscussThreads.Where(t => t.BoardID == boardID).Count();
                    ViewData["PageNumbers"] = new PageNumber(href + "&page=-1", (totalPosts/ThreadsPerPage) + 1,page.GetValueOrDefault(1) - 1, "pager");
                    ViewData["TotalPosts"] = string.Format("{0:n0}", totalPosts);
                    return View(data);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
            }
        }
        [Url("Discuss/Thread/{threadID}/{slug?}", Order=2)]
        [OutputCache(Duration = 7200, VaryByParam = "threadID;page")]
        public ActionResult ReadThread(long threadID, int? page)
        {
            var href = "Discuss/Thread/" + threadID;
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var thread = data.DiscussThreads.Where(t => t.ThreadID == threadID).SingleOrDefault();
                    if(thread==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    thread.FillProperties();
                    var posts = data.DiscussPosts.Where(p => p.ThreadID == threadID).OrderBy(p => p.DateOfSubmission).Skip((page.GetValueOrDefault(1) - 1) * PostsPerPage).Take(PostsPerPage).ToPagedList(page ?? 1, PostsPerPage);
                    foreach (var p in posts)
                    {
                        p.FillProperties();
                    }

                    var totalPosts = data.DiscussPosts.Where(p=>p.ThreadID==threadID).Count();
                    var pageNumbers = new PageNumber(href + "&page=-1", (totalPosts / PostsPerPage) + 1, page.GetValueOrDefault(1) - 1, "pager");
                    var totalPostsString = string.Format("{0:n0}", totalPosts);

                    var model = new DiscussReadThreadViewModel
                    {
                        Thread = thread,
                        Posts = posts,
                        TotalPosts = totalPostsString,
                        PageNumbers = pageNumbers
                    };
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
            }
        }
        
        [Url("Discuss/Thread/{threadID}/Post/{postID}", Order=1)]
        [OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult IndividualPost(long threadID, long postID)
        {
            var href = "Discuss/Thread/" + threadID;
            using (var data = new LegatoDataContext())
            {
                try
                {
                    var post = data.DiscussPosts.Where(p => p.PostID == postID).SingleOrDefault();
                    if (post == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    var thread = data.DiscussThreads.Where(t => t.ThreadID == threadID).SingleOrDefault();
                    if (thread == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    thread.FillProperties();

                    //Get page number
                    var page = (int)Math.Ceiling((double)post.PostNumberInThread / PostsPerPage);

                    //Get boundaries of post numbers for the page
                    var minimum = PostsPerPage * (page - 1) + 1;
                    var maximum = PostsPerPage * page;


                    var posts = data.DiscussPosts.Where(p => p.ThreadID == threadID).Where(p=>p.PostNumberInThread>=minimum&&p.PostNumberInThread<=maximum).OrderBy(p => p.DateOfSubmission).Skip((int)Math.Ceiling((double)(page - 1) * PostsPerPage)).Take(PostsPerPage).ToPagedList(page, PostsPerPage);
                    foreach (var p in posts)
                    {
                        p.FillProperties();
                    }

                    var totalPosts = data.DiscussPosts.Where(p => p.ThreadID == threadID).Count();
                    var pageNumbers = new PageNumber(href + "&page=-1", (totalPosts / PostsPerPage) + 1, page - 1, "pager");
                    var totalPostsString = string.Format("{0:n0}", totalPosts);

                    var model = new DiscussReadThreadViewModel
                    {
                        Thread = thread,
                        Posts = posts,
                        TotalPosts = totalPostsString,
                        PageNumbers = pageNumbers
                    };
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
        }
        [Url("Discuss/Post/{postID}")]
        [OutputCache(Duration = 7200, VaryByParam = "*")]
        public ActionResult IndividualPostRedirect(long postID)
        {
            using(var db = new LegatoDataContext())
            {
                try
                {
                    var post = db.DiscussPosts.Where(p => p.PostID == postID).SingleOrDefault();
                    if (post == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    return Redirect(string.Format("Discuss/Thread/{0}/Post/{1}#{1}",post.ThreadID,postID));
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
            }
            
        }
        #endregion

        #region Individual Post timeline- and revision-listing method
        [Url("Discuss/Timeline/Post/{postID}")]
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
                var results = DiscussThread.ProcessAjaxMapSearch(new BoundingBox()
            {
                extent1 = new LatLong() { latitude = lat1, longitude = long1 },
                extent2 = new LatLong() { latitude = lat2, longitude = long2 }
            });
                return Json(results);
            }

        }
        #endregion

        #region Submission and Editing methods
        [Url("Discuss/Create/{boardID}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
        [RateLimit(Name="DiscussSubmitGET", Seconds=600)]
        public ActionResult Submit(long boardID)
        {
            var model = new DiscussCreateViewModel()
            {
                BoardID=boardID
            };
            return View(model);
        }
        [Url("Discuss/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name="DiscussSubmitPOST", Seconds=600)]
        public ActionResult Submit(DiscussCreateViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    
                    //THREAD:
                    var thread = new DiscussThread();

                    thread.CreationDate = time;
                    thread.LatestActivity = time;
                    thread.Title = model.Title;
                    if(model.Address.IsNullOrEmpty())
                    {
                        thread.Address = null;
                        thread.Latitude = null;
                        thread.Longitude = null;
                    }
                    else
                    {
                        thread.Address = model.Address;
                        thread.Latitude = model.Lat;
                        thread.Longitude = model.Long;
                    }

                    db.DiscussThreads.InsertOnSubmit(thread);
                    db.SubmitChanges();

                    //POST:
                    var post = new DiscussPost();
                    post.DiscussThread = thread;
                    post.DateOfSubmission = time;
                    db.DiscussPosts.InsertOnSubmit(post);
                    db.SubmitChanges();

                    //POST REVISION:
                    var r = new DiscussPostRevision();
                    r.Markdown = model.Post.Markdown;
                    r.HTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.Post.Markdown));
                    r.DateOfEdit = time;
                    r.DiscussPost = post;
                    //r.RevisionNumberOfReview = (from rev in db.PianoReviewRevisions
                    //                            where rev.PianoReviewID == review.PianoReviewID
                    //                            select rev.RevisionNumberOfReview).Max() + 1;
                    r.EditNumber = 1;
                    r.UserID=userGuid;
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
            //edit an individual post
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var post = db.DiscussPosts.Where(p=>p.PostID==postID).SingleOrDefault();
                    if(post==null)
                    {
                        return RedirectToAction("NotFound","Error");
                    }
                    
                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var query = db.DiscussPostRevisions.Where(r => r.PostID == postID).OrderByDescending(r => r.EditNumber);
                    var revision = query.First();
                    var submitterGuid = query.Last().UserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }
                    var firstPost = db.DiscussPosts.Where(p => p.ThreadID == revision.Post.ThreadID).Where(p=>p.PostNumberInThread==1).Single();
                    //TODO: REPLY TO POST ID
                    var hours = db.VenueHours.Where(h => h.ReviewRevisionID == revision.ReviewRevisionID).ToList();
                    var revisionmodel = new DiscussEditViewModel()
                    {
                        CanChangeLocation=post.PostNumberInThread==1,
                        PostID=postID,
                        Post=new DiscussPostSubmissionViewModel()
                        {
                            Markdown = revision.Markdown
                        }
                    };
                    if(revisionmodel.CanChangeLocation)
                    {
                        var thread = post.DiscussThread;
                        revisionmodel.Address = thread.Address;
                        revisionmodel.Lat = thread.Latitude;
                        revisionmodel.Long = thread.Longitude;
                    }
                    
                    return View(revisionmodel);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [Url("Discuss/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name = "DiscussEditPOST", Seconds = 600)]
        public ActionResult Edit(DiscussEditViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    try
                    {
                        //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                        var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                        var submitterGuid = db.DiscussPostRevisions.Where(revisionforcheck => revisionforcheck.PostID == model.PostID).OrderBy(revisionforcheck=>revisionforcheck.EditNumber).First().UserID;
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
