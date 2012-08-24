using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

        public string ReadJson()
        {
            var bodyText = "";
            using (var stream = Request.InputStream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                    bodyText = reader.ReadToEnd();
            }
            return bodyText;
        }

        public dynamic SqueezeJson()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoObjectConverter() });
            var bodyText = "";
            using (var stream = Request.InputStream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                    bodyText = reader.ReadToEnd();
            }
            return serializer.Deserialize(bodyText, typeof(ExpandoObject));
        }

        public bool IsLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }

        public ActionResult LearnMVC3JSON(dynamic content)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoObjectConverter() });
            var json = serializer.Serialize(content);
            Response.ContentType = "application/json";
            return Content(json);

        }
    }
}
