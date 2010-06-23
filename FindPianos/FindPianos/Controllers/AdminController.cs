using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindPianos.Models;
using RiaLibrary.Web;
using FindPianos.Components;

namespace FindPianos.Controllers
{
    [AuthorizeExceptSuspended(Roles="Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Url("Admin")]
        [AuthorizeExceptSuspended(Roles = "Admin")]
        public ActionResult UserSearchByName()
        {
            return View();
        }

        [Url("Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeExceptSuspended(Roles = "Admin")]
        public ActionResult UserSearchByName(string nameContains)
        {
            using (var db = new PianoDataContext())
            {
                var results = db.aspnet_Users.Where(u => u.UserName.Contains(nameContains)).Take(50).ToList();
                ViewData["table"] = results;
            }
            return View();
        }

        [Url("Admin/Users/View/{UserID}")]
        [AuthorizeExceptSuspended(Roles = "Admin")]
        public ActionResult GetUserById(Guid UserId)
        {
            using (var db = new PianoDataContext())
            {
                ViewData["userInfo"] = db.aspnet_Users.Where(u => u.UserId == UserId).SingleOrDefault();
                var suspensions = db.PianoUserSuspensions.Where(s => s.UserID == UserId).ToList();
                ViewData["suspensionList"] = suspensions;
                ViewData["reinstateDate"] = suspensions.Max(r => r.ReinstateDate);
            }
            return View();
        }

        [Url("Admin/Users/Suspend/{UserID}")]
        [AuthorizeExceptSuspended(Roles = "Admin")]
        public ActionResult SuspendUser(Guid UserID)
        {
            return View();
        }
        [Url("Admin/Users/Suspend")]
        [AuthorizeExceptSuspended(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SuspendUser(Guid UserID, DateTime reinstateDate, string reason)
        {
            using(var db = new PianoDataContext())
            {
                var username = db.aspnet_Users.Where(u=>u.UserId == UserID).SingleOrDefault().UserName;
                var sus = new PianoUserSuspension()
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
                return RedirectToAction("GetUserById", new {UserId=UserID});
            }
        }


    }
}
