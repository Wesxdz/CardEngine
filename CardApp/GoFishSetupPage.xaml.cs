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

        int playerCount = 2;
        public GoFishSetupPage()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            List<string> names = new List<string>();
            if (tbxPlayer1Name.IsEnabled)
                names.Add(tbxPlayer1Name.Text);
            if (tbxPlayer2Name.IsEnabled)
                names.Add(tbxPlayer2Name.Text);
            if (tbxPlayer3Name.IsEnabled)
                names.Add(tbxPlayer3Name.Text);
            if (tbxPlayer4Name.IsEnabled)
                names.Add(tbxPlayer4Name.Text);
            GoFishPage goFishPage = new GoFishPage(names.ToArray(), null);


            NavigationService.Navigate(goFishPage);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
           
            if (playerCount < 4)
            {
                playerCount++;
                switch (playerCount)
                {
                    case 3:
                        tbxPlayer3Name.IsEnabled = true;
                        tbxPlayer3Name.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        tbxPlayer4Name.IsEnabled = true;
                        tbxPlayer4Name.Visibility = Visibility.Visible;
                        break;
                }
            }

        }

        private void btnRemovePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (playerCount > 2)
            {
                switch (playerCount)
                {
                    case 3:
                        tbxPlayer3Name.IsEnabled = false;
                        tbxPlayer3Name.Visibility = Visibility.Hidden;
                        break;
                    case 4:
                        tbxPlayer4Name.IsEnabled = false;
                        tbxPlayer4Name.Visibility = Visibility.Hidden;
                        break;

                }
                playerCount--;
            }
        }
    }
}
