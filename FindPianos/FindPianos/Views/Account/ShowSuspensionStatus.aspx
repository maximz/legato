<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoUserSuspension>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowSuspensionStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowSuspensionStatus</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">SuspensionID</div>
        <div class="display-field"><%= Html.Encode(Model.SuspensionID) %></div>
        
        <div class="display-label">UserID</div>
        <div class="display-field"><%= Html.Encode(Model.UserID) %></div>
        
        <div class="display-label">Reason</div>
        <div class="display-field"><%= Html.Encode(Model.Reason) %></div>
        
        <div class="display-label">SuspensionDate</div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.SuspensionDate)) %></div>
        
        <div class="display-label">ReinstateDate</div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.ReinstateDate)) %></div>
        
        <div class="display-label">IsValid</div>
        <div class="display-field"><%= Html.Encode(Model.IsValid) %></div>
        
    </fieldset>
    <p>

        <%= Html.ActionLink("Edit", "Edit", new { id=Model.SuspensionID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

