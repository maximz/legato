using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
    [Geocodable(AddressPropertyName = "Address", LatitudePropertyName = "Lat", LongitudePropertyName = "Long", AllowNull = true, ErrorMessage = "We couldn't locate that address.")]
    public class DiscussCreateViewModel
    {
        [Required(ErrorMessage="You must specify what board you are submitting to.")]
        public long BoardID
        {
            get;
            set;
        }
        public DiscussPostSubmissionViewModel Post
        {
            get;
            set;
        }
        [Required(ErrorMessage="A thread title is required.")]
        [DisplayName("Thread title")]
        public string Title
        {
            get;
            set;
        }
        [DisplayName("Street address")]
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