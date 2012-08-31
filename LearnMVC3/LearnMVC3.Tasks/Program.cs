using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using LearnMVC3.Model;
using LearnMVC3.Models;


namespace LearnMVC3.Tasks
{
    
    public class Program
    {
        static void Main(string[] args)
        {
            var db = new LearnMVC3DBContext();

            var productions = db.Productions;
            var orders = db.Orders;

            db.Database.ExecuteSqlCommand("delete from episodes");
            db.Database.ExecuteSqlCommand("delete from items");
            db.Database.ExecuteSqlCommand("delete from orders");
            db.Database.ExecuteSqlCommand("delete from productions");            


            //productions.Delete();
            //orders.Delete();



            for (var i = 0; i < 500; i++)
            {
                var production = CreateNewProduction(GetListOrEpisode(GetRandomNumber(1, 10)));
                var order = CreateNewOrder(GetListOrItems(GetRandomNumber(1, 10)));

                productions.Add(production);
                orders.Add(order);
                db.SaveChanges();
            }
        }

        static private readonly string[] productionTitles = {
                                                "MVC 1: Basic Programming",
                                                "MVC 2: The consolidation",
                                                "MVC 3: Advance Programming",
                                                "Ruby for C# Programmers",
                                                "Start GIT from Scratch!",
                                                "Inside Deep C#"
                                            };

        static private readonly string[] episodesTitles = {
                                                "1 - The beggining",
                                                "2. - Continuing what we started",
                                                "3. - Things get interesting",
                                                "4. - Consolidating",
                                                "5. - Review and closing on the topic",
                                                "6. - Conclusions",
                                                "A) Introduction",
                                                "B) The core",
                                                "C) Epilogue"
                                            };

        static private readonly string[] itemTitles = {
                                                "iPod",
                                                "iPad",
                                                "iPhone",
                                                "iHam",
                                                "iPho",
                                                "iPay",
                                                "Monitor",
                                                "CPU with case",
                                                "CPU without case"
                                            };


        static private readonly string[] emailList = {
                                                "chuck@norris.com",
                                                "silvester@stallone.com",
                                                "arnold@swasenagger.com",
                                                "billy@corgan.com",
                                                "jim@morrison.com",
                                                "kurt@cobain.com",
                                                "juan@gmail.com",
                                                "hasmin@gmai.com",
                                                "unai@gmail.com"
                                            };


        private static int GetRandomNumber(int min, int max)
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            return rnd.Next(min, max);
        }
        private static List<Episode> GetListOrEpisode(int number)
        {
            var episodeList = new List<Episode>();
            for (int i = 0; i < number; i++)
            {
                episodeList.Add(new Episode { Description = GetRandomValue(productionTitles), Title = GetRandomValue(productionTitles) });
            }
            return episodeList;

        }

                
        private static List<Items> GetListOrItems(int number)
        {
            var itemList = new List<Items>();
            for (int i = 0; i < number; i++)
            {
                itemList.Add(item: CreateNewItem(GetRandomValue(itemTitles)));
            }
            return itemList;

        }





        private static decimal GetRandomPrice()
        {
            var random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            return (decimal) random.NextDouble()*100;
        }
        private static string GetRandomValue(string[] array)
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var index = rnd.Next(array.Count());
            return array[index];
        }
        private static string GetDescription(string seed)
        {
            return "Description: " + seed;
        }
        private static Productions CreateNewProduction(List<Episode> episodes)
        {
            return new Productions()
            {
                Description = GetDescription(GetRandomValue(episodesTitles)),
                Title = GetRandomValue(episodesTitles),
                Price = GetRandomPrice(),
                Episodes = episodes
            };
        }
        private static Orders CreateNewOrder(List<Items> orderItems)
        {
            return new Orders()
            {
                Items = orderItems,
                created_at = DateTime.Now,
                email = GetRandomValue(emailList),
                token = Guid.NewGuid().ToString(),
                total_price = GetRandomPrice()
            };
        }
        private static Items CreateNewItem(string name)
        {
            return new Items()
            {
                grams = ((int)(new Random()).Next(1000, 10000)) / 100,
                title = name,
                product_id = Guid.NewGuid().ToString(),
                price = (((new Random()).Next(2000, 20000)) / 100).ToString(),
                quantity = (new Random()).Next(1, 20)
            };
        }



    }
}
