namespace LoggingKata
{
    public struct Point
    {
        //struct to use for coordinates
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}