using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class Orders : DynamicModel 
    {
        public Orders() : base("LearnMVC3", "Orders", "ID"){}

        [Required]
        public int ID { get; set; }
        public String email { get; set; }
        public String token { get; set; }
        public Decimal total_price { get; set; }
        public DateTime created_at { get; set; }
        public ICollection<Items> Items { get; set; }
    }
}