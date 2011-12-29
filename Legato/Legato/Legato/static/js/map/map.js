/**
 * @author Maxim
 * Handles map functions
 */
var Legato = Legato || {};

(function ()
{
	var Map = function ()
		{
			var self = this;
			
			
			this.getIndividualPageLink = function(id, slug)
			{
				// Instruments/Listing/{instrumentID}/{slug?}
				return "/Instruments/Listing/"+id+"/"+slug;
			}
			this.getMarkerJSONLink = function()
			{
				//return "exampleJson.html";
				return "/Instruments/AJAX/GetPoints";
			}

			this.map = null;
			this.earth = null;
			this.markerSize = 1;
			this.spriteWidth = 64;
			this.isEarthCurrentlyEnabled = false;
			this.isEarthReady = false;
			//this.timeZonesLayer = null;
			this.mapCanvas = null;
			this.markers = [];
			this.earthPlacemarks = [];
			this.geocoder = null;
			this.mapOptions = {
				maxZoom: 20,
				minZoom: 2,
				zoom: 3,
				center: new google.maps.LatLng(27.83877, 11.22343),
				scrollwheel: true,
				mapTypeControl: false,
				streetViewControl: true,
				zoomControlOptions: {
					position: google.maps.ControlPosition.LEFT_CENTER
				},
				panControlOptions: {
					position: google.maps.ControlPosition.LEFT_CENTER
				},
				mapTypeControlOptions: {
					position: google.maps.ControlPosition.TOP_RIGHT
				}
			}
			this.mapType = new google.maps.StyledMapType([
			{
				featureType: "all",
				elementType: "all",
				stylers: [
				{
					visibility: "off"
				}, // Hide everything
				{
					lightness: 100
				} // Makes the land white
				]
			}, {
				featureType: "water",
				elementType: "geometry",
				stylers: [
				{
					visibility: "on"
				}, // Show water, but no labels
				{
					lightness: -9
				}, // Must be < 0 to compensate for the "all" lightness
				{
					saturation: -100
				}]
			}, {
				featureType: "all",
				elementType: "labels",
				stylers: [
				{
					visibility: "on"
				}, {
					lightness: -50
				}]
			}], {
				name: "Styled Map"
			});
			this.overlay = null;

			this.init = function ()
			{
				self.mapCanvas = $("#mapCanvas")[0];

				self.map = new google.maps.Map(self.mapCanvas, self.mapOptions);
				self.map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
				//map.addMapType(G_SATELLITE_3D_MAP);
/* if we use the styled map type:
			self.map.mapTypes.set('styledMapType', self.mapType);
			self.map.setMapTypeId('styledMapType');
			*/

				self.overlay = new google.maps.OverlayView();
				self.overlay.draw = function ()
				{};
				self.overlay.setMap(self.map);

				geocoder = new google.maps.Geocoder();

				// When we add infobubbles, here's a hacky bugfix:
				google.maps.event.addListener(self.map, 'dragstart', function ()
				{
					$(".infoBubble").not("#infoBubbleTemplate").remove();
				});

				/*
				// timezones
				self.timeZonesLayer = new google.maps.KmlLayer("http://99.71.136.32/Features2.kmz");
				self.timeZonesLayer.setMap(self.map);
				self.panToWorld(); // KML might change current position */
				
				// Google Earth
				self.earth = new EarthMapType(self.map);
				
				google.maps.event.addListener(self.earth, 'initialized', function (ok) {
					if(ok)
					{
					if (self.earth) {
						console.log("earth is ready!");
						self.isEarthReady = true;
						/* this is if we want to switch to GEarth on load
						self.goToThreeD();
						*/
						
						// add overlays
							// self.timeZonesLayer.setMap(self.map);
							$.each(self.markers, function(index, value) {
								self.addMarkerToEarth(value);
							});
						
						self.panToWorld(); // KML might change current position
					}
					}
					else // if not OK
					{
						// KML overlay
						// self.timeZonesLayer.setMap(self.map);
						
						// pan to world
						self.panToWorld(); // KML might change current position
					}
				});
				self.panToWorld(); // KML might change current position
				
			}

			/*// Toggles KML layer visibility
			this.toggleTimeZoneLayer = function(on)
			{
				if(self.map && self.timeZonesLayer)
				{
					if(on)
					{
						// get information about current position
						var curPosition = self.map.getCenter();
						var curZoom = self.map.getZoom();
						
						// enable the time zone layer
						self.timeZonesLayer.setMap(self.map);
						
						// enabling the timezone layer usually pans the map automatically, so we want to go back to the previous position
						self.smartPanTo(curPosition);
						self.map.setZoom(curZoom);
					}
					else
					{
						self.timeZonesLayer.setMap(null);
					}
				}
			}*/
			
			this.calculateMarkerSize = function(count) // count is currently not used
			{
				// this method calculates marker size and scale factor for our marker sprites (see makeMarkerImage() and newMarker()). 
			
				/* Used for calculating marker size based on device count - which we are not doing right now
				if(count > 10)
				{
					count = 10;
				}
				var size = Math.floor(4*(count-1) + 8); // * spriteWidth/16; */

				var markerSize = (self.markerSize > 0 && self.markerSize <= 5) ? 16 * self.markerSize : 16;
				var markerScale = markerSize / self.spriteWidth;
				
				var result = {
					size : markerSize,
					scaleFactor : markerScale 
				};
				return result;
			}
			
			this.makeMarkerImage = function(size, scaleFactor, offset)
			{
				// returns a google.maps.MarkerImage() with the appropriate values for our marker creation procedure.
				var spriteWidth = self.spriteWidth;
				
				return new google.maps.MarkerImage('/static/images/map/markers.png', new google.maps.Size(spriteWidth * scaleFactor, spriteWidth * scaleFactor), new google.maps.Point(0, offset * scaleFactor), new google.maps.Point(spriteWidth / 2, spriteWidth / 2), new google.maps.Size(spriteWidth * scaleFactor, spriteWidth * 3 * scaleFactor));
			}

			this.newMarker = function (lat, lng, count)
			{

				// this method is in charge of creating individual markers
				
				var spriteWidth = self.spriteWidth;

				// Used for randomizing marker image
				var offset = Math.floor(Math.random() * 3) * spriteWidth;
				
				var markerSize = self.calculateMarkerSize(count);

				// Custom marker
				var marker = new google.maps.Marker(
				{
					position: new google.maps.LatLng(lat, lng),
					icon: self.makeMarkerImage(markerSize.size, markerSize.scaleFactor, offset)
				});
				
				marker.offset = offset; // in case we have to refresh this marker or change its size

				return marker;
			}

			this.markLocations = function (locations)
			{
				// this method takes our JSON of points and converts it into map markers

				console.log("marking our locations");
				if (locations == null)
				{
					// this is if no points exist in the DB
					return;
				}
				var i = locations.length;
				while (i--)
				{
					var loc = locations[i];

					loc.count = 1; // for now, we're excluding count from the map, but if we add it back into the JSON, all we have to do is remove this line and then uncomment the stuff in self.newMarker().
					
					// Make new marker
					var marker = self.newMarker(loc.lat, loc.lng, loc.count);
					self.markers.push(marker);

					// Tack on some extra data
					marker.id = loc.id;
					marker.label = loc.label;
					marker.typeid = loc.typeid;
					marker.typename = loc.typename;
					marker.lClass = loc.lClass;
					marker.slug = loc.slug;
					marker.omniType = 'marker'; // for omnibox autocomplete logic

					// Handle mouseover event
					google.maps.event.addListener(marker, 'mouseover', function ()
					{

						// Get screen xy
						var pos = self.latLngPixel(this.getPosition());

						// Create an overlay
						var overlay = self.createOverlay(this, pos);

						// Add it to the dom
						$(self.mapCanvas).after(overlay);
					});

					// Stagger animation onto the map
					if (i < 50)
					{
						marker.setAnimation(google.maps.Animation.DROP);
						setTimeout((function (marker)
						{
							return function ()
							{
								marker.setMap(self.map);
							}
						})(marker), i * 200 + Math.random() * 500 + 500);
					}
					else
					{
						marker.setAnimation(null);
						marker.setMap(self.map);
					}
					
					// Add to Google Earth
					self.addMarkerToEarth(marker);
				}
				
				self.panToWorld(); // KML might change current position
				
				// After all markers are added, initialize omnibox:
				Legato.omnibox.init();
			}
			
			this.addMarkerToEarth = function (marker)
			{
				if(self.isEarthReady)
				{
				try
				{
				console.log("in addMarkerToEarth()");
				var ge = self.earth.get('earth_plugin');
				
				// Create the placemark.
				var placemark = ge.createPlacemark('');
				placemark.setName(marker.label); // shows the name next to the icon

				// Set the placemark's location.  
				var point = ge.createPoint('');
				point.setLatitude(marker.position.lat());
				point.setLongitude(marker.position.lng());
				placemark.setGeometry(point);
				
				// Define a custom icon.
				var icon = ge.createIcon('');
				icon.setHref('http://maps.google.com/mapfiles/kml/paddle/red-circle.png');
				//icon.setX(10);
				//icon.setY(10);
				var style = ge.createStyle('');
				style.getIconStyle().setIcon(icon);
				style.getIconStyle().setScale(3.0);
				placemark.setStyleSelector(style);
				
				// For the balloon
				placemark.setDescription('<a href="'+self.getIndividualPageLink(marker.id, marker.slug)+'">'+marker.label+'</a>')

				// Add the placemark to Earth.
				ge.getFeatures().appendChild(placemark);
				self.earthPlacemarks.push(placemark);
				console.log("added one point to earth")
				}
				catch(err)
				{
					// nasty hack: swallow error because it's due to a race condition (markers are added at the same time as GEarth gets initialized)
					console.log('race condition bug');
				}
				}
			}
			
			this.addPlacemarkToEarth = function(placemark)
			{
				if(self.isEarthReady)
				{
					self.earth.get('earth_plugin').getFeatures().appendChild(placemark);
				}
			}
			
			this.removePlacemarkFromEarth = function(placemark)
			{
				if(self.isEarthReady)
				{
					var ge = self.earth.get('earth_plugin');
					ge.getFeatures().removeChild(placemark);
				}
			}

			this.createOverlay = function (marker, position)
			{
				var overlay = $("#messageTemplate").clone();

				overlay.attr("id", marker.id);
				overlay.attr("href", self.getIndividualPageLink(marker.id, marker.slug))

				// Position it at the marker
				overlay.css(
				{
					left: position.x - 52,
					top: position.y - 52,
				});

				// Use distance from center to decide when to hide
				var radius = 50;
				var radiusSq = radius * radius;

				var checkDistance = function (event)
					{
						var dx = event.pageX - position.x;
						var dy = event.pageY - position.y;

						if (dx * dx + dy * dy > radiusSq)
						{

							$(window).unbind("mousemove", checkDistance);
							overlay.data("MessageBubble").disappear(250);
							setTimeout(function ()
							{
								overlay.remove();
							}, 250);
						}
					}

				$(window).bind("mousemove", checkDistance);

				// Make it a message bubble
				overlay.messageBubble();

				var dom = overlay.data("MessageBubble").dom;
				dom.label.text(marker.typename);
				dom.lower.text('Listing');

				overlay.data("MessageBubble").disappear(0);
				setTimeout(function ()
				{
					overlay.data("MessageBubble").appear(250);
				}, 20);

				return overlay;
			}

			// Removes markers from the map: see http://code.google.com/apis/maps/documentation/javascript/overlays.html#RemovingOverlays and http://stackoverflow.com/questions/1544739/google-maps-api-v3-how-to-remove-all-markers/1903905#1903905
			this.clearMarkers = function ()
			{
				if (self.markers) // handles 2D markers
				{
					for (var i = 0; i < self.markers.length; i++)
					{
						self.markers[i].setMap(null); // remove from map
						self.markers[i] = null; // set marker to null to delete it from memory
					}
					self.markers.length = 0; // trash the whole array, removing all references to the old markers
				}
				
				if (self.isEarthReady && self.earthPlacemarks) // handles 3D markers (known as "placemarks")
				{
					for (var j = 0; j < self.earthPlacemarks.length; j++)
					{
						self.removePlacemarkFromEarth(self.earthPlacemarks[j]); // remove from earth
						self.earthPlacemarks[j] = null; // set placemark to null to delete it from memory
					}
					self.earthPlacemarks.length = 0; // trash the whole array, removing all references to the old markers
				}
			}

			// For panning to an address
			this.geocodeAddress = function (address)
			{
				geocoder.geocode(
								{
										'address': address
								}, function (results, status)
								{
										if (status == google.maps.GeocoderStatus.OK)
										{
												self.smartPanTo(results[0].geometry.location);
												//self.map.setCenter(results[0].geometry.location);
												self.map.fitBounds(results[0].geometry.viewport);
												//                              var marker = new google.maps.Marker({
												//                                      map: self.map, 
												//                                      position: results[0].geometry.location
												//                              });
										}
										else
										{
												alert("We couldn't find that address. Please try again."); // alert(status);
										}
								});
			}
			
			this.doGeoCode = function (address)
			{
				var geocoder = self.geocoder;
				if(!geocoder)
				{
					geocoder = new google.maps.Geocoder();
				}
				geocoder.geocode(
				{
					'address': address
				}, function (results, status)
					{
						if (status == google.maps.GeocoderStatus.OK)
						{
							return results;
						}
						else
						{
							return null;
						}
					});
					
					//return null;
			}
			
			
			this.earthPanTo = function(lat, lng, range)
			{
				if(self.isEarthReady)
				{
				var lookAt = self.earth.get('earth_plugin').getView().copyAsLookAt(self.earth.get('earth_plugin').ALTITUDE_RELATIVE_TO_GROUND);
					lookAt.setLatitude(lat);
					lookAt.setLongitude(lng);
					lookAt.setHeading(0.0); 
					lookAt.setTilt(0.0); 
					lookAt.setRange(range);
					self.earth.get('earth_plugin').getView().setAbstractView(lookAt);
				}
			}
			
			this.smartPanTo = function(latLng)
			{
				if(self.isEarthCurrentlyEnabled) // 3D mode
				{
					self.earthPanTo(latLng.lat(), latLng.lng(), 1000);
				}
				else // 2D mode
				{
					self.map.panTo(latLng);
					self.map.setZoom(15);
				}
			}

			this.panToMarker = function (marker)
			{
				self.smartPanTo(marker.getPosition());
			}

			this.latLngPixel = function (latLng)
			{
				var projection = self.overlay.getProjection();
				return projection.fromLatLngToContainerPixel(latLng);
			}

			this.goToTwoD = function ()
			{
				self.map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
				if(self.earth) {
					self.earth.set('graticules', false); // disable grid
				}
				self.isEarthCurrentlyEnabled = false;
			}

			this.goToThreeD = function ()
			{
				if (self.isEarthReady && self.earth) {
					self.map.setMapTypeId('Earth'); // switches to GEarth view
					self.earth.set('graticules', true); // enables grid
					self.isEarthCurrentlyEnabled = true; // changes our internal boolean
				}
				else
				{
					// show modal dialog
					$('<p></p>').html('To use 3D view, you must <a href="http://www.google.com/earth/explore/products/plugin.html" target="_blank" title="install the Google Earth Plugin">install the Google Earth Plugin</a>. If it is already installed, please refresh the page to enable 3D mode.').appendTo('#myModalDialog .modalContentWrap');
					$("#myModalDialog").overlay({
						
						// some mask tweaks suitable for modal dialogs
						mask: 'gray',
						effect: 'apple',
						closeOnClick: true,
						load: true
					});
				}
			}

			this.panToWorld = function ()
			{
				if(self.isEarthReady && self.isEarthCurrentlyEnabled)
				{
					self.earthPanTo(35.4533306,-55.0335555,13344353)
				}
				else
				{
					self.smartPanTo(new google.maps.LatLng(27.83877, 11.22343));
					self.map.setZoom(3);
				}
			}

			this.geolocate = function ()
			{
				var browserSupportFlag = new Boolean();
				// Try W3C Geolocation (Preferred)
				if (navigator.geolocation)
				{
					browserSupportFlag = true;
					navigator.geolocation.getCurrentPosition(function (position)
					{
						self.smartPanTo(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
						self.map.setZoom(15);
					}, function ()
					{
						alert("There was a problem with geolocation. Please enter your address into the searchbox manually.");
					});
					// Try Google Gears Geolocation
				}
				else if (google.gears)
				{
					browserSupportFlag = true;
					var geo = google.gears.factory.create('beta.geolocation');
					geo.getCurrentPosition(function (position)
					{
						self.smartPanTo(new google.maps.LatLng(position.latitude, position.longitude));
						self.map.setZoom(15);
					}, function ()
					{
						alert("There was a problem with geolocation. Please enter your address into the searchbox manually.");
					});
					// Browser doesn't support Geolocation
				}
				else
				{
					browserSupportFlag = false;
					alert("There was a problem with geolocation (not supported in your browser). Please enter your address into the searchbox manually.");
				}
			}
			
			
			this.loadMarkerData = function ()
			{
				spinner(true); // activate loading spinner
				// loads JSON with marker data

				self.clearMarkers(); // clears any markers that might be already on the map, since we're reloading them all
				
				$.getJSON(self.getMarkerJSONLink(),
					function (data) {
					self.markLocations(data);
					spinner(false); // deactivate loading spinner
				});
			}
			
			this.filterMarkers = function(typeID)
			{
				self.unfilterMarkers(); // reset any current filters
			
				for (var i = 0; i < self.markers.length; i++)
				{
					if(parseInt(self.markers[i].typeid) != parseInt(typeID))
					{
						self.markers[i].setMap(null); // remove from map
						self.removePlacemarkFromEarth(self.earthPlacemarks[i]); // remove from earth
					}
				}
			}
			
			this.unfilterMarkers = function()
			{
				for (var i = 0; i < self.markers.length; i++)
				{
					self.markers[i].setMap(self.map); // add to map
					self.addPlacemarkToEarth(self.earthPlacemarks[i]);
				}
			}

			this.updateMarkerSize = function()
			{
				for (var i = 0; i < self.markers.length; i++)
				{
					var marker = self.markers[i];
					
					var markerSize = self.calculateMarkerSize(marker.count);
					marker.icon = self.makeMarkerImage(markerSize.size, markerSize.scaleFactor, marker.offset); // change marker icon
					
					marker.setMap(self.map); // refresh marker by readding it to the map
				}
			}

		} // end Map
		Legato.map = new Map();
})();