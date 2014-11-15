using System;
using LinqToDB.Mapping;
using Models.Data.Enums;

namespace Models.Data
{
    public class UserData : Data
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarName { get; set; }
        public Gender Gender { get; set; }
        public int Rating { get; set; }
        public int MessageCount { get; set; }
        public DateTime AddTime { get; set; }
    }
}
