﻿using System;
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

    public static class AddressPrivacySettings
    {
        public const string ValueField = "Id";
        public const string TextField = "DisplayName";
        public static IEnumerable<AddressPrivacySetting> Settings = new[] {
            new AddressPrivacySetting() { Id = 1, Name = "Full", DisplayName = "Display full address", CanSeeFullAddress = true, CanSeeZipCode = true, CanSeeCity = true },
            new AddressPrivacySetting() { Id = 2, Name = "Zip", DisplayName = "Display neighborhood only", CanSeeFullAddress = false, CanSeeZipCode = true, CanSeeCity = true }
            //, new AddressPrivacySetting() { Id = 3, Name = "City", DisplayName = "Display city only", CanSeeFullAddress = false, CanSeeZipCode = false, CanSeeCity = true }
        };
    }

    public class AddressPrivacySetting
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }

        public bool CanSeeFullAddress
        {
            get;
            set;
        }
        public bool CanSeeZipCode
        {
            get;
            set;
        }
        public bool CanSeeCity
        {
            get;
            set;
        }

    }

    public static class InstrumentClasses
    {
        public const string ValueField = "Id";
        public const string TextField = "DisplayName";
        public static IEnumerable<InstrumentClass> Classes = new[] {
            new InstrumentClass() { Id = 1, Name = "Public", DisplayName = "A public instrument", ReviewBySubmitterEnabled = true, CanMessageOwner = false },
            new InstrumentClass() { Id = 2, Name = "Rent", DisplayName = "My own instrument", ReviewBySubmitterEnabled = false, CanMessageOwner = true }
            //new { Id = 3, Name = "Sale" },
        };
    }

    public class InstrumentClass
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether review by submitter is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [reviews enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ReviewBySubmitterEnabled
        {
            get;
            set;
        }

        public bool CanMessageOwner
        {
            get;
            set;
        }

    }
}