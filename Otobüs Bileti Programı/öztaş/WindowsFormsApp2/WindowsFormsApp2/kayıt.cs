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
    public partial class kayıt : Form
    {
        public kayıt()
        {
            InitializeComponent();
        }

        private void kayıt_Load(object sender, EventArgs e)
        {

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Kullanıcıkayıt.mdb");
        private void btnkayıtol_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtad.Text!=""&& txtsoyad.Text != "" && txtemail.Text!="" && txttc.Text.Length==11 && txtsifre.Text!="" && txttel.Text!="")
                {

                if (txtsifre.Text==txtsifret.Text)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("insert into Tablo1(ad,soyad,tc,tel,sifre,mail) values('" + txtad.Text + "','" + txtsoyad.Text + "','" + txttc.Text + "','" + txttel.Text + "','" + txtsifre.Text + "','" + txtemail.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kaydınız Eklenmiştir","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    baglanti.Close();
                    Form1 ans = new Form1();
                    ans.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Şifreler uyuşmuyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                }
                else
                {
                    MessageBox.Show("Lütfen boş alanları doldurunuz!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            catch (Exception ex)
            {
                baglanti.Close();
                MessageBox.Show("Bir hata meydana geldi tc kimlik numarası kullanılıyor olabilir., kaydedilemedi. "+Environment.NewLine+"Hata mesajı: "+Environment.NewLine+ ex.Message,"HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        
        }

        private void btnev_Click(object sender, EventArgs e)
        {
            Form1 yeni = new Form1();
            yeni.Show();
            Hide();
        }

        private void txttc_TextChanged(object sender, EventArgs e)
        {
            int islem = (11 - txttc.Text.Length);
            label1.Text = islem.ToString();
        }
    }
}
