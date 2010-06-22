using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindPianos.Components
{
    public class AuthorizeExceptSuspendedAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Either is suspended or just isn't of the required role
                if (!filterContext.HttpContext.User.IsInRole("ActiveUser"))
                {
                    //Is suspended
                    //RedirectToAction("ShowSuspensionStatus", "Account");
                    //filterContext.Result = new RedirectToRouteResult(); //TODO: route = "Account/Suspended"
                }
                else
                {
                    //Not of the required role.
                    //filterContext.Result = new RedirectToRouteResult(); //TODO: route = "403"
                }
                
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }
    }
}