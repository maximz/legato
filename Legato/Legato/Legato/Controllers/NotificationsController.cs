using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legato.Helpers;
using System.Web.Security;

namespace Legato.Controllers
{
    [HandleError]
    public class NotificationsController : CustomControllerBase
    {
        [CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
        public ActionResult View()
        {
            ClearNotifications();
            return View(GetNotifications());
        }


        public List<Notifications> GetNotifications(int userid)
	{
		var cachekey = "notifications-"+userid;
            var currentCache = Current.GetCachedObject(cachekey);
		if(currentCache != null)
		{
            return currentCache as List<Notifications>;
		}
		var db = Current.DB;
		var n = db.Notifications.Where(n=>n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread).OrderByDescending(n=>n.Date).ToList();
        Current.SetCachedObject(cachekey, n, 5 * 60);
		return n;
	}

        public bool GetState()
        {
            if (GetNotifications(Current.User) != null)
            {
                return true;
            }
            return false;
        }

        public void ClearNotifications(int userid)
        {
            var cachekey = "notifications-" + userid;
            var db = Current.DB;
            foreach (var n in db.Notifications.Where(n => n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread))
            {
                n.IsUnread = false;
            }
            db.SubmitChanges();

            Current.RemoveCachedObject(cachekey);
        }
    }
}
