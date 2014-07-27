using System.Web.Mvc;
using Models.InputModels.AddContent;

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
        public ActionResult AddSimplePost()
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
            return null;
        }
    }
}
