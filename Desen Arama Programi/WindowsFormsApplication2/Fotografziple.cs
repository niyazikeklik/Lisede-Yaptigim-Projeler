using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using System.Threading;
using System.Net.NetworkInformation;
using aejw.Network;
using System.Net.Mail;
using System.Net;
namespace WindowsFormsApplication2
{
    public partial class Fotografziple : Form
    {
        public Fotografziple()
        {
            InitializeComponent();
        }


        OpenFileDialog excelac = new OpenFileDialog();
        FolderBrowserDialog save = new FolderBrowserDialog();
        DirectoryInfo di2 = new DirectoryInfo(@"O:\");

        char degiscek; string konumcuk = ""; int say = 0;


        public static string InnerTrim(string input)
        {
            return input.Trim().Replace(" ", string.Empty);
        }
        void fordongusu()
        {
            progressBar1.Visible = true;
            label1.Visible = true;
            if (radioButton1.Checked)
            {

                try
                {
                    label1.Text = "Bekleyiniz.";
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\loading.gif");
                    FileInfo[] files2 = di2.GetFiles("*.jpg", SearchOption.AllDirectories);
                    progressBar1.Value = 0;
                    progressBar1.Maximum = dataGridView1.Rows.Count + 20;

                    label1.Text = "Resimler işleniyor, çekiliyor..";




                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            File.Copy("R:\\Katalog\\desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg", save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                            say++;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            progressBar1.Value++;
                            try
                            {
                                if (progressBar1.Value % 17 == 0)
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                }
                            }
                            catch (Exception)
                            {

                                ;
                            }
                            continue;
                        }
                        catch (FileNotFoundException)
                        {
                            try
                            {
                                File.Copy("R:\\Katalog\\çıkan desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg", save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                                say++;
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                progressBar1.Value++;
                                try
                                {
                                    if (progressBar1.Value % 17 == 0)
                                    {
                                        dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                    }
                                }
                                catch (Exception)
                                {

                                    ;
                                }
                                continue;

                            }
                            catch (FileNotFoundException)
                            {
                                bool varmi = false;
                                try
                                {
                                    foreach (FileInfo fi2 in files2)
                                    {

                                        if (fi2.Name == dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg")
                                        {

                                            File.Copy(fi2.FullName, save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                                            say++;
                                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                            progressBar1.Value++;
                                            varmi = true;
                                            try
                                            {
                                                if (progressBar1.Value % 17 == 0)
                                                {
                                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                                }
                                            }
                                            catch (Exception)
                                            {

                                                ;
                                            }
                                            break;
                                        }



                                    }


                                    if (varmi == false)
                                    {
                                        listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + " Bulunamadı Hatası,Yazım Hatası olabilir belki.");
                                        textBox4.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                                        progressBar1.Value++;
                                        try
                                        {
                                            if (progressBar1.Value % 17 == 0)
                                            {
                                                dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            ;
                                        }



                                    }

                                }


                                catch (Exception ss22ss)
                                {

                                    listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + ss22ss.Message);
                                    textBox7.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                    progressBar1.Value++;
                                    try
                                    {
                                        if (progressBar1.Value % 17 == 0)
                                        {
                                            dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        ;
                                    }

                                }
                            }
                            catch (Exception s332)
                            {
                                listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + s332.Message);
                                textBox7.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                progressBar1.Value++; try
                                {
                                    if (progressBar1.Value % 17 == 0)
                                    {
                                        dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                    }
                                }
                                catch (Exception)
                                {

                                    ;
                                }
                                continue;

                            }
                        }
                        catch (Exception SSSSSW)
                        {
                            listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + SSSSSW.Message);
                            textBox7.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            progressBar1.Value++; try
                            {
                                if (progressBar1.Value % 17== 0)
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                }
                            }
                            catch (Exception)
                            {

                                ;
                            }
                            continue;

                        }
                    }
                    ziple();
                }
                catch (DirectoryNotFoundException ex)
                {
                    try
                    {
                        label1.BackColor = Color.Red;
                        label1.ForeColor = Color.White;
                        label1.Text = "Ağ sürücüsüne bağlanılmaya çalışılıyor. "+Environment.NewLine+"İşlem biraz uzun sürebilir, bir seferliğine bekleyin." ;


                        try
                        {
                        NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                        oNetDrive.Force = true;
                        oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                        oNetDrive.LocalDrive = "O"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                        oNetDrive.ShareName = @"\\192.168.2.253\İhracat Ortak\file\DESENLER"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                        oNetDrive.SaveCredentials = false;
                        oNetDrive.MapDrive(textBox5.Text);
                        oNetDrive.MapDrive(textBox6.Text);
                        oNetDrive = null;
                        }
                        catch (Exception)
                        {

                            NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                            oNetDrive.Force = true;
                            oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                            oNetDrive.LocalDrive = "O"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                            oNetDrive.ShareName = @"\\nsa220\İhracat Ortak\file\DESENLER"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                            oNetDrive.SaveCredentials = false;
                            oNetDrive.MapDrive(textBox5.Text);
                            oNetDrive.MapDrive(textBox6.Text);
                            oNetDrive = null;
                        } 
                        MessageBox.Show("Ağ sürücüsünde O:\\\\ Yoluna otomatik bağlanıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label1.BackColor = SystemColors.ActiveBorder;
                        label1.ForeColor = DefaultForeColor;
                        hatamailigönder("Fotoğraf Zipleme Otomatik ağ yoluna bağlanıldı., ", "Fotoğraf Zipleme");
                        Thread baslat2 = new Thread(new ThreadStart(fordongusu));
                        baslat2.Start();

                    }
                    catch (Exception ex12)
                    {
                        hatamailigönder("Fotoğraf Zipleme Otomatik Ağ sürücüsüne Bağlanırken('O'), " + ex12.Message, "Fotoğraf Zipleme");
                        if (MessageBox.Show("Bir sorun nedeniyle ağ sürücüsü O:\\\\yoluna otomatik bağlanılamadı. Manuel olarak bağlanmayı deneyebilir. Yöneticiden destek alabilir veya Standart kullanıcıyı seçerek daha az kapsamlı olarak resim taraması yaptırabilirsiniz." + Environment.NewLine + "Standart kullanıcı modunda sadece güncel katalogda hatasız arama yapmak istiyor musunuz? (Bazı resimler el ile bulunmak zorunda kalınılabilir.)" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Hata Mesajı: " + ex12.Message, "Sorun", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            radioButton2.Checked = true;
                            radioButton1.Checked = false;
                            Thread baslat2 = new Thread(new ThreadStart(fordongusu));
                            baslat2.Start();
                        }
                        else
                        {
                            Hide();
                            Fotografziple yeni = new Fotografziple();
                            yeni.ShowDialog();
                        }
                    }

                }
                catch (Exception ss)
                {
                    hatamailigönder("Fotoğraf Zipleme fordongusu genel try kontrolü. İhracat Tarama, " + ss.Message, "Fotoğraf Zipleme");
                    MessageBox.Show("1.Hata Mesajı:" + Environment.NewLine + ss.Message + Environment.NewLine + Environment.NewLine + "Erişim hatası alırsanız standart kullanıcıyı seçiniz.");
                }
               
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (radioButton2.Checked)
            {
                label1.Text = "Bekleyiniz.";
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\loading.gif");

                progressBar1.Value = 0;
                progressBar1.Maximum = dataGridView1.Rows.Count + 20;
                label1.Text = "Resimler işleniyor, çekiliyor..";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        File.Copy("R:\\Katalog\\desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg", save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                        say++;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        progressBar1.Value++;
                        try
                        {
                            if (progressBar1.Value % 17 == 0)
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                            }
                        }
                        catch (Exception)
                        {

                            ;
                        }
                        continue;
                    }
                    catch (FileNotFoundException)
                    {
                        try
                        {
                            File.Copy("R:\\Katalog\\çıkan desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg", save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                            say++;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            progressBar1.Value++;
                            try
                            {
                                if (progressBar1.Value % 17 == 0)
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                }
                            }
                            catch (Exception)
                            {

                                ;
                            }
                            continue;

                        }
                        catch (FileNotFoundException)
                        {
                            listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + " Bulunamadı Hatası,Yazım Hatası olabilir belki.");
                            textBox4.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                            progressBar1.Value++;
                            try
                            {
                                if (progressBar1.Value % 17 == 0)
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                }
                            }
                            catch (Exception)
                            {
                                ;
                            }
                        }
                        catch (Exception eeee2)
                        {
                            listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + eeee2.Message);
                            textBox7.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            progressBar1.Value++; try
                            {
                                if (progressBar1.Value % 17 == 0)
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                                }
                            }
                            catch (Exception)
                            {

                                ;
                            }
                            continue;
                        }
                    }
                    catch (Exception eeee2)
                    {
                       
                        listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + eeee2.Message);
                        textBox7.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;

                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        progressBar1.Value++; try
                        {
                            if (progressBar1.Value % 17 == 0)
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex += 20;
                            }
                        }
                        catch (Exception)
                        {

                            ;
                        }
                        continue;
                    }
                }
                button1.Visible = true;
                button8.Visible = false;
                ziple();

            }
            button7.Enabled = true;

        }







        void ziple()
        {
            button1.Visible = false;
            button8.Visible = true;
            try
            {
                if (MessageBox.Show("Klasörlenen dosyaların ziplenmesini ister misiniz? Dosya sayısı 150den fazla ise uzun süreceğinden dolayı önerilmez.", "Zipleme", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath+"\\zip.png");
                    ZipFile zip = new ZipFile();
                    label1.Text = "Zip dosyası oluşturuluyor. " + Environment.NewLine + "Bu işlem biraz bekletebilir.";
                    zip.AddDirectory(save.SelectedPath + "\\" + textBox3.Text);
                    if (textBox3.Text != "")
                    {
                        zip.Save(save.SelectedPath + "\\" + textBox3.Text + "\\" + textBox3.Text + ".zip");
                    }

                    progressBar1.Value = progressBar1.Maximum;
                    label1.Text = "İşlem Tamammlandı.";
                    MessageBox.Show("Toplam: " + say + " dosya klasörlendi. İşlem Tamammlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
                    dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    label1.Text = "Zip dosyası istenmedi.";
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
                    progressBar1.Value = progressBar1.Maximum;
                    label1.Text = "İşlem Tamammlandı.";
                    MessageBox.Show("Toplam: " + say + " dosya klasörlendi. İşlem Tamammlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                }
            }
            catch (Exception ex)
            {

                hatamailigönder("Fotoğraf Zipleme,zipleme işlemi yapılırken, " + ex.Message, "Fotoğraf Zipleme");


            }
            button1.Visible = true;
            button8.Visible = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < listBox1.Items.Count; i++)
            {
                Clipboard.SetText(listBox1.Items[i].ToString().Substring(0, 6));
            }
        }
        /////////////////////////////////////////////////
        private void button4_Click_1(object sender, EventArgs e)
        {
           
            try
            {
                excelac.Filter = "Excel Dosyası|*.xls*| Tüm Desenler|*.*";
                excelac.FilterIndex = 1;
                if (excelac.ShowDialog() == DialogResult.OK)
                {
                    konumcuk = excelac.FileName;
                    if (checkBox1.Checked)
                    {
                        Settings1.Default.konumex = konumcuk;
                        Settings1.Default.Save();
                    }
                    OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + konumcuk + "; Extended Properties='Excel 12.0 Xml;HDR=YES'"); //excel_dosya.xlsx kısmını kendi excel dosyanızın adıyla değiştirin.
                    DataTable tablo = new DataTable();
                    xlsxbaglanti.Open();
                    tablo.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", xlsxbaglanti);
                    da.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                    xlsxbaglanti.Close();
                    button1.Enabled = true;
                    label6.Text = "Listelenen Toplam Ürün Sayısı:" + dataGridView1.Rows.Count.ToString();
                }
            }
            catch (OleDbException)
            {

                MessageBox.Show("Seçilen veritabanının sayfa isminin Sayfa1 şeklinde olduğuna ve verilerin 1. Sütunda Olduğuna ve ilk satırda Ürün Kodları(vb.) gibi bir başlık olduğuna emin olunuz. Exceli kapatıp tekrar deneyiniz.", "Excel Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                hatamailigönder("Fotoğraf Zipleme Veritabanı seçerken (excel tablosu), " + ex.Message, "Fotoğraf Zipleme");
                MessageBox.Show(ex.Message);

            }
            
        }
        void hatamailigönder(string hatamesaji, string baslik)
        {
            try
            {

                string ipAdresi = Dns.GetHostByName(Environment.MachineName.ToString()).AddressList[0].ToString();
                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: " + ipAdresi;


                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject = baslik +" "+ Environment.UserName.ToString();
                mesaj.Body = "Hata Mesajı; " + Environment.NewLine + Environment.NewLine + hatamesaji + Environment.NewLine + istekbilgi;
                NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                client.Credentials = guvenlik;
                client.EnableSsl = true;
                client.Send(mesaj);


            }
            catch (Exception ex)
            {
                ;
            }
        }



        private void Fotografziple_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            progressBar1.Visible = false;
            checkBox1.Checked = Settings1.Default.konumcheck;
            button8.Visible = false;
            textBox5.Text = Settings1.Default.ihrid;
            textBox6.Text = Settings1.Default.ihrpas;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
            try
            {
                OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Settings1.Default.konumex + "; Extended Properties='Excel 12.0 Xml;HDR=YES'"); //excel_dosya.xlsx kısmını kendi excel dosyanızın adıyla değiştirin.
                DataTable tablo = new DataTable();
                xlsxbaglanti.Open();
                tablo.Clear();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", xlsxbaglanti);
                da.Fill(tablo);
                dataGridView1.DataSource = tablo;
                xlsxbaglanti.Close();
                button1.Enabled = true;
                label6.Text = "Listelenen Toplam Ürün Sayısı:" + dataGridView1.Rows.Count.ToString();
                textBox3.Focus();
            }
            catch (Exception)
            {
                ;
            }
            if (textBox6.Text=="İhracat Şifre")
            {
                textBox6.PasswordChar = '\0';
            }
            else
            {
                textBox6.PasswordChar = '*'; 
            }
         
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(textBox2.Text, "");
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                degiscek = Convert.ToChar(textBox1.Text);

            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(degiscek, '-');
            }
        }
        Thread baslatt2;
        private void button1_Click_1(object sender, EventArgs e)
        {
          
            button7.Enabled = false;
            label1.BackColor = SystemColors.ActiveBorder;
            label1.ForeColor = DefaultForeColor;
            try
            {
                say = 0; listBox1.Items.Clear();
                textBox4.Text = "";
                textBox7.Text = "";
                listBox1.Items.Add("Ürün" + "    |    " + "Hata Raporu");
                listBox1.Items.Add("--------------------------------------------------------------------------------------------------------");
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Zip dosyası ismi girmediniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    label1.Text = "Kaydedilecek konum bekleniyor..";
                    save.Description = "Kaydedilecek konumu seçiniz.";

                    if (save.ShowDialog() == DialogResult.OK)
                    {

                        Directory.CreateDirectory(save.SelectedPath + "\\" + textBox3.Text);


                        try
                        {
                            baslatt2 = new Thread(new ThreadStart(fordongusu));
                            baslatt2.Start();
                            button8.Visible = true;
                            button1.Visible = false;
                            hatamailigönder("Fotoğraf klasörleme özelliği çalıştırıldı (KESİN)", "Özellik Kullanımı");
                        }
                        catch (Exception ex)
                        {
                            hatamailigönder("Fotoğraf Zipleme Thread baslat try kontrolü, " + ex.Message, "Fotoğraf Zipleme");

                            MessageBox.Show(ex.Message);
                        }



                    }
                }
              
            }
            catch (Exception ex)
            {
                hatamailigönder("Fotoğraf Zipleme Konum Seçerken İlk Try kontrolü, " + ex.Message, "Fotoğraf Zipleme");
                listBox1.Items.Add(ex.ToString());
            }

        }

        private void Fotografziple_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void Fotografziple_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            fordongusu();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = InnerTrim(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings1.Default.ihrpas = textBox6.Text;
            Settings1.Default.ihrid = textBox5.Text;
            Settings1.Default.Save();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Ürün" + "    |    " + "Hata Raporu");
            listBox1.Items.Add("--------------------------------------------------------------------------------------------------------");
            textBox4.Text = "";
            textBox7.Text = "";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
            label1.Text = "Lütfen bir excel dosyası seçiniz.";
            dataGridView1.DataSource = "";
            textBox3.Text = "";
            progressBar1.Value = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
           
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
         
            baslatt2.Abort();
            listBox1.Items.Clear();
            listBox1.Items.Add("Ürün" + "    |    " + "Hata Raporu");
            listBox1.Items.Add("--------------------------------------------------------------------------------------------------------");
            textBox4.Text = "";
            textBox7.Text = "";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
            label1.Text = "Lütfen bir excel dosyası seçiniz.";
            dataGridView1.DataSource = "";
            textBox3.Text = "";
            progressBar1.Value = 0;
            button8.Visible = false;
            button1.Visible = true;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label9.Text="Diğer hata;("+(textBox7.Lines.Length-1).ToString()+")";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label8.Text = "Bulunamaya;(" + (textBox4.Lines.Length - 1).ToString() + ")";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(@"R:\\Katalog\desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString()+".jpg");
                
            }

            catch (Exception)
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ".jpg");
                }
                catch (Exception)
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\resimyok.jpg");
                    MessageBox.Show("Dosya konumu bulunamıyor.", "Bulanamıyor.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                   
               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
      
		
	
	

           
                try
                {
                    ressimbuyut git = new ressimbuyut();
                    git.pictureBox1.Image = Image.FromFile(@"R:\Katalog\desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString()+".jpg");

                    git.ShowDialog();
                }
                catch (Exception)
                {
                    try
                    {
                        ressimbuyut git = new ressimbuyut();
                        git.pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ".jpg");

                        git.ShowDialog();


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tekrar Desen Seçiniz Veya Yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\resimyok.jpg");
                    }


                }

            }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
           
              
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(@"R:\\Katalog\desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ".jpg");

            }

            catch (Exception)
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ".jpg");
                }
                catch (Exception)
                {

                    MessageBox.Show("Dosya konumu bulunamıyor.", "Bulanamıyor.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                Settings1.Default.konumcheck = true;
            }
            else
            {
                Settings1.Default.konumcheck = false;
            }
            Settings1.Default.Save();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings1.Default.konumex = "";
            Settings1.Default.Save();

            listBox1.Items.Clear();
            listBox1.Items.Add("Ürün" + "    |    " + "Hata Raporu");
            listBox1.Items.Add("--------------------------------------------------------------------------------------------------------");
            textBox4.Text = "";
            textBox7.Text = "";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\A7VhUP0CUAEwR_y.png");
            label1.Text = "Lütfen bir excel dosyası seçiniz.";
            dataGridView1.DataSource = "";
            textBox3.Text = "";
            progressBar1.Value = 0;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "İhracat Şifre")
            {
                textBox6.PasswordChar = '\0';
            }
            else
            {
                textBox6.PasswordChar = '*';    
            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text=="İhracat Şifre")
            {
                textBox6.Text = "";
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text=="İhracat İD")
            {
                textBox5.Text = "";
            }
        }
        }
    }

