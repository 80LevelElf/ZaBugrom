using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Engine.CacheEngine
{
    public class CachedCollection<T, TKEy> : IEnumerable<T> where T:class 
    {
        protected ConcurrentDictionary<TKEy, CachedItem<T>> DataDict { get; set; }

        public Func<T, TKEy> KeyFunc { get; set; }

        public CachedCollection(IEnumerable<T> initialCollection, Func<T, TKEy> keyFunc)
        {
            KeyFunc = keyFunc;
            DataDict = new ConcurrentDictionary<TKEy, CachedItem<T>>(initialCollection.Select(i =>
                new KeyValuePair<TKEy, CachedItem<T>>(KeyFunc(i), new CachedItem<T>(i))));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DataDict.Select(i => i.Value.Item).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}