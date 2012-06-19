$(function () {
	
	
    /* Name: Demo
    Author: Demo King */
    /*demo namespace*/
    demo = {
        common: {
            init: function () {
                //initialize
            },
            finalize: function () {
                //finalize
            },
            config: {
                prop: "my value",
                constant: "42"
            }
        },
        mapping: {
            init: function () {
                //create a map
            },
            geolocate: function () {
                //geolocation is cool
            },
            geocode: function () {
                //look up an address or landmark
            },
            drawPolylines: function () {
                //draw some lines on a map
            },
            placeMarker: function () {
                //place markers on the map
            }
        }
    }

    $('input[title!=""]').hint(); // activates textbox hinting (see jquery.hint.js)

    $('.dateinput').datepicker(); // activates jQuery UI datepicker control

    $('span.timeago').timeago(); // activate timeago
});
