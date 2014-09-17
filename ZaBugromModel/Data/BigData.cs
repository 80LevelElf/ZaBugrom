using BLToolkit.DataAccess;

namespace Models.Data
{
    public class BigData
    {
        [PrimaryKey]
        [Identity]
        [NonUpdatable]
        public long Id { get; set; }
    }
}
