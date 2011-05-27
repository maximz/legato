using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Legato.Helpers;
using DotNetOpenAuth.OpenId;

namespace Legato.ViewModels
{
    //[CompareProperties(ComparisonProperty1="EmailAddress", ComparisonProperty2="ConfirmEmailAddress",LessThanAllowed=false,EqualToAllowed=true,GreaterThanAllowed=false,AllowNullValues=true,ErrorMessage="You must enter the same email address in both fields.")]
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
        //[Required(ErrorMessage = "You must enter you email address a second time.")]
        //[DisplayName("Confirm email address")]
        //[IsValidEmailAddress(ErrorMessage = "This is not a valid email address.")]
        //public string ConfirmEmailAddress
        //{
        //    get;
        //    set;
        //}
        [DisplayName("Nickname")]
        [Required(ErrorMessage="You must choose a Nickname.")]
        public string Nickname
        {
            get;
            set;
        }
        [DisplayName("Full name")]
        [Required(ErrorMessage = "You must enter your full name.")]
        public string FullName
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

        //DU-only properties:
        [DisplayName("Your affiliation to The Bishop's School")]
        [Range(0,2)]
        [Required(ErrorMessage="You must specify your affiliation to The Bishop's School.")]
        public int BishopsAffiliation
        {
            get;
            set;
        }
        [DisplayName("Class Year")]
        public int? ClassYear
        {
            get;
            set;
        }
        [DisplayName("Advisor")]
        public string Advisor
        {
            get;
            set;
        }
        [DisplayName("Department")]
        public string Department
        {
            get;
            set;
        }
    }
}