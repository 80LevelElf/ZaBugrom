using System.Web.Mvc;
using Models.Data.Settings;

namespace ZaBugrom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("CommonFlow");
        }

        public ActionResult CommonFlow()
        {
            return View();
        }

        public ActionResult UserFlow()
        {
            ViewBag.FilterSettings = new FilterSettingsData();

            return View();
        }
    }
}
