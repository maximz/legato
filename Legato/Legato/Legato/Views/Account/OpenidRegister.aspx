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
                    <%= Html.LabelFor(m=>m.Nickname) %>
                    <%= Html.TextBoxFor(m=>m.Nickname)%>
                    <%= Html.ValidationMessageFor(m=>m.Nickname) %>
                </p>
                <p>
                    <%= Html.LabelFor(m=>m.FullName) %>
                    <%= Html.TextBoxFor(m=>m.FullName)%>
                    <%= Html.ValidationMessageFor(m=>m.FullName) %>
                </p>
                <p>
                    <%= Html.LabelFor(m=>m.EmailAddress) %>
                    <%= Html.TextBoxFor(m=>m.EmailAddress) %>
                    <%= Html.ValidationMessageFor(m=>m.EmailAddress) %>
                </p>
<%--                <p>
                    <%= Html.LabelFor(m=>m.ConfirmEmailAddress) %>
                    <%= Html.TextBoxFor(m=>m.ConfirmEmailAddress) %>
                    <%= Html.ValidationMessageFor(m=>m.ConfirmEmailAddress) %>
                </p>--%>
                <p>Are you affiliated with The Bishop's School?</p>
                <p><%=Html.ValidationMessageFor(m=>m.BishopsAffiliation) %></p>
                <%= Html.RadioButton("BishopsAffiliation", "0") %>
                No
                <br />
                <%= Html.RadioButton("BishopsAffiliation", "1")%>
                Yes, I'm a student at Bishop's. My class year is <%=Html.TextBoxFor(m=>m.ClassYear) %> and my advisor is currently <%=Html.TextBoxFor(m=>m.Advisor) %>.
                <br /><%=Html.ValidationMessageFor(m=>m.ClassYear) %><%=Html.ValidationMessageFor(m=>m.Advisor) %>
                <br />
                <%= Html.RadioButton("BishopsAffiliation", "2")%>
                Yes, I am a member of the faculty or staff of Bishop's. My department is <%=Html.TextBoxFor(m=>m.Department) %>.
                <br /><%=Html.ValidationMessageFor(m=>m.Department) %>
                <%=Html.Hidden("BishopsAffiliation",null) /* see http://stackoverflow.com/questions/722655/asp-net-mvc-html-radiobutton-exception */ %>

                <p>
                    <label for="CAPTCHA">Verification word(s):</label>
                    <%= Html.GenerateCaptcha() %>
                </p>
                <%=Html.HiddenFor(m=>m.OpenIdClaim) %>
                <%=Html.AntiForgeryToken() %>
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
