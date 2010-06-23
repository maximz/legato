using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Controllers;
using System.Web.Security;

namespace FindPianos.Components
{
    public class AwesomeAuthorizeAttribute : ActionFilterAttribute
    {
        public bool AuthorizeSuspended
        {
            get;
            set;
        }
        public string AuthorizedRoles
        {
            get;
            set; 
        }
        public string UnauthorizedRoles
        {
            get;
            set;
        }

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
                else //User is authenticated
                {
                    if (AuthorizeSuspended != null)
                    {
                        if (!AuthorizeSuspended && !filterContext.HttpContext.User.IsInRole("ActiveUser"))
                        {
                            //Is suspended
                            filterContext.Result = (RedirectToRouteResult)new AccountController().ShowSuspensionStatus();
                            return;
                        }
                    }
                    //Unauthorized roles
                    bool isUnAuthorized = false;
                    if (!string.IsNullOrEmpty(UnauthorizedRoles))
                    {
                        if (!(UnauthorizedRoles.Trim() == ""))
                        {
                            var roleSplit = UnauthorizedRoles.Split(',');
                            foreach (var role in roleSplit)
                            {
                                if (filterContext.HttpContext.User.IsInRole(role.Trim()))
                                {
                                    isUnAuthorized = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (isUnAuthorized)
                    {
                        filterContext.Result = (RedirectToRouteResult)new ErrorController().Forbidden();
                        return;
                    }

                    //Authorized roles
                    bool isAuthorized = false;
                    if(!string.IsNullOrEmpty(AuthorizedRoles))
                    {
                        if(!(AuthorizedRoles.Trim() == ""))
                        {
                            var roleSplit = AuthorizedRoles.Split(',');
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
                    {
                        filterContext.Result = (RedirectToRouteResult)new ErrorController().Forbidden();
                        return;
                    }
                }
        }
    }
}