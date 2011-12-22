using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Helpers;
using Legato.Models;
using System.Web.Security;
using System.Net;

namespace Legato.Controllers
{
    [HandleError]
    [CustomAuthorization(AuthorizeEmailNotConfirmed=false, AuthorizeSuspended=false)]
    public class MessagesController : CustomControllerBase
    {
        //
        // GET: /Messages/

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        [Url("messages")]
        public virtual ActionResult List()
        {
            var db = Current.DB;
            var userID = Current.UserID.Value;
            var threads = db.Conversations.Where(c => c.aspnet_User.UserId == userID || c.aspnet_User1.UserId == userID).OrderByDescending(c => c.LastMessageDate).ToList();
            return View(threads);
        }

        /// <summary>
        /// Threads the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [Url("messages/{id}")]
        public virtual ActionResult Thread(int id) // individual thread
        {
            var db = Current.DB;
            var conversation = db.Conversations.Where(c => c.ConversationID == id).SingleOrDefault();
            var messages = db.Messages.Where(m => m.ConversationID == id).OrderBy(m => m.Date).ToList();
            return View(new Tuple<Conversation, List<Message>>(conversation, messages));
        }
        /// <summary>
        /// Messages the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [Url("messages/individual/{id}")]
        public virtual ActionResult Message(int id) // individual message
        {
            var db = Current.DB;
            var message = db.Messages.Where(m => m.MessageID == id).SingleOrDefault();
            var threadID = message.ConversationID;
            var Url = new UrlHelper(Current.Request.RequestContext);
            var redirect = Url.Action("Thread", new { id = threadID }) + "#message-" + id;
            return Redirect(redirect);
        }

        /// <summary>
        /// Replies the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        [VerifyReferrer]
        [Url("messages/reply")]
        public virtual ActionResult Reply(Message data)
        {
            var db = Current.DB;
            var currentTime = DateTime.Now;
            data.SenderID = Current.UserID.Value;
            var thread = db.Conversations.Where(c => c.ConversationID == data.ConversationID).SingleOrDefault();

            // Set receipient ID to the other user's ID
            if(Current.UserID.Value == thread.User1)
            {
                data.ReceipientID = thread.User2;
            }
            else // Current.UserID.Value == thread.User2
            {
                data.ReceipientID = thread.User1;
            }

            data.Date = currentTime;
            thread.LastMessageDate = currentTime;
            data.NumberInConvo = thread.Messages.OrderByDescending(m => m.Date).First().NumberInConvo + 1;
            data.IsUnread = true;

            // Handle markdown
            data.Markdown = HtmlUtilities.Sanitize(data.Markdown);
            data.Html = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(data.Markdown));

            // Submit
            try
            {
                db.Messages.InsertOnSubmit(data); //An exception will be thrown here if there are invalid properties
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                // Yikes
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }

            // Global Post:
            var gpostmessage = new GlobalPostID();
            gpostmessage.UserID = Current.UserID.Value;
            gpostmessage.PostCategory = MagicCategoryStrings.Message;
            gpostmessage.SubmissionDate = currentTime;
            gpostmessage.SpecificPostID = data.MessageID;
            db.GlobalPostIDs.InsertOnSubmit(gpostmessage);
            db.SubmitChanges();
            data.GlobalPostID = gpostmessage.GlobalPostID1;
            db.SubmitChanges();

            return RedirectToAction("Message", new { id = data.MessageID });
        }

        /// <summary>
        /// Flags the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        [VerifyReferrer]
        [Url("messages/flag")]
        public virtual JsonResult Flag(int id)
        {
            var db = Current.DB;
            var currentFlags = db.MessageFlags.Where(f => f.MessageID == id && f.FlaggerID == Current.UserID.Value);
            if(!(currentFlags == null || currentFlags.Count() == 0))
            {
                // This user has already flagged this post. Cancel this new flagging request.
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json(new { status = "error: already flagged" });
            }

            var flag = new MessageFlag();
            flag.FlaggerID = Current.UserID.Value;
            flag.Date = DateTime.Now;
            flag.MessageID = id;
            db.MessageFlags.InsertOnSubmit(flag);
            db.SubmitChanges();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success" });
        }

        /// <summary>
        /// Composes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Url("messages/compose")]
        public virtual ActionResult Compose()
        {
            return View(new Tuple<Message, string, string>(new Message(), "", ""));
        }

        /// <summary>
        /// Composes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        [VerifyReferrer]
        [Url("messages/compose")]
        public virtual ActionResult Compose(Tuple<Message, string, string> data) // Message, subject, receipient's user name
        {
            var db = Current.DB;
            var currentTime = DateTime.Now;
            var message = data.Item1;
            var conversation = new Conversation();

            // Sender IDs
            message.SenderID = Current.UserID.Value;
            conversation.User1 = Current.UserID.Value;

            message.Date = currentTime;
            message.IsUnread = true;
            message.NumberInConvo = 1;
            conversation.LastMessageDate = currentTime;
            conversation.StartDate = currentTime;
            conversation.Subject = data.Item2.TruncateWithEllipsis(100);

            // Look up other user
            var otherUser = (Guid)Membership.GetUser(data.Item3).ProviderUserKey;
            message.ReceipientID = otherUser;
            conversation.User2 = otherUser;

            // Handle markdown
            message.Markdown = HtmlUtilities.Sanitize(message.Markdown);
            message.Html = HtmlUtilities.Safe(HtmlUtilities.RawToCooked(message.Markdown));

            // Submit
            try
            {
                db.Conversations.InsertOnSubmit(conversation); //An exception will be thrown here if there are invalid properties
                db.SubmitChanges();
                message.ConversationID = conversation.ConversationID;
                db.Messages.InsertOnSubmit(message);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                // Yikes
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex, Current.Context);
                return RedirectToAction("InternalServerError", "Error");
            }

            // Global Post:

            var gpostconversation = new GlobalPostID();
            gpostconversation.UserID = Current.UserID.Value;
            gpostconversation.PostCategory = MagicCategoryStrings.Conversation;
            gpostconversation.SubmissionDate = currentTime;
            gpostconversation.SpecificPostID = conversation.ConversationID;
            db.GlobalPostIDs.InsertOnSubmit(gpostconversation);
            db.SubmitChanges();
            conversation.GlobalPostID = gpostconversation.GlobalPostID1;
            db.SubmitChanges();

            var gpostmessage = new GlobalPostID();
            gpostmessage.UserID = Current.UserID.Value;
            gpostmessage.PostCategory = MagicCategoryStrings.Message;
            gpostmessage.SubmissionDate = currentTime;
            gpostmessage.SpecificPostID = message.MessageID;
            db.GlobalPostIDs.InsertOnSubmit(gpostmessage);
            db.SubmitChanges();
            message.GlobalPostID = gpostmessage.GlobalPostID1;
            db.SubmitChanges();

            return RedirectToAction("Thread", new { id = conversation.ConversationID });
        }
    }
}
