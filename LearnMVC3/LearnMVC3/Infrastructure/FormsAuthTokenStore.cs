using System;
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
            HttpContext.Current.Response.Cookies["auth"].Value = token;
            HttpContext.Current.Response.Cookies["auth"].Expires = DateTime.Today.AddDays(60);
            HttpContext.Current.Response.Cookies["auth"].HttpOnly = true;

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