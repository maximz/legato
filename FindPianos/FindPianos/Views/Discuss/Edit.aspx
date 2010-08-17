<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussEditViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Edit</h2>

	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Post.Markdown) %>
	</div>
	<div class="editor-field">
		<%= Html.TextAreaFor(model => model.Post.Markdown) %>
		<%= Html.ValidationMessageFor(model => model.Post.Markdown) %>
	</div>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Post.InReplyToPostID) %>
	</div>
	<div class="editor-field">
		<%= Html.TextAreaFor(model => model.Post.InReplyToPostID) %>
		<%= Html.ValidationMessageFor(model => model.Post.InReplyToPostID) %>
	</div>
	<% if (Model.CanChangeLocation)
	   { %>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Address) %>
	</div>
	<div class="editor-field">
		<%= Html.TextBoxFor(model => model.Address) %>
		<%= Html.ValidationMessageFor(model => model.Address) %>
	</div>
	<% } %>
	<%=Html.HiddenFor(model=>model.PostID) %>
	<p>
		<input type="submit" value="Edit" />
	</p>

	<% } %>
	<div>
		<%= Html.ActionLink("Cancel and return to post", "IndividualPostRedirect", new { postID = Model.PostID})%>
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

