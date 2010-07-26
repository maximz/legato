<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Recover OpenID
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Recover OpenID</h2>
	<p>If you don't recall what OpenID your account uses, you can find out by entering your email address below. We will then send you the OpenID information for the account belonging to that email address.</p>
	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>

		<fieldset>
			<legend>Recovery</legend>
			
			<div class="editor-label">
				<%= Html.Label("Email address:" )%>
			</div>
			<div class="editor-field">
				<%= Html.TextBox("email")%>
				<%= Html.ValidationMessage("email") %>
			</div>
			
			<p>
				<input type="submit" value="Create" />
			</p>
		</fieldset>

	<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

