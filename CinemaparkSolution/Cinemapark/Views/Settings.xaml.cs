using Cinemapark.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace Cinemapark.Views
{
    public partial class Settings : PhoneApplicationPage
    {
        private readonly SettingsViewModel _settingsViewModel;

        public Settings()
        {
            InitializeComponent();

            _settingsViewModel = new SettingsViewModel();
            DataContext = _settingsViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _settingsViewModel.LoadMultiplexes();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _settingsViewModel.SaveSelectedMultiplex();
        }
    }
}
