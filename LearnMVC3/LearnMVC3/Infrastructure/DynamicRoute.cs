using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnMVC3.Infrastructure
{
    public class DynamicRoute : DynamicObject
    {
        private UrlHelper _helper;
        public DynamicRoute(UrlHelper helper)
        {
            _helper = helper;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetURL(binder.Name, new object[0]);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetURL(binder.Name, args);

            return true;
        }

        private string GetURL(string methodName, object[] args)
        {
            var stems = methodName.Split('_');
            var routeName = stems[0];
            var url = "";

            if (args.Length > 0)
            {
                url = _helper.RouteUrl(routeName, args[0]);
            }
            else
            {
                url = _helper.RouteUrl(routeName);
            }


            if (stems.Last() == "url")
            {
                url = Root(false) + url;
            }
            return url;
        }

        public  string Root(bool includeAppPath = true)
        {
            var context = HttpContext.Current;
            var Port = context.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;
            var Protocol = context.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            var appPath = "";
            if (includeAppPath)
            {
                appPath = context.Request.ApplicationPath;
                if (appPath == "/")
                    appPath = "";
            }
            var sOut = Protocol + context.Request.ServerVariables["SERVER_NAME"] + Port + appPath;
            return sOut;
        }
    }
}