using System;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class buyut : Form
    {
        public buyut()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Width += 200; this.Height += 200;
        }
    }
}