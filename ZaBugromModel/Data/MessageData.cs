using System;
using Models.Data.Enums;

namespace Models.Data
{
    public class MessageData : BigData
    {
        public Int64 UserFromId { get; set; }
        public Int64 UserToId { get; set; }
        public string Title { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public bool IsReaded { get; set; }
    }
}
