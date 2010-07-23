<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OpenIdRegistrationViewModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        We haven't seen that OpenID before. To create a new account, please fill out the form below.</p>
        <p>If you have registered on this site before, please <%=Html.ActionLink("return to the login page","OpenidLogin","Account") %> and enter the OpenID you used before. If you don't remember the OpenID you used, <%=Html.ActionLink("click here","RecoverOpenID","Account") %>.</p>
    <%= Html.ValidationSummary("Account creation was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("OpenidRegisterFormSubmit","Account",FormMethod.Post)) { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <%= Html.LabelFor(m=>m.Username) %>
                    <%= Html.TextBoxFor(m=>m.Username)%>
                    <%= Html.ValidationMessageFor(m=>m.Username) %>
                </p>
                <p>
                    <%= Html.LabelFor(m=>m.EmailAddress) %>
                    <%= Html.TextBoxFor(m=>m.EmailAddress) %>
                    <%= Html.ValidationMessageFor(m=>m.EmailAddress) %>
                </p>
                <p>
                    <%= Html.LabelFor(m=>m.ConfirmEmailAddress) %>
                    <%= Html.TextBoxFor(m=>m.ConfirmEmailAddress) %>
                    <%= Html.ValidationMessageFor(m=>m.ConfirmEmailAddress) %>
                </p>
                <p>
                    <label for="CAPTCHA">Verification word(s):</label>
                    <%= Html.GenerateCaptcha() %>
                </p>
                <%=Html.HiddenFor(m=>m.OpenIdClaim) /*TODO: This is a bug! What if the user changes hidden value before submitting the form? They could spoof their openid! How do I prevent this?*/%>
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
