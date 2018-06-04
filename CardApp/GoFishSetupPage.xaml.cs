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
    }
}
