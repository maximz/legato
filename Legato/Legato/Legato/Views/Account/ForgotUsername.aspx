<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Forgot Username
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Retrieve your username</h2>
	<p>
		Use the form below to retrieve your username.
	</p>
	<%= Html.ValidationSummary("Your input was invalid. Please correct any errors and try again.")%>
	<% using (Html.BeginForm())
	   { %>
	<div>
		<fieldset>
			<legend>Account Information</legend>
			<p>
				<label for="email">
					Your email address:</label>
				<%= Html.TextBox("email") %>
				<%= Html.ValidationMessage("email") %>
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
