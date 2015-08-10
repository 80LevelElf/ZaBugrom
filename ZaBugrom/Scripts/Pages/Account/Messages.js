$(function() {
    $($("div#profile-menu-div a")[0]).addClass("selected");

    $("div#filter-div label.setting-option").bind("click", ChangeMessageSettingsAndReturnNewList);
});

function ChangeMessageSettingsAndReturnNewList() {
    //Process click
    var option = $(event.target);
    if (option.hasClass("selected")) {
        option.removeClass("selected");
    } else {
        option.addClass("selected");
    }

    var isNewContent = $("div#filter-div label#new-content-filter").hasClass("selected");
    var isNotification = $("div#filter-div label#nofication-filter").hasClass("selected");
    var isUserMail = $("div#filter-div label#user-mail-filter").hasClass("selected");

    FillByCssLoad($("div#list-div"));

    $.ajax({
        type: "POST",
        url: ActionPath("ChangeMessageSettingsAndReturnNewList", "Account"),
        data: {
            isNewContent: isNewContent,
            isNotification: isNotification,
            isUserMail: isUserMail
        },
        success: function (data) {
            $("div#list-div").html(data);
        }
    });
}

