using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class LearnMVC3Controller : ApplicationController {
        public LearnMVC3Controller(ITokenHandler tokenStore) : base(tokenStore)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Productions()
        {
            return View();
        }
    }
}
