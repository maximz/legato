﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Legato.Helpers;
using System.Web.Security;
using System.Web.Routing;
using System.Web.Mvc;

namespace Legato.Models
{
    public partial class Instrument
    {
        /// <summary>
        /// The average overall rating given by the reviews for this Instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public int AverageOverallRating
        {
            get;
            internal set;
        }
        /// <summary>
        /// The last time a review for this Instrument was revised. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public DateTime? LatestReviewRevisionDate
        {
            get;
            internal set;
        }
        /// <summary>
        /// The latest time a reviewer for this Instrument used the instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        //public DateTime LatestUseDate
        //{
        //    get;
        //    internal set;
        //}
        /// <summary>
        /// The number of reviews that have been written for this Instrument. This property is filled only when the Instrument.FillProperties() method is called.
        /// </summary>
        public int NumberOfReviews
        {
            get;
            internal set;
        }

        /// <summary>
        /// The "title" of the instrument; used in URL slug, individual instrument page
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the URL slug (based on the Title)
        /// </summary>
        public string UrlSlug
        {
            get;
            internal set;
        }

        public string ListingClassDisplay
        {
            get;
            internal set;
        }

        /// <summary>
        /// If "instrument found in the wild", then false. If one the submitter owns and is renting out, then true.
        /// </summary>
        public bool RentalSubmission
        {
            get;
            internal set;
        }

        public void FillProperties()
        {
            // Arrange
            var OverallRatings = new List<int>();
            var RevisionDates = new List<DateTime>();
            //var UseDates = new List<DateTime>();
            var reviewCount = 0;

            // Get data
            var db = Current.DB;
                foreach (var review in db.InstrumentReviews.Where(rev => rev.InstrumentID == this.InstrumentID))
                {
                    var LatestRevision = db.InstrumentReviewRevisions.Where(revision => revision.ReviewID == review.ReviewID).OrderByDescending(revision => revision.RevisionDate).Take(1).ToList()[0];
                    OverallRatings.Add(LatestRevision.RatingGeneral);
                    RevisionDates.Add(LatestRevision.RevisionDate);
                    //UseDates.Add(LatestRevision.LastUseDate);
                    reviewCount++;
                }

            // Process
            AverageOverallRating = OverallRatings.Count > 0 ? ((int)Math.Round(OverallRatings.Average())) : 0;
            LatestReviewRevisionDate = RevisionDates.Count > 0 ? (DateTime?)RevisionDates.Max() : null; //this.SubmissionDate;
            //LatestUseDate = UseDates.Count > 0 ? UseDates.Max() : this.SubmissionDate;
            NumberOfReviews = reviewCount;

            // Generate Title and UrlSlug
            Title = "";
            if(this.Brand.HasValue())
            {
                Title += this.Brand.Trim() + " ";
            }
            if (this.Model.HasValue())
            {
                Title += this.Model.Trim() + " ";
            }
            if (this.InstrumentType != null && this.InstrumentType.Name.HasValue())
            {
                Title += this.InstrumentType.Name.Trim() + ": ";
            }
            if (this.StreetAddress.HasValue())
            {
                Title += DisplayedStreetAddress.GetValueOrDefault(StreetAddress).Trim() + " ";
            }
            Title = Title.Trim();
            UrlSlug = HtmlUtilities.URLFriendly(Title);

            var listClass = InstrumentClasses.Classes.Where(c => c.Id == Int32.Parse(this.ListingClass)).SingleOrDefault();
            ListingClassDisplay = listClass.DisplayName;
            RentalSubmission = (listClass.Name == "Rent");
        }

        public void PrepareRender()
        {
            DisplayedStreetAddress = DisplayedStreetAddress.GetValueOrDefault(StreetAddress);
            DisplayedLat = DisplayedLat.GetValueOrDefault(Lat);
            DisplayedLong = DisplayedLong.GetValueOrDefault(Long);
        }

