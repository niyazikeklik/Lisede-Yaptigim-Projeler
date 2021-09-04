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
    public partial class ekle : Form
    {
        public ekle()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sporsalonu.accdb");
        OleDbCommand cmd;
        DataSet ds;
        OleDbDataAdapter da;
        string cinsiyet = "";
        public enum harfler
        {
            a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, r, s, t, u, v, y, z
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                string fotoadi2 = "";

                Random rnd = new Random();
              
                int sayi = rnd.Next(0, 100);
                string fotoadi = Application.StartupPath + "\\images\\" + textBox2.Text.Replace(" ", "") + sayi.ToString() + ".jpg";
                pictureBox1.Image.Save(fotoadi);
                fotoadi2 = textBox2.Text.Replace(" ","") + sayi.ToString() + ".jpg";

                con.Open();
                string komut = "insert into uyebilgiler (uresim,uadsoyad,ucinsiyet,udogumtarihi,uadres,unumarasi,ueposta,boy,kilo,omuz,kol,gogus,bel,bacak,kayıttarihi) values('" + fotoadi2 + "','" + textBox2.Text + "','" + cinsiyet + "','" + dateTimePicker1.Value + "','" + textBox6.Text + "','" + maskedTextBox2.Text + "','" + textBox4.Text + "'," + maskedTextBox1.Text + "," + maskedTextBox3.Text + "," + maskedTextBox4.Text + "," + maskedTextBox5.Text + "," + maskedTextBox6.Text + "," + maskedTextBox7.Text + "," + maskedTextBox8.Text + ",'"+dateTimePicker2.Value+"')";
                cmd = new OleDbCommand(komut, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
            else
            {
                string cinsiyet = "";
                if (radioButton1.Checked)
                {
                    cinsiyet = "Erkek";

                }
                else if (radioButton2.Checked)
                {
                    cinsiyet = "Kadın";
                }
               
                cmd = new OleDbCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update uyebilgiler set ucinsiyet =@cinsiyet,uadsoyad=@adsoyad,udogumtarihi=@dogumtarihi,uadres=@adres,unumarasi=@numara,ueposta=@eposta,boy=@boy,kilo=@kilo,omuz=@omuz,kol=@kol,gogus=@gogus,bel=@bel,bacak=@bacak where uno=@uyeno";
                cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                cmd.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                cmd.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@adres", textBox6.Text);
                cmd.Parameters.AddWithValue("@numara", maskedTextBox2.Text);
                cmd.Parameters.AddWithValue("@eposta", textBox4.Text);
                cmd.Parameters.AddWithValue("@boy",Convert.ToInt32(maskedTextBox1.Text));
                cmd.Parameters.AddWithValue("@kilo", Convert.ToInt32(maskedTextBox3.Text));
                cmd.Parameters.AddWithValue("@omuz", Convert.ToInt32(maskedTextBox4.Text));
                cmd.Parameters.AddWithValue("@kol", Convert.ToInt32(maskedTextBox5.Text));
                cmd.Parameters.AddWithValue("@gogus", Convert.ToInt32(maskedTextBox6.Text));
                cmd.Parameters.AddWithValue("@bel",Convert.ToInt32(maskedTextBox7.Text));
                cmd.Parameters.AddWithValue("@bacak", Convert.ToInt32(maskedTextBox8.Text));
                cmd.Parameters.AddWithValue("@uyeno",Convert.ToInt32(textBox1.Text));

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("tamamdır");
                
            }
            



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "Erkek";
            label27.Text = "Erkek";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "Kadın";
            label27.Text = "Kadın";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label25.Text = textBox2.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label30.Text = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label29.Text = maskedTextBox2.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = textBox6.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        OpenFileDialog file = new OpenFileDialog();
        private void textBox3_Click(object sender, EventArgs e)
        {
           
            file.InitialDirectory = "C:\\";
            file.Filter = "|*.jpg";

            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = file.FileName.ToString();
                pictureBox1.Image = Image.FromFile(file.FileName);
                pictureBox1.Enabled = true;

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label26.Text = dateTimePicker1.Value.ToString();
        }

        private void ekle_Load(object sender, EventArgs e)
        {
            label26.Text = dateTimePicker1.Value.ToString();
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            label29.Text = maskedTextBox2.Text;
        }

        private void ekle_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ekle_Load_1(object sender, EventArgs e)
        {

        }
    }
}
