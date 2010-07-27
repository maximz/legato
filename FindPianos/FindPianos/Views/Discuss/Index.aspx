<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Discussion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Select board:</h2>
	<p>To participate in a board, start typing its name in the box below:</p>
	<% using (Html.BeginForm("FindBoardByName","Discuss",FormMethod.Post))
	   {%>
	<%= Html.ValidationSummary(true) %>
		<div class="editor-label">
			<label for="boards">
				Boards:
			</label>
		</div>
		<div class="editor-field">
			<%= Html.TextBox("boards")%>
			<%= Html.ValidationMessage("boards") %>
		</div>
		<p>
			<input type="submit" value="Browse!" />
		</p>
	<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript">$(function () {
		var cache = {};
		$("#boards").autocomplete({
			minLength: 2,
			source: function (request, response) {
				if (request.term in cache) {
					response(cache[request.term]);
					return;
				}

				$.ajax({
					url: "Discuss/Boards/List/all",
					dataType: "json",
					type: "POST",
					data: request,
					success: function (data) {
						cache[request.term] = data;
						response(data);
					}
				});
			}
		});
	});</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>
