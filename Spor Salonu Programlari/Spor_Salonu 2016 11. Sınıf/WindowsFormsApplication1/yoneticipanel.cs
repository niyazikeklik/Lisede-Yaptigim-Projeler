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
    public partial class yoneticipanel : Form
    {
        public yoneticipanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uyeler git = new uyeler();
            this.Close();
            git.Show();
            git.Text = "Tüm Üyeler";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ekle git2 = new ekle();
            this.Close();
            git2.Show();
            git2.Text = "Yeni Üye Kayıt Formu";
        }

        private void yoneticipanel_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Application.Exit();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hakkımızda hak = new hakkımızda();
            this.Close();
            hak.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ulas ulas = new ulas();
            this.Hide();
            ulas.Show();
        }
    }
}
