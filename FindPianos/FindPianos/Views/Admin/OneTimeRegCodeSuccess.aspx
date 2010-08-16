<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Guid>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Invitation code success!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Invitation code generated successfully!</h2>
	<p>Your friend should navigate to the following link in order to register at Legato Network:</p>
	<p><b><%=Html.ActionLink(Url.Action("Login", "Account", new { OneTimeSignupCode = Model }), "Login", "Account", new { OneTimeSignupCode = Model})%></b></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
