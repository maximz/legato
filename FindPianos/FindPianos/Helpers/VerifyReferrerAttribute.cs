using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindPianos.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class VerifyReferrerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Request = filterContext.HttpContext.Request;
            var referrer = Request.UrlReferrer;
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, urlHelper.Content("~"));
            if (!referrer.ToString().Contains(url))
            {
                //Referrer is from an external domain - eek!
                filterContext.Result = new RedirectResult("~/403");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}