using CommonDAL.SqlDAL;

namespace ZaBugrom.Managers
{
    public static class RepositoryManager
    {
        public static UserRepository UserRepository { get; private set; }
        public static PostRepository PostRepository { get; private set; }
        public static HeaderImageRepository HeaderImageRepository { get; private set; }
        public static MessageRepository MessageRepository { get; private set; }
        public static CommentRepository CommentRepository { get; private set; }
        public static PostVotingRepository PostVotingRepository { get; private set; }
        public static CommentVotingRepository CommentVotingRepository { get; private set; }

        static RepositoryManager()
        {
            UserRepository = new UserRepository();
            PostRepository = new PostRepository();
            HeaderImageRepository = new HeaderImageRepository();
            MessageRepository = new MessageRepository();
            CommentRepository = new CommentRepository();
            PostVotingRepository = new PostVotingRepository();
            CommentVotingRepository = new CommentVotingRepository();
        }
    }
}