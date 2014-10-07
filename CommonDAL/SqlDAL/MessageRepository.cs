using System;
using System.Collections.Generic;
using System.Dynamic;
using BLToolkit.DataAccess;
using Models.Data;
using Models.Data.Settings;

namespace CommonDAL.SqlDAL
{
    public abstract class MessageRepository : AbstractSqlRepository<MessageData>
    {
        [SprocName("spMessageInsert")]
        public new abstract string Insert(MessageData instance);

        [SprocName("spMessageGetListBySettings")]
        public abstract List<MessageData> GetList(int userId, Int64 indexFrom, Int64 count,
            bool isNewContent, bool isNotification, bool isUserMail);

        public List<MessageData> GetList(MessageSettingsData settings, Int64 indexFrom, Int64 count)
        {
            return GetList(
                settings.UserId,
                indexFrom,
                count,
                settings.IsNewContent,
                settings.IsNotification,
                settings.IsUserMail);
        }
    }
}