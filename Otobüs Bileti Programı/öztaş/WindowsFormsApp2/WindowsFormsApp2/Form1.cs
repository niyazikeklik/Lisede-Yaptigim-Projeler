using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kullanıcıgiris yeni =new  kullanıcıgiris();
            yeni.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayıt yeni = new kayıt();
            yeni.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admingiris yeni = new Admingiris();
            yeni.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
