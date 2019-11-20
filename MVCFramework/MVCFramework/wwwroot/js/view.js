$('#js_file_crate_button').click(function () {
    var button = getNewView('CrateFile', 'TextEditor', $('#file_crate_text_box').val());
    $('#new_file').replaceWith(button);
});

window.onbeforeunload = function () { location.reload(); };