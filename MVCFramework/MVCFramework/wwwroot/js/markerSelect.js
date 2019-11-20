//文章を選択したのアクション
$(document).click(function (event) {
    //入力した文章の要素を取得する
    getSelectText(event);
    //マーカーを選ぶポップアップを表示する
    viewRightPoput(event);

    function getSelectText() {
        global.setValue('currentText', $(event.target)[0]);
    }

    function viewRightPoput(event) {
        if ($(event.target).closest('.container').length) {
            var selectedStr;
            if (window.getSelection) {
                selectedStr
                    = window.getSelection().toString();
                global.setValue('selectStr', selectedStr);

                console.log(selectedStr.length + " " + selectedStr)

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

//マーカーを引く
$(".js_selectColor").click(function () {
    console.log(
        getMarkText(
            global.getValue('selectStr'),
            global.getValue('currentText'),
            ""));
})