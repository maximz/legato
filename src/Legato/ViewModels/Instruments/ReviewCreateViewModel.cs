﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Legato.ViewModels
{
    public class ReviewCreateViewModel
    {
        public RevisionSubmissionViewModel ReviewRevision
        {
            get;
            set;
        }
        [Required(ErrorMessage="The ListingID must be specified.")]
        public long InstrumentID
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewCreateViewModel"/> class.
        /// </summary>
        public ReviewCreateViewModel()
        {
            if(ReviewRevision == null)
            {
                ReviewRevision = new RevisionSubmissionViewModel();
            }
        }
    }
}