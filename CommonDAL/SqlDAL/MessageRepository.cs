using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<MessageData> GetList(int userId, int page, int pageSize,
            bool isNewContent, bool isNotification, bool isUserMail)
        {
            using (var db = new DataBase())
            {
               return db.MessageTable
                    .Where(i => i.UserToId == userId && (
                        (isNewContent && i.MessageType == MessageType.NewContent)
                        || (isNotification && i.MessageType == MessageType.Notification)
                        || (isUserMail && i.MessageType == MessageType.UserMail)))
                    .OrderByDescending(i => i.Id).Skip((page - 1)*pageSize).Take(pageSize).ToList();
            }
        }

        public List<MessageData> GetList(MessageSettingsData settings, int page, int pageCount)
        {
            return GetList(
                settings.UserId,
                page,
                pageCount,
                settings.IsNewContent,
                settings.IsNotification,
                settings.IsUserMail);
        }
    }
}