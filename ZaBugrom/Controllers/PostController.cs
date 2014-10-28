using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Models.Data;
using Models.Data.Enums;
using Models.HelpModels.Post;
using Models.InputModels.Post;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult SimplePost(PostData postData, bool isRichView = true)
        {
            ViewBag.IsRichView = isRichView;
            return View("SimplePost", postData);
        }

        [HttpGet]
        public ActionResult VideoPost(PostData postData, bool isRichView = true)
        {
            ViewBag.IsRichView = isRichView;
            return View("VideoPost", postData);
        }

        [HttpGet]
        public ActionResult Post(PostData postData, bool isRichView = true)
        {
            if (postData == null)
            {
                return HttpNotFound();
            }

            switch (postData.PostType)
            {
                case PostType.SimplePost:
                    return SimplePost(postData, isRichView);
                case PostType.VideoPost:
                    return VideoPost(postData, isRichView);
                default:
                    throw new InvalidDataException("There is no post shell for this kind of post!");
            } 
        }

        [HttpGet]
        public ActionResult DetailPost(Int64 postId)
        {
            var postData = RepositoryManager.PostRepository.GetById(postId);

            if (postData == null)
            {
                return HttpNotFound();
            }

            return View(postData);
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult AddComment(AddingComment comment)
        {
            var commentToAdd = new CommentData()
            {
                AddTime = DateTime.Now,
                AuthorId = UserManager.UserId,
                AuthorName = UserManager.UserName,
                ParentCommentId = comment.ParentCommentId,
                PostId = comment.PostId,
                Rating = 0,
                Source = comment.Source,
                CommentLevel = 1
            };

            //Check max level of comment
            if (commentToAdd.ParentCommentId.HasValue)
            {
                var parentComment = RepositoryManager.CommentRepository.GetById(commentToAdd.ParentCommentId.Value);

                if (parentComment.CommentLevel == CommentData.MaxCommentLevel)
                {
                    throw new ArgumentOutOfRangeException();
                }

                commentToAdd.CommentLevel = parentComment.CommentLevel + 1;
            }

            var id = RepositoryManager.CommentRepository.Insert(commentToAdd);
            commentToAdd.Id = id;

            return PartialView("~/Views/Element/CommentArea.cshtml", new List<CommentData> {commentToAdd});
        }

        [HttpPost]
        [Authorize]
        public JsonResult TryToVote(Int64 postId, bool isVoteUp)
        {
            var userId = UserManager.UserId;

            var result = RepositoryManager.PostVotingRepository.TryToVote(new PostVotingData()
            {
                PostId = postId,
                UserId = userId,
                IsVoteUp = isVoteUp
            });

            return Json(new AddVoteResult() {Id = postId, Result = result});
        }
    }
}
