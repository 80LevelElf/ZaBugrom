using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Engine;

namespace CommonDAL.CacheEngine
{
    public abstract class CachedCollection<T, TKEy> : IEnumerable<T> where T:class 
    {
        protected ConcurrentDictionary<TKEy, CachedItem<T>> DataDict { get; set; }

        public Func<T, TKEy> KeyFunc { get; set; }
        public TimeChecker TimeChecker { get; set; }

        protected CachedCollection(IEnumerable<T> initialCollection, Func<T, TKEy> keyFunc, TimeChecker timeChecker)
        {
            KeyFunc = keyFunc;
            TimeChecker = timeChecker;
            DataDict = new ConcurrentDictionary<TKEy, CachedItem<T>>(initialCollection.ToList().Select(i =>
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

        protected abstract void TryToUpate();
    }
}