using LinqToDB.Mapping;

namespace Models.Data
{
    public class BigData
    {
        [PrimaryKey]
        [Identity]
        public long Id { get; set; }
    }
}
