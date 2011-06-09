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
    public static class RoleNames
    {
        public const string ActiveUser = "ActiveUser";
        public const string EmailNotConfirmed = "EmailNotConfirmed";
        public const string Administrator = "Administrator";
        public const string Moderator = "Moderator";
    }
}