        public ListingPermissionsModel Permissions()
        {
            if (Current.Request.IsAuthenticated)
            {
                var model = new ListingPermissionsModel();

                model.Listing = this;
                var userGuid = (Guid)Membership.GetUser().ProviderUserKey;

                //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                var submitterGuid = this.UserID;
                if (userGuid != submitterGuid && !Roles.IsUserInRole(RoleNames.Administrator) && !Roles.IsUserInRole(RoleNames.Moderator)) // if user isn't submitter and doesn't have edit privileges, forbidden!
                {
                    model.CanEdit = false;
                    model.CanDelete = false;
                }
                else
                {
                    model.CanEdit = true;
                    model.CanDelete = true;
                }

                // Check to see whether this user has already reviewed this instrument (checks using UserGuid and InstrumentID)
                var existingReview = Current.DB.InstrumentReviews.Where(r => r.UserID == userGuid && r.InstrumentID == this.InstrumentID).SingleOrDefault();
                if (existingReview != null)
                {
                    model.HasAlreadyReviewed = true;
                }
                else
                {
                    model.HasAlreadyReviewed = false;
                }

                var style = InstrumentClasses.Classes.Where(c => c.Id == Int32.Parse(this.ListingClass)).SingleOrDefault();
                if(style != null && style.CanMessageOwner && userGuid != submitterGuid) // if we are allowed to message owner and this user isn't the owner of this instrument
                {
                    model.CanMessage = true;
                }
                else
                {
                    model.CanMessage = false;
                }
                return model;
            }
            else // Anonymous user
            {
                return new ListingPermissionsModel() { CanComment = false, CanEdit = false, CanDelete = false, CanFlag = false, HasAlreadyReviewed = false, CanMessage = false, Listing = this };
            }
        }

        public class ListingPermissionsModel
        {
            public Instrument Listing
            {
                get;
                set;
            }
            public bool CanEdit
            {
                get;
                set;
            }
            public bool CanFlag
            {
                get;
                set;
            }
            public bool CanDelete
            {
                get;
                set;
            }
            public bool CanComment
            {
                get;
                set;
            }
            public bool HasAlreadyReviewed
            {
                get;
                set;
            }
            public bool CanMessage
            {
                get;
                set;
            }
        }

    }

    public partial class InstrumentReview
    {
        public List<InstrumentReviewRevision> Revisions
        {
            get;
            internal set;
        }
        public string Title
        {
            get;
            internal set;
        }

        public void FillProperties()
        {
            var db = Current.DB;
                this.Revisions = db.InstrumentReviewRevisions.Where(rev => rev.ReviewID == this.ReviewID).OrderByDescending(rev => rev.RevisionDate).ToList();
                this.Title = "Review on " + this.Instrument.Title;
        }

        public ReviewPermissionsModel Permissions()
        {
            if (Current.Request.IsAuthenticated)
            {
                var model = new ReviewPermissionsModel();

                model.Review = this;
                var userGuid = (Guid)Membership.GetUser().ProviderUserKey;

                //verify that the logged in user making the request is the original author of the post or is an Admin or a Moderator
                var query = this.InstrumentReviewRevisions.OrderByDescending(d=>d.RevisionDate);
                var revision = query.First();
                var submitterGuid = query.Last().UserID;

                if (userGuid != submitterGuid && !Roles.IsUserInRole(RoleNames.Administrator) && !Roles.IsUserInRole(RoleNames.Moderator)) // if user isn't submitter and doesn't have edit privileges, forbidden!
                {
                    model.CanEdit = false;
                    model.CanDelete = false;
                }
                else
                {
                    model.CanEdit = true;
                    model.CanDelete = true;
                }

                return model;
            }
            else // Anonymous user
            {
                return new ReviewPermissionsModel() {CanComment = false, CanDelete = false, CanEdit = false, CanFlag = false, Review = this};
            }
        }

        public class ReviewPermissionsModel
        {
            public InstrumentReview Review
            {
                get;
                set;
            }
            public bool CanEdit
            {
                get;
                set;
            }
            public bool CanFlag
            {
                get;
                set;
            }
            public bool CanDelete
            {
                get;
                set;
            }
            public bool CanComment
            {
                get;
                set;
            }
        }
    }

    public class BoundingBox
    {
        public LatLong extent1
        {
            get;
            set;
        }
        public LatLong extent2
        {
            get;
            set;
        }
    }
    public class LatLong
    {
        public decimal latitude
        {
            get;
            set;
        }
        public decimal longitude
        {
            get;
            set;
        }
    }

    public partial class InstrumentHour
    {
        public string FriendlyLabel()
        {
            return Enum.GetName(typeof(DayOfWeek), this.Day);
        }

        public string FriendlyOutput()
        {
            var output = "";
            if(!this.OpenTime.HasValue) // Closed
            {
                output = "Closed";
            }
            else // Open
            {
                output += TimeSpanToFriendlyOutput(this.OpenTime.Value);
                output += " - ";
                output += TimeSpanToFriendlyOutput(this.CloseTime.Value);
            }

            return output;
        }
        private string TimeSpanToFriendlyOutput(TimeSpan t)
        {
            var output = "";
            var am = true; // AM or PM
            if(t.Hours > 12)
            {
                am = false; // PM
                output += (t.Hours - 12).ToString();
            }
            else
            {
                output += t.Hours.ToString();
            }

            if(t.Minutes > 0)
            {
                output += (":" + t.Minutes);
            }

            output += (am ? "AM" : "PM");

            return output;
        }
    }

