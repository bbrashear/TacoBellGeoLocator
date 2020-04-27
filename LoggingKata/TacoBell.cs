using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    public class TacoBell : ITrackable
    {
        //class used to describe restaurant locations
        public TacoBell()
        {
        }

        public TacoBell(string name, Point location)
        {
            Name = name;
            Location = location;
        }

        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
