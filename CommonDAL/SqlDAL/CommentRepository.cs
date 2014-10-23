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
        public List<CommentData> GetList(Int64 postId)
        {
            IEnumerable<CommentData> list;
            using (var db = new DataBase())
            {
                list = db.CommentTable.Where(i => i.PostId == postId).ToList();
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
    }
}
