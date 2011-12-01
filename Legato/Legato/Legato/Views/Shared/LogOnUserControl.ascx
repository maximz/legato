<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
	<li class="dropdown" data-dropdown="dropdown">
    <a href="#" class="dropdown-toggle"><%= Html.Encode(Page.User.Identity.Name) %></a>
        <ul class="dropdown-menu">
            <!--<li><a href="#">Secondary link</a></li>
            <li><a href="#">Something else here</a></li>
            <li class="divider"></li>-->
            <li><a href="<%=Url.Action("LogOff","Account") %>">Logout</a></li>
        </ul>
</li>
<%
    }
    else {
%> 
        <li><a href="<%= Url.Action("Login","Account", new { ReturnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery })%>">Login</a></li>
<%
    }
%>
