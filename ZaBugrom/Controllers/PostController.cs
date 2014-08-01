using System.Web.Mvc;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class PostController : Controller
    {
        public ActionResult SimplePost(int id)
        {
            var postData = RepositoryManager.PostRepository.GetById(id);

            if (postData == null)
            {
                return HttpNotFound();
            }

            return View(postData);
        }

        public ActionResult VideoPost(int id)
        {
            var postData = RepositoryManager.PostRepository.GetById(id);

            if (postData == null)
            {
                return HttpNotFound();
            }

            return View(postData);
        }
    }
}
