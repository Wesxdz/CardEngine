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
    class CardToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageString = "";
            if (targetType != typeof(ImageSource))
            {
                throw new Exception("Target type needs to be an image");
            }

            Card c = (Card)value;

            if (c.FaceUp)
            {
                imageString = @""; //Card Back
            }
            else
            {
                switch (c.Rank)
                {
                    case 1: //Ace
                        break;
                    case 2: //2
                        break;
                    case 3: //3
                        break;
                    case 4: //4
                        break;
                    case 5: //5
                        break;
                    case 6: //6
                        break;
                    case 7: //7
                        break;
                    case 8: //8
                        break;
                    case 9: //9
                        break;
                    case 10: //10
                        break;
                    case 11: //Jack
                        break;
                    case 12: //Queen
                        break;
                    case 13: //King
                        break;
                }

                switch (c.Suit)
                {
                    case 0: //Heart
                        break;
                    case 1: //Diamond
                        break;
                    case 2: //Club
                        break;
                    case 3: //Spade
                        break;
                }
            }

            return imageString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
