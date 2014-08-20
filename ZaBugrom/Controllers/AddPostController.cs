using System.Web.Mvc;
using Models.Data;
using Models.Data.Enums;
using Models.InputModels.AddContent;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class AddPostController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("AddSimplePost");
        }

        [Authorize]
        [HttpGet]
        public ViewResult AddSimplePost()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddVideoPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddSimplePost(AddSimplePostInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userData = UserManager.GetCurrentUser();

            var post = new PostData
            {
                AuthorId = userData.Id,
                Source = model.Source,
                PostType = PostType.SimplePost,
                Title = model.Title
            };

            RepositoryManager.PostRepository.Insert(post);
            //TODO redirect to new post
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddVideoPost(AddVideoPostInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userData = UserManager.GetCurrentUser();

            var post = new PostData
            {
                AuthorId = userData.Id,
                Source = VideoManager.GetYoutubeVideoId(model.Source),
                PostType = PostType.VideoPost,
                Title = model.Title
            };

            RepositoryManager.PostRepository.Insert(post);
            //TODO redirect to new post
            return RedirectToAction("Index", "Home");
        }
    }
}
