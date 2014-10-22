using LinqToDB.Mapping;

namespace Models.Data
{
    public class Data
    {
        [PrimaryKey]
        [Identity]
        public int Id { get; set; }
    }
}
