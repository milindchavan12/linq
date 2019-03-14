using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movies Queries with LINQ");

            var movies = new List<Movie> 
            {
                new Movie { Title = "Life Of PI", Rating = 7.9F, Year = 2012},
                new Movie { Title = "Cast Away", Rating = 7.8F, Year = 2000},
                new Movie { Title = "The Shawshank Redemption", Rating = 9.3F, Year = 1994},
                new Movie { Title = "The Green Mile", Rating = 8.6F, Year = 1999}
            };

            Console.WriteLine("------------------");
            Console.WriteLine("[LINQ] Movies released after 1999:");
            var query = movies.Where(m => m.Year > 1999);
            
            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);   
            }

            Console.WriteLine("------------------");
            Console.WriteLine("[Custome Filter] Movies released after 1999:");
            var query2 = movies.Filter(m => m.Year > 1999);

            foreach (var movie in query2)
            {
                Console.WriteLine(movie.Title);
            }
        }
    }
}
