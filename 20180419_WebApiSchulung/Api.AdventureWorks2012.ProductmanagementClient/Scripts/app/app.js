$(document).ready(function () {

    $('.products-container').html('Loading data please wait...');

    $.ajax({
        url: "http://localhost:3987/api/Product",
        method: "GET"
    }).done(function (data) {
        console.log(data); 
        for (var i = 0; i < data.length; i++) {

            // Beachte WebApiConfig 
            // => settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // => deswegen ist name in data[i].name kleingeschrieben!
            $('.products-container').append('<p>' + data[i].name + '</p>')
        }
        
    });
});