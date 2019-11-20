function getNewView(action, controller, val) {
    var answet = $.ajax({
        url: `/${controller}/${action}`,
        data: { fileName: val },
        type: 'POST',
        async: false,
        success: function (text) {
            return text;
        },
    });

    return answet.responseText;
}

function getMarkText(htmlElement, markText, colorCode) {
    var answet = $.ajax({
        url: `/$TextEditor/CrateFileView`,
        data: {
            htmlElement: htmlElement,
            markText: markText,
            colorCode: colorCode
        },
        async: false,
        success: function (text) {
            return text;
        },
    });

    return answet.responseText;
}