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

namespace CardApp {
    /// <summary>
    /// Interaction logic for BlackJackSetup.xaml
    /// </summary>
    public partial class BlackJackSetupPage : Page {

        int playerCount;

        public BlackJackSetupPage() {
            InitializeComponent();

            playerCount = 2;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {
            Uri page = new Uri("BlackJackPage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e) {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void addPlayerBtn_Click(object sender, RoutedEventArgs e) {
            if(playerCount < 5) {
                switch (playerCount) {
                    case 2:
                        player3Label.IsEnabled = true;
                        player3Text.IsEnabled = true;
                        break;
                    case 3:
                        player4Label.IsEnabled = true;
                        player4Text.IsEnabled = true;
                        break;
                    case 4:
                        player5Label.IsEnabled = true;
                        player5Text.IsEnabled = true;
                        break;
                }
                playerCount++;
            } else {
                // Mothing...
            }
        }
        private void removePlayerBtn_Click(object sender, RoutedEventArgs e) {
            if (playerCount > 2) {
                switch (playerCount) {
                    case 3:
                        player3Label.IsEnabled = false;
                        player3Text.IsEnabled = false;
                        break;
                    case 4:
                        player4Label.IsEnabled = false;
                        player4Text.IsEnabled = false;
                        break;
                    case 5:
                        player5Label.IsEnabled = false;
                        player5Text.IsEnabled = false;
                        break;
                }
                playerCount--;
            } else {
                // Mothing...
            }
        }
    }
}
