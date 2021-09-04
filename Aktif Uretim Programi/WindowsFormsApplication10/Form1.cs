using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net;
namespace WindowsFormsApplication10
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }
        public enum harfler
        {
            a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, r, s, t, u, v, y, z, x, q, mka, asd, akm
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=R:\\Aktif Üretim\\urunveribankasi.accdb");
        OleDbCommand cmd;
        OpenFileDialog file = new OpenFileDialog();
        string resim1 = "deneme.jpg", resim2 = "deneme2.jpg", resim3 = "deneme3.jpg", resim4 = "deneme4.jpg", resim5 = "deneme5.jpg";
        int i = 3;
        string surum = "SurumV1.0.0.txt";
        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            textBox14.Text = "";



        }
        void getir1()
        {
            try
            {


                if (comboBox3.Text != "" && comboBox1.Text != "")
                {


                    cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from urunn where STOK_KODU = '" + comboBox1.Text + "'and FİRMA = '" + comboBox3.Text + "'";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        temizle();
                        textBox2.Text = dr["Urun_Adi"].ToString();
                        textBox3.Text = dr["Ambalaj_Boyutu"].ToString();
                        textBox4.Text = dr["Sarim_Miktari"].ToString();
                        textBox5.Text = dr["Rulo_Ebati"].ToString();
                        textBox10.Text = dr["Tup_Ebati"].ToString();
                        textBox6.Text = dr["AMBALAJ_CİNSİ"].ToString();
                        textBox7.Text = dr["Aciklama"].ToString();
                        textBox1.Text = dr["Urun_Kg"].ToString();


                    }
                    else
                    {
                        temizle();
                        textBox2.Text = comboBox3.Text + " Adlı firmaya ait ürün( " + comboBox1.Text + " ) bulunamadı.";
                    }
                    con.Close();
                }



            }
            catch (Exception ex)
            {

                con.Close();
                MessageBox.Show(ex.Message);

            }
        }
        void getir2()
        {

            con.Open();
            cmd = new OleDbCommand();

            cmd.Connection = con;
            cmd.CommandText = "Select * from ambalaj where ambalaj_turu = '" + comboBox2.Text + "'";

            cmd.ExecuteNonQuery();
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                try
                {
                    textBox9.Text = dr["ambalaj_tanimi"].ToString();
                    textBox11.Text = dr["Firma"].ToString();
                    textBox14.Text = dr["paletleme_sekli"].ToString();
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + dr["resim1"].ToString());
                    resim1 = dr["resim1"].ToString();
                    resim2 = dr["resim2"].ToString();
                    resim3 = dr["resim3"].ToString();
                    resim4 = dr["resim4"].ToString();
                    resim5 = dr["resim5"].ToString();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message + " Dosya Bulunamadı.");

                }



            }
            else
            {
                textBox9.Text = "veri cekilemedi";
            }
            textBox8.Text = resim1;
            textBox15.Text = resim2;
            textBox16.Text = resim3;
            textBox17.Text = resim4;
            textBox18.Text = resim5;
            con.Close();



        }
        void listele1()
        {
            try
            {

                comboBox1.Items.Clear();
                con.Open();
                cmd = new OleDbCommand("SELECT * FROM urunn", con);
                cmd.CommandType = CommandType.Text;
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    if (comboBox1.Items.IndexOf(dr["Stok_Kodu"]) != -1)
                    {

                    }

                    else
                    {
                        comboBox1.Items.Add(dr["Stok_Kodu"]);

                    }

                } con.Close();
            }
            catch (Exception ex)
            {

                con.Close();
                MessageBox.Show(ex.Message);

            }



        }
        void listele2()
        {

            try
            {
                con.Open();
                comboBox2.Items.Clear();
                cmd = new OleDbCommand("SELECT * FROM ambalaj", con);
                cmd.CommandType = CommandType.Text;
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["Ambalaj_turu"]);

                } con.Close();

            }
            catch (Exception ex)
            {

                con.Close();
                MessageBox.Show(ex.Message);

            }


        }
        void listele3()
        {
            comboBox3.Items.Clear();


            try
            {
                con.Open();

                cmd = new OleDbCommand("SELECT * FROM urunn where Stok_Kodu = '" + comboBox1.Text + "'", con);
                cmd.CommandType = CommandType.Text;
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    if (comboBox3.Items.IndexOf(dr["FİRMA"]) != -1)
                    {

                    }
                    else
                    {
                        comboBox3.Items.Add(dr["FİRMA"]);
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {

                con.Close();
                MessageBox.Show(ex.Message);

            }
            if (comboBox3.Items.IndexOf("Standart") != -1)
            {
                comboBox3.Text = "Standart";
            }
            else if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
            else
            {
                comboBox3.SelectedIndex = -1;
            }

        }
        void acikmi(bool acik)
        {
            button10.Enabled = acik;
            textBox1.Enabled = acik;
            textBox2.Enabled = acik;
            textBox3.Enabled = acik;
            textBox4.Enabled = acik;
            textBox5.Enabled = acik;
            textBox6.Enabled = acik;
            textBox7.Enabled = acik;
            textBox9.Enabled = acik;
            textBox10.Enabled = acik;
            textBox11.Enabled = acik;
            textBox14.Enabled = acik;
            button1.Enabled = acik;
            button3.Enabled = acik;
            button4.Enabled = acik;
            button5.Enabled = acik;
            button6.Enabled = acik;
            button11.Enabled = acik;

            button13.Enabled = acik;


            button16.Enabled = acik;
            button17.Enabled = acik;
            button18.Enabled = acik;
            button19.Enabled = acik;


        }
        void stoktemizle()
        {
            comboBox3.Items.Clear();
            comboBox1.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox10.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";


        }
        void ambalajtemizle()
        {
            comboBox2.Text = "";
            textBox11.Text = "";
            textBox14.Text = "";
            textBox9.Text = "";
            resim1 = "deneme.jpg";
            resim2 = "deneme2.jpg";
            resim3 = "deneme3.jpg";
            resim4 = "deneme4.jpg";
            resim5 = "deneme5.jpg";
            pictureBox1.Image = pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim1);
            textBox8.Text = "deneme.jpg";
            textBox15.Text = "deneme2.jpg";
            textBox16.Text = "deneme3.jpg";
            textBox17.Text = "deneme4.jpg";
            textBox18.Text = "deneme5.jpg";



        }
        void tumunutemizle()
        {
            stoktemizle();
            ambalajtemizle();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            otomatikGuncellemeYapToolStripMenuItem.Checked = Settings1.Default.guncelleme;
            if (otomatikGuncellemeYapToolStripMenuItem.Checked == true)
            {
                if (File.Exists("R:\\Aktif Üretim\\" + surum)==false)
                {
                    if (MessageBox.Show("Programınız güncel değildir yeni sürüm mevcut. Yüklemek ister misiniz? Çok kısa sürücek.", "Guncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        try
                        {
                            string myPath = @"R:\Aktif Üretim\guncelle\AktifUguncelle.exe";
                            System.Diagnostics.Process prc = new System.Diagnostics.Process();
                            prc.StartInfo.FileName = myPath;
                            prc.Start();
                            Application.Exit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            try
            {
                acikmi(false);
                listele1();
                listele2();
               
            
                progressBar1.Value = 0;
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            getir1();

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            acikmi(true);

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


            acikmi(false);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            listele2();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getir2();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getir1();
            listele3();

        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            acikmi(false);
            radioButton1.Checked = true;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            getir1();
            Thread.Sleep(100);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (i == 5)
                {
                    i = 1;
                }
                else
                {
                    i++;
                }
                label18.Text = i.ToString() + " / 5";
                if (i == 1)
                {
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim1);
                }
                else if (i == 2)
                {
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim2);
                }
                else if (i == 3)
                {
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim3);
                }
                else if (i == 4)
                {
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim4);
                }
                else if (i == 5)
                {
                    pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);

            }




        }
        private void button7_Click(object sender, EventArgs e)
        {

            if (i == 1)
            {
                i = 5;
            }
            else
            {
                i--;
            }
            label18.Text = i.ToString() + " / 5";
            if (i == 1)
            {
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim1);
            }
            else if (i == 2)
            {
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim2);
            }
            else if (i == 3)
            {
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim3);
            }
            else if (i == 4)
            {
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim4);
            }
            else if (i == 5)
            {
                pictureBox1.Image = Image.FromFile("R:\\Aktif Üretim\\images\\" + resim5);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text != "" && comboBox3.Text != "")
            {
                progressBar1.Value = 0;
                try
                {
                    
                    cmd = new OleDbCommand("insert into urunn (STOK_KODU,Urun_Adi,Ambalaj_Boyutu,Sarim_Miktari,Rulo_Ebati,Tup_Ebati,AMBALAJ_CİNSİ,Urun_Kg,FİRMA,Aciklama) values('" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox10.Text + "','" + textBox6.Text.ToTitleCase() + "','" + textBox1.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yeni stok kayıdı eklendi Stok Kodu : ''" + comboBox1.Text + "'' Firması '': " + comboBox3.Text + "''", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } progressBar1.Value = 50;

                stoktemizle();
                progressBar1.Value = 100;
                progressBar1.Value = 0;
            }
            else
            {
                MessageBox.Show("Lütfen Stok Kodunu ve Firmasını Giriniz. Firması yoksa Standart olarak kaydediniz.", "Boş Geçilemez", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listele1();
            listele3();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            if (comboBox1.Text != "" && comboBox3.Text != "")
            {
                try
                {
                    string combobaxfirma = comboBox3.Text;
                    cmd = new OleDbCommand("update urunn set STOK_KODU=@STOK_KODU,Urun_Adi=@Urun_Adi,Ambalaj_Boyutu=@Ambalaj_Boyutu,Sarim_Miktari=@Sarım_Miktari,Rulo_Ebati=@Rulo_Ebati,Tup_Ebati=@Tup_Ebati,AMBALAJ_CİNSİ=@Ambalaj_Cinsi,Urun_Kg=@UrunKg,FİRMA=@Firma,Aciklama=@Aciklama where STOK_KODU='" + comboBox1.Text + "' and FİRMA = '" + comboBox3.Text + "'", con);

                    cmd.Parameters.AddWithValue("@Urun_Adi", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Ambalaj_Boyutu", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Sarım_Miktari", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Rulo_Ebati", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Tup_Ebati", textBox10.Text);
                    cmd.Parameters.AddWithValue("@Ambalaj_Cinsi", textBox6.Text);
                    cmd.Parameters.AddWithValue("@UrunKg", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Aciklama", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Firma", combobaxfirma);
                    cmd.Parameters.AddWithValue("@STOK_KODU", comboBox1.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                stoktemizle();
                progressBar1.Value = 50;
            }
            else
            {
                MessageBox.Show("Lütfen Stok Kodunu ve Firmasını Giriniz.", "Boş Geçilemez", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 100;
            progressBar1.Value = 0;
            listele1();
            listele3();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            try
            {
                cmd = new OleDbCommand("DELETE FROM urunn WHERE STOK_KODU='" + comboBox1.Text + "' and Firma = '" + comboBox3.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } progressBar1.Value = 50;

            stoktemizle();
            progressBar1.Value = 100;
            progressBar1.Value = 0;
            listele1();
            listele3();
        }
        private void button15_Click(object sender, EventArgs e)
        {

        }
        private void button14_Click(object sender, EventArgs e)
        {

        }
        private void button12_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click_1(object sender, EventArgs e)
        {

            if (comboBox2.Text != "")
            {
                try
                {
                    cmd = new OleDbCommand("update ambalaj set Ambalaj_tanimi=@ambalaj_tanimi,Firma=@Firma,paletleme_sekli=@paletleme_sekli,resim1=@resim1,resim2=@resim2,resim3=@resim3,resim4=@resim4,resim5=@resim5 where Ambalaj_turu ='" + comboBox2.Text + "'", con);
                    cmd.Parameters.AddWithValue("@ambalaj_tanimi", textBox9.Text);
                    cmd.Parameters.AddWithValue("@Firma", textBox11.Text);
                    cmd.Parameters.AddWithValue("@paletleme_sekli", textBox14.Text);
                    cmd.Parameters.AddWithValue("@resim1", textBox8.Text);
                    cmd.Parameters.AddWithValue("@resim2", textBox15.Text);
                    cmd.Parameters.AddWithValue("@resim3", textBox16.Text);
                    cmd.Parameters.AddWithValue("@resim4", textBox17.Text);
                    cmd.Parameters.AddWithValue("@resim5", textBox18.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Başarılı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata mesajı : " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Lütfen Güncellencek ambalaj tünü Seçiniz.");
            }
            listele2();

        }
        private void button13_Click(object sender, EventArgs e)
        {


            file.Filter = "|*.jpg";
            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {
                Random rnd = new Random();
                int harf = rnd.Next(0, 29);
                int sayi = rnd.Next(0, 10000);

                pictureBox1.Image = Image.FromFile(file.FileName);
                resim1 = comboBox2.Text + "_r1.jpg";
                textBox8.Text = comboBox2.Text + "_r1.jpg";


                pictureBox1.Image.Save("R:\\Aktif Üretim\\images\\" + comboBox2.Text + "_r1.jpg");
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {

            file.Filter = "|*.jpg";

            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {

                Random rnd = new Random();
                int harf = rnd.Next(0, 29);
                int sayi = rnd.Next(0, 10000);
                pictureBox1.Image = Image.FromFile(file.FileName);
                resim2 = comboBox2.Text + "_r2.jpg";
                textBox15.Text = comboBox2.Text + "_r2.jpg";


                pictureBox1.Image.Save("R:\\Aktif Üretim\\images\\" + comboBox2.Text + "_r2.jpg");
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {

            file.Filter = "|*.jpg";

            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {
                Random rnd = new Random();
                int harf = rnd.Next(0, 29);
                int sayi = rnd.Next(0, 10000);

                pictureBox1.Image = Image.FromFile(file.FileName);
                resim3 = comboBox2.Text + "_r3.jpg";
                textBox16.Text = comboBox2.Text + "_r3.jpg";

                pictureBox1.Image.Save("R:\\Aktif Üretim\\images\\" + comboBox2.Text + "_r3.jpg");
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {

            file.Filter = "|*.jpg";

            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {
                Random rnd = new Random();
                int harf = rnd.Next(0, 29);
                int sayi = rnd.Next(0, 10000);

                pictureBox1.Image = Image.FromFile(file.FileName);
                resim4 = comboBox2.Text + "_r4.jpg";
                textBox17.Text = comboBox2.Text + "_r4.jpg";

                pictureBox1.Image.Save("R:\\Aktif Üretim\\images\\" + comboBox2.Text + "_r4.jpg");
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {

            file.Filter = "|*.jpg";

            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {

                Random rnd = new Random();
                int harf = rnd.Next(0, 29);
                int sayi = rnd.Next(0, 10000);
                pictureBox1.Image = Image.FromFile(file.FileName);
                resim5 = comboBox2.Text + "_r5.jpg";
                textBox18.Text = comboBox2.Text + "_r5.jpg";


                pictureBox1.Image.Save("R:\\Aktif Üretim\\images\\" + comboBox2.Text + "_r5.jpg");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            try
            {
               
                cmd = new OleDbCommand("DELETE FROM ambalaj WHERE Ambalaj_turu='" + comboBox2.Text + "'", con);


                
              
                  
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 50;
          
            ambalajtemizle();
            listele2();
            progressBar1.Value = 100;
            progressBar1.Value = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (comboBox2.Text != "")
            {
                progressBar1.Value = 0;
                try
                {
                    con.Open();
                    cmd = new OleDbCommand("insert into ambalaj(Ambalaj_turu,Ambalaj_tanimi,Firma,paletleme_sekli,resim1,resim2,resim3,resim4,resim5) values('" + comboBox2.Text + "','" + textBox9.Text + "','" + textBox11.Text + "','" + textBox14.Text + "','" + textBox8.Text + "','" + textBox15.Text + "','" + textBox16.Text + "','" + textBox17.Text + "','" + textBox18.Text + "')",con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yeni stok kayıdı eklendi Stok Kodu : ''" + comboBox1.Text + "'' Firması '': " + comboBox3.Text + "''", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } progressBar1.Value = 50;

                stoktemizle();
                progressBar1.Value = 100;
                progressBar1.Value = 0;
            }
            else
            {
                MessageBox.Show("Lütfen Ambalaj Türü Giriniz", "Boş Geçilemez", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listele2();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            resimbuyut resimbuyut = new resimbuyut();
            resimbuyut.WindowState = FormWindowState.Maximized;
            resimbuyut.pictureBox1.Image = pictureBox1.Image;
            resimbuyut.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            resimbuyut resimbuyut = new resimbuyut();
            resimbuyut.Height = pictureBox1.Image.Height;
            resimbuyut.Width = pictureBox1.Image.Width;
            resimbuyut.pictureBox1.Image = pictureBox1.Image;
            resimbuyut.Show();
        }

        private void ambalajKutucuklarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ambalajtemizle();
        }

        private void stokKutucuklarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stoktemizle();
        }

        private void tümKutucuklarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tumunutemizle();
        }

        private void girişYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tumunutemizle();
            listele1();
            listele2();
            listele3();
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = this.toolStripTextBox2.Control as TextBox;
            tb.PasswordChar = '*';

            if (tb.Text == "nese1")
            {
                radioButton2.Checked = true;
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {

            if (toolStripTextBox1.Text == "neseplastik")
            {
                toolStripTextBox2.Focus();
            }
        }

        private void oturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void girişYapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "neseplastik")
            {
                if (toolStripTextBox2.Text == "nese1")
                {
                    radioButton2.Checked = true;

                }
                else
                {
                    MessageBox.Show("Şifreniz yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adınız yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripTextBox2.Text = "";
                toolStripTextBox2.Focus();

            }

        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
      
        private void UpdateKontrol()
        {

            if (File.Exists("R:\\Aktif Üretim\\" + surum))
            {
                MessageBox.Show("Programınız Güncel.", "Güncel Sürüm", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {


                if (MessageBox.Show("Programınız güncel değildir yeni sürüm mevcut. Yüklemek ister misiniz? Çok kısa sürücek.", "Olumsuz", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        string myPath = @"R:\Aktif Üretim\guncelle\AktifUguncelle.exe";
                        System.Diagnostics.Process prc = new System.Diagnostics.Process();
                        prc.StartInfo.FileName = myPath;
                        prc.Start();
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }



            }
        }
        private void guncellemeKontrolüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateKontrol();
        }

        private void niyaziKeklikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2016/2017 Neşe Plastik kış stajyeri.", "Koder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void epostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("niyazikeklikk@gmail.com" + Environment.NewLine + "niyazikeklik@gmail.com", "Eposta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void whatsappTelefoNNumarasıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+90 534 686 1675", "Telefon Numarası", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        int calistir = 0;
        private void şifreniMiUnuttunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calistir == 0)
            {


                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    MailMessage mesaj = new MailMessage();
                    mesaj.To.Add("saffetm@neseplastik.com");
                    mesaj.From = new MailAddress("odevimp@gmail.com");
                    mesaj.Subject = "Aktif Üretim Programı Giriş Şifreniz";
                    mesaj.Body = "Aktif Üretim Programı giriş için Kullanıcı Adı : ''neseplastik'', Şifresi : ''nese1'' ' dir. Niyazi Keklik";
                    NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                    client.Credentials = guvenlik;
                    client.EnableSsl = true;
                    client.Send(mesaj);
                    MessageBox.Show("Mail Başarıyla ''saffetm@neseplastik.com'' Adresine Gönderildi.", "Mail Gönderme");
                    calistir += 5;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mesajınız gönderelimedi. Hata Mesajı : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Zaten az önce bir eposta gönderildi.");
            }
        }
        bool silinsin = false;
        private void button10_Click_1(object sender, EventArgs e)
        {
            int say = 0;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select resim1,resim2,resim3,resim4,resim5 from ambalaj", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["resim1"].ToString());
                listBox1.Items.Add(dr["resim2"].ToString());
                listBox1.Items.Add(dr["resim3"].ToString());
                listBox1.Items.Add(dr["resim4"].ToString());
                listBox1.Items.Add(dr["resim5"].ToString());
            }
            
            con.Close();
        DirectoryInfo di = new DirectoryInfo(@"R:\Aktif Üretim\images");
        FileInfo[] files  = di.GetFiles("*.jpg");
        foreach (FileInfo fi in files)
        {
            silinsin = true;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString()==fi.Name)
                {
                    silinsin = false;
                }
            } 
            if (silinsin==true)
            {
                if (fi.Name!="deneme.jpg"||fi.Name!="deneme2.jpg"||fi.Name!="deneme3.jpg"||fi.Name!="deneme4.jpg"||fi.Name!="deneme5.jpg")
                {
                    say++;
                    File.Delete(@"R:\Aktif Üretim\images\" + fi.Name);
                }
              
            }
        }

        MessageBox.Show("Fazla resimler silinmiştir. Toplam : "+ say.ToString());
        }

        private void otomatikGuncellemeYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (otomatikGuncellemeYapToolStripMenuItem.Checked==false)
            {
                otomatikGuncellemeYapToolStripMenuItem.Checked = true;
            }
            else if (otomatikGuncellemeYapToolStripMenuItem.Checked==true)
            {
                otomatikGuncellemeYapToolStripMenuItem.Checked = false;

            }
            Settings1.Default.guncelleme = otomatikGuncellemeYapToolStripMenuItem.Checked;
            Settings1.Default.Save();

        }

    }
}


