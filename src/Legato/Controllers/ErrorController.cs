using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using System.Net;
using Legato.Helpers;

namespace Legato.Controllers
{
    [CustomCache(NoCachingForAuthenticatedUsers=true,Duration=7200,VaryByParam="None")]
    public partial class ErrorController : CustomControllerBase
    {
        //
        // GET: /Error/

        /// <summary>
        /// Returns a Not Found error.
        /// </summary>
        /// <returns></returns>
        [Url("404")]
        public virtual new ActionResult NotFound()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View();
        }
        /// <summary>
        /// Returns a Forbidden error.
        /// </summary>
        /// <returns></returns>
        [Url("403")]
        public virtual new ActionResult Forbidden()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View();
        }

        /// <summary>
        /// Returns an Unauthorized error.
        /// </summary>
        /// <returns></returns>
        [Url("401")]
        public virtual new ActionResult Unauthorized()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View("Forbidden");
        }

        [Url("409")]
        public virtual new ActionResult Conflict()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View("Conflict");
        }

        /// <summary>
        /// Returns an Internal Server Error.
        /// </summary>
        /// <returns></returns>
        [Url("500")]
        public virtual new ActionResult InternalServerError()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View();
        }
        /// <summary>
        /// Returns a Bad Request error.
        /// </summary>
        /// <returns></returns>
        [Url("400")]
        public virtual new ActionResult BadRequest()
        {
            try
            {
                Current.Context.Response.Clear();
                Current.Context.Response.ClearHeaders();
                Current.Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch
            {
                // there may be errors because "headers cannot be cleared/changed after they have been sent"
            }
            return View();
        }
        /// <summary>
        /// Handles all errors.
        /// </summary>
        /// <returns></returns>
        [Url("Error")]
        [CustomCache(NoCachingForAuthenticatedUsers=true,Duration = 7200, VaryByHeader = "Status")] //TODO
        public virtual new ActionResult AnyError()
        {
            try
            {
                switch (Current.Context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.NotFound:
                        return View("NotFound");
                    case (int)HttpStatusCode.Forbidden:
                        return View("Forbidden");
                    case (int)HttpStatusCode.Unauthorized:
                        return View("Forbidden");
                    case (int)HttpStatusCode.Conflict:
                        return View("Conflict");
                    case (int)HttpStatusCode.BadRequest:
                        return View("BadRequest");
                    case (int)HttpStatusCode.InternalServerError:
                        return View("InternalServerError");
                    default:
                        return View("InternalServerError");
                }
            }
            catch
            {
                return View("InternalServerError");
            }
        }

    }
}
