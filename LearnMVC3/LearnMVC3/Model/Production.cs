using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class Production
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(400)]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}