$(function () {
    $("div#type-div #video").addClass("selected");

    //Add messager
    var messageElement = $("p#message");

    AddMessageHandler($("input#Title"), messageElement
    , "Заголовок вашего поста - надпись наверху, которую будут видеть люди. Придумайте, что-нибудь интересное");

    AddMessageHandler($("input#Source"), messageElement, "Ссылка на видео, которые вы хотите показать. Пока поддерживается только Youtube. "
        + "Если вы не уверены как правильно вычислить ссылку, <a>посмотрите это</a>.");
})