<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.ActionLink(Html.Encode(Page.User.Identity.Name),"MyProfile","Account") %></b>!
        [ <%= Html.ActionLink("Logout", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Login", "LogOn", "Account") %> ]
        [ <%= Html.ActionLink("Register", "Register", "Account") %> ]
<%
    }
%>
