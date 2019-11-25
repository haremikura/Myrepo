//文章を選択したのアクション
$(document).click(function (event) {
    //入力した文章の要素を取得する

    //マーカーを選ぶポップアップを表示する

    if ($(event.target).closest('.container').length) {

        if (window.getSelection) {

            var selectedStr = window.getSelection().toString();
            getSelectText(event);

            if (selectedStr.length > 0) {
                visibleRightPoput(event);
                markCurrentEiemet(event, true);
            } else if (selectedStr.length == 0) {
                hiddenRightPoput();
            }
        }
    } else {
        hiddenRightPoput();
    }


    function getSelectText() {
        global.setValue('selectStr', selectedStr);
    }

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
});

//マーカーを引く
$('.js_markText').children('li').click(function () {

    var cha
        = getAjaxText(
            "MarkText",
            "TextEditor",
            {
                elementText: $('.currentSelect').text(),
                markedText: global.getValue('selectStr'),
                caretPosition: global.getValue('caretPosition'),
                colorCode: $(this).find('.themeColor_indigator').css('background-color'),
            });

    $('.currentSelect').text('').html(cha)
    markCurrentEiemet(event, false);
})

function markCurrentEiemet(event, isMark) {
    if (isMark) {
        $(event.target).addClass('currentSelect');
    } else {
        $('.currentSelect').removeClass('currentSelect');
    }

}

function getCaretPosition() {
    var sel = document.getSelection();
    sel.modify("extend", "backward", "paragraphboundary");
    var pos = sel.toString().length;
    if (sel.anchorNode != undefined) sel.collapseToEnd();
    global.setValue('caretPosition', pos);
}