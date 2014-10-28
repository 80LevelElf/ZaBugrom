using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Models.Data;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class ElementController : Controller
    {
        public PartialViewResult PostEditor()
        {
            return PartialView();
        }

        public PartialViewResult CommentAreaForPost(Int64 postId)
        {
            var commentList = RepositoryManager.CommentRepository.GetList(postId);

            return PartialView("CommentArea", commentList);
        }

        public PartialViewResult CommentArea(List<CommentData> commentList)
        {
            return PartialView(commentList);
        }

        public PartialViewResult RatingVoter(Int64 rating)
        {
            return PartialView("RatingVoter", rating);
        }
    }
}
