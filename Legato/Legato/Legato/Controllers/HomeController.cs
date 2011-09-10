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
    public partial class HomeController : Controller
    {
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        [Url("")]
        public virtual ActionResult Index()
        {
            ViewBag.PageType = "PresentationPage"; // see top of master layout page

            var dbTypes = (from t in Current.DB.InstrumentTypes
                           select new { Id = t.TypeID, Name = t.Name }).ToList();
            dbTypes.Add(new { Id = 0, Name = "All Instruments" });
            ViewBag.Types = new SelectList(dbTypes.ToArray(), "Id", "Name");

            return View();
        }

        [Url("About")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult About()
        {
            return View();
        }

        [Url("About/FAQ")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        public virtual ActionResult FAQ()
        {
            return View();
        }

        [Url("About/Contact")]
        [CustomCache(NoCachingForAuthenticatedUsers = true, Duration = 7200)]
        [HttpGet]
        public virtual ActionResult Contact()
        {
            var model = new ContactViewModel();
            return View(model);
        }

        [CaptchaValidator]
        [HttpPost]
        [Url("About/Contact")]
        public ActionResult Contact(ContactViewModel model, bool captchaValid)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if (!captchaValid)
            {
                ModelState.AddModelError("CAPTCHA", "You did not type the verification word (CAPTCHA) correctly.");
                return View(model);
            }

            try
            {
                SendEmailMessage(model.Message, model.Name, model.Email, "info@legatonetwork.com");
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("Email", "There was a problem sending your email. Please try again.");
                return View(model);
            }
        }

        public static void SendEmailMessage(string message, string name, string email, string to)
        {
            const string subject = "New contact message";
            const string fromName = "Legato Network";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello!");
            sb.Append(Environment.NewLine);
            sb.Append("A user has submitted a new contact message through the Contact page.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("<b>Name:</b> ");
            sb.Append(name);
            sb.Append(Environment.NewLine);
            
            sb.Append("<b>Email:</b> ");
            sb.Append(email);
            sb.Append(Environment.NewLine);
            
            sb.AppendLine("<b>Message:</b>");
            sb.AppendLine("<blockquote>"+message+"</blockquote>");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            
            sb.Append("<b>Submitted:</b> ");
            sb.AppendLine(DateTime.Now.ToLongDateString());
            sb.AppendLine(DateTime.Now.ToLongTimeString());
            sb.Append(Environment.NewLine);
            
            sb.Append(Environment.NewLine);
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
    }
}
