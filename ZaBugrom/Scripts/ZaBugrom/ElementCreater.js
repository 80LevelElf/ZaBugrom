/*  Elements
dialog-buttons = div with submit and cancel button
separate-line = line with 100% width to separate
___________________________________
  Events
browser-back = back to the preview page of browser
*/

$(function() {
    //This code create different elements with JQuiery UI or with my code with special attributes

    //------Elements
    //dialog-buttons
    $("div[data-create='dialog-buttons']")
        .addClass("dialog-buttons-div")
        .append("<input type='submit' value='OK' />")
        .append("<button data-event='browser-back'>Назад</button>");

    //separate-lie
    $("div[data-create='separate-line']").addClass("separate-line");

    //------Events
    $("*[data-event='browser-back']").bind("click", function (event) {
        history.back();
        event.preventDefault();
    });
    $("*[data-event='browser-to-main']").bind("click", function () {
        MoveTo("Index", "Home");
    });
})