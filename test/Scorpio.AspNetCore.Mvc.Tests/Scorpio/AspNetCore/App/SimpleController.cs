
using Microsoft.AspNetCore.Mvc;

namespace Scorpio.AspNetCore.App
{
    public class SimpleController : Controller
    {
        public IActionResult Index() => Content("Index-Result");

        public ActionResult About() =>
            // ReSharper disable once Mvc.ViewNotResolved
            View();

        public ActionResult ExceptionOnRazor() =>
            // ReSharper disable once Mvc.ViewNotResolved
            throw new ScorpioException();

    }
}
