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

        Dictionary<int, Panel> playerPanels;
        //Panel[] playerPanels;
        public BlackJackPage()
        {
            InitializeComponent();

            anteComboBox.Items.Add(1);
            anteComboBox.Items.Add(5);
            anteComboBox.Items.Add(10);
            anteComboBox.SelectedIndex = 0;

            playerPanels = new Dictionary<int, Panel>();
            playerPanels[0] = currPlayerPanel;
            if (playerAmo > 1) {
                playerPanels[1] = player2CardPanel;
            }
            if (playerAmo > 2) {
                playerPanels[2] = player3CardPanel;
            }
            if (playerAmo > 3) {
                playerPanels[3] = player4CardPanel;
            }
            if (playerAmo > 4) {
                playerPanels[4] = player5CardPanel;
            }
        }

        public void Initialize() {
            game = new BlackjackGame();
            game.InitializePlayers(playerAmo, playerNames, null);
            game.Start();

            // Display Cards
            // Buttons
            // Hit Btn

            // Center Card / Deck
            {
                Card c = new Card(1, 0);
                c.IsFlipped = true;
                var image = CardImageCreator.VisualizeCard(c, centerPanel);
                Grid.SetColumn(image, 0);
                Grid.SetRow(image, 0);
                Grid.SetColumnSpan(image, 2);
                Grid.SetRowSpan(image, 2);
            }

            HideBlackJack();
            SetupAnte();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e) {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e) {
            
            //game.Save()
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e) {

        }

        private void hitBtn_Click(object sender, RoutedEventArgs e) {
            TakeTurn(true);
        }
        private void passBtn_Click(object sender, RoutedEventArgs e) {
            TakeTurn(false);
        }
        private void TakeTurn(bool hitting) {
            game.TakeTurn(currPlayerTurn, new bool[1] { hitting });
            if (game.gameOver) {
                backBtn_Click(null, null);
                return;
            }

            if (game.roundOver) {
                playerAmo = BlackjackGame.PLAYER_AMO;

                DefaultPlayerContext();
                DefaultCards();

                MessageBox.Show("Round Ended");

                HideBlackJack();
                SetupAnte();
                return;
            }

            int previousPlayerTurn = currPlayerTurn;
            currPlayerTurn = game.GetCurrentPlayer();

            var previousPlayer = game.GetPlayer(previousPlayerTurn);
            var currPlayer = game.GetPlayer(currPlayerTurn);

            //Find curr Player Panel
            // Set us to that Panel
            playerPanels[currPlayerTurn].DataContext = null;
            playerPanels[currPlayerTurn].DataContext = previousPlayer;
            BindCards(GetCardPanel(playerPanels[currPlayerTurn]), previousPlayer.cards[0]);

            playerPanels[previousPlayerTurn].DataContext = null;
            playerPanels[previousPlayerTurn].DataContext = currPlayer;
            BindCards(GetCardPanel(playerPanels[previousPlayerTurn]), currPlayer.cards[0]);

            BindCards(houseCardPanel, game.houseCards);

            var temp = playerPanels[previousPlayerTurn];
            playerPanels[previousPlayerTurn] = playerPanels[currPlayerTurn];
            playerPanels[currPlayerTurn] = temp;

        }

        private void anteBtn_Click(object sender, RoutedEventArgs e) {
            game.TakeAnte(currPlayerTurn, (int)anteComboBox.SelectedItem);

            int previousPlayerTurn = currPlayerTurn;
            currPlayerTurn = game.GetCurrentPlayer();

            var previousPlayer = game.GetPlayer(previousPlayerTurn);

            var currPlayer = game.GetPlayer(currPlayerTurn);

            //Find curr Player Panel
            // Set us to that Panel
            playerPanels[currPlayerTurn].DataContext = null;
            playerPanels[currPlayerTurn].DataContext = previousPlayer;

            playerPanels[previousPlayerTurn].DataContext = null;
            playerPanels[previousPlayerTurn].DataContext = currPlayer;

            var temp = playerPanels[previousPlayerTurn];
            playerPanels[previousPlayerTurn] = playerPanels[currPlayerTurn];
            playerPanels[currPlayerTurn] = temp;

            if (previousPlayerTurn == playerAmo - 1) {
                HideAnte();
                SetupBlackBlackJack();
            }
        }

        public void SetupAnte() {
            currPlayerTurn = game.GetCurrentPlayer();

            InitPlayerInfo();

            RemoveCards();

            antePanel.Visibility = Visibility.Visible;
        }
        public void HideAnte() {
            antePanel.Visibility = Visibility.Hidden;
        }

        public void SetupBlackBlackJack() {
            game.StartRound();

            currPlayerTurn = game.GetCurrentPlayer();

            InitPlayerInfo();

            DefaultCards();

            gamePanel.Visibility = Visibility.Visible;
            player2isHittingLabel.Visibility = Visibility.Visible;
            player3isHittingLabel.Visibility = Visibility.Visible;
            player4isHittingLabel.Visibility = Visibility.Visible;
            player5isHittingLabel.Visibility = Visibility.Visible;
            currPlayerSumLabel.Visibility = Visibility.Visible;
            currPlayerIsHittingLabel.Visibility = Visibility.Visible;
        }
        public void HideBlackJack() {
            gamePanel.Visibility = Visibility.Hidden;

            player2isHittingLabel.Visibility = Visibility.Hidden;
            player3isHittingLabel.Visibility = Visibility.Hidden;
            player4isHittingLabel.Visibility = Visibility.Hidden;
            player5isHittingLabel.Visibility = Visibility.Hidden;
            currPlayerSumLabel.Visibility = Visibility.Hidden;
            currPlayerIsHittingLabel.Visibility = Visibility.Hidden;      
        }

        // Initializes the Data Context and Bindings...
        public void InitPlayerInfo() {
            DefaultPlayerContext();

            currPlayerPanel.Visibility = Visibility.Visible;
            if (playerAmo > 1) {
                player2Panel.Visibility = Visibility.Visible;
            }
            if (playerAmo > 2) {
                player3Panel.Visibility = Visibility.Visible;
            }
            if (playerAmo > 3) {
                player4Panel.Visibility = Visibility.Visible;
            }
            if (playerAmo > 4) {
                player5Panel.Visibility = Visibility.Visible;
            }
        }

        public void DefaultPlayerContext() {
            var currentPlayer = game.GetPlayer(0);
            currPlayerPanel.DataContext = null;
            currPlayerPanel.DataContext = currentPlayer;
            playerPanels[0] = currPlayerPanel;

            if (playerAmo > 1) {
                var p = game.GetPlayer(1);

                player2Panel.DataContext = null;
                player2Panel.DataContext = p;
                playerPanels[1] = player2Panel;
            } else {
                player2Panel.DataContext = null;
            }
            if (playerAmo > 2) {
                var p = game.GetPlayer(2);

                player3Panel.DataContext = null;
                player3Panel.DataContext = p;
                playerPanels[2] = player3Panel;
            } else {
                player3Panel.DataContext = null;
            }
            if (playerAmo > 3) {
                var p = game.GetPlayer(3);

                player4Panel.DataContext = null;
                player4Panel.DataContext = p;
                playerPanels[3] = player4Panel;
            } else {
                player4Panel.DataContext = null;
            }
            if (playerAmo > 4) {
                var p = game.GetPlayer(4);

                player5Panel.DataContext = null;
                player5Panel.DataContext = p;
                playerPanels[4] = player5Panel;
            } else {
                player5Panel.DataContext = null;
            }
        }
        public void DefaultCards() {
            var currentPlayer = game.GetPlayer(0);

            BindCards(currPlayerCardPanel, currentPlayer.cards[0]);
            BindCards(houseCardPanel, game.houseCards);

            if (playerAmo > 1) {
                var p = game.GetPlayer(1);
                BindCards(player2CardPanel, p.cards[0]);
            } else {
                player2CardPanel.Children.Clear();
            }
            if (playerAmo > 2) {
                var p = game.GetPlayer(2);
                BindCards(player3CardPanel, p.cards[0]);
            } else {
                player3CardPanel.Children.Clear();
            }
            if (playerAmo > 3) {
                var p = game.GetPlayer(3);
                BindCards(player4CardPanel, p.cards[0]);
            } else {
                player4CardPanel.Children.Clear();
            }
            if (playerAmo > 4) {
                var p = game.GetPlayer(4);
                BindCards(player5CardPanel, p.cards[0]);
            } else {
                player5CardPanel.Children.Clear();
            }
        }
        public void RemoveCards() {
            currPlayerCardPanel.Children.Clear();
            houseCardPanel.Children.Clear();

            if (playerAmo > 1) {
                player2CardPanel.Children.Clear();
            }
            if (playerAmo > 2) {
                player3CardPanel.Children.Clear();
            }
            if (playerAmo > 3) {
                player4CardPanel.Children.Clear();
            }
            if (playerAmo > 4) {
                player5CardPanel.Children.Clear();
            }
        }

        public void BindCards(Panel parent, List<Card> cards) {
            parent.Children.Clear();

            Image[] cardImages = new Image[cards.Count];
            for (int i = 0; i < cards.Count; i++) {
                Card c = cards[i];
                cardImages[i] = CardImageCreator.VisualizeCard(c, parent);
            }
        }

        public Panel GetCardPanel(Panel playerPanel) {
            if(playerPanel == player2Panel) {
                return player2CardPanel;
            } else if (playerPanel == player3Panel) {
                return player3CardPanel;
            } else if (playerPanel == player4Panel) {
                return player4CardPanel;
            } else if (playerPanel == player5Panel) {
                return player5CardPanel;
            } else if (playerPanel == currPlayerPanel) {
                return currPlayerCardPanel;
            }else {
                return null;
            }
        }

    }
}
