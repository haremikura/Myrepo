$('#js_file_crate_button').click(function () {
    var button
        = getAjaxText(
            'CrateFile',
            'MvcHtmlString',
            { fileName: $('#file_crate_text_box').val() },
        );
    $('#new_file').replaceWith(button);
});
