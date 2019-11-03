function getNewView(action, controller, val) {
    $.ajax({
        url: `/${action}/${controller}`,
        data: { fileName, val },
        success: function (text) {
            return text;
        },
    });
}