using Microsoft.AspNetCore.Mvc;
using Nest.Services.Implements;
using Nest.Services.Interfaces;
using System.Diagnostics;

namespace Nest.Controllers
{
    public class HomeController : Controller
    {
        readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public IActionResult Demo()
        {

            return View();
        }
        public IActionResult Index()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            ViewBag.QuickPartialView = true;
            return View();
        }

        public IActionResult ProductCategoryFilter(int? id)
        {
            return ViewComponent("Product", id);
        }

        //public IActionResult EmailSend()
        //{
        //	//_emailService.Send();

        //          return Ok();
        //}
    }
}
