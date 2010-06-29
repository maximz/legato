<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
	<script type="text/javascript">
		(function () {
			window.onload = function () {
				// Creating a LatLng object containing the coordinate for the center of the map  
				var latlng = new google.maps.LatLng(42.97, -75.30);
				// Creating an object literal containing the properties we want to pass to the map  
				var options = {
					zoom: 7,
					center: latlng,
					mapTypeId: google.maps.MapTypeId.ROADMAP
				};
				// Calling the constructor, thereby initializing the map  
				var map = new google.maps.Map(document.getElementById('map'), options);

				var marker = new google.maps.Marker({
					position: new google.maps.LatLng(42.97, -75.30),
					map: map,
					title: 'Address',
					clickable: true,
					icon: 'http://google-maps-icons.googlecode.com/files/music-classical.png'
				});
			}
		})();
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 class="centerPageHeading">find a piano</h2>
    <div id="map_canvas" style="width: 100%; height: 100%">
	</div>

</asp:Content>
