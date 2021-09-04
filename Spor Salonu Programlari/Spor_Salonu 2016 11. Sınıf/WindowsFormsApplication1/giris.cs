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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Admin" || textBox1.Text == "admin" || textBox1.Text == "ADMİN")
            {
                label9.Text = "";
                if (textBox2.Text == "12345") 
                {
                    label8.Text = "";
                    yoneticipanel git = new yoneticipanel();
                    
                    this.Hide();
                    git.Show();
                    git.Text = "Anasayfa";
                }
                else
                {
                    label8.Text = "";
                    label9.Text = "Şifreniz Yanlış";
                }
            }
            else
            {
                label9.Text = "";
                label8.Text = "Kullanıcı Adı Yanlış";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            unut unut = new unut();
            unut.Show();
        }

        private void giris_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

     
    }
}
