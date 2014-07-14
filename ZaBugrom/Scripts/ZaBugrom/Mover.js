function MoveTo(action, controller) {
    var host = window.location.host;

    window.location.href = "http://" + host + "/" + controller + "/" + action;
    //http://localhost:28832/Account/Login
}