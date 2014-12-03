using Cinemapark.Lib.DB;
using Cinemapark.Lib.Entities;
using Cinemapark.Lib.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cinemapark.Lib
{
    public class DataService
    {
        private MovieDataContext _db;

        public DataService()
        {
            _db = new MovieDataContext();
        }

        public List<Multiplex> GetMultiplexes()
        {
            return _db.Multiplexes.ToList();
        }

        

        public void SaveMultiplexes(List<Multiplex> items)
        {
            foreach(var m in _db.Multiplexes)
            {
                _db.Multiplexes.DeleteOnSubmit(m);
            }
            _db.SubmitChanges();

            _db.Multiplexes.InsertAllOnSubmit(items);

            _db.SubmitChanges();
        }

        public static List<Movie> ParseMovieCollection(string xml, int multiplexId)
        {
            TextReader textReader = new StringReader(xml);
            var xElement = XElement.Load(textReader);

            return (from item in xElement.Descendants("item")
                    select new Movie
                    {
                        Title = item.GetAttributeOrDefault("title"),
                        MovieId = item.GetAttributeIntOrDefault("id"),
                        MultiplexId = multiplexId
                    }).OrderBy(x => x.Title).ToList();
        }
    }
}
