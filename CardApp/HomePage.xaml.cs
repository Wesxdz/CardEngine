using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("BlackJackSetupPage.xaml", UriKind.Relative);
            
            NavigationService.Navigate(page); 
        }

        private void btnPlayGoFish_Click(object sender, RoutedEventArgs e)
        {

            Uri page = new Uri("GoFishPage.xaml", UriKind.Relative);
            
            NavigationService.Navigate(page);
        }

        private void btnPlayWar_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("WarPage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void btnPlayPoker_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("PokerPage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}