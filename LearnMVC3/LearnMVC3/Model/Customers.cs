using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public class Customers: DynamicModel
    {
        public Customers(): base("LearnMVC3", "Productions", "ID") 
        {

        }
    }
}