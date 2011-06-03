<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
	<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
	<script type="text/javascript" src="../../Scripts/js/Fluster2.packed"></script>
	<script type="text/javascript">
		if (!Array.prototype.forEach) {
			Array.prototype.forEach = function (fun /*, thisp*/) {
				var len = this.length;
				if (typeof fun != "function")
					throw new TypeError();

				var thisp = arguments[1];
				for (var i = 0; i < len; i++) {
					if (i in this)
						fun.call(thisp, this[i], i, this);
				}
			};
		}
	  (function () {
		  window.onload = function () {
			  var myLatlng = new google.maps.LatLng(42.97, -75.30);
			  var myOptions = {
				  zoom: 7,
				  center: myLatlng,
				  mapTypeId: google.maps.MapTypeId.ROADMAP
			  };
			  var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
			  var fluster = new Fluster2(map);
			  google.maps.event.addListener(map, 'idle', function () {
				  var bounds = map.getBounds();

				  //AJAX:
				  var postData = "lat1=" + bounds.getSouthWest().lat() + "&long1=" + bounds.getSouthWest().lng() + "&lat2=" + bounds.getNorthEast().lat() + "&long2=" + bounds.getNorthEast().lng();
				  $.ajax({ url: "Search/EnumerateBox", data: postData, type: "POST", dataType: "json", cache: true, beforeSend: function () {
					  $('#animatedGifLoading').show(); // TODO: add animated loading gif into such a div
				  },
					  success: function (data) {
						  ProcessAjaxData(data, fluster);
					  }, error: function () {
						  alert("There was an issue loading pianos for the area of the map you're currently looking at. Please try again.");
					  }, complete: function () {
						  $('#animatedGifLoading').hide();
					  }
				  });
			  }); //end event listener
		  } //end window.onload
	  })();
	function ProcessAjaxData(data, fluster) // TODO: implement - http://code.google.com/apis/maps/documentation/javascript/overlays.html#InfoWindows and http://code.google.com/apis/maps/documentation/javascript/overlays.html#RemovingOverlays
	{
		data.forEach(function (element, index, array) {
			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(element.Lat, element.Long),
				title: element.StreetAddress,
				clickable: true,
				icon: 'http://google-maps-icons.googlecode.com/files/music-classical.png'
			});
			fluster.addMarker(marker);
		});
		
	}
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 class="centerPageHeading">find a piano</h2>
	<div id="map_canvas" style="width: 950px; height: 400px">
	</div>

</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContentAtEndOfBody" runat="server"></asp:Content>
