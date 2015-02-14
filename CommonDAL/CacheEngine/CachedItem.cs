﻿namespace CommonDAL.CacheEngine
{
    public class CachedItem<T>
    {
        public CachedItem(T item)
        {
            Item = item;
        }

        public T Item { get; set; }
        public bool IsUpdated { get; set; }
    }
}
