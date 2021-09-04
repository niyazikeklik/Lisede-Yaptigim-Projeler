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
    public partial class guncelle : Form
    {
        public guncelle()
        {
            InitializeComponent();
        }
        
        private void liste()
        {
            uyeler goster = new uyeler();
            komut = "select * from uyeler";
            da = new OleDbDataAdapter(komut, baglanti);
            ds = new DataSet();
            da.Fill(ds, "uyeler");
            goster.dataGridView1.DataSource = ds.Tables[0];

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sporsalonu.accdb");
        OleDbDataAdapter da;
        DataSet ds;
        string komut = "";
        private void button1_Click(object sender, EventArgs e)
        {
            uyeler goster = new uyeler();
            if (MessageBox.Show("Yaptığınız Değişiklikler Kaydedilsinmi ? ","Onay Formu",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
            komut = "update uyeler set adı = @adı,soyadı = @soyadı,numara = @numara,tc = @tc,yas = @yas,adres = @adres,boy = @boy,kilo = @kilo,gobek = @gobek,kol = @kol,bacak = @bacak,boyun =@boyun  where uye_no = @anahtar";
            OleDbCommand isle = new OleDbCommand(komut,baglanti);
            baglanti.Open();
            isle.Parameters.AddWithValue("@adı", textBox1.Text);
            isle.Parameters.AddWithValue("@soyadı", textBox2.Text);
            isle.Parameters.AddWithValue("@numara", textBox3.Text);
            isle.Parameters.AddWithValue("@tc", textBox4.Text);
            isle.Parameters.AddWithValue("@yas", int.Parse(textBox5.Text));
            isle.Parameters.AddWithValue("@adres", textBox6.Text);
            isle.Parameters.AddWithValue("@boy", int.Parse(textBox12.Text));
            isle.Parameters.AddWithValue("@kilo", int.Parse(textBox11.Text));
            isle.Parameters.AddWithValue("@gobek", int.Parse(textBox10.Text));
            isle.Parameters.AddWithValue("@kol", int.Parse(textBox9.Text));
            isle.Parameters.AddWithValue("@bacak", int.Parse(textBox8.Text));
            isle.Parameters.AddWithValue("@boyun", int.Parse(textBox7.Text));
            isle.Parameters.AddWithValue("@anahtar", textBox13.Text);
            isle.ExecuteNonQuery();
            baglanti.Close();          
            MessageBox.Show("Değişiklikler Kaydedilmiştir.");
            liste();
            this.Close();
            goster.Show();
           
            }
            else
            {
                MessageBox.Show("Değişiklikler Kaydedilmemiştir.");
                this.Close();
                goster.Show();

            }
           



            
            
            
        }

        private void guncelle_Load(object sender, EventArgs e)
        {
            this.Text = "Güncelle";
            label13.Enabled = false;
            textBox13.Enabled = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            yoneticipanel git = new yoneticipanel();
            this.Close();
            git.Show();
        }

       

       
        

       
    }
}
