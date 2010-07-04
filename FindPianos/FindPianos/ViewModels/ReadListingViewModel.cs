using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Models;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
    public class ReadListingViewModel
    {
        [Required(ErrorMessage="The listing is required.")]
        public PianoListing Listing
        {
            get;
            set;
        }
        //optional - there might not be any reviews yet
        public List<PianoReview> Reviews
        {
            get;
            set;
        }
    }
}