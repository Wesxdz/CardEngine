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
	/// Interaction logic for WarGamePage.xaml
	/// </summary>
	public partial class WarGamePage : Page
	{
		public WarGame game { get; private set; } = new WarGame();
		public WarGamePage()
		{
			InitializeComponent();
		}

		public void UpdateUI()
		{
			lstP1Cards.ItemsSource = game.p1.cards;
			lstP1FaceDown.ItemsSource = game.p1.faceDown;
			lstP1FaceUp.ItemsSource = game.p1.faceUp;
			lstP2Cards.ItemsSource = game.p2.cards;
			lstP2FaceDown.ItemsSource = game.p2.faceDown;
			lstP2FaceUp.ItemsSource = game.p2.faceUp;
		}

		private void btnP1Play_Click(object sender, RoutedEventArgs e)
		{
			game.PlayCard(1);
			UpdateUI();
		}

		private void btnP2Play_Click(object sender, RoutedEventArgs e)
		{
			game.PlayCard(2);
			UpdateUI();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if (!sfd.ShowDialog().Value) return;
			game.Save(sfd.FileName);
		}

		private void btnQuit_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new WarPage());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			UpdateUI();
		}
	}
}
