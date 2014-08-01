using BLToolkit.Reflection;
using CommonDAL.SqlDAL;

namespace ZaBugrom.Managers
{
    public static class RepositoryManager
    {
        public static UserRepository UserRepository { get; private set; }
        public static PostRepository PostRepository { get; private set; }

        static RepositoryManager()
        {
            UserRepository = TypeAccessor<UserRepository>.CreateInstance();
            PostRepository = TypeAccessor<PostRepository>.CreateInstance();
        }
    }
}