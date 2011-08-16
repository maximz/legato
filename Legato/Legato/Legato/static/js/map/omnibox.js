/**
 * @author Maxim
 * Configures the omnibox.
 */
var Moon = Moon || {};

(function ()
{
	var Omnibox = function ()
		{
			var self = this;
			this.omni = null;

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
							// We're dealing with a device ID
							$.getJSON("searchdeviceids.php?id="+self.omni.val().substr(3),
								function (data) {
									if(locations == null)
									{
										alert('Invalid device ID.');
										return;
									}
									
									Moon.map.smartPanTo(new google.maps.LatLng(data[0].lat, data[0].lng));
									return;
							});
						}
						
						// Pan to address
						Moon.map.geocodeAddress(self.omni.val());
					}
				});

				/* TODO: */

				self.omni.autocomplete(
				{
					source: Moon.map.markers,
					select: function (event, ui)
					{
						Moon.map.panToMarker(ui.item);
					}
				})



			} // end init()
			this.doSearch = function ()
			{
				var results = [];
				var text = self.omni.val();

				$.each(Moon.map.markers, function (key, value) // loop through map markers
				{
					// if marker.label contains current omnibox text, add it to the result array
					if (value.label.contains(text))
					{
						results.push(value);
					}
				});


			} // end doSearch()
		} // end Omnibox()
		Moon.omnibox = new Omnibox();
})();