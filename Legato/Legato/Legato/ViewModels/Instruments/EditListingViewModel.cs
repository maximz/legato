using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.ViewModels
{
    public class EditListingViewModel
    {
        public List<VenueHourViewModel> Hours
        {
            get;
            set;
        }
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
            if (Hours == null)
            {
                Hours = new List<VenueHourViewModel>();
            }
            if(Listing==null)
            {
                Listing = new ListingSubmissionViewModel();
            }
        }
    }
}