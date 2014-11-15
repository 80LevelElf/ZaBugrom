using System.Collections.Generic;
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

                //If there is no such vote or there is different signs
                var copyVote = db.CommentVotingTable.FirstOrDefault(i => i.CommentId == instance.CommentId && i.UserId == instance.UserId);

                if (copyVote == null || copyVote.IsVoteUp != instance.IsVoteUp)
                {
                    db.Insert(instance);

                    //Update comment rating and remove old vote(if it's exists)
                    var firstOrDefault = db.CommentTable.FirstOrDefault(i => i.Id == instance.CommentId);
                    if (firstOrDefault != null)
                    {
                        //Get new rating
                        var rating = firstOrDefault.Rating;
                        if (instance.IsVoteUp)
                            rating++;
                        else
                            rating--;

                        //Remove copy
                        if (copyVote != null)
                        {
                            //We can't use delete because in this way we need to close connection
                            db.CommentVotingTable.Where(i =>
                                i.CommentId == copyVote.CommentId &&
                                i.IsVoteUp == copyVote.IsVoteUp &&
                                i.UserId == copyVote.UserId).Delete();

                            rating -= (copyVote.IsVoteUp) ? 1 : -1;
                        }

                        db.CommentTable.Where(i => i.Id == instance.CommentId).Set(i => i.Rating, rating).Update();
                    }
                    else
                    {
                        db.CommitTransaction();
                        return false;
                    }

                    db.CommitTransaction();
                    return true;
                }

                db.CommitTransaction();
                return false;
            }
        }
    }
}
