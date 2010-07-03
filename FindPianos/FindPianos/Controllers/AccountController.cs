using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using MvcReCaptcha;
using FindPianos.Models;
using RiaLibrary.Web;
using FindPianos.Helpers;
using System.Text;
using System.Web.Mail;

namespace FindPianos.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }
        [HttpGet][CustomAuthorization(OnlyAllowUnauthenticatedUsers=true)]
        public ActionResult LogOn()
        {

            return View();
        }

        [HttpPost]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(userName, password))
            {
                return View();
            }

            FormsAuth.SignIn(userName, rememberMe);
            
            //check whether user is suspended and whether suspension has already ended
            if (!Roles.IsUserInRole(userName, "ActiveUser"))
            {
                var currentProfile = AccountProfile.CurrentUser;
                if (DateTime.Now >= currentProfile.ReinstateDate)
                {
                    Roles.AddUserToRole(userName, "ActiveUser");
                    currentProfile.ReinstateDate = DateTime.MinValue;
                    currentProfile.Save();
                }
            }

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {

            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorization(UnauthorizedRoles="ActiveUser")]
        [Url("Account/Status/Suspended")]
        public ActionResult ShowSuspensionStatus()
        {
            using (var db = new PianoDataContext())
            {
                if (!(AccountProfile.GetProfileOfUser(Membership.GetUser().UserName).ReinstateDate < DateTime.Now))
                {
                    ViewData["suspension"] = db.PianoUserSuspensions.Where(s => s.UserID == (Guid)Membership.GetUser().ProviderUserKey).OrderByDescending(k => k.ReinstateDate).Take(1).ToList()[0];
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
        }
        [CustomAuthorization(AuthorizeSuspended=false)]
        [Url("Account/Status/NotVerified")]
        public ActionResult ShowEmailAddressVerificationStatus()
        {
            if(!User.IsInRole("EmailNotConfirmed"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View("TimeToValidateYourEmailAddress");
        }
        [CustomAuthorization(AuthorizeSuspended=false)]
        [Url("Account/VerifyEmail/Resend")]
        public ActionResult ResendVerificationEmail()
        {
            if(!User.IsInRole("EmailNotConfirmed"))
            {
                return RedirectToAction("Index", "Home");
            }
            using(var db = new PianoDataContext())
            {
                var user = Membership.GetUser();
                ConfirmEmailAddress confirm;
                try
                {
                    confirm = db.ConfirmEmailAddresses.Where(a => a.UserID == (Guid)user.ProviderUserKey).Single();
                    
                }
                catch
                {
                    Roles.RemoveUserFromRole(User.Identity.Name, "EmailNotConfirmed");
                    return RedirectToAction("Index", "Home");
                }
                try
                {
                    SendVerificationEmail(user.Email, confirm.ConfirmID);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
                return View("TimeToValidateYourEmailAddress");
            }
        }
        [CustomAuthorization(AuthorizedRoles="EmailNotConfirmed", AuthorizeSuspended=false)]
        [Url("Account/Verify/{confirmId}")]
        public ActionResult VerifyEmailAddress(Guid confirmId)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    var confirm = db.ConfirmEmailAddresses.Where(a => a.ConfirmID == confirmId).SingleOrDefault();
                    if (confirm == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    if ((Guid)Membership.GetUser().ProviderUserKey != confirm.UserID)
                    {
                        //wrong user
                        return RedirectToAction("Forbidden", "Error");
                    }
                    db.ConfirmEmailAddresses.DeleteOnSubmit(confirm);
                    db.SubmitChanges();
                    Roles.RemoveUserFromRole(User.Identity.Name, "EmailNotConfirmed");
                    return View("VerifyEmailAddressSuccess");
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpGet]
        [Url("http://legatonetwork.com/Account/Options/ResetPassword/{resetId}")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ResetPassword(Guid resetId)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    var reset = db.ResetPasswordRecords.Where(a => a.ResetID == resetId).SingleOrDefault();
                    if (reset == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpPost]
        [Url("http://legatonetwork.com/Account/Options/ResetPassword")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ResetPassword(Guid resetId, string newPassword, string confirmPassword)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateResetPassword(newPassword, confirmPassword))
            {
                return View();
            }
            try
            {
                using (var db = new PianoDataContext())
                {
                    var reset = db.ResetPasswordRecords.Where(a => a.ResetID == resetId).SingleOrDefault();
                    if (reset == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    var u = Membership.GetUser(reset.UserID);
                    u.ChangePassword(u.GetPassword(), newPassword);
                    Membership.UpdateUser(u);
                    db.ResetPasswordRecords.DeleteOnSubmit(reset);
                    db.SubmitChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        [HttpGet]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword, bool captchaValid)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!captchaValid)
            {
                ModelState.AddModelError("CAPTCHA", "It seems that you did not type the verification word(s) (CAPTCHA) correctly. Please try again.");
                return View();
            }

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);
                Roles.AddUserToRoles(userName, new string[]{"ActiveUser","EmailNotConfirmed"});
                AccountProfile.NewUser.Initialize(userName, true);
                AccountProfile.NewUser.ProfilePictureURL = null;
                AccountProfile.NewUser.ReinstateDate = DateTime.MinValue;
                AccountProfile.NewUser.Save();
                using(var db = new PianoDataContext())
                {
                    try
                    {
                        var confirm = new ConfirmEmailAddress();
                        confirm.UserID = db.aspnet_Users.Where(u => u.UserName == userName).Single().UserId;
                        db.ConfirmEmailAddresses.InsertOnSubmit(confirm);
                        db.SubmitChanges();
                        SendVerificationEmail(email, confirm.ConfirmID);
                    }
                    catch
                    {
                        ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                        return View();
                    }
                }
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuth.SignIn(userName, false /* createPersistentCookie */);
                    //return RedirectToAction("Index", "Home");
                    ViewData["email"] = email;
                    return View("TimeToValidateYourEmailAddress");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize][HttpGet][Url("Account/Options/Password")]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [Authorize]
        [HttpPost]
        [Url("Account/Options/Password")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
                {
                    return View("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
        }

        [Authorize]
        [Url("Account/Options/Email")]
        [HttpGet]
        public ActionResult ChangeEmail()
        {
            ViewData["current"] = Membership.GetUser().Email;
            return View();
        }

        [Authorize]
        [Url("Account/Options/Email")]
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in email not being changed.")]
        public ActionResult ChangeEmail(string NewEmail, string ConfirmEmail)
        {

            if (!ValidateChangeEmail(NewEmail, ConfirmEmail))
            {
                return View();
            }

            try
            {
                var u = Membership.GetUser();
                u.Email = NewEmail;
                Membership.UpdateUser(u);
                ViewData["new"] = NewEmail;
                return View("ChangeEmailSuccess");
            }
            catch
            {
                ModelState.AddModelError("_FORM", "There was an error changing your email address. Please try again.");
                return View();
            }
        }
        [HttpGet]
        [Url("Account/Recover/Username")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotUsername()
        {
            return View();
        }

        [HttpPost]
        [Url("Account/Recover/Username")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotUsername(string email)
        {
            //validate that email is a valid email address
            try
            {
                var e = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                ModelState.AddModelError("email", "Please enter a valid email address.");
                return View();
            }
            var result = Membership.GetUserNameByEmail(email);
            if(string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("email", "We weren't able to find a user with this email address. Please make sure that the address is correct.");
                return View();
            }
            ViewData["result"] = result;
            return View("ForgotUsernameResult");
        }
        [HttpGet]
        [Url("Account/Recover/Password")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Url("Account/Recover/Password")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotPassword(string username)
        {
            try
            {
                using (var db = new PianoDataContext())
                {
                    var r = new ResetPasswordRecord();
                    var u = Membership.GetUser(username);
                    if(u==null)
                    {
                        throw new ApplicationException();
                    }
                    r.UserID = (Guid)u.ProviderUserKey;
                    db.ResetPasswordRecords.InsertOnSubmit(r);
                    db.SubmitChanges();
                    SendPasswordResetEmail(u.Email, r.ResetID);

                    return View("ForgotPasswordEmailSent");
                }
            }
            catch
            {
                ModelState.AddModelError("username", "No such user exists.");
                return View();
            }
            return View();
        }

        internal void SendPasswordResetEmail(string emailAddress, Guid id)
        {
            const string subject = "Reset your password - Legato Network";
            const string fromName = "Legato Network";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("You have selected to reset your password. To do so, please follow the link below.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Click this link to reset your password: ");
            sb.Append("http://legatonetwork.com/Account/Options/ResetPassword");
            sb.Append(id.ToString());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("If you did NOT select to reset your password at Legato Network and believe you received this email in error, please ignore this message.");
            sb.Append(Environment.NewLine);
            sb.Append("- Legato Network :)");

            

            string body = sb.ToString();

            var emailmessage = new System.Web.Mail.MailMessage()
                                   {
                                       Subject = subject,
                                       Body = body,
                                       From = "\"" + fromName + "\" <no-reply@legatonetwork.com>",
                                       To = emailAddress,
                                       BodyFormat = MailFormat.Text,
                                       Priority = MailPriority.Normal
                                   };

            SmtpMail.SmtpServer = "relay-hosting.secureserver.net";
            SmtpMail.Send(emailmessage);

        }
        internal void SendVerificationEmail(string emailAddress, Guid id)
        {
            const string subject = "Verify your email address - Legato Network";
            const string fromName = "Legato Network";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("Thank you for registering at Legato Network. To complete the sign up process, please verify your email address by clicking the link below.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Click this link to verify: ");
            sb.Append("http://legatonetwork.com/Account/Verify/");
            sb.Append(id.ToString());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("If you did not register for an account at Legato Network and believe you received this email in error, please ignore this message.");
            sb.Append(Environment.NewLine);
            sb.Append("- Legato Network :)");

            string body = sb.ToString();

            var emailmessage = new System.Web.Mail.MailMessage()
                                   {
                                       Subject = subject,
                                       Body = body,
                                       From = "\""+fromName+"\" <no-reply@legatonetwork.com>",
                                       To = emailAddress,
                                       BodyFormat = MailFormat.Text,
                                       Priority = MailPriority.Normal
                                   };

            SmtpMail.SmtpServer = "relay-hosting.secureserver.net";
            SmtpMail.Send(emailmessage);

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateChangeEmail(string newEmail, string confirmEmail)
        {
            if (String.IsNullOrEmpty(newEmail))
            {
                ModelState.AddModelError("NewEmail", "You must specify a new email address.");
            }
            else
            {
                try
                {
                    var a = new System.Net.Mail.MailAddress(newEmail);
                    a = null;

                    //check whether email is already taken
                    if(!string.IsNullOrEmpty(Membership.GetUserNameByEmail(newEmail)))
                    {
                        ModelState.AddModelError("NewEmail", "A user already exists with this email address.");
                    }
                }
                catch
                {
                    ModelState.AddModelError("NewEmail", "You must specify a valid email address.");
                }
            }
            if (String.IsNullOrEmpty(confirmEmail))
            {
                ModelState.AddModelError("ConfirmEmail", "You must enter the new email address a second time.");
            }
            else
            {
                try
                {
                    var a = new System.Net.Mail.MailAddress(confirmEmail);
                    a = null;
                }
                catch
                {
                    ModelState.AddModelError("ConfirmEmail", "You must specify a valid email address.");
                }
            }
            if (!String.Equals(newEmail, confirmEmail, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new email and confirmation email do not match.");
            }
            return ModelState.IsValid;
        }
        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateResetPassword(string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }
        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            try
            {
                //validate email
                var a = new System.Net.Mail.MailAddress(email);
                a = null;
            }
            catch
            {
                //if an exception occurred, the email is invalid
                ModelState.AddModelError("email", "You must specify a valid email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }



    }
}
