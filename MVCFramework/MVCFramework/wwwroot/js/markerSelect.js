$(document).click(function (event) {
    if ($(event.target).closest('.container').length) {
        var selectedStr;
        if (window.getSelection) {  //selectionオブジェクト取得
            selectedStr
                = window.getSelection().toString();  //文章取得
                    if (selectedStr.length > 0) {
                visibleRightPoput(event);
            } else if (selectedStr.length == 0) {
                hiddenRightPoput();
            }
        }
    } else {
        hiddenRightPoput();
     //   console.log('内側がクリックされました。');
    }
});

function visibleRightPoput(event) {
    $('.right-popup')
        .css('top', event.pageY + 10)
        .css('left', event.pageX + 10)
        .css('visibility', 'visible');
}

function hiddenRightPoput() {
    $('.right-popup')
        .css('visibility', 'hidden');
}