using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CommonDAL.DAL;
using Engine;
using Models.Data;

namespace CommonDAL.CacheEngine
{
    public class TagCacheCollection : CachedCollection<TagData, string>
    {
        private object _getTagLock = new object();
        private ConcurrentDictionary<Int64, TagData> DataDictById { get; set; } 
        private TagRepository Repository { get; set; }

        public TagCacheCollection(IEnumerable<TagData> initialCollection, Func<TagData, string> keyFunc, TagRepository repository)
            : base(initialCollection, keyFunc, TimeChecker.Every6Hours)
        {
            Repository = repository;
            
            DataDictById = new ConcurrentDictionary<Int64, TagData>(initialCollection.ToList().Select(i =>
                new KeyValuePair<Int64, TagData>(i.Id, i)));
        }

        public TagData GetTag(string name)
        {
            lock (_getTagLock)
            {
                CachedItem<TagData> cachedTag;
                DataDict.TryGetValue(name, out cachedTag);

                if (cachedTag != null)
                {
                    cachedTag.Item.CountOfUsage++;
                    cachedTag.IsUpdated = true;
                    return cachedTag.Item;
                }

                //Try to add new
                var tag = new TagData
                {
                    CountOfUsage = 1,
                    Name = name
                };

                tag.Id = Repository.Insert(tag);

                DataDict.TryAdd(name, new CachedItem<TagData>(tag));
                DataDictById.TryAdd(tag.Id, tag);

                return tag;
            }
        }

        public TagData GetTag(Int64 id)
        {
            return DataDictById[id];
        }

        protected override void TryToUpate()
        {
            if (TimeChecker.Check())
            {
                foreach (var cachedItem in DataDict.Where(i => i.Value.IsUpdated))
                {
                    Repository.Update(cachedItem.Value.Item);
                }
            }
        }
    }
}