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
using FindPianos.ViewModels;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace FindPianos.Controllers
{

    /// <summary>
    /// Handles account methods.
    /// </summary>
    [HandleError]
    public class AccountController : Controller
    {
        private static readonly OpenIdRelyingParty openid = new OpenIdRelyingParty();

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

        #region OpenID Login and Registration

        [Url("Account/Login")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult Login()
        {
            return View("OpenidLogin");
        }

        [Url("Account/Authenticate")]
        [ValidateInput(false)]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult Authenticate(string returnUrl)
        {
            IAuthenticationResponse response = openid.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        IAuthenticationRequest request = openid.CreateRequest(Request.Form["openid_identifier"]);

                        request.AddExtension(new ClaimsRequest
                                                 {
                                                     Email = DemandLevel.Require,
                                                     Nickname = DemandLevel.Request,
                                                     FullName = DemandLevel.Request,
                                                     BirthDate = DemandLevel.Request
                                                 });

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewData["Message"] = ex.Message;
                        return View("OpenidLogin");
                    }
                }
                else
                {
                    ViewData["Message"] = "Invalid OpenID";
                    return View("OpenidLogin");
                }
            }
            else
            {
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var sreg = response.GetExtension<ClaimsResponse>();
                        UserOpenId openId = null;
                        using (var db = new LegatoDataContext())
                        {
                            openId = db.UserOpenIds.Where(o => o.OpenIdClaim == response.ClaimedIdentifier.OriginalString).FirstOrDefault();
                        }
                        
                        if (openId == null)
                        {
                            // create new user
                            string email = "";
                            string login = "";
                            if (sreg != null)
                            {
                                email = sreg.Email;
                                using (var db = new LegatoDataContext())
                                {
                                    var userNameAvailable = (db.aspnet_Users.Where(u => u.UserName == sreg.Nickname).FirstOrDefault()) == null;
                                    if (userNameAvailable)
                                    {
                                        login = sreg.Nickname;
                                    }
                                }
                            }
                            var model = new OpenIdRegistrationViewModel()
                            {
                                EmailAddress=email,
                                Username=login,
                                OpenIdClaim=response.ClaimedIdentifier
                            };
                            return View("OpenidRegisterForm", model);
                        }
                        else
                        {
                            //check whether user is suspended and whether suspension has already ended
                            var userName = openId.aspnet_User.UserName;
                            if (!Roles.IsUserInRole(userName, "ActiveUser"))
                            {
                                var currentProfile = AccountProfile.GetProfileOfUser(userName);
                                if (DateTime.Now >= currentProfile.ReinstateDate)
                                {
                                    Roles.AddUserToRole(userName, "ActiveUser");
                                    currentProfile.ReinstateDate = DateTime.MinValue;
                                    currentProfile.Save();
                                }
                            }
                            FormsAuthentication.RedirectFromLoginPage(userName, true);
                            return new EmptyResult();
                        }

                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("OpenidLogin");
                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("OpenidLogin");
                }
            }
            return new EmptyResult();
        }
        [Url("Account/Register/OpenID")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers=true)]
        [HttpPost]
        public ActionResult OpenidRegisterFormSubmit(OpenIdRegistrationViewModel model)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var userNameAvailable = (db.aspnet_Users.Where(u => u.UserName == model.Username).FirstOrDefault()) == null;
                    if (!userNameAvailable)
                    {
                        ModelState.AddModelError("Username", "This username is already taken.");
                        return View("OpenidRegisterForm", model);
                    }
                }
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Username, Membership.GeneratePassword(7, 0), model.EmailAddress);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRoles(model.Username, new string[] { "ActiveUser", "EmailNotConfirmed" });
                    AccountProfile.NewUser.Initialize(model.Username, true);
                    AccountProfile.NewUser.ReinstateDate = DateTime.MinValue;
                    AccountProfile.NewUser.Save();
                    using (var db = new LegatoDataContext())
                    {
                        try
                        {
                            var confirm = new ConfirmEmailAddress();
                            confirm.UserID = db.aspnet_Users.Where(u => u.UserName == model.Username).Single().UserId;
                            confirm.ConfirmID = Guid.NewGuid();
                            db.ConfirmEmailAddresses.InsertOnSubmit(confirm);
                            db.SubmitChanges();
                            SendEmailVerificationEmail(model.EmailAddress, confirm.ConfirmID);
                        }
                        catch
                        {
                            ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                            return View();
                        }
                    }
                    FormsAuth.SignIn(model.Username, true /* createPersistentCookie */);
                    ViewData["email"] = model.EmailAddress;
                    return View("TimeToValidateYourEmailAddress");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                    return View("OpenidRegisterForm", model);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion

        #region Login and Logout
        /// <summary>
        /// Handles login.
        /// </summary>
        /// <returns></returns>
        [HttpGet][CustomAuthorization(OnlyAllowUnauthenticatedUsers=true)]
        public ActionResult LogOn()
        {

            return View();
        }

        /// <summary>
        /// Handles login.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">if set to <c>true</c> the user's session does not end when the browser is closed.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Handles logoff.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Url("Account/LogOff")]
        public ActionResult LogOff()
        {
            //separated into Get and Post to prevent attacks - see http://meta.stackoverflow.com/questions/57159/stack-overflow-wmd-editor-anti-csrf/57160#57160
            return View();
        }
        [HttpPost]
        [Authorize]
        [Url("Account/LogOutPOST")]
        public ActionResult LogOut()
        {
            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <param name="captchaValid">if set to <c>true</c> the CAPTCHA is valid.</param>
        /// <returns></returns>
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
                
                if (createStatus == MembershipCreateStatus.Success)
                {
                    //Success! Now, we add metadata...
                    Roles.AddUserToRoles(userName, new string[] { "ActiveUser", "EmailNotConfirmed" });
                    AccountProfile.NewUser.Initialize(userName, true);
                    AccountProfile.NewUser.ReinstateDate = DateTime.MinValue;
                    AccountProfile.NewUser.Save();
                    using (var db = new LegatoDataContext())
                    {
                        try
                        {
                            var confirm = new ConfirmEmailAddress();
                            confirm.UserID = db.aspnet_Users.Where(u => u.UserName == userName).Single().UserId;
                            confirm.ConfirmID = Guid.NewGuid();
                            db.ConfirmEmailAddresses.InsertOnSubmit(confirm);
                            db.SubmitChanges();
                            SendEmailVerificationEmail(email, confirm.ConfirmID);
                        }
                        catch
                        {
                            ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                            return View();
                        }
                    }

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
        #endregion

        #region Suspended Users
        /// <summary>
        /// Shows the suspension status of a user.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization(UnauthorizedRoles="ActiveUser")]
        [Url("Account/Status/Suspended")]
        public ActionResult ShowSuspensionStatus()
        {
            var u = Membership.GetUser();
            using (var db = new LegatoDataContext())
            {
                if (!(AccountProfile.GetProfileOfUser(u.UserName).ReinstateDate < DateTime.Now))
                {
                    ViewData["suspension"] = db.UserSuspensions.Where(s => s.UserID == (Guid)u.ProviderUserKey).OrderByDescending(k => k.ReinstateDate).Take(1).ToList()[0];
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Email Verification
        /// <summary>
        /// Shows the email address verification status.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization]
        [Url("Account/Status/NotVerified")]
        public ActionResult ShowEmailAddressVerificationStatus()
        {
            if(!User.IsInRole("EmailNotConfirmed"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View("TimeToValidateYourEmailAddress");
        }
        /// <summary>
        /// Resends the verification email.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization]
        [Url("Account/VerifyEmail/Resend")]
        public ActionResult ResendVerificationEmail()
        {
            if(!User.IsInRole("EmailNotConfirmed"))
            {
                return RedirectToAction("Index", "Home");
            }
            using(var db = new LegatoDataContext())
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
                    SendEmailVerificationEmail(user.Email, confirm.ConfirmID);
                }
                catch
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
                return View("TimeToValidateYourEmailAddress");
            }
        }
        /// <summary>
        /// Verifies the email address.
        /// </summary>
        /// <param name="confirmId">The confirm id.</param>
        /// <returns></returns>
        [CustomAuthorization]
        [Url("Account/Verify/{confirmId}")]
        public ActionResult VerifyEmailAddress(Guid confirmId)
        {
            if (!User.IsInRole("EmailNotConfirmed"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var confirm = db.ConfirmEmailAddresses.Where(a => a.ConfirmID == confirmId).SingleOrDefault();
                    if (confirm == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    if ((Guid)Membership.GetUser(false).ProviderUserKey != confirm.UserID)
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
        #endregion

        #region Forgot Username or Password
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetId">The reset id.</param>
        /// <returns></returns>
        [HttpGet]
        [Url("Account/Options/ResetPassword/{resetId}")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ResetPassword(Guid resetId)
        {
            try
            {
                using (var db = new LegatoDataContext())
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
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetId">The reset id.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
        [HttpPost]
        [CaptchaValidator]
        [Url("Account/Options/ResetPassword")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ResetPassword(Guid resetId, string newPassword, string confirmPassword, bool captchaValid)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!captchaValid)
            {
                ModelState.AddModelError("CAPTCHA", "It seems that you did not type the verification word(s) (CAPTCHA) correctly. Please try again.");
                return View();
            }
            if (!ValidateResetPassword(newPassword, confirmPassword))
            {
                return View();
            }
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var reset = db.ResetPasswordRecords.Where(a => a.ResetID == resetId).SingleOrDefault();
                    if (reset == null)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    var u = Membership.GetUser(reset.UserID, false);
                    u.ChangePassword(u.GetPassword(), newPassword);
                    Membership.UpdateUser(u);
                    db.ResetPasswordRecords.DeleteOnSubmit(reset);
                    db.SubmitChanges();
                    FormsAuth.SignIn(u.UserName, false);
                    u = null;
                    return RedirectToAction("Index","Home");
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        /// <summary>
        /// Retrieves the username.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Url("Account/Recover/Username")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotUsername()
        {
            return View();
        }

        /// <summary>
        /// Retrieves the username.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
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
            if (string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("email", "We weren't able to find a user with this email address. Please make sure that the address is correct.");
                return View();
            }
            ViewData["result"] = result;
            return View("ForgotUsernameResult");
        }
        /// <summary>
        /// Allows user to reset their password if they forgot it.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Url("Account/Recover/Password")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Allows user to reset their password if they forgot it.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpPost]
        [Url("Account/Recover/Password")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public ActionResult ForgotPassword(string username)
        {
            try
            {
                using (var db = new LegatoDataContext())
                {
                    var r = new ResetPasswordRecord();
                    var u = Membership.GetUser(username, false);
                    if (u == null)
                    {
                        throw new ApplicationException();
                    }
                    r.UserID = (Guid)u.ProviderUserKey;
                    r.ResetID = Guid.NewGuid();
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
        }
        #endregion

        #region Profile Edit
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization][HttpGet][Url("Account/Options/Password")]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
        [CustomAuthorization]
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

        /// <summary>
        /// Changes the email.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization]
        [Url("Account/Options/Email")]
        [HttpGet]
        public ActionResult ChangeEmail()
        {
            ViewData["current"] = Membership.GetUser().Email;
            return View();
        }

        /// <summary>
        /// Changes the email.
        /// </summary>
        /// <param name="NewEmail">The new email.</param>
        /// <param name="ConfirmEmail">The confirm email.</param>
        /// <returns></returns>
        [CustomAuthorization]
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

        /// <summary>
        /// Displays a My Profile page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorization]
        [Url("Account/Profile")]
        public ActionResult MyProfile()
        {
            ViewData["MainInfo"] = Membership.GetUser();
            ViewData["IsEmailNotConfirmed"] = User.IsInRole("EmailNotConfirmed");
            ViewData["IsSuspended"] = !User.IsInRole("ActiveUser");
            return View();
        }
        #endregion

        #region Email Sending Methods
        /// <summary>
        /// Sends password reset emails.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="id">The id.</param>
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
            sb.Append("http://legatonetwork.com/Account/Options/ResetPassword/");
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
        /// <summary>
        /// Sends email verification email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="id">The id.</param>
        internal void SendEmailVerificationEmail(string emailAddress, Guid id)
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
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        /// <summary>
        /// Validates the change of email.
        /// </summary>
        /// <param name="newEmail">The new email.</param>
        /// <param name="confirmEmail">The confirm email.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Validates the change of password.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Validates the reset change of password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Validates the log on.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Validates the registration.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
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
