using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Legato.Models;
using System.Text.RegularExpressions;
using Legato.Helpers;
using System.Collections.Specialized;
using System.Net;

namespace Legato.Controllers
{
    public class CustomControllerBase : Controller, IController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            //ValidateRequest = false; // allow html/sql in form values - remember to validate!

            base.Initialize(requestContext);

            //if (WhiteListEnabled && !(this is AccountController || this is CachedContentServeController) && !User.Identity.IsAuthenticated)
            if (WhiteListEnabled && !(this is AccountController) && !User.Identity.IsAuthenticated)
            {
                requestContext.HttpContext.Response.Redirect("~/Account/Login");
            }
        }

        static bool? whiteListEnabled = true;
        static public bool WhiteListEnabled
        {
            get
            {
                if (whiteListEnabled == null)
                {
                    whiteListEnabled = Current.DB.OpenIDWhiteLists.Count(w=>w.IsEnabled) > 0;
                }
                return whiteListEnabled.Value;
            }
        }

        #region From SEDE
        
        /// <summary>
        /// called when the url doesn't match any of our known routes
        /// </summary>
        protected override void HandleUnknownAction(string actionName)
        {
            ErrorController e = new ErrorController();
            e.NotFound().ExecuteResult(ControllerContext);
        }

        private static readonly Regex _botUserAgent =
            new Regex(@"googlebot/\d|msnbot/\d|slurp/\d|jeeves/teoma|ia_archiver|ccbot/\d|yandex/\d|twiceler-\d",
                      RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// returns true if the current request is from a search engine, based on the User-Agent header
        /// </summary>
        public bool IsSearchEngine()
        {
            if (Request.UserAgent.IsNullOrEmpty()) return false;
            return _botUserAgent.IsMatch(Request.UserAgent);
        }


        /// <summary>
        /// known good bot DNS lookups:  
        ///   66.249.68.73     crawl-66-249-68-73.googlebot.com  
        ///   66.235.124.58    crawler5107.ask.com  
        ///   65.55.104.157    msnbot-65-55-104-157.search.msn.com 
        /// </summary>
        private static readonly Regex _botDns = new Regex(@"(googlebot\.com|ask\.com|msn\.com)$",
                                                          RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture |
                                                          RegexOptions.Compiled);

        /// <summary>
        /// returns true if the current request is from a search engine, based on the User-Agent header *AND* a reverse DNS check
        /// </summary>
        public bool IsSearchEngineDns()
        {
            if (!IsSearchEngine()) return false;
            string s = GetHostName();
            return _botDns.IsMatch(s);
        }

        /// <summary>
        /// perform a DNS lookup on the current IP address with a 2 second timeout
        /// </summary>
        /// <returns></returns>
        protected string GetHostName()
        {
            return GetHostName(GetRemoteIP(), 2000);
        }

        /// <summary>
        /// perform a DNS lookup on the provided IP address, with a timeout specified in milliseconds
        /// </summary>
        protected string GetHostName(string ipAddress, int timeout)
        {
            Func<string, string> fetcher = ip => Dns.GetHostEntry(ip).HostName;
            try
            {
                IAsyncResult result = fetcher.BeginInvoke(ipAddress, null, null);
                return result.AsyncWaitHandle.WaitOne(timeout, false) ? fetcher.EndInvoke(result) : "Timeout";
            }
            catch (Exception ex)
            {
                return ex.GetType().Name;
            }
        }

        /// <summary>
        /// When a client IP can't be determined
        /// </summary>
        public const string UnknownIP = "0.0.0.0";

        private static readonly Regex _ipAddress = new Regex(@"\b([0-9]{1,3}\.){3}[0-9]{1,3}$",
                                                             RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// returns true if this is a private network IP  
        /// http://en.wikipedia.org/wiki/Private_network
        /// </summary>
        private static bool IsPrivateIP(string s)
        {
            return (s.StartsWith("192.168.") || s.StartsWith("10.") || s.StartsWith("127.0.0."));
        }

        /// <summary>
        /// retrieves the IP address of the current request -- handles proxies and private networks
        /// </summary>
        public static string GetRemoteIP(NameValueCollection ServerVariables)
        {
            string ip = ServerVariables["REMOTE_ADDR"]; // could be a proxy -- beware
            string ipForwarded = ServerVariables["HTTP_X_FORWARDED_FOR"];

            // check if we were forwarded from a proxy
            if (ipForwarded.HasValue())
            {
                ipForwarded = _ipAddress.Match(ipForwarded).Value;
                if (ipForwarded.HasValue() && !IsPrivateIP(ipForwarded))
                    ip = ipForwarded;
            }

            return ip.HasValue() ? ip : UnknownIP;
        }

        /// <summary>
        /// Answers the current request's user's ip address; checks for any forwarding proxy
        /// </summary>
        public string GetRemoteIP()
        {
            return GetRemoteIP(Request.ServerVariables);
        }

        #endregion


    }
}
