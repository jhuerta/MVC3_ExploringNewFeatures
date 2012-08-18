using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Controllers;

namespace LearnMVC3.Infrastructure
{
    public class RequireAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (ApplicationController) filterContext.Controller;
            if(!controller.IsLoggedIn)
            {
                controller.TempData["Error"] = "You need to be logged in to do that!";
                filterContext.HttpContext.Response.Redirect("/account/logon");
                return;
            }

            var adminEmails = new string[]
                                  {
                                      "juan.huerta@gmail.com",
                                      "juan.huerta@asiarooms.com"
                                  };
            string userEmail = controller.CurrentUser.Email;
            if (!adminEmails.Contains(userEmail))
            {
                controller.TempData["Error"] = "Yo are not ahtorized ... tse tse tse";
                filterContext.HttpContext.Response.Redirect("/account/logon");
            }
        }
    }
}