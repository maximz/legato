<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Forgot Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Forgot Password</h2>
	<p>
		Use the form below to begin resetting your password.
	</p>
	<%= Html.ValidationSummary("Your input was invalid. Please correct any errors and try again.")%>
	<% using (Html.BeginForm())
	   { %>
	<div>
		<fieldset>
			<legend>Account Information</legend>
			<p>
				<label for="username">
					Your username:</label>
				<%= Html.TextBox("username") %>
				<%= Html.ValidationMessage("username") %>
			</p>
			<p>
				<input type="submit" value="Submit" />
			</p>
		</fieldset>
	</div>
	<% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
