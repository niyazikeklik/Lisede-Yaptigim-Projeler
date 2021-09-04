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
    public partial class Admingiris : Form
    {
        public Admingiris()
        {
            InitializeComponent();
        }

        private void btnkullanıcıgiriş_Click(object sender, EventArgs e)
        {
            if (txtkullanıcı.Text=="admin")
            {
                if (txtsifre.Text=="12345")
                {
                    adminpanel yeni = new adminpanel();
                    yeni.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Şifre yanış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı yanış.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void txtsifre_TextChanged(object sender, EventArgs e)
        {

            if (txtkullanıcı.Text == "admin")
            {
                if (txtsifre.Text == "12345")
                {
                    adminpanel yeni = new adminpanel();
                    yeni.Show();
                    Hide();
                }
             
        }
        }

        private void txtkullanıcı_TextChanged(object sender, EventArgs e)
        {
            if (txtkullanıcı.Text == "admin")
            {
                txtsifre.Focus();
            }
    }

        private void btnev1_Click(object sender, EventArgs e)
        {
            Form1 yeni = new Form1();
            yeni.Show();
            Hide();
        }
    }
}