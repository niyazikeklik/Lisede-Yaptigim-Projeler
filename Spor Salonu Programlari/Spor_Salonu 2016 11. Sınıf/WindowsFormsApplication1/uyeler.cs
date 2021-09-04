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
    public partial class uyeler : Form
    {
        public uyeler()
        {
            InitializeComponent();
        }
        guncelle frmguncel = new guncelle();
        private void liste() {

            komut = "select * from uyeler";
            da = new OleDbDataAdapter(komut, baglanti);
            ds = new DataSet();
            da.Fill(ds, "uyeler");
            dataGridView1.DataSource = ds.Tables[0];
           
        
        }
        private void button5_Click(object sender, EventArgs e)
        {
            yoneticipanel git = new yoneticipanel();
            this.Close();
            git.Show();
            git.Text = "Anasayfa";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ekle git2 = new ekle();
            this.Hide();
            git2.Show();   
            git2.Text = "Yeni Kayıt Ekleme";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           frmguncel.textBox13.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frmguncel.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frmguncel.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frmguncel.textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frmguncel.textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frmguncel.textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frmguncel.textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            frmguncel.textBox7.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            frmguncel.textBox8.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            frmguncel.textBox9.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            frmguncel.textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            frmguncel.textBox11.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            frmguncel.textBox12.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            frmguncel.ShowDialog();
            this.Close();
          



          
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sporsalonu.accdb");
        OleDbDataAdapter da;
        DataSet ds;
        string komut = "";
        private void uyeler_Load(object sender, EventArgs e)
        {
            
            liste();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[0].HeaderText = "Üye No";
            dataGridView1.Columns[1].HeaderText = "Adı";
            dataGridView1.Columns[2].HeaderText = "Soyadı";
            dataGridView1.Columns[3].HeaderText = "Telefon No";
            dataGridView1.Columns[4].HeaderText = "TC.Kimlik No";
            dataGridView1.Columns[5].HeaderText = "Yaşı";
            dataGridView1.Columns[6].HeaderText = "Adresi";
            dataGridView1.Columns[7].HeaderText = "Boyu";
            dataGridView1.Columns[8].HeaderText = "Kilosu";
            dataGridView1.Columns[9].HeaderText = "Göbek(cm)";
            dataGridView1.Columns[10].HeaderText = "Kol(cm)";
            dataGridView1.Columns[11].HeaderText = "Bacak(cm)";
            dataGridView1.Columns[12].HeaderText = "Boyun(cm)";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Text = "Üyeler";

          
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Kayıtlı Üyeyi Silmek İstiyormusunuz ?", "Nasıl Devam Etmek İstiyorsunuz..", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                komut = "DELETE FROM uyeler where uye_no=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                OleDbCommand isle = new OleDbCommand(komut,baglanti);
                baglanti.Open();
                isle.ExecuteNonQuery();
                baglanti.Close();
                liste();
            }
              
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

            komut = "select * from uyeler where uye_no LIKE'" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(komut,baglanti);
            ds =  new DataSet();
            da.Fill(ds, "uyeler");
            dataGridView1.DataSource = ds.Tables[0];

            }
            else if (radioButton2.Checked)
            {

            komut = "select * from uyeler where tc LIKE'" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(komut,baglanti);
            ds =  new DataSet();
            da.Fill(ds, "uyeler");
            dataGridView1.DataSource = ds.Tables[0];

            }

            else if (radioButton3.Checked)
            {

            komut = "select * from uyeler where adı LIKE'" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(komut,baglanti);
            ds =  new DataSet();
            da.Fill(ds, "uyeler");
            dataGridView1.DataSource = ds.Tables[0];

            }

            

            


            
           


           


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label6.Text = "Üye No Giriniz..";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label6.Text = "TC Kimlik No Giriniz..";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                label6.Text = "Üye İsmini Giriniz..";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            liste();
        }   
    }
}
