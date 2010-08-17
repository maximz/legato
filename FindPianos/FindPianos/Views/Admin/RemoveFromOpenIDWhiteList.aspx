<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Remove from OpenID whitelist?
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Are you sure you want to remove this OpenID from the whitelist?</h2>
	<%using(Html.BeginForm("RemoveFromOpenIDWhiteList","Admin",FormMethod.Post)) { %>
		<%=Html.Hidden("confirm") %>
		<%=Html.HiddenFor(m=>m) %>
		<input type="submit" value="I'm sure!" />
	<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
