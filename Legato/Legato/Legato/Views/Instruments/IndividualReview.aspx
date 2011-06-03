<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoReview>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	IndividualReview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>IndividualReview</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">IsValid</div>
        <div class="display-field"><%= Html.Encode(Model.IsValid) %></div>
        
        <div class="display-label">PianoReviewID</div>
        <div class="display-field"><%= Html.Encode(Model.PianoReviewID) %></div>
        
        <div class="display-label">PianoListingID</div>
        <div class="display-field"><%= Html.Encode(Model.PianoListingID) %></div>
        
    </fieldset>
    <p>

        <%= Html.ActionLink("Edit", "Edit", new { id=Model.PianoReviewID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

