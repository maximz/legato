using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class ReadListingViewModel
    {
        [Required(ErrorMessage="The listing is required.")]
        public Listing Listing
        {
            get;
            set;
        }
        //optional - there might not be any reviews yet
        public List<Review> Reviews
        {
            get;
            set;
        }
    }
}