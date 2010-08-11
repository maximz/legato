<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussReplyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reply
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Reply to "<%=Html.Encode(Model.ThreadName) %>"</h2>

	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Post.InReplyToPostID) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Post.InReplyToPostID, new { @readonly="readonly", @disabled="disabled" }) %>
				<%= Html.ValidationMessageFor(model => model.Post.InReplyToPostID) %>
			</div>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Post.Markdown) %>
	</div>
	<div class="editor-field">
		<%= Html.TextAreaFor(model => model.Post.Markdown) %>
		<%= Html.ValidationMessageFor(model => model.Post.Markdown) %>
	</div>
	<%=Html.HiddenFor(model=>model.ThreadID) %>
			
			<p>
				<input type="submit" value="Reply" />
			</p>

	<% } %>

	<div>
		<%= Html.ActionLink("Cancel and return to thread", "ReadThread", new { threadID = Model.ThreadID, slug = HtmlUtilities.URLFriendly(Model.ThreadName)})%>
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

