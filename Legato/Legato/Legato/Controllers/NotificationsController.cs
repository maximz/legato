using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legato.Helpers;
using Legato.Models;
using System.Web.Security;
using RiaLibrary.Web;

namespace Legato.Controllers
{
	[HandleError]
	public partial class NotificationsController : CustomControllerBase
	{
		/// <summary>
		/// Lists a user's notification.
		/// </summary>
		/// <returns></returns>
		[CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
		[Url("notifications")]
		public virtual ActionResult List()
		{
			ClearNotifications((Guid)Membership.GetUser().ProviderUserKey);
			return View(GetNotifications((Guid)Membership.GetUser().ProviderUserKey));
		}


		/// <summary>
		/// Gets the notifications.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <returns></returns>
		internal List<Notification> GetNotifications(Guid userid)
		{
			if(userid == null)
			{
				return null;
			}
			var cachekey = "notifications-"+userid;
			var currentCache = Current.GetCachedObject(cachekey);
			if(currentCache != null)
			{
				return currentCache as List<Notification>;
			}
			var db = Current.DB;
			var notifications = db.Notifications.Where(n=>n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread).OrderByDescending(n=>n.Date).ToList();
			foreach(var n in notifications)
			{
				n.GlobalPostID1.FillProperties();
			}
			Current.SetCachedObject(cachekey, notifications, 5 * 60);
			return notifications;
		}

		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <returns></returns>
		internal bool GetState()
		{
			var results = GetNotifications((Guid)Membership.GetUser().ProviderUserKey);
			if (results != null && results.Count() >= 1)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Clears the notifications.
		/// </summary>
		/// <param name="userid">The userid.</param>
		internal void ClearNotifications(Guid userid)
		{
			if(userid == null)
			{
				return;
			}
			var cachekey = "notifications-" + userid;
			var db = Current.DB;
			foreach (var n in db.Notifications.Where(n => n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread))
			{
				n.IsUnread = false;
			}
			db.SubmitChanges();

			Current.RemoveCachedObject(cachekey);
		}

		/// <summary>
		/// Creates the notification.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <param name="post">The post.</param>
		/// <param name="time">The time.</param>
		/// <returns></returns>
		internal Notification CreateNotification(Guid userid, GlobalPostID post, DateTime time)
		{
			var db = Current.DB;
			var notification = new Notification();
			notification.UserID = userid;
			notification.IsUnread = true;
			notification.GlobalPostID = post.GlobalPostID1;
			notification.Date = time;

			db.Notifications.InsertOnSubmit(notification);
			db.SubmitChanges();

			return notification;
		}
	}
}
