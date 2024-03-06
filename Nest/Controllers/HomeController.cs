using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Nest.Controllers
{
	public class HomeController : Controller
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
