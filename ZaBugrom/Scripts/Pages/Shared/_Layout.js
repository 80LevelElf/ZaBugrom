$(function () {

    //Try to load header image (we do it to get better performance)
    $.post(ActionPath("CurrentHeaderImage", "Helper"), function (headerImage) {
        var header = $("header");
        header.css(
        {
            "background": "url(\"http://" + window.location.host + "/Content/images/HeaderImages/" + headerImage.FileName + "\")",
            "background-position": "-" + headerImage.ShiftByX + "px -" + headerImage.ShiftByY + "px",
            "background-repeat" : "no-repeat",
            "background-size" : "cover"
        });

        $("label#city-name").text(headerImage.Title);
    });
})