    public partial class Message
    {
        public string Subject
        {
            get;
            internal set;
        }
        public aspnet_User OtherUser
        {
            get;
            internal set;
        }
        public void FillProperties()
        {
            var db = Current.DB;
            var conversation = db.Conversations.Where(c => c.ConversationID == this.ConversationID).SingleOrDefault();
            Subject = conversation.Subject;
            if (Current.UserID.HasValue)
            {
                var currentUser = Current.UserID.Value;
                if (currentUser == SenderID)
                {
                    OtherUser = db.aspnet_Users.Where(u => u.UserId == ReceipientID).SingleOrDefault();
                }
                else if (currentUser == ReceipientID)
                {
                    OtherUser = db.aspnet_Users.Where(u => u.UserId == SenderID).SingleOrDefault();
                }
            }

        }
    }

    public partial class Conversation
    {
        public aspnet_User OtherUser
        {
            get;
            internal set;
        }
        public void FillProperties()
        {
            var db = Current.DB;
            if (Current.UserID.HasValue)
            {
                var currentUser = Current.UserID.Value;
                if(currentUser == this.User1)
                {
                    OtherUser = db.aspnet_Users.Where(u => u.UserId == this.User2).SingleOrDefault();
                }
                else if (currentUser == this.User2)
                {
                    OtherUser = db.aspnet_Users.Where(u => u.UserId == this.User1).SingleOrDefault();
                }
            }

        }
    }

    /// <summary>
    /// Global reference to a specific post in some category
    /// </summary>
    public partial class GlobalPostID
    {
        /// <summary>
        /// Gets the underlying type.
        /// </summary>
        /// <value>
        /// The underlying type.
        /// </value>
        public Type UnderlyingType
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets the underlying post.
        /// </summary>
        public dynamic UnderlyingPost
        {
            get;
            internal set;
        }

        public string Title
        {
            get;
            internal set;
        }

        public ActionResult DetailsRoute
        {
            get;
            internal set;
        }

        /// <summary>
        /// Fills the properties UnderlyingType and UnderlyingPost.
        /// </summary>
        public void FillProperties()
        {
            var db = Current.DB;
            switch(this.PostCategory)
            {
                case MagicCategoryStrings.Instrument:
                    UnderlyingType = typeof(Instrument);
                    UnderlyingPost = db.Instruments.Where(p => p.InstrumentID == this.SpecificPostID).SingleOrDefault();
                    DetailsRoute = MVC.Instruments.Individual((UnderlyingPost as Instrument).InstrumentID, (UnderlyingPost as Instrument).UrlSlug);
                    Title = UnderlyingPost.Title;
                    break;
                case MagicCategoryStrings.InstrumentReview:
                    UnderlyingType = typeof(InstrumentReview);
                    UnderlyingPost = db.InstrumentReviews.Where(r => r.ReviewID == this.SpecificPostID).SingleOrDefault();
                    DetailsRoute = MVC.Instruments.IndividualReview((UnderlyingPost as InstrumentReview).ReviewID);
                    Title = UnderlyingPost.Title;
                    break;
                case MagicCategoryStrings.InstrumentReviewRevision:
                    UnderlyingType = typeof(InstrumentReviewRevision);
                    UnderlyingPost = db.InstrumentReviewRevisions.Where(rev => rev.RevisionID == this.SpecificPostID).SingleOrDefault();
                    DetailsRoute = MVC.Instruments.IndividualReview((UnderlyingPost as InstrumentReviewRevision).ReviewID);
                    break;
                case MagicCategoryStrings.Message:
                    UnderlyingType = typeof(Message);
                    UnderlyingPost = db.Messages.Where(m => m.MessageID == this.SpecificPostID).SingleOrDefault();
                    UnderlyingPost.FillProperties();
                    DetailsRoute = MVC.Messages.Message((UnderlyingPost as Message).MessageID);
                    Title = UnderlyingPost.Subject + " - Conversation with " + UnderlyingPost.OtherUser.UserName;
                    break;
                case MagicCategoryStrings.Conversation:
                    UnderlyingType = typeof(Conversation);
                    UnderlyingPost = db.Conversations.Where(c => c.ConversationID == this.SpecificPostID).SingleOrDefault();
                    UnderlyingPost.FillProperties();
                    DetailsRoute = MVC.Messages.Thread((UnderlyingPost as Conversation).ConversationID);
                    Title = UnderlyingPost.Subject + " - Conversation with "+UnderlyingPost.OtherUser.UserName;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}