using System.ComponentModel;

namespace Models.Data.Enums
{
    public enum Gender
    {
        [Description("Не указан")]
        NotSpecified = 0,
        [Description("Мужчина")]
        Male = 1,
        [Description("Женщина")]
        Female = 2
    }
}