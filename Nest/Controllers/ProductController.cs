using Microsoft.AspNetCore.Mvc;

namespace Nest.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            ViewBag.QuickPartialView = true;
            return View();
        }
    }
}
