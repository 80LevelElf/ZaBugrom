using System;
using System.Linq;
using LinqToDB;
using Models.Data;

namespace CommonDAL
{
    public class BigSqlRepository<T> : BaseSqlRepository<T> where T : BigData 
    {
        public virtual Int64 Insert(T entity)
        {
            using (var db = new DataBase())
            {
                return Convert.ToInt64(db.InsertWithIdentity(entity));
            }
        }

        public virtual T GetById(long id)
        {
            using (var db = new DataBase())
            {
                return db.GetTable<T>().FirstOrDefault(i => i.Id == id);
            }
        }
    }
}
