using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    [Geocodable(AddressPropertyName = "Address", LatitudePropertyName = "Lat", LongitudePropertyName = "Long", AllowNull = true, ErrorMessage = "We couldn't locate that address.")]
    public class DiscussEditViewModel
    {
        public bool CanChangeLocation
        {
            get;
            set;
        }
        [Required(ErrorMessage = "You must specify what board you are submitting to.")]
        public long PostID
        {
            get;
            set;
        }
        public DiscussPostSubmissionViewModel Post
        {
            get;
            set;
        }
        [DisplayName("Street address")]
        public string? Address
        {
            get;
            set;
        }
        public decimal? Lat
        {
            get;
            set;
        }
        public decimal? Long
        {
            get;
            set;
        }
    }
}