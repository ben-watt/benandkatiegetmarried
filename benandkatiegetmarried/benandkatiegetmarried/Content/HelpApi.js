/// reference path="https://code.jquery.com/jquery-3.2.1.min.js"

(function helpApi($) {

    $('document').ready((ev) => {
        $(".method:contains('GET')").css('color', 'green');
        $(".method:contains('POST')").css('color', 'black');
        $(".method:contains('PUT')").css('color', 'orange');
        $(".method:contains('DELETE')").css('color', 'red');
    });

    $('.modules').click((ev) => {

        var moduleId = ev.delegateTarget.id.toString().replace('.', '\\.');
        var routes = $('#' + moduleId).children('.routes');

        routes.toggle();

    });

})($);