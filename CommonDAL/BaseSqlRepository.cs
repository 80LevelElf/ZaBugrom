using System.Collections.Generic;
using System.Linq;
using CommonDAL.SqlDAL;
using LinqToDB;

namespace CommonDAL
{
    public class BaseSqlRepository<T> where T : class
    {
        public void Update(T entity)
        {
            using (var db = new DataBase())
            {
                db.Update(entity);
            }
        }

        public List<T> GetList()
        {
            using (var db = new DataBase())
            {
                return db.GetTable<T>().ToList();
            }
        }

        public void Delete(T entity)
        {
            using (var db = new DataBase())
            {
                db.Delete(entity);
            }
        }
    }
}
