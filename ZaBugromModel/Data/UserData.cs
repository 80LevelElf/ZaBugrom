using Models.Data.Enums;

namespace Models.Data
{
    public class UserData:Data
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public Gender Gender { get; set; }
        public int Rating { get; set; }
    }
}
