using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnMVC3.Infrastructure
{
    public class LearnMVC3BaseViewPage<TModel> : WebViewPage<TModel>
    {

        public dynamic Routes { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Routes = new DynamicRoute(Url);
        }

        public override void Execute()
        {
            base.ExecutePageHierarchy();
        }
    }
}