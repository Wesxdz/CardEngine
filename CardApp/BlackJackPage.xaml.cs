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
    /// Interaction logic for BlackJackPage.xaml
    /// </summary>
    public partial class BlackJackPage : Page
    {

        public int playerAmo;
        public string[] playerNames;
        BlackjackGame game;

        int currPlayerTurn = 0;

        public BlackJackPage()
        {
            InitializeComponent();

            anteComboBox.Items.Add(1);
            anteComboBox.Items.Add(5);
            anteComboBox.Items.Add(10);
            anteComboBox.SelectedIndex = 0;
        }

        public void Initialize() {
            game = new BlackjackGame();
            game.InitializePlayers(playerAmo, playerNames, null);
            game.Start();

            // Do antes
            // Start Round

            // Display Cards
            // Buttons
            // Hit Btn

            SetupAnte();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void anteBtn_Click(object sender, RoutedEventArgs e) {
            game.TakeAnte(currPlayerTurn, (int)anteComboBox.SelectedItem);

            int previousPlayerTurn = currPlayerTurn;
            currPlayerTurn = game.GetCurrentPlayer();
            // Swap between this panel and another...

            var previousPlayer = game.GetPlayer(previousPlayerTurn);
            var currPlayer = game.GetPlayer(currPlayerTurn);

            currPlayerNameLabel.Content = currPlayer.name;

            switch (currPlayerTurn) {
                case 1:
                    player2NameLabel.Content = previousPlayer.name;
                    player2AnteLabel.Content = "$" +previousPlayer.currBet;
                    break;
                case 2:
                    player3NameLabel.Content = previousPlayer.name;
                    player3AnteLabel.Content = "$" + previousPlayer.currBet;
                    break;
                case 3:
                    player4NameLabel.Content = previousPlayer.name;
                    player4AnteLabel.Content = "$" + previousPlayer.currBet;
                    break;
                case 4:
                    player5NameLabel.Content = previousPlayer.name;
                    player5AnteLabel.Content = "$" + previousPlayer.currBet;
                    break;
            }
            if(previousPlayerTurn == playerAmo-1) {
                HideAnte();
                SetupBlackBlackJack();
            }
        }

        public void SetupAnte() {
            currPlayerTurn = game.GetCurrentPlayer();
            var currentPlayer = game.GetPlayer(currPlayerTurn);

            currPlayerNameLabel.Visibility = Visibility.Visible;
            currPlayerNameLabel.Content = currentPlayer.name;

            anteBtn.Visibility = Visibility.Visible;
            anteComboBox.Visibility = Visibility.Visible;

            if (playerAmo > 1) {
                var p = game.GetPlayer(1);

                player2NameLabel.Visibility = Visibility.Visible;
                player2AnteLabel.Visibility = Visibility.Visible;

                player2NameLabel.Content = p.name;
                player2AnteLabel.Content = "$" + p.currBet;
            } else {
                player2NameLabel.Visibility = Visibility.Hidden;
                player2AnteLabel.Visibility = Visibility.Hidden;
            }

            if (playerAmo > 2) {
                var p = game.GetPlayer(2);

                player3NameLabel.Visibility = Visibility.Visible;
                player3AnteLabel.Visibility = Visibility.Visible;

                player3NameLabel.Content = p.name;
                player3AnteLabel.Content = "$" + p.currBet;
            } else {
                player3NameLabel.Visibility = Visibility.Hidden;
                player3AnteLabel.Visibility = Visibility.Hidden;
            }

            if (playerAmo > 3) {
                var p = game.GetPlayer(3);

                player4NameLabel.Visibility = Visibility.Visible;
                player4AnteLabel.Visibility = Visibility.Visible;

                player4NameLabel.Content = p.name;
                player4AnteLabel.Content = "$" + p.currBet;
            } else {
                player4NameLabel.Visibility = Visibility.Hidden;
                player4AnteLabel.Visibility = Visibility.Hidden;
            }

            if (playerAmo > 4) {
                var p = game.GetPlayer(4);

                player5NameLabel.Visibility = Visibility.Visible;
                player5AnteLabel.Visibility = Visibility.Visible;

                player5NameLabel.Content = p.name;
                player5AnteLabel.Content = "$" + p.currBet;
            } else {
                player5NameLabel.Visibility = Visibility.Hidden;
                player5AnteLabel.Visibility = Visibility.Hidden;
            }
        }
        public void HideAnte() {
            currPlayerNameLabel.Visibility = Visibility.Hidden;
            anteBtn.Visibility = Visibility.Hidden;
            anteComboBox.Visibility = Visibility.Hidden;

            player2NameLabel.Visibility = Visibility.Hidden;
            player2AnteLabel.Visibility = Visibility.Hidden;

            player3NameLabel.Visibility = Visibility.Hidden;
            player3AnteLabel.Visibility = Visibility.Hidden;

            player4NameLabel.Visibility = Visibility.Hidden;
            player4AnteLabel.Visibility = Visibility.Hidden;

            player5NameLabel.Visibility = Visibility.Hidden;
            player5AnteLabel.Visibility = Visibility.Hidden;

        }

        public void SetupBlackBlackJack() {
            game.StartRound();

            currPlayerTurn = game.GetCurrentPlayer();
            var currentPlayer = game.GetPlayer(currPlayerTurn);

            currPlayerNameLabel.Visibility = Visibility.Visible;
            currPlayerNameLabel.Content = currentPlayer.name;
            currPlayerAnteLabel.Content = "Bet: $" + currentPlayer.currBet;
            currPlayerBankLabel.Content = "Bank: $" + currentPlayer.bank;

            Image[] cardImages = new Image[currentPlayer.cards[0].Count];
            for(int i = 0; i < currentPlayer.cards[0].Count; i++) {
                cardImages[i] = CardImageCreator.VisualizeCard(currentPlayer.cards[0][i], currPlayerPanel);
            }

            if (playerAmo > 1) {
                var p = game.GetPlayer(1);

                player2NameLabel.Visibility = Visibility.Visible;
                player2AnteLabel.Visibility = Visibility.Visible;
                player2BankLabel.Visibility = Visibility.Visible;

                player2NameLabel.Content = p.name;
                player2AnteLabel.Content = "Bet: $" + p.currBet;
                player2BankLabel.Content = "Bank: $" + p.bank;
            } else {
                player2NameLabel.Visibility = Visibility.Hidden;
                player2AnteLabel.Visibility = Visibility.Hidden;
                player2BankLabel.Visibility = Visibility.Hidden;
            }
            if (playerAmo > 2) {
                var p = game.GetPlayer(1);

                player3NameLabel.Visibility = Visibility.Visible;
                player3AnteLabel.Visibility = Visibility.Visible;
                player3BankLabel.Visibility = Visibility.Visible;
                player3NameLabel.Content = p.name;
                player3AnteLabel.Content = "Bet: $" + p.currBet;
                player3BankLabel.Content = "Bank: $" + p.bank;
            } else {
                player3NameLabel.Visibility = Visibility.Hidden;
                player3AnteLabel.Visibility = Visibility.Hidden;
                player3BankLabel.Visibility = Visibility.Hidden;
            }
            if (playerAmo > 3) {
                var p = game.GetPlayer(1);

                player4NameLabel.Visibility = Visibility.Visible;
                player4AnteLabel.Visibility = Visibility.Visible;
                player4BankLabel.Visibility = Visibility.Visible;
                player4NameLabel.Content = p.name;
                player4AnteLabel.Content = "Bet: $" + p.currBet;
                player4BankLabel.Content = "Bank: $" + p.bank;
            } else {
                player4NameLabel.Visibility = Visibility.Hidden;
                player4AnteLabel.Visibility = Visibility.Hidden;
                player4BankLabel.Visibility = Visibility.Hidden;
            }
            if (playerAmo > 4) {
                var p = game.GetPlayer(1);

                player5NameLabel.Visibility = Visibility.Visible;
                player5AnteLabel.Visibility = Visibility.Visible;
                player5BankLabel.Visibility = Visibility.Visible;
                player5NameLabel.Content = p.name;
                player5AnteLabel.Content = "Bet: $" + p.currBet;
                player5BankLabel.Content = "Bank: $" + p.bank;
            } else {
                player5NameLabel.Visibility = Visibility.Hidden;
                player5AnteLabel.Visibility = Visibility.Hidden;
                player5BankLabel.Visibility = Visibility.Hidden;
            }
        }

    }
}
