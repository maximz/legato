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
    }
}