﻿using System;
using System.Linq;
using CommonDAL.SqlDAL;
using LinqToDB;
using Models.Data;

namespace CommonDAL
{
    public class SqlRepository<T> : BaseSqlRepository<T> where T : Data 
    {
        public int Insert(T entity)
        {
            using (var db = new DataBase())
            {
                return Convert.ToInt32(db.InsertWithIdentity(entity));
            }
        }

        public T GetById(int id)
        {
            using (var db = new DataBase())
            {
                return db.GetTable<T>().FirstOrDefault(i => i.Id == id);
            }
        }
    }
}
