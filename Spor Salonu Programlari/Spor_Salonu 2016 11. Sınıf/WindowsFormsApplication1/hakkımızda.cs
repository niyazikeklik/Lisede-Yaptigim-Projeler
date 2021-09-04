using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class hakkımızda : Form
    {
        public hakkımızda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yoneticipanel ana = new yoneticipanel();
            this.Close();
            ana.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ulas ulas1 = new ulas();
            this.Close();
            ulas1.Show();
            
        }
    }
}
