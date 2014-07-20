function MoveTo(action, controller) {
    var host = window.location.host;

    window.location.href = "http://" + host + "/" + controller + "/" + action;
}

function ActionPath(action, controller) {
    var host = window.location.host;

    return "http://" + host + "/" + controller + "/" + action;
}