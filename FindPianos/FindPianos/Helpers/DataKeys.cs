﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Helpers
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
}