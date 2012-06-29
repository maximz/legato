<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoListing>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Read
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Read</h2>

	<fieldset>
		<legend>Fields</legend>
		
		<div class="display-label">IsValid</div>
		<div class="display-field"><%= Html.Encode(Model.IsValid) %></div>
		
		<div class="display-label">ListingID</div>
		<div class="display-field"><%= Html.Encode(Model.ListingID) %></div>
		
		<div class="display-label">Lat</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:F}", Model.Lat)) %></div>
		
		<div class="display-label">Long</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:F}", Model.Long)) %></div>
		
		<div class="display-label">StreetAddress</div>
		<div class="display-field"><%= Html.Encode(Model.StreetAddress) %></div>
		
		<div class="display-label">OriginalSubmitterUserID</div>
		<div class="display-field"><%= Html.Encode(Model.OriginalSubmitterUserID) %></div>
		
		<div class="display-label">DateOfSubmission</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.DateOfSubmission)) %></div>
		
		<div class="display-label">AverageOverallRating</div>
		<div class="display-field"><%= Html.Encode(Model.AverageOverallRating) %></div>
		
		<div class="display-label">LatestReviewRevisionDate</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.LatestReviewRevisionDate)) %></div>
		
		<div class="display-label">LatestUseOfPianoDate</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.LatestUseOfPianoDate)) %></div>
		
		<div class="display-label">NumberOfReviews</div>
		<div class="display-field"><%= Html.Encode(Model.NumberOfReviews) %></div>
		
		<div class="display-label">AveragePricePerHourInUSD</div>
		<div class="display-field"><%= Html.Encode(String.Format("{0:F}", Model.AveragePricePerHourInUSD)) %></div>
		
	</fieldset>
	<p>

		<%= Html.ActionLink("Edit", "Edit", new { id=Model.ListingID }) %> |
		<%= Html.ActionLink("Back to List", "Index") %>
	</p>

</asp:Content>

