function SetMessage(messageOwner, message) {
    messageOwner.hide(200);
    messageOwner.empty();
    messageOwner.append(message);
    messageOwner.show(200);
}

function AddMessageHandler(dataElement, messageElement, message) {
    $(dataElement).bind("focusin", function () {
        SetMessage($(messageElement), message);
    });
}