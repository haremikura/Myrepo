function getNewView(action, controller, val) {
    var answet = $.ajax({
        url: `/${controller}/${action}`,
        data: { fileName: val },
        async: false,
        success: function (text) {
            return text;
        },
    });

    return answet.responseText;
}