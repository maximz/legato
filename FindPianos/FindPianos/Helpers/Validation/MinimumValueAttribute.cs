using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    /// <summary>
    /// Requires the value of the property to be greater than or equal to a certain minimum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumValueAttribute : RangeAttribute
    {
        public MinimumValueAttribute(double minimum)
            : base(minimum, double.MaxValue)
        {
        }
    }
}