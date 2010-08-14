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
            <%=item.Revisions[0].HTML.TruncateWithEllipsis(400)%> <!-- TODO: bug when ending in the middle of an html tag see http://stackoverflow.com/questions/1714764/c-truncate-html-safely-for-article-summary-->
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
    </div>
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
        <!-- TODO: user partial view, figure out how to do jQuery call for Flag to show the flag form and bind jquery.form to it! -->
        <div class="thread-post-functions">
            <div class="thread-post-functions-link">
                <%=Html.ActionLink("link","IndividualPostRedirect","Discuss",new { postID = item.PostID })%></div>
            |
            <div class="thread-post-functions-link">
                <%=Html.ActionLink("timeline","PostTimeline","Discuss",new { postID = item.PostID }) %></div>
            |
            <div class="thread-post-functions-link">
                flag</div>
            |
            <div class="thread-post-functions-link">
                <%=Html.ActionLink("delete","Delete","Discuss",new { postID = item.PostID})%></div>
            <% if (item.ReplyCount > 0)
               { %>
            |
            <div class="thread-post-functions-link">
                <%=Html.ActionLink("replies (" + item.ReplyCount+")", "PostReplies", "Discuss", new { postID = item.PostID })%></div>
            <% } %>
        </div>
        <div class="thread-post-flag">
            <% if (User.Identity.IsAuthenticated && !User.IsInRole("EmailNotConfirmed") && User.IsInRole("ActiveUser"))
               {
                   using (Html.BeginForm("AjaxFlagPost", "Discuss", FormMethod.Post, new { @id = "flag-form-" + item.PostID }))
                   { %>
            <%=Html.Hidden("idOfPost", item.PostID)%>
            <h2>
                Flag</h2>
            <input type="radio" id="flag-<%=item.PostID %>-1" name="flagTypeId" value="1" />
            <label for="flag-<%=item.PostID %>-1">
                Spam</label>
            <br />
            <input type="radio" id="flag-<%=item.PostID %>-2" name="flagTypeId" value="2" />
            <label for="flag-<%=item.PostID %>-2">
                Offensive</label>
            <br />
            <input type="radio" id="flag-<%=item.PostID %>-3" name="flagTypeId" value="3" />
            <label for="flag-<%=item.PostID %>-3">
                Needs serious improvement</label>
            <br />
            <input type="submit" value="Flag" />
            <% }
      }
               else
               { %>
            <p>
                You must be logged in as a user who is not suspended and who has confirmed their
                email address to flag a post.</p>
            <% } %>
        </div>
    </div>
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

