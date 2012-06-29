using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Legato.Helpers
{
    /// <summary>
    /// Requires the value of the property to be less than or equal to a certain maximum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple=false)]
    public class MaximumValueAttribute : RangeAttribute
    {
        public MaximumValueAttribute(double maximum)
            : base(double.MinValue, maximum)
        {
            if (ErrorMessage.IsNullOrEmpty())
            {
                ErrorMessage = "The value must be less than or equal to " + maximum;
            }
        }
    }
}