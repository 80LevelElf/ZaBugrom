﻿using Models.Data.Enums;

namespace Models.Data
{
    public class MessageData
    {
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public string Title { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public bool IsReaded { get; set; }
    }
}