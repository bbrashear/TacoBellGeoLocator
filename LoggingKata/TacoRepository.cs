using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LoggingKata
{
    class TacoRepository : ITacoRepository
    {
        private readonly IDbConnection _conn;
        public TacoRepository(IDbConnection connection)
        {
            _conn = connection;
        }
        public IEnumerable<ITrackable> GetAllTacoBells()
        {
            var bells = _conn.Query<TacoBell>("SELECT name FROM tacobells;").ToList(); //gets all names of taco bells from database and places in list
            var points = _conn.Query<Point>("SELECT latitude, longitude FROM tacobells;").ToList(); //gets all points from database and places in list
            for(int i = 0; i < bells.Count; i++) //for loop iterates through each taco bell and assigns the point at the same index to its location
            {
                bells[i].Location = points[i];
            }
            return bells;
        }

        public void CreateTacoBells(ITrackable tacoBell)
        {      
            //inserts taco bell information into database
            _conn.Execute("INSERT INTO tacobells (name, longitude, latitude) VALUES (@bellName, @bellLong, @bellLat);", 
               new { bellName = tacoBell.Name, bellLong = tacoBell.Location.Longitude, bellLat = tacoBell.Location.Latitude} );
        }
    }
}
