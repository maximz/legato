using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Legato.Helpers;

namespace Legato.ViewModels
{
    public class ContactViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Please input your name.")]
        [Display(Name = "Name", Description = "Enter your name here.")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Please input your email.")]
        [IsValidEmailAddress(ErrorMessage = "This is not a valid email address.")]
        [Display(Name = "Email Address", Description = "Enter your email address here.")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required(ErrorMessage = "Please write a message.")]
        [Display(Name = "Message", Description = "Enter your message here.")]
        public string Message
        {
            get;
            set;
        }
    }
}