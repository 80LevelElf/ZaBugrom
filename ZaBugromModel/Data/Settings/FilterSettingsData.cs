using LinqToDB.Mapping;
using Models.Data.Enums.Settings;

namespace Models.Data.Settings
{
    public class FilterSettingsData
    {
        public FilterSettingsData(int userId, bool sourceFromSite, bool sourceFromYoutube, bool sourceFromLiveJournal, FilterSortType sortType)
        {
            UserId = userId;
            SourceFromSite = sourceFromSite;
            SourceFromYoutube = sourceFromYoutube;
            SourceFromLiveJournal = sourceFromLiveJournal;
            SortType = sortType;
        }

        public FilterSettingsData()
        {
            SourceFromSite = false;
            SourceFromYoutube = true;
            SourceFromLiveJournal = true;
            SortType = FilterSortType.ByTime;
        }

        public bool SourceFromSite { get; set; }
        public bool SourceFromYoutube { get; set; }
        public bool SourceFromLiveJournal { get; set; }

        public FilterSortType SortType { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }
    }
}
