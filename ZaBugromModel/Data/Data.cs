using BLToolkit.DataAccess;

namespace Models.Data
{
    public class Data
    {
        [PrimaryKey]
        [Identity]
        [NonUpdatable]
        public int Id;
    }
}
