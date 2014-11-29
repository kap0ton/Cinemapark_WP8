﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace Cinemapark.Lib.Entities
{
	public class Movie : INotifyPropertyChanged
	{
		/// <summary>
		/// {0} - multiplex id
		/// </summary>
		public const string MoviesUri = "http://www.cinemapark.ru/gadgets/data/movies/{0}/";

		/// <summary>
		///  //{0} - multiplex id, {1} - booking numper
		/// </summary>
		public const string BookinInfoUri = "http://booking.www.cinemapark.ru/info/{0}/{1}/";

		/// <summary>
		/// {0} - movie id
		/// </summary>
		public const string PosterUri = "http://stasis.www.cinemapark.ru/img/film/poster_large/{0}.jpg";

		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged("Title");
			}
		}

		public int MovieId { get; set; }

		public int MultiplexId { get; set; }

        public Uri ImageUrl
        {
            get
            {
                var id = MovieId.ToString(CultureInfo.InvariantCulture).Substring(0, 4);
                return new Uri(string.Format(PosterUri, id), UriKind.Absolute);
            }
        }

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
