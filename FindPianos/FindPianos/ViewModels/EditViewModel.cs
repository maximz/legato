using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.ViewModels
{
    public class EditViewModel
    {
        public RevisionSubmissionViewModel ReviewRevision
        {
            get;
            set;
        }
        public List<VenueHourViewModel> Hours
        {
            get;
            set;
        }
        public ReadListingViewModel Listing
        {
            get;
            set;
        }
    }
}