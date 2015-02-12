using System;
using System.Collections.Generic;
using CommonDAL.SqlDAL;
using Models.Data;

namespace Engine.CacheEngine
{
    public class TagCacheCollection : CachedCollection<TagData, string>
    {
        private TagRepository Repository { get; set; }

        public TagCacheCollection(IEnumerable<TagData> initialCollection, Func<TagData, string> keyFunc, TagRepository repository)
            : base(initialCollection, keyFunc)
        {
            Repository = repository;
        }

        public TagData GetTag(string name)
        {
            CachedItem<TagData> cachedTag;
            DataDict.TryGetValue(name, out cachedTag);

            if (cachedTag != null)
            {
                cachedTag.Item.CountOfUsage++;
                cachedTag.IsChanged = true;
                return cachedTag.Item;
            }

            //Try to add new
            var tag = new TagData()
            {
                CountOfUsage = 1,
                Name = name
            };

            //We need to use this because of multy-thread code (two users add the same tag in same time -
            // first add it, second add simular tag). If we will use "lock" only one user can use this code
            // in one time
            Repository.InsertOrReplace(tag);
            tag = Repository.GetByName(name);

            DataDict.TryAdd(name, new CachedItem<TagData>(tag));
            return tag;
        }
    }
}
