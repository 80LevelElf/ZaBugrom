using CommonDAL.SqlDAL;

namespace ZaBugrom.Managers
{
    public static class RepositoryManager
    {
        public static UserRepository UserRepository { get; private set; }
        public static PostRepository PostRepository { get; private set; }
        public static HeaderImageRepository HeaderImageRepository { get; private set; }
        public static MessageRepository MessageRepository { get; private set; }

        static RepositoryManager()
        {
            UserRepository = new UserRepository();
            PostRepository = new PostRepository();
            HeaderImageRepository = new HeaderImageRepository();
            MessageRepository = new MessageRepository();
        }
    }
}