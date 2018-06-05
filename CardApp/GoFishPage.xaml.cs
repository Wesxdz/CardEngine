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
using System.Windows.Forms;

namespace CardApp
{
    /// <summary>
    /// Interaction logic for GoFishPage.xaml
    /// </summary>
    public partial class GoFishPage : Page
    {
        public GoFishGame instance = new GoFishGame();
        public GoFishPage(string[] names, bool[] isHumans)
        {
            InitializeComponent();
            instance.page = this;
            instance.InitializePlayers(names.Length, names, isHumans);
            instance.Start();
        }

        private void btnGFBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savePrompt = new SaveFileDialog();
            savePrompt.ShowDialog();
            if (savePrompt.FileName != "")
            {
                instance.Save(savePrompt.FileName);
            }
        }

        private void Deck_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
