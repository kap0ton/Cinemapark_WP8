using Cinemapark.Lib.Entities;
using System.Data.Linq;

namespace Cinemapark.Lib.DB
{
    public class MovieDataContext : DataContext
    {
        public static string ConnectionString = "Data Source=isostore:/Movie.sdf";

        public MovieDataContext()
            : base(ConnectionString)
        { }

        public Table<Multiplex> Multiplexes;
    }
}
