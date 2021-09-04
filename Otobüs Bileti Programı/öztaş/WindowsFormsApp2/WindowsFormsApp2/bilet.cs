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
    public partial class bilet : Form
    {
        public bilet()
        {
            InitializeComponent();
        }
        void griddodlur()
        {
            baglantı.Open();
            string kayit = "SELECT * from tablo2";
            OleDbCommand komut = new OleDbCommand(kayit, baglantı);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();


        }
        void butonboya(Button btn)
        {

            foreach (Control c in groupBox4.Controls)
            {
                if (c.Enabled)
                {
                    c.BackColor = Color.White;
                    c.ForeColor = Color.Black;
                }
            }
            if (cb3.SelectedIndex == 1)
            {
                btn.BackColor = Color.Blue;
                btn.ForeColor = Color.White;
                textBox3.Text = btn.Text;

            }
            else if (cb3.SelectedIndex == 0)
            {
                btn.BackColor = Color.Pink;
                textBox3.Text = btn.Text;
            }
            else
            {
                MessageBox.Show("Lütfen cinsiyet belirtiniz.", "Bilgi Eksik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Kullanıcıkayıt.mdb");
        private void bilet_Load(object sender, EventArgs e)
        {
            griddodlur();
            for (int i = 0; i <= dataGridView1.Rows.Count - 2; i++)
            {
                foreach (Control c in groupBox4.Controls)
                {
                    if (dataGridView1.Rows[i].Cells[8].Value.ToString() == c.Text)
                    {
                        if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "Kadın")
                        {
                            c.Enabled = false;
                            c.BackColor = Color.Pink;
                        }
                        else if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "Erkek")
                        {
                            c.Enabled = false;
                            c.BackColor = Color.Blue;
                            c.ForeColor = Color.White;
                        }

                        break;
                    }
                }
            }
        }




        int fiyat = 0;
        private void cb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb1.Text == "İstanbul" && cb2.Text == "Ankara")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Konya" && cb2.Text == "Ankara")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Konya" && cb2.Text == "İzmir")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İstanbul" && cb2.Text == "Çanakkale")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İzmir" && cb2.Text == "Konya")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İzmir" && cb2.Text == "Çanakkale")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İzmir" && cb2.Text == "Ankara")
            {
                fiyat = 45;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İstanbul" && cb2.Text == "İzmir")
            {
                fiyat = 40;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Çanakkale" && cb2.Text == "Ankara")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Konya" && cb2.Text == "İstanbul")
            {
                fiyat = 60;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Konya" & cb2.Text == "Çanakkale")
            {
                fiyat = 60;

            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Ankara" && cb2.Text == "İstanbul")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Ankara" && cb2.Text == "Konya")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Çanakkale" && cb2.Text == "İstanbul")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Çanakkale" && cb2.Text == "izmir")
            {
                fiyat = 30;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Ankara" && cb2.Text == "İzmir")
            {
                fiyat = 45;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Ankara" && cb2.Text == "Çanakkale")
            {
                fiyat = 55;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "İstanbul" && cb2.Text == "Konya")
            {
                fiyat = 60;
            }
            label12.Text = fiyat.ToString();
            if (cb1.Text == "Çanakkale" && cb2.Text == "Konya")
            {
                fiyat = 60;
            }
            label12.Text = fiyat.ToString();
        }


        private void button42_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != "" && txtksahibi.Text != "" && txtknumarası.Text != "" && txtgkodu.Text != "" && cb1.SelectedIndex != -1 && cb2.SelectedIndex != -1 && cb3.SelectedIndex != -1)
            {

                string kombo1 = cb1.SelectedItem.ToString(), kombo2 = cb2.SelectedItem.ToString(), kombo3 = cb3.SelectedItem.ToString();

                if (checkBox1.Checked)
                {

                    baglantı.Close();
                    baglantı.Open();
                    string ekle = "insert into tablo2(ad,soyad,tc,cinsiyet,nereden,nereye,tarih,koltuk,fiyat) values (@ad,@soyad,@tc,@cinsiyet,@nereden,@nereye,@tarih,@koltuk,@fiyat)";
                    OleDbCommand komut = new OleDbCommand(ekle, baglantı);
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@tc", textBox4.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", kombo3);
                    komut.Parameters.AddWithValue("@nereden", kombo1);
                    komut.Parameters.AddWithValue("@nereye", kombo2);
                    komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToString());
                    komut.Parameters.AddWithValue("@koltuk", Convert.ToInt32(textBox3.Text));
                    komut.Parameters.AddWithValue("@fiyat", Convert.ToInt32(label12.Text));
                    komut.ExecuteNonQuery();
                    baglantı.Close();
                    MessageBox.Show("Ödemeniz alınmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 yeni = new Form2();
                    yeni.label8.Text = textBox4.Text;
                    yeni.label9.Text = textBox1.Text;
                    yeni.label10.Text = textBox2.Text;
                    yeni.label11.Text = textBox6.Text;
                    yeni.label12.Text = dateTimePicker1.Value.ToString();
                    yeni.label13.Text = kombo1 + "-" + kombo2 + " Otobüsü " + textBox3.Text + ". Koltuk";
                    yeni.Show();

                }
                else
                {
                    MessageBox.Show("Sözleşmeyi kabul etmek, boş alanları doldurmak zorundasınız.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb2.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);

        }
        private void button1_Click_2(object sender, EventArgs e)
        {

            butonboya((Button)sender);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            OleDbCommand komut = new OleDbCommand("update Tablo1 set ad=@ad,soyad=@soyad,tel=@tel,mail=@mail where tc='" + textBox4.Text + "'", baglantı);

            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@tel", textBox6.Text);
            komut.Parameters.AddWithValue("@mail", textBox5.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kaydınız güncellenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button28_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }


        private void button30_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);

        }

        private void button31_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button33_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button34_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button37_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button38_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button39_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void button43_Click_1(object sender, EventArgs e)
        {
            butonboya((Button)sender);
        }

        private void oturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void bilet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

