using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LearnMVC3.Controllers;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Areas.Api.Controllers
{
    public class ProductionsController : ApplicationController {
        
        readonly dynamic _productions;
        
        public ProductionsController(ITokenHandler tokenStore) : base(tokenStore)
        {
            _productions = new ProductionsV2();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return LearnMVC3JSON(_productions.All(orderby: "ID DESC"));
        }


        [HttpPost]
        public ActionResult Create()
        {
            var model = SqueezeJson();
            _productions.Insert(model);
            return LearnMVC3JSON(model);
        }

        [HttpPut]
        public ActionResult Edit()
        {
            var model = SqueezeJson();
            _productions.Update(model, model.ID);
            return LearnMVC3JSON(model);
        }


    }

    public class ProductionModel
    {
        public string Title { get; set; }
    }
}
