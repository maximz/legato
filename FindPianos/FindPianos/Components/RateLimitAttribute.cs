using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Net;

namespace FindPianos.Components
{
    /// <summary>
    /// Decorates any MVC route that needs to have client requests limited by time.
    /// </summary>
    /// <remarks>
    /// Uses the current System.Web.Caching.Cache to store each client request to the decorated route. See http://stackoverflow.com/questions/33969/best-way-to-implement-request-throttling-in-asp-net-mvc/1318059#1318059 and http://stackoverflow.com/questions/3082084/how-do-i-implement-rate-limiting-in-an-asp-net-mvc-site/ for more information.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// A unique name for this Throttle.
        /// </summary>
        /// <remarks>
        /// We'll be inserting a Cache record based on this name and client IP, e.g. "Name-192.168.0.1"
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The number of seconds clients must wait before executing this decorated route again.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// A text message that will be sent to the client upon throttling.  You can include the token {n} to
        /// show this.Seconds in the message, e.g. "Wait {n} seconds before trying again".
        /// </summary>
        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
            var allowExecute = false;

            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true, // is this the smallest data we can have?
                    null, // no dependencies
                    DateTime.Now.AddSeconds(Seconds), // absolute expiration
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null); // no callback

                allowExecute = true;
            }

            if (!allowExecute)
            {
                if (String.IsNullOrEmpty(Message))
                    Message = "You may only perform this action every {n} seconds.";

                c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }
    }

    //TO USE:
    //[RateLimit(Name="TestThrottle", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 5)]
    //public ActionResult TestThrottle()
    //{
    //    return Content("TestThrottle executed");
    //}
}