using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Helpers;

namespace Legato.Controllers
{
    [HandleError]
    [CustomAuthorization(AuthorizeEmailNotConfirmed=false, AuthorizeSuspended=false)]
    public class MessagesController : CustomControllerBase
    {
        //
        // GET: /Messages/

        [Url("messages")]
        public virtual ActionResult List()
        {
            return View();
        }

        [Url("messages/{id}")]
        public virtual ActionResult Thread(int id) // individual thread
        {
            return View();
        }
        public virtual ActionResult Message() // individual message
        {
            // redirect to thread page with #messageid
            return View();
        }
        public virtual ActionResult Reply()
        {
            return View();

        }
        public virtual ActionResult Flag()
        {
            return View();
        }
        public virtual ActionResult Compose()
        {
            return View();
        }
    }
}
