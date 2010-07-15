using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels.Discuss
{
    [Geocodable(AddressPropertyName="Address",LatitudePropertyName="Lat",LongitudePropertyName="Long",AllowNull=true,ErrorMessage="We couldn't locate that address.")]
    public class DiscussPostSubmissionViewModel
    {
        [Required(ErrorMessage="You must include some text.")]
        public string Markdown
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public decimal Lat
        {
            get;
            set;
        }

        public decimal Long
        {
            get;
            set;
        }
    }
}