using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;
using RiaLibrary.Web;
using FindPianos.Helpers;
using System.Globalization;
using System.Web.Security;

namespace FindPianos.Controllers
{
    [CustomAuthorization(AuthorizedRoles="Admin", AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Url("Admin")][HttpGet]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult UserSearchByName()
        {
            return View("UserSearchByNameGET");
        }

        [Url("Admin")]
        [HttpPost]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult UserSearchByName(string nameContains)
        {
            using (var db = new PianoDataContext())
            {
                var results = db.aspnet_Users.Where(u => u.UserName.Contains(nameContains)).Take(50).ToList();
                ViewData["table"] = results;
            }
            return View("UserSearchByNamePOST");
        }

        [Url("Admin/Users/View/{UserID}")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult GetUserById(Guid UserId)
        {
            using (var db = new PianoDataContext())
            {
                ViewData["userInfo"] = Membership.GetUser(UserId, false);
                var suspensions = db.PianoUserSuspensions.Where(s => s.UserID == UserId).ToList();
                ViewData["suspensionList"] = suspensions;
                ViewData["reinstateDate"] = suspensions.Max(r => r.ReinstateDate);
            }
            return View();
        }

        [Url("Admin/Users/Suspend/{UserID}")][HttpGet]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult SuspendUser(Guid UserID)
        {
            return View();
        }
        [Url("Admin/Users/Suspend")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost]
        public ActionResult SuspendUser(Guid UserID, DateTime reinstateDate, string reason)
        {
            var sus = new PianoUserSuspension();
            try
            {
                using (var db = new PianoDataContext())
                {
                    var username = Membership.GetUser(UserID,false).UserName;
                    sus = new PianoUserSuspension()
                    {
                        SuspensionDate = DateTime.Now,
                        ReinstateDate = reinstateDate,
                        Reason = reason,
                        UserID = UserID
                    };
                    db.PianoUserSuspensions.InsertOnSubmit(sus);
                    db.SubmitChanges();
                    AccountProfile.GetProfileOfUser(username).ReinstateDate = reinstateDate;
                    AccountProfile.GetProfileOfUser(username).Save();
                    return RedirectToAction("GetUserById", new
                    {
                        UserId = UserID
                    });
                }
            }
            catch
            {
                foreach (RuleViolation rv in sus.GetRuleViolations())
                {
                    ModelState.AddModelError(rv.PropertyName, rv.ErrorMessage);
                    ModelState.SetModelValue(rv.PropertyName, new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
                }
                return View();
            }
        }


    }
}
