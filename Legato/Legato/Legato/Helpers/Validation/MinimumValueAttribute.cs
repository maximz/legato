using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Legato.Helpers
{
    /// <summary>
    /// Requires the value of the property to be greater than or equal to a certain minimum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class MinimumValueAttribute : RangeAttribute
    {
        public MinimumValueAttribute(double minimum)
            : base(minimum, double.MaxValue)
        {
            if (ErrorMessage.IsNullOrEmpty())
            {
                ErrorMessage = "The value must be greater than or equal to " + minimum;
            }
        }
    }
}