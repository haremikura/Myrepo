//マーカー選択ポップアップ
//文章を選択したとき、マーカー
$(document).click(function (event) {
    //入力した文章の要素を取得する

    //マーカーを選ぶポップアップを表示する
    if ($(event.target).closest('.container').length) {

        if (window.getSelection) {

            var selectedStr = window.getSelection().toString();
            getSelectText(event);

            if (selectedStr.length > 0) {
                visibleRightPoput(event);
                markCurrentEleemet(event, true);
                getSelectionPosition();
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

    //ポップアップを開く
    function visibleRightPoput(event) {
        $('.right-popup')
            .css('top', event.pageY + 10)
            .css('left', event.pageX + 10)
            .css('visibility', 'visible');
    }

    //ポップアップを閉じる。
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
    markCurrentEleemet(event, false);
})

//現在選択した要素にマーカーを引く。
function markCurrentEleemet(event, isMark) {
    if (isMark) {
        $(event.target).addClass('currentSelect');
    } else {
        $('.currentSelect').removeClass('currentSelect');
    }

}

//選択した文の、文章中の距離となる情報を記録する。
function getSelectionPosition() {
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