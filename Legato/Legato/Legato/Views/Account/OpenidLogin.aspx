<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Legato" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
  Log In or Register
</asp:Content>


<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page-description">
  <h1>Log In or Register</h1>
	<% if((ViewData["OneTimeSignupCode"] as string).HasValue())
	{ %>
		<div class="one-time-signup-welcome">
			<h2>Welcome <%if((ViewData["WelcomeName"] as string).HasValue()) { %> <%=Html.Encode(ViewData["WelcomeName"] as string)%><% } %>!</h2>
			<p>You've been invited to join Legato Network. To sign up, please click your OpenID account provider below.</p>
		</div>
	<% } %>
	<div>
		<p>To log in or register, select your <a href="http://openid.net/what/">OpenID account provider</a> below.</p>
	</div>
	<div class="form-error">
	</div>
	<form id="openid_form" action="<%=Url.Action("Authenticate","Account") %>" method="post">       
			<% if((ViewData["OneTimeSignupCode"] as string).HasValue()) { %><%=Html.Hidden("OneTimeSignupCode",ViewData["OneTimeSignupCode"] as string) %><% } %>

			<div id="openid_choice"> 
				<div id="openid_btns"></div> 
			</div> 
			
			<div id="openid_input_area"> 
			</div> 
			
			<div> 
				<noscript> 
				<p>OpenID is a service that allows you to log on to many different websites using a single identity.
				Find out <a href="http://openid.net/what/">more about OpenID</a> and <a href="http://openid.net/get/">how to get an OpenID enabled account</a>.</p> 
				</noscript> 
			</div> 
						
			<p>Or, manually enter your OpenID</p> 
			
			<table id="openid-url-input"> 
				<tr>            
					<td class="vt large"><input id="openid_identifier" name="openid_identifier" 
					  class="openid-identifier" style="height:28px; width:500px;"  tabindex="100"></td> 
					<td class="vt large"><input id="submit-button" 

					  style="margin-left:5px; height:36px;" type="submit" value="Log in" tabindex="101"></td> 
				</tr>                                
				
 
			</table> 
			
		   
			
		</form> 
  </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<link rel="stylesheet" href="/static/openid/openid.css"/>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server">
  <script src="/static/js/openid-jquery.js" type="text/javascript"></script>

  <script type="text/javascript">
	$().ready(function () {
	  openid.init('openid_identifier');
	  $("#openid_identifier").focus();
	});
	</script> 
</asp:Content>
