
$(document).click(function (event) {
    getSelectText(event);
    viewRightPoput(event);

    function getSelectText() {
        global.setValue('currentText', $(event.target)[0]);
    }

    function viewRightPoput(event) {
        if ($(event.target).closest('.container').length) {
            var selectedStr;
            if (window.getSelection) {  //selectionオブジェクト取得
                selectedStr
                    = window.getSelection().toString();
                global.setValue('selectStr', selectedStr);
                //console.log(selectedStr.length + " " + selectedStr)
                if (selectedStr.length > 0) {
                    visibleRightPoput(event);
                } else if (selectedStr.length == 0) {
                    hiddenRightPoput();
                }
            }
        } else {
            hiddenRightPoput();
            console.log('内側がクリックされました。');
        }
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






///////////

$(".js_selectColor").click(function () {
    var getString = global.getValue('selectStr');
    var allstring = global.getValue('currentText');
    console.log(allstring);
})