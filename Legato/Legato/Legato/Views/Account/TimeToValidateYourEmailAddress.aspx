<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Verify your email address
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Verify your email address</h2>

	<p>Thanks for registering!</p>
	<p>We have sent an email to <i><%=ViewData["email"]%></i> with a link to confirm your email address. Please click that link to be able to use all the site's functions.</p>
	<p>Didn't get the email? No problem - <%=Html.ActionLink("click here to resend the email","ResendVerificationEmail","Account")%>.</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
