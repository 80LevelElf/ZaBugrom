$(function () {

    //For somee.com
    $("a[href='http://somee.com']").hide();

    //Try to load header image (we do it to get better performance)
    $.post(ActionPath("CurrentHeaderImage", "Helper"), function (headerImage) {
        var header = $("header");
        header.css(
        {
            "background": "url(\"http://" + window.location.host + "/Content/images/HeaderImages/" + headerImage.FileName + "\")",
            "background-position-y": headerImage.ShiftByTop + "%",
            "background-repeat" : "no-repeat",
            "background-size" : "cover"
        });

        $("label#city-name").text(headerImage.Title);
    });
})