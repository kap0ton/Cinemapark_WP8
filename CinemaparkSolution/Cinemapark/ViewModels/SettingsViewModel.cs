using Cinemapark.Lib;
using Cinemapark.Lib.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;

namespace Cinemapark.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly AppSettings _appSettings;

        private readonly ObservableCollection<Multiplex> _multiplexes;
        public ObservableCollection<Multiplex> Multiplexes
        {
            get { return _multiplexes; }
        }

        private Multiplex _selectedMuliplex;
        public Multiplex SelectedMultiplex
        {
            get { return _selectedMuliplex; }
            set
            {
                _selectedMuliplex = value;
                OnPropertyChanged("SelectedMultiplex");
            }
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

        public SettingsViewModel()
        {
            _appSettings = new AppSettings();
            _multiplexes = new ObservableCollection<Multiplex>();
            ProgressBarIsIndeterminate = false;
            ProgressBarVisibility = Visibility.Collapsed;
        }

        public void LoadMultiplexes()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += GetMultiplexesCompleted;
            client.DownloadStringAsync(new Uri(Multiplex.MultiplexUri, UriKind.Absolute));
            UpdateProgressBar(true);
        }

        private void GetMultiplexesCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                }
                else
                {
                    var items = DataService.ParseMultiplexCollection(e.Result);

                    Multiplexes.Clear();
                    foreach (var multiplex in items)
                    {
                        Multiplexes.Add(multiplex);
                    }
                    SetSelectedMultiplex();
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

        private void SetSelectedMultiplex()
        {
            if (_appSettings.Multiplex != null)
            {
                SelectedMultiplex = Multiplexes.FirstOrDefault(p => p.MultiplexId == _appSettings.Multiplex.MultiplexId);
            }
        }

        public void SaveSelectedMultiplex()
        {
            _appSettings.Multiplex = SelectedMultiplex;
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
