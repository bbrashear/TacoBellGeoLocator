using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using Microsoft.Extensions.Configuration;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("Default");
            #endregion

            var lines = File.ReadAllLines(csvPath); //creates new string[] 'lines' that calls the File class and uses 'Read All Lines'
                                                    //method to read all lines in the file path and convert them to a string[]

            var parser = new TacoParser(); //creates a new instance of the TacoParser class to be able to use the 'Parse' method

            var locations = lines.Select(parser.Parse).ToArray(); //creates a new ITrackable instance 'locations', parses each 
                                                                  //line, and returns it in an ITrackable array
            
            //Empty variables to be used later
            ITrackable tacoBell1 = new TacoBell();
            ITrackable tacoBell2 = new TacoBell();
            double farthestDistance = 0;


            for(int i = 0; i < locations.Length; i++) //for loop iterates through each of our lines
            {
                var locA = locations[i]; //sets the first location to compare to the location at the current index
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude); //creates a GeoCoordinate and stores locA's lat and long into coordinates
                for(int a = 1; a < locations.Length; a++) //nested for loop allows us to compare to other locations
                {
                    var locB = locations[a]; //sets the location to compare to
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude); //creates a GeoCoordinate for the comparison location
                    double distance = corA.GetDistanceTo(corB); //Using the GeoCoordinate class, creates a variable that stores the distance between the two
                                                               //coordinates of our comparing locations
                    if(distance > farthestDistance) //compares the distance between current location to the farthest distance calculated so far
                    {
                        //updates farthest distance if the new distance is greater
                        farthestDistance = distance; 
                        //stores the locations with the farthest distances so far as our two Taco Bells
                        tacoBell1 = locA; 
                        tacoBell2 = locB;
                    }
                }
            }

            //Below, the same thing is done in a foreach loop

            //foreach (var location in locations)
            //{
            //    var locA = location;
            //    var corA = new GeoCoordinate(location.Location.Latitude, location.Location.Longitude);
            //    foreach (var loc in locations)
            //    {
            //        var locB = loc;
            //        var corB = new GeoCoordinate(loc.Location.Latitude, loc.Location.Longitude);
            //        double distance = corA.GetDistanceTo(corB);
            //        if (distance > farthestDistance)
            //        {
            //            farthestDistance = distance;
            //            tacoBell1 = locA;
            //            tacoBell2 = locB;
            //        }
            //    }
            //}

            Console.WriteLine($"The Taco Bells furthest away from each other are {tacoBell1.Name} and {tacoBell2.Name}:" +
                $" {farthestDistance} meters apart");
            Console.ReadLine();
        
        }
    }
}