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
    public partial class InstrumentsController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public InstrumentsController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected InstrumentsController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult Map() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Map);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult PossibleInstrumentTypes() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.PossibleInstrumentTypes);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Individual() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Individual);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult IndividualReview() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.IndividualReview);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Timeline() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Timeline);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Review() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Review);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult EditReview() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.EditReview);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult EditListing() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.EditListing);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UserLinksForListing() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UserLinksForListing);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UserLinksForReview() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UserLinksForReview);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public InstrumentsController Actions { get { return MVC.Instruments; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Instruments";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Map = "Map";
            public readonly string PossibleInstrumentTypes = "PossibleInstrumentTypes";
            public readonly string Individual = "Individual";
            public readonly string IndividualReview = "IndividualReview";
            public readonly string Timeline = "Timeline";
            public readonly string Submit = "Submit";
            public readonly string Review = "Review";
            public readonly string EditReview = "EditReview";
            public readonly string EditListing = "EditListing";
            public readonly string UserLinksForListing = "UserLinksForListing";
            public readonly string UserLinksForReview = "UserLinksForReview";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string AlreadyReviewed = "~/Views/Instruments/AlreadyReviewed.cshtml";
            public readonly string Edit = "~/Views/Instruments/Edit.aspx";
            public readonly string EditListing = "~/Views/Instruments/EditListing.cshtml";
            public readonly string EditReview = "~/Views/Instruments/EditReview.cshtml";
            public readonly string IndividualReview = "~/Views/Instruments/IndividualReview.aspx";
            public readonly string List = "~/Views/Instruments/List.aspx";
            public readonly string Listing = "~/Views/Instruments/Listing.cshtml";
            public readonly string Map = "~/Views/Instruments/Map.cshtml";
            public readonly string Read = "~/Views/Instruments/Read.aspx";
            public readonly string Review = "~/Views/Instruments/Review.cshtml";
            public readonly string ReviewTimeline = "~/Views/Instruments/ReviewTimeline.aspx";
            public readonly string Submit = "~/Views/Instruments/Submit.cshtml";
            public readonly string Timeline = "~/Views/Instruments/Timeline.cshtml";
            public readonly string UserLinksForListing = "~/Views/Instruments/UserLinksForListing.cshtml";
            public readonly string UserLinksForReview = "~/Views/Instruments/UserLinksForReview.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_InstrumentsController: Legato.Controllers.InstrumentsController {
        public T4MVC_InstrumentsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Map(string classIns) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Map);
            callInfo.RouteValueDictionary.Add("classIns", classIns);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PossibleInstrumentTypes(string classIns) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.PossibleInstrumentTypes);
            callInfo.RouteValueDictionary.Add("classIns", classIns);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Individual(int instrumentID, string slug) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Individual);
            callInfo.RouteValueDictionary.Add("instrumentID", instrumentID);
            callInfo.RouteValueDictionary.Add("slug", slug);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult IndividualReview(int reviewID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.IndividualReview);
            callInfo.RouteValueDictionary.Add("reviewID", reviewID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Timeline(int reviewID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Timeline);
            callInfo.RouteValueDictionary.Add("reviewID", reviewID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Submit() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Submit);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Submit(Legato.ViewModels.SubmitViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Submit);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Review(int instrumentID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Review);
            callInfo.RouteValueDictionary.Add("instrumentID", instrumentID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Review(Legato.ViewModels.ReviewCreateViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Review);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditReview(int reviewID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditReview);
            callInfo.RouteValueDictionary.Add("reviewID", reviewID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditReview(Legato.ViewModels.EditReviewViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditReview);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditListing(int instrumentID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditListing);
            callInfo.RouteValueDictionary.Add("instrumentID", instrumentID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditListing(Legato.ViewModels.EditListingViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditListing);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserLinksForListing(int instrumentID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UserLinksForListing);
            callInfo.RouteValueDictionary.Add("instrumentID", instrumentID);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserLinksForReview(int reviewID) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UserLinksForReview);
            callInfo.RouteValueDictionary.Add("reviewID", reviewID);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
