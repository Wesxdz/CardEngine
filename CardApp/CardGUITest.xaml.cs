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
    /// <summary>
    /// Interaction logic for CardGUITest.xaml
    /// </summary>
    public partial class CardGUITest : Page
    {

        public CardGUITest()
        {
           InitializeComponent();

            Card c = new Card(1, 3);

            ObservableCollection<Card> cards = new ObservableCollection<Card>
            {
                new Card(1,3)
            };

            cardComboBox.ItemSource = cards;
        }

    }
}
