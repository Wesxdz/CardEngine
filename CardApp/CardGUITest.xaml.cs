﻿using System;
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
        public CardGUITest()
        {
            InitializeComponent();

            


            Card CardObject = new Card(1, 3);
            CardToImageConverter CardToImage = new CardToImageConverter();
            Binding binding = new Binding
            {
                Source = CardObject,
                Converter = CardToImage
            };
            image.SetBinding(Image.SourceProperty, binding);

        }

    }
}
