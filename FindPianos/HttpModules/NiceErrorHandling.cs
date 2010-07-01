using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HttpModules
{
    public class BetterCustomErrorsModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.Error += new EventHandler(Context_Error);
        }
        void Context_Error(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            HttpException httpException = context.Error as HttpException;

            if (httpException != null)
                context.Response.StatusCode = httpException.GetHttpCode();
            else
                context.Response.StatusCode = 500;

            context.ClearError();
            context.Server.Transfer("~/Error");
        }
    }
}
