using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RiaLibrary.Web;
//using MvcMiniProfiler;
namespace Legato
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoutes(); // Register Attribute Based Routes which the current assembly contains (RiaLibrary.Web = http://maproutes.codeplex.com)

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Current.Context.Response.BufferOutput = true;
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Current.DisposeDB();
        }

        protected void Application_Exit()
        {

        }

    }
}