<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reset Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Reset password</h2>
	<p>
		Use the form below to pick a new password.
	</p>
	<p>
		New passwords are required to be a minimum of
		<%=Html.Encode(ViewData["PasswordLength"])%>
		characters in length.
	</p>
	<%= Html.ValidationSummary("Password change was unsuccessful. Please correct the errors and try again.")%>
	<% using (Html.BeginForm())
	   { %>
	<div>
		<fieldset>
			<legend>Account Information</legend>
			<p>
				<label for="newPassword">
					New password:</label>
				<%= Html.Password("newPassword") %>
				<%= Html.ValidationMessage("newPassword") %>
			</p>
			<p>
				<label for="confirmPassword">
					Confirm new password:</label>
				<%= Html.Password("confirmPassword") %>
				<%= Html.ValidationMessage("confirmPassword") %>
			</p>
			<p>
				<input type="submit" value="Reset Password" />
			</p>
		</fieldset>
	</div>
	<% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
