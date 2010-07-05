using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class EditViewModel
    {
        [DisplayName("Review")]
        public RevisionSubmissionViewModel ReviewRevision
        {
            get;
            set;
        }
        [DisplayName("Hours of availability")]
        public List<VenueHourViewModel> Hours
        {
            get;
            set;
        }
        [DisplayName("Listing")]
        public ReadListingViewModel Listing
        {
            get;
            set;
        }
    }
}