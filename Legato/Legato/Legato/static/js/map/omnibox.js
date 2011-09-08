/**
 * @author Maxim
 * Configures the omnibox.
 */
var Legato = Legato || {};

(function ()
{
	var Omnibox = function ()
		{
			var self = this;
			this.omni = null;
			
			this.getSearchUrl = function()
			{
				return "/Instruments/AJAX/SearchInsIds";
			}

			this.init = function ()
			{
				self.omni = $("#q");

				// Handle ENTER keypress
				self.omni.keypress(function (e)
				{
					var code = (e.keyCode ? e.keyCode : e.which);
					if (code == 13)
					{ //Keycode for ENTER key

                        if(self.isDeviceId(self.omni.val()))
						{
							// We're dealing with a device ID
							spinner(true); // activate spinner
							$.getJSON(this.getSearchUrl() + "?strId=" + self.isDeviceId(self.omni.val()),
								function (data) {
									spinner(false); // deactivate spinner
									
									if(data == null)
									{
										alert('Invalid device ID.');
										return;
									}
									
									Legato.map.smartPanTo(new google.maps.LatLng(data[0].lat, data[0].lng));
									return;
							});
						}
						else
						{
							// if we ever modify omnibox code so that this if-else block doesn't work with the AJAX stuff, here's how to make sure nothing unnecessary gets called: call an event when getJson finishes if data == null. event handler executes the geocode:
						
							// Pan to address
							Legato.map.geocodeAddress(self.omni.val());
						}
					}
				});

				self.omni.autocomplete(
				{
					source: Legato.map.markers.concat(Legato.instypes),
					select: function (event, ui)
					{
						if(ui.item && ui.item.omniType)
						{
							if(ui.item.omniType == 'type')
							{
								// handle instrument type filtering
								
								if(ui.item.id == -1) // if it's "All"
								{
									Legato.map.unfilterMarkers();
								}
								else
								{
									// filter markers
									Legato.map.filterMarkers(ui.item.id);
								}
							}
							else if(ui.item.omniType == 'marker')
							{
								// handle marker
								Legato.map.panToMarker(ui.item);
								Legato.map.unfilterMarkers(); // in case the marker was filtered out.
							}
						}
					}
				})

			} // end init()

			this.doSearch = function ()
			{
				var results = [];
				var text = self.omni.val();

				$.each(Legato.map.markers, function (key, value) // loop through map markers
				{
					// if marker.label contains current omnibox text, add it to the result array
					if (value.label.contains(text))
					{
						results.push(value);
					}
				});

			} // end doSearch()

            this.isDeviceId = function (input)
			{
				var r = /(([a-zA-Z][0-9]{2}){2})[0-9]{3}/; // regexp that matches device IDs - it looks for 2 groups of a letter and 2 numbers, followed by 3 numbers.
				
				if(input == null)
				{
					return null; // just so we don't get an exception when we try to match
				}
				
				if(input.match(r))
				{
					return input.match(r)[0]; // returns first match
				}
				else
				{
					return null;
				}
			}

		} // end Omnibox()
		Legato.omnibox = new Omnibox();
})();