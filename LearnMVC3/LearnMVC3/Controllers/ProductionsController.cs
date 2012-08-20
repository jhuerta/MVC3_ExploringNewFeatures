using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Model;
using LearnMVC3.Models;
using LearnMVC3DBContext = LearnMVC3.Models.LearnMVC3DBContext;

namespace LearnMVC3.Controllers
{   
    public class ProductionsController : Controller
    {
        private LearnMVC3DBContext context = new LearnMVC3DBContext();

        //
        // GET: /Productions/

        public ViewResult Index()
        {
            return View(context.Productions.Include(production => production.Episodes).ToList());
        }

        //
        // GET: /Productions/Details/5

        public ViewResult Details(int id)
        {
            Productions productions = context.Productions.Single(x => x.ID == id);
            return View(productions);
        }

        //
        // GET: /Productions/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Productions/Create

        [HttpPost]
        public ActionResult Create(Productions productions)
        {
            if (ModelState.IsValid)
            {
                context.Productions.Add(productions);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(productions);
        }
        
        //
        // GET: /Productions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Productions productions = context.Productions.Single(x => x.ID == id);
            return View(productions);
        }

        //
        // POST: /Productions/Edit/5

        [HttpPost]
        public ActionResult Edit(Productions productions)
        {
            if (ModelState.IsValid)
            {
                context.Entry(productions).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productions);
        }

        //
        // GET: /Productions/Delete/5
 
        public ActionResult Delete(int id)
        {
            Productions productions = context.Productions.Single(x => x.ID == id);
            return View(productions);
        }

        //
        // POST: /Productions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Productions productions = context.Productions.Single(x => x.ID == id);
            context.Productions.Remove(productions);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}