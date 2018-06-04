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
using System.Windows.Shapes;

namespace CardApp
{
    public class CardImageCreator
    {
        public delegate void ProcessCardClick(Image image, Card card);
        public ProcessCardClick OnCardSelect;

        static public Image VisualizeCard(Card card, Panel panel)
        {
            Image cardImage = new Image();
            card.image = cardImage;
            cardImage.Width = 66;
            cardImage.Height = 100;
            cardImage.Source = Convert(card);
            cardImage.DataContext = card;
            panel.Children.Add(cardImage);
            return cardImage;
        }

        static public ImageSource Convert(Card card)
        {
            string imageString = @"Images\Cards\";
            if (card.IsFlipped)
            {
                imageString += "gray_back"; //Card Back
            }
            else
            {
                switch (card.Rank)
                {
                    case 1: //Ace
                        imageString += "A";
                        break;
                    case 11: //Jack
                        imageString += "J";
                        break;
                    case 12: //Queen
                        imageString += "Q";
                        break;
                    case 13: //King
                        imageString += "K";
                        break;
                    default: //Numbers
                        imageString += card.Rank;
                        break;
                }

                switch (card.Suit)
                {
                    case 0: //Heart
                        imageString += "H";
                        break;
                    case 1: //Diamond
                        imageString += "D";
                        break;
                    case 2: //Club
                        imageString += "C";
                        break;
                    case 3: //Spade
                        imageString += "S";
                        break;
                }

            }
            imageString += ".png";
            return new BitmapImage(new Uri(imageString, UriKind.Relative));
        }
    }
}
