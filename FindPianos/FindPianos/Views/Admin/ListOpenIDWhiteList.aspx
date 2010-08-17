<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<FindPianos.Models.OpenIDWhiteList>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	OpenID Whitelist
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>OpenID Whitelist</h2>

	<table>
		<tr>
			<th></th>
			<th>
				ID
			</th>
			<th>
				OpenID
			</th>
			<th>
				IsEnabled
			</th>
		</tr>

	<% foreach (var item in Model) { %>
	
		<tr>
			<td>
				<%= Html.ActionLink("Toggle status", "ToggleEnabledDisabledFromOpenIDWhiteList", new { OpenIDClaim=item.OpenID })%> |
				<%= Html.ActionLink("Remove", "RemoveFromOpenIDWhiteList", new { OpenIDClaim=item.OpenID })%>
			</td>
			<td>
				<%= Html.Encode(item.ID) %>
			</td>
			<td>
				<%= Html.Encode(item.OpenID) %>
			</td>
			<td>
				<%= Html.Encode(item.IsEnabled) %>
			</td>
		</tr>
	
	<% } %>

	</table>

	<p>
		<% using(Html.BeginForm("AddToOpenIDWhiteList","Admin",FormMethod.Post)) { %>
			<label for="OpenIDClaim">Add OpenID to whitelist: </label>
			<%=Html.TextBox("OpenIDClaim") %>
			<p><input type="submit" value="Add!" /></p>
		<% } %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

