using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CSV to Objects");

            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            // Top 10 Combined Efficiency with Ordered By Name
            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            foreach (var car in query.Take(10))
            {
                System.Console.WriteLine($"{car.Name} : {car.Combined}");
            }

            //Top 10 Fuel Efficient BMW cars
            var queryBMW =
                from car in cars
                where car.Manufacturer.Equals("BMW")
                orderby car.Combined descending, car.Name ascending
                select car;
            
            System.Console.WriteLine();
            System.Console.WriteLine("Printing op 10 Fuel Efficient BMW cars");
            foreach (var car in queryBMW.Take(10))
            {
                System.Console.WriteLine($"{car.Name} : {car.Combined}");
            }

            System.Console.WriteLine();
            //Top 10 Fuel Efficient cars with Manufacturers
            var queryJoin =
                from car in cars
                join manufacturer in manufacturers 
                    on new { car.Manufacturer, car.Year }
                        equals new { Manufacturer = manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name ascending
                select new 
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };
            foreach (var car in queryJoin.Take(10))
            {
                System.Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("With Group Join");
            var queryGroupJoin =
                manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, 
                    (m, g) => 
                     new
                       {
                           Manufacturer = m,
                           Cars = g
                       })
                .OrderBy(m => m.Manufacturer.Name);
            foreach (var group in queryGroupJoin)
            {
                System.Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderBy(c => c.Name))
                {
                    System.Console.WriteLine($"{car.Name} : {car.Year}");
                }
            }
        }

        private static IEnumerable<Car> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                   File.ReadAllLines(path)
                       .Where(l => l.Length > 1)
                       .Select(l =>
                       {
                           var columns = l.Split(',');
                           return new Manufacturer
                           {
                               Name = columns[0],
                               Headquarters = columns[1],
                               Year = int.Parse(columns[2])
                           };
                       });
            return query.ToList();
        }
    }
}
