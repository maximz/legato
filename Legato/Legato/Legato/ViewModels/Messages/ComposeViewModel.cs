using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Legato.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Legato.ViewModels
{
    [ValidateInput(false)]
    public class ComposeViewModel
    {
        [Required]
        [AllowHtml]
        [Display(Name = "Message")]
        public string Markdown
        {
            get;
            set;
        }
        [Required]
        [StringLength(100)]
        [Display(Name="Subject")]
        [AllowHtml]
        public string Subject
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "Recipient")]
        [AllowHtml]
        public string UserName
        {
            get;
            set;
        }
    }
}