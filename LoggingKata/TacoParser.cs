namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            if(string.IsNullOrEmpty(line)) //returns null if the line gives us nothing to parse
            {
                return null;
            }
            
            var cells = line.Split(','); //splits line up into an array of strings, separated by the char ','

            if (cells.Length < 3) //returns null if there is not the correct amount of data
            {
                return null;
            }

            //stores the latitude, longitude, and name in a string variable
            string strLat = cells[0];
            string strLong = cells[1];
            string storeName = cells[2].Trim();

            //parses latitude and longitude as a double
            double latitude = double.Parse(strLat); 
            double longitude = double.Parse(strLong);

            //returns null if data is out of bounds of coordinate system
            if(latitude > 85 || latitude < -85.05115)
            {
                return null;
            }
            if(longitude > 180 || longitude < -180)
            {
                return null;
            }

            Point location = new Point() { Latitude = latitude, Longitude = longitude }; //instantiates a new Point to use in our TacoBell class

            TacoBell aTacoBell = new TacoBell() { Name = storeName, Location = location }; //instantiates a new TacoBell with name and point set

            return aTacoBell; //returns an instance of TacoBell class which conforms to ITrackable interface
        }
    }
}