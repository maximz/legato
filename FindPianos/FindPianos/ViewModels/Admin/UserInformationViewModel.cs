using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FindPianos.Models;
using System.ComponentModel;

namespace FindPianos.ViewModels
{
    public class UserInformationViewModel
    {
        [DisplayName("User")]
        public MembershipUser User
        { get; set; }
        [DisplayName("Suspensions")]
        public List<UserSuspension> Suspensions
        { get; set; }
        [DisplayName("Reinstate date")]
        public DateTime ReinstateDate
        { get; set; }

    }
}