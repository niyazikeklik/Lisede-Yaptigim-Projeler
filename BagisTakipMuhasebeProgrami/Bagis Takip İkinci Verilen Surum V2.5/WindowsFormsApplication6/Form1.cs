using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Graphics g;

          
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        int b1 = 0, b2 = 0, b3 = 0, b4 = 0, sonuc = 0, sifir = 0; int toplamm=0;
        public string buyut(string text)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
        private void grid2doldur(){
        
           
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                
                    if (comboBox2.Items.IndexOf(dataGridView1.Rows[i].Cells[1].Value) == -1)
                    {
                        comboBox2.Items.Add(dataGridView1.Rows[i].Cells[1].Value);
                    }
                   }
        }
        private void geliradidoldur() {
          
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (comboBox3.Items.IndexOf(dataGridView2.Rows[i].Cells[0].Value)==-1)
                {
                    comboBox3.Items.Add(dataGridView2.Rows[i].Cells[0].Value);
                }
            }


        }
        private void ogrnodoldur()
        {

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (comboBox4.Items.IndexOf(dataGridView1.Rows[i].Cells[0].Value) == -1)
                {
                    comboBox4.Items.Add(dataGridView1.Rows[i].Cells[0].Value);
                }
            }


        }
        private void temizle()
        {
            comboBox4.Text = "";
          comboBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePicker3.Value = DateTime.Today;
            dateTimePicker4.Value = DateTime.Today;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void degis()
        {
            dataGridView1.Columns[0].HeaderText = "Öğrenci No";
            dataGridView1.Columns[1].HeaderText = "Sınıfı";
            dataGridView1.Columns[2].HeaderText = "Adı";
            dataGridView1.Columns[3].HeaderText = "Veli Adı";
            dataGridView1.Columns[4].HeaderText = "1. Bağış";
            dataGridView1.Columns[5].HeaderText = "1. Tarih";
            dataGridView1.Columns[6].HeaderText = "2. Bağış";
            dataGridView1.Columns[7].HeaderText = "2. Tarih";
            dataGridView1.Columns[8].HeaderText = "3. Bağış";
            dataGridView1.Columns[9].HeaderText = "3. Tarih";
            dataGridView1.Columns[10].HeaderText = "4. Bağış";
            dataGridView1.Columns[11].HeaderText = "4. Tarih";
            dataGridView1.Columns[12].HeaderText = "Toplam";
        }
        
        private void doldur(){
            try
            {
                toplamm = 0;
                con.Open();
                string komut = "select * from bagis";
                da = new OleDbDataAdapter(komut, con);
                ds = new DataSet();
                da.Fill(ds, "bagis");
                dataGridView1.DataSource = ds.Tables["bagis"];
                con.Close();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    toplamm += Convert.ToInt32(dataGridView1.Rows[i].Cells[12].Value);

                }

                label13.Text = toplamm.ToString();
                degis();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }


        private void doldur2()
        {
            try
            {

            toplamm = 0;
            con.Open();
            string komut = "select * from gelirler";
            da = new OleDbDataAdapter(komut, con);
            ds = new DataSet();
            da.Fill(ds, "gelirler");
            dataGridView2.DataSource = ds.Tables["gelirler"];
            con.Close();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                toplamm += Convert.ToInt32(dataGridView2.Rows[i].Cells[13].Value);

            }

            label13.Text = toplamm.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

         
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
            textBox10.Text = Settings1.Default.setts;
            doldur();   
            button1.Enabled = false;
            dataGridView1.CurrentCell = null;
            grid2doldur();
            ogrnodoldur();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
           {

               comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value);
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value);
            dateTimePicker3.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[9].Value);
            dateTimePicker4.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[11].Value);

                comboBox4.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            this.AcceptButton = button1;
          }
            catch (Exception)
           {
               ;
                
           }
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            temizle();
            comboBox3.Text = "";
            comboBox1.Text = "Ay Seçiniz...";
            textBox12.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cmd = new OleDbCommand("DELETE FROM bagis WHERE Ogrno=@ogrno", con);
                    cmd.Parameters.AddWithValue("@ogrno", dataGridView1.CurrentRow.Cells[0].Value);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    doldur();
                   
                }
                catch (OleDbException)
                {
                    MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
         
            int toplam = 0;

            //try
            //{
          
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;

                //DİĞER GELİRLER EKLEME
                if (radioButton6.Checked)
                {
                if (comboBox3.Text=="" ||comboBox1.Text== "Ay Seçiniz..."|| textBox12.Text=="")
                {
                    con.Close();
                    MessageBox.Show("Lütfen bilgileri eksiksiz giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { 
                  
                    try
                    {
                        toplam += Convert.ToInt32(textBox12.Text); 
                        string komut = "insert into gelirler(geliradi," + comboBox1.Text + ") values('" + comboBox3.Text + "'," + Convert.ToInt32(textBox12.Text) + ")";
                        cmd.CommandText = komut;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Yeni Gelir Kaydedildi.");
                        con.Close();
                        doldur2();
                        temizle();
                    }
                    catch (OleDbException)
                    {
                        string komut = "update gelirler set " + comboBox1.Text + "="+Convert.ToInt32(textBox12.Text)+" where geliradi = '"+comboBox3.Text+"'";
                        cmd.CommandText = komut;
                        MessageBox.Show("Güncel.");
                        cmd.ExecuteNonQuery();
                        con.Close();
                        doldur2();
                        temizle();
                        
                    }
                    doldur2();
                    geliradidoldur();
                    button1.Enabled = false;
                    listBox1.Items.Clear();
                    int ocak = 0, subat = 0, mart = 0, nisan = 0, mayıs = 0, haziran = 0, temmuz = 0, agustos = 0, eylul = 0, ekim = 0, kasım = 0, aralık = 0;

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        ocak += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                        subat += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                        mart += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                        nisan += Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                        mayıs += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
                        haziran += Convert.ToInt32(dataGridView2.Rows[i].Cells[6].Value);
                        temmuz += Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
                        agustos += Convert.ToInt32(dataGridView2.Rows[i].Cells[8].Value);
                        eylul += Convert.ToInt32(dataGridView2.Rows[i].Cells[9].Value);
                        ekim += Convert.ToInt32(dataGridView2.Rows[i].Cells[10].Value);
                        kasım += Convert.ToInt32(dataGridView2.Rows[i].Cells[11].Value);
                        aralık += Convert.ToInt32(dataGridView2.Rows[i].Cells[12].Value);
                    }
                    listBox1.Items.Add("Ocak Ayı Toplam: " + ocak.ToString());
                    listBox1.Items.Add("Şubat Ayı Toplam: " + subat.ToString());
                    listBox1.Items.Add("Mart Ayı Toplam: " + mart.ToString());
                    listBox1.Items.Add("Nisan Ayı Toplam: " + nisan.ToString());
                    listBox1.Items.Add("Mayıs Ayı Toplam: " + mayıs.ToString());
                    listBox1.Items.Add("Haziran Ayı Toplam: " + haziran.ToString());
                    listBox1.Items.Add("Temmuz Ayı Toplam: " + temmuz.ToString());
                    listBox1.Items.Add("Ağustos Ayı Toplam: " + agustos.ToString());
                    listBox1.Items.Add("Eylül Ayı Toplam: " + eylul.ToString());
                    listBox1.Items.Add("Ekim Ayı Toplam: " + ekim.ToString());
                    listBox1.Items.Add("Kasım Ayı Toplam: " + kasım.ToString());
                    listBox1.Items.Add("Aralık Ayı Toplam: " + aralık.ToString());

                }

            }

                    //ÖĞRENCİ BAĞIŞI EKLEME
                else if (radioButton5.Checked)
                {


                    if (comboBox4.Text == "" || comboBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                    {
                    con.Close();
                        MessageBox.Show("Lütfen başında yıldız olan bilgileri eksiksiz giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        int sifir = 0;
                        if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
                        {
                            textBox5.Text = sifir.ToString();
                            textBox6.Text = sifir.ToString();
                            textBox7.Text = sifir.ToString();
                            textBox8.Text = sifir.ToString();
                        }

                    try
                    {
                        b1 = Convert.ToInt32(textBox5.Text);
                        b2 = Convert.ToInt32(textBox6.Text);
                        b3 = Convert.ToInt32(textBox7.Text);
                        b4 = Convert.ToInt32(textBox8.Text);
                        sonuc = b1 + b2 + b3 + b4;
                        cmd.CommandText = "insert into bagis (Ogrno,ogrsinif,ograd,veliad,bagis1,tarih1,bagis2,tarih2,bagis3,tarih3,bagis4,tarih4,toplam) values('" + buyut(comboBox4.Text) + "','" + buyut(comboBox2.Text) + "','" + buyut(textBox3.Text) + "','" + buyut(textBox4.Text) + "'," + Convert.ToInt32(textBox5.Text) + ",'" + dateTimePicker1.Value + "'," + Convert.ToInt32(textBox6.Text) + ",'" + dateTimePicker2.Value + "'," + Convert.ToInt32(textBox7.Text) + ",'" + dateTimePicker3.Value + "'," + Convert.ToInt32(textBox8.Text) + ",'" + dateTimePicker4.Value + "'," + sonuc + ")";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        doldur();
                        temizle();

                        ogrnodoldur();
                        grid2doldur();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show("Hata: " + ex.Message,"Hata meydana geldi!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                   

                    }

                }
               
            //}
            //catch (OleDbException)
            //{
            //    con.Close();
            //    MessageBox.Show("Öğrenci Numarası Zaten Kayıtlı", "Kayıt Zaten Var. İkinci bir bağış girmek istiyor olabilirsiniz, bu durumda güncelleme butonunu kullanmalısınız.");
                
            //}
                
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox4.Text == "" || comboBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {

                MessageBox.Show("Lütfen başında yıldız olan bilgileri eksiksiz giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                
                if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
                {
                    textBox5.Text = sifir.ToString();
                    textBox6.Text = sifir.ToString();
                    textBox7.Text = sifir.ToString();
                    textBox8.Text = sifir.ToString();}

                int sonuc2 = 0;
                        b1 = Convert.ToInt32(textBox5.Text);
                        b2 = Convert.ToInt32(textBox6.Text);
                        b3 = Convert.ToInt32(textBox7.Text);
                        b4 = Convert.ToInt32(textBox8.Text);
                        
                        sonuc2 = b1 + b2 + b3 + b4;

                        if (MessageBox.Show(comboBox4.Text + " Numaralı öğrencinin bilgilerini güncellemek üzeresiniz devam edilsin mi ?", "DİKKAT !!!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            cmd = new OleDbCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update bagis set ogrsinif='" + comboBox2.Text + "',ograd='" + textBox3.Text + "',veliad='" + textBox4.Text + "', bagis1=" + Convert.ToInt32(textBox5.Text) + ",bagis2=" + Convert.ToInt32(textBox6.Text) + ",bagis3=" + Convert.ToInt32(textBox7.Text) + ",bagis4=" + Convert.ToInt32(textBox8.Text) + ",tarih1='" + dateTimePicker1.Value + "',tarih2='" + dateTimePicker2.Value + "',tarih3='" + dateTimePicker3.Value + "',tarih4='" + dateTimePicker4.Value + "',toplam = '" + sonuc2 + "' where Ogrno='" + comboBox4.Text + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            doldur(); MessageBox.Show(comboBox4.Text + " Numaralı kayıt başarıla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        temizle();
                        grid2doldur();
                    }
                }
          
           
        

        private void Form1_Click(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
           
            
                doldur2();
                doldur();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
   
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void Yazdirilacak(Graphics gg)
        {
            
        }
        private void yazdir()
        { 
        short baskiAdeti = Convert.ToInt16(numericUpDown1.Value);
            if (baskiAdeti<= 0||textBox10.Text=="")
            {
                MessageBox.Show("Lütfen baskı adedini pozitif bir sayı giriniz. veya Öğretim yılını boş geçmeyiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                try
                {

                    printDocument1.PrinterSettings.Copies = baskiAdeti;
                    printDocument1.Print();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorun çıktı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
         }
        
        }
        private void button3_Click(object sender, EventArgs e)
        {
            yazdir();
           
        }
           
            






        

        

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        { 
        
            Image newImage = Image.FromFile("images\\Adsız.png");
            Point ulCorner = new Point(23, 20);
           
            Image newImage2 = Image.FromFile("images\\Adsız2.png");
            Point ulCorner2 = new Point(133, 140);
            
            Image newImage3 = Image.FromFile("images\\Adsız3.png");
            Point ulCorner3 = new Point(613, 140);
            
            Image newImage4 = Image.FromFile("images\\Adsız4.png");
            Point ulCorner4 = new Point(243, 140);

            e.Graphics.DrawImage(newImage, ulCorner);
            e.Graphics.DrawImage(newImage2, ulCorner2);
            e.Graphics.DrawImage(newImage3, ulCorner3);
            e.Graphics.DrawImage(newImage4, ulCorner4);
        
           Font myFont = new Font("Times New Roman", 14,FontStyle.Bold);
           Font myFont2 = new Font("Times New Roman", 13, FontStyle.Italic);
           SolidBrush sbrush = new SolidBrush(Color.Black);
           Pen myPen = new Pen(Color.Black);
           
           
          e.Graphics.DrawString("SAYI :", myFont, sbrush, 144, 240);
          e.Graphics.DrawString("Özel", myFont2, sbrush, 203, 243);
          e.Graphics.DrawString("KONU :", myFont, sbrush, 133, 260);
          e.Graphics.DrawString("Teşekkür Belgesi", myFont2, sbrush, 203, 263);


          myFont = new Font("Monotype Corsiva", 17, FontStyle.Italic);
           e.Graphics.DrawString("SAYIN "+textBox4.Text, myFont, sbrush, 333, 285);
           
            myFont2 = new Font("Times New Roman", 12, FontStyle.Italic);
            string bilgi ="("+ textBox3.Text + " - " + comboBox2.Text + " - " + comboBox4.Text + ")";
           e.Graphics.DrawString(bilgi, myFont2, sbrush, 335, 315);
            string yil=textBox10.Text;
           string yazi = yil+" Eğitim Öğretim yılında okulumuza bulunduğunuz maddi ve" + Environment.NewLine + "manevi destekten dolayı teşekkür ederiz.";
            myFont = new Font("Monotype Corsiva", 16, FontStyle.Italic);
            e.Graphics.DrawString(yazi, myFont, sbrush, 138, 343);

            Image newImage5 = Image.FromFile("images\\Adsız5.png");
            Point ulCorner5 = new Point(503, 390);

            e.Graphics.DrawImage(newImage5, ulCorner5);
 
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
       
        }

        private void textBox10_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox10_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox10_MouseHover(object sender, EventArgs e)
        {
            if (textBox10.Text=="")
            {
                textBox10.Text = Settings1.Default.setts;
            }
            
        }

        private void lütfenOkuyunuzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programda kullanılan sabit isimler(Okul,Aile Başkanlığı Başkanı Adı vs) programın kurulu olduğu dizinde images klasörünün içindedir." + Environment.NewLine + Environment.NewLine + "Herhangi bir değişiklik durumunda aynı dosya isminde ve ölçütlerinde farklı resimler ile değiştirmeniz durumunda güncel yazıcı çıktıları almak mümkündür. " +Environment.NewLine+"Okuduğunuz için teşekkür ederim.","Yazıcı Çıktısını Güncel Tutma",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Twitter : www.twitter.com\\alienationxs", "Twitter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string komut = "";
            int toplam = 0;
            con.Open();
            if (radioButton1.Checked)
            {
                komut = "Select * from bagis where Ogrno like '" + textBox9.Text + "%' ";

            }
            else if (radioButton2.Checked)
            {
                komut = "Select * from bagis where veliad like '" + textBox9.Text + "%' ";

            }
            else if (radioButton3.Checked)
            {
                komut = "Select * from bagis where ograd like '" + textBox9.Text + "%' ";

            }
            else if (radioButton4.Checked)
            {
                komut = "Select * from bagis where ogrsinif like '" + textBox9.Text + "%' ";

            }

            da = new OleDbDataAdapter(komut, con);
            ds = new DataSet();
            da.Fill(ds, "bagis");
            dataGridView1.DataSource = ds.Tables["bagis"];
            con.Close();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                toplam += Convert.ToInt32(dataGridView1.CurrentRow.Cells[12].Value);

            }
            label13.Text = toplam.ToString(); 
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
        
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
           
            this.AcceptButton = button8;
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yazdir();
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings1.Default.setts = textBox10.Text;
            Settings1.Default.Save();

        
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void niyaziKeklikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2016/2017 Neşe Plastik Kış Dönemi Stajyeri, Pendik Yunus Emre Mesleki Ve Teknik Anadolu Lisesi öğrencisi.","Who am I?",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void whatsappToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+90 534 686 1675", "WhatSapp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ePostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("E-posta 1: niyazikeklikk@gmail.com "+Environment.NewLine+"E-posta 2: niyazikeklik@gmail.com", "Who am I?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void instagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("İnstagram: www.instagram.com\\alienationxs " , "İnstagram", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void programıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Close();
            Application.Exit();
        }

        private void öğrenciAdınaGöreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
        }

        private void veliAdınaGöreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
        }

        private void öğrenciNumarasınaGöreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void sınıfaGöreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }

        private void enÇokBağışYapanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[12], ListSortDirection.Descending);
        }

        private void enAzBağışYapanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[12], ListSortDirection.Ascending);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            listBox1.Visible = true;
            listBox1.Items.Clear();          
            button8.Text = "Kayıt/Güncel";   
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = false;
            label4.Enabled = false;
            label5.Enabled = false;
            label6.Enabled = false;
            label7.Enabled = false;
            label8.Enabled = false;
            textBox8.Enabled = false;
            textBox7.Enabled = false;
            textBox6.Enabled = false;
            textBox5.Enabled = false;
            textBox4.Enabled = false;
            textBox3.Enabled = false;
            comboBox2.Enabled = false;
            comboBox4.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;
            comboBox3.Visible = true;
            textBox12.Visible = true;
            comboBox1.Visible = true;
            label23.Visible = true;
            label22.Visible = true;
            doldur2();
            geliradidoldur();
            button1.Enabled = false;
            int ocak = 0, subat = 0, mart = 0, nisan = 0, mayıs = 0, haziran = 0, temmuz = 0, agustos = 0, eylul = 0, ekim = 0, kasım = 0, aralık = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {

                ocak += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                subat += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                mart += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                nisan += Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                mayıs += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
                haziran += Convert.ToInt32(dataGridView2.Rows[i].Cells[6].Value);
                temmuz += Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
                agustos += Convert.ToInt32(dataGridView2.Rows[i].Cells[8].Value);
                eylul += Convert.ToInt32(dataGridView2.Rows[i].Cells[9].Value);
                ekim += Convert.ToInt32(dataGridView2.Rows[i].Cells[10].Value);
                kasım += Convert.ToInt32(dataGridView2.Rows[i].Cells[11].Value);
                aralık += Convert.ToInt32(dataGridView2.Rows[i].Cells[12].Value);
            }
            listBox1.Items.Add("Ocak Ayı Toplam: " + ocak.ToString());
            listBox1.Items.Add("Şubat Ayı Toplam: " + subat.ToString());
            listBox1.Items.Add("Mart Ayı Toplam: " + mart.ToString());
            listBox1.Items.Add("Nisan Ayı Toplam: " + nisan.ToString());
            listBox1.Items.Add("Mayıs Ayı Toplam: " + mayıs.ToString());
            listBox1.Items.Add("Haziran Ayı Toplam: " + haziran.ToString());
            listBox1.Items.Add("Temmuz Ayı Toplam: " + temmuz.ToString());
            listBox1.Items.Add("Ağustos Ayı Toplam: " + agustos.ToString());
            listBox1.Items.Add("Eylül Ayı Toplam: " + eylul.ToString());
            listBox1.Items.Add("Ekim Ayı Toplam: " + ekim.ToString());
            listBox1.Items.Add("Kasım Ayı Toplam: " + kasım.ToString());
            listBox1.Items.Add("Aralık Ayı Toplam: " + aralık.ToString());

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satırları silmek istediğinize emin misiniz?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (radioButton5.Checked==true)
            {
              
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
                {
                   
                   
                        try
                        {
                            cmd = new OleDbCommand("DELETE FROM bagis WHERE Ogrno=@ogrno", con);
                            cmd.Parameters.AddWithValue("@ogrno",drow.Cells[0].Value);
                            cmd.ExecuteNonQuery();
                            

                        }
                        catch (OleDbException)
                        {
                            con.Close(); MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                   
                }
                con.Close();
                doldur();
            }
            else
            {
                foreach (DataGridViewRow drow in dataGridView2.SelectedRows)  //Seçili Satırları Silme
                {
                   
                   
                        try
                        {
                            cmd = new OleDbCommand("DELETE FROM gelirler WHERE geliradi=@ogrno", con);
                            cmd.Parameters.AddWithValue("@ogrno", drow.Cells[0].Value);
                            cmd.ExecuteNonQuery();


                        }
                        catch (OleDbException)
                        {
                            MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    
                }
                con.Close();
                    doldur2();

                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox3.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked==true)
            {

                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                listBox1.Visible = true;
                listBox1.Items.Clear();
                button8.Text = "Kayıt/Güncel";
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                textBox8.Enabled = false;
                textBox7.Enabled = false;
                textBox6.Enabled = false;
                textBox5.Enabled = false;
                textBox4.Enabled = false;
                textBox3.Enabled = false;
                comboBox2.Enabled = false;
                comboBox4.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                comboBox3.Visible = true;
                textBox12.Visible = true;
                comboBox1.Visible = true;
                label23.Visible = true;
                label22.Visible = true;
                doldur2();
                geliradidoldur();
                button1.Enabled = false;
                int ocak = 0, subat = 0, mart = 0, nisan = 0, mayıs = 0, haziran = 0, temmuz = 0, agustos = 0, eylul = 0, ekim = 0, kasım = 0, aralık = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    ocak += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                    subat += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                    mart += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                    nisan += Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                    mayıs += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
                    haziran += Convert.ToInt32(dataGridView2.Rows[i].Cells[6].Value);
                    temmuz += Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
                    agustos += Convert.ToInt32(dataGridView2.Rows[i].Cells[8].Value);
                    eylul += Convert.ToInt32(dataGridView2.Rows[i].Cells[9].Value);
                    ekim += Convert.ToInt32(dataGridView2.Rows[i].Cells[10].Value);
                    kasım += Convert.ToInt32(dataGridView2.Rows[i].Cells[11].Value);
                    aralık += Convert.ToInt32(dataGridView2.Rows[i].Cells[12].Value);
                }
                listBox1.Items.Add("Ocak Ayı Toplam: " + ocak.ToString());
                listBox1.Items.Add("Şubat Ayı Toplam: " + subat.ToString());
                listBox1.Items.Add("Mart Ayı Toplam: " + mart.ToString());
                listBox1.Items.Add("Nisan Ayı Toplam: " + nisan.ToString());
                listBox1.Items.Add("Mayıs Ayı Toplam: " + mayıs.ToString());
                listBox1.Items.Add("Haziran Ayı Toplam: " + haziran.ToString());
                listBox1.Items.Add("Temmuz Ayı Toplam: " + temmuz.ToString());
                listBox1.Items.Add("Ağustos Ayı Toplam: " + agustos.ToString());
                listBox1.Items.Add("Eylül Ayı Toplam: " + eylul.ToString());
                listBox1.Items.Add("Ekim Ayı Toplam: " + ekim.ToString());
                listBox1.Items.Add("Kasım Ayı Toplam: " + kasım.ToString());
                listBox1.Items.Add("Aralık Ayı Toplam: " + aralık.ToString());
            }
            else
            {
                button1.Enabled = true;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                listBox1.Visible = false;
                button8.Text = "Bağış Kaydet";
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                label1.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
                label8.Enabled = true;
                textBox8.Enabled = true;
                textBox7.Enabled = true;
                textBox6.Enabled = true;
                textBox5.Enabled = true;
                textBox4.Enabled = true;
                textBox3.Enabled = true;
                comboBox2.Enabled = true;
                comboBox4.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
                comboBox3.Visible = false;
                textBox12.Visible = false;
                comboBox1.Visible = false;
                label23.Visible = false;
                label22.Visible = false;

                doldur();
                ogrnodoldur();
            }
        }

        private void bağlantıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.setts = textBox10.Text;
            Settings1.Default.Save();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            listBox1.Visible = false;
            button8.Text = "Bağış Kaydet";
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;
            label5.Enabled = true;
            label6.Enabled = true;
            label7.Enabled = true;
            label8.Enabled = true;
            textBox8.Enabled = true;
            textBox7.Enabled = true;
            textBox6.Enabled = true;
            textBox5.Enabled = true;
            textBox4.Enabled = true;
            textBox3.Enabled = true;
            comboBox2.Enabled = true;
            comboBox4.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            dateTimePicker3.Enabled = true;
            dateTimePicker4.Enabled = true;
            comboBox3.Visible = false;
            textBox12.Visible = false;
            comboBox1.Visible = false;
            label23.Visible = false;
            label22.Visible = false;
            doldur();
        }

        private void textBox11_MouseClick(object sender, MouseEventArgs e)
        {

            
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            
            
                
            }

        private void sınıflarıTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
            
        }

      
        
    }

