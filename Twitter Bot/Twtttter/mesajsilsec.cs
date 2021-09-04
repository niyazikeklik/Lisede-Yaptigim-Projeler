using System;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class mesajsilsec : Form
    {
        public mesajsilsec()
        {
            InitializeComponent();
        }

        private Anaekran anaform = (Anaekran)Application.OpenForms["Anaekran"];

        private void modernTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (modernTextBox3.Text[0] != '@')
                {
                    modernTextBox3.Text = "@" + modernTextBox3.Text;
                }
                anaform.kontroledildi.Add(modernTextBox3.Text);

                listBox1.Items.Add((listBox1.Items.Count) + ". " + modernTextBox3.Text);
                modernTextBox3.Text = "";
            }
        }

        private void modernTextBox3_Click(object sender, EventArgs e)
        {
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            anaform.basilanbuton = 1;
            Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            anaform.basilanbuton = 2;
            Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            anaform.basilanbuton = -1;
            Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            anaform.basilanbuton = 3;
            Close();
        }
    }
}