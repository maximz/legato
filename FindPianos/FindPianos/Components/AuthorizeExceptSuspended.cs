using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Controllers;
using System.Web.Security;

namespace FindPianos.Components
{
    public class AuthorizeExceptSuspendedAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {

                    //use the current url for the redirect
                    string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;

                    //send them off to the login page
                    string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                    string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
                    filterContext.HttpContext.Response.Redirect(loginUrl, true);

                }
                else
                {
                    if (!filterContext.HttpContext.User.IsInRole("ActiveUser"))
                    {
                        //Is suspended
                        filterContext.Result = (RedirectToRouteResult)new AccountController().ShowSuspensionStatus();
                    }
                    bool isAuthorized = false;
                    if(!string.IsNullOrEmpty(Roles))
                    {
                        if(!(Roles.Trim() == ""))
                        {
                            var roleSplit = Roles.Split(',');
                            foreach (var role in roleSplit)
                            {
                                if (filterContext.HttpContext.User.IsInRole(role.Trim()))
                                {
                                    isAuthorized = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        isAuthorized = true;
                    }
                    if (!isAuthorized)
                        filterContext.Result = (RedirectToRouteResult)new ErrorController().Forbidden();
                }
        }
    }
}