using System.Linq;
using LinqToDB;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class PostVotingRepository : BaseSqlRepository<PostVotingData>
    {
        public bool TryToVote(PostVotingData instance)
        {
            using (var db = new DataBase())
            {
                db.BeginTransaction();

                var post = db.PostTable.FirstOrDefault(i => i.Id == instance.PostId);
                if (post == null)
                    return false;

                //Update vote
                var copyVote = db.PostVotingTable.FirstOrDefault(i => i.PostId == instance.PostId && i.UserId == instance.UserId);
                if (copyVote != null)
                {
                    //Delete opposite vote
                    if (copyVote.IsVoteUp != instance.IsVoteUp)
                    {
                        db.PostVotingTable.Where(i => i.PostId == copyVote.PostId && i.UserId == copyVote.UserId).Delete();
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
                var postRating = post.Rating + (instance.IsVoteUp ? 1 : -1);
                db.PostTable.Where(i => i.Id == instance.PostId).Set(i => i.Rating, postRating).Update();

                db.CommitTransaction();
                return true;
            }
        }
    }
}
