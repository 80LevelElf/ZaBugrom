using System.Web.Mvc;
using Models.Data.Settings;

namespace ZaBugrom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FilterSettings = new FilterSettingsData();

            return View();
        }
    }
}
