using System;
using System.Web;
using CommonDAL.SqlDAL;

namespace ZaBugrom.Shells
{
    public class ServerSingleton<T>
    {
        private readonly GetInstance _getInstanceFunction;

        public delegate T GetInstance();

        public ServerSingleton()
        {
            
        }

        public ServerSingleton(GetInstance getInstance)
        {
            _getInstanceFunction = getInstance;
        }

        public T Instance
        {
            get
            {
                var typeName = typeof (T).Name;
                var items = HttpContext.Current.Items;

                if (!items.Contains(typeName))
                {
                    if (_getInstanceFunction != null)
                    {
                        items[typeName] = _getInstanceFunction();
                    }
                    else
                    {
                        items[typeName] = Activator.CreateInstance(typeof(T));
                    }
                }

                return (T)items[typeName];
            }
        }
    }
}