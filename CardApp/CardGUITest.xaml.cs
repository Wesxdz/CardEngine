//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace CardApp
{
    /// <summary>
    /// Interaction logic for CardGUITest.xaml
    /// </summary>
    public partial class CardGUITest : Page
    {
        private Canvas canvas;
        private Rectangle card;
        private Button move, flip;
        int x = 0, y = 0;
        private bool faceUp = false;
        public CardGUITest()
        {
            //InitializeComponent();
           canvas = new Canvas();
           canvas.Background = new SolidColorBrush(Colors.LightCyan);

           card = new Rectangle();
           card.Width = 50;
           card.Height = 75;
           card.Fill = new SolidColorBrush(Colors.Red);

           Canvas.SetLeft(card, x);
           Canvas.SetTop(card, y);

           canvas.Children.Add(card);


           move = new Button();
           move.Content = "Move";

           move.Click += Button_Click;

           Canvas.SetLeft(move, 600);
           Canvas.SetTop(move, 350);
            move.Click += Move_btn_Click;

           canvas.Children.Add(move);
           Content = canvas;
       }


            canvas.Children.Add(move);



            flip = new Button();
            flip.Content = "Flip";

            flip.Click += Flip_btn_Click;

            Canvas.SetLeft(flip, 630);
            Canvas.SetTop(flip, 350);

            canvas.Children.Add(flip);
            Content = canvas;
        }

        private void Move_btn_Click(object sender, RoutedEventArgs e)
        {
            x += 5;
            y += 5;
            Canvas.SetLeft(card, x);
            Canvas.SetTop(card, y);

        }

        private void Flip_btn_Click(object sender, RoutedEventArgs e)
        {
            if (faceUp)
            {
                card.Fill = new SolidColorBrush(Colors.Red);
                faceUp = false;
            }
            else
            {
                card.Fill = new SolidColorBrush(Colors.Blue);
                faceUp = true;
            }
        }
    }
}
