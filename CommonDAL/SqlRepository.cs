using System;
using System.Linq;
using CommonDAL.SqlDAL;
using LinqToDB;
using Models.Data;

namespace CommonDAL
{
    public class SqlRepository<T> : BaseSqlRepository<T> where T : Data 
    {
        public virtual int Insert(T entity)
        {
            using (var db = new DataBase())
            {
                return Convert.ToInt32(db.InsertWithIdentity(entity));
            }
        }

        public virtual T GetById(int id)
        {
            using (var db = new DataBase())
            {
                return db.GetTable<T>().FirstOrDefault(i => i.Id == id);
            }
        }
    }
}
