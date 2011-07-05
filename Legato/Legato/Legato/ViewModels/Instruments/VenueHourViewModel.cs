using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Legato.Helpers;

namespace Legato.ViewModels
{
    [CompareProperties(ComparisonProperty1="StartTime",ComparisonProperty2="EndTime",LessThanAllowed=false,GreaterThanAllowed=true,EqualToAllowed=false,AllowNullValues=true,ErrorMessage="End time must be after start time.")]
    public class VenueHourViewModel
    {
        [Required(ErrorMessage="Day of week ID is required.")]
        public int DayOfWeekId
        {
            get;
            set;
        }
        [Required(ErrorMessage="Day of week name is required.")]
        public string DayOfWeekName
        {
            get;
            set;
        }
        [DisplayName("Start Time")]
        public DateTime StartTime
        {
            get;
            set;
        }
        [DisplayName("End Time")]
        public DateTime EndTime
        {
            get;set;
        }

        [DisplayName("Closed")]
        public bool Closed
        {
            get;
            set;
        }
    }
}