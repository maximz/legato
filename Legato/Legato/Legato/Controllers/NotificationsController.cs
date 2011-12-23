﻿using System;
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
	/// <summary>
	/// Handles user notifications.
	/// </summary>
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
			ViewBag.curPage = "Account";
            var notifications = GetNotifications((Guid)Membership.GetUser().ProviderUserKey);
			ClearNotifications((Guid)Membership.GetUser().ProviderUserKey);
			return View(notifications);
		}


		/// <summary>
		/// Gets the notifications.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <returns></returns>
		public List<Notification> GetNotifications(Guid userid)
		{
			if(userid == null)
			{
				return null;
			}
			var cachekey = GetCacheKey(userid);
			var currentCache = Current.GetCachedObject(cachekey);
			if(currentCache != null)
			{
				return currentCache as List<Notification>;
			}
			var db = Current.DB;
			var notifications = db.Notifications.Where(n=>n.UserID == (Guid)Membership.GetUser().ProviderUserKey).OrderByDescending(n=>n.Date).ToList();
			foreach(var n in notifications)
			{
				n.GlobalPostID1.FillProperties();
			}
			Current.SetCachedObjectPermanent(cachekey, notifications); // not removed until cache is invalidated with a new notification
			return notifications;
		}

		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <returns>True if unread notifications exist.</returns>
        public bool GetState()
		{
			var results = GetNotifications((Guid)Membership.GetUser().ProviderUserKey);
			if (results != null && results.Where(r=>r.IsUnread).Count() >= 1)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Clears notifications: sets to read
		/// </summary>
		/// <param name="userid">The userid.</param>
        public void ClearNotifications(Guid userid)
		{
			if(userid == null)
			{
				return;
			}
			var db = Current.DB;
			foreach (var n in db.Notifications.Where(n => n.UserID == (Guid)Membership.GetUser().ProviderUserKey && n.IsUnread))
			{
				n.IsUnread = false;
			}
			db.SubmitChanges();

			InvalidateNotificationCache(userid);
		}

		/// <summary>
		/// Creates the notification.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <param name="post">The post.</param>
		/// <param name="time">The time.</param>
		/// <returns></returns>
        public Notification CreateNotification(Guid userid, GlobalPostID post, DateTime time)
		{
			var db = Current.DB;
			var notification = new Notification();
			notification.UserID = userid;
			notification.IsUnread = true;
			notification.GlobalPostID = post.GlobalPostID1;
			notification.Date = time;

			db.Notifications.InsertOnSubmit(notification);
			db.SubmitChanges();

			InvalidateNotificationCache(userid);

			return notification;
		}

		/// <summary>
		/// Invalidates the notification cache.
		/// </summary>
		/// <param name="userid">The userid.</param>
        public void InvalidateNotificationCache(Guid userid)
		{
			Current.RemoveCachedObject(GetCacheKey(userid));
		}

        [Url("notifications/invalidate/{username}")]
        [CustomAuthorization(AuthorizedRoles="Administrator,Moderator")]
        public ActionResult Invalidate(string username)
        {
            Current.RemoveCachedObject(GetCacheKey((Guid)Membership.FindUsersByName(username)[username].ProviderUserKey));
            return Content("done");
        }

		/// <summary>
		/// Gets the cache key.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <returns></returns>
        public string GetCacheKey(Guid userid)
		{
			return "notifications-" + userid;
		}
	}
}
