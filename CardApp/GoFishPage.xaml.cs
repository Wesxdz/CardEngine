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
    /// Interaction logic for GoFishPage.xaml
    /// </summary>
    public partial class GoFishPage : Page
    {
        GoFishGame instance = new GoFishGame();
        public GoFishPage()
        {
            InitializeComponent();
            string[] names = new string[] { "Fred", "George" };
            bool[] humans = new bool[] { true, true };
            instance.page = this;
            instance.InitializePlayers(2, names, humans);
            instance.Start();
        }

        private void btnGFBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);

            NavigationService.Navigate(page);
        }
    }
}
