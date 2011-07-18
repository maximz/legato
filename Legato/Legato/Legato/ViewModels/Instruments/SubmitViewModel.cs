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

            if(Listing==null)
            {
                Listing = new ListingSubmissionViewModel();
            }
        }
    }
}