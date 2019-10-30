$('.card_button').on({
    'mousedown': function () {
        $(this).css('transform', 'translateY(4px)');
        console.log("push");
    },
    'mouseup': function () {
        $(this).css('transform', '');
        // window.location.href = "Try2.html"
    }
});
