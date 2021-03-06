﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Net;

namespace Legato.Helpers
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
        /// The number of seconds clients must wait before executing the decorated route again.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// A text message that will be sent to the client upon throttling.  You can include the token {n} to
        /// show this.Seconds in the message, e.g. "Wait {n} seconds before trying again".
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// If the user is in these roles, they are not rate limited.
        /// </summary>
        public string ExceptForRoles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
            var allowExecute = false;
            var isInRole = false;

            //Checking whether the user is in one of the allowed roles
            if(c.HttpContext.User.Identity.IsAuthenticated)
            {
                if (ExceptForRoles != null)
                {
                    foreach (var role in ExceptForRoles.Split(','))
                    {
                        if (c.HttpContext.User.IsInRole(role))
                        {
                            isInRole = true;
                            break;
                        }
                    }
                }

                if(isInRole)
                {
                    allowExecute = true;
                }
            }

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

                // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
                //c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict; // TODO: why is this not working?
                c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
            }
        }

        /// <summary>
        /// Cancels the rate limit (if a request fails validation, for example)
        /// </summary>
        public void CancelRateLimit(string name)
        {
            Name = name;
            var key = string.Concat(Name, "-", Current.Context.Request.UserHostAddress);
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
    }

    /*TO USE:
    [RateLimit(Name="TestThrottle", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 5)]
    public ActionResult TestThrottle()
    {
       return Content("TestThrottle executed");
    }
     * */
}