using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Helpers;

namespace Legato.Controllers
{
    public partial class LegalController : CustomControllerBase
    {
        //
        // GET: /Legal/
        [Url("Legal")]
        [CustomCache(Duration = 14400, NoCachingForAuthenticatedUsers = true)]
        public virtual ActionResult Index()
        {
            return View();
        }
        [Url("Legal/Terms")]
        [CustomCache(Duration=14400,NoCachingForAuthenticatedUsers=true)]
        public virtual ActionResult TermsOfService()
        {
            return View();
        }
        [Url("Legal/Privacy")]
        [CustomCache(Duration = 14400, NoCachingForAuthenticatedUsers = true)]
        public virtual ActionResult Privacy()
        {
            return View();
        }
        [Url("Legal/Content")]
        [CustomCache(Duration = 14400, NoCachingForAuthenticatedUsers = true)]
        public virtual ActionResult Content()
        {
            return View();
        }

    }
}
