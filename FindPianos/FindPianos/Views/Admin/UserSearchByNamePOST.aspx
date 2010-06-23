<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FindPianos.Models.aspnet_User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UserSearchByNamePOST
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UserSearchByNamePOST</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ApplicationId
            </th>
            <th>
                UserId
            </th>
            <th>
                UserName
            </th>
            <th>
                LoweredUserName
            </th>
            <th>
                MobileAlias
            </th>
            <th>
                IsAnonymous
            </th>
            <th>
                LastActivityDate
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.UserId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.UserId })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.UserId })%>
            </td>
            <td>
                <%= Html.Encode(item.ApplicationId) %>
            </td>
            <td>
                <%= Html.Encode(item.UserId) %>
            </td>
            <td>
                <%= Html.Encode(item.UserName) %>
            </td>
            <td>
                <%= Html.Encode(item.LoweredUserName) %>
            </td>
            <td>
                <%= Html.Encode(item.MobileAlias) %>
            </td>
            <td>
                <%= Html.Encode(item.IsAnonymous) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.LastActivityDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

