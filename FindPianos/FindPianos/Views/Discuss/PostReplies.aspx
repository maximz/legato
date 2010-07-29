<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FindPianos.Models.DiscussPost>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PostReplies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>PostReplies</h2>

	<table>
		<tr>
			<th></th>
			<th>
				PostID
			</th>
			<th>
				ThreadID
			</th>
			<th>
				PostNumberInThread
			</th>
			<th>
				DateOfSubmission
			</th>
		</tr>

	<% foreach (var item in Model) { %>
	
		<tr>
			<td>
				<%= Html.ActionLink("Edit", "Edit", new { id=item.PostID }) %> |
				<%= Html.ActionLink("Details", "Details", new { id=item.PostID })%> |
				<%= Html.ActionLink("Delete", "Delete", new { id=item.PostID })%>
			</td>
			<td>
				<%= Html.Encode(item.PostID) %>
			</td>
			<td>
				<%= Html.Encode(item.ThreadID) %>
			</td>
			<td>
				<%= Html.Encode(item.PostNumberInThread) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:g}", item.DateOfSubmission)) %>
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

