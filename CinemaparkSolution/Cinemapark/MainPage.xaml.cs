using Cinemapark.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Cinemapark
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MainPageViewModel _mainPageViewModel;

        public MainPage()
        {
            InitializeComponent();

            _mainPageViewModel = new MainPageViewModel();
            DataContext = _mainPageViewModel;

            BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _mainPageViewModel.LoadMovies();
        }

        private void MovieListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            var btnSettings = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.feature.settings.rest.png", UriKind.Relative));
            btnSettings.Text = "settings"; //AppResources.AppBarButtonText;
            btnSettings.Click += btnSettings_Click;
            ApplicationBar.Buttons.Add(btnSettings);

            var btnRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.refresh.rest.png", UriKind.Relative));
            btnRefresh.Text = "refresh";
            btnRefresh.Click += btnRefresh_Click;
            ApplicationBar.Buttons.Add(btnRefresh);

            // Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void btnSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Settings.xaml", UriKind.Relative));
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            _mainPageViewModel.LoadMovies();
        }
    }
}
