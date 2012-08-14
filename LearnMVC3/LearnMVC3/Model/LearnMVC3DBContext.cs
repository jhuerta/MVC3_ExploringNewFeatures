using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class LearnMVC3DBContext : DbContext
    {
        public LearnMVC3DBContext()
        {
        }

        public DbSet<Production> Productions { get; set; }
    }
}