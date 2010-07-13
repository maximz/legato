<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FindPianos.ViewModels.User>" %>
<%@ Import Namespace="StackExchange.DataExplorer" %>
<%@ Import Namespace="StackExchange.DataExplorer.ViewModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User <%=Model.Login%> - Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="AdditionalStyles" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <%bool isCurrentUser = Model.Id == (Guid)Membership.GetUser(true).ProviderUserKey;%>
  <div>
    <table class="vcard">
            <tr>
                <!--cell-->
                <td style="vertical-align:top; width:170px">
                    <table>
                        <tr>
                            <td style="padding:20px 20px 8px 20px">
                               <%=Model.Gravatar(128)%>
                            </td>
                        </tr>

                        <!--
                        <tr>
                            <td class="summaryinfo">
                                <div class="summarycount">10,133</div>
                                <div style="margin-top:5px; font-weight:bold">reputation</div>
                            </td>
                        </tr>

                        <tr style="height:30px">
                            <td class="summaryinfo" style="vertical-align:bottom">716 views</td>
                        </tr>
                        -->
                        
                    </table>
                </td>
                <!--cell-->
                <td style="vertical-align: top; width:350px">

                    <%
      if (isCurrentUser || User.IsInRole("Admin"))
      {%>
                      <div style="float: right; margin-top: 19px; margin-right: 4px">
                        <a href="/account/edit/<%=Model.Id%>">edit profile</a> 
                      </div>
                    <%
      }%>
                    <h2 style="margin-top:20px">User Profile</h2>
                    <table class="user-details">
                        <tr>
                            <td style="width:120px">username</td>
                            <td style="width:230px" class="fn nickname"><b><%=Model.Login%></b></td>
                        </tr>
                        <tr>
                            <td>member for</td>
                            <td><div class="timeago" title="<%=Model.CreationDate%>"><%=DateTime.Now - Model.CreationDate%></div></td>
                        </tr>
                        <tr>
                            <td>last seen</td>
                            <td><div class="timeago" title="<%=Model.LastSeenDate%>"><%=DateTime.Now - Model.LastSeenDate%></div></td>
                        </tr>
                        
                        <%
      if (isCurrentUser)
      {%>
                        <tr>
                            <td>OpenID</td>
                            <td><div class="no-overflow"><%=Model.UserOpenIds[0].OpenIdClaim%></div></td>

                        </tr>
                        <%
      }%>
                                                
                        <tr>

                            <td>location</td>
                            <td class="label adr">
                                <%=Model.Location%>
                            </td>
                        </tr>
                    </table>
                </td>

                <td style="width:390px">
                    <div id="user-about-me" class="note"><%=Model.SafeAboutMe%></div>
                </td>
               
            </tr>
        </table>

         <%
      Html.RenderPartial("SubHeader", ViewData["UserQueryHeaders"]);%>
        <ul class="querylist">
          <%
      var queries = ViewData["Queries"] as IEnumerable<QueryExecutionViewData>;%>
          <%
      foreach (QueryExecutionViewData query in queries)
      {%>
              <li> <a title="<%:query.Description%>" href="<%=query.Url%>"><%=Html.Encode(query.Name)%></a> </li> 
           <%
      }%>
        </ul>
        <%
      if (ViewData["EmptyMessage"] != null)
      {%>
        <h3><%=ViewData["EmptyMessage"]%></h3>
        <%
      }%>
  </div>
</asp:Content>
