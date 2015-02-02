using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using Models.Data;
using Models.Data.Enums;
using Models.Data.Settings;

namespace CommonDAL.SqlDAL
{
    public class MessageRepository : BigSqlRepository<MessageData>
    {
        public override long Insert(MessageData instance)
        {
            instance.AddTime = DateTime.Now;
            using (var db = new DataBase())
            {
                db.BeginTransaction();

                var id = (long) db.InsertWithIdentity(instance);

                db.UserTable
                    .Where(i => i.Id == instance.UserToId)
                    .Set(i => i.MessageCount, i => i.MessageCount + 1)
                    .Update();

                db.CommitTransaction();

                return id;
            }
        }

        public List<MessageData> GetList(int userId, int page, int pageSize, MessageSettingsData settings)
        {
            using (var db = new DataBase())
            {                           
                return db.MessageTable
                    .Where(i => i.UserToId == userId && (
                        (settings.IsNewContent && i.MessageType == MessageType.NewContent)
                        || (settings.IsNotification && i.MessageType == MessageType.Notification)
                        || (settings.IsUserMail && i.MessageType == MessageType.UserMail)))
                    .OrderByDescending(i => i.Id).Skip((page - 1)*pageSize).Take(pageSize).ToList();
            }
        }
    }
}