using System;
using System.Collections.Generic;
using System.Linq;
using CommonDAL.Managers;
using Models.Data;

namespace CommonDAL.DAL
{
    public class PostRepository : BigSqlRepository<PostData>
    {
        public override Int64 Insert(PostData instance)
        {
            instance.AddTime = DateTime.Now;
            var id = base.Insert(instance);

            //Add tag's ids
            var tagPostRepository = new TagPostRepository();
            tagPostRepository.AddTagsToPost(id, instance.TagList);

            return id;
        }

        public List<PostData> GetList(int userId)
        {
            using (var db = new DataBase())
            {
                return (from p in db.PostTable
                    from pv in db.PostVotingTable.Where(i => i.PostId == p.Id && i.UserId == userId).DefaultIfEmpty()
                    select new PostData
                    {
                        AddTime = p.AddTime,
                        AuthorId = p.AuthorId,
                        AuthorName = p.AuthorName,
                        Id = p.Id,
                        PostType = p.PostType,
                        Rating = p.Rating,
                        Source = p.Source,
                        Title = p.Title,
                        IsVote = (pv.UserId != 0),
                        IsVoteUp = pv.IsVoteUp,
                        TagList = (db.TagPostTable.Where(i => i.PostId == p.Id)
                                .Select(i => RepositoryManager.TagRepository.GetById(i.TagId))).ToList()
                    }).OrderByDescending(i => i.Id).ToList();
            }
        }

        public PostData GetById(long id, int userId)
        {
            using (var db = new DataBase())
            {
                return (from p in db.PostTable.Where(p => p.Id == id)
                        from pv in db.PostVotingTable.Where(i => i.PostId == p.Id && i.UserId == userId).DefaultIfEmpty()
                        select new PostData
                        {
                            AddTime = p.AddTime,
                            AuthorId = p.AuthorId,
                            AuthorName = p.AuthorName,
                            Id = p.Id,
                            PostType = p.PostType,
                            Rating = p.Rating,
                            Source = p.Source,
                            Title = p.Title,
                            IsVote = (pv.UserId != 0),
                            IsVoteUp = pv.IsVoteUp,
                            TagList = (db.TagPostTable.Where(i => i.PostId == p.Id)
                                    .Select(i => RepositoryManager.TagRepository.GetById(i.TagId))).ToList()
                        }).First();
            }
        }
    }
}
