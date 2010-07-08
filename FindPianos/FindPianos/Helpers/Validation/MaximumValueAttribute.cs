using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    /// <summary>
    /// Requires the value of the property to be less than or equal to a certain maximum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaximumValueAttribute : RangeAttribute
    {
        public MaximumValueAttribute(double maximum)
            : base(double.MinValue, maximum)
        {
        }
    }
}