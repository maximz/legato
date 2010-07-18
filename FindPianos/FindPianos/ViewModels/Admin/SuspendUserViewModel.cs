using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    public class SuspendUserViewModel
    {
        [DisplayName("User ID")]
        [Required(ErrorMessage="You must specify a user ID.")]
        public Guid UserID
        {
            get;
            set;
        }
        [DisplayName("Reinstatement Date")]
        [CompareValue(ComparisonValue="DateTime.Now",EqualToAllowed=false,GreaterThanAllowed=true,LessThanAllowed=false,AllowNullValues=true,ErrorMessage="The reinstatement date must be in the future.")]
        public DateTime ReinstateDate
        { 
            get;
            set;
        }
        [DisplayName("Reason")]
        [Required(ErrorMessage="You must specify a reason.")]
        public string Reason
        {
            get;
            set;
        }
    }
}