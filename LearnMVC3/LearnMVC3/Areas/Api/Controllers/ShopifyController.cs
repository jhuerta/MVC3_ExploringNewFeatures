using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Controllers;
using LearnMVC3.Infrastructure;
using LearnMVC3.Infrastructure.Logging;
using LearnMVC3.Model;
using LearnMVC3.Models;


namespace LearnMVC3.Areas.Api.Controllers
{

    public class ShopifyController : ApplicationController
    {
        readonly ILogger _logger;
        private readonly DbSet<Orders> _orders;
        private DbSet<Items> _items;
        private LearnMVC3DBContext db;

        public ShopifyController(ITokenHandler tokenStore, ILogger logger) : base(tokenStore)
        {
            _logger = logger;
            db = new LearnMVC3DBContext();
            _orders = db.Orders;
            _items = db.Items;
        }

        public ActionResult Receiver()
        {
            //var noorder = this.SqueezeJson();

            try
            {
                var json = this.ReadJson();

                var order = System.Web.Helpers.Json.Decode(json);

                //_logger.LogInfo(" - New Order Received: " + order.order_number.ToString());

                var newOrder = new Orders()
                {

                    Items = CreateItemsList(order.line_items),
                    created_at = DateTime.Parse(order.created_at),
                    email = order.email,
                    token = order.token,
                    total_price = Convert.ToDecimal(order.total_price)
                };

                dynamic savedOrder = _orders.Add(newOrder);

                           foreach (var item in order.line_items)
                           {
                               var xtraItem = new Items()
                                                  {
                                                      OrderID = Convert.ToString(savedOrder.ID),
                                                      grams = item.grams,
                                                      title = "Xtra Item: " + item.title,
                                                      quantity = item.quantity,
                                                      product_id = item.product_id
                                                  };
                               // _items.Add(xtraItem);
                           }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                //_logger.LogInfo(string.Format("  > Exception: {0} ", msg));
                
                throw;
            }
                return Content("OK");
        }

        private List<Items> CreateItemsList(dynamic lineItems)
        {
            var itemsOrder = new List<Items>();

            foreach (var item in lineItems)
            {
                var newItem =  CreateNewItem(item);
                itemsOrder.Add(newItem);
            }

            return itemsOrder;

            //return new[]
            //           {
            //               CreateNewItem("iPod Shuffle"),
            //               CreateNewItem("iPAD"),
            //               CreateNewItem("New Balance Trail Run"),
            //               CreateNewItem("Coke Can"),
            //               CreateNewItem("Starbucks Coffee"),
            //               CreateNewItem("Telephone"),
            //               CreateNewItem("Monitor Dell")
            //           };
        }

        private static Items CreateNewItem(dynamic item)
        {
            return new Items()
                       {
                           grams = item.grams,
                           price = item.price,
                           quantity = item.quantity,
                           title = item.title,
                           product_id = item.product_id
                       };
        }

        private Items CreateNewItem(string name)
        {
            return new Items()
            {
                grams = ((int)(new Random()).Next(1000, 10000)) / 100,
                title = name,
                product_id = Guid.NewGuid().ToString(),
                price = (((new Random()).Next(2000, 20000)) / 100).ToString(),
                quantity = (new Random()).Next(1, 20)
            };
        }

    }
}
