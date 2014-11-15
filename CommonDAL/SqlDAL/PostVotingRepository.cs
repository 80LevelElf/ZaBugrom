﻿using System.Linq;
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

                //If there is no such vote or there is different signs
                var copyVote = db.PostVotingTable.FirstOrDefault(i => i.PostId == instance.PostId && i.UserId == instance.UserId);

                if (copyVote == null || copyVote.IsVoteUp != instance.IsVoteUp)
                {
                    db.Insert(instance);
                    
                    //Update post rating and remove old vote(if it's exists)
                    var firstOrDefault = db.PostTable.FirstOrDefault(i => i.Id == instance.PostId);
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
                            db.PostVotingTable.Where(i =>
                                i.PostId == copyVote.PostId &&
                                i.IsVoteUp == copyVote.IsVoteUp &&
                                i.UserId == copyVote.UserId).Delete();

                            rating -= (copyVote.IsVoteUp) ? 1 : -1;
                        }

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
