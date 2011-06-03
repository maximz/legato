using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
    public class ReplyViewModel
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
        [Required(ErrorMessage="The ListingID must be specified.")]
        public long ListingID
        {
            get;
            set;
        }
    }
}