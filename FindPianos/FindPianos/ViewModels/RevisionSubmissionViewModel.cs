using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
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
        public int RatingOverall
        {
            get;
            set;
        }
        [Range(0, 5)]
        public int RatingTuning
        {
            get;
            set;
        }
        [Range(0, 5)]
        public int RatingToneQuality
        {
            get;
            set;
        }
        [Range(0, 5)]
        public int RatingPlayingCapability
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        [Required(ErrorMessage="Price is required.")]
        //TODO: validate that it's not less than 0.
        public double PricePerHour
        {
            get;
            set;
        }
        public string VenueName
        {
            get;
            set;
        }
        [Required(ErrorMessage="The date you last used this instrument is required.")]
        public DateTime DateOfLastUsage
        {
            get;
            set;
        }
    }
}