using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Legato.Helpers;
using DotNetOpenAuth.OpenId;
using System.Web.Mvc;

namespace Legato.ViewModels
{
    public class OpenIdRegistrationViewModel
    {
        [Required(ErrorMessage="You must specify an email address.")]
        [Display(Name="Email address",Description="Enter your email address here.")]
        [IsValidEmailAddress(ErrorMessage="This is not a valid email address.")]
        public string EmailAddress
        {
            get;
            set;
        }
        [Required(ErrorMessage = "You must enter you email address a second time.")]
        [Display(Name="Confirm email address",Description="Please enter your email address a second time here.")]
        [IsValidEmailAddress(ErrorMessage = "This is not a valid email address.")]
        [Compare("EmailAddress",ErrorMessage="You must enter the same email address in both fields.")]
        public string ConfirmEmailAddress
        {
            get;
            set;
        }
        [Display(Name="Nickname",Description="Enter your nickname for the site here.")]
        [Required(ErrorMessage="You must choose a Nickname.")]
        public string Nickname
        {
            get;
            set;
        }
        [Display(Name="Full Name",Description="Please enter your real name here (optional).")]
        public string FullName
        {
            get;
            set;
        }
        [Display(Name = "About Me", Description = "Please write something about yourself (optional).")]
        public string AboutMe
        {
            get;
            set;
        }
        [Required(ErrorMessage="An OpenID claim must be included.")]
        public string OpenIdClaim
        {
            get;
            set;
        }

    }
}