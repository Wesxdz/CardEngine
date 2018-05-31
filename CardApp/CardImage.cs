using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardApp
{
    public partial class CardImage : UserControl
    {
        delegate void Selected(Card card);
        public Card Card { get; set; }
        public CardImage()
        {
            InitializeComponent();
        }
    }
}
