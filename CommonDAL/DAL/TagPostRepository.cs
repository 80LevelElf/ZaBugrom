using System;
using System.Collections.Generic;
using LinqToDB;
using Models.Data;

namespace CommonDAL.DAL
{
    public class TagPostRepository
    {
        public void AddTagsToPost(Int64 postId, List<TagData> tagList)
        {
            using (var db = new DataBase())
            {
                foreach (var tag in tagList)
                {
                    db.Insert(new TagPostData
                    {
                        PostId = postId,
                        TagId = tag.Id
                    });
                }
            }
        }
    }
}
