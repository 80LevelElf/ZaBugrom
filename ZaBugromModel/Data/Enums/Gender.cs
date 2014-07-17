using System.ComponentModel;

namespace Models.Data.Enums
{
    public enum Gender
    {
        [Description("Не указан")]
        NotSpecified = 1,
        [Description("Мужчина")]
        Male = 2,
        [Description("Женщина")]
        Female = 3
    }
}
