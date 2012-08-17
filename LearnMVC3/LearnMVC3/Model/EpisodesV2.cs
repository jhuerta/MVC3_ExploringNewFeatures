using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class EpisodesV2 :DynamicModel
    {
        public EpisodesV2() : base("LearnMVC3", "Episodes", "ID") { }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductionID { get; set; }


        public dynamic FuzzySearch(string query)
        {
            var queryFormatted = string.Format("select * " +
                                      "from episodes where title like ('%{0}%') " +
                                      "or description like ('%{0}%') " +
                                      "or productionid like ('%{0}%')", query);
            return this.Query(queryFormatted);
        }

    }
}