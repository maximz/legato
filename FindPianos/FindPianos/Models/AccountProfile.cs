using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace FindPianos
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

        public string ProfilePictureURL
        {
            get { return ((string)(base["ProfilePictureURL"])); }
            set { base["ProfilePictureURL"] = value; Save(); }
        }

        public DateTime ReinstateDate
        {
            get { return ((DateTime)(base["ReinstateDate"])); }
            set { base["ReinstateDate"] = value; Save(); }
        }
        // add additional properties here

        //To use AccountProfile from other places:
        //AccountProfile currentProfile = AccountProfile.CurrentUser;
        //currentProfile.FullName = "Snoopy";
        //currentProfile.OtherProperty = "ABC";
        //currentProfile.Save();

    }
}