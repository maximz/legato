using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace HttpModules
{
    public class GoDaddyUrlRewrite : IHttpModule
    {
        private EventHandler onBeginRequest;

        public GoDaddyUrlRewrite()
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
            var subdir = ConfigurationManager.AppSettings["GoDaddyUrlRewrite"];
            if (!string.IsNullOrEmpty(subdir))
            {
                if(subdir[0]=='/')
                {
                    subdir = subdir.Remove(0, 1);
                }
                if (context.Request.Path.Contains("/" + subdir))
                {
                    HttpContext.Current.RewritePath(context.Request.Path.Replace("/" + subdir, ""));
                }
            }
        }
    }
}
