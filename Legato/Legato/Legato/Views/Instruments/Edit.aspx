<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoReviewRevision>" %>

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
                <%= Html.LabelFor(model => model.PianoReviewRevisionID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PianoReviewRevisionID) %>
                <%= Html.ValidationMessageFor(model => model.PianoReviewRevisionID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PianoReviewID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PianoReviewID) %>
                <%= Html.ValidationMessageFor(model => model.PianoReviewID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PianoStyleID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PianoStyleID) %>
                <%= Html.ValidationMessageFor(model => model.PianoStyleID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Brand) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Brand) %>
                <%= Html.ValidationMessageFor(model => model.Brand) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Model) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Model) %>
                <%= Html.ValidationMessageFor(model => model.Model) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RatingOverall) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RatingOverall) %>
                <%= Html.ValidationMessageFor(model => model.RatingOverall) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RatingTuning) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RatingTuning) %>
                <%= Html.ValidationMessageFor(model => model.RatingTuning) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RatingToneQuality) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RatingToneQuality) %>
                <%= Html.ValidationMessageFor(model => model.RatingToneQuality) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RatingPlayingCapability) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RatingPlayingCapability) %>
                <%= Html.ValidationMessageFor(model => model.RatingPlayingCapability) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Message) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Message) %>
                <%= Html.ValidationMessageFor(model => model.Message) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PricePerHourInUSD) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PricePerHourInUSD, String.Format("{0:F}", Model.PricePerHourInUSD)) %>
                <%= Html.ValidationMessageFor(model => model.PricePerHourInUSD) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.VenueName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.VenueName) %>
                <%= Html.ValidationMessageFor(model => model.VenueName) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SubmitterUserID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SubmitterUserID) %>
                <%= Html.ValidationMessageFor(model => model.SubmitterUserID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DateOfRevision) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DateOfRevision, String.Format("{0:g}", Model.DateOfRevision)) %>
                <%= Html.ValidationMessageFor(model => model.DateOfRevision) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DateOfLastUsageOfPianoBySubmitter) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DateOfLastUsageOfPianoBySubmitter, String.Format("{0:g}", Model.DateOfLastUsageOfPianoBySubmitter)) %>
                <%= Html.ValidationMessageFor(model => model.DateOfLastUsageOfPianoBySubmitter) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RevisionNumberOfReview) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RevisionNumberOfReview) %>
                <%= Html.ValidationMessageFor(model => model.RevisionNumberOfReview) %>
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

