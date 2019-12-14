

function getMarkText(htmlElement, markText, colorCode) {
    var answet = $.ajax({
        url: `/MvcHtmlString/CrateFileView`,
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



function getAjaxText(action, controller, dataset) {
    var answet = $.ajax({
        url: `/${controller}/${action}`,
        data: dataset,
        //type: 'POST',
        async: false,
        success: function (text) {
            return text;
        },
    });

    return answet.responseText;
}