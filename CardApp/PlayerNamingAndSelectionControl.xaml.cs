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
    /// Interaction logic for PlayerNamingAndSelectionControl.xaml
    /// </summary>
    public partial class PlayerNamingAndSelectionControl : UserControl
    {
        public NavigationService Service { get; set; }
        public PlayerNamingAndSelectionControl()
        {
            InitializeComponent();
            txtbxPlayer1Name.IsEnabled = false;
            txtbxPlayer2Name.IsEnabled = false;
            txtbxPlayer3Name.IsEnabled = false;
            txtbxPlayer4Name.IsEnabled = false;
            txtbxPlayer5Name.IsEnabled = false;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);
            Service.Navigate(page);

        }

        private void CmbbxNumOfPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CmbbxNumOfPlayers.SelectedIndex)
            {
                case 0:
                    txtbxPlayer1Name.IsEnabled = true;
                    txtbxPlayer2Name.IsEnabled = false;
                    txtbxPlayer3Name.IsEnabled = false;
                    txtbxPlayer4Name.IsEnabled = false;
                    txtbxPlayer5Name.IsEnabled = false;
                    break;
                case 1:
                    txtbxPlayer1Name.IsEnabled = true;
                    txtbxPlayer2Name.IsEnabled = true;
                    txtbxPlayer3Name.IsEnabled = false;
                    txtbxPlayer4Name.IsEnabled = false;
                    txtbxPlayer5Name.IsEnabled = false;
                    break;
                case 2:

                    txtbxPlayer1Name.IsEnabled = true;
                    txtbxPlayer2Name.IsEnabled = true;
                    txtbxPlayer3Name.IsEnabled = true;
                    txtbxPlayer4Name.IsEnabled = false;
                    txtbxPlayer5Name.IsEnabled = false;
                    break;
                case 3:
                    txtbxPlayer1Name.IsEnabled = true;
                    txtbxPlayer2Name.IsEnabled = true;
                    txtbxPlayer3Name.IsEnabled = true;
                    txtbxPlayer4Name.IsEnabled = true; 
                    txtbxPlayer5Name.IsEnabled = false;
                    break;
                case 4:
                    txtbxPlayer1Name.IsEnabled = true;
                    txtbxPlayer2Name.IsEnabled = true;
                    txtbxPlayer3Name.IsEnabled = true;
                    txtbxPlayer4Name.IsEnabled = true;
                    txtbxPlayer5Name.IsEnabled = true;
                    break;
            }

        }
    }
}
