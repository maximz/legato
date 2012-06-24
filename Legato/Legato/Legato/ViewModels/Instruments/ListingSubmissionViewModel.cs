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
    [Geocodable(AddressPropertyName = "StreetAddress", LatitudePropertyName = "Lat", LongitudePropertyName = "Long", FilteredAddressPropertyName = "FilteredAddress", FilterToZipCodeOnlyProperty = "SelectedPrivacy")]
    public class ListingSubmissionViewModel
    {
        [Required(ErrorMessage="A street address is required.")]
        [DisplayName("Street address")]
        public string StreetAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected address privacy setting.
        /// </summary>
        /// <value>The selected address privacy setting.</value>
        [Required(ErrorMessage = "You must select a privacy setting.")]
        [DisplayName("Address privacy")]
        public int SelectedPrivacy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets possible address privacy settings; e.g., full address, neighborhood (zip code) only, city only.
        /// </summary>
        /// <value>The styles.</value>
        [DisplayName("Possible address privacy settings")]
        public IEnumerable<SelectListItem> PrivacySettings
        {
            get;
            set;
        }

        public string FilteredAddress
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

        public double? Price
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
        public double Lat
        {
            get;
            set;
        }
        [Required]
        public double Long
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

        [DisplayName("Description")]
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

        public ListingSubmissionViewModel()
        {
            if(Equipment==null)
            {
                Equipment = new EquipmentViewModel();
            }

            if (PrivacySettings == null)
            {
                PrivacySettings = new SelectList(AddressPrivacySettings.Settings, AddressPrivacySettings.ValueField, AddressPrivacySettings.TextField);
            }
        }
        
    }
}