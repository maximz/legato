using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FindPianos.Helpers;

namespace FindPianos.ViewModels
{
    [CompareProperties(ComparisonProperty1="StartTime",ComparisonProperty2="EndTime",LessThanAllowed=false,GreaterThanAllowed=true,EqualToAllowed=false,AllowNullValues=true,ErrorMessage="End time must be after start time.")]
    public class StoreHourViewModel
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
        [Required(ErrorMessage="You must indicate whether the instrument is available or not on this day of the week.")]
        public bool Closed
        {
            get;
            set;
        }
    }
}