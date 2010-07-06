using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FindPianos.Helpers;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class ListingSubmissionViewModel
    {
        [Required(ErrorMessage="A street address is required.")]
        [Geocodable]
        [DisplayName("Street address")]
        public string StreetAddress
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
        /// <summary>
        /// Gets or sets the instrument name (e.g., piano or drums).
        /// </summary>
        /// <value>The name of the instrument.</value>
        [Required(ErrorMessage="You must include an instrument name.")]
        [InstrumentNameVerification]
        [DisplayName("Instrument")]
        public string InstrumentName
        {
            get;
            set;
        }
    }
}