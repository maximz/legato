using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RiaLibrary.Web;
using MvcMiniProfiler;
using System.IO;
using Legato.Models;
using MvcSiteMapProvider.Web;

namespace Legato
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("admin/elmah.axd");
            routes.IgnoreRoute("admin/{resource}.axd/{*pathInfo}");

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

            XmlSiteMapController.RegisterRoutes(RouteTable.Routes); // register sitemap.xml

            RegisterRoutes(RouteTable.Routes);

            InitProfilerSettings();

            // Route Debugger: to use, uncomment this line:
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Current.Context.Response.BufferOutput = true;

            // MvcMiniProfiler stuff:

            MiniProfiler profiler = null;

            // might want to decide here (or maybe inside the action) whether you want
            // to profile this request - for example, using an "IsSystemAdmin" flag against
            // the user, or similar; this could also all be done in action filters, but this
            // is simple and practical; just return null for most users. For our test, we'll
            // profile only for local requests (seems reasonable)
            //if (Request.IsLocal)
            //{
            //    profiler = MvcMiniProfiler.MiniProfiler.Start();
            //}

#if DEBUG
            profiler = MvcMiniProfiler.MiniProfiler.Start();
#endif

            using (profiler.Step("Application_BeginRequest"))
            {
                // you can start profiling your code immediately
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Current.DisposeDB();
            MvcMiniProfiler.MiniProfiler.Stop();
        }

        protected void Application_Exit()
        {

        }

        /// <summary>
        /// Customize aspects of the MiniProfiler.
        /// </summary>
        private void InitProfilerSettings()
        {
            // a powerful feature of the MiniProfiler is the ability to share links to results with other developers.
            // by default, however, long-term result caching is done in HttpRuntime.Cache, which is very volatile.
            // 
            // let's rig up methods to binary serialize our profiler results to a database, so they survive app restarts.
            // (note: this method is more to test that the MiniProfiler can be serialized by protobuf-net - a real database storage
            // scheme would put each property into its own column, so they could be queried independently of the MiniProfiler's UI)

            // a setter will take the current profiler and should save it somewhere by its guid Id
            MiniProfiler.Settings.LongTermCacheSetter = (profiler) =>
            {
                using (var ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(ms, profiler);

                    var conn = Current.DB;
                        // we use the insert to ignore syntax here, because MiniProfiler will
                        var toAdd = new MiniProfilerResult() { Id = profiler.Id, Results = ms.GetBuffer() };    
                        conn.MiniProfilerResults.InsertOnSubmit(toAdd);
                        conn.SubmitChanges();
                }
            };

            // the getter will be passed a guid and should return the saved MiniProfiler
            MiniProfiler.Settings.LongTermCacheGetter = (id) =>
            {
                byte[] results;
                var conn = Current.DB;
                dynamic buffer = conn.MiniProfilerResults.Where(p => p.Id==id).SingleOrDefault();
                if (buffer == null)
                {
                    return null;
                }
                results = buffer.Results as byte[];

                if (results == null || results.Length == 0)
                    return null;

                using (var ms = new MemoryStream(results))
                {
                    return ProtoBuf.Serializer.Deserialize<MiniProfiler>(ms);
                }
            };

        }

    }
}