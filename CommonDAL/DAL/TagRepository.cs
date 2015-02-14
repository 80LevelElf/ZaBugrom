using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CommonDAL.CacheEngine;
using Models.Data;

//For TagCacheCollection can use TagRepository internal functions
[assembly: InternalsVisibleTo("DAL")]

namespace CommonDAL.DAL
{
    public sealed class TagRepository : BigSqlRepository<TagData>
    {
        private TagCacheCollection CacheCollection { get; set; }

        public TagRepository()
        {
            CacheCollection = new TagCacheCollection(GetList(), i => i.Name, this);
        }

        public TagData GetTag(string name)
        {
            return CacheCollection.GetTag(name);
        }

        public override TagData GetById(Int64 id)
        {
            return CacheCollection.GetTag(id);
        }

        public List<TagData> GetTagList(List<string> nameList)
        {
            return nameList.Select(GetTag).ToList();
        }

        internal TagData GetTagFromDB(string name)
        {
            using (var db = new DataBase())
            {
                return db.TagTable.FirstOrDefault(i => i.Name == name);
            }
        }
    }
}
