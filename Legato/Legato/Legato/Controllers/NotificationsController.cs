using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legato.Helpers;
using Legato.Models;
using System.Web.Security;
using RiaLibrary.Web;
using System.Text;
using System.Threading;

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

        private void SendNotificationEmail(Notification n)
        {
            try
            {
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    const string subject = "New notification [Legato Network]";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Hello!");
                    sb.Append(Environment.NewLine);
                    sb.Append("There's a new notification for you in your Legato Network inbox.");
                    sb.Append(Environment.NewLine);
                    sb.Append("Click this link to view it: ");
                    sb.Append(new UrlHelper(Current.Context.Request.RequestContext).Action("List", "Notifications"));
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("If you did not register for an account at Legato Network and believe you received this email in error, please ignore this message.");
                    sb.Append(Environment.NewLine);
                    sb.Append("- Legato Network :)");

                    string body = sb.ToString();
                    var emailAddress = Current.DB.aspnet_Memberships.SingleOrDefault(u => u.UserId == n.UserID).LoweredEmail;

                    var netmessage = SendEmail.StandardNoReply(emailAddress, subject, body, false);
                    SendEmail.Send(netmessage);
                });
            }
            catch(Exception ex)
            {
                // Fail quietly
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
            }
            
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
        public virtual ActionResult Invalidate(string username)
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
