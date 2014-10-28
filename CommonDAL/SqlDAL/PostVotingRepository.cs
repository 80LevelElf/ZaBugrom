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

                //If there is no such vote
                if (!db.PostVotingTable.Any(i => i.PostId == instance.PostId && i.UserId == instance.UserId))
                {
                    db.Insert(instance);
                    
                    //Update post rating
                    var firstOrDefault = db.PostTable.FirstOrDefault(i => i.Id == instance.PostId);
                    if (firstOrDefault != null)
                    {
                        //Get new rating
                        var rating = firstOrDefault.Rating;
                        if (instance.IsVoteUp)
                            rating++;
                        else
                            rating--;

                        db.PostTable.Where(i => i.Id == instance.PostId).Set(i => i.Rating, rating).Update();
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
