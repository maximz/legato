<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.DiscussPost>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Post Revision Timeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Post Revision Timeline</h2>
	<div id="revisions-list">
	<% foreach (var item in Model.Revisions) { %>
		<div class="revision">
			<div class="revision-user">
				Revised by <%= Html.Encode(item.UserID) %>
			</div>
			<div class="revision-html">
				<%= item.HTML %>
			</div>
			<div class="revision-date">
				Revision created on <%= Html.Encode(String.Format("{0:g}", item.DateOfEdit)) %>
			</div>
			<div class="revision-number">
				Edit #<%= Html.Encode(item.EditNumber) %>
			</div>
			<% if(item.InReplyToPostID.HasValue) { %>
			<div class="revision-replyto">
				In reply to <%=Html.ActionLink("post #"+ Html.Encode(item.InReplyToPostID),"IndividualPostRedirect","Discuss",new { postID=item.InReplyToPostID })%>
			</div>
			<% } %>
		</div>
	<% } %>
	</div>

	<p>
		<%= Html.ActionLink("Return to post", "IndividualPostRedirect","Discuss",new { postID = Model.PostID}) %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

