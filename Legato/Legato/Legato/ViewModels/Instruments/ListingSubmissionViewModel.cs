using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Legato.Helpers;
using System.ComponentModel;
using System.Web.Mvc;

namespace Legato.ViewModels
{
    [Geocodable(AddressPropertyName="StreetAddress",LatitudePropertyName="Lat",LongitudePropertyName="Long")]
    public class ListingSubmissionViewModel
    {
        [Required(ErrorMessage="A street address is required.")]
        [DisplayName("Street address")]
        public string StreetAddress
        {
            get;
            set;
        }
        [Required(ErrorMessage = "A venue name is required.")]
        [DisplayName("Venue name")]
        public string VenueName
        {
            get;
            set;
        }

        public decimal? Price
        {
            get;
            set;
        }

        public string TimeSpanOfPrice
        {
            get;
            set;
        }

        [Required]
        public decimal Lat
        {
            get;
            set;
        }
        [Required]
        public decimal Long
        {
            get;
            set;
        }
        [Required(ErrorMessage="Equipment must be present.")]
        [DisplayName("Equipment")]
        public EquipmentViewModel Equipment
        {
            get;
            set;
        }

        [Required]
        [AllowHtml]
        public string GeneralInfoMarkdown
        {
            get;
            set;
        }

        public int? InstrumentID
        {
            get;
            set;
        }
        
    }
}