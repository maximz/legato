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

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitViewModel"/> class.
        /// </summary>
        public SubmitViewModel()
        {
            if(ReviewRevision==null)
            {
                ReviewRevision = new RevisionSubmissionViewModel();
            }
            if (Hours == null)
            {
                Hours = new List<VenueHourViewModel>();
            }
            if(Listing==null)
            {
                Listing = new ListingSubmissionViewModel();
                Listing.Equipment = new EquipmentViewModel();
            }
        }
    }
}