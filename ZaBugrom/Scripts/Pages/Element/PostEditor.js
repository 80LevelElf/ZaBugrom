$(function () {
    $("div.post-editor-area div.tool-area select").selectmenu();

    //Textarea
    var textarea = $("textarea[name='Source']");
    textarea.autosize();

    //Add click events
    $("div.post-editor-area div.tool-area a").bind("click", function() {
        var toolItem = $(this);
        var data = toolItem.attr("data-value");

        textarea.insertAtCaret(data);
    });

    $("div.post-editor-area div.tool-area select.header").bind("change", function () {
        var toolItem = $(this).children(":selected");
        var data = toolItem.attr("data-value");

        textarea.insertAtCaret(data);
    });
});