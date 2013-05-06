using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Legato.ViewModels
{
    public class SubmitViewModel
    {
        [DisplayName("Have you played on this instrument?")]
        public bool HasPlayedOnInstrument
        {
            get;
            set;
        }

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
        /// If we fail out of first submit, go back to selected instrument class view.
        /// </summary>
        public bool InProcess
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

            InProcess = false;
        }
    }
}