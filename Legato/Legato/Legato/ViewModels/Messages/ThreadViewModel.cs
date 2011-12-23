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
    public class ThreadViewModel
    {
        public Conversation Conversation
        {
            get;
            set;
        }
        public aspnet_User OtherUser
        {
            get;
            set;
        }
        public List<Message> Messages
        {
            get;
            set;
        }

        // Reply

        [Required]
        [AllowHtml]
        [Display(Name = "Message")]
        public string Markdown
        {
            get;
            set;
        }
        [Required]
        public int ThreadID
        {
            get;
            set;
        }
    }
}