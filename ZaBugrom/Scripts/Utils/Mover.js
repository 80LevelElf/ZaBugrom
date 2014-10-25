function MoveTo(action, controller) {
    var host = window.location.host;

    window.location.href = "http://" + host + "/" + controller + "/" + action;
}

function ActionPath(action, controller) {
    var host = window.location.host;

    return "http://" + host + "/" + controller + "/" + action;
}

function ActionPathByUrl(url) {
    var host = window.location.host;

    if (url.charAt(0) != '/')
        url = '/' + url;

    return "http://" + host + url;
}

function ParamValueOfAddress(paramName) {
    var result = "Not found",
        tmp = [];
    location.search
    //.replace ( "?", "" ) 
    // this is better, there might be a question mark inside
    .substr(1)
        .split("&")
        .forEach(function (item) {
            tmp = item.split("=");
            if (tmp[0] === paramName) result = decodeURIComponent(tmp[1]);
        });
    return result;
}