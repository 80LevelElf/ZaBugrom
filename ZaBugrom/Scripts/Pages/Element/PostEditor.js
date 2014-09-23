$(function() {
    //Textarea
    var textarea = $("textarea[name='Source']");
    textarea.autosize();

    //Add click events
    $("div.post-editor-area div.tool-area a").bind("click", function() {
        var toolItem = $(this);
        var data = toolItem.attr("data-value");

        textarea.insertAtCaret(data);
    });
});