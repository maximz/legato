using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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
            System.Collections.Specialized.NameValueCollection SimpleSettings = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("GoDaddyUrlRewrite/SimpleSettings");
            if (SimpleSettings != null && SimpleSettings.Count > 1)
            {
                //http://www.15seconds.com/issue/030522.htm
                foreach (var key in SimpleSettings.Keys)
                {
                    var subdirectory = SimpleSettings(
                    if (context.Request.Path.Contains("/" + subdirectory))
                    {
                        HttpContext.Current.RewritePath(context.Request.Path.Replace("/" + subdirectory, ""));
                    }
                }
            }
        }
    }
}
