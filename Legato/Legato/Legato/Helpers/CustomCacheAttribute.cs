using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Legato.Helpers
{
    /// <summary>
    /// [OutputCache] but better.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomCacheAttribute : OutputCacheAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether [no caching for authenticated users].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [no caching for authenticated users]; otherwise, <c>false</c>.
        /// </value>
        public bool NoCachingForAuthenticatedUsers
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [allow only valid search engines].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [allow only valid search engines]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowOnlyValidSearchEngines
        {
            get;
            set;
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (VaryByParam.IsNullOrEmpty())
            {
                VaryByParam = "none"; // see http://stackoverflow.com/questions/288608/asp-net-mvc-output-caching-the-directive-or-the-configuration-settings-profile-m
            }
            VaryByParam = VaryByParam.Replace(",", ";"); //proper delimiter
            
            this.Location = System.Web.UI.OutputCacheLocation.Client; // to make sure people only get their own cached stuff
            Current.Context.Response.Cache.SetCacheability(HttpCacheability.Private); // same thing, just in case

            if (AllowOnlyValidSearchEngines)
            {
                var IsSearchEngine = (filterContext.Controller as Legato.Controllers.CustomControllerBase).IsSearchEngineDns();
                if (IsSearchEngine)
                {
                    base.OnResultExecuting(filterContext);
                    return;
                }
                return;
            }
            else
            {
                if (NoCachingForAuthenticatedUsers)
                {
                    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        base.OnResultExecuting(filterContext);
                    }
                    return;
                }
                else
                {
                    base.OnResultExecuting(filterContext);
                    return;
                }
            }
        }
    }
}