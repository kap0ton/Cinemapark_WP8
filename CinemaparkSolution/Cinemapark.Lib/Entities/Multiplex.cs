using Cinemapark.Lib.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cinemapark.Lib.Entities
{
    [Table]
    public class Multiplex : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// 18 - Saratov, Triumph Mall
        /// </summary>
        //public const int DefaultMultiplexId = 18;

        public const string MultiplexUri = "http://www.cinemapark.ru/gadgets/data/multiplexes/";

        private int _multiplexId;

        [Column(IsPrimaryKey = true, IsDbGenerated = false, DbType = "INT NOT NULL", CanBeNull = false)]
        public int MultiplexId
        {
            get { return _multiplexId; }
            set
            {
                if (_multiplexId != value)
                {
                    OnPropertyChanging("MultiplexId");
                    _multiplexId = value;
                    OnPropertyChanged("MultiplexId");
                }
            }
        }

        private string _city;

        [Column]
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    OnPropertyChanging("City");
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string _title;

        [Column]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    OnPropertyChanging("Title");
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        //public string FullName
        //{
        //    get { return City + ", " + Title; }
        //}

        [Column(IsVersion = true)]
        private Binary _version;

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

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanging(string propertyName)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
