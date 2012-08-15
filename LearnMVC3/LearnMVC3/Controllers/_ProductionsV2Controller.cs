using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Model;

namespace LearnMVC3.Controllers
{
    public class _ProductionsV2Controller : Controller
    {
        ProductionsV2 _productions = new ProductionsV2();
        public ActionResult Index()
        {
            
            var productionList = _productions.All().ToList();

            return View(productionList);
        }

        //
        // GET: /ProductionsV2/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            dynamic item = _productions.CreateFrom(collection);

            try
            {
                _productions.Insert(item);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["alert"] = "There was an error: " + ex.Message;
                return View();
            }
        }
        
        //
        // GET: /ProductionsV2/Edit/5
 
        public ActionResult Edit(int id)
        {
            dynamic item = _productions.Single(where: "ID = @0", args: id);
            return View(item);
        }

            
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = _productions.CreateFrom(collection);
            try
            {
                _productions.Update(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error editing: " + ex.Message;
                return View(model);
            }
        }

        //
        // GET: /ProductionsV2/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ProductionsV2/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
