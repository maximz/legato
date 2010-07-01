﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Controllers;
using System.Web.Security;

namespace FindPianos.Helpers
{
    public class AwesomeAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether suspended users are authorized.
        /// </summary>
        /// <value><c>true</c> if suspended users are authorized; otherwise, <c>false</c>.</value>
        /// <remarks>Default value: <c>false</c></remarks>
        public bool AuthorizeSuspended
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether users who have not confirmed their email addresses are authorized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if users who have not confirmed their email addresses are authorized; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Default value: <c>false</c></remarks>
        public bool AuthorizeEmailNotConfirmed
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the authorized roles.
        /// </summary>
        /// <value>The authorized roles.</value>
        public string AuthorizedRoles
        {
            get;
            set; 
        }
        /// <summary>
        /// Gets or sets the unauthorized roles.
        /// </summary>
        /// <value>The unauthorized roles.</value>
        public string UnauthorizedRoles
        {
            get;
            set;
        }

        /// <summary>
        /// Called by the MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
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
                    if (!AuthorizeSuspended && !filterContext.HttpContext.User.IsInRole("ActiveUser"))
                    {
                        //Is suspended
                        filterContext.Result = (RedirectToRouteResult)new AccountController().ShowSuspensionStatus();
                        return;
                    }
                    if(!AuthorizeEmailNotConfirmed && filterContext.HttpContext.User.IsInRole("EmailNotConfirmed"))
                    {
                        //Email hasn't been confirmed
                        filterContext.Result = (RedirectToRouteResult)new AccountController().ShowEmailAddressVerificationStatus();
                        return;
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