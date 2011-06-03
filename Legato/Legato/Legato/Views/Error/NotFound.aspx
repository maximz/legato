<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Page Not Found
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Sorry, but we couldn't find the page you were looking for.</h2>
	<p>It looks like this was the result of either:</p>
	   <ul>
		   <li>a mistyped address</li>
		   <li>an out-of-date link</li>
	   </ul>
	<p>Here's what we recommend you do now:</p>
	<ul>
	<li><a href="javascript:history.go(-1)">Return to the last page you visited</a></li>
	<li><%=Html.ActionLink("Go to the home page","Index","Home")%></li>
	</ul>
	<p>Sorry for any inconvenience!</p>

	<script>
		var GOOG_FIXURL_LANG = (navigator.language || '').slice(0, 2),
		GOOG_FIXURL_SITE = location.host;
	</script>
	<script src="http://linkhelp.clients.google.com/tbproxy/lh/wm/fixurl.js"></script>

</asp:Content>
