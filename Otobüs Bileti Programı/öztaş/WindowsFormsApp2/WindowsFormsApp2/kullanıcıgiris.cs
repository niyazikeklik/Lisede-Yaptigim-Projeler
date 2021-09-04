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
    public partial class kullanıcıgiris : Form
    {
        public kullanıcıgiris()
        {
            InitializeComponent();
        }
        public static string tckimliknumarası = "";
        OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Kullanıcıkayıt.mdb");
        private void btnkullanıcıgiriş_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                OleDbCommand komut = new OleDbCommand("select*from Tablo1 where tc=@tc  and sifre='" + txtsifre.Text + "'", baglantı);
                    komut.Parameters.AddWithValue("@tc",txtkullanıcı.Text);
                OleDbDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    bilet yeni = new bilet();
                    tckimliknumarası = oku["tc"].ToString();
                    yeni.textBox4.Text = oku["tc"].ToString();
                    yeni.textBox1.Text = oku["ad"].ToString();
                    yeni.textBox2.Text = oku["soyad"].ToString();
                    yeni.textBox6.Text = oku["tel"].ToString();
                    yeni.textBox5.Text = oku["mail"].ToString();
                    yeni.Show();
                    this.Hide();
                    baglantı.Close();
                   
                  
                }
                else
                {
                    baglantı.Close();
                    MessageBox.Show("Giriş Başarısız");
                }
            }
            catch (Exception ex)
            {
                baglantı.Close();
                MessageBox.Show("Hata :" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
