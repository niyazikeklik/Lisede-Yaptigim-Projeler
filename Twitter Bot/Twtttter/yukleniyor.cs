using OpenQA.Selenium;
using System;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class yukleniyor : Form
    {
        public yukleniyor()
        {
            InitializeComponent();
        }
        public Anaekran anaform;
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            anaform.driver.Navigate().Refresh();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            anaform.SSal();
        }
    }
}