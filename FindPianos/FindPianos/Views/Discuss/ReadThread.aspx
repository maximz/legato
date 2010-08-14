<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussReadThreadViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.Encode(Model.Thread.Title) %> - <%= Html.Encode(Model.BoardName) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div id="thread-title">
		<p>
			<b><%=Html.Encode(Model.Thread.Title)%></b> - <%=Html.ActionLink(Html.Encode(Model.BoardName),"ReadBoard","Discuss",new{boardID=Model.BoardID,slug=FindPianos.Helpers.HtmlUtilities.URLFriendly(Model.BoardName),page=1}) %> board</p>
	</div>
	<div id="thread-posts">
		<% foreach (var item in Model.Posts)
		   { %>
		<div class="thread-post">
			<div class="thread-post-text">
			<%=Html.Encode(item.Revisions[0].HTML)%>
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
			<!-- TODO: user partial view, figure out how to do jQuery call for Flag to show the flag form and bind jquery.form to it! -->
			<div class="thread-post-functions">
			<div class="thread-post-functions-link"><%=Html.ActionLink("link","IndividualPostRedirect","Discuss",new { postID = item.PostID })%></div>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("timeline","PostTimeline","Discuss",new { postID = item.PostID }) %></div>
				<% if (User.Identity.IsAuthenticated && !User.IsInRole("EmailNotConfirmed") && User.IsInRole("ActiveUser")) { %>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("flag","Flag","Discuss",new { postID = item.PostID}) %></div> <%} %>
			<% if (User.Identity.IsAuthenticated && (User.IsInRole("Moderator") || User.IsInRole("Admin")))
			   {
			   %>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("delete", "Delete", "Discuss", new
{
	postID = item.PostID
})%></div>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("edit", "Edit", "Discuss", new
{
	postID = item.PostID
})%></div><% } %>
			<% if (item.ReplyCount>0)
	  { %>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("replies (" + item.ReplyCount+")", "PostReplies", "Discuss", new { postID = item.PostID })%></div>
			<% } %>
			| <div class="thread-post-functions-link"><%=Html.ActionLink("Reply to this post","Reply","Discuss",new { threadID = Model.Thread.ThreadID, postID = item.PostID})%></div>
			</div>
		</div>
		<% } %>
	</div>
	<div id="thread-metadata">
		<div class="thread-metadata-header">
			Thread Information</div>
		<div id="postcount">
			<div class="large-number">
				<%=Model.TotalPosts%></div>
			<div class="large-number-label">
				posts</div>
		</div>
	</div>
	<div id="pagination">
		<% Html.RenderPartial("PageNumbers", Model.PageNumbers);%></div>
	<div id="reply">
		<div class="boxbutton">
			<%=Html.ActionLink("Reply","Reply","Discuss",new { threadID = Model.Thread.ThreadID})%></div>
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

