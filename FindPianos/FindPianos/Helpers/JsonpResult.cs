﻿using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FindPianos.Helpers
{
    public class JsonpResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
                response.ContentType = ContentType;
            else
                response.ContentType = "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                HttpRequestBase request = context.HttpContext.Request;

                var serializer = new JavaScriptSerializer();
                if (!string.IsNullOrEmpty(request.Params["callback"]))
                    response.Write(SanitizeCallback(request.Params["callback"]) + "(" + serializer.Serialize(Data) +
                                   ");");
                else
                    response.Write(serializer.Serialize(Data));
            }
        }

        private string SanitizeCallback(string callback)
        {
            if (String.IsNullOrEmpty(callback)) return callback;
            return Regex.Replace(callback, @"[^_A-Za-z0-9]", "").Truncate(80);
        }



    }
}