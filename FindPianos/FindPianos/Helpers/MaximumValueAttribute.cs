using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaximumValueAttribute : RangeAttribute
    {
        public MaximumValueAttribute(double maximum)
            : base(double.MinValue, maximum)
        {
        }
    }
}