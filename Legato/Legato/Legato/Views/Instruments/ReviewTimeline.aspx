<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FindPianos.Models.PianoReviewRevision>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReviewTimeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ReviewTimeline</h2>

    <table>
        <tr>
            <th></th>
            <th>
                IsValid
            </th>
            <th>
                PianoReviewRevisionID
            </th>
            <th>
                PianoReviewID
            </th>
            <th>
                PianoStyleID
            </th>
            <th>
                Brand
            </th>
            <th>
                Model
            </th>
            <th>
                RatingOverall
            </th>
            <th>
                RatingTuning
            </th>
            <th>
                RatingToneQuality
            </th>
            <th>
                RatingPlayingCapability
            </th>
            <th>
                Message
            </th>
            <th>
                PricePerHourInUSD
            </th>
            <th>
                VenueName
            </th>
            <th>
                SubmitterUserID
            </th>
            <th>
                DateOfRevision
            </th>
            <th>
                DateOfLastUsageOfPianoBySubmitter
            </th>
            <th>
                RevisionNumberOfReview
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.PianoReviewRevisionID }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.PianoReviewRevisionID })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.PianoReviewRevisionID })%>
            </td>
            <td>
                <%= Html.Encode(item.IsValid) %>
            </td>
            <td>
                <%= Html.Encode(item.PianoReviewRevisionID) %>
            </td>
            <td>
                <%= Html.Encode(item.PianoReviewID) %>
            </td>
            <td>
                <%= Html.Encode(item.PianoStyleID) %>
            </td>
            <td>
                <%= Html.Encode(item.Brand) %>
            </td>
            <td>
                <%= Html.Encode(item.Model) %>
            </td>
            <td>
                <%= Html.Encode(item.RatingOverall) %>
            </td>
            <td>
                <%= Html.Encode(item.RatingTuning) %>
            </td>
            <td>
                <%= Html.Encode(item.RatingToneQuality) %>
            </td>
            <td>
                <%= Html.Encode(item.RatingPlayingCapability) %>
            </td>
            <td>
                <%= Html.Encode(item.Message) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.PricePerHourInUSD)) %>
            </td>
            <td>
                <%= Html.Encode(item.VenueName) %>
            </td>
            <td>
                <%= Html.Encode(item.SubmitterUserID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateOfRevision)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateOfLastUsageOfPianoBySubmitter)) %>
            </td>
            <td>
                <%= Html.Encode(item.RevisionNumberOfReview) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

