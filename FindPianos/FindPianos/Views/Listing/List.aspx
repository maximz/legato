<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
	<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
	<script type="text/javascript"> 
	  function initialize() {
		var myLatlng = new google.maps.LatLng(42.97, -75.30);
		var myOptions = {
		  zoom: 7,
		  center: myLatlng,
		  mapTypeId: google.maps.MapTypeId.ROADMAP
		}
var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

google.maps.event.addListener(map, 'idle', function () {
	var bounds = map.getBounds();

//	console.log('North East: ' +
//				  bounds.getNorthEast().lat() + ' ' +
//				  bounds.getNorthEast().lng());

//	console.log('South West: ' +
//				  bounds.getSouthWest().lat() + ' ' +
//				  bounds.getSouthWest().lng());

	//AJAX:
	var postData = "lat1=" + bounds.getSouthWest().lat() + "&long1=" + bounds.getSouthWest().lng() + "&lat2=" + bounds.getNorthEast().lat() + "&long2=" + bounds.getNorthEast().lng();
	$.ajax({ url: "Search/EnumerateBox", data: postData, type: "POST", dataType: "json", cache: true, beforeSend: function () {
		$('#animatedGifLoading').show(); // TODO: add animated loading gif into such a div
	},
		success: function (data) {
			ProcessAjaxData(data);
		}, error: function () {
			alert("There was an issue loading pianos for the area of the map you're currently looking at. Please try again.");
		}, complete: function () {
			$('#animatedGifLoading').hide();
		}
	});
}); //end event listener
}
function ProcessAjaxData() // TODO: implement - http://code.google.com/apis/maps/documentation/javascript/overlays.html#InfoWindows and http://code.google.com/apis/maps/documentation/javascript/overlays.html#RemovingOverlays
{
		var marker = new google.maps.Marker({
			position: myLatlng,
			map: map,
			title: "Address"
});
var marker2 = new google.maps.Marker({
	position: new google.maps.LatLng(42.97, -75.30),
	map: map,
	title: 'Address',
	clickable: true,
	icon: 'http://google-maps-icons.googlecode.com/files/music-classical.png'
});
	  }
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 class="centerPageHeading">find a piano</h2>
	<div id="map_canvas" style="width: 100%; height: 100%">
	</div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BodyOpeningTag" runat="server"><body onload="initialize()"></asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server"></asp:Content>
