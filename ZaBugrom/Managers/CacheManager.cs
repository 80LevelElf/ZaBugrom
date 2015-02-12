using Engine.CacheEngine;

namespace ZaBugrom.Managers
{
    public static class CacheManager
    {
        public static TagCacheCollection TagCacheCollection { private set; get; }

        static CacheManager()
        {
            TagCacheCollection = new TagCacheCollection(RepositoryManager.TagRepository.GetList(), i => i.Name, RepositoryManager.TagRepository);
        }
    }
}