using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legato.Helpers;
using Legato.Models;
using System.Web.Security;

namespace Legato.Controllers
{
    [HandleError]
    public class NotificationsController : CustomControllerBase
    {
        [CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
        public virtual ActionResult List()
        {
            ClearNotifications((Guid)Membership.GetUser().ProviderUserKey);
            return View(GetNotifications((Guid)Membership.GetUser().ProviderUserKey));
        }


        public List<Notification> GetNotifications(Guid userid)
	{
		var cachekey = "notifications-"+userid;
            var currentCache = Current.GetCachedObject(cachekey);
		if(currentCache != null)
		{
            return currentCache as List<Notification>;
		}
		var db = Current.DB;
		var notifications = db.Notifications.Where(n=>n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread).OrderByDescending(n=>n.Date).ToList();
        Current.SetCachedObject(cachekey, notifications, 5 * 60);
        return notifications;
	}

        public bool GetState()
        {
            if (GetNotifications((Guid)Membership.GetUser().ProviderUserKey) != null)
            {
                return true;
            }
            return false;
        }

        public void ClearNotifications(Guid userid)
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
