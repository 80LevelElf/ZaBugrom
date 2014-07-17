using System;
using System.Collections.Generic;
using BLToolkit.Data;
using BLToolkit.Data.DataProvider;
using BLToolkit.DataAccess;

namespace CommonDAL.SqlDAL
{
    /// <summary>
    /// Abstract class which using to get access to DbManager
    /// </summary>
    public abstract class AbstractSqlRepository
    {
        public static void SetDataBaseConfig(string connectionString)
        {
            CommonDbManager = new DbManager(new SqlDataProvider(), connectionString);
        }

        protected static DbManager CommonDbManager { get; private set; }
    }

    public abstract class AbstractSqlRepository<T> : AbstractSqlRepository, IRepository<T>
    {
        private readonly SqlQuery<T> _sqlQuery;

        protected AbstractSqlRepository()
        {
            if (CommonDbManager == null)
            {
                throw new NullReferenceException("Please, initialize CommonDbManager before using of AbstractSqlRepository.");
            }

            _sqlQuery = new SqlQuery<T>(CommonDbManager);
        }

        public T GetById(int id)
        {
            return _sqlQuery.SelectByKey(id);
        }

        public List<T> GetList()
        {
            return _sqlQuery.SelectAll();
        }

        public int Insert(T instance)
        {
            return _sqlQuery.Insert(instance);
        }

        public int Insert(IEnumerable<T> instanceList)
        {
            return _sqlQuery.Insert(instanceList);
        }

        public void Update(T instance)
        {
            _sqlQuery.Update(instance);
        }

        public void Update(IEnumerable<T> instanceList)
        {
            _sqlQuery.Update(instanceList);
        }

        public void Delete(int id)
        {
            _sqlQuery.DeleteByKey(id);
        }

        public void Delete(T instance)
        {
            _sqlQuery.Delete(instance);
        }

        public void Delete(IEnumerable<T> instanceList)
        {
            _sqlQuery.Delete(instanceList);
        }
    }
}
