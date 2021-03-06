/* Author: Maxim Zaslavsky
 * 
*/

// Map functions
	Legato.map.init();
	
	// omnibox is initialized after all markers are added, not here

	google.maps.event.addListenerOnce(Legato.map.map, 'tilesloaded', function () {
		Legato.map.loadMarkerData();
	});

	
var mapVersionCookieName = "legatomap_useold"; 
var cookieSettings = { expires: 30, path: '/' };
var cookieCheck = function()
{
	var mapVersionCookie = $.cookie(mapVersionCookieName);
	
	if(!oldMap && (mapVersionCookie != null && mapVersionCookie == 'true'))
	{
		redirectToMap(true); // redirect to old map (no GEarth, standard GMaps markers)
		return;
	}
	if(oldMap && (mapVersionCookie == null || mapVersionCookie != 'true'))
	{
		redirectToMap(false); // redirect to new map (GEarth and custom markers enabled)
		return;
	}
};
	
	
/* Global stuff */

$(document).ready(function () {
	cookieCheck();

    // load all instype information
    Legato.instypes = [];
    $.getJSON('/Instruments/AJAX/GetTypes', function (data) {
        $.each(data, function (i, n) {
            n.omniType = 'type'; // for omnibox autocomplete logic
            n.label = n.name; // for jQuery UI autocomplete
            Legato.instypes.push(n);
            $('<input type="radio" name="selectedType" />').val(n.id).attr('id', 'type-' + n.id).appendTo('#chooseTypeInputs');
            $('<label></label>').html(n.name).attr('for', 'type-' + n.id).appendTo('#chooseTypeInputs');
            $('<br/>').appendTo('#chooseTypeInputs');
        });
    });
    // add allType to omnibox
    var allType = {};
    allType.label = 'All';
    allType.name = 'All';
    allType.id = -1;
    allType.omniType = "type";
    Legato.instypes.push(allType);





// Configure slider choosing marker size
$("#chooseMarkerSize #markerSizeSelect").slider({
    value: 1,
    min: 1,
    max: 3,
    step: .5,
    slide: function (event, ui) {
        $("#chooseMarkerSize #amount").val(ui.value);
    }
});
$("#chooseMarkerSize #amount").val($("#chooseMarkerSize #markerSizeSelect").slider("value"));

$('#chooseMarkerSize .submitButton').click(function (e) {
    e.preventDefault(); // don't submit the form
    Legato.map.markerSize = parseInt($('#chooseMarkerSize #amount').val());
    Legato.map.updateMarkerSize(); // changes marker sizes
    $('#typeOverlayTrigger').data('overlay').close(); // close the overlay window
    return false; // so the form isn't submitted
});

/*$('#chooseKmlVisible .submitButton').click(function (e) {
    e.preventDefault(); // don't submit the form

    var isOn = $('input[name=kmlVisible]', '#chooseKmlVisible').is(':checked');
    Legato.map.toggleTimeZoneLayer(isOn);

    $('#typeOverlayTrigger').data('overlay').close(); // close the overlay window
    return false; // so the form isn't submitted
});*/

// handle button click in modal dialog
$('#chooseTypeForm #submitButton').click(function (e) {

    //debugAlert('handling click');
    e.preventDefault(); // don't submit the form
    var selected = $('input[name=selectedType]:checked', '#chooseTypeForm');

    if (selected == null || parseInt(selected.val()) == -1) {
        // if All is checked or none of them are checked, unfilter
        Legato.map.unfilterMarkers();
    }
    else {
        // else if one of them is checked, filter to its id
        Legato.map.filterMarkers(parseInt(selected.val()));
    }
    $('#typeOverlayTrigger').data('overlay').close(); // close the overlay window
    return false; // so the form isn't submitted
});

$('#typeOverlayTrigger').click(function (e) {
	if(!Legato.map.isEarthCurrentlyEnabled)
	{
		// configure modal dialog
		$("#typeOverlay").overlay({
			// some mask tweaks suitable for modal dialogs
			mask: 'gray',
			effect: 'apple',
			closeOnClick: true,
			load: true
		});
	}

	return false;
});

$('#helpText').click(function (e) {
	if(!Legato.map.isEarthCurrentlyEnabled)
	{
		// configure modal dialog
		$("#helpOverlay").overlay({
			// some mask tweaks suitable for modal dialogs
			mask: 'gray',
			effect: 'apple',
			closeOnClick: true,
			onBeforeLoad: function() {
				// grab wrapper element inside content
				var wrap = this.getOverlay().find('.modalContentWrap');
				
				// load the page specified in the overlay trigger
				wrap.load($('#helpText').attr('href'));
			},
			load: true
		});
	}

	return false;
});

$('#problems').click(function(e) {
	$.cookie(mapVersionCookieName, 'true', cookieSettings);
	cookieCheck();
});

$('#trybeta').click(function(e) {
	$.removeCookie(mapVersionCookieName, cookieSettings);
	cookieCheck();
});


if ($.browser.msie && $.browser.version <= 8) {
    $("#logo-wrap").css({marginTop: 0});
} else {
    /*
    * All animations which aren't IE
    * Compatible should go here
    */
    setTimeout(function() {
		
		// Show logo
		$("#logo-wrap").animate({
			"margin-top" : "0px",
			"opacity": 1.0
		}, 400, "easeOutExpo");
		
	}, 300);
    
    /**
     * Animate message count
     */
    
    var messageCounter = $("#insShown");
    messageCounter.delay(200).animate({
    	opacity: 1.0
    }, 650);
    
    if(messageCounter.length > 0) {
    	var container = $(messageCounter.find('p')[0]);
		var count = container.text().replace(/[^\d]/g, '');
    	var total = parseInt(count, 10);
    	container.text('0');
    	animateMessageCount( container, total );
    }
}

function animateMessageCount( container, total ) {
	
	var i,
		j,
		arr,
		str,
		count,
		rawCount = 0;
	
	var update = setInterval(function() {
		
		rawCount += (total - rawCount) * 0.15;
		count = Math.round(rawCount);
		
		arr = count.toString().split('');
		str = '';
		i = arr.length;
		j = 0;
		
		while(--i >= 0) {
			str = (i > 0 && (j++ % 3 == 2) ? ',' : '') + arr[i] + str;
		}
		
		container.text(str);
		
		if(count >= total) {
			clearInterval(update);
		}
		
	}, 1000 / 30);
}

setTimeout(function() {
	// Show footer
	$("footer").delay(300).animate({
		"bottom" : "0px"
	}, 300, "easeOutCirc");
	
}, 300);


/* Misc stuff */

/*if (!("autofocus" in document.createElement("input"))) { // autofocus (html5 element) fallback (see http://diveintohtml5.org/forms.html)
      $("#q").focus();
   } */

//$('input[title!=""]').hint(); // activates textbox hinting (see jquery.hint.js)

});

function changeUrlToLastDir() // TODO: update this to use the string reverse thing we use for new map redirect (scroll down)
{
	var x = window.location.href;

	var removeLast = 0; // how many characters to remove from end of a
	if(x[x.length - 1] == '/') // http://legatonetwork.com/instruments/
	{
		removeLast = 1;
	}
	else if(x[x.length - 1] == '#') // http://legatonetwork.com/instruments/# or http://legatonetwork.com/instruments#
	{
		if(x[x.length - 2] == '/') // http://legatonetwork.com/instruments/#
		{
			removeLast = 2;
		}
		else // http://legatonetwork.com/instruments#
		{
			removeLast = 1;
		}
	}
	
	return x.substr(0, x.length - removeLast);
}

String.prototype.reverse=function(){return this.split("").reverse().join("");}

function redirectToMap(old)
{
	var url = changeUrlToLastDir();
	
	if(old) // redirect to old map
	{
		window.location.href = url + '/' + 'old';
		return;
	}
	
	// redirect to new map
	var n = url.reverse().indexOf('/') // how far from the end of the url string is the last / (that slash begins '/old', which we want to get rid of)
	url = url.substr(0, url.length - n - 1); // remove that far from the end (including the slash itself)
	window.location.href = url;
}