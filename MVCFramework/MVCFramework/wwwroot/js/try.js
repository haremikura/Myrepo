function getNewView(action, controller, val) {
    $.ajax({
        url: `/${controller}/${action}`,
        data: { fileName: val },
        success: function (text) {
            return text;
        },
    });
}