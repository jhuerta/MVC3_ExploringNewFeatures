using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure.Logging;
using LearnMVC3.Model;
using LearnMVC3.Models;

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

            var db = new LearnMVC3DBContext();
            var productions = db.Productions;
            var production = CreateNewProduction();
            productions.Add(production);
            int success = db.SaveChanges();
            
            _logger.LogInfo(string.Format("Datatabase: {0}", ((IObjectContextAdapter)db).ObjectContext.Connection.ConnectionString));
            

            return View();
        }

        private static Production CreateNewProduction()
        {
            return new Production()
                       {
                           Description = "Production title",
                           Title = "Production Title",
                           Price = 25,
                           Episodes = new[]
                                          {
                                              new Episode()
                                                  {
                                                      Description = "Part 1 - New episode on MVC. Cool!!!!!!!",
                                                      Title = "MVC - Go for it!"
                                                  },
                                              new Episode()
                                                  {
                                                      Description = "Part 2 - New episode on MVC. Cool!!!!!!!",
                                                      Title = "MVC - Go for it!"
                                                  }
                                          }

                       };
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
