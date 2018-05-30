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
    public partial class Persistence : UserControl
    {
        public Persistence()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
        }
    }
}
