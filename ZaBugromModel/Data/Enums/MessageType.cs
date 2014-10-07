using System.ComponentModel;

namespace Models.Data.Enums
{
    public enum MessageType
    {
        [Description("Новый контент")]
        NewContent = 0,
        [Description("Уведомление")]
        Notification = 1,
        [Description("Сообщение от пользователя")]
        UserMail = 2
    }
}
