using System.Web.Mvc;
using Managers;

namespace ZaBugrom.Controllers
{
    public class HelperController : Controller
    {
        [HttpPost]
        public JsonResult CurrentHeaderImage()
        {
            return Json(HeaderImageManager.CurrentHeaderImage);
        }

    }
}
