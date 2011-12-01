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
using Legato.Models;
using RiaLibrary.Web;
using Legato.Helpers;
using System.Text;
using System.Web.Mail;
using Legato.ViewModels;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Net;
using System.Configuration;

namespace Legato.Controllers
{

    /// <summary>
    /// Handles account methods.
    /// </summary>
    [HandleError]
    public partial class AccountController : CustomControllerBase
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

        [Url("Account/Login/{OneTimeSignupCode?}")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public virtual ActionResult Login(Guid? OneTimeSignupCode, string ReturnUrl)
        {
            if (OneTimeSignupCode.HasValue)
            {
                var db = Current.DB;
                var record = db.OneTimeRegistrationCodes.Where(s => s.Id == OneTimeSignupCode.Value).SingleOrDefault();
                if (record == null || record.UsesRemaining < 1)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                ViewData["OneTimeSignupCode"] = OneTimeSignupCode.Value.ToString();
                if (record.CustomWelcomeName.HasValue())
                {
                    ViewData["WelcomeName"] = record.CustomWelcomeName.Trim();
                }
            }
            if(ReturnUrl.HasValue() && Url.IsLocalUrl(ReturnUrl))
            {
                Session["ReturnURL"] = ReturnUrl;
            }
            return View("OpenidLogin");
        }

        [Url("Account/Authenticate")]
        [ValidateInput(false)]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public virtual ActionResult Authenticate(string returnUrl)
        {
            var db = Current.DB;
            if (Request.Form["OneTimeSignupCode"].HasValue())
            {
                Session["OneTimeSignupCode"] = Request.Form["OneTimeSignupCode"];
            }
            IAuthenticationResponse response = openid.GetResponse();
            OneTimeRegistrationCode recordcopy = null;
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;

                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    if (WhiteListEnabled)
                    {
                        if (Request.Form["OneTimeSignupCode"].HasValue())
                        {
                            var record = db.OneTimeRegistrationCodes.Where(c => c.Id.ToString() == Request.Form["OneTimeSignupCode"]).SingleOrDefault();
                            if (record == null)
                            {
                                //not allowed in
                                Current.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                return View("WhiteListBlock");
                            }
                        }
                    }
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
                            if (Request.Form["OneTimeSignupCode"].HasValue())
                            {
                                ViewData["OneTimeSignupCode"] = Request.Form["OneTimeSignupCode"];
                            }
                            return View("OpenidLogin");
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Invalid OpenID";
                        if (Request.Form["OneTimeSignupCode"].HasValue())
                        {
                            ViewData["OneTimeSignupCode"] = Request.Form["OneTimeSignupCode"];
                        }
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
                            openId = db.UserOpenIds.Where(o => o.OpenIdClaim == response.ClaimedIdentifier.OriginalString).FirstOrDefault();
                            object signupcode = null;
                            if (Request.Form["OneTimeSignupCode"].HasValue())
                            {
                                signupcode = Request.Form["OneTimeSignupCode"];
                            }
                            else if (Session["OneTimeSignupCode"] != null)
                            {
                                signupcode = Session["OneTimeSignupCode"];
                            }
                            if (WhiteListEnabled)
                            {
                                if (signupcode != null)
                                {
                                    var record = db.OneTimeRegistrationCodes.Where(c => c.Id.ToString() == (string)signupcode).SingleOrDefault();
                                    if (record == null)
                                    {
                                        //not allowed in
                                        try
                                        {
                                            Current.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                        }
                                        catch
                                        {

                                        }
                                        return View("WhiteListBlock");
                                    }
                                    recordcopy = record;
                                    --record.UsesRemaining;
                                    if (record.UsesRemaining < 1)
                                    {
                                        db.OneTimeRegistrationCodes.DeleteOnSubmit(record);
                                    }
                                    db.SubmitChanges();
                                }
                                //else if (db.OpenIDWhiteLists.Where(w => w.IsEnabled).Where(w => w.OpenID == response.ClaimedIdentifier.OriginalString).FirstOrDefault() == null && (sreg == null || !sreg.Email.Contains("APPROVEDOPENIDDOMAIN.com") && openId == null))
                                else if ((db.OpenIDWhiteLists.Where(w => w.IsEnabled).Where(w => w.OpenID == response.ClaimedIdentifier.OriginalString).FirstOrDefault() == null || sreg == null)  && openId == null) // if (not-in-whitelisted-openids or no-openid-submitted) and doesn't-match-any-openid-in-the-system
                                {
                                    //not allowed in
                                    try
                                    {
                                        Current.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                    }
                                    catch
                                    {

                                    }
                                    return View("WhiteListBlock");
                                }
                            }


                            if (openId == null)
                            {
                                // create new user
                                string email = "";
                                string login = "";
                                if (sreg != null)
                                {
                                    email = sreg.Email;
                                    var userNameAvailable = (db.aspnet_Users.Where(u => u.UserName == sreg.Nickname).FirstOrDefault()) == null;
                                    if (userNameAvailable)
                                    {
                                        login = sreg.Nickname;
                                    }
                                }
                                var model = new OpenIdRegistrationViewModel()
                                {
                                    EmailAddress = email,
                                    Nickname = login,
                                    OpenIdClaim = Crypto.EncryptStringAES(response.ClaimedIdentifier.OriginalString, "OpenIDRegistrationFrenzy"),
                                    ReturnURL = Session["ReturnURL"] as string
                                };
                                return View("OpenidRegister", model);
                            }
                            else
                            {
                                //check whether user is suspended and whether suspension has already ended
                                var userName = openId.aspnet_User.UserName;
                                
                                if (!Roles.IsUserInRole(userName, RoleNames.ActiveUser))
                                {
                                    var currentProfile = AccountProfile.GetProfileOfUser(userName);
                                    if (DateTime.Now >= currentProfile.ReinstateDate)
                                    {
                                        Roles.AddUserToRole(userName, RoleNames.ActiveUser);
                                        currentProfile.ReinstateDate = DateTime.MinValue;
                                        currentProfile.Save();
                                    }
                                }
                                FormsAuthentication.SetAuthCookie(userName, true);
                                var URLreturn = Session["ReturnURL"];
                                if (URLreturn == null || !(URLreturn as string).HasValue())
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                return Redirect(URLreturn as string);
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

        /// <summary>
        /// Handles OpenID registration form submission.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="captchaValid">if set to <c>true</c> [captcha valid].</param>
        /// <returns></returns>
        [Url("Account/Register/OpenID")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        [HttpPost]
        [VerifyReferrer]
        [CaptchaValidator]
        [ValidateAntiForgeryToken]
        public virtual ActionResult OpenidRegisterFormSubmit(OpenIdRegistrationViewModel model, bool captchaValid)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("CAPTCHA", "It seems that you did not type the verification word(s) (CAPTCHA) correctly. Please try again.");
                return View("OpenidRegister", model);
            }
            if (!ModelState.IsValid)
            {
                return View("OpenidRegister", model);
            }

            var DecryptedOpenID = Crypto.DecryptStringAES(model.OpenIdClaim, "OpenIDRegistrationFrenzy");
            var validator = new IsSemiValidURLAttribute();
            var isValid = validator.IsValid(DecryptedOpenID);
            validator = null;
            if (!isValid)
            {
                //User tried to spoof encryption
                ModelState.AddModelError("OpenID", "There's a problem with the OpenID that you specified.");
                return View("OpenidRegister", model);
            }

            try
            {
                var db = Current.DB;
                var userNameAvailable = (db.aspnet_Users.Where(u => u.UserName == model.Nickname).FirstOrDefault()) == null;
                if (!userNameAvailable)
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                    return View("OpenidRegister", model);
                }

                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Nickname, Membership.GeneratePassword(7, 0), model.EmailAddress);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRoles(model.Nickname, new string[] { RoleNames.ActiveUser, RoleNames.EmailNotConfirmed });
                    AccountProfile.NewUser.Initialize(model.Nickname, true);
                    AccountProfile.NewUser.ReinstateDate = DateTime.MinValue;
                    AccountProfile.NewUser.FullName = model.FullName.Trim();
                    AccountProfile.NewUser.AboutMe = (model.AboutMe.IsNullOrEmpty() ? null : HtmlUtilities.Safe(HtmlUtilities.Sanitize(model.AboutMe.Trim())));
                    AccountProfile.NewUser.Save();
                    try
                    {
                        //Check OpenID-whitelist status and add OpenID to whitelist if needed
                        if (WhiteListEnabled)
                        {
                            //If we got here, this means that the user used a valid one-time registration code.
                            var whitelistRecord = new OpenIDWhiteList();
                            whitelistRecord.OpenID = DecryptedOpenID;
                            whitelistRecord.IsEnabled = true;
                            db.OpenIDWhiteLists.InsertOnSubmit(whitelistRecord);
                            db.SubmitChanges();
                        }

                        var userid = db.aspnet_Users.Where(u => u.UserName == model.Nickname).Single().UserId; // if we fail here, this usually means that we didn't specify a constant ApplicationName in Web.config, so each user has multiple entries in that table.

                        var openid = new UserOpenId();
                        openid.OpenIdClaim = DecryptedOpenID;
                        openid.UserId = userid;
                        db.UserOpenIds.InsertOnSubmit(openid);
                        db.SubmitChanges();

                        var confirm = new ConfirmEmailAddress();
                        confirm.UserID = userid;
                        confirm.ConfirmID = Guid.NewGuid();
                        db.ConfirmEmailAddresses.InsertOnSubmit(confirm);
                        db.SubmitChanges();

                        SendEmailVerificationEmail(model.EmailAddress, confirm.ConfirmID);


                        FormsAuth.SignIn(model.Nickname, true /* createPersistentCookie */);

                        if (ConfigurationManager.AppSettings["PromptEmailConfirmation"] == "true")
                        {
                            ViewData["email"] = model.EmailAddress;
                            return View("TimeToValidateYourEmailAddress");
                        }
                        else
                        {
                            if(model.ReturnURL.HasValue())
                            {
                                return Redirect(model.ReturnURL);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    catch
                    {
                        ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                        return View("OpenidRegister", model);
                    }
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                    return View("OpenidRegister", model);
                }
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion

        #region Logout

        /// <summary>
        /// Handles logoff.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Url("Account/LogOff")]
        public virtual ActionResult LogOff()
        {
            //separated into Get and Post to prevent attacks - see http://meta.stackoverflow.com/questions/57159/stack-overflow-wmd-editor-anti-csrf/57160#57160
            return View();
        }
        [HttpPost]
        [VerifyReferrer]
        [Authorize]
        [Url("Account/LogOutPOST")]
        public virtual ActionResult LogOut()
        {
            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Suspended Users
        /// <summary>
        /// Shows the suspension status of a user.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization(UnauthorizedRoles = RoleNames.ActiveUser)]
        [Url("Account/Status/Suspended")]
        public virtual ActionResult ShowSuspensionStatus()
        {
            var u = Membership.GetUser();
            var db = Current.DB;
            if (!(AccountProfile.GetProfileOfUser(u.UserName).ReinstateDate < DateTime.Now))
            {
                var suspension = db.UserSuspensions.Where(s => s.UserID == (Guid)u.ProviderUserKey).OrderByDescending(k => k.ReinstateDate).Take(1).ToList()[0];
                return View(suspension);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Email Verification
        /// <summary>
        /// Shows the email address verification status.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
        [Url("Account/Status/NotVerified")]
        public virtual ActionResult ShowEmailAddressVerificationStatus()
        {
            if (!Current.Context.User.IsInRole(RoleNames.EmailNotConfirmed))
            {
                return RedirectToAction("Index", "Home");
            }
            return View("TimeToValidateYourEmailAddress");
        }
        /// <summary>
        /// Resends the verification email.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
        [Url("Account/VerifyEmail/Resend")]
        public virtual ActionResult ResendVerificationEmail()
        {
            if (!Current.Context.User.IsInRole(RoleNames.EmailNotConfirmed))
            {
                return RedirectToAction("Index", "Home");
            }
            var db = Current.DB;
            var user = Membership.GetUser();
            ConfirmEmailAddress confirm;
            try
            {
                confirm = db.ConfirmEmailAddresses.Where(a => a.UserID == (Guid)user.ProviderUserKey).Single();

            }
            catch
            {
                Roles.RemoveUserFromRole(User.Identity.Name, RoleNames.EmailNotConfirmed);
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
        /// <summary>
        /// Verifies the email address.
        /// </summary>
        /// <param name="confirmId">The confirm id.</param>
        /// <returns></returns>
        [CustomAuthorization(AuthorizeEmailNotConfirmed = true, AuthorizeSuspended = true)]
        [Url("Account/Verify/{confirmId}")]
        public virtual ActionResult VerifyEmailAddress(Guid confirmId)
        {
            if (!User.IsInRole(RoleNames.EmailNotConfirmed))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var db = Current.DB;
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
                Roles.RemoveUserFromRole(User.Identity.Name, RoleNames.EmailNotConfirmed);
                return View("VerifyEmailAddressSuccess");
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        #endregion

        #region Forgot OpenID
        /// <summary>
        /// Retrieves the OpenID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Url("Account/Recover/OpenID")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public virtual ActionResult ForgotOpenID()
        {
            return View();
        }

        /// <summary>
        /// Retrieves the username.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [HttpPost]
        [VerifyReferrer]
        [Url("Account/Recover/OpenID")]
        [CustomAuthorization(OnlyAllowUnauthenticatedUsers = true)]
        public virtual ActionResult ForgotOpenID(string email)
        {
            //validate that email is a valid email address
            try
            {
                var e = new System.Net.Mail.MailAddress(email);
                e = null;
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
            var db = Current.DB;
            var oId = db.UserOpenIds.Where(o => o.UserId == (Guid)Membership.GetUser(result, false).ProviderUserKey).FirstOrDefault();
            SendForgotOpenIDEmail(email, oId.OpenIdClaim);
            oId = null;
            ViewData["email"] = email;
            return View("SentForgotOpenidEmail");
        }
        #endregion

        #region Profile Edit
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization]
        [HttpGet]
        [Url("Account/Options/Password")]
        public virtual ActionResult ChangePassword()
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
        [VerifyReferrer]
        [Url("Account/Options/Password")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public virtual ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
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
        public virtual ActionResult ChangeEmail()
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
        [VerifyReferrer]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in email not being changed.")]
        public virtual ActionResult ChangeEmail(string NewEmail, string ConfirmEmail)
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
        public virtual ActionResult MyProfile()
        {
            ViewData["MainInfo"] = Membership.GetUser();
            ViewData["IsEmailNotConfirmed"] = User.IsInRole(RoleNames.EmailNotConfirmed);
            ViewData["IsSuspended"] = !User.IsInRole(RoleNames.ActiveUser);
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

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("You have selected to reset your password. To do so, please follow the link below.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Click this link to reset your password: ");
            sb.Append(ActionLinkExtensions.BuildURLFromRoot("/Account/Options/ResetPassword/"));
            sb.Append(id.ToString());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("If you did NOT select to reset your password at Legato Network and believe you received this email in error, please ignore this message.");
            sb.Append(Environment.NewLine);
            sb.Append("- Legato Network :)");



            string body = sb.ToString();

            var netmessage = SendEmail.StandardNoReply(emailAddress, subject, body, false);
            SendEmail.Send(netmessage);
        }
        /// <summary>
        /// Sends email verification email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="id">The id.</param>
        internal void SendEmailVerificationEmail(string emailAddress, Guid id)
        {
            const string subject = "Verify your email address - Legato Network";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("Thank you for registering at Legato Network. To complete the sign up process, please verify your email address by clicking the link below.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Click this link to verify: ");
            sb.Append(ActionLinkExtensions.BuildURLFromRoot("/Account/Verify/" + id.ToString()));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("If you did not register for an account at Legato Network and believe you received this email in error, please ignore this message.");
            sb.Append(Environment.NewLine);
            sb.Append("- Legato Network :)");

            string body = sb.ToString();

            var netmessage = SendEmail.StandardNoReply(emailAddress, subject, body, false);
            SendEmail.Send(netmessage);
        }
        /// <summary>
        /// Sends email verification email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="id">The id.</param>
        internal void SendForgotOpenIDEmail(string emailAddress, string openID)
        {
            const string subject = "OpenID retrieval - Legato Network";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("Someone requested the OpenID belonging to this email address at the Legato Network.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("The OpenID belonging to the user with this email address is: ");
            sb.Append(openID);
            sb.Append(Environment.NewLine);
            sb.Append("To log in, go to: "+ActionLinkExtensions.BuildURLFromRoot("/Account/Login"));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("If you did not register for an account at Legato Network and believe you received this email in error, please ignore this message.");
            sb.Append(Environment.NewLine);
            sb.Append("- Legato Network :)");

            string body = sb.ToString();

            var netmessage = SendEmail.StandardNoReply(emailAddress, subject, body, false);
            SendEmail.Send(netmessage);

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
                    if (!string.IsNullOrEmpty(Membership.GetUserNameByEmail(newEmail)))
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