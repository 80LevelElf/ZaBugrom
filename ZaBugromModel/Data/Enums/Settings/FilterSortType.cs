using System.ComponentModel;

namespace Models.Data.Enums.Settings
{
    public enum FilterSortType
    {
        [Description("По времени")]
        ByTime = 0,
        [Description("По рейтингу")]
        ByRating = 1
    }
}
