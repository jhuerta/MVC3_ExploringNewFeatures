using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class EpisodesV2Controller : CruddyControllerBase
    {
        public EpisodesV2Controller(ITokenHandler tokenStore)
            : base(tokenStore)
        {
            _table = new EpisodesV2();
            ViewBag.Table = _table;
        }
    }
}
