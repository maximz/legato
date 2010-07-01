<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Error
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Sorry, but it seems that something bad happened.</h2>
	<p>Here's what we recommend you do now:</p>
	<ul>
		<li><a href="javascript:history.go(-1)">Return to the last page you visited</a></li>
		<li><%=Html.ActionLink("Go to the Legato Network home page","Index","Home")%></li>
	</ul>
	<p>Sorry for any inconvenience! :)</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
