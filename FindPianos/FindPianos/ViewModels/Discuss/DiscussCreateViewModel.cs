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
        public string BoardName
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance can set location.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can set location; otherwise, <c>false</c>.
        /// </value>
        public bool CanSetLocation
        {
            get;
            set;
        }
    }
}