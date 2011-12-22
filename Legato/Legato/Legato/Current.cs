using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Legato.Models;
using System.Text.RegularExpressions;
using Legato.Helpers;
using System.Collections.Specialized;
using System.Net;
using Legato.Controllers;
using System.Runtime.Remoting.Messaging;
using System.Web.Caching;
using MvcMiniProfiler;
using MvcMiniProfiler.Data;
using System.Reflection;
using System.Diagnostics;
using System.Web.Security;

namespace Legato
{
    /// <summary>
    /// Helper class that provides quick access to common objects used across a single request.
    /// </summary>
    public static class Current
    {
        //const string DISPOSE_CONNECTION_KEY = "dispose_connections";

        /// <summary>
        /// Shortcut to HttpContext.Current.
        /// </summary>
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        /// <summary>
        /// Shortcut to HttpContext.Current.Request.
        /// </summary>
        public static HttpRequest Request
        {
            get { return Context.Request; }
        }

        public static Guid? UserID
        {
            get
            {
                if(!Request.IsAuthenticated)
                {
                    return null;
                }
                return (Guid)Membership.GetUser().ProviderUserKey;
            }
        }

        public static string UserName
        {
            get
            {
                if (!Request.IsAuthenticated)
                {
                    return null;
                }
                return Membership.GetUser().UserName;
            }
        }

        public static MembershipUser User
        {
            get
            {
                if (!Request.IsAuthenticated)
                {
                    return null;
                }
                return Membership.GetUser();
            }
        }

        /// <summary>
        /// Gets the controller for the current request; should be set during init of current request's controller.
        /// </summary>
        public static CustomControllerBase Controller
        {
            get { return Context.Items["Controller"] as CustomControllerBase; }
            set { Context.Items["Controller"] = value; }
        }

        /// <summary>
        /// Returns the current MiniProfiler. Shorthand for MiniProfiler.Current - just so that all our common things can be stored under one class (Current)
        /// </summary>
        public static MiniProfiler MiniProfiler
        {
            get { return MiniProfiler.Current; }
        }


        ///// <summary>
        ///// Gets the current "authenticated" user from this request's controller.
        ///// </summary>
        //public static User User
        //{
        //    get { return Controller.CurrentUser; }
        //}

        /// <summary>
        /// Gets the single data context for this current request.
        /// </summary>
        public static LegatoDataContext DB
        {
            get
            {
                LegatoDataContext result = null;

                if (Context != null)
                {
                    result = Context.Items["DB"] as LegatoDataContext;
                }
                else
                {
                    // unit tests
                    result = CallContext.GetData("DB") as LegatoDataContext;
                }

                if (result == null)
                {
                    //result = new LegatoDataContext(); // without MvcMiniProfiler
                    result = LegatoDataContext.GetContext(); // so that MvcMiniProfiler SQL logging is enabled - see Models --> SqlProfiling.cs
                    if (Context != null)
                    {
                        Context.Items["DB"] = result;
                    }
                    else
                    {
                        CallContext.SetData("DB", result);
                    }
                }

                return result;
            }
        }


        /// <summary>
        /// Allows end of reqeust code to clean up this request's DB.
        /// </summary>
        public static void DisposeDB()
        {
            LegatoDataContext db = null;
            if (Context != null)
            {
                db = Context.Items["DB"] as LegatoDataContext;
            }
            else
            {
                db = CallContext.GetData("DB") as LegatoDataContext;
            }
            if (db != null)
            {
                db.Dispose();
                if (Context != null)
                {
                    Context.Items["DB"] = null;
                }
                else
                {
                    CallContext.SetData("DB", null);
                }
            }
        }

        /// <summary>
        /// retrieve an integer from the HttpRuntime.Cache; returns 0 if value does not exist
        /// </summary>
        public static int GetCachedInt(string key)
        {
            object o = HttpRuntime.Cache[key];
            if (o == null) return 0;
            return (int)o;
        }

        /// <summary>
        /// remove a cached object from the HttpRuntime.Cache
        /// </summary>
        public static void RemoveCachedObject(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// retrieve an object from the HttpRuntime.Cache
        /// </summary>
        public static object GetCachedObject(string key)
        {
            return HttpRuntime.Cache[key];
        }

        /// <summary>
        /// add an object to the HttpRuntime.Cache with an absolute expiration time
        /// </summary>
        public static void SetCachedObject(string key, object o, int durationSecs)
        {
            HttpRuntime.Cache.Add(
                key,
                o,
                null,
                DateTime.Now.AddSeconds(durationSecs),
                Cache.NoSlidingExpiration,
                CacheItemPriority.High,
                null);
        }

        /// <summary>
        /// add an object to the HttpRuntime.Cache with a sliding expiration time. sliding means the expiration timer is reset each time the object is accessed, so it expires 20 minutes, for example, after it is last accessed.
        /// </summary>
        public static void SetCachedObjectSliding(string key, object o, int slidingSecs)
        {
            HttpRuntime.Cache.Add(
                key,
                o,
                null,
                Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 0, slidingSecs),
                CacheItemPriority.High,
                null);
        }

        /// <summary>
        /// add a non-removable, non-expiring object to the HttpRuntime.Cache
        /// </summary>
        public static void SetCachedObjectPermanent(string key, object o)
        {
            HttpRuntime.Cache.Remove(key);
            HttpRuntime.Cache.Add(
                key,
                o,
                null,
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable,
                null);
        }

        /// <summary>
        /// retrieves a string from the HttpContext.Cache, or null if the key doesn't exist
        /// </summary>
        public static string GetCachedString(string key)
        {
            object o = HttpRuntime.Cache[key];
            if (o != null) return o.ToString();
            return null;
        }

        /// <summary>
        /// places a string in the HttpContext.Cache
        /// cached with "sliding expiration", so will only be deleted if NOT accessed for durationSecs
        /// </summary>
        public static void SetCachedString(string key, int durationSecs, string s)
        {
            HttpRuntime.Cache.Add(
                key,
                s,
                null,
                DateTime.MaxValue,
                TimeSpan.FromSeconds(durationSecs),
                CacheItemPriority.High,
                null);
        }


        /// <summary>
        /// Gets the current "revision number" slug (for busting our caching when we update files).
        /// </summary>
        /// <returns></returns>
        public static string RevNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion; // gets file version info

            var lastTwoPartsOfVersion = version.Substring(version.IndexOf(".", version.IndexOf(".") + 1) + 1); // gets substring of version, starting at index of second dot (first dot's index is used as startIndex in indexOf)
            return HtmlUtilities.URLFriendly(lastTwoPartsOfVersion);
        }

        /// <summary>
        /// Gets the current Google Analytics key.
        /// </summary>
        /// <returns></returns>
        public static string GAnalyticsKey()
        {
            var cacheKey = "GAnalyticsKey";
            var cacheContents = Current.GetCachedString(cacheKey);

            if (cacheContents == null)
            {
                var x = System.Configuration.ConfigurationManager.AppSettings["GAnalyticsKey"];
                Current.SetCachedObjectPermanent(cacheKey, x); // add to cache
                return x;
            }
            else
            {
                return cacheContents;
            }
        }

    }
}