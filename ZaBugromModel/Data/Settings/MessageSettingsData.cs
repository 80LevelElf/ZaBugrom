namespace Models.Data.Settings
{
    public class MessageSettingsData
    {
        public MessageSettingsData(int userId, bool isNewContent, bool isNotification, bool isUserMail)
        {
            UserId = userId;
            IsNewContent = isNewContent;
            IsNotification = isNotification;
            IsUserMail = isUserMail;
        }

        public MessageSettingsData()
        {
            IsNewContent = true;
            IsNotification = true;
            IsUserMail = true;
        }

        public bool IsNewContent { get; set; }
        public bool IsNotification { get; set; }
        public bool IsUserMail { get; set; }
        public int UserId { get; set; }
    }
}
