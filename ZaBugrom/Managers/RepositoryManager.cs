using System.Data.Entity.Infrastructure;
using BLToolkit.Reflection;
using CommonDAL.SqlDAL;
using ZaBugrom.Shells;

namespace ZaBugrom.Managers
{
    public static class RepositoryManager
    {
        public static UserRepository UserRepository { get; private set; }
        public static PostRepository PostRepository { get; private set; }
        public static HeaderImageRepository HeaderImageRepository { get; private set; }

        static RepositoryManager()
        {
            UserRepository = TypeAccessor<UserRepository>.CreateInstance();
            PostRepository = TypeAccessor<PostRepository>.CreateInstance();
            HeaderImageRepository = TypeAccessor<HeaderImageRepository>.CreateInstance();
        }
    }
}