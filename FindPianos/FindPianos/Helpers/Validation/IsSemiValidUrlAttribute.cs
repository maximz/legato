using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    /// <summary>
    /// Checks that the field/property is a valid email address.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class IsSemiValidURLAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsSemiValidURLAttribute"/> class.
        /// </summary>
        public IsSemiValidURLAttribute()
            : base()
        {
            if (ErrorMessage.IsNullOrEmpty())
                ErrorMessage = "Please enter a valid URL.";
        }
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            try
            {
                var thirdparty = new Regex(@"([\d\w-.]+?\.(a[cdefgilmnoqrstuwz]|b[abdefghijmnorstvwyz]|c[acdfghiklmnoruvxyz]|d[ejkmnoz]|e[ceghrst]|f[ijkmnor]|g[abdefghilmnpqrstuwy]|h[kmnrtu]|i[delmnoqrst]|j[emop]|k[eghimnprwyz]|l[abcikrstuvy]|m[acdghklmnopqrstuvwxyz]|n[acefgilopruz]|om|p[aefghklmnrstwy]|qa|r[eouw]|s[abcdeghijklmnortuvyz]|t[cdfghjkmnoprtvwz]|u[augkmsyz]|v[aceginu]|w[fs]|y[etu]|z[amw]|aero|arpa|biz|com|coop|edu|info|int|gov|mil|museum|name|net|org|pro)(\b|\W(?<!&|=)(?!\.\s|\.{3}).*?))(\s|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
                var microsoft = new Regex(@"(?<protocol>http|ftp|https|file)://(?<domain>[\w\.]+)(?<path>/.*)?", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

                return (thirdparty.IsMatch(value as string) || microsoft.IsMatch(value as string));
            }
            catch
            {
                return false;
            }

        }
    }
}