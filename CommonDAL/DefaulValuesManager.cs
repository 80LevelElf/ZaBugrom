using Models.Data.Settings;

namespace CommonDAL
{
    public static class DefaulValuesManager
    {
        public static MessageSettingsData GetMessageSettings(int userId)
        {
            return new MessageSettingsData(userId, true, true, true);
        }
    }
}
