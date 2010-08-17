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

        #region OpenID methods

        [HttpGet]
        [Url("Admin/OpenID/Whitelist/List")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult ListOpenIDWhiteList()
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var records = db.OpenIDWhiteLists.OrderBy(p => p.ID).ToList();
                    return View(records);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpPost][VerifyReferrer]
        [Url("Admin/OpenID/Whitelist/Add")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult AddToOpenIDWhiteList(string OpenIDClaim)
        {
            try
            {
                if (OpenIDClaim.IsNullOrEmpty())
                {
                    return RedirectToAction("BadRequest", "Error");
                }
                using (var db = new LegatoDataContext())
                {
                    var DupeCheck = db.OpenIDWhiteLists.Where(l => l.OpenID == OpenIDClaim.Trim()).SingleOrDefault();
                    if (DupeCheck != null)
                    {
                        return RedirectToAction("BadRequest", "Error");
                    }

                    var record = new OpenIDWhiteList();
                    record.IsEnabled = true;
                    record.OpenID = OpenIDClaim.Trim();
                    db.OpenIDWhiteLists.InsertOnSubmit(record);
                    db.SubmitChanges();
                }
                return RedirectToAction("ListOpenIDWhiteList");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpGet]
        [Url("Admin/OpenID/Whitelist/Remove")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult RemoveFromOpenIDWhiteList(string OpenIDClaim)
        {
            return View(OpenIDClaim);
        }

        [HttpPost][VerifyReferrer]
        [Url("Admin/OpenID/Whitelist/Remove")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult RemoveFromOpenIDWhiteList(string OpenIDClaim, string confirm)
        {
            try
            {
                if (OpenIDClaim.IsNullOrEmpty())
                {
                    return RedirectToAction("BadRequest", "Error");
                }
                using (var db = new LegatoDataContext())
                {
                    var ExistsCheck = db.OpenIDWhiteLists.Where(l => l.OpenID == OpenIDClaim.Trim()).SingleOrDefault();
                    if (ExistsCheck == null)
                    {
                        return RedirectToAction("BadRequest", "Error");
                    }

                    db.OpenIDWhiteLists.DeleteOnSubmit(ExistsCheck);
                    db.SubmitChanges();
                }
                return RedirectToAction("ListOpenIDWhiteList");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        [Url("Admin/OpenID/Whitelist/Toggle")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult ToggleEnabledDisabledFromOpenIDWhiteList(string OpenIDClaim)
        {
            return View(OpenIDClaim);
        }

        [HttpPost]
        [VerifyReferrer]
        [Url("Admin/OpenID/Whitelist/Toggle")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult ToggleEnabledDisabledFromOpenIDWhiteList(string OpenIDClaim, string confirm)
        {
            try
            {
                if (OpenIDClaim.IsNullOrEmpty())
                {
                    return RedirectToAction("BadRequest", "Error");
                }
                using (var db = new LegatoDataContext())
                {
                    var ExistsCheck = db.OpenIDWhiteLists.Where(l => l.OpenID == OpenIDClaim.Trim()).SingleOrDefault();
                    if (ExistsCheck == null)
                    {
                        return RedirectToAction("BadRequest", "Error");
                    }

                    ExistsCheck.IsEnabled = ExistsCheck.IsEnabled.Flip();
                    db.SubmitChanges();
                }
                return RedirectToAction("ListOpenIDWhiteList");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        [VerifyReferrer]
        [Url("Admin/OpenID/Whitelist/DisableAll")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult DisableAllInOpenIDWhiteList()
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    foreach(var record in db.OpenIDWhiteLists.Where(p=>p.IsEnabled))
                    {
                        record.IsEnabled = false;
                    }
                    db.SubmitChanges();
                }
                return RedirectToAction("ListOpenIDWhiteList");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        [VerifyReferrer]
        [Url("Admin/OpenID/Whitelist/EnableAll")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult EnableAllInOpenIDWhiteList()
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    foreach (var record in db.OpenIDWhiteLists.Where(p => !p.IsEnabled))
                    {
                        record.IsEnabled = true;
                    }
                    db.SubmitChanges();
                }
                return RedirectToAction("ListOpenIDWhiteList");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }


        [HttpGet]
        [Url("Admin/Accounts/Invite")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult GenerateOneTimeRegistrationCode()
        {
            return View();
        }
        [HttpPost][VerifyReferrer]
        [Url("Admin/Accounts/Invite")]
        [CustomAuthorization(AuthorizedRoles = "Admin", AuthorizeSuspended = false, AuthorizeEmailNotConfirmed = false)]
        public ActionResult GenerateOneTimeRegistrationCode(string CustomWelcomeName)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var record = new OneTimeRegistrationCode();
                    record.Id = Guid.NewGuid();
                    record.CustomWelcomeName = CustomWelcomeName.HasValue() ? CustomWelcomeName.Trim() : string.Empty;
                    db.OneTimeRegistrationCodes.InsertOnSubmit(record);

                    db.SubmitChanges();

                    return View("OneTimeRegCodeSuccess", record.Id);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion


    }
}
