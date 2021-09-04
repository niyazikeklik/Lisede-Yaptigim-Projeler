using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApp2
{
    public partial class adminpanel : Form
    {
        public adminpanel()
        {
            InitializeComponent();
        }
        void getir(string komutt)
        {
            con.Open();
            string kayit = komutt;
            OleDbCommand komut = new OleDbCommand(kayit, con);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        private void adminpanel_Load(object sender, EventArgs e)
        {
            getir("SELECT * from Tablo2");

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "TC No Giriniz:";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "İsim Giriniz:";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Telefon No Giriniz:";
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Kullanıcıkayıt.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                getir("Select * from tablo2 where tc like '" + textBox1.Text + "%'");

            }
            else if (radioButton2.Checked)
            {
                getir("Select * from tablo2 where ad like '" + textBox1.Text + "%'");

            }


        }

        private void sadeceKadınlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getir("Select * from tablo2 where cinsiyet like  '" + "Kadın" + "%'");
        }

        private void sadeceErkeklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getir("Select * from tablo2 where cinsiyet like  '" + "Erkek" + "%'");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM tablo2 WHERE kimlik=@uyeno", con);
                    cmd.Parameters.AddWithValue("@uyeno", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    getir("SELECT * from tablo2");
                    MessageBox.Show("Seçili satır silindi.", "Başarılı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (OleDbException)
                {
                    con.Close();
                    MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void adminpanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void kapatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1 yeni = new Form1();
            yeni.Show();
            Hide();
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
