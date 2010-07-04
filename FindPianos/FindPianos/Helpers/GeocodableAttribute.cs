using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    public class GeocodableAttribute : ValidationAttribute
    {
        public GeocodableAttribute() : base()
        {
            ErrorMessage = "We weren't able to find that location.";
        }
        public override bool IsValid(object value)
        {
            if (value == null) //we don't care if it's required or not.
            {
                return true;
            }
            var address = (string)value;
            //TODO
            throw new NotImplementedException();
            //if (price < MinPrice)
            //{
            //    return false;
            //}
            //double cents = price - Math.Truncate(price);
            //if (cents < 0.99 || cents >= 0.995)
            //{
            //    return false;
            //}

            return true;
        }
    }
}