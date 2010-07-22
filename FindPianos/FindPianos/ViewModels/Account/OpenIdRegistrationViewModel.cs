using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FindPianos.Helpers;
using DotNetOpenAuth.OpenId;

namespace FindPianos.ViewModels
{
    [CompareProperties(ComparisonProperty1="EmailAddress", ComparisonProperty2="ConfirmEmailAddress",LessThanAllowed=false,EqualToAllowed=true,GreaterThanAllowed=false,AllowNullValues=true,ErrorMessage="You must enter the same email address in both fields.")]
    public class OpenIdRegistrationViewModel
    {
        [Required(ErrorMessage="You must specify an email address.")]
        [DisplayName("Email address")]
        [IsValidEmailAddress(ErrorMessage="This is not a valid email address.")]
        public string EmailAddress
        {
            get;
            set;
        }
        [Required(ErrorMessage = "You must enter you email address a second time.")]
        [DisplayName("Confirm email address")]
        [IsValidEmailAddress(ErrorMessage = "This is not a valid email address.")]
        public string ConfirmEmailAddress
        {
            get;
            set;
        }
        [DisplayName("Nickname")]
        [Required(ErrorMessage="You must choose a Nickname.")]
        public string Username
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
        [Required(ErrorMessage="The CAPTCHA must be submitted.")]
        public bool captchaValid
        {
            get;
            set;
        }
    }
}