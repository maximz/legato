<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussCreateViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Submit to <i><%=Html.Encode(Model.BoardName) %></i> board</h2>

	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Title) %>
	</div>
	<div class="editor-field">
		<%= Html.TextBoxFor(model => model.Title) %>
		<%= Html.ValidationMessageFor(model => model.Title) %>
	</div>
	<div class="editor-label">
		<%= Html.LabelFor(model => model.Post.Markdown) %>
	</div>
	<div class="editor-field">
		<%= Html.TextAreaFor(model => model.Post.Markdown) %>
		<%= Html.ValidationMessageFor(model => model.Post.Markdown) %>
	</div>
	<% if(Model.CanSetLocation) { %>
	    <div class="editor-label">
		    <%= Html.LabelFor(model => model.Address) %>
	    </div>
	    <div class="editor-field">
		    <%= Html.TextBoxFor(model => model.Address) %>
		    <%= Html.ValidationMessageFor(model => model.Address) %>
	    </div>
	<% } %>
	<%=Html.HiddenFor(model=>model.BoardID) %>
	<p>
		<input type="submit" value="Submit" />
	</p>
	<% } %>
	<div>
		<%= Html.ActionLink("Cancel and return to board", "ReadBoard", new { boardID = Model.BoardID, slug = HtmlUtilities.URLFriendly(Model.BoardName)})%>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

