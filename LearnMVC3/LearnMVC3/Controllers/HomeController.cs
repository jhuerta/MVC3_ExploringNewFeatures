using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure.Logging;

namespace LearnMVC3.Controllers
{
    public class HomeController : Controller
    {
        ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            _logger.LogInfo("Hey - I called the Home Page!!!!");
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
