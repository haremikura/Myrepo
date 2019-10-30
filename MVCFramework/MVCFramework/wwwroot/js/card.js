$('.card_button').on({
    'mousedown': function () {
        $(this).css('transform', 'translateY(4px)');

        var url = $(this).attr('data-action');
        if (url !== undefined)
            window.location.href = url;
    },
    'mouseup': function () {
        $(this).css('transform', '');
        // window.location.href = "Try2.html"
    }
});