<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.Models.PianoUserSuspension>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SuspendUser
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SuspendUser</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SuspensionID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SuspensionID) %>
                <%= Html.ValidationMessageFor(model => model.SuspensionID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.UserID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.UserID) %>
                <%= Html.ValidationMessageFor(model => model.UserID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Reason) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Reason) %>
                <%= Html.ValidationMessageFor(model => model.Reason) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SuspensionDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SuspensionDate) %>
                <%= Html.ValidationMessageFor(model => model.SuspensionDate) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ReinstateDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ReinstateDate) %>
                <%= Html.ValidationMessageFor(model => model.ReinstateDate) %>
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

