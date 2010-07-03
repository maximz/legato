using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Answers true if this String is either null or empty.
        /// </summary>
        /// <remarks>I'm so tired of typing String.IsNullOrEmpty(s)</remarks>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// force string to be maxlen or smaller
        /// </summary>
        public static string Truncate(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            return (s.Length > maxLength) ? s.Remove(maxLength) : s;
        }

        public static void RedirectPermanent(this HttpResponseBase response, string url)
        {
            response.Clear();
            response.Status = "301 Moved Permanently";
            response.AddHeader("Location", url);
            response.End();
        }

        public static bool Flip(this bool b)
        {
            if(b)
            {
                return !b;
            }
            else
            {
                return b;
            }
        }
    }
}