$('.card_button').on({
    'mousedown': function () {
        $(this).css('transform', 'translateY(4px)');
    },
    'mouseup': function () {
        $(this).css('transform', '');
    }
});