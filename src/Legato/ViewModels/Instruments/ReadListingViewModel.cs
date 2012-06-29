using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Legato.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Legato.ViewModels
{
    public class ReadListingViewModel
    {
        [Required(ErrorMessage="The instrument is required.")]
        public Instrument Instrument
        {
            get;
            set;
        }

        //optional - there might not be any reviews yet
        public List<InstrumentReview> Reviews
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered to individual review. if it is, the view displays a message saying that only one review is being shown, linking to the entire listing thread. Optional (if not given, assume false).
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered to individual review; otherwise, <c>false</c>.
        /// </value>
        public bool? IsFilteredToIndividualReview
        {
            get;
            set;
        }
    }
}