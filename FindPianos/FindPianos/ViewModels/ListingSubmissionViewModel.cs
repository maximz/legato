using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    public class ListingSubmissionViewModel
    {
        [Required(ErrorMessage="A street address is required.")]
        [Geocodable]
        public string StreetAddress
        {
            get;
            set;
        }
        [Required(ErrorMessage="Equipment must be present.")]
        public EquipmentViewModel Equipment
        {
            get;
            set;
        }
        [Required(ErrorMessage="You must include an instrument name.")]
        [InstrumentNameVerification]
        public string InstrumentName
        {
            get;
            set;
        }
    }
}