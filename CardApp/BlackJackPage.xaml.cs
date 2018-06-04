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

        public BlackJackPage()
        {
            InitializeComponent();

            game = new BlackjackGame();
            game.InitializePlayers(playerAmo, playerNames, null);
            game.Start();

            // Do antes
            // Start Round

            // Display Cards
            // Buttons
            // Hit Btn

            var player = game.GetCurrentPlayer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        public void SetupAnte() {

        }

        public void SetupBlackBlackJack() {

        }
    }
}
