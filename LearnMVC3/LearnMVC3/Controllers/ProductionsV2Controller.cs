using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Controllers
{
    public class ProductionsV2Controller : CruddyControllerBase {
        public ProductionsV2Controller(ITokenHandler tokenStore) : base(tokenStore)
        {
            _table = new ProductionsV2();
            ViewBag.Table = _table;
        }
    }
}
