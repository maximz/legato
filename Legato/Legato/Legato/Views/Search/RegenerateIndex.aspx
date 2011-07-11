<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Regenerate Search Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Regenerate Search Index</h2>
	<p>To regenerate the search index, fill out the form below. <b>Warning: this is a <i>non-reversible operation</i> and will deactivate searching while it processes.</b> This function should only be used when some database changes have occurred that the index doesn't reflect.</p>
	<p>If you don't know whether or not you should do this, ask an administrator.</p>
	<% using (Html.BeginForm()) {%>
		<%= Html.ValidationSummary(true) %>

		<fieldset>
			<legend>Regenerate Index</legend>
			
			<div class="editor-label">
				CAPTCHA:
			</div>
			<div class="editor-field">
				<%=Html.GenerateCaptcha() %>
			</div>
			<p>
				<input type="submit" value="Regenerate!" />
			</p>
		</fieldset>

	<% } %>

</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

