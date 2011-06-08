using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Legato.Helpers;

namespace Legato.ViewModels
{
    public class RevisionSubmissionViewModel
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
        [DisplayName("Tuning")]
        public int RatingTuning
        {
            get;
            set;
        }
        [Range(0, 5)]
        [DisplayName("Tone Quality")]
        public int RatingToneQuality
        {
            get;
            set;
        }
        [Range(0, 5)]
        [DisplayName("Playing Capability")]
        public int RatingPlayingCapability
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
        [Required(ErrorMessage="Price is required.")]
        [DisplayName("Price Per Hour (in USD)")]
        [MinimumValue(0)]
        public double PricePerHour
        {
            get;
            set;
        }
        [DisplayName("Venue Name")]
        public string VenueName
        {
            get;
            set;
        }
        [Required(ErrorMessage="The date you last used this instrument is required.")]
        [DisplayName("Date of Last Usage")]
        [CompareValue(ComparisonValue="DateTime.Now",LessThanAllowed=true,EqualToAllowed=true,GreaterThanAllowed=false,AllowNullValues=true,ErrorMessage="Date of last usage must be in the past.")]
        public DateTime DateOfLastUsage
        {
            get;
            set;
        }
    }
}