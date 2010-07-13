using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FindPianos.Helpers;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    [Geocodable(AddressPropertyName="StreetAddress",LatitudePropertyName="Lat",LongitudePropertyName="Long", ErrorMessage="We couldn't locate this address. Please verify that it is correct.")]
    public class StoreListingSubmissionViewModel
    {
        [Required(ErrorMessage="A street address is required.")]
        [DisplayName("Street address")]
        public string StreetAddress
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
        [Required(ErrorMessage="You must specify a store name.")]
        [DisplayName("Store name")]
        public string Name
        {
            get;
            set;
        }
        [Required(ErrorMessage="You must specify a store description.")]
        [DisplayName("Store description")]
        public string Description
        {
            get;
            set;
        }
        [Required(ErrorMessage="You must specify whether you are affiliated with the store.")]
        [DisplayName("Are you affiliated with the store?")]
        public bool IsSubmitterAffiliatedWithStore
        {
            get;
            set;
        }
    }
}