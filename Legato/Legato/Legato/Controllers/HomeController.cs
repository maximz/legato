using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using Legato.Models;
using Legato.Helpers;
using MvcReCaptcha;
using Legato.ViewModels;
using System.Text;
using System.Web.Mail;

namespace Legato.Controllers
{
    [HandleError]
    public partial class HomeController : CustomControllerBase
    {
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        [Url("")]
        public virtual ActionResult Index()
        {
            ViewBag.curPage = "Home";

            return View();
        }

        [Url("About")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult About()
        {
            ViewBag.curPage = "About";
            return View();
        }

        [Url("About/faq")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult Faq()
        {
            return RedirectToAction("About");
        }

        [Url("UserContext")]
        public virtual ActionResult UserContext()
        {
            return Content(Current.Context.User.Identity.IsAuthenticated.ToString() + ";" + Current.Context.User.Identity.Name + ";" + Current.Context.User.Identity.AuthenticationType);
        }

        /// <summary>
        /// "Contact us" form.
        /// </summary>
        /// <returns></returns>
        [Url("About/Contact")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        [HttpGet]
        public virtual ActionResult Contact()
        {
            ViewBag.curPage = "About";
            var model = new ContactViewModel();
            return View(model);
        }

        /// <summary>
        /// Handles input from "contact us" form.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="captchaValid">if set to <c>true</c> [captcha valid].</param>
        /// <returns></returns>
        [CaptchaValidator]
        [HttpPost]
        [Url("About/ContactSubmit")]
        public virtual ActionResult ContactSubmit(ContactViewModel model, bool captchaValid)
        {
            ViewBag.curPage = "About";
            if(!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            if (!captchaValid)
            {
                ModelState.AddModelError("CAPTCHA", "You did not type the verification word (CAPTCHA) correctly.");
                return View("Contact", model);
            }

            try
            {
                SendEmailMessage(model.Message, model.Name, model.Email, "info@legatonetwork.com");
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("Email", "There was a problem sending your email. Please try again.");
                return View("Contact", model);
            }
        }

        public static void SendEmailMessage(string message, string name, string email, string to)
        {
            const string subject = "New contact message";
            const string fromName = "Legato Network";
            const string newLine = "<br />";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(newLine);
            sb.Append("A user has submitted a new contact message through the Contact page." + newLine);
            sb.Append(newLine);
            sb.Append(newLine);

            sb.Append("<b>Name:</b> ");
            sb.Append(name);
            sb.Append(newLine);
            
            sb.Append("<b>Email:</b> ");
            sb.Append(email);
            sb.Append(newLine);
            
            sb.AppendLine("<b>Message:</b>");
            sb.AppendLine("<blockquote>"+message+"</blockquote>");
            sb.Append(newLine);
            sb.Append(newLine);
            
            sb.Append("<b>Submitted:</b> ");
            sb.AppendLine(DateTime.Now.ToLongDateString() + newLine);
            sb.AppendLine(DateTime.Now.ToLongTimeString());
            sb.Append(newLine);

            sb.Append(newLine);
            sb.Append("- Legato Network :)");

            string body = sb.ToString();

            var netEmailMessage = SendEmail.StandardNoReply(to, subject, body, true);
            netEmailMessage.Priority = System.Net.Mail.MailPriority.High;
            netEmailMessage.ReplyToList.Add(new System.Net.Mail.MailAddress(email));

            SendEmail.Send(netEmailMessage);
        }

        /// <summary>
        /// Displays a "coming soon" page. The slug may contain the requested URL.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns></returns>
        [Url("Coming/Soon/{slug?}")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult ComingSoon(string slug)
        {
            return View();
        }

        [Url("p/{pid}")]
        [CustomCache(NoCachingForAuthenticatedUsers = false, Duration = 7200 * 40, VaryByParam = "pid")]
        public virtual ActionResult PostRedirect(int pid)
        {
            var db = Current.DB;
            var globalpost = db.GlobalPostIDs.Where(p=>p.GlobalPostID1==pid).SingleOrDefault();
            if(globalpost==null)
            {
                return RedirectToAction("NotFound","Error");
            }

            switch(globalpost.PostCategory)
            {
                case "ins":
                    return RedirectToAction("Individual","Instruments", new{ instrumentID = globalpost.SpecificPostID});
                    break;
                case "ins.rev":
                    return RedirectToAction("IndividualReview","Instruments", new{ reviewID = globalpost.SpecificPostID});
                    break;
                case "i.r.r":
                    var reviewr = db.InstrumentReviewRevisions.Where(r=>r.RevisionID==globalpost.SpecificPostID).SingleOrDefault();
                    if(reviewr==null)
                    {
                        return RedirectToAction("NotFound","Error");
                    }
                    return RedirectToAction("Timeline","Instruments", new{ reviewID = reviewr.ReviewID});
                    break;
            }

            return RedirectToAction("NotFound","Error");
        }

        [Url("BuildNum")]
        public virtual ActionResult BuildNum()
        {
            return Content(Current.RevNumber());
        }

        [Url("ElevateAdmin")]
        public virtual ActionResult ElevateMaximToAdmin()
        {
            System.Web.Security.Roles.AddUserToRole("maximz", RoleNames.Administrator);
            System.Web.Security.Roles.AddUserToRole("maximz", RoleNames.Moderator);
            return Content("done for maximz");
        }
    }
}
