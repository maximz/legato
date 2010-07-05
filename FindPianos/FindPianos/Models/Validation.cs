using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace FindPianos.Models
{
    #region Validation Models
    public partial class UserSuspension
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Reason))
                yield return new RuleViolation("A suspension reason is required.", "Reason");
            if (ReinstateDate == null)
                yield return new RuleViolation("A reinstate date is required.", "ReinstateDate");
            else if (ReinstateDate <= SuspensionDate)
                yield return new RuleViolation("Reinstate date should be after suspension date.", "ReinstateDate");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class ListingFlag
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            using(var db = new LegatoDataContext())
            {
                if (ListingID == null)
                    yield return new RuleViolation("Invalid listing.", "ListingID");
                else if (db.Listings.Where(l => l.ListingID == this.ListingID).Count() != 1)
                    yield return new RuleViolation("Invalid listing.", "ListingID");
                if (TypeID == null)
                    yield return new RuleViolation("Invalid flag type.", "TypeID");
                else if (db.FlagTypes.Where(t => t.FlagTypeID == this.TypeID).Count() != 1)
                    yield return new RuleViolation("Invalid flag type.", "TypeID");

            yield break;
            }
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class ReviewFlag
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            using (var db = new LegatoDataContext())
            {
                if (ReviewID == null)
                    yield return new RuleViolation("Invalid review.", "ReviewID");
                else if (db.Reviews.Where(l => l.PianoReviewID == this.ReviewID).Count() != 1)
                    yield return new RuleViolation("Invalid review.", "ReviewID");
                if (TypeID == null)
                    yield return new RuleViolation("Invalid flag type.", "TypeID");
                else if (db.FlagTypes.Where(t => t.FlagTypeID == this.TypeID).Count() != 1)
                    yield return new RuleViolation("Invalid flag type.", "TypeID");

                yield break;
            }
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class ReviewComment
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (!string.IsNullOrEmpty(this.MessageText))
            {
                if (this.MessageText.Trim() == "")
                {
                    yield return new RuleViolation("No text entered for this comment.", "Message");
                }
            }
            else
                yield return new RuleViolation("No text entered for this comment.", "Message");

                yield break;
            
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving.");

        }
    }
    public partial class PianoListingComment
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (!string.IsNullOrEmpty(this.MessageText))
            {
                if (this.MessageText.Trim() == "")
                {
                    yield return new RuleViolation("No text entered for this comment.", "Message");
                }
            }
            else
                yield return new RuleViolation("No text entered for this comment.", "Message");

            yield break;

        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving.");

        }
    }
    public partial class Review
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            using (var db = new LegatoDataContext())
            {
                if (this.ListingID == null)
                    yield return new RuleViolation("Listing ID is required", "ListingID");
                else if (db.Listings.Where(l=>l.ListingID==this.ListingID).Count() != -1)
                    yield return new RuleViolation("Listing doesn't exist", "ListingID");

                yield break;
            }
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class Listing
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (Lat == 0 || Long == 0)
                yield return new RuleViolation("We weren't able to locate that street address.", "StreetAddress");
            if (string.IsNullOrEmpty(StreetAddress))
                yield return new RuleViolation("A street address is required", "StreetAddress");
            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class ReviewRevision
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            using(var db = new LegatoDataContext())
            {
                if (PianoReviewID == null)
                    yield return new RuleViolation("PianoReview is required", "PianoReviewID");
                else if (db.PianoReviews.Where(r => r.PianoReviewID == this.PianoReviewID).Count() != 1)
                    yield return new RuleViolation("PianoReview is invalid", "PianoReviewID");
                if (PianoStyleID == null)
                    yield return new RuleViolation("PianoStyle is required", "PianoStyleID");
                else if (db.PianoStyles.Where(s => s.PianoStyleID== this.PianoStyleID).Count() != 1)
                    yield return new RuleViolation("PianoStyle is invalid", "PianoStyleID");
                if (RatingOverall == null)
                    yield return new RuleViolation("Overall rating is required", "RatingOverall");
                if (PricePerHourInUSD == null)
                    yield return new RuleViolation("Price per hour is required", "PricePerHourInUSD");
                else if (PricePerHourInUSD < 0)
                    yield return new RuleViolation("Price per hour must not be less than 0", "PricePerHourInUSD");

                yield break;
            }
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    public partial class PianoVenueHour
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            using(var db = new LegatoDataContext())
            {
                if (ReviewRevisionID == null)
                    yield return new RuleViolation("Review revision ID is required.", "ReviewRevisionID");
                else if (db.PianoReviewRevisions.Where(r => r.PianoReviewRevisionID == this.ReviewRevisionID).Count() != 1)
                    yield return new RuleViolation("Review revision ID is invalid.", "ReviewRevisionID");
                if (DayOfWeek == null)
                    yield return new RuleViolation("Day of week is required.", "DayOfWeek");
                else if (db.WeekDays.Where(d => d.WeekDayID == this.DayOfWeek).Count() != 1)
                    yield return new RuleViolation("Day of week is invalid.", "DayOfWeek");

                yield break;
            }
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }
    #endregion

    #region Base Validation Class(es)

    /// <summary>
    /// Denotes a violation of a validation rule of a certain Type
    /// </summary>
    public class RuleViolation
    {
        /// <summary>
        /// The error message that exposes the validation error.
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// Denotes what property is causing the validation error.
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// Default constructor. Creates a new RuleViolation object with the parameters.
        /// </summary>
        /// <param name="errorMessage">The error message that exposes the validation error.</param>
        /// <param name="propertyName">Denotes what property is causing the validation error.</param>
        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
    
    #endregion
}