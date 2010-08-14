<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<long>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Flag Post
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Flag Post</h2>
	<p>If you want to flag this post, please fill out the form below.</p>
	<%	using (Html.BeginForm("AjaxFlagPost", "Discuss", FormMethod.Post))
	{ %>
	<%=Html.Hidden("idOfPost", Model)%>
	<p>Flag reason:</p>
	<input type="radio" id="flag-<%=Model%>-1" name="flagTypeId" value="1"/>
	<label for="flag-<%=Model%>-1">Spam</label>
	<br />
	<input type="radio" id="flag-<%=item.PostID %>-2" name="flagTypeId" value="2"/>
	<label for="flag-<%=Model%>-2">
		Offensive</label>
	<br />
	<input type="radio" id="flag-<%=item.PostID %>-3" name="flagTypeId" value="3"/>
	<label for="flag-<%=Model%>-3">
		Needs serious improvement</label>
	<br />
		<input type="submit" value="Flag" />
	<% } %>
	<div>
		<%= Html.ActionLink("Cancel and return to post", "IndividualPostRedirect", new { postID = Model})%>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
