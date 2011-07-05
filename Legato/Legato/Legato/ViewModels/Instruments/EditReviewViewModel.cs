using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Legato.ViewModels
{
    public class EditReviewViewModel
    {
        /// <summary>
        /// Gets or sets the review revision. Is first set to the latest revision, then the user changes it to create another revision.
        /// </summary>
        /// <value>The review revision.</value>
        [DisplayName("Review")]
        [Required]
        public RevisionSubmissionViewModel ReviewRevision
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditReviewViewModel"/> class.
        /// </summary>
        public EditReviewViewModel()
        {
            if(ReviewRevision == null)
            {
                ReviewRevision = new RevisionSubmissionViewModel();
            }
        }
    }
}