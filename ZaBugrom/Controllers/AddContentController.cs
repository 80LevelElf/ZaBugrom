using System.Web.Mvc;

namespace ZaBugrom.Controllers
{
    public class AddContentController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("AddSimplePost");
        }

        [Authorize]
        public ActionResult AddSimplePost()
        {
            return View();
        }
    }
}
