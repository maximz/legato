<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussCreateViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Submit</h2>

	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>

		<fieldset>
			<legend>Fields</legend>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.BoardID) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.BoardID) %>
				<%= Html.ValidationMessageFor(model => model.BoardID) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Title) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Title) %>
				<%= Html.ValidationMessageFor(model => model.Title) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Address) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Address) %>
				<%= Html.ValidationMessageFor(model => model.Address) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Lat) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Lat) %>
				<%= Html.ValidationMessageFor(model => model.Lat) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Long) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Long) %>
				<%= Html.ValidationMessageFor(model => model.Long) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.CanSetLocation) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.CanSetLocation) %>
				<%= Html.ValidationMessageFor(model => model.CanSetLocation) %>
			</div>
			
			<p>
				<input type="submit" value="Create" />
			</p>
		</fieldset>

	<% } %>

	<div>
		<%= Html.ActionLink("Back to List", "Index") %>
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

