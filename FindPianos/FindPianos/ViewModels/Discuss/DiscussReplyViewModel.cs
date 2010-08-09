using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class DiscussReplyViewModel
    {
        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        /// <value>The post.</value>
        [Required(ErrorMessage="The post must be included.")]
        public DiscussPostSubmissionViewModel Post
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the thread ID.
        /// </summary>
        /// <value>The thread ID.</value>
        [Required(ErrorMessage="The thread ID must be included.")]
        public long ThreadID
        {
            get;
            set;
        }
        /// <summary>
        /// Displayed on the View
        /// </summary>
        public string ThreadName
        {
            get;
            set;
        }
    }
}