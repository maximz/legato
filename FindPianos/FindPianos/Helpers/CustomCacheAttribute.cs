using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindPianos.Helpers
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
            if (AllowOnlyValidSearchEngines)
            {
                //TODO! See http://stackoverflow.com/questions/3465072/accessing-controller-method-from-resultexecutingcontext-in-asp-net-mvc-site
                var IsSearchEngine = (filterContext.Controller as FindPianos.Controllers.CustomControllerBase).IsSearchEngineDns();
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