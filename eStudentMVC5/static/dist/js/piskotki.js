/**
 * Created by Matic-ProBook on 27. 10. 2015.
 */
var novUser = null;
var starUser = null;
$(document).ready(function () {
    novUser = Cookies.get('alreadyHere');
    if (novUser == null) {
        Cookies.set('alreadyHere', 'true', {expires: 365});
        $("#piskotek").text("(nov uporabnik)");
    }
    else
        $("#piskotek").text("(already here)");
});