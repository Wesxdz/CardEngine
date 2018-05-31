using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CardApp
{
    public class CardToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageString = @"Images\Cards\";
            if (targetType != typeof(ImageSource))
            {
                throw new Exception("Target type needs to be an image");
            }

            Card c = (Card)value;

            if (c.IsFlipped)
            {
                imageString += "gray_back"; //Card Back
            }
            else
            {
                switch (c.Rank)
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
                        imageString += c.Rank;
                        break;
                }

                switch (c.Suit)
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

            return imageString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
