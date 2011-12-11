<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="topbar" data-scrollspy="scrollspy">
	  <div class="fill">
		<div class="container">
		  <a class="brand" href="<%= Url.Content("~") %>" style="background: url(<%= Url.Content("~/static/images/logo_stretched.png") %>); text-indent: -9999px; background-size: 200px 38px; background-repeat: no-repeat; width: 160px;">Legato Network</a>
		  <ul class="nav">
			<li><a href="<%= Url.Content("~") %>">Home</a></li>

			<li><a href="/instruments">Map</a></li>
			<li><a href="/instruments/submit">Submit</a></li>
			<li class="dropdown" data-dropdown="dropdown">
				<a href="/about" class="dropdown-toggle">About</a>
				<ul class="dropdown-menu">
					<li><a href="/about">About Legato</a></li>
					<li><a href="/about#faq">FAQ</a></li>
					<li class="divider"></li>
					<li><a href="/about/contact">Contact</a></li>
					<li><a href="http://blog.legatonetwork.com">Blog</a></li>
					<li><a href="/legal">Legal</a></li>
				</ul>
			</li>
		  </ul>
		  <ul class="nav secondary-nav">
			<% Html.RenderPartial("LogOnUserControl"); %>
		  </ul>
		  <% using(Html.BeginForm("IndexPost","Search",new { @class = "pull-right" }))
		  { %>
			<input type="text" id="query" name="query" placeholder="Search" />
		  <% } %>
		</div>
	  </div>
	</div>