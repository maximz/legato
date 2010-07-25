<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.DiscussEditViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Edit</h2>

	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>
		
		<fieldset>
			<legend>Fields</legend>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.CanChangeLocation) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.CanChangeLocation) %>
				<%= Html.ValidationMessageFor(model => model.CanChangeLocation) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.PostID) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.PostID) %>
				<%= Html.ValidationMessageFor(model => model.PostID) %>
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
				<%= Html.TextBoxFor(model => model.Lat, String.Format("{0:F}", Model.Lat)) %>
				<%= Html.ValidationMessageFor(model => model.Lat) %>
			</div>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Long) %>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Long, String.Format("{0:F}", Model.Long)) %>
				<%= Html.ValidationMessageFor(model => model.Long) %>
			</div>
			
			<p>
				<input type="submit" value="Save" />
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

