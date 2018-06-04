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
    /// Interaction logic for GoFishSetupPage.xaml
    /// </summary>
    public partial class GoFishSetupPage : Page
    {
        public GoFishSetupPage()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("GoFishPage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }
    }
}
