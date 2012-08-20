using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LearnMVC3.Infrastructure;
using LearnMVC3.Infrastructure.Logging;

namespace LearnMVC3.Controllers
{
    public class CruddyControllerBase : ApplicationController
    {
        protected dynamic _table = null;

        protected CruddyControllerBase(ITokenHandler tokenStore): base(tokenStore)
        {
            ViewBag.Table = _table;
        }


        [HttpGet]
        public virtual ActionResult _Index()
        {
            var query = "aaaabb";
            IEnumerable<dynamic> results;

            results = _table.FuzzySearch(query);

            return LearnMVC3JSON(results);
            //return Json(results, JsonRequestBehavior.AllowGet);


            //return View(results);
        }


        [HttpGet]
        public virtual ActionResult Index(string query)
        {
            IEnumerable<dynamic> results;

            if (!string.IsNullOrEmpty(query))
            {
                results = _table.FuzzySearch(query);
            }
            else
            {
                results = _table.All();
            }

            if (Request.IsAjaxRequest())
            {
                return LearnMVC3JSON(results);
                //return Json(results, JsonRequestBehavior.AllowGet);
            }

            return View(results);
        }

        [HttpGet]
        public virtual ActionResult Details(int id) {
            //return View(_table.FindBy(ID: id, schema: true));

            dynamic model = _table.Get(ID: id);
            model._Table = _table;
            return View(model);
        }

        [HttpGet]
        [RequireAdmin]
        public ActionResult Create() {
            return View(_table.Prototype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAdmin]
        public virtual ActionResult Create(FormCollection collection)
        {
            var item = _table.CreateFrom(collection);

            try
            {
                _table.Insert(item);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Error"] = "There was an error: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        //[RequireAdmin]
        public virtual ActionResult Edit(int id)
        {
            dynamic model = _table.Get(ID: id);
            model._Table = _table;
            return View(model);
        }

        //[HttpPut]
        //[RequireAdmin]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            var model = _table.CreateFrom(collection);
            try
            {
                _table.Update(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error editing: " + ex.Message;
                return View(model);
            }
        }

        [HttpDelete]
        [RequireAdmin]
        public virtual ActionResult Delete(int id)
        {
            _table.Delete(id);
            return View("Index",_table.All());
        }
    }
}