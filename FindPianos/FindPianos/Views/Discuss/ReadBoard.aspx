<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FindPianos.Models.DiscussThread>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReadBoard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>ReadBoard</h2>

	<table>
		<tr>
			<th></th>
			<th>
				ThreadID
			</th>
			<th>
				BoardID
			</th>
			<th>
				Title
			</th>
			<th>
				CreationDate
			</th>
			<th>
				Latitude
			</th>
			<th>
				Longitude
			</th>
			<th>
				Address
			</th>
			<th>
				LatestActivity
			</th>
			<th>
				NumberOfPosts
			</th>
		</tr>

	<% foreach (var item in Model) { %>
	
		<tr>
			<td>
				<%= Html.ActionLink("Edit", "Edit", new { id=item.ThreadID }) %> |
				<%= Html.ActionLink("Details", "Details", new { id=item.ThreadID })%> |
				<%= Html.ActionLink("Delete", "Delete", new { id=item.ThreadID })%>
			</td>
			<td>
				<%= Html.Encode(item.ThreadID) %>
			</td>
			<td>
				<%= Html.Encode(item.BoardID) %>
			</td>
			<td>
				<%= Html.Encode(item.Title) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:g}", item.CreationDate)) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:F}", item.Latitude)) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:F}", item.Longitude)) %>
			</td>
			<td>
				<%= Html.Encode(item.Address) %>
			</td>
			<td>
				<%= Html.Encode(String.Format("{0:g}", item.LatestActivity)) %>
			</td>
			<td>
				<%= Html.Encode(item.NumberOfPosts) %>
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

