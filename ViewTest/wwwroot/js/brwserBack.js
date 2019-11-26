
history.replaceState(
    null, document.getElementsByTagName('title')[0].innerHTML, null
);

window.addEventListener('popstate', function (e) {
    this.alert(`${$(".stat-widget-one")[0].outerText}`);
    // var url = window.location.hostname;
    // if (url = "file:///C:/Users/TR/OneDrive/Program/C%23File/MVCFrameworkFile/GitRepository/ViewTest/Views/TextType.html") {
    //     this.alert(`${$(".stat-widget-one")[0].outerText}`);
    // }
});