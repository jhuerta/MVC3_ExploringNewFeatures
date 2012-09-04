using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quixote;



namespace LearnMVC3.UITesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner.SiteRoot = "http://learnmvc3.local";

            TheFollowing.Describes("Home Page");

            It.Should("Have Layout in the Title", () =>
                                                         {
                                                             return Runner.Get("/").Title.ShouldContain("Layout");
                                                         });

            

            TheFollowing.Describes("Logging On the Page");

            It.Should("Says Welcome!", () =>
                                           {
                                               var post = Runner.Post("/account/logon",
                                                                      new
                                                                          {
                                                                              email = "juan1@gmail.com",
                                                                              password = "password"
                                                                          });
                                               //Console.WriteLine(post.Html);
                                               return post.Title.ShouldContain("Layout");
                                           });

            Console.Read();

            

        }
    }
}
