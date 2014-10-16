using System.IO;
using System.Web.Mvc;
using Models.Data;
using Models.Data.Enums;
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
        public ActionResult DetailPost(int postId)
        {
            var postData = RepositoryManager.PostRepository.GetById(postId);

            if (postData == null)
            {
                return HttpNotFound();
            }

            return View(postData);
        }
    }
}
