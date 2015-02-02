using System.Linq;
using LinqToDB;
using Models.Data.Settings;

namespace CommonDAL.SqlDAL
{
    public class MessageSettingsRepository : BaseSqlRepository<MessageSettingsData>
    {
        public virtual void Insert(MessageSettingsData entity)
        {
            using (var db = new DataBase())
            {
                db.InsertWithIdentity(entity);
            }
        }

        public virtual MessageSettingsData GetById(int userId)
        {
            MessageSettingsData result;

            using (var db = new DataBase())
            {
                result = db.GetTable<MessageSettingsData>().FirstOrDefault(i => i.UserId == userId);
            }

            if (result == null)
            {
                result = DefaulValuesManager.GetMessageSettings(userId);
                Insert(result);
            }

            return result;
        }
    }
}
