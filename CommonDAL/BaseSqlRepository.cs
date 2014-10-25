﻿using System.Collections.Generic;
using System.Linq;
using CommonDAL.SqlDAL;
using LinqToDB;

namespace CommonDAL
{
    public class BaseSqlRepository<T> where T : class
    {
        public virtual void Update(T entity)
        {
            using (var db = new DataBase())
            {
                db.Update(entity);
            }
        }

        public virtual List<T> GetList()
        {
            using (var db = new DataBase())
            {
                return db.GetTable<T>().ToList();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var db = new DataBase())
            {
                db.Delete(entity);
            }
        }
    }
}
