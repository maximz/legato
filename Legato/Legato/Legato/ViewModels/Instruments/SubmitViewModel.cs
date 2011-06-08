using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.ViewModels
{
    public class SubmitViewModel
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
        public ListingSubmissionViewModel Listing
        {
            get;
            set;
        }
    }
}