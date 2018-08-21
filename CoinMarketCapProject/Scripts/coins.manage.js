var cookieIds = [];

$(document).ready(function () {
    $(".tablesorter").tablesorter({
        theme: 'blue',
        headerTemplate: '{content} {icon}', 
        widgets: ['zebra', 'filter', 'resizable'],
    });

    getCookies();
    setInterval(getTable, 60000);
    initEvents();
})

function initEvents() {
    $(".favoriteCheckbox").on('click', function () {
        var name;
        if ($(this).prop('checked') === true) {
            var currTr = $(this).parent().parent();
            currTr.insertBefore('table > tbody > tr:first');
            name = currTr.find(".coinName").attr("id");
            if (cookieIds.indexOf(name) === -1) {
                cookieIds.push(name);
                $.cookie("cookieIds", cookieIds.join(','));
            }
        } else {
            name = $(this).parent().parent().find(".coinName").attr("id");
            var index = cookieIds.indexOf(name);
            if (index !== -1) {
                cookieIds.splice(index, 1);
                $.cookie("cookieIds", cookieIds.join(','));
            }
        }
        $(".tablesorter").trigger("update");
    })
}

function getCookies() {
    var cookie = $.cookie("cookieIds");
    cookieIds = cookie ? cookie.split(/,/) : new Array();
    if (cookieIds.length > 0) {
        for (var i = 0; i < cookieIds.length; i++){
            var currTr = $(".tablesorter").find("#" + cookieIds[i]).parent();
            currTr.insertBefore('table > tbody > tr:first');
            currTr.find(".favoriteCheckbox").prop('checked', true);
        }
        $(".tablesorter").trigger("update");
    }
}

function getTable() {
    $("#loadingGif").show();
    var url = "/Home/GetTable"
    $.ajax({
        url: url,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $(".tablesorter tbody").html(data);
            $(".tablesorter").trigger("update");
            getCookies();
            initEvents();
            setTimeout(function () {
                $("#loadingGif").hide();
            }, 3000);
        },
        error: function (response) {
            if (parseInt(response.status) == 401) {
                location.reload();
            } else {
                alert("Произошла непредвиденная ошибка");
            }
            getCookies();
            initEvents();
            setTimeout(function () {
                $("#loadingGif").hide();
            }, 3000);
        }
    });
}