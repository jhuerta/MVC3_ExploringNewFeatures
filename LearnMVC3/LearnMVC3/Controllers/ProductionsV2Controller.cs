using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class CruddyControllerBase : ApplicationController
    {
        protected dynamic _table = null;

        protected CruddyControllerBase(ITokenHandler tokenStore): base(tokenStore)
        {
            ViewBag.Table = _table;
        }

        public virtual ActionResult Index() {
			
            return View( _table.All());
        }

        public virtual ActionResult Details(int id) {
            //return View(_table.FindBy(ID: id, schema: true));

            dynamic model = _table.Get(ID: id);
            model._Table = _table;
            return View(model);
        }

        public ActionResult Create() {
            return View(_table.Prototype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public virtual ActionResult Edit(int id)
        {
            dynamic model = _table.Get(ID: id);
            model._Table = _table;
            return View(model);
        }

        [HttpPost]
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

        public virtual ActionResult Delete(int id)
        {
            _table.Delete(id);
            return View("Index",_table.All());
        }
    }

    public class ProductionsV2Controller : CruddyControllerBase {
        public ProductionsV2Controller(ITokenHandler tokenStore) : base(tokenStore)
        {
            _table = new ProductionsV2();
        }
    }
}
