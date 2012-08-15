using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class EpisodesV2Controller : Controller {	
        dynamic _table;

		public EpisodesV2Controller() {
			_table = new EpisodesV2();
			ViewBag.Table = _table;
		}

        public ActionResult Index() {
			
            return View( _table.All());
        }

        public ActionResult Details(int id) {
            return View(_table.FindBy(ID: id, schema: true));
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
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
