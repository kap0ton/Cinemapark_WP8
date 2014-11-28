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
        public static List<Multiplex> ParseMultiplexCollection(string xml)
        {
            TextReader textReader = new StringReader(xml);
            var xElement = XElement.Load(textReader);

            return (from item in xElement.Descendants("item")
                         select new Multiplex
                         {
                             City = item.GetAttributeOrDefault("city"),
                             Title = item.GetAttributeOrDefault("title"),
                             MultiplexId = item.GetAttributeIntOrDefault("id")
                         }).OrderBy(x => x.City).ThenBy(y => y.Title).ToList();
        }
    }
}
