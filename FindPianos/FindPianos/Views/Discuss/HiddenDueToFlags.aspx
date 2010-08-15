<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Post Not Available
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>This post is not available</h2>
	<p>Members of our community have flagged this post. It is <b>currently under review</b> by our moderators. We apologize for any inconvenience.</p>
	<h3>What to do now:</h3>
	<p>We recommend that you either:</p>
	<ul>
		<li><a href="javascript:history.go(-1)">Return to the last page you visited</a></li>
		<li><%=Html.ActionLink("Go to the Legato Network home page","Index","Home")%></li>
	</ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
