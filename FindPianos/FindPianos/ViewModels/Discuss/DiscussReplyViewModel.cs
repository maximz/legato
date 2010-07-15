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
        [Required(ErrorMessage="The post must be included.")]
        public DiscussPostSubmissionViewModel Post
        {
            get;
            set;
        }
        [Required(ErrorMessage="The thread ID must be included.")]
        public long ThreadID
        {
            get;
            set;
        }

        public long PostID
        {
            get;
            set;
        }

    }
}