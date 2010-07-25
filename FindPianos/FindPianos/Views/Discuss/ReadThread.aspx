<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussReadThreadViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReadThread
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>ReadThread</h2>

	<fieldset>
		<legend>Fields</legend>
		
		<div class="display-label">TotalPosts</div>
		<div class="display-field"><%= Html.Encode(Model.TotalPosts) %></div>
		
	</fieldset>
	<p>
		<%= Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
		<%= Html.ActionLink("Back to List", "Index") %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

