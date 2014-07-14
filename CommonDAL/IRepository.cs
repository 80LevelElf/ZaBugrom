using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CommonDAL
{
    interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetList(); 
        int Create(T instance);
        void Update(int id, T instance);
        void Delete(int id);
    }
}
