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
                getCaretPosition();
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
            "GetMarkText",
            "MvcHtmlString",
            {
                elementText: $('.currentSelect')[0].innerHTML,
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
    var selectedStr = {
        anchorNode: sel.anchorNode,
        anchorOffset: sel.anchorOffset,
        focusNode: sel.focusNode,
        focusOffset: sel.focusOffset
    };
    sel.modify("extend", "backward", "paragraphboundary");
    var pos = sel.toString().length;
    global.setValue('caretPosition', pos);
    sel.setBaseAndExtent(
        selectedStr.anchorNode,
        selectedStr.anchorOffset,
        selectedStr.focusNode,
        selectedStr.focusOffset);

}