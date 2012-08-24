using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Controllers;
using LearnMVC3.Infrastructure;
using LearnMVC3.Infrastructure.Logging;
using LearnMVC3.Model;


namespace LearnMVC3.Areas.Api.Controllers
{

    public class ShopifyController : ApplicationController
    {
        readonly ILogger _logger;
        private Orders _orders;
        private Items _items;

        public ShopifyController(ITokenHandler tokenStore, ILogger logger) : base(tokenStore)
        {
            _logger = logger;
            _orders = new Orders();
            _items = new Items();
        }

        public ActionResult Receiver()
        {
            //var order = this.SqueezeJson();

            var json = this.ReadJson();
            var order = System.Web.Helpers.Json.Decode(json);

            _logger.LogInfo(" - New Order Received: " + order.order_number.ToString());

            var newOrder = new {
                                   created_at = order.created_at,
                                   email = order.email,
                                   token = order.token,
                                   total_price = order.total_price
                               };
            dynamic savedOrder = _orders.Insert(newOrder);

            foreach (var item in order.line_items)
            {
                var newItem = new Items
                                  {
                                      //OrderID = Convert.ToInt32(savedOrder.ID),
                                      //grams = 1,//item.grams,
                                      //price = "1",//item.price,
                                      //title = "a",//item.title,
                                      //product_id = "1",//item.product_id,
                                      //quantity = 1,//item.quantity,
                                      requires_shipping = 1
                                  };
                try
                {
                    var savedItem = _items.Insert(newItem);
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
                    var msg = ex.Message;
                    
                    throw;
                }
                
            }

                return Content("OK");
        }
    }
}
