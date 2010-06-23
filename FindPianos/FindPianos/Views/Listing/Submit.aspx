<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoListing>" %>

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
                <%= Html.LabelFor(model => model.PianoID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PianoID) %>
                <%= Html.ValidationMessageFor(model => model.PianoID) %>
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
                <%= Html.LabelFor(model => model.StreetAddress) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.StreetAddress) %>
                <%= Html.ValidationMessageFor(model => model.StreetAddress) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.OriginalSubmitterUserID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.OriginalSubmitterUserID) %>
                <%= Html.ValidationMessageFor(model => model.OriginalSubmitterUserID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DateOfSubmission) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DateOfSubmission) %>
                <%= Html.ValidationMessageFor(model => model.DateOfSubmission) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.AverageOverallRating) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.AverageOverallRating) %>
                <%= Html.ValidationMessageFor(model => model.AverageOverallRating) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.LatestReviewRevisionDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.LatestReviewRevisionDate) %>
                <%= Html.ValidationMessageFor(model => model.LatestReviewRevisionDate) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.LatestUseOfPianoDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.LatestUseOfPianoDate) %>
                <%= Html.ValidationMessageFor(model => model.LatestUseOfPianoDate) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.NumberOfReviews) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.NumberOfReviews) %>
                <%= Html.ValidationMessageFor(model => model.NumberOfReviews) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.AveragePricePerHourInUSD) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.AveragePricePerHourInUSD) %>
                <%= Html.ValidationMessageFor(model => model.AveragePricePerHourInUSD) %>
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

