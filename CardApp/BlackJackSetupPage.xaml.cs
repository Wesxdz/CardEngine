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
            int playerAmo = 1;
            if(player2Text.Visibility == Visibility.Visible) { playerAmo++; }
            else if(player3Text.Visibility == Visibility.Visible) { playerAmo++; }
            else if (player4Text.Visibility == Visibility.Visible) { playerAmo++; }
            else if (player5Text.Visibility == Visibility.Visible) { playerAmo++; }  

            BlackJackPage blackJackPage = new BlackJackPage();
            blackJackPage.playerAmo = playerAmo;
            blackJackPage.playerNames = new string[playerAmo];

            blackJackPage.playerNames[0] = player1Text.Text;
            if(player2Text.Visibility == Visibility.Visible) {
                blackJackPage.playerNames[1] = player2Text.Text;
            } else if(player3Text.Visibility == Visibility.Visible) {
                blackJackPage.playerNames[2] = player3Text.Text;
            } else if (player4Text.Visibility == Visibility.Visible) {
                blackJackPage.playerNames[3] = player4Text.Text;
            } else if (player5Text.Visibility == Visibility.Visible) {
                blackJackPage.playerNames[4] = player5Text.Text;
            }
            //Uri page = new Uri("BlackJackPage.xaml", UriKind.Relative);

            NavigationService.Navigate(blackJackPage);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e) {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void addPlayerBtn_Click(object sender, RoutedEventArgs e) {
            if(playerCount < 5) {
                switch (playerCount) {
                    case 1:
                        player2Label.Visibility = Visibility.Visible;
                        player2Text.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        player3Label.Visibility = Visibility.Visible;
                        player3Text.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        player4Label.Visibility = Visibility.Visible;
                        player4Text.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        player5Label.Visibility = Visibility.Visible;
                        player5Text.Visibility = Visibility.Visible;
                        break;
                }
                playerCount++;
            } else {
                // Mothing...
            }
        }
        private void removePlayerBtn_Click(object sender, RoutedEventArgs e) {
            if (playerCount > 1) {
                switch (playerCount) {
                    case 2:
                        player2Label.Visibility = Visibility.Hidden;
                        player2Text.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        player3Label.Visibility = Visibility.Hidden;
                        player3Text.Visibility = Visibility.Hidden;
                        break;
                    case 4:
                        player4Label.Visibility = Visibility.Hidden;
                        player4Text.Visibility = Visibility.Hidden;
                        break;
                    case 5:
                        player5Label.Visibility = Visibility.Hidden;
                        player5Text.Visibility = Visibility.Hidden;
                        break;
                }
                playerCount--;
            } else {
                // Mothing...
            }
        }
    }
}
