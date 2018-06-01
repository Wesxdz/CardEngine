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

        public Image VisualizeCard(Card card, Panel panel)
        {
            Image cardImage = new Image();
            cardImage.Width = 66;
            cardImage.Height = 100;
            cardImage.DataContext = card;
            Binding imageBinding = new Binding();
            imageBinding.Source = cardImage.DataContext;
            imageBinding.Converter = new CardToImageConverter();
            //imageBinding.Mode = BindingMode.OneWay;
            cardImage.SetBinding(Image.SourceProperty, imageBinding);
            cardImage.MouseDown += CardClicked;
            panel.Children.Add(cardImage);
            return cardImage;
        }

        private void CardClicked(object sender, MouseButtonEventArgs e)
        {
            Card card = (((Image)sender).DataContext as Card);
            if (OnCardSelect != null)
            {
                OnCardSelect((Image)sender, card);
            }
        }
    }
}
