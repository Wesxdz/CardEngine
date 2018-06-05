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
		Image imgP1FaceDown, imgP2FaceDown, imgP1FaceUp, imgP2FaceUp;
		public WarGamePage()
		{
			InitializeComponent();
			Card back = new Card(0, 0) { IsFlipped = true };
			Image card;
			Thickness faceDownThick = new Thickness(0, 100, 0, 0);
			Thickness faceUpThick = new Thickness(0, 0, 0, 100);

			card = CardImageCreator.VisualizeCard(back, grdParent);
			Grid.SetColumn(card, 0);
			card = CardImageCreator.VisualizeCard(back, grdParent);
			Grid.SetColumn(card, 4);

			imgP1FaceDown = CardImageCreator.VisualizeCard(back, grdParent);
			imgP1FaceDown.Margin = faceDownThick;
			imgP1FaceDown.Visibility = Visibility.Hidden;
			Grid.SetColumn(imgP1FaceDown, 1);
			imgP2FaceDown = CardImageCreator.VisualizeCard(back, grdParent);
			imgP2FaceDown.Margin = faceDownThick;
			imgP2FaceDown.Visibility = Visibility.Hidden;
			Grid.SetColumn(imgP2FaceDown, 3);

			imgP1FaceUp = CardImageCreator.VisualizeCard(back, grdParent);
			imgP1FaceUp.Margin = faceUpThick;
			imgP1FaceUp.Visibility = Visibility.Hidden;
			Grid.SetColumn(imgP1FaceUp, 1);
			imgP2FaceUp = CardImageCreator.VisualizeCard(back, grdParent);
			imgP2FaceUp.Margin = faceUpThick;
			imgP2FaceUp.Visibility = Visibility.Hidden;
			Grid.SetColumn(imgP2FaceUp, 3);

			UpdateUI();
		}

		public void UpdateUI()
		{
			if (game.p1.isComputer) btnP1Play.IsEnabled = false;
			if (game.p2.isComputer) btnP2Play.IsEnabled = false;
			lblAnnounce.Text = game.GetAnnounceText();
			if (lblAnnounce.Text.Length > 5 && lblAnnounce.Text.Substring(lblAnnounce.Text.Length - 5).Equals("game!"))
			{
				btnP1Play.IsEnabled = false;
				btnP2Play.IsEnabled = false;
			}
			lblP1CardCount.Content = game.p1.cards.Count;
			lblP2CardCount.Content = game.p2.cards.Count;
			if (game.p1.faceUp.Count > 0)
			{
				imgP1FaceUp.Visibility = Visibility.Visible;
				imgP1FaceUp.Source = CardImageCreator.Convert(game.p1.faceUp.Last());
				lblP1FaceUpCount.Visibility = Visibility.Visible;
				lblP1FaceUpCount.Content = game.p1.faceUp.Count;
			}
			else
			{
				imgP1FaceUp.Visibility = Visibility.Hidden;
				lblP1FaceUpCount.Visibility = Visibility.Hidden;
			}
			if (game.p2.faceUp.Count > 0)
			{
				imgP2FaceUp.Visibility = Visibility.Visible;
				imgP2FaceUp.Source = CardImageCreator.Convert(game.p2.faceUp.Last());
				lblP2FaceUpCount.Visibility = Visibility.Visible;
				lblP2FaceUpCount.Content = game.p2.faceUp.Count;
			}
			else
			{
				imgP2FaceUp.Visibility = Visibility.Hidden;
				lblP2FaceUpCount.Visibility = Visibility.Hidden;
			}
			if (game.p1.faceDown.Count > 0)
			{
				imgP1FaceDown.Visibility = Visibility.Visible;
				lblP1FaceDownCount.Visibility = Visibility.Visible;
				lblP1FaceDownCount.Content = game.p1.faceDown.Count;
			}
			else
			{
				imgP1FaceDown.Visibility = Visibility.Hidden;
				lblP1FaceDownCount.Visibility = Visibility.Hidden;
			}
			if (game.p2.faceDown.Count > 0)
			{
				imgP2FaceDown.Visibility = Visibility.Visible;
				lblP2FaceDownCount.Visibility = Visibility.Visible;
				lblP2FaceDownCount.Content = game.p2.faceDown.Count;
			}
			else
			{
				imgP2FaceDown.Visibility = Visibility.Hidden;
				lblP2FaceDownCount.Visibility = Visibility.Hidden;
			}
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
			sfd.Filter = "War Save|*.warsave|All Files|*.*";
			if (!sfd.ShowDialog().Value) return;
			game.Save(sfd.FileName);
		}

		private void btnQuit_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new WarPage());
		}
	}
}
