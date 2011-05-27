<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Your Profile</h2>

	<fieldset>
		<legend>Basic Account Information</legend>
				
		<div class="display-label">Username</div>
		<div class="display-field"><%= Html.Encode(((MembershipUser)ViewData["MainInfo"]).UserName) %></div>
		
		<div class="display-label">Password</div>
		<div class="display-field"><%= Html.ActionLink("Change your password", "ChangePassword", "Account")%></div>

		<div class="display-label">Email address</div>
		<div class="display-field">
			<%= ((MembershipUser)ViewData["MainInfo"]).Email %> - <%=Html.ActionLink("change your email address", "ChangeEmail", "Account")%>
			<br />
			<% if ((bool)ViewData["IsEmailNotConfirmed"])
			   { %>
			   <b>email not verified</b> - <%=Html.ActionLink("resend verification email", "ResendVerificationEmail", "Account") %>
			   <% } %>
		</div>

		<div class="display-label">
			Account standing</div>
		<div class="display-field">
			<% if ((bool)ViewData["IsSuspended"])
			   { %>
			<p style="color: Red">
				<b>Suspended</b> -
				<%=Html.ActionLink("view details", "ShowSuspensionStatus", "Account")%></p>
			<% } %>
			<% else
			   { %>
			<p style="color: Green">
				In Good Standing</p>
			<% } %>
		</div>

		<legend>Account Activity (Creation date, last activity date)</legend>

		<div class="display-label">Account was created</div>
		<div class="display-field timeago"><%= Html.Encode(((MembershipUser)ViewData["MainInfo"]).CreationDate.ToString("o")) %></div>

		<div class="display-label">Latest activity</div>
		<div class="display-field timeago"><%= Html.Encode(((MembershipUser)ViewData["MainInfo"]).LastActivityDate.ToString("o"))%></div>

		<div class="display-label">Last login</div>
		<div class="display-field timeago"><%= Html.Encode(((MembershipUser)ViewData["MainInfo"]).LastLoginDate.ToString("o")) %></div>

		<div class="display-label">LastPasswordChangedDate</div>
		<div class="display-field timeago"><%= Html.Encode(((MembershipUser)ViewData["MainInfo"]).LastPasswordChangedDate.ToString("o")) %></div>

	</fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

