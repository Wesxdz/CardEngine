using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for CardGUITest.xaml
    /// </summary>
    public partial class CardGUITest : Window
    {
        Card CardObject;
        CardToImageConverter CardToImage;
        public CardGUITest()
        {
            InitializeComponent();
            

            CardObject = new Card(1, 3); 
            CardToImage = new CardToImageConverter();

            image.Source = CardToImage.Convert(CardObject);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CardObject.IsFlipped = !CardObject.IsFlipped;
            image.Source = CardToImage.Convert(CardObject);
        }
    }
}
