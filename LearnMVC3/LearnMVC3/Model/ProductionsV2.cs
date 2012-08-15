using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class ProductionsV2 :DynamicModel
    {
        public ProductionsV2() : base("LearnMVC3", "Productions", "ID") { }
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}