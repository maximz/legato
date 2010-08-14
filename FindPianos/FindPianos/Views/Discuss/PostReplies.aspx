<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<FindPianos.Models.DiscussPost>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Replies to Post by <%=ViewData["OriginalPostAuthor"] as string%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Replies to Post by
		<%=ViewData["OriginalPostAuthor"] as string%></h2>

	<% foreach (var item in Model) { %>
	<div class="reply-post">
		<div class="reply-post-text">
			<%=Html.Encode(item.Revisions[0].HTML.ConvertHtmlIntoText().TruncateWithEllipsis(400))%> <!-- http://stackoverflow.com/questions/3485238/safely-truncate-html-using-html-agility-pack TODO: bug when ending in the middle of an html tag see http://stackoverflow.com/questions/1714764/c-truncate-html-safely-for-article-summary-->
		</div>
		<div class="reply-post-timestamps">
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
		<div>
			<%=Html.ActionLink("Read this post", "IndividualPostRedirect","Discuss",new {postID=item.PostID})%>
		</div>
	</div>
	<% } %>
	<div>
		<%= Html.ActionLink("Return to post", "IndividualPostRedirect", new { postID = ViewData["OriginalPostID"]})%>
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

