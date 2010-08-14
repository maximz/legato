<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FindPianos.Models.DiscussThread>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["BoardName"].ToString().Trim() %> board
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="board-title">
		<p>
			<b>
				<%=ViewData["BoardName"].ToString().Trim() %></b> board</p></div>

	<div id="board-threads">
		<% foreach (var item in Model)
		   { %>
		<div class="board-thread">
			<div class="board-thread-title">
				<%=Html.ActionLink(Html.Encode(item.Title), "ReadThread", "Discuss", new
{
	threadID=item.ThreadID,
	page = 1,
	slug = FindPianos.Helpers.HtmlUtilities.URLFriendly(item.Title)
}) %>
			</div>
			<div class="board-thread-timestamps">
			Thread created: 
				<div class="timeago" title="<%= item.CreationDate.ToString("o") %>">
			</div>
				| latest activity:
				<div class="timeago" title="<%=item.LatestActivity.ToString("o") %>"></div>
			</div>
			<div class="board-thread-posts"><%=Html.Encode(item.NumberOfPosts) %> posts</div>
		</div>
		<% } %>
	</div>

	<div id="board-metadata">
	<div class="board-metadata-header">Board Information</div>
	<div id="threadcount">
	<div class="large-number"><%=ViewData["TotalPosts"].ToString()%></div>
	<div class="large-number-label"> threads</div>
	</div>
	</div>
	<div id="pagination"><% Html.RenderPartial("PageNumbers", ViewData["PageNumbers"]);%></div>

	<div id="createnew"><div class="boxbutton"><%=Html.ActionLink("Create new","Submit","Discuss",new { boardID = long.Parse(ViewData["BoardID"].ToString())})%></div></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

