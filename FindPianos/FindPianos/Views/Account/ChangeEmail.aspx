<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Change Email
</asp:Content>
<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Change your email</h2>
    <p>
        Use the form below to change your email address.
    </p>
    <%= Html.ValidationSummary("Email address change was unsuccessful. Please correct the errors and try again.")%>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <p>Current email address: <b><%=ViewData["current"]%></b></p>
            <p>
                <label for="NewEmail">
                    New email:</label>
                <%= Html.TextBox("NewEmail") %>
                <%= Html.ValidationMessage("NewEmail") %>
            </p>
            <p>
                <label for="ConfirmEmail">
                    Confirm new email:</label>
                <%= Html.TextBox("ConfirmEmail") %>
                <%= Html.ValidationMessage("ConfirmEmail") %>
            </p>
            <p>
                <input type="submit" value="Change Email" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
