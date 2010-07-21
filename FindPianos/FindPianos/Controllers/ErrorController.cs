using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using System.Net;

namespace FindPianos.Controllers
{
    [OutputCache(Duration=7200,VaryByParam="None")]
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        /// <summary>
        /// Returns a Not Found error.
        /// </summary>
        /// <returns></returns>
        [Url("404")]
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
        /// <summary>
        /// Returns a Forbidden error.
        /// </summary>
        /// <returns></returns>
        [Url("403")]
        public ActionResult Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View();
        }

        /// <summary>
        /// Returns an Unauthorized error.
        /// </summary>
        /// <returns></returns>
        [Url("401")]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("Forbidden");
        }

        [Url("409")]
        public ActionResult Conflict()
        {
            Response.StatusCode = (int)HttpStatusCode.Conflict;
            return View("Conflict");
        }

        /// <summary>
        /// Returns an Internal Server Error.
        /// </summary>
        /// <returns></returns>
        [Url("500")]
        public ActionResult InternalServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
        /// <summary>
        /// Handles all errors.
        /// </summary>
        /// <returns></returns>
        [Url("Error")]
        [OutputCache(Duration = 7200, VaryByHeader = "StatusCode")] //TODO
        public ActionResult AnyError()
        {
            try
            {
                switch (Response.StatusCode)
                {
                    case (int)HttpStatusCode.NotFound:
                        return View("NotFound");
                    case (int)HttpStatusCode.Forbidden:
                        return View("Forbidden");
                    case (int)HttpStatusCode.Unauthorized:
                        return View("Forbidden");
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
