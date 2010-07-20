using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
    public class StoreReplyViewModel
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
        [Required(ErrorMessage = "The ListingID must be specified.")]
        public long ListingID
        {
            get;
            set;
        }
    }
}