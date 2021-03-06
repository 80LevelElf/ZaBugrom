﻿$(function() {
    $("div#type-div #simple").addClass("selected");

    var titleElement = $("input#Title");
    var sourceElement = $("textarea[name='Source']");

    //Add tags
    $("input#Tags").textext(
    {
        enabled: true,
        plugins: 'tags'
    });

    //Add messager
    var messageElement = $("p#message");

    AddMessageHandler(titleElement, messageElement
    , "Заголовок вашего поста - надпись наверху, которую будут видеть люди. Придумайте, что-нибудь интересное.");

    AddMessageHandler(sourceElement, messageElement, "Здесь напишите то, о чем вы хотите рассказать. Вы можете использовать "
    + "специальную разметку для того, чтобы сделать ваш пост более наглядным!");

    //Add ok click event
    $("input.ok-button").bind("click", function (event) {
        event.preventDefault();

        var title = titleElement.val().trim();
        var source = sourceElement.val().trim();

        var separators = [',', '\\[', '\\]', '"'].join('|');
        var tagList = $("input[name='Tags'][type='hidden']").val().split(new RegExp(separators, 'g'))
            .filter(function (element) {
                return element.length != 0;
            });

        if (title == '') {
            SetMessage(messageElement, "Введите заголовок(первое поле)!");
            return;
        }

        if (source == '') {
            SetMessage(messageElement, "Вы не написали сам пост!");
            return;
        }

        source = PrepareNewPostForAdding(source);

         $.ajax({
            type: "POST",
            url: ActionPath("AddSimplePost", "AddPost"),
            data: JSON.stringify(
            {
                Title: title,
                Source: source,
                TagList: tagList
            }),
            success: function (data) {
                window.location.href = ActionPathByUrl(data);
            },
            contentType: 'application/json'
        });
    });
})