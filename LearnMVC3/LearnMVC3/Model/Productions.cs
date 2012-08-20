using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class Productions
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(400)]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }

    public class Episode
    {
        [Required]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductionID { get; set; }
    }
}