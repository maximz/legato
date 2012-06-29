using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace Legato
{
    public class AccountProfile : ProfileBase
    {
        /// <summary>
        /// Gets the current user's Profile.
        /// </summary>
        public static AccountProfile CurrentUser
        {
            get
            {
                if (Membership.GetUser() != null)
                    return ProfileBase.Create(Membership.GetUser().UserName) as AccountProfile;
                else
                    return null;
            }
        }

        public static AccountProfile GetProfileOfUser(string userName)
        {
            return ProfileBase.Create(userName) as AccountProfile;
        }

        internal static AccountProfile NewUser
        {
            get { return System.Web.HttpContext.Current.Profile as AccountProfile; }
        }


        public DateTime ReinstateDate
        {
            get { return ((DateTime)(base["ReinstateDate"])); }
            set { base["ReinstateDate"] = value; Save(); }
        }

        public string FullName
        {
            get { return ((string)(base["FullName"])); }
            set { base["FullName"] = value; Save(); }
        }

        public int Reputation
        {
            get { return ((int)(base["Reputation"])); }
            set { base["Reputation"] = value; Save(); }
        }

        public string AboutMe
        {
            get { return ((string)(base["AboutMe"])); }
            set { base["AboutMe"] = value; Save(); }
        }
        // add additional properties here

        //To use AccountProfile from other places:
        //AccountProfile currentProfile = AccountProfile.CurrentUser;
        //currentProfile.FullName = "Snoopy";
        //currentProfile.OtherProperty = "ABC";
        //currentProfile.Save();

    }
}