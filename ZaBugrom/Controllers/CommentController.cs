using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Models.Data;
using Models.HelpModels.Post;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class CommentController : Controller
    {
        public PartialViewResult CommentAreaForPost(Int64 postId)
        {
            var commentList = RepositoryManager.CommentRepository.GetList(postId, UserManager.UserId);

            return PartialView("CommentArea", commentList);
        }

        public PartialViewResult CommentArea(List<CommentData> commentList)
        {
            return PartialView(commentList);
        }

        [HttpPost]
        [Authorize]
        public JsonResult TryToVote(Int64 commentId, bool isVoteUp)
        {
            var userId = UserManager.UserId;

            var result = RepositoryManager.CommentVotingRepository.TryToVote(new CommentVotingData()
            {
                CommentId = commentId,
                UserId = userId,
                IsVoteUp = isVoteUp
            });

            return Json(new AddVoteResult() { Id = commentId, Result = result });
        }
    }
}
