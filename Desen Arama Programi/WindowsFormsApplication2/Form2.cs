using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
          
            if (File.Exists(Application.StartupPath + "\\goster.txt") == true) // dizindeki dosya var mı ?
            {           
                    File.Delete(Application.StartupPath + "\\goster.txt");
            }
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           listBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#1c1c1c");
        }
    }
}
