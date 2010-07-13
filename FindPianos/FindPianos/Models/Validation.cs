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
                else if (db.Reviews.Where(l => l.ReviewID == this.ReviewID).Count() != 1)
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
    public partial class ListingComment
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