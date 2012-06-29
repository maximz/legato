using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web;
using System.Collections.Specialized;
using System.IO;


namespace Legato.Helpers
{
    public class GlobalGzip : IHttpModule
    {
        private EventHandler onBeginRequest;

        public GlobalGzip()
        {
            onBeginRequest = new EventHandler(this.HandleBeginRequest);
        }

        void IHttpModule.Dispose()
        {

        }
        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += onBeginRequest;
        }


        private void HandleBeginRequest(object sender, EventArgs evargs)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Headers["Accept-encoding"] != null && (context.Request.Headers["Accept-encoding"] as string).Contains("gzip"))
            {
                context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                HttpContext.Current.Response.AppendHeader("Content-encoding", "gzip");
            }
            HttpContext.Current.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
        }
    }
}
