$(function () {
    var messageElement = $("p#message");

    var setMessage = function(message) {
        messageElement.hide(200);
        messageElement.text(message);
        messageElement.show(200);
    };

    $("input#Email").bind("focusin", function () {
        setMessage("Укажите, пожалуйста, свой реальный адрес электронной почты,"
                    + " чтобы на него пришло письмо подтверждения(без этого невозможно будет пользоваться сайтом).");
    });

    $("input#Name").bind("focusin", function () {
        setMessage("Это имя, под которым вас будут видеть другие пользователи."
                    + " В будущем вы сможете сменить его.");
    });

    $("input#Password").bind("focusin", function () {
        setMessage("Мы не накладываем никаких ограничений на ваш пароль, он может быть любым."
                    + " Всего лишь помните, что чем сложнее пароль, тем сложнее злоумышленнику будет взломать его.");
    });
})