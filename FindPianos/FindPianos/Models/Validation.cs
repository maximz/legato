using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace FindPianos.Models
{
    public partial class PianoReviewRevision
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (RatingOverall == null)
                yield return new RuleViolation("Overall rating is required", "RatingOverall");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");

        }
    }


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
}