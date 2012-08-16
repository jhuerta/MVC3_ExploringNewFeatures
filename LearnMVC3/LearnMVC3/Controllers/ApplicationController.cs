using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Infrastructure;
using LearnMVC3.Infrastructure.Logging;
using LearnMVC3.Tests;

namespace LearnMVC3.Controllers
{
    public class ApplicationController : Controller
    {
        public ITokenHandler TokenStore;
        dynamic _currentUser;

        public ApplicationController(ITokenHandler tokenStore)
        {
            TokenStore = tokenStore;
            //initialize this
            ViewBag.CurrentUser = CurrentUser ?? new { Email = "" };
        }

        public dynamic CurrentUser
        {
            get { 
                var token = TokenStore.GetToken();
                if(!String.IsNullOrEmpty(token))
                {
                    _currentUser = UserModel.FindByToken(token);

                    if(_currentUser == null)
                    {
                        // force log them out
                        TokenStore.RemoveClientAccess();
                    }
                }

                return _currentUser;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}
