using System.Linq;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class TagRepository : BigSqlRepository<TagData>
    {
        public TagData GetByName(string name)
        {
            using (var db = new DataBase())
            {
                return db.TagTable.FirstOrDefault(i => i.Name == name);
            }
        }
    }
}
