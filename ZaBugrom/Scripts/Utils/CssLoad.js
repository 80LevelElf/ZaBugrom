function FillByCssLoad(element) {
    $.ajax({
        type: "POST",
        url: ActionPath("CssLoad", "Element"),
        success: function (data) {
            $(element).html(data);
        }
    });
}