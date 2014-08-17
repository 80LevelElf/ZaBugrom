using System;
using System.Collections.Generic;
using BLToolkit.Data;
using BLToolkit.Data.DataProvider;
using BLToolkit.DataAccess;

namespace CommonDAL.SqlDAL
{
    public abstract class AbstractSqlRepository : DataAccessor
    {
        protected static string ConnectionString { get; set; }

        public static DbManager GetNewDbManager()
        {
            return new DbManager(new SqlDataProvider(), ConnectionString);
        }

        public static void SetDataBaseConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }

    public abstract class AbstractSqlRepository<T> : AbstractSqlRepository, IRepository<T>
    {
        protected AbstractSqlRepository()
        {
            if (ConnectionString == null)
            {
                throw new NullReferenceException("Please, initialize DbManager before using AbstractSqlRepository.");
            }
        }

        public T GetById(int id)
        {
            using (var dbManager = GetNewDbManager())
            {
                return new SqlQuery<T>(dbManager).SelectByKey(id);
            }
        }

        public List<T> GetList()
        {
            using (var dbManager = GetNewDbManager())
            {
                return new SqlQuery<T>(dbManager).SelectAll();
            }
        }

        public int Insert(T instance)
        {
            using (var dbManager = GetNewDbManager())
            {
                return new SqlQuery<T>(dbManager).Insert(instance);
            }
        }

        public int Insert(IEnumerable<T> instanceList)
        {
            using (var dbManager = GetNewDbManager())
            {
                return new SqlQuery<T>(dbManager).Insert(instanceList);
            }
        }

        public void Update(T instance)
        {
            using (var dbManager = GetNewDbManager())
            {
                new SqlQuery<T>(dbManager).Update(instance);
            }
        }

        public void Update(IEnumerable<T> instanceList)
        {
            using (var dbManager = GetNewDbManager())
            {
                new SqlQuery<T>(dbManager).Update(instanceList);
            }
        }

        public void Delete(int id)
        {
            using (var dbManager = GetNewDbManager())
            {
                new SqlQuery<T>(dbManager).DeleteByKey(id);
            }
        }

        public void Delete(T instance)
        {
            using (var dbManager = GetNewDbManager())
            {
                new SqlQuery<T>(dbManager).Delete(instance);
            }
        }

        public void Delete(IEnumerable<T> instanceList)
        {
            using (var dbManager = GetNewDbManager())
            {
                new SqlQuery<T>(dbManager).Delete(instanceList);
            }
        }
    }
}
