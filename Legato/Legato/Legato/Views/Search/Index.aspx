<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Search!</h2>

	<%using(Html.BeginForm("IndexPost","Search")) { %>
		<p>Search for...</p>
		<input id="query" type="text" name="query" size="31" title="search query/term"/> 
		<!--<input id="tags" type="text" name="tags" size="31" title="in these tags (optional)" />-->
		<p><input type="submit" value="Search" /></p>
	<%} %>

</asp:Content>