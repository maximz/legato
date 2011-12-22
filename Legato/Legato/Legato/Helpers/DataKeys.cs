using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.Helpers
{
    /// <summary>
    /// Contains keys for common app-wide keys (used in querystring, form, etc)
    /// </summary>
    public static class Keys
    {
        public const string OpenId = "openid_identifier";
        public const string Session = "s";
        public const string ReturnUrl = "returnurl";
        public const string UserFlag = "m";
    }
    /// <summary>
    /// Used to store magic strings for role names.
    /// </summary>
    public static class RoleNames
    {
        public const string ActiveUser = "ActiveUser";
        public const string EmailNotConfirmed = "EmailNotConfirmed";
        public const string Administrator = "Administrator";
        public const string Moderator = "Moderator";

        /// <summary>
        /// Returns a string of role names, concatenated by ",", to be used within CustomAuthorizationAttribute.
        /// </summary>
        /// <param name="role">The role list.</param>
        /// <returns></returns>
        public static string ToAttributeList(params string[] role)
        {
            return string.Join(",", role);
        }

    }
    /// <summary>
    /// Used to store magic strings for category names, such as for GlobalPostIDs and for search. 
    /// </summary>
    public static class MagicCategoryStrings
    {
        public const string Instrument = "Instrument";
        public const string InstrumentReview = "InstrumentReview";
        public const string InstrumentReviewRevision = "InstrumentReviewRevision";
        public const string Message = "Message";
        public const string Conversation = "Conversation";
    }
}