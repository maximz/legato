<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Delete Discussion Thread or Post</h2>
	<p>Use the form below to delete a post or thread. <b>Note: there is <i>NO UNDO!</i></b></p>

	<% using(Html.BeginForm("Delete","Discuss",FormMethod.Post)) {%>
	<%= Html.ValidationSummary(true) %>
	<div class="editor-label">
		<%= Html.Label("Selected post:" )%>
	</div>
	<div class="editor-field">
	<input type="text" name="postID" readonly="readonly" value="<%=ViewData["PostID"] as string %>"/>
		<%= Html.ValidationMessage("postID") %>
	</div>
	<div class="editor-bool">
	<p>Do you want to delete the whole thread or just one post?</p>
	<input type="radio" name="expungeThread" title="Delete Thread" value="true"/>Delete Thread<br />
	<input type="radio" name="expungeThread" title="Delete Post" value="false"/>Delete Post
	</div>
	<%=Html.Hidden("hiddenVerification",null) %>
	<%=Html.Hidden("hiddenPostNumber",ViewData["PostNumberInThread"].ToString()) %>
	<p>
		<input type="submit" value="Delete" />
	</p>
	<% } %>
	<div>
		<%= Html.ActionLink("Cancel and return to post", "IndividualPostRedirect", new { postID = ViewData["PostID"]})%>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
