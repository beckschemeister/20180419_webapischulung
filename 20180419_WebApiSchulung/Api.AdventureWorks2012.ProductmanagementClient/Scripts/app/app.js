$(document).ready(function () {

    $('.products-container').html('Hello API');

    $.ajax({
        url: "http://localhost:3987/api/Product",
        method: "GET"
    }).done(function (data) {
        console.log(data); // hier haben wir jetzt eine Fehlermeldung bzgl. CORS-Problematik im Log stehen.
    });
});