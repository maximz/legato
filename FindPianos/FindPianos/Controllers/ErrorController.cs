using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;

namespace FindPianos.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        [Url("404")]
        public ActionResult NotFound()
        {
            return View();
        }

    }
}
