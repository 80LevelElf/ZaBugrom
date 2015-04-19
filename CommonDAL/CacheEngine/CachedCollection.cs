using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine;

namespace CommonDAL.CacheEngine
{
    public abstract class CachedCollection<T, TKey> : IEnumerable<T> where T:class 
    {
        protected Dictionary<TKey, CachedItem<T>> DataDict { get; set; }
        protected object _locker = new object();

        public Func<T, TKey> KeyFunc { get; set; }
        public TimeChecker TimeChecker { get; set; }

        protected CachedCollection(IEnumerable<T> initialCollection, Func<T, TKey> keyFunc, TimeChecker timeChecker)
        {
            KeyFunc = keyFunc;
            TimeChecker = timeChecker;
            DataDict = initialCollection.ToDictionary(keyFunc, i => new CachedItem<T>(i));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DataDict.Select(i => i.Value.Item).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual T GetItem(TKey key)
        {
            lock (_locker)
            {
                CachedItem<T> cachedItem;
                DataDict.TryGetValue(key, out cachedItem);

                if (cachedItem != null)
                    return cachedItem.Item;

                //Add new item
                var newItem = SaveNewItem(key);
                DataDict.Add(key, new CachedItem<T>(newItem));

                return newItem;
            }
        }

        ~CachedCollection()
        {
            TryToUpate();
        }

        protected abstract void TryToUpate();
        protected abstract T SaveNewItem(TKey key);
    }
}