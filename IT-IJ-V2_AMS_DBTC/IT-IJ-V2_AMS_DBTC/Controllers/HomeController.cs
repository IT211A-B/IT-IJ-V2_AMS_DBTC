using System.Diagnostics;
using IT_IJ_V2_AMS_DBTC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_IJ_V2_AMS_DBTC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
