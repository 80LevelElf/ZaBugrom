using System.Collections.Generic;

namespace CommonDAL
{
    interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetList(); 
        int Insert(T instance);
        int Insert(IEnumerable<T> instanceList);
        void Update(T instance);
        void Update(IEnumerable<T> instanceList);
        void Delete(int id);
        void Delete(T instance);
        void Delete(IEnumerable<T> instanceList);
    }
}
