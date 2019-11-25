
$(document).click(function (event) {
    getSelectText(event);

    viewRightPoput(event);


    function getSelectText() {
        global.setValue('currentText', $(event.target)[0]);
    }

    function viewRightPoput(event) {
        if ($(event.target).closest('.container').length) {

            if (window.getSelection) {

                var selectedStr = window.getSelection().toString();
                global.setValue('selectStr', selectedStr);

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
    }
    console.log(getCaretPosition());

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


$('.js_markText').children('li').click(function () {

    console.log($(this).find('.themeColor_indigator').css('background-color'));
    var allstring = $('.currentSelect').text();

    var cha
        = allstring.replace(
            global.getValue('selectStr'),
            `<span style="background:${"#aaa"};">${global.getValue('selectStr')}</span>`);
    console.log(cha);
    $('.currentSelect').html(cha)
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
    return pos;
}