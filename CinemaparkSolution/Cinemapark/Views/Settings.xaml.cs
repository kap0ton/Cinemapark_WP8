using Cinemapark.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
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

            BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _settingsViewModel.LoadMultiplexes();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //_settingsViewModel.SaveSelectedMultiplex();
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            var btnSave = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.save.rest.png", UriKind.Relative));
            btnSave.Text = "Save"; //AppResources.AppBarButtonText;
            btnSave.Click += btnSave_Click;
            ApplicationBar.Buttons.Add(btnSave);

            // Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            _settingsViewModel.SaveSelectedMultiplex();
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
