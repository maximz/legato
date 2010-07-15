<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.LatLong>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Log Out
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Log Out</h2>
	<form action="Account/LogOff" method="post">         
		<p>
			<input type="submit" value="Log Out" />
		</p>
	</form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
</asp:Content>

