using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindPianos.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FindPianos.ViewModels
{

    public class DiscussPostSubmissionViewModel
    {
        [Required(ErrorMessage="You must include some text.")]
        [DisplayName("Text")]
        public string Markdown
        {
            get;
            set;
        }
    }
}