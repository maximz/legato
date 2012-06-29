using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Legato.Helpers
{
    /// <summary>
    /// Checks that the field/property is a valid email address.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple=false)]
    public class IsValidEmailAddressAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsValidEmailAddressAttribute"/> class.
        /// </summary>
        public IsValidEmailAddressAttribute() : base()
        {
            if(ErrorMessage.IsNullOrEmpty())
                ErrorMessage = "Please enter a valid email address.";
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
            if (value == null) //we don't care if it's required or not.
            {
                return true;
            }
            var address = (string)value;
            try
            {
                var addressMail = new MailAddress(address);
                addressMail = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}