using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CardApp
{
    public class CardToImageConverter
    {
        public ImageSource Convert(Card card)
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

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageString, UriKind.Relative);
            bitmap.EndInit();
            
            return bitmap;
        }
    }
}
