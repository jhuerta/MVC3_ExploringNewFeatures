using System.ComponentModel.DataAnnotations;

namespace LearnMVC3.Model
{
    public class Items : DynamicModel 
    {
        public Items() : base("LearnMVC3", "Items", "ID") { }

        [Required]
        public int ID { get; set; }
        public int grams { get; set; }
        public string price { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public int requires_shipping { get; set; }
        public string title { get; set; }
        public int OrderID { get; set; }
    }
}