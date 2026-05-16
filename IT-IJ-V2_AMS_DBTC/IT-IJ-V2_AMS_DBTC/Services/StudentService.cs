using Microsoft.AspNetCore.Mvc;

namespace IT_IJ_V2_AMS_DBTC.Services
{
    public class StudentService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
