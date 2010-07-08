using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumValueAttribute : RangeAttribute
    {
        public MinimumValueAttribute(double minimum)
            : base(minimum, double.MaxValue)
        {
        }
    }
}