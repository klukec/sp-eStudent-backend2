/**
 * Created by Matic-ProBook on 27. 10. 2015.
 */
function writeAddressName(latLng) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
            "location": latLng
        },
        function(results, status) {
            if (status == google.maps.GeocoderStatus.OK)
                document.getElementById("address").innerHTML = results[0].formatted_address;
            else
                document.getElementById("error").innerHTML += "Vašega naslova ni mogoèe pridobiti." + "<br />";
        });
}

function geolocationSuccess(position) {
    var userLatLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    // Write the formatted address
    writeAddressName(userLatLng);

    var myOptions = {
        zoom : 16,
        center : userLatLng,
        mapTypeId : google.maps.MapTypeId.ROADMAP
    };
    // Draw the map
    var mapObject = new google.maps.Map(document.getElementById("map"), myOptions);
    // Place the marker
    new google.maps.Marker({
        map: mapObject,
        position: userLatLng
    });
    // Draw a circle around the user position to have an idea of the current localization accuracy
    var circle = new google.maps.Circle({
        center: userLatLng,
        radius: position.coords.accuracy,
        map: mapObject,
        fillColor: '#0000FF',
        fillOpacity: 0.5,
        strokeColor: '#0000FF',
        strokeOpacity: 1.0
    });
    mapObject.fitBounds(circle.getBounds());

    var mojaLokacijaA = position.coords.latitude;
    var mojaLokacijaB = position.coords.longitude;

    var povezava = "https://maps.google.com?daddr=46.0500176, 14.4668417&saddr=";
    povezava = povezava + mojaLokacijaA + "," + mojaLokacijaB;
    //console.log(povezava);
    //<a href="povezava" title="Navodila za pot" target="_blank">gumb</a>
    $("#povezavaOdpri").attr("href", povezava);
}

function geolocationError(positionError) {
    document.getElementById("error").innerHTML += "Error: " + positionError.message + "<br />";
}

function geolocateUser() {
    // If the browser supports the Geolocation API
    if (navigator.geolocation)
    {
        var positionOptions = {
            enableHighAccuracy: true,
            timeout: 10 * 1000 // 10 seconds
        };
        navigator.geolocation.getCurrentPosition(geolocationSuccess, geolocationError, positionOptions);
    }
    else
        document.getElementById("error").innerHTML += "Vaš brskalnik ne podpira Geolocation API-ja.";
}

window.onload = geolocateUser;