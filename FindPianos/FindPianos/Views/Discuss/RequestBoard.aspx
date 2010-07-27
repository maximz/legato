<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Request a new Board
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% if (!User.Identity.IsAuthenticated || User.IsInRole("EmailNotConfirmed") || !User.IsInRole("ActiveUser"))
   {%>
   <p>You must be logged in as a user that is not suspended and whose email has been confirmed to use this function. <%=Html.ActionLink("Click here to return to the home page.","Index", "Home")%></p>
<% }
   else
   {%>
	<h2>Request a new Board</h2>
	<p>If you would like to request the creation of a new board that isn't currently available in our system, please fill out the form below.</p>
	<% using (Html.BeginForm("RequestBoard","Discuss",FormMethod.Post))
	   {%>
	<%= Html.ValidationSummary(true) %>
		<div class="editor-label">
			<%= Html.Label("Board Name:" )%>
		</div>
		<div class="editor-field">
			<%= Html.TextBox("name",ViewData["name"].ToString())%>
			<%= Html.ValidationMessage("name") %>
		</div>
		<p>
			<input type="submit" value="Submit Request" />
		</p>
	<% } %>
<% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
