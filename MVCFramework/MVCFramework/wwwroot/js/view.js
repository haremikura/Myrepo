
$('#js_file_crate_button').click(function () {
    $('#new_file')
        .replaceWith(
            getNewView('CrateFile', 'TextEditor', $('#file_crate_text_box').val())
        );
});
