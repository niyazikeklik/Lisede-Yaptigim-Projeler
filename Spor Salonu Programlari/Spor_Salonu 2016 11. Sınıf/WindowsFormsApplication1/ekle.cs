using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication1
{
    public partial class ekle : Form
    {
        public ekle()
        {
            InitializeComponent();
        }
       

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sporsalonu.accdb");
        OleDbDataAdapter da;
        DataSet ds;
        string komut = "";
        private void button1_Click(object sender, EventArgs e)
        {
            komut = "insert into uyeler(adı,soyadı,numara,tc,yas,adres,boy,kilo,gobek,kol,bacak,boyun) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "'," + textBox5.Text + ",'" + textBox6.Text + "'," + textBox12.Text + "," + textBox11.Text + "," + textBox10.Text + "," + textBox9.Text + "," + textBox8.Text + "," + textBox7.Text + ")";
            baglanti.Open();
            OleDbCommand isle = new OleDbCommand(komut, baglanti);
            isle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yeni Üye Başarıyla Kayıt Edilmiştir");
            
            if (MessageBox.Show("Başka Üye Kayıt Etmek İstermisiniz ?", "Nasıl Devam Etmek İstiyorsunuz..", MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                this.Close();
                ekle git = new ekle();
                git.Show();
            }
            else
            {
                this.Close();
                yoneticipanel git1 = new yoneticipanel();
                git1.Show();
            }

            
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            yoneticipanel ansayfa = new yoneticipanel();
            this.Close();
            ansayfa.Show();
        }

        private void ekle_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
