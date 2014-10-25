/*  Elements
dialog-buttons = div with submit and cancel button
dialog-save = div with submit-"refresh" button
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
        .append("<input class='ok-button' type='submit' value='OK' />")
        .append("<button class='cancel-button' data-event='browser-back'>Назад</button>");

    //refresh-button
    $("div[data-create='dialog-save']")
        .addClass("dialog-save-div")
        .append("<input type='submit' value='Сохранить' />");

    //separate-lie
    $("div[data-create='separate-line']").addClass("separate-line");

    //------Events
    $("*[data-event='browser-back']").bind("click", function (event) {
        history.back();
        event.preventDefault();
    });

    $("*[data-event='browser-to-main']").bind("click", function (event) {
        if (event.target == this) {
            MoveTo("Index", "Home");
        }
    });
})