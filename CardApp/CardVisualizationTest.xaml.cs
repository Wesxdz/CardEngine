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
    /// Interaction logic for CardVisualizationTest.xaml
    /// </summary>
    public partial class CardVisualizationTest : Window
    {
        public CardVisualizationTest()
        {
            InitializeComponent();
            CardImageCreator creator = new CardImageCreator();
            Card card = new Card(1, 0);
            Image cardImage = creator.VisualizeCard(card, Table);
            cardImage.Margin = new Thickness(400, 0, 0, 0);
        }
    }
}
