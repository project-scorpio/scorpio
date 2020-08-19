using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Scorpio.AspNetCore.App
{
    public class SimpleController : Controller
    {
        public IActionResult Index()
        {
            return Content("Index-Result");
        }

        public ActionResult About()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View();
        }

        public ActionResult ExceptionOnRazor()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            throw new ScorpioException();
            return View();
        }

    }
}
