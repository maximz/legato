using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legato.Models;
using System.Globalization;
using RiaLibrary.Web;
using System.Web.Security;
using Legato.Helpers;
using System.Net;
using Legato.ViewModels;
using System.Web.Routing;
using MvcMiniProfiler;

namespace Legato.Controllers
{
    [HandleError]
    public partial class InstrumentsController : CustomControllerBase
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (ViewData["CurrentMenuItem"] == null || ViewData["CurrentMenuItem"].ToString().IsNullOrEmpty())
            {
                ViewData["CurrentMenuItem"] = "Instruments";
            }
        }

        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "None")]
        [Url("Instruments")]
        public virtual ActionResult Index()
        {
            return View(); // In the view, we give options of navigating to public/rent/sale pages or of viewing them all together on a map
        }

        #region Map

        /// <summary>
        /// Displays a map of instruments.
        /// </summary>
        /// <param name="classIns">Optional. The instrument class that is requested; e.g. "public", "rent", "sale"</param>
        /// <returns></returns>
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200, VaryByParam = "classIns")]
        [Url("Instruments/Map/{classIns?}")]
        public virtual ActionResult Map(string classIns)
        {
            // TODO: currently, users can view instruments in all classes or in one specific class. Ideally, we can have them select classes as checkboxes, so we can have them view 2 classes out of 3, for example. Should be improved, but later.

            // Rough hack: put everything as JS array for now
            var db = Current.DB;
            var points = (from ins in db.Instruments
                         select new {
                             id = ins.InstrumentID,
                             lat = ins.Lat,
                             lng = ins.Long,
                             label = ins.Brand.Trim() + " "+ ins.Model.Trim() + " (" + ins.InstrumentType.Name + ") at" + ins.StreetAddress.Trim(),
                             slug = HtmlUtilities.URLFriendly(ins.Brand.Trim() + " "+ ins.Model.Trim() + " (" + ins.InstrumentType.Name + ") at" + ins.StreetAddress.Trim()),
                             typename = ins.InstrumentType.Name,
                             typeid = ins.InstrumentType.TypeID,
                             type = ins.InstrumentType
                             //icon = ins.InstrumentReviews.Average(r=>r.InstrumentReviewRevisions.OrderByDescending(rr=>rr.RevisionDate).Take(1).ToList()[0].RatingGeneral) + "-" + ins.ListingClass
                         }).ToList();
            if (classIns.HasValue()) // for example, if classIns="public", only instruments that are allowed in public mode are shown.
            {
                foreach (var p in points)
                {
                    bool forceBreak = false; // so we can break out of this foreach - see default case in switch block
                    switch(classIns.Trim())
                    {
                        case "public":
                            if(!p.type.AllowedInPublic.GetValueOrDefault(false))
                            {
                                points.Remove(p);
                            }
                            break;
                        case "rent":
                            if(!p.type.AllowedInRent.GetValueOrDefault(false))
                            {
                                points.Remove(p);
                            }
                            break;
                        case "sale":
                            if (!p.type.AllowedInSale.GetValueOrDefault(false))
                            {
                                points.Remove(p);
                            }
                            break;
                        default:
                            // some weird classIns value was given, so let's not waste time on the rest of the points
                            forceBreak = true;
                            break;
                    }
                    if(forceBreak)
                    {
                        break; // see default case of switch block ^^
                    }
                }
            }
            return Json(points,JsonRequestBehavior.AllowGet);
            if(ControllerContext.IsChildAction)
            {
                //return PartialView(result);
            }
            //return View(result);
        }

        #endregion

        #region AJAX Methods

        /// <summary>
        /// Returns a list of instrument types, such as "piano", "clarinet", etc. On the map page, the user can either view all instrument types, or only specific ones. To choose specific ones, we load this list via AJAX and show it as checkboxes. Then, the map points are filtered via JS. This is also used on the Submit page.
        /// </summary>
        /// <param name="classIns">Optional. The instrument class that is requested; e.g. "public", "rent", "sale"</param>
        /// <returns></returns>
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200, VaryByParam = "classIns")]
        [Url("Instruments/AJAX/InsList/{classIns?}")]
        public virtual ActionResult PossibleInstrumentTypes(string classIns)
        {
            var db = Current.DB;
            var types = db.InstrumentTypes.Select(p => p).ToList();
            if (classIns.HasValue()) // for example, if classIns="public", only instrument types that are allowed in public mode are shown.
            {
                foreach (var p in types)
                {
                    bool forceBreak = false; // so we can break out of this foreach - see default case in switch block
                    switch (classIns.Trim())
                    {
                        case "public":
                            if (!p.AllowedInPublic.GetValueOrDefault(false))
                            {
                                types.Remove(p);
                            }
                            break;
                        case "rent":
                            if (!p.AllowedInRent.GetValueOrDefault(false))
                            {
                                types.Remove(p);
                            }
                            break;
                        case "sale":
                            if (!p.AllowedInSale.GetValueOrDefault(false))
                            {
                                types.Remove(p);
                            }
                            break;
                        default:
                            // some weird classIns value was given, so let's not waste time on the rest of the types
                            forceBreak = true;
                            break;
                    }
                    if (forceBreak)
                    {
                        break; // see default case of switch block ^^
                    }
                }
            }

            return Json(types, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Read Listings and Reviews
        /// <summary>
        /// Displays an instrument's individual page, with the listing and reviews.
        /// </summary>
        /// <param name="instrumentID">The instrument ID.</param>
        /// <returns></returns>
        [Url("Instruments/Listing/{instrumentID}/{slug?}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "instrumentID")]
        public virtual ActionResult Individual(int instrumentID, string slug)
        {
            var db = Current.DB;
                try
                {
                    var listing = db.Instruments.Where(l => l.InstrumentID == instrumentID).Single();
                    listing.FillProperties();
                    var reviews = db.InstrumentReviews.Where(r => r.InstrumentID == instrumentID).ToList();
                    foreach (var r in reviews)
                    {
                        r.FillProperties();
                    }

                    db.ExecuteCommand("UPDATE Instruments SET ListingViews=ListingViews+1 WHERE InstrumentID={0}", instrumentID);

                    var model = new ReadListingViewModel
                    {
                        Instrument = listing,
                        Reviews = reviews
                    };
                    return View("Listing",model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
        }

        /// <summary>
        /// Shows the instrument listing page with only the review in question.
        /// </summary>
        /// <param name="reviewID">The review ID.</param>
        /// <returns></returns>
        [Url("Instrument/Review/{reviewID}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "reviewID")]
        public virtual ActionResult IndividualReview(int reviewID)
        {
            var db = Current.DB;
                try
                {
                    var review = db.InstrumentReviews.Where(r => r.ReviewID == reviewID).Single();
                    review.FillProperties();

                    var listing = review.Instrument;
                    listing.FillProperties();

                    var model = new ReadListingViewModel()
                    {
                        Instrument = listing,
                        Reviews = new List<InstrumentReview>(),
                        IsFilteredToIndividualReview = true
                    };
                    model.Reviews.Add(review);
                    return View("Listing",model);
                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
        }
        #endregion

        #region Individual Review timeline- and revision-listing method
        /// <summary>
        /// Displays a timeline of revisions of a specific review.
        /// </summary>
        /// <param name="reviewID">The review ID.</param>
        /// <returns></returns>
        [Url("Instrument/Review/Timeline/{reviewID}")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByParam = "reviewID")]
        public virtual ActionResult Timeline(int reviewID)
        {
            try
            {
                var db = Current.DB;
                    var review = db.InstrumentReviews.Where(r => r.ReviewID == reviewID).Single();
                    review.Revisions = db.InstrumentReviewRevisions.Where(rev => rev.ReviewID == review.ReviewID).OrderByDescending(rev => rev.RevisionDate).ToList();
                    return View(review);
            }
            catch
            {
                return RedirectToAction("NotFound","Error");
            }
        }
        #endregion

        #region Listing and Review Creation Methods
        [Url("Instrument/Submit")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
        public virtual ActionResult Submit()
        {
            var model = new SubmitViewModel();

            for(int i = 0;i<7;i++)
            {
                var day = Enum.GetName(typeof(DayOfWeek),i);
                var hourmodel = new VenueHourViewModel()
                {
                    DayOfWeekId = i,
                    DayOfWeekName = day
                };
                model.Hours.Add(hourmodel);
            }

            return View(model);
        }
        [Url("Instrument/Submit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        //[RateLimit(Name="InstrumentSubmitPOST", Seconds=600)]
        public virtual ActionResult Submit(SubmitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                new RateLimitAttribute().CancelRateLimit("InstrumentSubmitPOST");
                return View(model);
            }

            try
            {
                var db = Current.DB;
                    var time = DateTime.Now;

                    //LISTING:
                    var listing = new Instrument();

                    listing.StreetAddress = model.Listing.StreetAddress;
                    listing.Lat = (decimal)model.Listing.Lat;
                    listing.Long = (decimal)model.Listing.Long;
                    listing.Model = model.Listing.Equipment.Model.Trim();
                    listing.Brand = model.Listing.Equipment.Brand.Trim();
                    listing.Price = (decimal?)model.Listing.Price;
                    listing.TimeSpanOfPrice = model.Listing.TimeSpanOfPrice;
                    listing.VenueName = model.Listing.VenueName;
                    
                    /*Matching instrument and style:
                     * 1. take instrument name, find match in Instruments table
                     * 2. apply SelectedIndex of type to dropdownlist, extract name from the list
                     * 3. Match name to a record in InstrumentTypes with InstrumentID from step 1 and Name from step 2
                     * 4. Apply ID of record in #3 to Listing
                     * 5. Same for Styles
                     * that's how we do it! */
                    var type = db.InstrumentTypes.Where(i => i.Name == model.Listing.Equipment.Types.ElementAtOrDefault(model.Listing.Equipment.SelectedType).Text).SingleOrDefault();
                    if(type==null)
                    {
                        ModelState.AddModelError("SelectedType", "No such instrument type exists.");
                        new RateLimitAttribute().CancelRateLimit("InstrumentSubmitPOST");
                        return View(model);
                    }
                    listing.TypeID = type.TypeID;

                    var style = model.Listing.Equipment.Classes.ElementAtOrDefault(model.Listing.Equipment.SelectedClass).Text.ToLowerInvariant();
                    if(style==null || (style != "public" && style != "rent" && style != "sale"))
                    {
                        ModelState.AddModelError("SelectedClass", "No such class exists.");
                        new RateLimitAttribute().CancelRateLimit("InstrumentSubmitPOST");
                        return View(model);
                    }
                    listing.ListingClass = style;

                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                    listing.UserID = userGuid;
                    listing.SubmissionDate = time;

                    db.Instruments.InsertOnSubmit(listing);
                    db.SubmitChanges();

                    // Global Post:
                    var gpost = new GlobalPostID();
                    gpost.UserID = userGuid;
                    gpost.PostCategory = MagicCategoryStrings.Instrument;
                    gpost.SubmissionDate = time;
                    gpost.SpecificPostID = listing.InstrumentID;
                    db.GlobalPostIDs.InsertOnSubmit(gpost);
                    db.SubmitChanges();
                    listing = db.Instruments.Where(i => i.InstrumentID == listing.InstrumentID).SingleOrDefault(); // Nasty SQL hack
                    listing.GlobalPostID = gpost.GlobalPostID1;
                    db.SubmitChanges();

                    //REVIEW:
                    var review = new InstrumentReview();
                    review.Instrument = listing;
                    review.UserID = userGuid;
                    review.SubmissionDate = time;
                    db.InstrumentReviews.InsertOnSubmit(review);
                    db.SubmitChanges();

                    // Global Post:
                    var gpostrev = new GlobalPostID();
                    gpostrev.UserID = userGuid;
                    gpostrev.PostCategory = MagicCategoryStrings.InstrumentReview;
                    gpostrev.SubmissionDate = time;
                    gpostrev.SpecificPostID = review.ReviewID;
                    db.GlobalPostIDs.InsertOnSubmit(gpostrev);
                    db.SubmitChanges();
                    review.GlobalPostID = gpostrev.GlobalPostID1;
                    db.SubmitChanges();

                    //REVISION:
                    var r = new InstrumentReviewRevision();
                    r.LastUseDate = model.ReviewRevision.DateOfLastUsage.Value;
                    r.MessageMarkdown = Microsoft.Web.Mvc.AjaxExtensions.JavaScriptStringEncode(HtmlUtilities.Sanitize(model.ReviewRevision.ReviewMarkdown));
                    r.MessageHTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.ReviewRevision.ReviewMarkdown));
                    r.RatingGeneral = model.ReviewRevision.RatingOverall;
                    r.RevisionDate = time;
                    r.InstrumentReview = review;
                    r.UserID = userGuid;

                    db.InstrumentReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    // Global Post:
                    var gpostrevis = new GlobalPostID();
                    gpostrevis.UserID = userGuid;
                    gpostrevis.PostCategory = MagicCategoryStrings.InstrumentReviewRevision;
                    gpostrevis.SubmissionDate = time;
                    gpostrevis.SpecificPostID = r.RevisionID;
                    db.GlobalPostIDs.InsertOnSubmit(gpostrevis);
                    db.SubmitChanges();
                    r.GlobalPostID = gpostrevis.GlobalPostID1;
                    db.SubmitChanges();

                    //VENUE HOURS:
                    foreach (var hour in model.Hours)
                    {
                        var submit = new InstrumentHour();
                        submit.Day = hour.DayOfWeekId;
                        if(!hour.Closed)
                        {
                            submit.OpenTime = new TimeSpan(hour.StartTime.Value.Hour, hour.StartTime.Value.Minute, hour.StartTime.Value.Second);
                            submit.CloseTime = new TimeSpan(hour.EndTime.Value.Hour, hour.EndTime.Value.Minute, hour.EndTime.Value.Second);
                        }
                        else
                        {
                            submit.OpenTime = null;
                            submit.CloseTime = null;
                        }
                        submit.Instrument = listing;
                        db.InstrumentHours.InsertOnSubmit(submit);
                    }
                    db.SubmitChanges();

                    // Search
                    // Add to Lucene index:
                    var s = new SearchController();
                    s.AddToIndex(listing);
                    s.AddToIndex(review);
                    s = null;

                    return RedirectToAction("Individual", new { instrumentID = listing.InstrumentID }); //shows details for that submission thread, with only one revision!
            }
            catch(Exception ex)
            {
                new RateLimitAttribute().CancelRateLimit("InstrumentSubmitPOST");
                return RedirectToAction("InternalServerError", "Error");
            }
        }


        [Url("Instrument/Review/Create/{instrumentID}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public virtual ActionResult Review(int instrumentID)
        {
            var db = Current.DB;
            var profiler = Current.MiniProfiler;

            using (profiler.Step("Checks"))
            {
                using (profiler.Step("Instrument to review exists"))
                {
                    // Check to see whether instrumentID exists
                    if (instrumentID == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    if (db.Instruments.Where(i => i.InstrumentID == instrumentID).SingleOrDefault() == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                }

                using (profiler.Step("User hasn't previously reviewed this"))
                {
                    // Check to see whether this user has already reviewed this instrument (checks using UserGuid and InstrumentID)
                    var existingReview = db.InstrumentReviews.Where(r => r.UserID == (Guid)Membership.GetUser().ProviderUserKey && r.InstrumentID == instrumentID).SingleOrDefault();
                    if (existingReview != null)
                    {
                        return View("AlreadyReviewed", existingReview);
                    }
                }
            }

            // All checks succeeded. Rendering review creation view.
            return View(new ReviewCreateViewModel() { InstrumentID = instrumentID });
        }
        
        [HttpPost][VerifyReferrer]
        [Url("Instrument/Review/Create")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [RateLimit(Name = "InstrumentReviewSubmitPOST", Seconds = 600)]
        public virtual ActionResult Review(ReviewCreateViewModel model)
        {
            var profiler = Current.MiniProfiler;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("InternalServerError", "Error");
            }
            try
            {
                var db = Current.DB;
                    var time = DateTime.Now;
                    var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc

                    var listing = db.Instruments.Where(l => l.InstrumentID == model.InstrumentID).SingleOrDefault();
                    if(listing==null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }

                    InstrumentReview review;
                    using (profiler.Step("Create Review"))
                    {
                        // REVIEW:
                        review = new InstrumentReview();
                        review.Instrument = listing;
                        review.UserID = userGuid;
                        review.SubmissionDate = time;

                        db.InstrumentReviews.InsertOnSubmit(review); //An exception will be thrown here if there are invalid properties
                        db.SubmitChanges();

                        // Global Post:
                        var gpostrev = new GlobalPostID();
                        gpostrev.UserID = userGuid;
                        gpostrev.PostCategory = MagicCategoryStrings.InstrumentReview;
                        gpostrev.SubmissionDate = time;
                        gpostrev.SpecificPostID = review.ReviewID;
                        db.GlobalPostIDs.InsertOnSubmit(gpostrev);
                        db.SubmitChanges();
                        review.GlobalPostID = gpostrev.GlobalPostID1;
                        db.SubmitChanges();
                    }

                    InstrumentReviewRevision r;
                    using (profiler.Step("Create Revision"))
                    {
                        // REVISION:
                        r = new InstrumentReviewRevision();
                        r.LastUseDate = model.ReviewRevision.DateOfLastUsage.Value;
                        r.MessageMarkdown = Microsoft.Web.Mvc.AjaxExtensions.JavaScriptStringEncode(HtmlUtilities.Sanitize(model.ReviewRevision.ReviewMarkdown));
                        r.MessageHTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.ReviewRevision.ReviewMarkdown));
                        r.RatingGeneral = model.ReviewRevision.RatingOverall;
                        r.RevisionDate = time;
                        r.InstrumentReview = review;
                        r.UserID = userGuid;

                        db.InstrumentReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                        db.SubmitChanges();

                        // Global Post:
                        var gpostrevis = new GlobalPostID();
                        gpostrevis.UserID = userGuid;
                        gpostrevis.PostCategory = MagicCategoryStrings.InstrumentReviewRevision;
                        gpostrevis.SubmissionDate = time;
                        gpostrevis.SpecificPostID = r.RevisionID;
                        db.GlobalPostIDs.InsertOnSubmit(gpostrevis);
                        db.SubmitChanges();
                        r.GlobalPostID = gpostrevis.GlobalPostID1;
                        db.SubmitChanges();
                    }

                    // Search
                    // Add to Lucene index:
                    var s = new SearchController();
                    s.AddToIndex(review);
                    s = null;

                    // Done
                    return RedirectToAction("IndividualReview", new
                    {
                        reviewID = review.ReviewID
                    });
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        
        #endregion

        #region Editing Methods

        /// <summary>
        /// Edit an individual review.
        /// </summary>
        /// <param name="reviewID">The review ID.</param>
        /// <returns></returns>
        [Url("Instrument/Review/Edit/{reviewID}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public virtual ActionResult EditReview(int reviewID)
        {
            try
            {
                var db = Current.DB;
                var review = db.InstrumentReviews.Where(r=>r.ReviewID == reviewID).SingleOrDefault();
                if(review==null)
                {
                    return RedirectToAction(MVC.Error.NotFound());
                }
                var permissions = review.Permissions();
                    
                    if (!permissions.CanEdit) // if user isn't submitter and doesn't have edit privileges, forbidden!
                    {
                        return RedirectToAction("Forbidden", "Error");
                    }

                    var revision = review.InstrumentReviewRevisions.OrderByDescending(rr => rr.RevisionDate).First();    
                    var listing = review.Instrument;
                    var hours = listing.InstrumentHours;

                    var revisionmodel = new RevisionSubmissionViewModel()
                    {
                        DateOfLastUsage=revision.LastUseDate,
                        ReviewMarkdown=revision.MessageMarkdown,
                        RatingOverall=revision.RatingGeneral,
                        ReviewID=(int)revision.ReviewID
                    };
                    
                    var model = new EditReviewViewModel()
                    {
                        ReviewRevision=revisionmodel
                    };
                    return View(model);
            }
            catch
            {
                return RedirectToAction("NotFound", "Error");
            }
        }
        [Url("Instrument/Review/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        [RateLimit(Name = "InstrumentReviewEditPOST", Seconds = 600)]
        public virtual ActionResult EditReview(EditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var db=Current.DB;
                var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                var review = db.InstrumentReviews.Where(rrr => rrr.ReviewID == model.ReviewRevision.ReviewID).SingleOrDefault();
                if(review==null)
                {
                    return RedirectToAction(MVC.Error.NotFound());
                }
                var permissions = review.Permissions();
                    try
                    {
                        //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                        if(!permissions.CanEdit)
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
                    var r = new InstrumentReviewRevision();
                    r.LastUseDate = model.ReviewRevision.DateOfLastUsage.Value;
                    r.MessageMarkdown = Microsoft.Web.Mvc.AjaxExtensions.JavaScriptStringEncode(HtmlUtilities.Sanitize(model.ReviewRevision.ReviewMarkdown));
                    r.MessageHTML = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(model.ReviewRevision.ReviewMarkdown));
                    r.RatingGeneral = model.ReviewRevision.RatingOverall;
                    r.RevisionDate = time;
                    r.ReviewID = model.ReviewRevision.ReviewID;
                    r.UserID = userGuid;

                    db.InstrumentReviewRevisions.InsertOnSubmit(r); //An exception will be thrown here if there are invalid properties
                    db.SubmitChanges();

                    // Global Post:
                    var gpostrevis = new GlobalPostID();
                    gpostrevis.UserID = userGuid;
                    gpostrevis.PostCategory = MagicCategoryStrings.InstrumentReviewRevision;
                    gpostrevis.SubmissionDate = time;
                    gpostrevis.SpecificPostID = r.RevisionID;
                    db.GlobalPostIDs.InsertOnSubmit(gpostrevis);
                    db.SubmitChanges();
                    r.GlobalPostID = gpostrevis.GlobalPostID1;
                    db.SubmitChanges();

                    // Search
                    // Add to Lucene index:
                    var s = new SearchController();
                    s.ChangeIndex(review);
                    s = null;

                    return RedirectToAction("IndividualReview", new { reviewID = model.ReviewRevision.ReviewID});
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        /// <summary>
        /// Edit an individual listing.
        /// </summary>
        /// <param name="reviewID">The listing ID.</param>
        /// <returns></returns>
        [Url("Instrument/Listing/Edit/{instrumentID}")]
        [HttpGet]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public virtual ActionResult EditListing(int instrumentID)
        {
            try
            {
                var db = Current.DB;
                //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                var listing = db.Instruments.Where(i => i.InstrumentID == instrumentID).SingleOrDefault();
                if(listing==null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                if (!listing.Permissions().CanEdit) // if user isn't submitter and doesn't have edit privileges, forbidden!
                {
                    return RedirectToAction("Forbidden", "Error");
                }

                var hours = listing.InstrumentHours;
                var hourModel = new List<VenueHourViewModel>();
                foreach (var hour in hours)
                {
                    var mhour = new VenueHourViewModel();
                    mhour.DayOfWeekId = hour.Day;
                    
                    if(hour.CloseTime.GetValueOrDefault() == null && hour.OpenTime.GetValueOrDefault() == null)
                    {
                        mhour.Closed = true;
                    }
                    else
                    {
                        mhour.StartTime = new DateTime(2000,1,1,hour.OpenTime.Value.Hours,hour.OpenTime.Value.Minutes,hour.OpenTime.Value.Seconds);
                        mhour.EndTime = new DateTime(2000,1,1,hour.CloseTime.Value.Hours,hour.CloseTime.Value.Minutes,hour.CloseTime.Value.Seconds);

                    }
                    hourModel.Add(mhour);
                }


                var listingmodel = new ListingSubmissionViewModel()
                {
                    Lat = (double)listing.Lat,
                    Long = (double)listing.Long,
                    Price = (double?)listing.Price,
                    TimeSpanOfPrice = listing.TimeSpanOfPrice,
                    StreetAddress = listing.StreetAddress,
                    VenueName = listing.VenueName,
                    InstrumentID = listing.InstrumentID
                };
                listingmodel.Equipment = new EquipmentViewModel()
                {
                    Brand=listing.Brand,
                    Model=listing.Model
                };

                var model = new EditListingViewModel()
                {
                    Listing = listingmodel,
                    Hours=hourModel
                };
                return View(model);
            }
            catch
            {
                return RedirectToAction("NotFound", "Error");
            }
        }
        [Url("Instrument/Listing/Edit")]
        [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        [HttpPost]
        [VerifyReferrer]
        [RateLimit(Name = "InstrumentListingEditPOST", Seconds = 600)]
        public virtual ActionResult EditListing(EditListingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var db = Current.DB;
                var userGuid = (Guid)Membership.GetUser().ProviderUserKey; //http://stackoverflow.com/questions/924692/how-do-you-get-the-userid-of-a-user-object-in-asp-net-mvc and http://stackoverflow.com/questions/263486/how-to-get-current-user-in-asp-net-mvc
                var listing = db.Instruments.Where(i => i.InstrumentID == model.Listing.InstrumentID).SingleOrDefault();
                if (listing == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                var submitterGuid = listing.UserID;

                if (!listing.Permissions().CanEdit) // if user isn't submitter and doesn't have edit privileges, forbidden!
                {
                    return RedirectToAction("Forbidden", "Error");
                }

                var time = DateTime.Now;

                //LISTING:
                listing.StreetAddress = model.Listing.StreetAddress;
                listing.Lat = (decimal)model.Listing.Lat;
                listing.Long = (decimal)model.Listing.Long;
                listing.Model = model.Listing.Equipment.Model.Trim();
                listing.Brand = model.Listing.Equipment.Brand.Trim();
                listing.Price = (decimal?)model.Listing.Price;
                listing.TimeSpanOfPrice = model.Listing.TimeSpanOfPrice;
                listing.VenueName = model.Listing.VenueName;
                
                /*Matching instrument and style:
                 * 1. take instrument name, find match in Instruments table
                 * 2. apply SelectedIndex of type to dropdownlist, extract name from the list
                 * 3. Match name to a record in InstrumentTypes with InstrumentID from step 1 and Name from step 2
                 * 4. Apply ID of record in #3 to Listing
                 * 5. Same for Styles
                 * that's how we do it! */
                var instrument = db.InstrumentTypes.Where(i => i.Name == model.Listing.Equipment.Types.ElementAtOrDefault(model.Listing.Equipment.SelectedType).Text).SingleOrDefault();
                if (instrument == null)
                {
                    ModelState.AddModelError("Type", "No such instrument type exists.");
                    return View();
                }
                var style = model.Listing.Equipment.Classes.ElementAtOrDefault(model.Listing.Equipment.SelectedClass).Value;
                if (style == null || (style != "public" && style != "rent" && style != "sale"))
                {
                    ModelState.AddModelError("Style", "No such style exists.");
                    return View();
                }

                listing.UserID = userGuid;
                listing.SubmissionDate = time;

                db.SubmitChanges(); // Listing is changed

                //VENUE HOURS:
                db.InstrumentHours.DeleteAllOnSubmit(listing.InstrumentHours); // remove all previous venue hours

                foreach (var hour in model.Hours)
                {
                    var submit = new InstrumentHour();
                    submit.Day = hour.DayOfWeekId;
                    if (!hour.Closed)
                    {
                        submit.OpenTime = new TimeSpan(hour.StartTime.Value.Hour, hour.StartTime.Value.Minute, hour.StartTime.Value.Second);
                        submit.CloseTime = new TimeSpan(hour.EndTime.Value.Hour, hour.EndTime.Value.Minute, hour.EndTime.Value.Second);
                    }
                    else
                    {
                        submit.OpenTime = null;
                        submit.CloseTime = null;
                    }
                    submit.Instrument = listing;
                    db.InstrumentHours.InsertOnSubmit(submit);
                }
                db.SubmitChanges();

                // Search
                // Add to Lucene index:
                var s = new SearchController();
                s.ChangeIndex(listing);
                s = null;

                return RedirectToAction("Individual", new { instrumentID = listing.InstrumentID });
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion

        #region User Links
        // http://stackoverflow.com/questions/6247009/whats-a-clean-dry-way-to-show-available-operations-on-user-content

        /// <summary>
        /// Displays available actions on a listing as links.
        /// </summary>
        /// <param name="instrumentID">The instrument listing ID.</param>
        /// <returns></returns>
        public virtual ActionResult UserLinksForListing(int instrumentID)
        {
            var db = Current.DB;
            var instrument = db.Instruments.Where(i => i.InstrumentID == instrumentID).SingleOrDefault();
            if(instrument==null)
            {
                return RedirectToAction(MVC.Error.NotFound());
            }

            var model = instrument.Permissions();

            return PartialView(model);
        }

        /// <summary>
        /// Displays available actions on a review as links.
        /// </summary>
        /// <param name="instrumentID">The instrument review ID.</param>
        /// <returns></returns>
        public virtual ActionResult UserLinksForReview(int reviewID)
        {
            var db = Current.DB;
            var review = db.InstrumentReviews.Where(i => i.ReviewID == reviewID).SingleOrDefault();
            if (review == null)
            {
                return RedirectToAction(MVC.Error.NotFound());
            }

            var model = review.Permissions();

            return PartialView(model);
        }

        #endregion

    }
}
