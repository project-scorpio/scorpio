using Microsoft.AspNetCore.Mvc;

namespace Scorpio.AspNetCore.Mvc.Tests
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
