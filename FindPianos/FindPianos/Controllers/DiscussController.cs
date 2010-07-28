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
using MvcReCaptcha;

namespace FindPianos.Controllers
{
    [HandleError]
    public class DiscussController : Controller
    {
        int ThreadsPerPage = 30;
        int PostsPerPage = 20;

        [OutputCache(Duration = 7200, VaryByParam = "None")]
        [Url("Discuss")]
        public ActionResult Index()
        {
            return View();
        }

        #region Get List of Boards
        [HttpPost]
        [Url("Discuss/Boards/List/{type?}")]
        [OutputCache(Duration=7200, VaryByParam="*")]
        public ActionResult ListBoards(string type)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    IEnumerable<DiscussBoard> query = null;
                    string realType = type.GetValueOrDefault("all");
                    switch (realType)
                    {
                        case "city":
                            query = db.DiscussBoards.Where(b => b.IsCityBoard).ToList();
                            break;
                        case "other":
                            query = db.DiscussBoards.Where(b => !b.IsCityBoard).ToList();
                            break;
                        case "all":
                            query = db.DiscussBoards.ToList();
                            break;
                        default:
                            return RedirectToAction("NotFound", "Error");
                    }
                    return Json(query);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        [Url("Discuss/Boards/Find")]
        [OutputCache(Duration=7200,VaryByParam="*")]
        public ActionResult FindBoardByName(string boardName)
        {
            try
            {
                using(var db = new LegatoDataContext())
                {
                    var board = db.DiscussBoards.Where(b => b.BoardName == boardName.Trim()).SingleOrDefault();
                    if(board==null)
                    {
                        return RedirectToAction("RequestBoard", new
                        {
                            name=boardName
                        });
                    }
                    return RedirectToAction("ReadBoard", new { boardID=board.BoardID, page = 1, slug = HtmlUtilities.URLFriendly(board.BoardName)});
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpGet]
        [Url("Discuss/Boards/Request/{name?}")]
        public ActionResult RequestBoard(string name)
        {
            ViewData["name"] = name;
            return View();
        }
        [HttpPost]
        [Url("Discuss/Boards/RequestBoard")]
        [CustomAuthorization(AuthorizeSuspended=false,AuthorizeEmailNotConfirmed=false)]
        [CaptchaValidator]
        public ActionResult RequestBoard(string name, bool captchaValid)
        {
            try
            {
                if (!captchaValid)
                {
                    ModelState.AddModelError("CAPTCHA", "Please enter the verification word (CAPTCHA) accurately.");
                    return View();
                }
                if (name.IsNullOrEmpty())
                {
                    ModelState.AddModelError("name", "You must specify a board name.");
                    return View();
                }
                var safeName = HtmlUtilities.Safe(HtmlUtilities.Encode(name.Trim()));
                if(safeName.Length>100)
                {
                    ModelState.AddModelError("name", "Please use a shorter name.");
                    ViewData["name"] = name;
                    return View();
                }
                using (var db = new LegatoDataContext())
                {
                    if (db.DiscussBoards.Where(b => b.BoardName == safeName).Count() != 0)
                    {
                        ModelState.AddModelError("name", "A board already exists with this name.");
                        ViewData["name"] = name;
                        return View();
                    }
                    if (db.DiscussRequestedBoards.Where(b => b.BoardName == safeName).Count() != 0)
                    {
                        ModelState.AddModelError("name", "A board with this name has already been proposed.");
                        ViewData["name"] = name;
                        return View();
                    }

                    var record = new DiscussRequestedBoard();
                    record.BoardName = safeName;
                    record.RequestDate = DateTime.Now;
                    record.RequestUserID = (Guid)Membership.GetUser().ProviderUserKey;
                    db.DiscussRequestedBoards.InsertOnSubmit(record);
                    db.SubmitChanges();
                    record = null;
                }
                ViewData["name"] = name;
                return View("RequestBoardSuccess");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion

        #region Read Boards, Threads, and Posts

        /// <summary>
        /// Reads the board.
        /// </summary>
        /// <param name="boardID">The board ID.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [Url("Discuss/{boardID}/{slug?}")]
        [OutputCache(Duration=180, VaryByParam="boardID;page")]
        public ActionResult ReadBoard(long boardID, int? page, string? slug)
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
                    ViewData["BoardName"] = board.BoardName;
                    ViewData["BoardID"] = board.BoardID;
                    return View(data);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
            }
        }
        [Url("Discuss/Thread/{threadID}/{slug?}", Order=2)]
        [OutputCache(Duration = 180, VaryByParam = "threadID;page")]
        public ActionResult ReadThread(long threadID, int? page, string? slug)
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
        [OutputCache(Duration = 180, VaryByParam = "*")]
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
                    return View("ReadThread",model);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
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
        [OutputCache(Duration = 180, VaryByParam = "postID")]
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

        #region Deletion methods
        /// <summary>
        /// Deletes the specified post ID.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorization(AuthorizeEmailNotConfirmed=false,AuthorizeSuspended=false,AuthorizedRoles="Admin,Moderator")]
        [Url("Discuss/Delete/{postID}")]
        public ActionResult Delete(long postID)
        {
            //Verify that the post exists
            using(var db = new LegatoDataContext())
            {
                var post = db.DiscussPosts.Where(p=>p.PostID==postID).SingleOrDefault();
                if(post==null)
                {
                    return RedirectToAction("NotFound","Error");
                }

                ViewData["PostNumberInThread"] = post.PostNumberInThread;

                post = null;
            }
            
            ViewData["HiddenPostVerificationValue"] = Crypto.EncryptStringAES(postID.ToString(), "IHopeNoOneEverGuessesThisString...12347890");
            ViewData["PostID"] = postID.ToString();
            return View();

        }
        /// <summary>
        /// Deletes the specified post ID.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        /// <param name="hiddenVerification">The hidden verification.</param>
        /// <param name="expungeThread">if set to <c>true</c> [expunge thread].</param>
        /// <param name="captchaValid">if set to <c>true</c> [captcha valid].</param>
        /// <returns></returns>
        [HttpPost]
        [CaptchaValidator]
        [CustomAuthorization(AuthorizeEmailNotConfirmed=false,AuthorizeSuspended=false,AuthorizedRoles="Admin,Moderator")]
        [Url("Discuss/Delete")]
        public ActionResult Delete(long postID, string hiddenVerification, string hiddenPostNumber, bool expungeThread, bool captchaValid)
        {
            try
            {
                if(!captchaValid)
                {
                    ModelState.AddModelError("CAPTCHA", "Please re-enter the verification word.");
                    ViewData["HiddenPostVerificationValue"] = hiddenVerification;
                    ViewData["PostID"] = postID.ToString();
                    ViewData["PostNumberInThread"] = hiddenPostNumber;
                    return View();
                }
                var decrypted = Crypto.DecryptStringAES(hiddenVerification, "IHopeNoOneEverGuessesThisString...12347890");
                if (decrypted != postID.ToString())
                {
                    return RedirectToAction("Forbidden", "Error");
                }

                if (expungeThread)
                {
                    long BoardID;
                    using(var db = new LegatoDataContext())
                    {
                        var thread = db.DiscussPosts.Where(p => p.PostID == postID).SingleOrDefault().DiscussThread;
                        BoardID=thread.BoardID;
                        db.DiscussThreads.DeleteOnSubmit(thread);
                        db.SubmitChanges();
                    }

                    return RedirectToAction("ReadBoard", "Discuss", new
                    {
                        boardID=BoardID
                    });
                }
                else
                {
                    long ThreadID;
                    using (var db = new LegatoDataContext())
                    {
                        var post = db.DiscussPosts.Where(p => p.PostID == postID).SingleOrDefault();
                        ThreadID = post.ThreadID;
                        db.DiscussPosts.DeleteOnSubmit(post);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("ReadThread", "Discuss", new
{
    threadID = ThreadID
});
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
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
        [HttpPost][OutputCache(Duration = 180, VaryByParam = "*")]
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
            using(var db = new LegatoDataContext())
            {
                var board = db.DiscussBoards.Where(b => b.BoardID == boardID).SingleOrDefault();
                if(board==null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                var model = new DiscussCreateViewModel()
                {
                    BoardID = boardID,
                    CanSetLocation = board.IsCityBoard
                };
                return View(model);
            }

        }
        [Url("Discuss/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [RateLimit(Name="DiscussSubmitPOST", Seconds=600)]
        public ActionResult Submit(DiscussCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var time = DateTime.Now;
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    
                    //Board:
                    var board = db.DiscussBoards.Where(b => b.BoardID == model.BoardID).SingleOrDefault();
                    if(board==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //THREAD:
                    var thread = new DiscussThread();

                    thread.CreationDate = time;
                    thread.LatestActivity = time;
                    thread.Title = model.Title;
                    thread.BoardID = model.BoardID;
                    if(!(board.IsCityBoard) || model.Address.IsNullOrEmpty())
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
                    
                    return RedirectToAction("ReadThread", new { threadID = thread.ThreadID }); //shows details for that submission thread, with only one revision!
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
        [RateLimit(Name = "DiscussReplyPOST", Seconds = 600)]
        public ActionResult Reply(DiscussReplyViewModel model)
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

                    //Thread:
                    var thread = db.DiscussThreads.Where(b => b.ThreadID == model.ThreadID).SingleOrDefault();
                    if (thread == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //POST:
                    var post = new DiscussPost();
                    post.DiscussThread = thread;
                    post.DateOfSubmission = time;
                    post.PostNumberInThread = (from rev in db.DiscussPosts
                                                where rev.ThreadID == thread.ThreadID
                                                select rev.PostNumberInThread).Max() + 1;
                    db.DiscussPosts.InsertOnSubmit(post);
                    db.SubmitChanges();

                    //POST REVISION:
                    var r = new DiscussPostRevision();
                    r.Markdown = model.Post.Markdown;
                    r.HTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.Post.Markdown));
                    r.DateOfEdit = time;
                    r.DiscussPost = post;
                    r.EditNumber = 1;
                    r.UserID = userGuid;
                    db.DiscussPostRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    return RedirectToAction("IndividualPostRedirect", new
                    {
                        postID = post.PostID
                    });
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
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

                    var board = post.DiscussThread.DiscussBoard;

                    var revisionmodel = new DiscussEditViewModel()
                    {
                        CanChangeLocation=(post.PostNumberInThread==1 && board.IsCityBoard),
                        PostID=postID,
                        Post=new DiscussPostSubmissionViewModel()
                        {
                            Markdown = revision.Markdown,
                            InReplyToPostID = revision.InReplyToPostID
                        }
                    };
                    if(revisionmodel.CanChangeLocation)
                    {
                        var thread = post.DiscussThread;
                        revisionmodel.Address = thread.Address;
                        revisionmodel.Lat = thread.Latitude;
                        revisionmodel.Long = thread.Longitude;
                    }
                    else
                    {
                        revisionmodel.Address = null;
                        revisionmodel.Lat = null;
                        revisionmodel.Long = null;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var post = db.DiscussPosts.Where(p => p.PostID == model.PostID).SingleOrDefault();
                    if(post==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    var submitterGuid = db.DiscussPostRevisions.Where(revisionforcheck => revisionforcheck.PostID == model.PostID).OrderBy(revisionforcheck=>revisionforcheck.EditNumber).First().UserID;
                    if (userGuid != submitterGuid && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }

                    var board = post.DiscussThread.DiscussBoard;
                    var CanChangeLocation = (post.PostNumberInThread == 1 && board.IsCityBoard);
                    var time = DateTime.Now;

                    if(model.Post.InReplyToPostID.HasValue)
                    {
                        var inreplytopost = db.DiscussPosts.Where(p => p.PostID == model.Post.InReplyToPostID.Value).SingleOrDefault();
                        if(inreplytopost == null)
                        {
                            ModelState.AddModelError("Post.InReplyToPostID", "That post doesn't exist.");
                            return View();
                        }
                        else if(inreplytopost.ThreadID!=post.ThreadID)
                        {
                            ModelState.AddModelError("Post.InReplyToPostID", "That post is in another thread.");
                            return View();
                        }
                    }

                    //REVISION:
                    var r = new DiscussPostRevision();
                    r.DateOfEdit = time;
                    r.Markdown = model.Post.Markdown;
                    r.HTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.Post.Markdown));
                    r.InReplyToPostID = model.Post.InReplyToPostID;
                    r.PostID = model.PostID;
                    r.UserID = userGuid;
                    r.EditNumber = (from rev in db.DiscussPostRevisions
                                                where rev.PostID == model.PostID
                                                select rev.EditNumber).Max() + 1;
                    db.DiscussPostRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    if (CanChangeLocation)
                    {
                        var thread = post.DiscussThread;
                        if (model.Address.HasValue())
                        {
                            thread.Latitude = model.Lat;
                            thread.Longitude = model.Long;
                            thread.Address = model.Address;
                        }
                        else
                        {
                            thread.Latitude = null;
                            thread.Longitude = null;
                            thread.Address = null;
                        }
                        db.SubmitChanges();
                    }

                    return RedirectToAction("PostTimeline", new { postID = model.PostID});
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion
        #region AJAX: Flag Listings and Reviews
        [RateLimit(Name="DiscussFlagPostPOST", Seconds=120)]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        [Url("Discuss/Flag")]
        public ActionResult AjaxFlagPost(long idOfPost, int flagTypeId)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    //Check whether the given listing exists before creating a possibly-useless record
                    if (db.DiscussPosts.Where(l => l.PostID == idOfPost).SingleOrDefault() == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    //If we've gotten this far, everything's probably A-OK.
                    var flag = new DiscussPostFlag();
                    flag.FlagDate = DateTime.Now;
                    flag.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    flag.TypeID = flagTypeId;
                    flag.PostID = idOfPost;
                    db.DiscussPostFlags.InsertOnSubmit(flag);
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
