$(function () {
    $("div#type-div #video").addClass("selected");

    //Add messager
    var messageElement = $("p#message");

    var setMessage = function (message) {
        messageElement.hide(200);
        messageElement.empty();
        messageElement.append(message);
        messageElement.show(200);
    };

    $("input#Title").bind("focusin", function () {
        setMessage("Заголовок вашего поста - надпись наверху, которую будут видеть люди. Придумайте, что-нибудь интересное =)");
    });

    $("input#Source").bind("focusin", function () {
        setMessage("Ссылка на видео, которые вы хотите показать. Пока поддерживается только Youtube. "
                + "Если вы не уверены как правильно вычислить ссылку, <a>посмотрите это</a>.");
    });
})