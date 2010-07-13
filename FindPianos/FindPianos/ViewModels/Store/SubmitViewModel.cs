using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindPianos.ViewModels
{
    public class StoreSubmitViewModel
    {
        public StoreRevisionSubmissionViewModel ReviewRevision
        {
            get;
            set;
        }
        public List<StoreHourViewModel> Hours
        {
            get;
            set;
        }
        public StoreListingSubmissionViewModel Listing
        {
            get;
            set;
        }
    }
}