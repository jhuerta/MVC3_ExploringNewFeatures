using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LearnMVC3.Infrastructure
{
    public interface ITokenHandler
    {
        void SetClientAccess(string token);
        void RemoveClientAccess();
        string GetToken();
    }

    public class FormsAuthTokenStore : ITokenHandler
    {
        public void SetClientAccess(string token)
        {
            FormsAuthentication.SetAuthCookie(token, true);
        }

        public void RemoveClientAccess()
        {
            FormsAuthentication.SignOut();
        }

        public string GetToken()
        {
            var result = "";
            if (HttpContext.Current.Request.Cookies["auth"] != null)
            {
                result = HttpContext.Current.Request.Cookies["auth"].Value;
            }
            return result;
        }
    }
}