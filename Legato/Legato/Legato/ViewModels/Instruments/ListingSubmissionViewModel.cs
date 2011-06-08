using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Legato.Helpers;
using System.ComponentModel;

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
        
    }
}