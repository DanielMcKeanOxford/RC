$(document).ready(function () {
    $(".js-row-click-table tbody tr").addClass("js-wholeRowClick");
    $("tr.js-wholeRowClick").click(function () {
        var link = $(".js-row-click", this);
        var sel = getSelection().toString();
        if (link) {
            if (!sel) {
                if (link.attr("href"))
                    location.href = link.attr("href");
                else {
                    link.removeClass("js-row-click");
                    link.click();
                }
            }
        }
    });
});