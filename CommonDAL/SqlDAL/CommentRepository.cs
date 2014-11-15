using System;
using System.Collections.Generic;
using System.Linq;
using Models.Data;

namespace CommonDAL.SqlDAL
{
    public class CommentRepository : BigSqlRepository<CommentData>
    {
        /// <summary>
        /// Get all comments of specified post as a tree 
        /// </summary>
        public List<CommentData> GetList(Int64 postId, int userId)
        {
            List<CommentData> list;
            using (var db = new DataBase())
            {
                list = (from c in db.CommentTable.Where(i => i.PostId == postId)
                         from cv in db.CommentVotingTable.Where(i => i.CommentId == c.Id && i.UserId == userId).DefaultIfEmpty()
                         select new CommentData
                         {
                             AddTime = c.AddTime,
                             AuthorId = c.AuthorId,
                             AuthorName = c.AuthorName,
                             Id = c.Id,
                             Rating = c.Rating,
                             Source = c.Source,
                             IsVote = (cv.UserId != 0),
                             IsVoteUp = cv.IsVoteUp,
                             CommentLevel = c.CommentLevel,
                             ParentCommentId = c.ParentCommentId
                         }).ToList();
            }

            //Make a tree
            foreach (var comment in list)
            {
                foreach (var possibleParent in list)
                {
                    if (comment.ParentCommentId == possibleParent.Id)
                    {
                        possibleParent.SubComments.Add(comment);
                    }
                }
            }

            //Take a comments which has not any parents
            return list.Where(i => !i.ParentCommentId.HasValue).ToList();
        }

        public override Int64 Insert(CommentData instance)
        {
            instance.AddTime = DateTime.Now;
            return base.Insert(instance);
        }
    }
}
