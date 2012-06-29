using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.ViewModels
{
    public class EditListingViewModel
    {

        public ListingSubmissionViewModel Listing
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditListingViewModel"/> class.
        /// </summary>
        public EditListingViewModel()
        {
            if(Listing==null)
            {
                Listing = new ListingSubmissionViewModel();
            }
        }
    }
}