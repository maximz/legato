/* Author: 
 * 
*/

// Map functions
	Legato.map.init();
	
	// omnibox is initialized after all markers are added, not here

	google.maps.event.addListenerOnce(Legato.map.map, 'tilesloaded', function () {
		Legato.map.loadMarkerData();
	});

/* Global stuff */

$(document).ready(function() {

// configure modal dialog
$("#helpText").overlay({
 
	// some mask tweaks suitable for modal dialogs
	mask: 'gray',
	effect: 'apple',
	closeOnClick: true,
	onBeforeLoad: function() {
		// grab wrapper element inside content
		var wrap = this.getOverlay().find('.modalContentWrap');
		
		// load the page specified in the overlay trigger
		wrap.load(this.getTrigger().attr('href'));
	}
});

// configure modal dialog
$("#typeOverlayTrigger").overlay({
 
	// some mask tweaks suitable for modal dialogs
	mask: 'gray',
	effect: 'apple',
	closeOnClick: true,
	onBeforeLoad: function() {
		// todo: load pathologies via AJAX, add as radio buttons
		
		$.getJSON('/Instruments/AJAX/GetTypes',
		function (data) {
			$.each(data, function(i, n){
				$('<input type="radio" name="selectedType" />').val(n.id).attr('id','type-'+n.id).appendTo('#chooseTypeInputs'); 
				$('<label></label>').html(n.name).attr('for', 'type-'+n.id).appendTo('#chooseTypeInputs');
				$('<br/>').appendTo('#chooseTypeInputs');
			});
		});
		
		/* // grab wrapper element inside content
		var wrap = this.getOverlay().find('.modalContentWrap');
		
		// load the page specified in the overlay trigger
		wrap.load(this.getTrigger().attr('href')); */
	}
});

// handle button click in modal dialog
	$('#chooseTypeForm #submitButton').click(function(e) {
	
		//debugAlert('handling click');
		e.preventDefault(); // don't submit the form
		
		var selected = $('input[name=selectedType]:checked', '#chooseTypeForm');
		
		if(selected == null || parseInt(selected.val()) == -1)
		{
			// if All is checked or none of them are checked, unfilter
			Legato.map.unfilterMarkers();
			$('#typeOverlayTrigger').text('Filters Disabled');
		}
		else
		{
			// else if one of them is checked, filter to its id
			Legato.map.filterMarkers(parseInt(selected.val()));
			$('#typeOverlayTrigger').text('Filters Enabled');
		}
		$('#typeOverlayTrigger').data('overlay').close(); // close the overlay window
		
		return false; // so the form isn't submitted
		
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