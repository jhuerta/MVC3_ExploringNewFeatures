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
    }
}