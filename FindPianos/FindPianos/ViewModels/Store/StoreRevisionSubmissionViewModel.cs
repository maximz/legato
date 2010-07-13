using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    public class StoreRevisionSubmissionViewModel
    {
        //Not required because this is also used for Submit, where we don't have a ReviewId yet
        public int ReviewId
        {
            get;
            set;
        }
        [Required(ErrorMessage="Overall rating is required.")]
        [Range(1,5)]
        [DisplayName("Overall Rating")]
        public int RatingOverall
        {
            get;
            set;
        }
        [Range(0, 5)]
        [DisplayName("Service")]
        public int RatingService
        {
            get;
            set;
        }
        [Range(0, 5)]
        [DisplayName("Product Quality")]
        public int RatingProductQuality
        {
            get;
            set;
        }
        [Range(0, 5)]
        [DisplayName("Environment")]
        public int RatingEnvironment
        {
            get;
            set;
        }
        [DisplayName("Message")]
        public string Message
        {
            get;
            set;
        }
        [Required(ErrorMessage="The date you last visited this store is required.")]
        [DisplayName("Date of Last Visit")]
        [CompareValue(ComparisonValue=DateTime.Now,LessThanAllowed=true,EqualToAllowed=true,GreaterThanAllowed=false,AllowNullValues=true,ErrorMessage="Date of last visit must be in the past.")]
        public DateTime DateOfLastVisit
        {
            get;
            set;
        }
        [Required(ErrorMessage = "The date you last purchased from this store is required.")]
        [DisplayName("Date of Last Purchase")]
        [CompareValue(ComparisonValue = DateTime.Now, LessThanAllowed = true, EqualToAllowed = true, GreaterThanAllowed = false, AllowNullValues = true, ErrorMessage = "Date of last purchase must be in the past.")]
        public DateTime DateOfLastPurchase
        {
            get;
            set;
        }
    }
}