using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class EditViewModel
    {
        /// <summary>
        /// Gets or sets the review revision. Is first set to the latest revision, then the user changes it to create another revision.
        /// </summary>
        /// <value>The review revision.</value>
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
        /// <summary>
        /// Gets or sets the listing. Do not change it from the View!
        /// </summary>
        /// <value>The listing.</value>
        [DisplayName("Listing")]
        public ReadListingViewModel Listing
        {
            get;
            set;
        }
    }
}