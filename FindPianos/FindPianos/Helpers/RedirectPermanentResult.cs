﻿using System;
using System.Web.Mvc;

namespace FindPianos.Helpers
{
    public class RedirectPermanentResult : ActionResult
    {
        public RedirectPermanentResult(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentException("url should not be empty");
            }

            Url = url;
        }


        public string Url { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (context.IsChildAction)
            {
                throw new InvalidOperationException("You can not redirect in child actions");
            }

            string destinationUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);
            context.Controller.TempData.Keep();
            context.HttpContext.Response.RedirectPermanent(destinationUrl);
        }
    }
}