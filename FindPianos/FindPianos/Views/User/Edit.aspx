<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.User>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <table width="720px" id="user-edit-table">
    <tbody>
      <tr>
        <td style="vertical-align: top; text-align: center; padding: 20px; width: 128px;
          vertical-align: top; text-align: center; padding: 20px; width: 128px;">
          <h3>
            <%=Model.Gravatar(128)%>
            <a href="http://gravatar.com">Change Picture</a>
          </h3>
        </td>
        <td style="vertical-align: top;">
          <h2>
            Edit Profile</h2>
          <form action="/users/update/<%=Model.Id%>" method="post">
          <%=Html.ValidationSummary(true)%>
          <table style="width: 600px;">
            <tbody>
              <tr>
                <td>
                  <%=Html.LabelFor(model => model.Email)%>
                </td>
                <td>
                  <%=Html.ActionLink("Click here to edit your email.",%>
                </td>
              </tr>
              <tr>
                <td>
                  <%=Html.LabelFor(model => model.Location)%>
                </td>
                <td>
                  <%=Html.TextBoxFor(model => model.Location, new {style = "width: 260px"})%>
                  <%=Html.ValidationMessageFor(model => model.Location)%>
                </td>
              </tr>
              <tr>
                <td style="vertical-align: top;">
                  <label>
                    About Me</label>
                </td>
                <td>
                  <%=Html.TextAreaFor(model => model.AboutMe, new {rows = 12, cols = 56})%>
                  <%=Html.ValidationMessageFor(model => model.AboutMe)%>
                </td>
              </tr>
              <tr>
                <td>
                </td>
                <td class="form-submit">
                  <input type="submit" value="Save Profile" />
                  <input type="button" onclick="location.href='/users/<%=Model.Id%>'" value="Cancel" name="cancel" id="cancel"/>
                </td>
              </tr>
            </tbody>
          </table>
          </form>
        </td>
      </tr>
    </tbody>
  </table>
</asp:Content>
