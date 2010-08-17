<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.DiscussPost>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Post Revision Timeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Post Revision Timeline</h2>

	<table>
		<tr>
			<th></th>
			<th>
				PostRevisionID
			</th>
			<th>
				UserID
			</th>
			<th>
				PostID
			</th>
			<th>
				Markdown
			</th>
			<th>
				HTML
			</th>
			<th>
				DateOfEdit
			</th>
			<th>
				EditNumber
			</th>
			<th>
				InReplyToPostID
			</th>
		</tr>

	<% foreach (var item in Model.Revisions) { %>
	
		<tr>
			<td>
				<%= Html.ActionLink("Edit", "Edit", new { id=item.PostRevisionID }) %> |
				<%= Html.ActionLink("Details", "Details", new { id=item.PostRevisionID })%> |
				<%= Html.ActionLink("Delete", "Delete", new { id=item.PostRevisionID })%>
			</td>
			<td>
				<%= Html.Encode(item.PostRevisionID) %>
			</td>
			<td>
				<%= Html.Encode(item.UserID) %>
			</td>
			<td>
				<%= Html.Encode(item.PostID) %>
			</td>
			<td>
				<%= Html.Encode(item.Markdown) %>
			</td>
			<td>
				<%= Html.Encode(item.HTML) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:g}", item.DateOfEdit)) %>
			</td>
			<td>
				<%= Html.Encode(item.EditNumber) %>
			</td>
			<td>
				<%= Html.Encode(item.InReplyToPostID) %>
			</td>
		</tr>
	
	<% } %>

	</table>

	<p>
		<%= Html.ActionLink("Create New", "Create") %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

