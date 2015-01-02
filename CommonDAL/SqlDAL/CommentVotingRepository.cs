using System.Linq;
using LinqToDB;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class CommentVotingRepository : BaseSqlRepository<CommentVotingData>
    {
        public bool TryToVote(CommentVotingData instance)
        {
            using (var db = new DataBase())
            {
                db.BeginTransaction();

                var comment = db.CommentTable.FirstOrDefault(i => i.Id == instance.CommentId);
                if (comment == null)
                    return false;

                //Update vote
                var copyVote = db.CommentVotingTable.FirstOrDefault(i => i.CommentId == instance.CommentId && i.UserId == instance.UserId);
                if (copyVote != null)
                {
                    //Delete opposite vote
                    if (copyVote.IsVoteUp != instance.IsVoteUp)
                    {
                        db.CommentVotingTable.Where(i => i.CommentId == copyVote.CommentId && i.UserId == copyVote.UserId).Delete();
                    }
                    else
                    {
                        //User already have such vote
                        db.CommitTransaction();
                        return false;
                    }
                }
                else
                {
                    db.Insert(instance);
                }

                //Update post ratig
                var commentRating = comment.Rating + (instance.IsVoteUp ? 1 : -1);
                db.CommentTable.Where(i => i.Id == instance.CommentId).Set(i => i.Rating, commentRating).Update();

                db.CommitTransaction();
                return true;
            }
        }
    }
}
