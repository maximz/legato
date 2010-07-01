<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="centerPageHeading">explore the future of music today</h1>
    <p>
        Founded in 2010, the Legato Network is the premiere site for pianists around the world. Through our services, we offer a way of taking music to a new level.
    </p>
    <h2>[TODOLINKS]Find a piano for temporary use or for sale, browse lessons, locate a store, or join the discussion today!</h2>
    <div id="map"></div>
    <div id="mapoptions"></div>
</asp:Content>
