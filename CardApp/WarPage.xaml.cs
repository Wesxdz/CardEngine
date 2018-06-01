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
using Microsoft.Win32;

namespace CardApp
{
    /// <summary>
    /// Interaction logic for WarPage.xaml
    /// </summary>
    public partial class WarPage : Page
    {
        public WarPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("HomePage.xaml", UriKind.Relative);
            NavigationService.Navigate(page);
        }

		private void btnStart_Click(object sender, RoutedEventArgs e)
		{
			WarGamePage uri = new WarGamePage();
			string[] names =
			{
				txtP1Name.Text,
				txtP2Name.Text
			};
			bool[] comps =
			{
				cbxP1Computer.IsChecked.Value,
				cbxP2Computer.IsChecked.Value
			};
			uri.game.InitializePlayers(2, names, comps);
			uri.game.Start();
			uri.UpdateUI();
			NavigationService.Navigate(uri);
		}

		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (!ofd.ShowDialog().Value) return;
			WarGamePage page = new WarGamePage();
			page.game.Load(ofd.FileName);
			NavigationService.Navigate(page);
		}
	}
}
