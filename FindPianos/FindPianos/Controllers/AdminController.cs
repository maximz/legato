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
using FindPianos.ViewModels;

namespace FindPianos.Controllers
{
    [CustomAuthorization(AuthorizedRoles="Admin", AuthorizeSuspended=false, AuthorizeEmailNotConfirmed=false)]
    public class AdminController : CustomControllerBase
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
        [HttpPost][VerifyReferrer]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult UserSearchByName(string nameContains)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var results = db.aspnet_Users.Where(u => u.UserName.Contains(nameContains)).Take(50).ToList();
                    return View("UserSearchByNamePOST", results);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
            
        }

        [Url("Admin/Users/View/{UserId}")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult GetUserById(Guid UserId)
        {
            using (var db = new LegatoDataContext())
            {
                var model = new UserInformationViewModel();
                model.User = Membership.GetUser(UserId, false);
                if(model.User==null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                model.Suspensions = db.UserSuspensions.Where(s => s.UserID == UserId).ToList();
                model.ReinstateDate = model.Suspensions.Max(r => r.ReinstateDate);
                return View(model);
            }
        }

        [Url("Admin/Users/Suspend/{UserId}")][HttpGet]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        public ActionResult SuspendUser(Guid UserId)
        {
            return View(new SuspendUserViewModel() { UserID=UserId });
        }
        [Url("Admin/Users/Suspend")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed=false)]
        [HttpPost][VerifyReferrer]
        public ActionResult SuspendUser(SuspendUserViewModel model)
        {
            var sus = new UserSuspension();
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var username = Membership.GetUser(model.UserID,false).UserName;
                    sus = new UserSuspension()
                    {
                        SuspensionDate = DateTime.Now,
                        Reason = model.Reason,
                        UserID = model.UserID
                    };
                    if(model.ReinstateDate==null)
                    {
                        sus.ReinstateDate = DateTime.MaxValue;
                    }
                    db.UserSuspensions.InsertOnSubmit(sus);
                    db.SubmitChanges();
                    AccountProfile.GetProfileOfUser(username).ReinstateDate = sus.ReinstateDate;
                    AccountProfile.GetProfileOfUser(username).Save();
                    return RedirectToAction("GetUserById", new
                    {
                        UserId = model.UserID
                    });
                }
            }
            catch
            {
                return View("NotFound","Error");
            }
        }


    }
}
