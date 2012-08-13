using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMVC3.Model
{
    public static class Quotes
    {
        public static dynamic FromUsers()
        {
            var quotes = new[]
                             {
                                 "Money frees you from doing things you dislike. Since I dislike doing nearly everything, money is handy. --- Grouch Marx"
                                 ,"I find television very educating. Every time somebody turns on the set, I go into the other room and read a book. --- Grouch Marx"
                                 ,"A child of five would understand this. Send someone to fetch a child of five. --- Grouch Marx"
                                 ,"It frees you from doing things you dislike. Since I dislike doing nearly everything, money is handy. --- Grouch Marx"
                                 , "Whoever called it necking was a poor judge of anatomy. --- Grouch Marx"
                                 , "Humor is reason gone mad. --- Grouch Marx"
                                 , "I've had a wonderful evening - but this wasn't it. --- Grouch Marx"
                                 , "Politics doesn't make strange bedfellows - marriage does. --- Grouch Marx"
                                 ,"There's one way to find out if a man is honest - ask him. If he says,  you know he is a crook. --- Grouch Marx"
                                 ,"In any relationship, the woman has control, the clever ones don't let the men know. --- Grouch Marx"
                                 , "Don't let the fear of the thorn keep you from the rose. --- Grouch Marx"
                                 , "Those are my principles. If you don't like them, I have others. --- Grouch Marx"
                                 , "Time flies like the wind. Fruit flies like a banana. --- Grouch Marx"
                                 , "If you want to know where God is ask a drunk. --- Grouch Marx"
                                 , "If you're not having fun, you're doing something wrong. --- Grouch Marx"
                                 , "If you go by the book then you have no book of your own to write. --- Grouch Marx"
                                 , "Either this man is dead or my watch has stopped. --- Grouch Marx"
                                 , "24 hours in a day...24 beers in a case...coincidence? --- Grouch Marx"
                                 , "Patience is the art of finding something else to do. --- Grouch Marx"
                                 ,"While money can't buy happiness, it certainly lets you choose your own form of misery. --- Grouch Marx"
                             };

            return quotes.Shuffle(new Random()).ToList();

            //Random rnd = new Random();
            //return quotes.OrderBy(x => rnd.Next()).ToList();
        }

        public  static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray(); 
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

    }
}