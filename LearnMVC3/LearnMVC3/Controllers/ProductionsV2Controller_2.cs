using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class ProductionsV2Controller : Controller {	
        dynamic _table;

        public ProductionsV2Controller()
        {
			_table = new ProductionsV2();
			ViewBag.Table = _table;
		}

        public ActionResult Index() {
			
            return View( _table.All());
        }

        public ActionResult Details(int id) {


            dynamic model = _table.Get(ID: id);
            model._Table = _table;
            return View(model);

            // return View(_table.FindBy(ID: id, schema: true));

            
        }

        public ActionResult Create() {
            return View(_table.Prototype);
        } 

        [HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
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
 
        public ActionResult Edit(int id)
        {
            dynamic model = _table.Get(ID: id);
			model._Table = _table;
            return View(model);
        }

            
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        public ActionResult Delete(int id)
        {
            _table.Delete(id);
            return View("Index",_table.All());
        }
    }
}
