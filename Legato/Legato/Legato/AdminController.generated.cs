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
    public partial class AdminController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AdminController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AdminController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult SetMessage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SetMessage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SetWhitelist() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SetWhitelist);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeletePost() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.DeletePost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UserSearchByName() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UserSearchByName);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetGuidFromUsername() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetGuidFromUsername);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetUserById() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetUserById);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetEmailList() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetEmailList);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AdminController Actions { get { return MVC.Admin; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Admin";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string ToggleMessage = "ToggleMessage";
            public readonly string SetMessage = "SetMessage";
            public readonly string ToggleWhitelist = "ToggleWhitelist";
            public readonly string GetWhitelist = "GetWhitelist";
            public readonly string SetWhitelist = "SetWhitelist";
            public readonly string DeletePost = "DeletePost";
            public readonly string UserSearchByName = "UserSearchByName";
            public readonly string GetGuidFromUsername = "GetGuidFromUsername";
            public readonly string GetUserById = "GetUserById";
            public readonly string SuspendUser = "SuspendUser";
            public readonly string GetEmailList = "GetEmailList";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string GetUserById = "~/Views/Admin/GetUserById.cshtml";
            public readonly string GetWhitelist = "~/Views/Admin/GetWhitelist.cshtml";
            public readonly string Index = "~/Views/Admin/Index.cshtml";
            public readonly string SuspendUser = "~/Views/Admin/SuspendUser.cshtml";
            public readonly string UserSearchByName = "~/Views/Admin/UserSearchByName.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AdminController: Legato.Controllers.AdminController {
        public T4MVC_AdminController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ToggleMessage() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ToggleMessage);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SetMessage(string message) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SetMessage);
            callInfo.RouteValueDictionary.Add("message", message);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ToggleWhitelist() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ToggleWhitelist);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetWhitelist() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetWhitelist);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SetWhitelist(string list) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SetWhitelist);
            callInfo.RouteValueDictionary.Add("list", list);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeletePost(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DeletePost);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserSearchByName(string nameContains) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UserSearchByName);
            callInfo.RouteValueDictionary.Add("nameContains", nameContains);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetGuidFromUsername(string username) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetGuidFromUsername);
            callInfo.RouteValueDictionary.Add("username", username);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetUserById(System.Guid UserId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetUserById);
            callInfo.RouteValueDictionary.Add("UserId", UserId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SuspendUser() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SuspendUser);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SuspendUser(Legato.Controllers.AdminController.SuspendUserViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SuspendUser);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetEmailList(string delimiter) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetEmailList);
            callInfo.RouteValueDictionary.Add("delimiter", delimiter);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
