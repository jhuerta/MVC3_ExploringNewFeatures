using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC3.Controllers;
using LearnMVC3.Infrastructure;
using LearnMVC3.Model;


namespace LearnMVC3.Areas.ReportingAreaRegistration.Controllers
{
    public class AnnualController : ApplicationController
    {
        public AnnualController(ITokenHandler tokenStore) : base(tokenStore)
        {


        
        }

        public ActionResult Sales(int id = 0)
        {
            var sales = GetData(id);

            return View(sales);
        }

        public FileResult CSVSales(int id = 0)
        {
            var sales = GetData(id);

            return new CSVResult(sales,"data.csv");
        }

        private static IEnumerable<dynamic> GetData(int id)
        {
            const string query = "SELECT Episodes.Title as EpisodeTitle, Productions.Title AS ProductionTitle, " +
                                 "Productions.Price, Episodes.Duration " +
                                 "FROM Productions " +
                                 "INNER JOIN Episodes ON Productions.ID = Episodes.Productions_ID " +
                                 "WHERE Productions.Price > {0}";

            var sales = DynamicModel.Open("LearnMVC3").Query(string.Format(query, id));
            return sales;
        }
    }
}
