<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Bad Request
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Sorry, but your browser seems to have submitted a bad request.</h2>
	<p>Here's what we recommend you do now:</p>
	<ul>
	<li><a href="javascript:history.go(-1)">Return to the last page you visited</a></li>
	<li><%=Html.ActionLink("Go to the home page","Index","Home")%></li>
	</ul>
	<p>Sorry for any inconvenience!</p>

</asp:Content>
