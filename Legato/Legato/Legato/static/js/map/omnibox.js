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
						
						if(self.omni.val()!=null && self.omni.val().indexOf("ID:")==0)
						{
							// We're dealing with an instrument ID
							$.getJSON(this.getSearchUrl+"?strId="+self.omni.val().substr(3),
								function (data) {
									if(locations == null)
									{
										alert('Invalid instrument ID.');
										return;
									}
									
									Legato.map.smartPanTo(new google.maps.LatLng(data[0].lat, data[0].lng));
									return;
							});
						}
						
						// Pan to address
						Legato.map.geocodeAddress(self.omni.val());
					}
				});

				/* TODO: */

				self.omni.autocomplete(
				{
					source: Legato.map.markers,
					select: function (event, ui)
					{
						Legato.map.panToMarker(ui.item);
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
		} // end Omnibox()
		Legato.omnibox = new Omnibox();
})();