$(function () { // kurzschreibweise für document.ready!
    var baseUrl = "http://localhost:15330";
    var accessToken = "";

    var saveAccessToken = function (data) {
        console.log(data);
        accessToken = data.access_token;
    };

    var showResponse = function (data) {
        $('#output').text(JSON.stringify(data, null, 4));
    };

    var login = function () {
        var url = baseUrl + "/token";
        var data = $('#userdata').serialize();
        data += "&grant_type=password";

        console.log(data);

        $.post(url, data)
            .done(saveAccessToken)
            .always(showResponse);

        return false;
    }

    var getData = function () {
        var url = baseUrl + "/api/Products";
        
        $.ajax(url, {
            method: "GET",
            headers: getHeaders()
        }).always(showResponse);
    }

    var getHeaders = function () {
        if (accessToken) {
            return {
                'Authorization': 'bearer ' + accessToken
            }
        }
    }

    $('#login').on("click", login);
    $('#getData').on("click", getData);
})