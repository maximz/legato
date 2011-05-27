<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Legato.Models.UserSuspension>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Suspension Status
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Your account is currently suspended.</h2>

	<fieldset>
		<legend>Suspension Details</legend>
		
		<div class="display-label">SuspensionDate</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.SuspensionDate)) %></div>

		<div class="display-label">Reason</div>
		<div class="display-field"><%= Html.Encode(Model.Reason) %></div>
		
		<div class="display-label">ReinstateDate</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.ReinstateDate)) %></div>
	</fieldset>

</asp:Content>