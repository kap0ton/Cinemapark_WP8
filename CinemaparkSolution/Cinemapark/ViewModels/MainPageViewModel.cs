using Cinemapark.Lib;
using Cinemapark.Lib.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Cinemapark.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly AppSettings _appSettings;

        private readonly ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
        }

        private bool _progressBarIsIndeterminate;
        public bool ProgressBarIsIndeterminate
        {
            get { return _progressBarIsIndeterminate; }
            set
            {
                _progressBarIsIndeterminate = value;
                OnPropertyChanged("ProgressBarIsIndeterminate");
            }
        }

        private Visibility _progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public MainPageViewModel()
        {
            _appSettings = new AppSettings();
            _movies = new ObservableCollection<Movie>();
            ProgressBarIsIndeterminate = false;
            ProgressBarVisibility = Visibility.Collapsed;
        }

        public void LoadMovies()
        {
            if (_appSettings.Multiplex != null)
            {
                UpdateProgressBar(true);
                var client = new WebClient();
                client.DownloadStringCompleted += GetMoviesCompleted;
                var path = string.Format(Movie.MoviesUri, _appSettings.Multiplex.MultiplexId);
                client.DownloadStringAsync(new Uri(path, UriKind.Absolute));
            }
        }

        private void GetMoviesCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                }
                else
                {
                    var multiplexId = _appSettings.Multiplex.MultiplexId;

                    var items = DataService.ParseMovieCollection(e.Result, multiplexId);

                    Movies.Clear();
                    foreach (var movie in items)
                    {
                        Movies.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateProgressBar(false);
            }
        }

        private void UpdateProgressBar(bool isEnabled)
        {
            if (isEnabled)
            {
                ProgressBarIsIndeterminate = true;
                ProgressBarVisibility = Visibility.Visible;
            }
            else
            {
                ProgressBarIsIndeterminate = false;
                ProgressBarVisibility = Visibility.Collapsed;
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
