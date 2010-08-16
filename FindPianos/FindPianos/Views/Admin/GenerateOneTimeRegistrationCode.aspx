<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Invite a friend to join Legato Network!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Invite a friend to join Legato Network!</h2>
	<p>Use the form below to generate a one-time registration code.</p>
	<% using(Html.BeginForm("GenerateOneTimeRegistrationCode","Admin",FormMethod.Post))
	{ %>
		<fieldset>
		<legend>Invitation information</legend>
			<label for="CustomWelcomeName">Option custom welcome name: </label>
			<%=Html.TextBox("CustomWelcomeName") %>
			<p><input type="submit" value="Invite" /></p>
		</fieldset>
	<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
