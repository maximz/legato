<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_1">&nbsp;</div>
    <div class="grid_10"><h1 class="centerPageHeading">explore the future of music today</h1></div>
    <div class="grid_1">&nbsp;</div>
    <div class="clear">&nbsp;</div>

    <div class="grid_1">&nbsp;</div>
    <div class="grid_6"><p>Founded in 2010, the Legato Network is the premiere site for pianists around the world. Through our services, we offer a way of taking music to a new level.</p></div>
    <div class="grid_1">&nbsp;</div>
    <div class="grid_3">Some image</div>
    <div class="grid_1">&nbsp;</div>
    <div class="clear">&nbsp;</div>

    <div class="grid_2">&nbsp;</div>
    <div class="grid_5"><p>Through the Legato Network, you can: </p></div>
    <div class="grid_3">
        <ul class="VerticalListWithoutBullets">
            <li><%=Html.ActionLink("use a piano temporarily","Index","Listing") %></li>
            <li><%=Html.ActionLink("buy or sell a piano", "Index", "PianoTrade") %></li>
            <li><%=Html.ActionLink("browse or post lesson advertisements", "Index", "Lessons") %></li>
            <li><%=Html.ActionLink("find or review a store", "Index","Stores") %></li>
            <li><%=Html.ActionLink("interact with fellow musicians","Index","Discuss") %></li>
            <li>and more</li>
        </ul>
    </div>
    <div class="grid_2">&nbsp;</div>
    <div class="clear">&nbsp;</div>

    <div class="grid_1">&nbsp;</div>
    <div class="grid_5">MAP</div>
    <div class="grid_1">&nbsp;</div>
    <div class="grid_4">Map Options</div>
    <div class="grid_1">&nbsp;</div>
    <div class="clear">&nbsp;</div>

</asp:Content>
