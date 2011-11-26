// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Legato.Controllers {
    public partial class HomeController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

		[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
		protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result) {
			var callInfo = result.GetT4MVCResult();
			return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
		}

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ContactSubmit() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ContactSubmit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ComingSoon() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ComingSoon);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult PostRedirect() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.PostRedirect);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string About = "About";
            public readonly string Faq = "Faq";
            public readonly string UserContext = "UserContext";
            public readonly string Contact = "Contact";
            public readonly string ContactSubmit = "ContactSubmit";
            public readonly string ComingSoon = "ComingSoon";
            public readonly string PostRedirect = "PostRedirect";
            public readonly string BuildNum = "BuildNum";
            public readonly string ElevateMaximToAdmin = "ElevateMaximToAdmin";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string About = "~/Views/Home/About.cshtml";
            public readonly string ComingSoon = "~/Views/Home/ComingSoon.cshtml";
            public readonly string Contact = "~/Views/Home/Contact.cshtml";
            public readonly string Faq = "~/Views/Home/Faq.cshtml";
            public readonly string Index = "~/Views/Home/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_HomeController: Legato.Controllers.HomeController {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult About() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.About);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Faq() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Faq);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserContext() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UserContext);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Contact() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Contact);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ContactSubmit(Legato.ViewModels.ContactViewModel model, bool captchaValid) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ContactSubmit);
            callInfo.RouteValueDictionary.Add("model", model);
            callInfo.RouteValueDictionary.Add("captchaValid", captchaValid);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ComingSoon(string slug) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ComingSoon);
            callInfo.RouteValueDictionary.Add("slug", slug);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PostRedirect(int pid) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.PostRedirect);
            callInfo.RouteValueDictionary.Add("pid", pid);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult BuildNum() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.BuildNum);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ElevateMaximToAdmin() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ElevateMaximToAdmin);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
