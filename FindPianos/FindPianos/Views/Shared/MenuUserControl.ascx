<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="menu">
    <li <%if(!string.IsNullOrEmpty(ViewData["CurrentMenuItem"]) && ViewData["CurrentMenuItem"]=="About") { %> class="current" <% } %> >
        <%= Html.ActionLink("About",  "About", "Home")%></li>
    <li <%if(!string.IsNullOrEmpty(ViewData["CurrentMenuItem"]) && ViewData["CurrentMenuItem"]=="Search") { %>
        class="current" <% } %>>
        <%= Html.ActionLink("Search", "List", "Listing")%></li>
    <li <%if(!string.IsNullOrEmpty(ViewData["CurrentMenuItem"]) && ViewData["CurrentMenuItem"]=="Submit") { %>
        class="current" <% } %>>
        <%= Html.ActionLink("Submit", "Submit", "Listing")%></li>
    <% if (HttpContext.Current.User.IsInRole("Admin"))
       {%>
    <li <%if(!string.IsNullOrEmpty(ViewData["CurrentMenuItem"]) && ViewData["CurrentMenuItem"]=="Admin") { %>
        class="current" <% } %>>
        <%= Html.ActionLink("Admin", "UserSearchByName", "Admin") %></li>
    <% } %>
</ul>