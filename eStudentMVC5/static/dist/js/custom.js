/**
 * Created by Matic-ProBook on 3. 11. 2015.
 */

$( "#foo" ).load( "chat.html" );
$("#odpriChat").click(function () {
    if ($("#foo").is(":hidden")) {
        $("#foo").show("slow");
    } else {
        $("#foo").slideUp();
    }
});