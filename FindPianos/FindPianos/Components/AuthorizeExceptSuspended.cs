using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindPianos.Components
{
    public class AuthorizeAllExceptSuspended : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Is suspended
                //filterContext.Result = new RedirectToRouteResult(); //TODO
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }
    }
}