using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure;
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
            var orders = db.Orders;

            var production = CreateNewProduction();
            var order = CreateNewOrder();

            productions.Add(production);
            orders.Add(order);

            try
            {
            int success = db.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _logger.LogInfo(string.Format("  > Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                
                _logger.LogInfo(string.Format("Error DB: {0}", ex.Message));
            }
            
            
            //_logger.LogInfo(string.Format("Datatabase: {0}", ((IObjectContextAdapter)db).ObjectContext.Connection.ConnectionString));

            return View();
        }

        private Orders CreateNewOrder()
        {
            return new Orders()
                       {
                           Items = new[]
                                       {
                                           CreateNewItem("iPod Shuffle"),
                                           CreateNewItem("iPAD"),
                                           CreateNewItem("New Balance Trail Run"),
                                           CreateNewItem("Coke Can"),
                                           CreateNewItem("Starbucks Coffee"),
                                           CreateNewItem("Telephone"),
                                           CreateNewItem("Monitor Dell")
                                       },
                           created_at = DateTime.Now,
                           email = "juan.huerta@gmail.com",
                           token = Guid.NewGuid().ToString(),
                           total_price = ((decimal) (new Random()).Next(2000, 50000))/100
                       };
        }

        private Items CreateNewItem(string name)
        {
            return new Items()
                       {
                           grams = ((int) (new Random()).Next(1000, 10000))/100,
                           requires_shipping = 1,
                           title = name,
                           product_id = Guid.NewGuid().ToString(),
                           price = (((new Random()).Next(2000, 20000)) / 100).ToString(),
                           quantity = (new Random()).Next(1, 20)
                       };
        }

        private static Productions CreateNewProduction()
        {
            return new Productions()
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
