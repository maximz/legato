using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;

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
        /// Answers true if this String is neither null or empty.
        /// </summary>
        /// <remarks>I'm also tired of typing !String.IsNullOrEmpty(s)</remarks>
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Gets the value of the string or returns the specified default value if the string is null or empty.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetValueOrDefault(this string s, string defaultValue)
        {
            return s.HasValue() ? s : defaultValue;
        } 

        /// <summary>
        /// Returns the first non-null/non-empty parameter when this String is null/empty.
        /// </summary>
        public static string IsNullOrEmptyReturn(this string s, params string[] otherPossibleResults)
        {
            if (s.HasValue())
                return s;

            for (int i = 0; i < (otherPossibleResults ?? new string[0]).Length; i++)
            {
                if (otherPossibleResults[i].HasValue())
                    return otherPossibleResults[i];
            }

            return "";
        }

        /// <summary>
        /// returns string with any mulitple, sequential spaces replaced by a single space, 
        /// and any extra spaces trimmed from beginning and end.
        /// </summary>
        public static string RemoveExtraSpaces(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            s = Regex.Replace(s, "\u200c", " "); // see http://en.wikipedia.org/wiki/Zero-width_non-joiner
            s = Regex.Replace(s, @"\s{2,}", " ").Trim();
            return s;
        }

        /// <summary>
        /// force string to be maxlen or smaller
        /// </summary>
        public static string Truncate(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            return (s.Length > maxLength) ? s.Remove(maxLength) : s;
        }

        public static string TruncateWithEllipsis(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            if (s.Length <= maxLength) return s;

            return string.Format("{0}...", Truncate(s, maxLength - 3));
        }

        /// <summary>
        /// Produces a URL-friendly version of this String, "like-this-one".
        /// </summary>
        public static string URLFriendly(this string s)
        {
            return s.HasValue() ? HtmlUtilities.URLFriendly(s) : s;
        }

        /// <summary>
        /// returns Url Encoded string
        /// </summary>
        public static string UrlEncode(this string s)
        {
            return s.HasValue() ? HttpUtility.UrlEncode(s) : s;
        }

        /// <summary>
        /// returns Html Encoded string
        /// </summary>
        public static string HtmlEncode(this string s)
        {
            return s.HasValue() ? HttpUtility.HtmlEncode(s) : s;
        }

        /// <summary>
        /// returns true if this looks like a semi-valid email address
        /// </summary>
        public static bool IsSemiValidEmailAddress(this string s)
        {
            return s.HasValue()
                       ? Regex.IsMatch(s, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase)
                       : false;
        }

        /// <summary>
        /// Determines whether [is completely valid email address] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="AllowNull">if set to <c>true</c> [allow null].</param>
        /// <returns>
        /// 	<c>true</c> if [is completely valid email address] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompletelyValidEmailAddress(this string s, bool AllowNull)
        {
            if(s.HasValue())
            {
                MailAddress a;
                var success = false;
                try
                {
                    a = new MailAddress(s);
                    success = true;
                }
                catch
                {

                }
                finally
                {
                    a = null;
                }
                return success;
            }
            return AllowNull;
        }
        /// <summary>
        /// returns true if this looks like a semi-valid openid string; it starts with "=@+$!(" or contains a period.
        /// </summary>
        public static bool IsOpenId(this string s)
        {
            return s.HasValue() ? Regex.IsMatch(s, @"^[=@+$!(]|.*?\.") : false;
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