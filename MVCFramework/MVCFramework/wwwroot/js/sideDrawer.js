bodyDrawerConst = {
    hiddenRihgt: '-300px',
    visibleRight: '0px',
}

$(function () {
    setSidebarResize();
})

$(document).click(function (event) {
    if (canCloseSide(event)) {
        closeNav();
    }
});

//ウインドウがリサイズされたら発動
$(window).resize(function () {
    setSidebarResize();
});

$('.navbar').resize(function () {
    setSidebarResize();
});

$('.nav_selector').click(function () {
    viewSideBarContent($(this).attr('id'));
    if ($(".sidenav_body").css('right')
        == bodyDrawerConst.hiddenRihgt) {
        openNav();
    }
})

$('.closebtn').click(function () {
    closeNav();
})

function canCloseSide(event) {
    return !$(event.target).closest('.sidenav_body').length
        && !$(event.target).closest('.nav_selector').length
        && $(".sidenav_body").css('right') == bodyDrawerConst.visibleRight;
}

function closeNav() {
    $(".sidenav_body").css('right', bodyDrawerConst.hiddenRihgt);
    $("#sidenav_overray").css('visibility', 'hidden');
}

function openNav() {
    $(".sidenav_body").css('right', bodyDrawerConst.visibleRight);
    $("#sidenav_overray").css('visibility', 'visible');
}

function setSidebarResize() {
    var top = $(".navbar").height() + 15;
    $(".sidenav_body").css('top', top);
    $("#sidenav_overray").css('top', top);
}

function viewSideBarContent(name) {
    $('.sidebar_cotent').css('display', 'none');
    $(`.sidebar_cotent[name=${name}]`).css('display', 'block');
}