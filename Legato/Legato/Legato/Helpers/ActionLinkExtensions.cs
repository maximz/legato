using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Legato.Helpers
{
    /// <summary>
    /// This helper class is meant to resolve bugs and missing features within the ActionLink methods in MVC.
    /// </summary>
    public static class ActionLinkExtensions
    {
        /// <summary>
        /// Makes an anchor tag to an action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        /// <remarks> The original set of ActionLink methods did not include one with controllerName and routeValues, so any such ActionLink calls were being passed to some other method that accepts object instead of string - that's why we got weird URLs with ?length=7 query strings. If you don't want to use this, you can just add ", new object { }" to the end of your method call, but inside the parantheses.</remarks>
        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return System.Web.Mvc.Html.LinkExtensions.ActionLink(htmlHelper, linkText, actionName, controllerName, routeValues, new object { }).ToHtmlString();
        }

        /// <summary>
        /// Gets the application root.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationRoot()
        {
            return GetBaseUrl().AbsoluteUri;
        }

        // See http://blog.veggerby.dk/2009/01/13/getting-an-absolute-url-from-aspnet-mvc/

        /// <summary>
        /// Builds the URL from root.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns></returns>
        public static string BuildURLFromRoot(string relativePath)
        {
            //return new Uri(GetBaseUrl(url), url.Action(actionName, controllerName)).AbsoluteUri; // URLHelper
            return new Uri(GetBaseUrl(), relativePath).AbsoluteUri;
        }

        /// <summary>
        /// Gets the application root URI.
        /// </summary>
        /// <returns></returns>
        public static Uri GetBaseUrl()
        {
            Uri contextUri = new Uri(HttpContext.Current.Request.Url, HttpContext.Current.Request.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri) { Path = HttpContext.Current.Request.ApplicationPath, Query = null, Fragment = null };
            return realmUri.Uri;
        }
    }
}