using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Models;
using Legato.Helpers;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace Legato.Controllers
{
    [CustomAuthorization(AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false, AuthorizedRoles = RoleNames.Administrator)]
    public class AdminController : CustomControllerBase
    {
        public static class WCKeys
        {
            public static string messageEnabled = "NotificationMessageEnabled";
            public static string messageText = "CurrentNotificationMessage";
        }


        //
        // GET: /Admin/
        [Url("Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.isMessageEnabled = (ConfigurationManager.AppSettings[WCKeys.messageEnabled] == "true");
            ViewBag.currentMessage = Server.HtmlDecode(ConfigurationManager.AppSettings[WCKeys.messageText]);
            ViewBag.isWhiteListEnabled = CustomControllerBase.WhiteListEnabled;



            return View();
        }



        [Url("Admin/Message/Toggle")]
        [HttpPost]
        [VerifyReferrer]
        public ActionResult ToggleMessage()
        {
            var key = WCKeys.messageEnabled;
            var messageEnabled = ConfigurationManager.AppSettings[key];
            if(messageEnabled == "true")
            {
                messageEnabled = "false";
            }
            else
            {
                messageEnabled = "true";
            }
            ConfigurationManager.AppSettings.Set(key, messageEnabled);
            return RedirectToAction("Index");
        }

        [Url("Admin/Message/Set")]
        [HttpPost]
        [ValidateInput(false)]
        [VerifyReferrer]
        public ActionResult SetMessage(string message)
        {
            var key = WCKeys.messageText;
            ConfigurationManager.AppSettings.Set(key, Server.HtmlEncode(message));
            return RedirectToAction("Index");
        }

        [Url("Admin/Whitelist/Toggle")]
        [HttpPost]
        [VerifyReferrer]
        public ActionResult ToggleWhitelist()
        {
            var current = CustomControllerBase.WhiteListEnabled;
            if(current)
            {
                CustomControllerBase.WhiteListEnabled = false;
            }
            else
            {
                CustomControllerBase.WhiteListEnabled = true;
            }
            return RedirectToAction("Index");
        }

        [Url("Admin/Whitelist/Get")]
        [HttpGet]
        public ActionResult GetWhitelist()
        {
            var list = string.Join(";", (IEnumerable<OpenIDWhiteList>)Current.DB.OpenIDWhiteLists.Where(w => w.IsEnabled).ToArray());
            ViewBag.list = list;
            return View("WhitelistForm");
        }

        [Url("Admin/Whitelist/Set")]
        [HttpPost]
        [ValidateInput(false)]
        [VerifyReferrer]
        public ActionResult SetWhitelist(string list)
        {
            var openids = list.Split(';');
            foreach(var i in Current.DB.OpenIDWhiteLists)
            {
                i.IsEnabled = false;
            }
            Current.DB.SubmitChanges();

            foreach(var j in openids)
            {
                var openid = new OpenIDWhiteList();
                openid.OpenID = j;
                openid.IsEnabled = true;
                Current.DB.OpenIDWhiteLists.InsertOnSubmit(openid);
            }
            Current.DB.SubmitChanges();

            return View("Index");
        }

        [Url("Admin/DeletePost")]
        [HttpPost]
        [VerifyReferrer]
        public ActionResult DeletePost(int id)
        {
            var gpost = Current.DB.GlobalPostIDs.Where(p => p.GlobalPostID1 == id).SingleOrDefault();
            gpost.FillProperties();
            var post = gpost.UnderlyingPost;
            post.FillProperties();

            // Serialize
            XmlSerializer s = new XmlSerializer(gpost.UnderlyingType);
            StringWriter sw = new StringWriter();
            s.Serialize(sw, post);
            string serializedXml = sw.ToString();
            sw.Close();

            switch(gpost.UnderlyingType.Name)
            {
                case "Instrument":
                    Current.DB.Instruments.DeleteOnSubmit(post);
                    break;
                case "InstrumentReview":
                    Current.DB.InstrumentReviews.DeleteOnSubmit(post);
                    break;
                case "InstrumentReviewRevision":
                    Current.DB.InstrumentReviewRevisions.DeleteOnSubmit(post);
                    break;
                default:
                    return Content("no type");
            }

            Current.DB.SubmitChanges();
            return Content(serializedXml);

            /* // Deserialize
             ShoppingList newList;
            newList = (gpost.UnderlyingType)s.Deserialize( new StringReader( serializedXml ) );
             * 
             * */
        }

        // Search stuff is at SearchController.RegenerateIndex();


        #region Users

        [Url("Admin/Users")]
        [HttpPost]
        [VerifyReferrer]
        public virtual ActionResult UserSearchByName(string nameContains)
        {
            try
            {
                var db = Current.DB;
                var results = db.aspnet_Users.Where(u => u.UserName.Contains(nameContains)).Take(50).ToList();
                return View("UserSearchByNamePOST", results);
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }

        }

        [Url("Admin/Users/View/{UserId}")]
        [HttpGet]
        public virtual ActionResult GetUserById(Guid UserId)
        {
            var db = Current.DB;
            var model = new UserInformationViewModel();
            model.User = Membership.GetUser(UserId, false);
            if (model.User == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            model.Suspensions = db.UserSuspensions.Where(s => s.UserID == UserId).ToList();
            model.ReinstateDate = model.Suspensions.Max(r => r.ReinstateDate);
            return View(model);
        }

        [Url("Admin/SuspendUser/{UserId}")]
        [HttpGet]
        public virtual ActionResult SuspendUser(Guid UserId)
        {
            return View(new SuspendUserViewModel() { UserID = UserId });
        }
        [Url("Admin/SuspendUser")]
        [HttpPost]
        [VerifyReferrer]
        public virtual ActionResult SuspendUser(SuspendUserViewModel model)
        {
            var sus = new UserSuspension();
                var db = Current.DB;
                var username = Membership.GetUser(model.UserID, false).UserName;
                sus = new UserSuspension()
                {
                    SuspensionDate = DateTime.Now,
                    Reason = model.Reason,
                    UserID = model.UserID
                };
                if (model.ReinstateDate == null)
                {
                    sus.ReinstateDate = DateTime.MaxValue;
                }
                db.UserSuspensions.InsertOnSubmit(sus);
                db.SubmitChanges();
                AccountProfile.GetProfileOfUser(username).ReinstateDate = sus.ReinstateDate;
                AccountProfile.GetProfileOfUser(username).Save();
                return RedirectToAction("Index");
        }

        public class SuspendUserViewModel
        {
            [DisplayName("User ID")]
            [Required(ErrorMessage = "You must specify a user ID.")]
            public Guid UserID
            {
                get;
                set;
            }
            [DisplayName("Reinstatement Date")]
            [CompareValue(ComparisonValue = "DateTime.Now", EqualToAllowed = false, GreaterThanAllowed = true, LessThanAllowed = false, AllowNullValues = true, ErrorMessage = "The reinstatement date must be in the future.")]
            public DateTime ReinstateDate
            {
                get;
                set;
            }
            [DisplayName("Reason")]
            [Required(ErrorMessage = "You must specify a reason.")]
            public string Reason
            {
                get;
                set;
            }
        }
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

        [Url("Admin/Users/Emails/{delimiter}")]
        [HttpGet]
        public ActionResult GetEmailList(string delimiter)
        {
            return Content(string.Join(delimiter, Current.DB.aspnet_Memberships.Select(m => m.Email).ToArray()));
        }

        #endregion



        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
