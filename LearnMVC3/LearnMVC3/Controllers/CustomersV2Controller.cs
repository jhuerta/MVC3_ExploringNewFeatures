using LearnMVC3.Infrastructure;
using LearnMVC3.Model;
using LearnMVC3.Tests;

namespace LearnMVC3.Controllers
{
    public class CustomersV2Controller : CruddyControllerBase {
        public CustomersV2Controller(ITokenHandler tokenStore) : base(tokenStore)
        {
            _table = new UserModel();
            ViewBag.Table = _table;
        }
    }
}
