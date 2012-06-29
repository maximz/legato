var Legato = Legato || {};

(function() {
	var Map = function() {
		var self = this;

		this.map = null;
		this.mapCanvas = null;
		this.markers = [];
		this.geocoder = null;
		this.mapOptions = {
			maxZoom: 20,
			minZoom:2,
			zoom: 3,
			center: new google.maps.LatLng(40, 190),
			scrollwheel: true,
			mapTypeControl: true,
			streetViewControl: true,
			zoomControlOptions: {
				position: google.maps.ControlPosition.LEFT_TOP
			},
			panControlOptions: {
				position: google.maps.ControlPosition.LEFT_TOP
			}
		}
		this.mapType = new google.maps.StyledMapType([
			{
				featureType: "all",
				elementType: "all",
				stylers: [
					{ visibility: "off" },  // Hide everything
					{ lightness: 100 }  // Makes the land white
				]
			}, {
				featureType: "water",
				elementType: "geometry",
				stylers: [
					{ visibility: "on" },  // Show water, but no labels
					{ lightness: -9 },  // Must be < 0 to compensate for the "all" lightness
					{ saturation: -100 }
				]
			}, {
                featureType: "all",
                elementType: "labels",
                stylers: [
                  { visibility: "on" },
                  { lightness: -50 }
                ]
              }
		], {
            name: "Styled Map"
        });
		this.overlay = null;

		this.init = function() {
			self.mapCanvas = $("#mapCanvas")[0];

			self.map = new google.maps.Map(self.mapCanvas, self.mapOptions);
            self.map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
            // if we use the styled map type:
//			self.map.mapTypes.set('styledMapType', self.mapType);
//			self.map.setMapTypeId('styledMapType');

			self.overlay = new google.maps.OverlayView();
			self.overlay.draw = function() {};
			self.overlay.setMap(self.map);

			geocoder = new google.maps.Geocoder();

			// This is a stop-gap measure to minimize the effect of
			// a bug we haven't solved yet.
			
//			google.maps.event.addListener(self.map, 'dragstart', function() {
//				$(".message").not("#messageTemplate").remove();
//			});

		}

		this.newMarker = function(lat, lng) {
			// Used for randomizing marker image
			var offset = Math.floor(Math.random() * 3) * 16;
			// Custom marker
			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(lat, lng)				
			});

			return marker;
		}

		this.markLocations = function(locations) {
			if(locations==null) {
                // this is if no instruments exist in the DB
                return;
            }
            var i = locations.length;
			while (i--) {
				var loc = locations[i];

				// Make new marker
				var marker = self.newMarker(loc.lat, loc.lng);
				self.markers.push(marker);

				// Tack on some extra data
				marker.id = loc.id;

				// Make infowindow
				var contentString = '<div id="content">'+
	'<p><a href="http://legatonetwork.com/Instruments/Listing/'+loc.id+'/'+loc.slug+'">'+loc.label+'</a>'+
	'</p></div>';

var infowindow = new google.maps.InfoWindow({
	content: contentString
});


				// Handle click event
				google.maps.event.addListener(marker, 'click', function() {
	
					infowindow.open(self.map,marker);
				});

				// Stagger animation onto the map
				if (i < 50) {
					marker.setAnimation(google.maps.Animation.DROP);
					setTimeout((function(marker) {
						return function() {
							marker.setMap(self.map);
						}
					})(marker), i*200 + Math.random()*500+500);
				} else {
					marker.setAnimation(null);
					marker.setMap(self.map);
				}
			}
		}

        // Removes markers from the map: see http://code.google.com/apis/maps/documentation/javascript/overlays.html#RemovingOverlays and http://stackoverflow.com/questions/1544739/google-maps-api-v3-how-to-remove-all-markers/1903905#1903905
        this.clearMarkers = function() {
            if (self.markers) {
                for (var i = 0; i < self.markers.length; i++) {
                    self.markers[i].setMap(null); // remove from map
                    self.markers[i] = null; // set marker to null to delete it from memory
                }
                self.markers.length = 0; // trash the whole array, removing all references to the old markers
            }
        }

		// For panning to an address
		this.codeAddress = function() {
			var address = $('#address').val();
			geocoder.geocode( { 'address': address}, function(results, status) {
			  if (status == google.maps.GeocoderStatus.OK) {
				self.map.setCenter(results[0].geometry.location);
                self.map.fitBounds(results[0].geometry.viewport);
//				var marker = new google.maps.Marker({
//					map: self.map, 
//					position: results[0].geometry.location
//				});
			  } else {
				alert("We couldn't find that address. Please try again."); // alert(status);
			  }
			});
		}

		this.latLngPixel = function(latLng) {
			var projection = self.overlay.getProjection();
			return projection.fromLatLngToContainerPixel(latLng);
		}
	}

	Legato.map = new Map();
})();