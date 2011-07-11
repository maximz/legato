using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;


namespace Legato.Helpers
{
    public class IPBlacklist : IHttpModule
    {
        private EventHandler onBeginRequest;

        public IPBlacklist()
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

        private const string BLOCKEDIPSKEY = "blockedips";
        private const string BLOCKEDIPSFILE = "blockedips.config";

        public static StringDictionary GetBlockedIPs(HttpContext context)
        {
            StringDictionary ips = (StringDictionary) context.Cache[BLOCKEDIPSKEY];
            if(ips==null)
            {
                var filePath = GetBlockedIPsFilePathFromCurrentContext(context);
                ips = GetBlockedIPs(filePath);
                context.Cache.Insert(BLOCKEDIPSKEY, ips, new CacheDependency(filePath));
            }
            return ips;
        }

        private static string BlockedIPFileName = null;
        private static object blockedIPFileNameObject = new object();
        public static string GetBlockedIPsFilePathFromCurrentContext(HttpContext context)
        {
            if (BlockedIPFileName != null)
                return BlockedIPFileName;
            lock(blockedIPFileNameObject)
            {
                if(BlockedIPFileName == null)
                {
                    BlockedIPFileName = HostingEnvironment.MapPath("~/" + BLOCKEDIPSFILE);
                }
            }
            return BlockedIPFileName;
        }

        public static StringDictionary GetBlockedIPs(string configPath)
        {
            StringDictionary retval = new StringDictionary();
            try
            {
                using (StreamReader sr = new StreamReader(configPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length != 0)
                        {
                            retval.Add(line, null);
                        }
                    }
                }
            }
            catch
            {
                //Usually this will be a file not found exception - swallow!
            }
            return retval;
        }

        private void HandleBeginRequest(object sender, EventArgs evargs)
        {
            HttpApplication app = sender as HttpApplication;

            if(app!=null)
            {
                string IPAddr = app.Context.Request.ServerVariables["REMOTE_ADDR"];
                if(string.IsNullOrEmpty(IPAddr))
                {
                    return;
                }

                StringDictionary badIPs = GetBlockedIPs(app.Context);
                if(badIPs != null && badIPs.ContainsKey(IPAddr))
                {
                    app.Context.Response.StatusCode = 403;
                    app.Context.Response.SuppressContent = true;
                    app.Context.Response.End();
                    return;
                }
            }
        }
    }
}
