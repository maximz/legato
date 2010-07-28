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
	<div id="thread-title">
		<p>
			<b><%=Html.Encode(Model.Thread.Title)%></b> - <%=Html.ActionLink(Model.BoardName,"ReadBoard","Discuss",new{boardID=Model.BoardID,slug=FindPianos.Helpers.HtmlUtilities.URLFriendly(Model.BoardName),page=1}) %> board</p>
	</div>
	<div id="thread-posts">
		<% foreach (var item in Model.Posts)
		   { %>
		<div class="thread-post">
			<div class="thread-post-text">
			<%=item.Revisions[0].HTML%>
			</div>
			<div class="thread-post-timestamps">
				Post created:
				<div class="timeago" title="<%= item.DateOfSubmission.ToString("o") %>">
				</div>
				<% if (item.Revisions.Count > 1)
				   { %>
				| latest revision:
				<div class="timeago" title="<%=item.Revisions[0].RevisionDate.ToString("o") %>">
				</div>
				<% } %>
			</div>
			<!-- TODO: user partial view, links -->
			<div class="thread-post-functions">
			<div class="thread-post-functions-link"><%=Html.ActionLink("link","IndividualPostRedirect","Discuss",new { postID = item.PostID })%></div>
			| <div class="thread-post-functions-link">flag</div>
			| <div class="thread-post-functions-link">flag</div>
			| <div class="thread-post-functions-link">delete</div>
			</div>
			<div class="board-thread-title">
				<%=Html.ActionLink(Html.Encode(item.Title), "ReadThread", "Discuss", new
{
	threadID=item.ThreadID,
	page = 1,
	slug = FindPianos.Helpers.HtmlUtilities.URLFriendly(item.Title)
}) %>
			</div>
			<div class="board-thread-timestamps">

			</div>
			<div class="board-thread-posts">
				<%=Html.Encode(item.NumberOfPosts) %>
				posts</div>
		</div>
		<% } %>
	</div>
	<div id="board-metadata">
		<div class="board-metadata-header">
			Board Information</div>
		<div id="threadcount">
			<div class="large-number">
				<%=ViewData["TotalPosts"].ToString()%></div>
			<div class="large-number-label">
				threads</div>
		</div>
	</div>
	<div id="pagination">
		<% Html.RenderPartial("PageNumbers", ViewData["PageNumbers"]);%></div>
	<div id="createnew">
		<div class="boxbutton">
			<%=Html.ActionLink("Create new","Submit","Discuss",new { boardID = long.Parse(ViewData["BoardID"].ToString())})%></div>
	</div>
	<p>
		<%= Html.ActionLink("Create New", "Create") %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

