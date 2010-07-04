using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.ViewModels
{
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
        public DateTime StartTime
        {
            get;
            set;
        }
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