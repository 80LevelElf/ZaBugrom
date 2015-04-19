using System;
using System.Collections.Generic;
using System.Linq;
using CommonDAL.Classes;
using CommonDAL.DAL;
using Engine;
using Models.Data;

namespace CommonDAL.CacheEngine
{
    public class TagCachedCollection : CachedCollection<TagData, string>
    {
        private Dictionary<Int64, TagData> DataDictById { get; set; }
        private AutocompliteList<TagData> AutocompliteList { get; set; }
        private TagRepository Repository { get; set; }

        public TagCachedCollection(IEnumerable<TagData> initialCollection, Func<TagData, string> keyFunc, TagRepository repository)
            : base(initialCollection, keyFunc, TimeChecker.EveryHour)
        {
            Repository = repository;

            DataDictById = initialCollection.ToDictionary(i => i.Id, i => i);
            AutocompliteList = new AutocompliteList<TagData>(initialCollection, i => i.Name);
        }

        public override TagData GetItem(string name)
        {
            lock (_locker)
            {
                CachedItem<TagData> cachedTag;
                DataDict.TryGetValue(name, out cachedTag);

                if (cachedTag != null)
                {
                    cachedTag.Item.CountOfUsage++;
                    cachedTag.IsUpdated = true;
                    return cachedTag.Item;
                }

                return SaveNewItem(name);
            }
        }

        public List<TagData> GetAutocompliteList(string prefix, int count)
        {
            return AutocompliteList.GetAutocompliteList(prefix, count);
        }

        public TagData GetItem(Int64 id)
        {
            lock (_locker)
            {
                return DataDictById[id];
            }
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

        protected override TagData SaveNewItem(string key)
        {
            var tag = new TagData
            {
                CountOfUsage = 1,
                Name = key
            };

            tag.Id = Repository.Insert(tag);

            DataDict.Add(key, new CachedItem<TagData>(tag));
            DataDictById.Add(tag.Id, tag);

            AutocompliteList.Add(tag);

            return tag;
        }
    }
}