using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Spor_Salonu
{
    public partial class uyebilgiler : Form
    {
        public uyebilgiler()
        {
            InitializeComponent();
        }

      
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sporsalonu.accdb");
        OleDbCommand cmd;
        DataSet ds;
        OleDbDataAdapter da;
        public void griddoldur(){

            da = new OleDbDataAdapter("Select * from uyebilgiler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "uyebilgiler");
            dataGridView1.DataSource = ds.Tables["uyebilgiler"];
            con.Close();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            
            
           
        }
        public void aramaislemleri() {


            da = new OleDbDataAdapter("Select * from uyebilgiler where uno like '" + textBox1.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "uyebilgiler");
            dataGridView1.DataSource=ds.Tables["uyebilgiler"];
            con.Close();
        }
      

        private void button1_Click_1(object sender, EventArgs e)
        {
            ekle uyeekle = new ekle();
            uyeekle.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cmd = new OleDbCommand("DELETE FROM uyebilgiler WHERE uno=@uyeno", con);
                    cmd.Parameters.AddWithValue("@uyeno", dataGridView1.CurrentRow.Cells[0].Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    griddoldur();
                    MessageBox.Show("Seçili satır silindi.", "Başarılı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (OleDbException)
                {
                    con.Close();
                    MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            

            ekle guncelle = new ekle();
            guncelle.button1.Text = "guncelle";
            guncelle.Show();
            guncelle.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            guncelle.textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            guncelle.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //cinsiyet
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "Erkek")
            {
                guncelle.radioButton1.Checked = true;
            }
            else
            {
                guncelle.radioButton2.Checked = true; ;
            }
            //
            guncelle.textBox4.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            guncelle.maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            guncelle.textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            guncelle.maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            guncelle.maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            guncelle.maskedTextBox4.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            guncelle.maskedTextBox5.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            guncelle.maskedTextBox6.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            guncelle.maskedTextBox7.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            guncelle.maskedTextBox8.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
            guncelle.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);
            guncelle.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\images\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString());
          
        }
        double boy = 0;
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                boy = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)*10000 / (Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value)* Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value)));
                button2.Enabled = true;
                button3.Enabled = true;
                listBox1.Items.Clear();
                listBox1.Items.Add("Üye No: " + dataGridView1.CurrentRow.Cells[0].Value.ToString());
                listBox1.Items.Add("");
                listBox1.Items.Add("--------------------- ÜYE BİLGİLERİ ----------------------");
                listBox1.Items.Add("Adı Soyadı: " + dataGridView1.CurrentRow.Cells[2].Value.ToString());
                listBox1.Items.Add("Cinsiyet: " + dataGridView1.CurrentRow.Cells[3].Value.ToString());
                listBox1.Items.Add("Doğum Tarihi: " + dataGridView1.CurrentRow.Cells[4].Value.ToString());
                listBox1.Items.Add("Adres: " + dataGridView1.CurrentRow.Cells[5].Value.ToString());
                listBox1.Items.Add("Numarası: " + dataGridView1.CurrentRow.Cells[6].Value.ToString());
                listBox1.Items.Add("E-posta: " + dataGridView1.CurrentRow.Cells[7].Value.ToString());
                listBox1.Items.Add("");
                listBox1.Items.Add("------------------- VUCÜD ÖLÇÜMLERİ -------------------");
                listBox1.Items.Add("Boy: " + dataGridView1.CurrentRow.Cells[8].Value.ToString());
                listBox1.Items.Add("Kilo: " + dataGridView1.CurrentRow.Cells[9].Value.ToString());
                listBox1.Items.Add("Omuz: " + dataGridView1.CurrentRow.Cells[10].Value.ToString());
                listBox1.Items.Add("Kol: " + dataGridView1.CurrentRow.Cells[11].Value.ToString());
                listBox1.Items.Add("Göğüs: " + dataGridView1.CurrentRow.Cells[12].Value.ToString());
                listBox1.Items.Add("Bel: " + dataGridView1.CurrentRow.Cells[13].Value.ToString());
                listBox1.Items.Add("Bacak: " + dataGridView1.CurrentRow.Cells[14].Value.ToString());
             
                
                listBox1.Items.Add("Vücud Kitle Endeksi: " + boy.ToString());

                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\images\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString());
            }
            catch (Exception)
            {

                MessageBox.Show("Hata");
            }
        }

        private void uyebilgiler_Load(object sender, EventArgs e)
        {
          
            griddoldur();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            home.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            aramaislemleri();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
              
                try
                {
                    cmd = new OleDbCommand("DELETE FROM uyebilgiler WHERE uno=@uyeno", con);
                    cmd.Parameters.AddWithValue("@uyeno", dataGridView1.Rows[i].Cells[0].Value);
                    cmd.ExecuteNonQuery();  
                }
                catch (OleDbException)
                {
                    con.Close();
                    MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
            }
            con.Close();
            griddoldur();
            MessageBox.Show("Tüm kayıtlar başarıyla silindi.", "Başarılı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "E-mail Giriniz :";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Üye No Giriniz :";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Adı Soyadı Giriniz :";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Telefon Numarası Giriniz :";
        }

        private void kaloriCetveliToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void besinKaleriTablosuToolStripMenuItem_Click(object sender, EventArgs e)
        {

          

        }
    
}
}
