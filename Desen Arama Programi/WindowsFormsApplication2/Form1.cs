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
    using System.Threading;
    using System.Diagnostics;
    using System.Collections;
    using System.Net;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Net.Mail;
    using aejw.Network;

    namespace WindowsFormsApplication2
    {

        public partial class Form1 : Form
        {
            public string surum = "SurumV7.txt";

            public Form1()
            {

                CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();
              

       
            }

            //GLOBALLER
          
      Rectangle ClientCozunurluk = new Rectangle();



            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=R:\\Katalog\\desenler\\desen_veritabanı.accdb");
            ArrayList secilenler = new ArrayList(32);
            OleDbDataAdapter da;
            OleDbCommand dm;
            DataTable tbl;
            ressimbuyut r2;
            void duzenlemebekleyenler()
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Düzenleme Bekliyor")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Settings1.Default.Darkaplan;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor =Settings1.Default.Dyazırengi;
                    }
                else if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Çıkan Desen")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Settings1.Default.Carkaplan;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Settings1.Default.Cyazırengi;
                }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Settings1.Default.Garkaplan;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Settings1.Default.Gyazi;
                    }

              
                }
            }
            private void dosyatasi()
            {
                try
                {
                    int sayyy = 0;
                    bool varmi;
                    DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\desenler");
                    FileInfo[] files = di.GetFiles("*.jpg");
                    Directory.CreateDirectory(secilenyer + "\\Taşınan Desenler");
                    foreach (FileInfo fi in files)
                    {
                        label9.Text = fi.Name + " Karşılaştırılıyor... ";
                        progressBar1.Value++;
                        varmi = false;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {

                            if (dataGridView1.Rows[i].Cells[0].Value + ".jpg" == fi.Name)
                            {
                                varmi = true;
                            }
                        }
                        if (varmi == false)
                        {

                            File.Move("R:\\Katalog\\desenler\\" + fi.Name, secilenyer + "\\Taşınan Desenler\\" + fi.Name);
                            sayyy++;
                        }
                    }
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                    label9.Text = "";
                    label9.Visible = false;
                    button38.Visible = true;
                    button42.Visible = false;
                    button42.Enabled = false;
                    MessageBox.Show("Toplam: " + sayyy + " Fotoğraf" + secilenyer + " konumuna taşınmıştır.", "Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata Mesajı;"+Environment.NewLine+ex.Message, "Hatta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hatamailigönder("Dosya Taşıma Özelliği Kullanırken, " + ex.Message, " Dosyaları taşırken.");
                }
            }
    private void UpdateKontrol()
            {

                if (File.Exists("R:\\Katalog\\Desen Arama\\" + surum))
                {
                    MessageBox.Show("Programınız Güncel.", "Güncel Sürüm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    linkLabel1.Visible = false;
                }
                else
                {
                    linkLabel1.Visible = true;

                    if (MessageBox.Show("Programınız güncel değildir yeni sürüm mevcut. Yüklemek ister misiniz? Çok kısa sürücek.", "Olumsuz", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        try
                        {
                            string myPath = @"R:\Katalog\Desen Arama\Guncelle\guncelle.exe";
                            System.Diagnostics.Process prc = new System.Diagnostics.Process();
                            prc.StartInfo.FileName = myPath;
                            prc.Start();
                            Application.Exit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            hatamailigönder("Güncelleem kontrolü yapılırken , " + ex.Message, "Güncelleme denetlenirken.");
                        }
                    }
                }
            }
            int gridsatirsayisi = 0, kackerecalisti = 0, yenikayit = 0;
            string resimadii = "", cumle, konum = "R:\\Katalog\\desenler\\";
            bool goster2 = true, goster = true;
            bool birak = false; bool calistimi32 = false;
            void aramaislemleri()
            {
            try
            {
                yeniurunkontrol.Abort();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label9.Text = "";
            }
            catch (Exception)
            {
                ;
            }
                if (comboBox1.Text == "Tüm Desenler")
                {
                    cumle = "Select * from desenler where kod like '" + textBox1.Text + "%' ";
                }
                else
                {
                    cumle = "Select * from desenler where kod like '" + textBox1.Text + "%' and firma like '" + comboBox1.Text + "'";
                }
            tbl.Clear();
                tbl = new DataTable();
                da = new OleDbDataAdapter(cumle, con);
                da.Fill(tbl);
            if (checkBox10.Checked==true)
            { 
            DataColumn[] col = new DataColumn[3];
            DataRow row;
                    DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\çıkan desenler\\");
                    FileInfo[] files = di.GetFiles("*.jpg");

                    string yol = "";
                    foreach (FileInfo fi in files)
                    {
                     
                        if (fi.Name.Substring(0, textBox1.TextLength) == textBox1.Text)
                        {

                            row = tbl.NewRow();
                            row["kod"] = fi.Name.ToString();
                            row["firma"] = "Katalog Dışı";
                            row["durum"] = "Çıkan Desen";

                            tbl.Rows.Add(row);

                        }
                    }
                  
                
            
          

            }

                 dataGridView1.DataSource = tbl;
                label8.Text = dataGridView1.Rows.Count.ToString();
            
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                dataGridView1.CurrentCell = null;

                 if (checkBox5.Checked == false)
                    {
                        if (textBox1.Text.Length > 0)
                        {
                            pictureBox6.Visible = true;

                        }
                        else
                        {
                            pictureBox6.Visible = false;
                        }
                if (dataGridView1.Rows.Count == 0)
                {
                    textBox1.BackColor = Color.IndianRed;
                    textBox1.ForeColor = Color.White;
                }
                else if (dataGridView1.Rows.Count >0)
                {
                   
                    duzenlemebekleyenler();
                    
                    textBox1.BackColor = Color.White;
                    textBox1.ForeColor = Color.Black;
                }


                if (textBox1.Text.Length == 0)
                {
                    textBox1.BackColor = Color.White;
                    textBox1.ForeColor = Color.Black;
                }
                    }
               
            
            }
            void ototemizlik()
            {
                DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\desenler");
                int var = 0; int kactanesilindi = 0;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                FileInfo[] files = di.GetFiles("*.jpg");
                listBox2.Items.AddRange(files);
                progressBar1.Maximum = dataGridView1.Rows.Count;
                con.Open();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    progressBar1.Value += 1;
                    bool calistimi = false;
                    for (int j = 0; j < listBox2.Items.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg" == listBox2.Items[j].ToString())
                        {

                            label9.Text = progressBar1.Maximum + "/" + progressBar1.Value + " Deneniyor...";
                            calistimi = true; ;
                            var++;
                        }
                    }
                    if (calistimi == false)
                    {
                        try
                        {
                            kactanesilindi++;
                            dm = new OleDbCommand("DELETE FROM desenler WHERE kod=@desenkod", con);
                            dm.Parameters.AddWithValue("@desenkod", dataGridView1.Rows[i].Cells[0].Value.ToString());
                            dm.ExecuteNonQuery();
                        }
                        catch (OleDbException)
                        {

                            MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                con.Close();
                if (kactanesilindi == 0)
                {
                    MessageBox.Show("Çıkan desen bulunamadı.", "Güncel.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (kactanesilindi > 0)
                {
                    MessageBox.Show("Toplam Silinen Kayıt : " + kactanesilindi.ToString() + " Yenile Butonuna Basınız.", "Güncel.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                progressBar1.Visible = false;
                progressBar1.Value = 0;
                label9.Text = "";
                button9.Visible = true;
                button40.Visible = false;
            }
          
            void guncelle()
            {
                DateTime tarih = new DateTime();
            
                try
                {
                    int dosyaSayisi = 0;
                    dosyaSayisi = Directory.GetFiles("R:\\Katalog\\desenler", "*.jpg", SearchOption.AllDirectories).Length;
                    int yeniurun = 0, zatenkayitli = 0;

                    zatenkayitli = 0;
                    progressBar1.Value = 0;
                    int iyebak = 0;
                    bool varmiii = false;
                  
                    DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\desenler");
                    FileInfo[] files = di.GetFiles("*.jpg");
                    progressBar1.Visible = true;
                    progressBar1.Maximum = dosyaSayisi + 1;
                    dm = new OleDbCommand();
                    con.Open();
                    dm.Connection = con;
                    foreach (FileInfo fi in files)
                    {
                        try
                        {
                          
                            dm.CommandText = "insert into desenler (durum,kod,images,firma,eklenmezamani) values ('" + "Düzenleme Bekliyor" + "','" + fi.Name.Substring(0, (fi.Name.Length - 4)) + "','" + fi.Name + "','" + "Firma Yok" + "','" + DateTime.Now.ToString() +"')";
                            dm.ExecuteNonQuery();
                            yeniurun++;
                        }
                        catch (OleDbException)
                        {
                            
                            zatenkayitli += 1;
                        }

                        progressBar1.Value += 1;
                        label9.Text = dosyaSayisi + "/" + progressBar1.Value + " İşleniyor";
                    }

                    con.Close();

                    if (yeniurun == 0)
                        MessageBox.Show("Dosyalarınız Kontrol edildi. Yeni Ürün Kayıdı bulunamadı.", "Yeni kayıt yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (yeniurun >= 1)
                        MessageBox.Show("Toplam " + yeniurun + " Yeni kayıt vardır. Lütfen güncellemeyi unutmayınız. Yenile Butonuna Basınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Visible = false;
                    button4.Visible = true;
                    button39.Visible = false;
                    button39.Enabled = false;
                    label9.Text = "";
                    zatenkayitli = 0;
                    progressBar1.Value = 0;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK); con.Close();
                hatamailigönder("Ürün Güncellemesi yapılırken , " + ex.Message, "Veritabanı  güncellemesi");
                }

            }
      
            void yeniurunkontroıl()
            {
                try
                {
  int dosyaSayisi = 0;
                dosyaSayisi = Directory.GetFiles("R:\\Katalog\\desenler", "*.jpg", SearchOption.AllDirectories).Length;
                DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\desenler");
                FileInfo[] files = di.GetFiles("*.jpg");
                progressBar1.Value = 0;
                progressBar1.Maximum = dosyaSayisi;
                progressBar1.Visible = true;
                label9.Text = "Güncellemeler Kontrol Ediliyor.";
                bool varmiii = false; int sayac = 0;
                foreach (FileInfo fi in files)
                {
                    
                try
                {
                    progressBar1.Value++;
                    varmiii = false;
                    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (fi.Name == dataGridView1.Rows[i].Cells[1].Value.ToString())
                        {
                            varmiii = true;
                            break;
                        }
                    }
                    if (varmiii == false)
                    {
                        sayac++;
                    }
                }
                catch (Exception)
                {

                    ;
                }
                   
                }
                if (sayac > 0)
                {
                    MessageBox.Show(sayac + " Yeni Ürün güncellemesi mevcut. Lütfen kontrol ediniz veya yöneticiye (Saffet Mahmutoğlu) ulaşınız.", "Yeni Ürün Güncellemesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               hatamailigönder(sayac+" Yeni Ürün Güncellemesi Var.","Yeni Ürün Güncellemesi");

                }


                progressBar1.Value = 0;
                progressBar1.Maximum = dosyaSayisi + 3;
                progressBar1.Visible = false;
                label9.Text = "";
           
                }
                catch (Exception ex)
                {

                    hatamailigönder("Yeni ürün kontrolü yapılırken, " + ex.Message, "Ürün kontrolü ");
                }
               
              

            }
            void cikanlarilistele()
            {

                try
                {
                    int satir = 0;
                    if (radioButton2.Checked)
                    {

                        DirectoryInfo di = new DirectoryInfo(konum);
                        FileInfo[] files = di.GetFiles("*.jpg");

                        string yol = "";
                        foreach (FileInfo fi in files)
                        {

                            dataGridView2.Rows.Add(fi.Name);
                            dataGridView2.Rows[satir].Cells[1].Value = "Çıkan Desen";
                            string zamann = fi.CreationTime.Month.ToString();

                            if (zamann == "1") { zamann = " Ocak "; }
                            else if (zamann == "2") { zamann = " Şubat "; }
                            else if (zamann == "3") { zamann = " Mart "; }
                            else if (zamann == "4") { zamann = " Nisan "; }
                            else if (zamann == "5") { zamann = " Mayıs "; }
                            else if (zamann == "6") { zamann = " Haziran "; }
                            else if (zamann == "7") { zamann = " Temmuz "; }
                            else if (zamann == "8") { zamann = " Ağustos "; }
                            else if (zamann == "9") { zamann = " Eylül "; }
                            else if (zamann == "10") { zamann = " Ekim "; }
                            else if (zamann == "11") { zamann = " Kasım "; }
                            else if (zamann == "12") { zamann = " Aralık "; }
                            dataGridView2.Rows[satir].Cells[2].Value = fi.CreationTime.Day.ToString() + zamann + fi.CreationTime.Year.ToString();

                            satir++;

                        }
                        groupBox1.Enabled = false;
                        label8.Text = dataGridView2.RowCount.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    hatamailigönder("Çıkan desenler listelenirken, " + ex.Message, "Çıkan Desen Listele");
                }


            }
            void kontrolcu()
            {
                try
                {
                    int dosyaSayisi = 0;
                    dosyaSayisi = Directory.GetFiles("R:\\Katalog\\desenler", "*.jpg", SearchOption.AllDirectories).Length;
                    bool varmiii = true;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    label9.Visible = true;
                    int sayac = 0;
                    DirectoryInfo di = new DirectoryInfo("R:\\Katalog\\desenler");
                    FileInfo[] files = di.GetFiles("*.jpg");
                    progressBar1.Maximum = dosyaSayisi + 3;
                    foreach (FileInfo fi in files)
                    {
                        label9.Text = fi.Name + " Eşleştiriliyor.";
                        progressBar1.Value++;
                        varmiii = false;
                        for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {


                            if (fi.Name == dataGridView1.Rows[i].Cells[1].Value.ToString())
                            {

                                varmiii = true;

                                break;
                            }


                        }
                        if (varmiii == false)
                        {
                            sayac++;
                            MessageBox.Show(fi.Name + " Ürünü yeni bir güncelleme yada katalog\\desenler içerisinde bulunan fakat veritabanında bulunmayan bir ürün. Bu butona tıkladığınıza göre bir sorun yaratıyor gibi gözüküyor.  " + Environment.NewLine + Environment.NewLine + "İlk olarak dosya olarak kontrolünü gerçekleştiriniz(ismine,boşluklara vb.)" + Environment.NewLine + Environment.NewLine + "Daha sonra sorunu bulamazsanız veritabananında kontrol ediniz. " + Environment.NewLine + Environment.NewLine + "3. çözüm olarak veritabananında ürün zaten kayıtlı gözüküyorsa sil butonuyla silip tekrar veritabanı güncelleme yapınız." + Environment.NewLine + Environment.NewLine + "Son olarak ise katalog\\desenler klasörününde dosyanın benzer isimde bir eşi olup olamdığını kontrol ediniz. bu sorunu çözebilirsiniz size güveniyoruz. ", "Ürün Eşleşmedi veritabanında yok. Bu Ürün Güncelleme Yapılmaya Çalışıyor.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }


                    }
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                    button14.Visible = true;
                    button41.Visible = false;
                    button41.Enabled = false;
                    label9.Text = "";
                    sayac = 0;
                    if (sayac == 0)
                    {
                        MessageBox.Show("Herhangi bir sorun gözükmüyor her şey yolunda.", "İşleminiz tamamlanmıştır.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    hatamailigönder("Yeni ürün kontrolü yapılırken, " + ex.Message, "Ürün kontrolü ");
                    
                }
          
            }
            void griddoldur()
            {
            try
            {

                da = new OleDbDataAdapter("Select *from desenler", con);
                tbl = new DataTable();
                da.Fill(tbl);
                dataGridView1.DataSource = tbl;
                label8.Text = dataGridView1.Rows.Count.ToString();
                dataGridView1.Columns[0].HeaderText = "Kod";
                dataGridView1.Columns[2].HeaderText = "Firma";
                dataGridView1.Columns[3].HeaderText = "Durum";
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                dataGridView1.CurrentCell = null;

                if (checkBox5.Checked == false)
                {
                    duzenlemebekleyenler();
                }
            }
            catch (OleDbException eee)
            {
               
                try
                {
                    NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                    oNetDrive.Force = true;
                    oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                    oNetDrive.LocalDrive = "R"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                    if (kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked ==false)
                    {
                        oNetDrive.ShareName = @"\\192.168.2.253\public"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    }
                    else
                    {
                        oNetDrive.ShareName = @"\\nsa220\public"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    }
                  
                    oNetDrive.SaveCredentials = false;
                    oNetDrive.MapDrive();
                    oNetDrive = null;
                    try
                    {
                        yenile();                   
                        hatamailigönder("Grid listelerken, ama korkma yazdığın kodlar ile otomatik çözüldü. " + eee.Message, "Grid Listelerken ");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Eğer hata mesajı içeriği ''Yerel Makine Kayıtlı Değil'' şeklinde ise ''Gereken Programlar'' Klasöründeki 1. Programı Kurunuz. Programın 32bit sürümünü kullanınız çalışmaz ise 64bit deneyiniz" + Environment.NewLine + Environment.NewLine + "Veya R:\\Katalog\\ dizininin \\\\nsa220\\public konumuna bağlı olduğuna emin olunuz.", "Bağlantı Sorunu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        hatamailigönder("Grid Listelerken (nsa'ya otomatik bağlanma kodlarından sonra yenile tryında), " + ex.Message, "2.Grid Listeleme");
                    
                    }
                }
                catch (Exception ex2) {
                MessageBox.Show( ex2.Message+",Nsa'ya otomatik bağlanılamadı. Manuel olarak R: sürücüsü olarak bağlantı kurabilir veya program içerisinde 'Ağ Sürücüsüne Bağlan' Sekmesi altında işaretli kutucuğu değiştirdikten sonra Katalog(nsa220\\public)  özelliğini çalıştırarak otomatik bağlanabilirsiniz..","Ağ yolu bulunamıyor.",MessageBoxButtons.OK,MessageBoxIcon.Information);
                hatamailigönder("Gris Listeleme yapılırken, (nsa ya otomatik bağlanma kodlarında) " + ex2.Message, "3.Grid listeleme");
                }
              
            }
            catch (Exception ex2) {
                MessageBox.Show("Hata Mesajı;" + Environment.NewLine + ex2.Message + Environment.NewLine + Environment.NewLine + "Hata Mesajı ''Yerel Makine Kayıtlı Değil'' şeklinde ise ''Gereken Programlar'' bölümünden 1. programı kurunuz2." + Environment.NewLine + ex2.Message); ;

            hatamailigönder("Grid listelerken çözülemeyen bilinmeyen hata genel try kontrolüne takılan hata maili. , " + ex2.Message, "1.Grid Listeleme");
            }
          

        }
            void yenile()
            {
                Random rnd = new Random();
                int gecici = rnd.Next(0, 12);
                string istenilen = dizi[gecici];



                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + istenilen);
            checkBox10.Checked = false;
                radioButton1.Checked = true;
                dataGridView1.Visible = true;
                konum = "R:\\Katalog\\desenler\\";
                pictureBox1.Enabled = false;
                button8.Enabled = false;
              
                textBox1.Focus();
                griddoldur();
            }
            void combo1doldur()
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (comboBox1.Items.IndexOf(dataGridView1.Rows[i].Cells[2].Value) == -1)
                    {
                        comboBox1.Items.Add(dataGridView1.Rows[i].Cells[2].Value);
                    }
                }
            }
            private void pictureBox1_Click(object sender, EventArgs e)
            {
         

            if (checkBox6.Checked==true)
                    {
                        try
                        {
                            if (dataGridView1.CurrentRow.Cells[2].Value == "Katalog Dışı")
                            {
                                try
                                { 
                                   
                                    System.Diagnostics.Process.Start(@"R:\Katalog\çıkan desenler\");//ÇALIŞMIYOR, UĞRAŞMAK İSTEMEDİM GEREKSİZ
                                }
                                catch (Exception)
                                {

                                    System.Diagnostics.Process.Start(@"R:\Katalog\çıkan desenler\" + dataGridView2.CurrentRow.Cells[0].Value);
                                }
                               
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(konum + resimadii);
                            }
                      
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        } 
                     
                    }
                    else
                    {
                        try 
	                    {
                    if (checkBox1.Checked == false)
                    {
                        ressimbuyut git = new ressimbuyut();
                        git.pictureBox1.Image = Image.FromFile(konum + resimadii);
                        git.Width = pictureBox1.Image.Width + 5;
                        git.Height = pictureBox1.Image.Height + 5;
                        git.WindowState = FormWindowState.Normal;
                        git.ShowDialog();
                    }
                    else
                    {
                        ressimbuyut git = new ressimbuyut();
                        git.pictureBox1.Image = Image.FromFile(konum + resimadii);
                        git.WindowState = FormWindowState.Maximized;
                        git.ShowDialog();
                    }
                   
	                    }
                        catch (Exception)
                        {
                            try
                            {

                        if (checkBox1.Checked == false)
                        {
                            ressimbuyut git = new ressimbuyut();
                            git.pictureBox1.Image = Image.FromFile(konum + resimadii);
                            git.Width = pictureBox1.Image.Width + 5;
                            git.Height = pictureBox1.Image.Height + 5;
                            git.WindowState = FormWindowState.Normal;
                            git.ShowDialog();
                        }
                        else
                        {
                            ressimbuyut git = new ressimbuyut();
                            git.pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString());
                            git.WindowState = FormWindowState.Maximized;
                            git.ShowDialog();
                        }

                       
                    
                    
                            }
                            catch (Exception)
                            {
                              
                                    MessageBox.Show("Tekrar Desen Seçiniz Veya Yenileyiniz. Program desen dosyasını katalogda bulamıyor. Katalogdan çıkartılmış olabilir böyle bir durumda desen kaydını veritabanından da silmeniz gerekir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                               
                                
                            
                          

                        }
                       
                    }
              
               
            }
            private void button3_Click(object sender, EventArgs e)
            {
                yenile();
            }
            private void button1_Click(object sender, EventArgs e)
            {

                if (radioButton1.Checked) { aramaislemleri(); }
                else
                {
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        dataGridView2.Rows[i].Visible = false;
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString().Substring(0, textBox1.TextLength) == textBox1.Text)
                        {
                            dataGridView2.Rows[i].Visible = true;

                        }
                    }
                    label8.Text = dataGridView1.Rows.Count.ToString();
                }
            }
            private void iletişimToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Adı Soyadı : Niyazi Keklik" + Environment.NewLine + "1. E-posta: niyazikeklikk@gmail.com" + Environment.NewLine + "2. E-posta: niyazikeklik@gmail.com " + Environment.NewLine + "Telefon: +90 534 686 1675" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Pendik Yunus Emre Mesleki Ve Teknik Anadolu Lisesi Stajyeri. 2016/2017", "İletişim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  Thread baslat;
            private void button4_Click(object sender, EventArgs e)
            {
               if (MessageBox.Show("Sistem saatiniz; " + DateTime.Now.ToString() + "'dir. Eğer yanlışlık varsa düzelttikten sonra bu işlemi yapmanız çok önemlidir." + Environment.NewLine + Environment.NewLine + "Saatiniz ve Tarihinizin doğru olduğunu onaylıyor ve devam etmek istiyor musunuz?", "Okuyun Lütfen Dikkat !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    yenile();
                  
                    baslat = new Thread(new ThreadStart(guncelle));
                    baslat.Start();
                    button4.Visible = false;
                    button39.Visible = true;
                    button39.Enabled = true;
                    hatamailigönder("Veritabanını Güncelle özelliği çalıştırıldı.", "Özellik Kullanımı.");
                }
              
            }
            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
               
            }
            private void button2_Click_1(object sender, EventArgs e)
            {
                giris giris = new giris();
                giris.ShowDialog();
                this.Hide();
            }
            private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
            {
                timer1.Stop();
                Application.Exit();
            }
            private void desenV14ToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Desen Arama programı Niyazi Keklik tarafından Neşe Plastik için kodlanmıştır. Tüm hakları saklıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                timer1.Stop();
                Application.Exit();
            }
            private void yeniKayıtlaraGöreToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (azalanToolStripMenuItem.Checked == true)
                {
                  dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
                }
                else if (artanToolStripMenuItem.Checked==true)
                {
                  dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
                }
               
            }
            private void ürünKodunaGöreToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (azalanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                }
                else if (artanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                }
               
            }
            private void firmaİsmineGöreToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (azalanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
                }
                else if (artanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
                }
               
            }
            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    
                    pictureBox1.Image = Image.FromFile(konum + textBox1.Text+".jpg");
                    label3.Text = "Listeden deseni seçiniz.";
                    label5.Text = textBox1.Text;
                    resimadii = textBox1.Text + ".jpg";
                    pictureBox1.Enabled = true;
                  
                }
                catch (Exception)
                {

                    ;
                } 
                try
                {

                if (progressBar1.Value >10)
                {
                    try
                    {
                        yeniurunkontrol.Abort();
                        progressBar1.Value = 0;
                        progressBar1.Visible = false;
                        label9.Text = "";
                    }
                    catch (Exception)
                    {

                        ;
                    }
               
                }
               
                    if (radioButton1.Checked)
                    {
                        aramaislemleri();
                    }
                    else
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count - 1; i++) 
                        {

                            if (dataGridView2.Rows[i].Cells[0].Value.ToString().Substring(0, textBox1.TextLength) == textBox1.Text)
                            {

                                dataGridView2.Rows[i].Visible = true;
                            }
                            else
                            {
                                dataGridView2.Rows[i].Visible = false;
                            }
                        }

                    }
                    if (dataGridView1.Rows.Count == 0)
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\resimyok" + ".jpg");
                        label3.Text = "Desen Yok.";
                        label5.Text = "Desen Bulunamadı.";
                        resimadii = "resimyok" + ".jpg";
                    }
                

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }  
            }
            private void button5_Click_2(object sender, EventArgs e)
            {
               
           button17.Enabled =false;
            button38.Enabled = false;
                button9.Enabled = false;
                button5.Visible = false;
                button2.Visible = true;
                comboBox3.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                button4.Enabled = false;
                textBox2.Enabled = false;
                label10.Enabled = false;
                button11.Enabled = false;
                button14.Enabled = false;
                checkBox3.Enabled = false;
                checkBox2.Enabled = false;
                veritabanıToolStripMenuItem.Enabled = false;
            }
            private void button8_Click_1(object sender, EventArgs e)
            {
                try
                {
                    r2 = new ressimbuyut();
                    r2.Show();
                    r2.WindowState = FormWindowState.Maximized;
                    r2.pictureBox1.Image = Image.FromFile(konum + resimadii);
                    r2.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception) {
                    try
                    {
                     
                    r2 = new ressimbuyut();
                    r2.Show();
                    r2.WindowState = FormWindowState.Maximized;
                    r2.pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    r2.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tekrar Desen Seçiniz Veya Yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        r2.Close();
                    }
                    
                    
                 
                }
            }
            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                yeniurunkontrol.Abort();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label1.Text = "";
            }
            catch (Exception)
            {

                ;
            }
       
             aramaislemleri();
                
            }
            private void dataGridView1_SelectionChanged(object sender, EventArgs e)
            {
                dataGridView1.RowsDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            private void klasörAçToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    radioButton3.Checked = true;
                    dataGridView2.Visible = true;
                    dataGridView2.Rows.Clear();
                    folderBrowserDialog1.ShowNewFolderButton = true;
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    folderBrowserDialog1.Description = "Listelemek istediğiniz dosyaların klasörünü seçiniz.";
                    if (result == DialogResult.OK)
                    {
                        int satir = 0;
                        konum = folderBrowserDialog1.SelectedPath + "\\";
                        DirectoryInfo di = new DirectoryInfo(konum);

                        FileInfo[] files = di.GetFiles("*.jpg");
                    
                        dataGridView1.Visible = false;
                        foreach (FileInfo fi in files)
                        {


                            dataGridView2.Rows.Add(fi.Name);
                            dataGridView2.Rows[satir].Cells[1].Value = di.Name;
                            string zamann = fi.CreationTime.Month.ToString();

                            if (zamann == "1") { zamann = " Ocak "; }
                            else if (zamann == "2") { zamann = " Şubat "; }
                            else if (zamann == "3") { zamann = " Mart "; }
                            else if (zamann == "4") { zamann = " Nisan "; }
                            else if (zamann == "5") { zamann = " Mayıs "; }
                            else if (zamann == "6") { zamann = " Haziran "; }
                            else if (zamann == "7") { zamann = " Temmuz "; }
                            else if (zamann == "8") { zamann = " Ağustos "; }
                            else if (zamann == "9") { zamann = " Eylül "; }
                            else if (zamann == "10") { zamann = " Ekim "; }
                            else if (zamann == "11") { zamann = " Kasım "; }
                            else if (zamann == "12") { zamann = " Aralık "; }
                            dataGridView2.Rows[satir].Cells[2].Value = fi.CreationTime.Day.ToString() + zamann + fi.CreationTime.Year.ToString();
                            
                            satir++;
                            dataGridView2.Rows.Add(fi.Name);
                          
                        }
                        groupBox1.Enabled = false;
                    }
                    label8.Text = dataGridView2.RowCount.ToString();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata Mesajı :" + ex.Message);            }
           
            }
            private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Yüksek kartuş kullanımı dikkat !", "Dikkat", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                        printDocument1.Print();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Hata: "+ex.Message,"Sorun",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    }
                  
                
                

                }
            }
            private void dosyaAçToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OpenFileDialog file = new OpenFileDialog();
                file.InitialDirectory = "C:\\";
                file.Filter = "|*.jpg";
                radioButton3.Checked = true;
                DialogResult result = file.ShowDialog();
                if (result == DialogResult.OK)
                {
                    konum = file.FileName;
                    pictureBox1.Image = Image.FromFile(konum);
                    pictureBox1.Enabled = true;
                }
            }
            private void konumuSıfırlaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                yenile();
            }
            private void arkaPlanRenginiDeğişrtirToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                    this.BackColor = colorDialog1.Color;
                    Settings1.Default.arkaplanrenk = colorDialog1.Color;
                    Settings1.Default.Garkaplan = colorDialog1.Color;
                    
                    Settings1.Default.Save();
                    duzenlemebekleyenler();

                }
            }
            private void yazıRenginiDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                  
                    ForeColor = colorDialog1.Color;
                    girişYapToolStripMenuItem.ForeColor = colorDialog1.Color;
                    çıkışToolStripMenuItem1.ForeColor = colorDialog1.Color;
                    sıralamaToolStripMenuItem.ForeColor = colorDialog1.Color;
                    listeleToolStripMenuItem.ForeColor = colorDialog1.Color;
                    optionsToolStripMenuItem.ForeColor = colorDialog1.Color;
                    programToolStripMenuItem.ForeColor = colorDialog1.Color;
                    yapımcıToolStripMenuItem.ForeColor = colorDialog1.Color;
                    ağSürücüsüneBağlanToolStripMenuItem.ForeColor = colorDialog1.Color;
                    görüşBildirToolStripMenuItem.ForeColor = colorDialog1.Color;
                    groupBox1.ForeColor = colorDialog1.Color;
                    groupBox2.ForeColor = colorDialog1.Color;
                    Settings1.Default.yazirenk = colorDialog1.Color;
                    Settings1.Default.Gyazi = colorDialog1.Color;
                    Settings1.Default.Save();
                    duzenlemebekleyenler();
                }
            }
            private void yazıTipiniDeğiştirToolStripMenuItem_Click(object sender, EventArgs e) { this.Font = new Font("Arial Black", 16); }
            private void kalınYazıToolStripMenuItem_Click(object sender, EventArgs e) { this.Font = new Font("Arial Black", 16, FontStyle.Bold); }
            private void eğikYazıToolStripMenuItem_Click(object sender, EventArgs e) { this.Font = new Font("Arial Black", 16, FontStyle.Italic); }
            Thread ototmzbaslat;
            private void button9_Click_1(object sender, EventArgs e)
            {
                yenile();
                ototmzbaslat = new Thread(new ThreadStart(ototemizlik));
                ototmzbaslat.Start();
                button9.Visible = false;
                button40.Visible = true;
                button40.Enabled = true;
                hatamailigönder("Çıkan Desenleri otomatik bul özelliği çalıştırıldı.", "Özellik Kullanımı.");
            }
            private void radioButton1_CheckedChanged(object sender, EventArgs e)
            {
                checkBox9.Enabled = true;
                textBox1.Text = "";
                comboBox1.Text = "Tüm Desenler";
                groupBox1.Enabled = true;
                if (radioButton1.Checked)
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\ataturk.jpg");
                    konum = "R:\\Katalog\\desenler\\";
                    dataGridView1.Visible = true;
                   dataGridView2.Visible = false;
                    try
                    {
                        griddoldur();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK); con.Close(); }
                }
            }
            private void radioButton2_CheckedChanged(object sender, EventArgs e)
            {
                checkBox9.Enabled = false;
                textBox1.Text = "";
                comboBox1.Text = "Tüm Desenler";
                label3.Text = "Çıkan Desenler";
                pictureBox1.Enabled = false;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\ataturk.jpg");
                dataGridView2.Rows.Clear();
                konum = "R:\\Katalog\\çıkan desenler\\";
              dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                cikanlarilistele();
                dataGridView2.CurrentCell = null;
            }
            private void radioButton3_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    label3.Text = "Diğer";
                    groupBox1.Enabled = false;
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\ataturk.jpg");
                    dataGridView2.Visible = true;
                    dataGridView2.Rows.Clear();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            private void button10_Click(object sender, EventArgs e)
            {
                textBox1.Text = "";
                comboBox1.Text = button21.Text;
                textBox1.Focus();
                combo1doldur();
            }
            private void telefonNumarasıToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Telefon Numarası : +90 534 686 16 75", "Phone", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            private void ePostaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("1. Eposta : niyazikeklik@gmail.com" + Environment.NewLine + "2. Eposta : niyazikeklikk@gmail.com", "E- Posta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
                try
                {
                    int X = printDocument1.DefaultPageSettings.Margins.Left;
                    int Y = printDocument1.DefaultPageSettings.Margins.Top;
                    int Genislik = 600;
                    int Yukseklik = 480;
                    e.Graphics.DrawImage(pictureBox1.Image, X, Y, Genislik, Yukseklik);
                    Font font = new System.Drawing.Font("Arial", 11);
                    e.Graphics.DrawString("Ürün Kodu : " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "         Firma Adı : " + dataGridView1.CurrentRow.Cells[2].Value.ToString(), font, Brushes.Black, 10f, 10f);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Bir desen seçmemiş olabilirsiniz.", "Sorun", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
          
       
            }
            private void niyaziKeklikToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Niyazi Keklik, 2016/2017 Eğitim öğretim yılı Pendik Yunus Emre Mesleki ve Teknik Anadolu Lisesi, Bilişim bölümü, Web Tasarımı ve Bilgisayar Programcılığı dalı öğrencisi ,Neşe Plastik bilgi işlem kış stajyeri.", "Who am I", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Jpeg Dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp|Png(*.png)|*.png";
                sfd.Title = "Kayıt";
                sfd.FileName = "Resim";
                DialogResult sonuç = sfd.ShowDialog();
                if (sonuç == DialogResult.OK)
                {
                    pictureBox1.Image.Save(sfd.FileName);//Böylelikle resmi istediğimiz yere kaydediyoruz.
                }
            }
            private void button11_Click(object sender, EventArgs e)
            {
            try
            {
                if (textBox2.Text != "")
                {


                    con.Open();
                    dm = new OleDbCommand("insert into firmalar (firma_adı) values ('" + textBox2.Text + "')", con);
                    dm.ExecuteNonQuery();
                    con.Close();
                    comboBox3.Items.Add(textBox2.Text);
                    hatamailigönder("Yeni Firma Ekle özelliği çalıştırıldı.", "Özellik Kullanımı.");
                    MessageBox.Show("Yeni firma başarıyla eklendi.");
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz.", "Boş geçilemez", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (OleDbException) {
                MessageBox.Show("Aynı akyıttan zaten var");
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
           
          
            }
            private void bağlantıyıKapatToolStripMenuItem_Click(object sender, EventArgs e)
            {
                con.Close();
               
            }
            private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Application.Restart();
            }
            private void button12_Click(object sender, EventArgs e)
            {
                comboBox1.Text = "Tüm Desenler";
            }
            private void button13_Click(object sender, EventArgs e)
            {
                comboBox1.Text = "Firma Yok";
            }
            private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
            {


                pictureBox1.Enabled = true;
                button8.Enabled = true;
                try
                {
                  
                    label5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    pictureBox1.Image = Image.FromFile(konum + dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    resimadii = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    label5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                }
           
	                    catch (Exception ex)
	                    {
		                    MessageBox.Show(ex.Message);
	
	                    }
                 
               
               }             
            Thread baslatkont;
            private void button14_Click(object sender, EventArgs e)
            {
                yenile();
                baslatkont = new Thread(new ThreadStart(kontrolcu));
                baslatkont.Start();
                button14.Visible = false;
                button41.Visible = true;
                button41.Enabled = true;
                hatamailigönder("Sorunlu Ürün Kontrolü özelliği çalıştırıldı.", "Özellik Kullanımı.");
            }
            private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
            {
                Thread baslattt2 = new Thread(new ThreadStart(combo1doldur));
                baslattt2.Start();
            }
            private void varsaylıanToolStripMenuItem_Click(object sender, EventArgs e)
            { dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending); }
            private void hepsiToolStripMenuItem_Click(object sender, EventArgs e)
            { griddoldur(); }
            private void button15_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Seçilen satırlar silinecek emin misiniz?","Dikkat",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count >= 1)
                {

                    con.Open();
                    foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
                    {
                            try
                            {
                             
                              
                            dm = new OleDbCommand("Delete from desenler WHERE kod='"+ drow.Cells[0].Value.ToString() + "'", con);
                            dm.ExecuteNonQuery();
                            kackerecalisti++;

                            }
                            catch(OleDbException)
                            {
                                con.Close();
                                MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch(Exception ex)
                            {
                                con.Close();
                                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK);
                            }

                    }
                        con.Close();
                        MessageBox.Show(kackerecalisti + " Kere işlem gerçekleşmiştir.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        griddoldur();
                        kackerecalisti = 0;
             
                }
                else
                {
                    MessageBox.Show("Lütfen bir veya birden fazla satır seçiniz.", "Satır seçilmedi !!!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
           
        }
            private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
            {
                button16.Text =" Seçilen desenlere ''"+comboBox3.Text+"'' Firmasını Güncelle";
                checkBox7.Text = "Üstüne çift tıkladığım desene " +Environment.NewLine+"''" +comboBox3.Text + "'' firmasını güncelle.";
            }
            private void button16_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Seçilen satırlar güncellenecek emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 1)
                    {
                        if (comboBox3.Text != "")
                        {
                            con.Open();
                            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Güncelleme
                                {
                                    try
                                    {
                                        dm.CommandText = "update desenler set firma='" + comboBox3.Text + "',durum='" + "Güncel" + "' where kod='" + drow.Cells[0].Value.ToString() + "'";
                                        dm.ExecuteNonQuery();
                                        kackerecalisti++;
                                    }
                                    catch (OleDbException)
                                    {
                                        MessageBox.Show("Aynı Kayıttan Zaten var.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        con.Close();
                                    }
                                    catch (InvalidOperationException exd)
                                    {
                                        MessageBox.Show(exd.Message);
                                        con.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        con.Close();
                                    }
                                    if (checkBox2.Checked == false)
                                    {

                                    drow.Cells[2].Value = comboBox3.Text;
                                    drow.Cells[3].Value = "Güncellendi.Yenile...";

                                    }
                                }

                                con.Close();
                                if (checkBox2.Checked == true)
                                {
                                    griddoldur();
                                }
                                else
                                {
                                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                    {
                                        if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Güncellendi.Yenile...") 
                                        {
                                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                        }
                                    }
                                }
                        progressBar1.Value = 0;
                        label9.Text = "";
                        progressBar1.Visible = false;
                        MessageBox.Show(kackerecalisti + " Kere işlem gerçekleşmiştir.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        kackerecalisti = 0;
                        dataGridView1.CurrentCell = null;
                    }
                        else
                        {
                            MessageBox.Show("Lütfen bir firma seçiniz !!!", "Firma seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir veya birden fazla satır seçiniz.", "Satır seçilmedi !!!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
            }
           
        }


        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
            {
                aramaislemleri();
            }
            private void sadeceGüncelOlanlarToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    da = new OleDbDataAdapter("Select *from desenler where durum = '" + "Güncel" + "'", con);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;



                }
                catch (DataException) { ;}
                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            private void sadeceDüzenlemeBekleyenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    da = new OleDbDataAdapter("Select * from desenler where durum = '" + "Düzenleme Bekliyor" + "'", con);               
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                     duzenlemebekleyenler();
                    dataGridView1.CurrentCell = null;
                }
                catch (DataException) { ;}
                catch (OleDbException) { MessageBox.Show("con.Close(); R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
     
     
      
            private void button19_Click(object sender, EventArgs e)
            {
                Thread baslattt = new Thread(new ThreadStart(yenile));
                baslattt.Start();
            }
            private void açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked == false)
                {
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked = true;
                    Settings1.Default.guncellemeyapılacakmı = true;
                    Settings1.Default.Save();

                }
                else if (açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked == true)
                {
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked = false;
                    Settings1.Default.guncellemeyapılacakmı = false;
                    Settings1.Default.Save();
                }


            }
            private void button6_Click(object sender, EventArgs e)
            {
                yenile();
            }
            Thread baslattt;
                    private void Form1_Shown(object sender, EventArgs e)
            {
                 if (File.Exists(Application.StartupPath + "\\goster.txt") == true) // dizindeki dosya var mı ?
                 {
                try
                {
                    Form2 form2 = new Form2();
                    form2.listBox1.Items.Clear();
                    StreamReader inputstream = new StreamReader(@"R:\Katalog\Desen Arama\" + surum, Encoding.GetEncoding("windows-1254"));
                    string yazi, yazi2 = "";
                    while ((yazi = inputstream.ReadLine()) != "****") //satır boş olana kadar satır satır okumaya devam eder
                    {
                        form2.listBox1.Items.Add(yazi);//listbox1’i .txt içeriği ile satır satır doldu
                    }
                    inputstream.Close();
                    form2.ShowDialog();
                    File.Delete(Application.StartupPath + "\\goster.txt");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata Mesajı : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                }


            }
            private void comboBox3_Click(object sender, EventArgs e)
            {
                comboBox3.Items.Clear();
                con.Open();
                dm = new OleDbCommand("Select * from firmalar", con);
                OleDbDataReader reader = dm.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader["firma_adı"].ToString());
                }
                con.Close();

            }
            private void comboBox1_Click(object sender, EventArgs e)
            {
                backgroundWorker3.RunWorkerAsync();
            }
            private void varsayılanRenklerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                girişYapToolStripMenuItem.ForeColor = Color.Black;
                çıkışToolStripMenuItem1.ForeColor =Color.Black;
                sıralamaToolStripMenuItem.ForeColor = Color.Black;
                listeleToolStripMenuItem.ForeColor = Color.Black;
                optionsToolStripMenuItem.ForeColor = Color.Black;
                programToolStripMenuItem.ForeColor = Color.Black;
                yapımcıToolStripMenuItem.ForeColor =Color.Black;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.Black;
                görüşBildirToolStripMenuItem.ForeColor =  Color.Black;
                groupBox1.ForeColor =  Color.Black;
                groupBox2.ForeColor = Color.Black;
                this.BackColor = Color.White;
                ForeColor = Color.Black;
                Settings1.Default.arkaplanrenk = Color.White;
                Settings1.Default.yazirenk = Color.Black;
                Settings1.Default.Cyazırengi = Color.White;
                Settings1.Default.Carkaplan = Color.IndianRed;
                Settings1.Default.Darkaplan = Color.Red;
                Settings1.Default.Dyazırengi = Color.White;
                Settings1.Default.Garkaplan = Color.White;
                Settings1.Default.Gyazi = Color.Black;
                Settings1.Default.Save();
                duzenlemebekleyenler();
            }
            string kaynak = "";
            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                try
                {
                    string myPath = @"R:\Katalog\Desen Arama\Guncelle\guncelle.exe";
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
            Thread yeniurunkontrol;
        void mailgönder() {
            try
            {
              
                string ipAdresi = Dns.GetHostByName(Environment.MachineName.ToString()).AddressList[0].ToString();
                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: " + ipAdresi;
                int duzenlemebekleyenler=0,guncel=0,desensayisi;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString()=="Düzenleme Bekliyor")
                    {
                        duzenlemebekleyenler++;
                    }
                    else if (dataGridView1.Rows[i].Cells[3].Value.ToString()=="Güncel")
                    {
                        guncel++;
                    }
                }

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject = Environment.UserName.ToString() + " Tarafından " + kullanimsayisi.ToString() + ". Kez Açıldı.";
                mesaj.Body = "Desen arama programı hala aktif bir şekilde kullanılıyor. Sürüm: " + surum +Environment.NewLine+ istekbilgi + Environment.NewLine + "Düzenleme bekleyen ürün sayısı: " + duzenlemebekleyenler + Environment.NewLine + "Güncel olan ürün sayısı: " + guncel+Environment.NewLine + "Toplam ürün sayısı: " + dataGridView1.Rows.Count + Environment.NewLine + "Settings ayarları:" + Environment.NewLine +"1. " +Settings1.Default.b1 + Environment.NewLine + "2. "+Settings1.Default.b2 + Environment.NewLine + "3. "+Settings1.Default.b3 + Environment.NewLine + "4. "+Settings1.Default.b4 + Environment.NewLine + "5. "+Settings1.Default.b5 + Environment.NewLine + "6. "+Settings1.Default.b6 + Environment.NewLine +"7. "+ Settings1.Default.b7 + Environment.NewLine + "8. "+Settings1.Default.b8;
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

        void hatamailigönder(string hatamesaji,string baslik)
        {
            try
            {

                string ipAdresi = Dns.GetHostByName(Environment.MachineName.ToString()).AddressList[0].ToString();
                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: " + ipAdresi;
                int duzenlemebekleyenler = 0, guncel = 0, desensayisi;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Düzenleme Bekliyor")
                    {
                        duzenlemebekleyenler++;
                    }
                    else if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Güncel")
                    {
                        guncel++;
                    }
                }

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject =Environment.UserName.ToString()+baslik;
                mesaj.Body = "Hata Mesajı; "+ Environment.NewLine +hatamesaji+Environment.NewLine+"Settings ayarları:"+ Environment.NewLine  + Environment.NewLine + "Sürüm: " + surum + Environment.NewLine + istekbilgi + Environment.NewLine + "Düzenleme bekleyen ürün sayısı: " + duzenlemebekleyenler + Environment.NewLine + "Güncel olan ürün sayısı: " + guncel + Environment.NewLine + "Toplam ürün sayısı: " + dataGridView1.Rows.Count;
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
    
        


        string[] dizi = { "ataturk.jpg", "ataturk2.jpg", "ataturk3.jpg", "ataturk4.jpg", "ataturk5.jpg", "ataturk6.jpg", "ataturk7.jpg", "ataturk8.jpg", "ataturk9.jpg", "ataturk10.jpg", "ataturk11.png", "ataturk12.jpg" }; 
        long kullanimsayisi = Settings1.Default.kullanımsayisi;

   //     int bizimPcGenislik = 1366;
   //     int bizimPcYukseklik = 768;
   //     float kullanilanPcGenislik = SystemInformation.PrimaryMonitorSize.Width;
   //     float kullanilanPcYukseklik = SystemInformation.PrimaryMonitorSize.Height;
   ////     void ekranCozunurlugu()
   //     //{     
   //      //   float genislikOrani = (bizimPcGenislik / kullanilanPcGenislik);
   //      //   float yukseklikOrani = (bizimPcYukseklik / kullanilanPcYukseklik);
   //      //   float forGen = this.Size.Width;
   //      //   float forYuk = this.Size.Height;
           
   //       //  string nesFontAdi;
   //       ////  float nesFont, nesX, nesY, nesGen, nesYuk;
   //        // int fontBuyuk;
   //         //Size.Width = new Size((int)((kullanilanPcGenislik * 968) / bizimPcGenislik), (int)((kullanilanPcYukseklik * 643) / bizimPcYukseklik));
            
   //     //    foreach (Control nesne in Controls)
   //     //    {
               

   //     //        nesFontAdi = nesne.Font.SystemFontName;
   //     //        nesFont = nesne.Font.Size;
   //     //        nesX = nesne.Location.X;
   //     //        nesY = nesne.Location.Y;
   //     //        nesGen = nesne.Size.Width;
   //     //        nesYuk = nesne.Size.Height;
   //     //        nesne.Location = new Point((int)(nesX / genislikOrani), (int)(nesY / yukseklikOrani));
   //     //        nesne.Size = new Size((int)(nesGen / genislikOrani), (int)(nesYuk / yukseklikOrani));
   //     //        fontBuyuk = (int)(nesFont / yukseklikOrani);
   //     //       // if (fontBuyuk < 8) fontBuyuk = 8;
   //     //        nesne.Font = new Font(nesFontAdi, fontBuyuk);
   //     //        this.Size = new Size((int)((forGen / genislikOrani) + 20), (int)(forYuk / yukseklikOrani) + 150);

   //     //     if (nesne.Controls.Count > 0)
	  //     //     foreach (Control subNesne in nesne.Controls)
	  //     //     {
		 //     //      nesFontAdi = subNesne.Font.SystemFontName;
		 //     //      nesFont = subNesne.Font.Size;
		 //     //      nesX = subNesne.Location.X;
		 //     //      nesY = subNesne.Location.Y;
		 //     //      nesGen = subNesne.Size.Width;
		 //     //      nesYuk = subNesne.Size.Height;
		 //     //      subNesne.Location = new Point((int)(nesX / genislikOrani), (int)(nesY / yukseklikOrani));
		 //     //      subNesne.Size = new Size((int)(nesGen / genislikOrani), (int)(nesYuk / yukseklikOrani));
		 //     //      fontBuyuk = (int)(nesFont / yukseklikOrani);
		 //     //      //if (fontBuyuk < 8) fontBuyuk = 8;
   //     //            subNesne.Font = new Font(nesFontAdi, fontBuyuk);
		 //     //      this.Size = new Size((int)((forGen / genislikOrani) + 20), (int)(forYuk / yukseklikOrani) + 150);
   //     //        }
   //     //    }
   //     //}

        private void Form1_Load(object sender, EventArgs e)
            {
            // ekranCozunurlugu();

         
            kullanimsayisi++;
            Settings1.Default.kullanımsayisi = kullanimsayisi;
            Settings1.Default.Save();

            
            secilenler.Clear();

            try
            {
                kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked = Settings1.Default.ipüzerindenbaglan;
                if (kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked == true)
                {
                    kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked = false;
                    kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\tickoval.png");
                    kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\bosoval.png");
                }
                else
                {
                    kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked = true;
                    kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\bosoval.png");
                    kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\tickoval.png");
                }
              griddoldur();
              dataGridView1.Columns[1].Visible = false;
              dataGridView1.Columns[4].Visible = false;
              otomatikGüncellemeYapToolStripMenuItem.Checked = Settings1.Default.guncellemeyap;
              if (otomatikGüncellemeYapToolStripMenuItem.Checked == true)
              {
                  otomatikGüncellemeYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                  if (File.Exists("R:\\Katalog\\Desen Arama\\" + surum) == true)
                  {
                      linkLabel1.Visible = false;
                  }
                  else
                  {
                      try
                      {
                          MessageBox.Show("Programın yeni sürümü bulundu otomatik güncellenecektir ve program kapanacaktır." + Environment.NewLine + Environment.NewLine + "Tamam butonuna tıkladıktan sonra program kapanacak ve güncelleme sihirbazı çalışma izni isteyecektir. 'Programı çalıştır' Butonuna diyerek devam edin. 'Bir tuşa basınız' benzeri bir cümle yazılana kadar bekleyin ve yazıldığında bir tuşa basarak işlemi sonlandırınız." + Environment.NewLine + Environment.NewLine + " Otomatik güncellemeyi Program->Otomatik Guncelleme Yap bölümünden kapatabilirsiniz (Önerilmez.).", "Güncelleme Bulundu.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          string myPath = @"R:\Katalog\Desen Arama\Guncelle\guncelle.exe";
                          System.Diagnostics.Process prc = new System.Diagnostics.Process();
                          prc.StartInfo.FileName = myPath;
                          prc.Start();
                          Application.Exit();
                      }
                      catch (Win32Exception ex)
                      {
                          MessageBox.Show("Dosya Bulunamadı, Hata Mesajı : " + ex.Message, "Update Dosyası Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          hatamailigönder("Güncelleme programı Açılırken , update dosyası bulunamadı. " + ex.Message, "Güncelleme App Açılırken");
                      }
                      catch (Exception ex)
                      {

                          MessageBox.Show("Hata Kodu: " + ex.Message);
                          hatamailigönder("Güncelleme Yapılacakken , " + ex.Message, "Güncelleme App açılırken. ");
                      }


                  }
                  secilenler.Clear();
              }
              else
              {
                  otomatikGüncellemeYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                  if (File.Exists("R:\\Katalog\\Desen Arama\\" + surum) == true)
                  {
                      linkLabel1.Visible = false;
                  }
                  else
                  {
                      linkLabel1.Visible = true;
                  }
              }
            
            }
            catch (Exception ex)
            {
              MessageBox.Show("Hata Mesajı; " + ex.Message + Environment.NewLine + Environment.NewLine + "Hata Mesajı; Yerel Makine Kayıtlı Değil Şeklinde İse Gereken Programlar klasöründen 1. Programı Kurunuz."+Environment.NewLine+Environment.NewLine+"Değil ise ağ veya nsa bağlantınızı kontrol ediniz.","Program sağlıklı açılamıyor.",MessageBoxButtons.OK,MessageBoxIcon.Stop);
             
            }




          

            //RENK AYARLARI
                BackColor = Settings1.Default.arkaplanrenk;
                ForeColor = Settings1.Default.yazirenk;
                girişYapToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                çıkışToolStripMenuItem1.ForeColor = Settings1.Default.yazirenk;
                sıralamaToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                listeleToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                optionsToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                programToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                yapımcıToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                groupBox1.ForeColor = Settings1.Default.yazirenk;
                groupBox2.ForeColor = Settings1.Default.yazirenk;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                görüşBildirToolStripMenuItem.ForeColor = Settings1.Default.yazirenk;
                comboBox1.Items.Add("Tüm Desenler");
                comboBox1.Text = "Tüm Desenler";
                secilenler.Clear();
            timer1.Start();
                //SETTİNGS AYARLARI
                checkBox5.Checked = Settings1.Default.hızlandır;
                groupBox1.Visible = Settings1.Default.yoneticipanel; ;
                groupBox3.Visible = Settings1.Default.kısayol ;
                checkBox6.Checked = Settings1.Default.wfgorunteleyici;
                checkBox1.Checked = Settings1.Default.siyaharkaplan;
                checkBox9.Checked = Settings1.Default.eklenmezamanigöster;
           
                //ÜRÜN GÜNCELLEMESİ KONTROLÜ KODLARI
                açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked = Settings1.Default.guncellemeyapılacakmı;
                if (açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Checked == true)
                {
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");

                    yeniurunkontrol = new Thread(new ThreadStart(yeniurunkontroıl));
                    yeniurunkontrol.Start();
                }
                else if (groupBox1.Visible==true && button2.Visible==false)
                {
                    yeniurunkontrol = new Thread(new ThreadStart(yeniurunkontroıl));
                    yeniurunkontrol.Start();
                }
                else
                {
                    açılıştaÜrünGüncellemesiKontrolüYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                    progressBar1.Visible = false;
                }
                try
                {
                    Thread mail = new Thread(new ThreadStart(mailgönder));
                    mail.Start();
                }
                catch (Exception)
                {
                    ;
                   
                }
                Random rnd = new Random();
                int gecici = rnd.Next(0, 12);
                string istenilen = dizi[gecici];
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + istenilen);

          
               
            

           //yöneticipaneli gizle göster butonu
            if (Settings1.Default.yoneticipanel == false)
            {
                gizleGösterToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\göster2.png");
            }
            else
            {
                gizleGösterToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\gizle2.png");
            }

        }
            private void güncellemeKontrölüToolStripMenuItem_Click(object sender, EventArgs e)
            {
                UpdateKontrol();
            }
            private void button7_Click_2(object sender, EventArgs e)
            {

                combo1doldur();
                comboBox1.Text = button7.Text;
            }
            private void button12_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button12.Text;
            }
            private void button25_Click(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button25.Text;

            }
            private void button24_Click(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button24.Text;

            }
            private void button23_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button23.Text;
            }
            private void button20_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button20.Text;
            }
            private void button19_Click_2(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button19.Text;
            }
            private void button13_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button13.Text;
            }
            private void button21_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button21.Text;
            }
            private void button22_Click_1(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = "Firma Yok";
            }
            private void denemeToolStripMenuItem1_Click(object sender, EventArgs e)
            {

            }
            public class Shortcut
            {

                private static Type m_type = Type.GetTypeFromProgID("WScript.Shell");
                private static object m_shell = Activator.CreateInstance(m_type);
                [ComImport, TypeLibType((short)0x1040), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
                private interface IWshShortcut
                {
                    [DispId(0)]
                    string FullName { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0)] get; }
                    [DispId(0x3e8)]
                    string Arguments { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] set; }
                    [DispId(0x3e9)]
                    string Description { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] set; }
                    [DispId(0x3ea)]
                    string Hotkey { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] set; }
                    [DispId(0x3eb)]
                    string IconLocation { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] set; }
                    [DispId(0x3ec)]
                    string RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ec)] set; }
                    [DispId(0x3ed)]
                    string TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] set; }
                    [DispId(0x3ee)]
                    int WindowStyle { [DispId(0x3ee)] get; [param: In] [DispId(0x3ee)] set; }
                    [DispId(0x3ef)]
                    string WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] set; }
                    [TypeLibFunc((short)0x40), DispId(0x7d0)]
                    void Load([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
                    [DispId(0x7d1)]
                    void Save();
                }
                public static void Create(string fileName, string targetPath, string arguments, string workingDirectory, string description, string hotkey, string iconPath)
                {
                    IWshShortcut shortcut = (IWshShortcut)m_type.InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, m_shell, new object[] { fileName });
                    shortcut.Description = description;
                    shortcut.Hotkey = hotkey;
                    shortcut.TargetPath = targetPath;
                    shortcut.WorkingDirectory = workingDirectory;
                    shortcut.Arguments = arguments;
                    if (!string.IsNullOrEmpty(iconPath))
                        shortcut.IconLocation = iconPath;
                    shortcut.Save();
                }
            }
            private void kısayolOluşturDesktopToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    Thread.Sleep(100);
                    string uygulama_dizin = "C:\\Desen Arama\\Desen Arama.exe";// kısayolu oluşturulacak dizin
                    string kisayol_adi = "Desen Arama"; // kısayolu oluşturulacak uygulamanın masaüzerindeki adı
                    string masaustu_dizin = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + kisayol_adi + ".lnk"; // masaüstü dizini ve birleştirildi.
                    Shortcut.Create(masaustu_dizin, "C:\\Desen Arama\\Desen Arama.exe", "", null, kisayol_adi, "", null); // kısayol oluşturma fonksiyonu kullanıldı.
                    MessageBox.Show("Masaüstüne Klasör Oluşturuldu", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception hata) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Kısayol oluşturulamadı. Hata: " + hata.Message); }
            }

            private void otomatikGüncellemeYapToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (otomatikGüncellemeYapToolStripMenuItem.Checked == false)
                {
                    otomatikGüncellemeYapToolStripMenuItem.Checked = true;
                    otomatikGüncellemeYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                   
                    Settings1.Default.guncellemeyap = true;
                    Settings1.Default.Save();
                }
                else if (otomatikGüncellemeYapToolStripMenuItem.Checked == true)
                {
                    otomatikGüncellemeYapToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                    otomatikGüncellemeYapToolStripMenuItem.Checked = false;
                    Settings1.Default.guncellemeyap = false;
                    Settings1.Default.Save();
                }
            }

            private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
            {

            }

            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                resimadii = "";
                if (pictureBox1.Enabled == false) { pictureBox1.Enabled = true; }
                if (button8.Enabled == false) { button8.Enabled = true; }
                try
                {
                    pictureBox1.Image = Image.FromFile(konum + dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    label3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    label5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    resimadii = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }

                catch (Exception)
                {
                    if (pictureBox1.Enabled == false) { pictureBox1.Enabled = true; }
                    if (button8.Enabled == false) { button8.Enabled = true; }
                    try
                    {
                        pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    }
                    catch (Exception es)
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\resimyok.jpg");
                        MessageBox.Show(es.Message+ Environment.NewLine + " Program desen dosyasını katalogda bulamıyor. Katalogdan çıkartılmış olabilir böyle bir durumda desen kaydını veritabanından da silmeniz gerekir.","Boş Kayıt",MessageBoxButtons.OK,MessageBoxIcon.Error);
                      
                        if (button5.Visible==true)
	                    {
		                     if (MessageBox.Show("Desen kaydını veritabanından silmek ister misiniz?","Silinsin mi?",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                             {

                                 try
                                 {


                                     con.Open();
                                     dm = new OleDbCommand("Delete from desenler WHERE kod=@ogrno", con);
                                     dm.Parameters.AddWithValue("@ogrno", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                                     dm.ExecuteNonQuery();
                                     if (checkBox2.Checked == false)
                                     {

                                         dataGridView1.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
                                         dataGridView1.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
                                         dataGridView1.CurrentRow.Cells[2].Value = "Bu ürün silindi.";
                                         dataGridView1.CurrentRow.Cells[3].Value = "Yenile Butonuna Basınız..";

                                     }


                                 }
                                 catch (OleDbException) { con.Close(); MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                 catch (Exception ex) { con.Close(); MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK); }

                                 con.Close();
                                 if (checkBox3.Checked == true)
                                 {
                                     griddoldur();
                                 }






                             }
         
        
                        
                            
                        }
                    
	
                }
                }
            }
             
            
            
        

        

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
            
            }

            private void yeniÜrünKontrolToolStripMenuItem_Click(object sender, EventArgs e)
            {

                yenile();
                yeniurunkontroıl();
            }

            private void çıkanDesenleriVeritabanındanSilToolStripMenuItem_Click(object sender, EventArgs e)
            {

                yenile();
                Thread ototmzbaslat = new Thread(new ThreadStart(ototemizlik));
                ototmzbaslat.Start();
            }

            private void veritabanınıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
            {

                yenile();
                Thread baslat;
                baslat = new Thread(new ThreadStart(guncelle));
                baslat.Start();
            }

            private void sorunluÜrünKontrolüYapToolStripMenuItem_Click(object sender, EventArgs e)
            {
                yenile();
                Thread baslat = new Thread(new ThreadStart(kontrolcu));
                baslat.Start();
            }

            private void toolStripTextBox1_Click(object sender, EventArgs e)
            {
                toolStripTextBox1.Text = "";


            }

            private void toolStripTextBox2_Click(object sender, EventArgs e)
            {
                toolStripTextBox2.Text = "";
            }

            private void girişYapToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                if (toolStripTextBox2.Text == "nese1")
                {
                    if (toolStripTextBox1.Text != "neseplastik")
                    {
                        MessageBox.Show("Kullanıcı adınız yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        button38.Enabled = true;
                        backgroundWorker3.RunWorkerAsync();
                        button9.Enabled = true;
                        button4.Enabled = true;
                        button2.Visible = false;
                        button5.Visible = true;
                        button9.Enabled = true;
                        textBox2.Enabled = true;
                        label10.Enabled = true;
                        button11.Enabled = true;
                        button14.Enabled = true;
                        comboBox3.Enabled = true;
                        button15.Enabled = true;
                        button16.Enabled = true;
                        checkBox7.Enabled = true;
                        checkBox8.Enabled = true;
                        checkBox3.Enabled = true;
                        checkBox2.Enabled = true;
                        veritabanıToolStripMenuItem.Enabled = true;
                        groupBox1.Visible = true;
                        groupBox3.Visible = false;
                    button17.Enabled = true;

                  
                }

                }
                else
                {
                    MessageBox.Show("Şifreniz yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            int i = 0;
            private void şifreniMiUnuttunToolStripMenuItem_Click(object sender, EventArgs e)
            {

                if (i == 0)
                {

                    try
                    {
                        string ipAdresi = Dns.GetHostByName(Environment.MachineName.ToString()).AddressList[0].ToString();
                        string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: " + ipAdresi;

                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        MailMessage mesaj = new MailMessage();
                        mesaj.To.Add("saffetm@neseplastik.com");
                        mesaj.To.Add("huseyin@neseplastik.com");
                        mesaj.To.Add("niyazikeklikk@gmail.com");
                        mesaj.From = new MailAddress("odevimp@gmail.com");
                        mesaj.Subject = "Desen Arama Programı Giriş Şifreniz";
                        mesaj.Body = "Desen arama programı giriş için Kullanıcı Adı : ''neseplastik'', Şifresi : ''nese1'' ' dir. Niyazi Keklik"+Environment.NewLine+ istekbilgi;
                        NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                        client.Credentials = guvenlik;
                        client.EnableSsl = true;
                        client.Send(mesaj);
                        MessageBox.Show("Mail Başarıyla ''saffetm@neseplastik.com'' Adresine ve ''huseyin@neseplastik.com'' Adresine Gönderildi.", "Mail Gönderme");
                        i++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mesajınız gönderelimedi. Hata Mesajı : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Kısa bir süre içinde ikinci kez mail gönderemezsiniz.", "Bot Engel", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            
            }

            private void oturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Settings1.Default.siyaharkaplan = false;
                button9.Enabled = false;
                button5.Visible = false;
                button2.Visible = true;
                comboBox3.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                button4.Enabled = false;
                textBox2.Enabled = false;
                label10.Enabled = false;
                button11.Enabled = false;
                button14.Enabled = false;
                checkBox3.Enabled = false;
                checkBox2.Enabled = false;
                veritabanıToolStripMenuItem.Enabled = false;
                button38.Enabled = false; button17.Enabled = false; 
        }

            private void girişYapToolStripMenuItem1_TextChanged(object sender, EventArgs e)
            {
                if (toolStripTextBox1.Text == "neseplastik")
                {
                    if (toolStripTextBox2.Text == "nese1")
                    {
                        backgroundWorker3.RunWorkerAsync();
                        button9.Enabled = true;
                        button38.Enabled = true;
                        button4.Enabled = true;
                        button2.Visible = false;
                        button5.Visible = true;
                        button9.Enabled = true;
                        textBox2.Enabled = true;
                        label10.Enabled = true;
                        button14.Enabled = true;
                        comboBox3.Enabled = true;
                        button15.Enabled = true;
                        button16.Enabled = true;
                        checkBox7.Enabled = true;
                        checkBox8.Enabled = true;
                    button17.Enabled = true;
                    checkBox3.Enabled = true;
                        checkBox2.Enabled = true;
                        veritabanıToolStripMenuItem.Enabled = true; 

                    }
                    else
                    {
                        MessageBox.Show("Şifreniz yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adınız yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Focus();

                }
            
            }

            private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
            {
                if (toolStripTextBox1.Text == "neseplastik")
                {
                    toolStripTextBox2.Focus();
                    toolStripTextBox1.BackColor = Color.SeaGreen;
                    toolStripTextBox1.ForeColor = Color.Black;

                }
                else {


                    toolStripTextBox1.BackColor = Color.IndianRed;
                    toolStripTextBox1.ForeColor = Color.White;
                
                }
            }

            private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
            {
                TextBox tb = this.toolStripTextBox2.Control as TextBox;
                tb.PasswordChar = '*';

                if (toolStripTextBox2.Text == "nese1")
                {
                    if (toolStripTextBox1.Text != "neseplastik")
                    {
                        MessageBox.Show("Kullanıcı adınız yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripTextBox2.Focus();
                        toolStripTextBox1.BackColor = Color.SeaGreen;
                        toolStripTextBox2.ForeColor = Color.Black;
                    }
                    else if (toolStripTextBox1.Text == "neseplastik")
                   
                    {
                        toolStripTextBox2.BackColor = Color.SeaGreen;
                        toolStripTextBox2.ForeColor = Color.Black;
                        backgroundWorker3.RunWorkerAsync();
                        button9.Enabled = true;
                        button4.Enabled = true;
                        button2.Visible = false;
                        button5.Visible = true;
                        button9.Enabled = true;
                        textBox2.Enabled = true;
                        label10.Enabled = true;
                        button11.Enabled = true;
                        button14.Enabled = true;
                        comboBox3.Enabled = true;
                        button15.Enabled = true;
                        button16.Enabled = true;
                        checkBox7.Enabled = true;
                        checkBox8.Enabled = true;
                        checkBox3.Enabled = true;
                        checkBox2.Enabled = true;
                        veritabanıToolStripMenuItem.Enabled = true;
                        groupBox1.Visible = true;
                        groupBox3.Visible = false;
                     
                        button38.Enabled = true;
                        button17.Enabled = true;

                     
                    }


                }
                else
                {
                    toolStripTextBox2.BackColor = Color.IndianRed;
                    toolStripTextBox2.ForeColor = Color.White;
                  
                    button17.Enabled = false;
                    button38.Enabled = false;
                    button9.Enabled = false;
                    button5.Visible = false;
                    button2.Visible = true;
                    comboBox3.Enabled = false;
                    button15.Enabled = false;
                    button16.Enabled = false;
                    checkBox7.Enabled = false;
                    checkBox8.Enabled = false;
                    button4.Enabled = false;
                    textBox2.Enabled = false;
                    label10.Enabled = false;
                    button11.Enabled = false;
                    button14.Enabled = false;
                    checkBox3.Enabled = false;
                    checkBox2.Enabled = false;
                    veritabanıToolStripMenuItem.Enabled = false;
                }
          


            }

            int sayi2 = 0;
            int sayi3 = 0;
            private void girişYapToolStripMenuItem_Click(object sender, EventArgs e)
            {
         
                Random sayi = new Random();
                sayi2 = sayi.Next(11, 99);
                sayi3 = sayi.Next(11, 99);
                güvenlikSorusuToolStripMenuItem.Text = sayi2.ToString() + "x" + sayi3.ToString() + "= ?";

            }

            private void button26_Click(object sender, EventArgs e)
            {
            

            }

            private void button36_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = "FANTASTİK CLASSİC";
            }

            private void button35_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button35.Text;
            }

        

            private void button33_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button33.Text;
            }

            private void button30_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button30.Text;
            }

            private void button31_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = "Mehmet Diş";
            }

            private void button32_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button32.Text;
            }

            private void button28_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button28.Text;
            }

            private void fotoğrafZipleToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Fotografziple yenii = new Fotografziple();
                yenii.ShowDialog();
              
            }

            private void checkBox4_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox4.Checked)
                {
                    Opacity = 0.5;

                }
                else
                {
                    Opacity = 1;
                }
            }

            private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
            {

            }

            private void firmaAdınıKopyalaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    Clipboard.SetText(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                }
                catch (Exception)
                {

                    MessageBox.Show("Bir satır seçiniz.", "Satır seçilmedi.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
           
            }

            private void ürünKodunuKopyalaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    Clipboard.SetText(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                }
                catch (Exception)
                {

                    MessageBox.Show("Bir satır seçiniz.", "Satır seçilmedi.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
 
            }

            private void firmaAdınıVeÜrünKodunuKopyalaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    string bos = dataGridView1.CurrentRow.Cells[0].Value.ToString() + "   " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    Clipboard.SetText(bos);
                }
                catch (Exception)
                {

                    MessageBox.Show("Bir satır seçiniz.", "Satır seçilmedi.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
           
            }

            private void checkBox5_CheckedChanged(object sender, EventArgs e)
            {
           
                if (checkBox5.Checked == true)
                {
                        Settings1.Default.hızlandır = true;
                     
                }
                else
                {
                    Settings1.Default.hızlandır = false;
                }
                Settings1.Default.Save();
               
            }

            private void denemeToolStripMenuItem_Click(object sender, EventArgs e)
            {
            
     
           
            }

            private void programToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {

            }

            private void listeleToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void girişYapToolStripMenuItem_TextChanged(object sender, EventArgs e)
            {
                TextBox tb = this.toolStripTextBox2.Control as TextBox;
                tb.PasswordChar = '*';
            }

            private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
            {
                if (pictureBox1.Enabled == false) { pictureBox1.Enabled = true; }
                if (button8.Enabled == false) { button8.Enabled = true; }
                try
                {
                    pictureBox1.Image = Image.FromFile(konum + dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    label3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    label5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    resimadii = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch (ArgumentOutOfRangeException) {; }
                catch (OutOfMemoryException) {; }
                catch (IndexOutOfRangeException) { MessageBox.Show("Seçtiğiniz dizin geçersiz !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                catch (BadImageFormatException) { MessageBox.Show("Resim yolu bulunamadı !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                catch (FileNotFoundException) { MessageBox.Show("Resim yolu bulunamadı !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }

            private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
            {

            }
            double msaniye;
            private void timer1_Tick(object sender, EventArgs e)
            {
             msaniye+=1;
             double kacdkoldu = msaniye / 60;
               if (msaniye % 7200 == 0)
	{		 
                  hatamailigönder("Program "+kacdkoldu+" Dakikadır Çalışıyor, Buda "+kacdkoldu/60+" Saat Yapar.","Program Uzun Süredir Açık");
	}
           
            if (File.Exists(@"C:\Desen Arama\kapat.txt") == true || kacdkoldu >=480) 
                {
                    try
                    {
                        Process[] p;
                        p = Process.GetProcessesByName("Desen Arama");
                        if (p.Length > 0)
                        {

                            foreach (Process process in p)
                            {
                                process.Kill();

                            }
                            Application.Exit();



                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception)
                    {

                        Application.Exit();
                    }
                  
               
                }

           
            }

            private void yapımcıToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
            {
            }

            private void küçükDosyalarıAyıklaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (button2.Visible==true)
                {
                    MessageBox.Show("Bu özelliği kullanmak için yetkiniz bulunmamaktadır. Lütfen yönetici olarak giriş yaptıktan sonra tekrar deneyiniz.", "Yetkisiz kullanım.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                   
                }
                else
                {
                    Form3 git = new Form3();
                    git.ShowDialog();
                    hatamailigönder("Küçük fotoğrafları ayıkla özelliği çalıştırıldı.", "Özellik Kullanımı.");
                }
              
            }

            private void windowsFotoğrafGörünteleyicisindeAçToolStripMenuItem_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start(konum + resimadii);
            }

            private void checkBox6_CheckedChanged(object sender, EventArgs e)
            {
          
            if (checkBox6.Checked == true)
                {
                    Settings1.Default.wfgorunteleyici = true;

                }
                else
                {
                    Settings1.Default.wfgorunteleyici = false;
                }
                Settings1.Default.Save();
            checkBox10.Checked = false;
        }

            private void ihracatToolStripMenuItem_Click(object sender, EventArgs e)
            {
                NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                try
                {
               
                    oNetDrive.Force = true;
                    oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                    oNetDrive.LocalDrive = "O"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                    if (kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked == true)
                    {
                        oNetDrive.ShareName = @"\\192.168.2.253\İhracat Ortak\file\DESENLER"; 
                    }
                    else if (kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked == true)
                    {
                        oNetDrive.ShareName = @"\\nsa220\İhracat Ortak\file\DESENLER"; 
                    }
                    
                    /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    oNetDrive.SaveCredentials = false;
                    oNetDrive.MapDrive("ihr6");
                    oNetDrive.MapDrive("ihr");
                    yenile();
                }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
                oNetDrive = null;

            }

            private void katalogToolStripMenuItem_Click(object sender, EventArgs e)
            {
                NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                try
                {
                    oNetDrive.Force = true;
                    oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                    oNetDrive.LocalDrive = "R"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                    if (kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked==true)
                    {
                        oNetDrive.ShareName = @"\\192.168.2.253\public"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    }
                    else if (kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked==true)
                    {
                        oNetDrive.ShareName = @"\\nsa220\public"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    }
                    oNetDrive.SaveCredentials = false;
                    oNetDrive.MapDrive();
                    yenile();
                }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
                oNetDrive = null;
            }

            private void ağSürücüsüneBağlanToolStripMenuItem_Click(object sender, EventArgs e)
            {
            
            }

            private void bağlantıyıKesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                NetworkDrive oNetDrive = new NetworkDrive();
                try
                {
                    //unmap the drive
                    oNetDrive.Force = true;
                    oNetDrive.LocalDrive = "R";
                    oNetDrive.UnMapDrive();
                }
                catch (Exception err)
                { MessageBox.Show(err.Message); }
                oNetDrive = null;
            }

            private void bağlantıyıKesToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                NetworkDrive oNetDrive = new NetworkDrive();
                try
                {
                    //unmap the drive
                    oNetDrive.Force = true;
                    oNetDrive.LocalDrive = "O";
                    oNetDrive.UnMapDrive();
                }
                catch (Exception err)
                { MessageBox.Show(err.Message); }
                oNetDrive = null;
            }

            private void toolStripTextBox3_TextChanged(object sender, EventArgs e)
            {
                TextBox tb = this.toolStripTextBox3.Control as TextBox;
                tb.PasswordChar = '*';
                if (toolStripTextBox3.Text == "nese1")
                {
                
                    ihracatToolStripMenuItem.Enabled = true;
                }
                else
	            {
                
                    ihracatToolStripMenuItem.Enabled = false;
                }
         
            }

            private void toolStripTextBox3_Click(object sender, EventArgs e)
            {
                TextBox tb = this.toolStripTextBox3.Control as TextBox;
                tb.PasswordChar = '*';
                toolStripTextBox3.Text = "";
            }

            private void toolStripTextBox4_TextChanged(object sender, EventArgs e)
            {
                if (toolStripTextBox4.Text==(sayi2*sayi3).ToString())
                {
                    şifreniMiUnuttunToolStripMenuItem.Enabled = true;
                    toolStripTextBox4.BackColor = Color.SeaGreen;
                    toolStripTextBox4.ForeColor = Color.Black;

                }
                else
                {
                    toolStripTextBox4.BackColor = Color.IndianRed;
                    toolStripTextBox4.ForeColor = Color.White;
                    şifreniMiUnuttunToolStripMenuItem.Enabled = false;
                }
            }

            private void toolStripTextBox4_Click(object sender, EventArgs e)
            {
                toolStripTextBox4.Text = "";
            }

            private void girişYapToolStripMenuItem_MouseHover(object sender, EventArgs e)
            {
                Random sayi = new Random();
                sayi2 = sayi.Next(11, 99);
                sayi3 = sayi.Next(11, 99);
                güvenlikSorusuToolStripMenuItem.Text = sayi2.ToString() + "x" + sayi3.ToString() + "= ?";
            }

            private void checkBox4_MouseHover(object sender, EventArgs e)
            {
            
            }

            private void checkBox4_MouseLeave(object sender, EventArgs e)
            {
            
            }

            private void pictureBox1_MouseHover(object sender, EventArgs e)
            {
              pictureBox1.Cursor = Cursors.NoMove2D;
          
            }

            private void pictureBox1_MouseLeave(object sender, EventArgs e)
            {

              
            }

            private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void button26_MouseHover(object sender, EventArgs e)
            {
            
            }

            private void button26_MouseLeave(object sender, EventArgs e)
            {
            }

            string secilenyer = "";
            Thread ss;
            private void button38_Click(object sender, EventArgs e)
        {
            yenile();
            FolderBrowserDialog save = new FolderBrowserDialog();
                save.ShowNewFolderButton = true;
                progressBar1.Maximum = Directory.GetFiles("R:\\Katalog\\desenler", "*.jpg", SearchOption.AllDirectories).Length;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                label9.Text = "";
                label9.Visible = true;
                save.Description = "Desenlerin Taşınacağı Klasörü Seçiniz.";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    secilenyer = save.SelectedPath;
                   ss = new Thread(new ThreadStart(dosyatasi));
                    ss.Start();
                    button38.Visible = false;
                    button42.Visible = true;
                    button42.Enabled = true;
                    hatamailigönder("Kayıt dışı olanları taşı özelliği çalıştırıldı.", "Özellik Kullanımı.");
                }




                
            }

            private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
            {
                dosyatasi();
            }

            private void checkBox8_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox8.Checked==true)
                {
                    checkBox7.Checked = false;
                }

            }

            private void checkBox7_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox7.Checked == true)
                {
                    checkBox8.Checked = false;
                }
            }

            private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    if (checkBox7.Checked == true)
                    {
                        if (comboBox3.Text != "")
                        {
                            con.Open();
                            dm = new OleDbCommand();
                            dm.Connection = con;
                            for (int i = (secilenler.Count - dataGridView1.SelectedRows.Count); i < secilenler.Count; i++)
                            {
                                try
                                {
                                  
                                    dm.CommandText = "update desenler set firma='" + comboBox3.Text + "',durum='" + "Güncel" + "' where kod='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                                    dm.ExecuteNonQuery();
                                    
                                    if (checkBox2.Checked == false)
                                    {

                                        dataGridView1.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Goldenrod;
                                        dataGridView1.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
                                        dataGridView1.CurrentRow.Cells[2].Value = comboBox3.Text;
                                        dataGridView1.CurrentRow.Cells[3].Value = "Güncellendi.Yenile...";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);

                                }
                            }
                            con.Close();
                            if (checkBox2.Checked == true)
                            {
                                griddoldur();


                            }
                        }
                        else
                        {
                            MessageBox.Show("Lütfen bir firma seçiniz !!!", "Firma seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                       
                    }
                    if (checkBox8.Checked == true)
                    {
                        try
                        {
                          

                            con.Open();
                            dm = new OleDbCommand("Delete from desenler WHERE kod=@ogrno", con);
                            dm.Parameters.AddWithValue("@ogrno", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                            dm.ExecuteNonQuery();
                            if (checkBox2.Checked == false)
                            {

                                dataGridView1.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
                                dataGridView1.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
                                dataGridView1.CurrentRow.Cells[2].Value = "Bu ürün silindi.";
                                dataGridView1.CurrentRow.Cells[3].Value = "Yenile Butonuna Basınız..";

                            }


                        }
                        catch (OleDbException) { con.Close(); MessageBox.Show("Hata olustu!,Listeyi yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        catch (Exception ex) { con.Close(); MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK); }

                        con.Close();
                        if (checkBox3.Checked == true)
                        {
                            griddoldur();
                        }
                    }
                }
                catch (Exception ww)
                {
                    MessageBox.Show(ww.Message, "Hata", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    con.Close();
                }

               
            }

            private void groupBox1_Enter(object sender, EventArgs e)
            {

            }

            private void dataGridView1_Sorted(object sender, EventArgs e)
            {
                if (checkBox5.Checked!=true)
                {
                    duzenlemebekleyenler();
                }
            }

            private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
            {
          
            }

            private void dataGridView1_SelectionChanged_2(object sender, EventArgs e)
            {
                label15.Text = dataGridView1.SelectedRows.Count.ToString();
            }

            private void iletişimToolStripMenuItem_Click_1(object sender, EventArgs e)
            {

            }

            private void sosyalMedyaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Twitter: @alienationxs"+Environment.NewLine+"İnstagram: @alienationxs","Social Media",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        private void dataGridView2_KeyUp(object sender, KeyEventArgs e)
        {
            
            pictureBox1.Enabled = true;
            button8.Enabled = true;
            try
            {
                label5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                pictureBox1.Image = Image.FromFile(konum + dataGridView2.CurrentRow.Cells[0].Value.ToString());
                resimadii = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
            catch (ArgumentOutOfRangeException) {; }
            catch (NullReferenceException) {; }
            catch (OutOfMemoryException) {; }
            catch (IndexOutOfRangeException) { MessageBox.Show("Seçtiğiniz dizin geçersiz !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            catch (BadImageFormatException) { MessageBox.Show("Resim yolu bulunamadı !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            catch (FileNotFoundException) { MessageBox.Show("Resim yolu bulunamadı !", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            cikanlarilistele();
            dataGridView2.Visible = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
           
                
            aramaislemleri();
            checkBox6.Checked = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            Form4 sil = new Form4();
            sil.ShowDialog();
             
        }

        private void button27_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button27.Text;
            }

            private void button29_Click(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button29.Text;
            }

            private void button37_Click(object sender, EventArgs e)
            {
                combo1doldur();

                comboBox1.Text = button37.Text;
            }

            private void button34_Click(object sender, EventArgs e)
            {
                combo1doldur();
                comboBox1.Text = button34.Text;
            }

            private void sürümNotlarıToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    Form2 form2 = new Form2();
                    form2.listBox1.Items.Clear();
                    StreamReader inputstream = new StreamReader(@"R:\Katalog\Desen Arama\" + surum, Encoding.GetEncoding("windows-1254"));
                    string yazi, yazi2 = "";
                    while ((yazi = inputstream.ReadLine()) != "****") //satır boş olana kadar satır satır okumaya devam eder
                    {
                        form2.listBox1.Items.Add(yazi);//listbox1’i .txt içeriği ile satır satır doldu
                    }
                    inputstream.Close();
                    form2.ShowDialog();
       
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eski bir sürüm kullanıyor olabilirsiniz lütfen programı güncelleyiniz."+Environment.NewLine+"Hata Mesajı : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
         
            }

            private void görüşBildirToolStripMenuItem_Click(object sender, EventArgs e)
            {
                görüsbildir yeni = new görüsbildir();
                yeni.Show();
            }

            private void programİçerisindeAçToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    ressimbuyut git = new ressimbuyut();
                    git.pictureBox1.Image = Image.FromFile(konum + resimadii);

                    git.ShowDialog();
                }
                catch (Exception)
                {
                    try
                    {
                        ressimbuyut git = new ressimbuyut();
                        git.pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString());

                        git.ShowDialog();


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tekrar Desen Seçiniz Veya Yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }


                }
            }

            private void tamEkrandaAçToolStripMenuItem_Click(object sender, EventArgs e)
            {
                 try
                {
                    r2 = new ressimbuyut();
                    r2.Show();
                    r2.WindowState = FormWindowState.Maximized;
                    r2.pictureBox1.Image = Image.FromFile(konum + resimadii);
                    r2.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception) {
                    try
                    {
                     
                    r2 = new ressimbuyut();
                    r2.Show();
                    r2.WindowState = FormWindowState.Maximized;
                    r2.pictureBox1.Image = Image.FromFile(@"R:\Katalog\çıkan desenler\" + dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    r2.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tekrar Desen Seçiniz Veya Yenileyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        r2.Close();
                    }
                }
                    
                    
            }

            private void eklenmeSırasınaGöreToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (azalanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Descending);
                }
                else if (artanToolStripMenuItem.Checked == true)
                {
                    dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
                }
     

            }

            private void checkBox9_CheckedChanged(object sender, EventArgs e)
            {

                
                if (dataGridView1.Columns[4].Visible == false || checkBox9.Checked)
                {
                      dataGridView1.Columns[4].Visible = true;
                      dataGridView1.Columns[3].Visible = false;
                      dataGridView1.Columns[2].Width = 100;
                      dataGridView1.Columns[4].Width = 100;
                      dataGridView1.Columns[4].HeaderText = "Tarih";
                      Settings1.Default.eklenmezamanigöster = true;
                }
                else if (dataGridView1.Columns[3].Visible = false|| checkBox9.Checked==false)
                {
                    dataGridView1.Columns[3].Visible = true;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[2].Width = 100;
                    Settings1.Default.eklenmezamanigöster = false;
                }
                Settings1.Default.Save();
            }

            private void artanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                azalanToolStripMenuItem.Checked = false;
                artanToolStripMenuItem.Checked = true;
                artanToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                azalanToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
             
                
            }

            private void azalanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                
                    azalanToolStripMenuItem.Checked = true;
                    artanToolStripMenuItem.Checked = false;
                    azalanToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                    artanToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                
            }

            private void bugünEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {DateTime birgunoncesi = DateTime.Now.AddDays(-1);
            string birgunoncesi2 = birgunoncesi.ToString();
     
            try
            {
                OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                da = new OleDbDataAdapter(cmd);
                tbl = new DataTable();
                da.Fill(tbl);
                dataGridView1.DataSource = tbl;

                gridsatirsayisi = dataGridView1.Rows.Count - 1;
                label8.Text = gridsatirsayisi.ToString();
                dataGridView1.CurrentCell = null;
                duzenlemebekleyenler();


            }
           
            catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }

            private void sonBirHaftaİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birhafta = DateTime.Now.AddDays(-7);
                string birhafta2 = birhafta.ToString();
          
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birhafta  ", con);
                    cmd.Parameters.AddWithValue("birhafta", birhafta2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            private void son1AyİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {

                DateTime biray = DateTime.Now.AddMonths(-1);
                string biray2 = biray.ToString();

                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @biray  ", con);
                    cmd.Parameters.AddWithValue("biray", biray2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            private void sonBirYılİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {

                DateTime biryıl = DateTime.Now.AddYears(-1);
                string biryıl2 = biryıl.ToString();

                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @biryıl  ", con);
                    cmd.Parameters.AddWithValue("biryıl", biryıl2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            private void kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem_Click(object sender, EventArgs e)
            {
                kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked = true;
                kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked = false;
                    kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Image = Image.FromFile(Application.StartupPath+"\\bosoval.png");
                    kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                    Settings1.Default.ipüzerindenbaglan = true;
                    Settings1.Default.Save();
                   

            }

            private void kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem_Click(object sender, EventArgs e)
            {
                kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Checked = false;
                kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Checked = true;
                kataloğaİsimÜzerindenBağlanNSAToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\tickoval.png");
                kataloğaİpÜzerindenBağlan1921682253ToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\bosoval.png");
                Settings1.Default.ipüzerindenbaglan = false;
                Settings1.Default.Save();
             
            }

            private void sonBirSaatİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birgunoncesi = DateTime.Now.AddHours(-1);
                string birgunoncesi2 = birgunoncesi.ToString();
               
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                    cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }

            private void son15DakikaİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birgunoncesi = DateTime.Now.AddMinutes(-15);
                string birgunoncesi2 = birgunoncesi.ToString();
                
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                    cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }

            private void sonİkiGünİçerisindeEklenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birgunoncesi = DateTime.Now.AddDays(-2);
                string birgunoncesi2 = birgunoncesi.ToString();
                
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                    cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }

            private void sonİkiHaftaİçerisindeEKlenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birgunoncesi = DateTime.Now.AddDays(-14);
                string birgunoncesi2 = birgunoncesi.ToString();
            
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                    cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }

            private void sonİkiAyİçerisindeEKlenenlerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DateTime birgunoncesi = DateTime.Now.AddMonths(-2);
                string birgunoncesi2 = birgunoncesi.ToString();
              
                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @birgunoncesi  ", con);
                    cmd.Parameters.AddWithValue("birgunoncesi", birgunoncesi2);
                    da = new OleDbDataAdapter(cmd);
                    tbl = new DataTable();
                    da.Fill(tbl);
                    dataGridView1.DataSource = tbl;

                    gridsatirsayisi = dataGridView1.Rows.Count - 1;
                    label8.Text = gridsatirsayisi.ToString();
                    dataGridView1.CurrentCell = null;
                    duzenlemebekleyenler();


                }

                catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            
            }
            void tarihleriekle() {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@"R:\Katalog\desenler\");
                    FileInfo[] files = di.GetFiles("*.jpg");
                    con.Open();
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;

                    progressBar1.Maximum = Directory.GetFiles("R:\\Katalog\\desenler", "*.jpg", SearchOption.AllDirectories).Length;
                    OleDbCommand dm = new OleDbCommand();
                    foreach (FileInfo fi in files)
                    {
                        label9.Text = "tarihler işleniyor. " + fi.Name;
                        progressBar1.Value++;
                        dm.CommandText = "update desenler set eklenmezamani='" + fi.CreationTime + "' where images='" + fi.Name + "'";
                        dm.Connection = con;
                        dm.ExecuteNonQuery();


                    }
                    con.Close();

                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                    label9.Text = "";
                    MessageBox.Show("İşlemler sorunsuz tamamlandı.", "Başarılı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata");
                    hatamailigönder("tarihleri güncelleme yaparken kırmızı buton ile. " + ex.Message, "kırmızı buton");
                }
             
            }
            private void button18_Click(object sender, EventArgs e)
            {
                if (textBox1.Text!="Galatasaray1453")
                {
                   
                    MessageBox.Show("Gizli parola girili olmadığı için button kullanıma kapatılmıştır. Lütfen parolayı girdikten sonra programı kapatıp tekrar deneyiniz. "+Environment.NewLine+"Parola İpucu: txt1.text,1905İstanbul ","Yetkisiz kullanım.",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                   
                    if (MessageBox.Show("Bu buton veritabanındaki tarih verilerini fotoğrafların dosya değiştirilme tarihleriyle değiştirir. Bilinçsiz kullanımlar olumsuz etkiler doğurabilir. Devem etmek istiyor musunuz?", "DİKKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (MessageBox.Show("Emin misiniz?", "DİKKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {

                            Thread basla = new Thread(new ThreadStart(tarihleriekle));
                            basla.Start();


                        }
                    }
                    hatamailigönder("KIRMIZI BUTON özelliği çalıştırıldı.", "KIRMIZI BUTONA BASILDI.");
                }
               
            }

            private void checkBox9_MouseHover(object sender, EventArgs e)
            {
                //Image.FromFile(Application.StartupPath + "\\bosoval.png");
            }

            private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
            {
               
            }

            private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
              { 
            }

            private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            {
          
            }

            private void toolStripTextBox5_Click(object sender, EventArgs e)
            {
                toolStripTextBox5.Text = "";
            }

            private void günİçindeEklenelerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (toolStripTextBox5.Text==""||toolStripTextBox5.Text=="Gün sayısı giriniz.")
                {
                    MessageBox.Show("Lütfen gün sayısı giriniz.","Gün sayısı girilmedi.",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
                else
                {

                    try
                    {
                        DateTime biryıl = DateTime.Now.AddDays(-int.Parse(toolStripTextBox5.Text));
                        string biryıl2 = biryıl.ToString();
                        OleDbCommand cmd = new OleDbCommand("Select *from desenler where eklenmezamani >= @biryıl  ", con);
                        cmd.Parameters.AddWithValue("biryıl", biryıl2);
                        da = new OleDbDataAdapter(cmd);
                        tbl = new DataTable();
                        da.Fill(tbl);
                        dataGridView1.DataSource = tbl;

                        gridsatirsayisi = dataGridView1.Rows.Count - 1;
                        label8.Text = gridsatirsayisi.ToString();
                        dataGridView1.CurrentCell = null;
                        duzenlemebekleyenler();


                    }

                    catch (OleDbException) { con.Close(); MessageBox.Show("R:\\Katalog\\desenler konumuna erişilemiyor. İnternet bağlantınızı kontrol ediniz.", "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    catch (Exception ex) {

                        MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        hatamailigönder("Hata; "+ex.Message,"listeleme yaparken");
                    
                    }
                
                }
            }

            private void pictureBox6_Click(object sender, EventArgs e)
            {
                textBox1.Text = "";
            }

            private void otomatikHataVeLogMailiGönderToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Yapımcıya otomatik olarak Hata,Kullanım istatislikleri,Bilgisayar ve İşletim Sistemi bilgileri gönderilir."+Environment.NewLine+Environment.NewLine+"Bu işlem programın her zaman stabil kalmasına ve yapımı tamamen ücretsiz olan Desen Arama programının yapımcısına bilgi bahşişi vermenizi sağlar. Kişisel hiçbir bilginiz öğrenilmez,paylaşılmaz tamamen program istatislikleri ile sınırlıdır. Anlayışınız İçin Teşekkür Ederim. ","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            private void toolTip1_Popup(object sender, System.Windows.Forms.PopupEventArgs e)
            {

            }

            private void katalogKonumunuAçToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void çıkanDesenlerKonumunaGitToolStripMenuItem_Click(object sender, EventArgs e)
            {
             
            }

            private void katalogKonumunaGitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start(@"R:\Katalog\desenler");
            }

            private void çıkanDesenlerKonumunaGitToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start(@"R:\Katalog\desenler");
            }

            private void sıralamaToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void Form1_Leave(object sender, EventArgs e)
            {
            
            }

            private void Form1_Move(object sender, EventArgs e)
            {
               
            }

            private void Form1_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
            {
               
            }

            private void düzenlemeBekleyenleriçinYazıRengiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                    
                    Settings1.Default.Dyazırengi = colorDialog1.Color;
                    Settings1.Default.Save();
                    duzenlemebekleyenler();
                }
              
            }

            private void düzenlemeBekleyenlerŞuRenkteGözüksünToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                  
                    Settings1.Default.Darkaplan = colorDialog1.Color;
                    Settings1.Default.Save();
                    duzenlemebekleyenler();
                }
            }

            private void çıkanDesenlerİçinYazıRengşToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                
                    Settings1.Default.Cyazırengi = colorDialog1.Color;
                    Settings1.Default.Save();
                    duzenlemebekleyenler();
                }
            }

            private void çıkanDesenlerŞuRenkteGözüksünToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult tus;
                tus = colorDialog1.ShowDialog();
                if (tus == DialogResult.OK)
                {
                  
                    Settings1.Default.Carkaplan = colorDialog1.Color;
                    Settings1.Default.Save();
                    duzenlemebekleyenler();
                }
            }

            private void tümTercihlerSıfırlaVeVarsyalıanAyarlaraDönToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Settings1.Default.Reset();
                Settings1.Default.Save();
                if (MessageBox.Show("Ayarların uygulanabilmesi için programı yeniden başlatılması gerekli."+Environment.NewLine+"Yeniden başlatılsın mı?", "Dikkat",MessageBoxButtons.YesNo, MessageBoxIcon.Information )==DialogResult.Yes) 
                {
                    Application.Restart();
                }
                else
	{
        ;
	}
            }
          
            private void checkBox12_CheckedChanged(object sender, EventArgs e)
            {
               
            }

            private void label1_VisibleChanged(object sender, EventArgs e)
            {
                label1.Visible = true;
            }

            private void checkBox11_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void button40_Click(object sender, EventArgs e)
            {
               
            }

            private void button39_Click(object sender, EventArgs e)
            {
                
            }

            private void button41_Click(object sender, EventArgs e)
            {
               
            }

            private void button42_Click(object sender, EventArgs e)
            {
            try
            {
                ss.Abort();
                button38.Visible = true;
                button42.Visible = false;
                button42.Enabled = false;
                con.Close();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label9.Text = "";
            }
            catch (Exception)
            {

                ;
            }
            try
            {
                yenile();
            }
            catch (Exception)
            {

                ;
            }

        }

        private void porgramKonumunaGitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start(Application.StartupPath);
            }
            bool gecemodu=false;
            private void toolStripMenuItem3_Click(object sender, EventArgs e)
            {
                if (gecemodu==false)
                {
                    BackColor = System.Drawing.ColorTranslator.FromHtml("#1c1c1c");
                    ForeColor = System.Drawing.ColorTranslator.FromHtml("#9acd32");
                    
                   
                    Settings1.Default.arkaplanrenk = System.Drawing.ColorTranslator.FromHtml("#1c1c1c");
                    Settings1.Default.Garkaplan = System.Drawing.ColorTranslator.FromHtml("#1c1c1c");
               ForeColor = Color.GreenYellow;
               girişYapToolStripMenuItem.ForeColor = Color.GreenYellow;
               çıkışToolStripMenuItem1.ForeColor = Color.GreenYellow;
               sıralamaToolStripMenuItem.ForeColor = Color.GreenYellow;
               listeleToolStripMenuItem.ForeColor =Color.GreenYellow;
               optionsToolStripMenuItem.ForeColor = Color.GreenYellow;
               programToolStripMenuItem.ForeColor = Color.GreenYellow;
               yapımcıToolStripMenuItem.ForeColor = Color.GreenYellow;
               ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.GreenYellow;
               görüşBildirToolStripMenuItem.ForeColor = Color.GreenYellow;
               groupBox1.ForeColor = Color.GreenYellow;
               groupBox2.ForeColor = Color.GreenYellow;
               groupBox3.ForeColor = Color.GreenYellow;
               Settings1.Default.yazirenk = Color.GreenYellow;
               Settings1.Default.Gyazi= Color.GreenYellow;

                toolStripMenuItem3.Image = Image.FromFile(Application.StartupPath + "\\gunduz.png");
                duzenlemebekleyenler();
                gecemodu = true;
                }
                else if (gecemodu==true)
                {

                    BackColor = Color.White;
                    ForeColor = Color.Black;
              
                    Settings1.Default.arkaplanrenk = Color.White;
                    Settings1.Default.Garkaplan = Color.White;
                    ForeColor = Color.Black;
                    girişYapToolStripMenuItem.ForeColor = Color.Black;
                    çıkışToolStripMenuItem1.ForeColor = Color.Black;
                    sıralamaToolStripMenuItem.ForeColor = Color.Black;
                    listeleToolStripMenuItem.ForeColor = Color.Black;
                    optionsToolStripMenuItem.ForeColor = Color.Black;
                    programToolStripMenuItem.ForeColor = Color.Black;
                    yapımcıToolStripMenuItem.ForeColor = Color.Black;
                    ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.Black;
                    görüşBildirToolStripMenuItem.ForeColor = Color.Black;
                    groupBox1.ForeColor = Color.Black;
                    groupBox2.ForeColor = Color.Black;
                    groupBox3.ForeColor = Color.Black;
                    Settings1.Default.yazirenk = Color.Black;
                    Settings1.Default.Gyazi = Color.Black;

                    toolStripMenuItem3.Image = Image.FromFile(Application.StartupPath + "\\gece.png");
                    duzenlemebekleyenler();
                    gecemodu = false;
                }

            }

            private void dataGridView2_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
            {

            }

            private void beyazToolStripMenuItem_Click(object sender, EventArgs e)
            {
                BackColor = Color.White;
                ForeColor = Color.Black;

                Settings1.Default.arkaplanrenk = Color.White;
                Settings1.Default.Garkaplan = Color.White;
                ForeColor = Color.Black;
                girişYapToolStripMenuItem.ForeColor = Color.Black;
                çıkışToolStripMenuItem1.ForeColor = Color.Black;
                sıralamaToolStripMenuItem.ForeColor = Color.Black;
                listeleToolStripMenuItem.ForeColor = Color.Black;
                optionsToolStripMenuItem.ForeColor = Color.Black;
                programToolStripMenuItem.ForeColor = Color.Black;
                yapımcıToolStripMenuItem.ForeColor = Color.Black;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.Black;
                görüşBildirToolStripMenuItem.ForeColor = Color.Black;
                groupBox1.ForeColor = Color.Black;
                groupBox2.ForeColor = Color.Black;
                groupBox3.ForeColor = Color.Black;
                Settings1.Default.yazirenk = Color.Black;
                Settings1.Default.Gyazi = Color.Black;

                Settings1.Default.Save();
                duzenlemebekleyenler();
              
            }

            private void geceModuToolStripMenuItem_Click(object sender, EventArgs e)
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#FF3A1212");
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#9acd32");


                Settings1.Default.arkaplanrenk = System.Drawing.ColorTranslator.FromHtml("#FF3A1212");
                Settings1.Default.Garkaplan = System.Drawing.ColorTranslator.FromHtml("#FF3A1212");
                ForeColor = Color.GreenYellow;
                girişYapToolStripMenuItem.ForeColor = Color.GreenYellow;
                çıkışToolStripMenuItem1.ForeColor = Color.GreenYellow;
                sıralamaToolStripMenuItem.ForeColor = Color.GreenYellow;
                listeleToolStripMenuItem.ForeColor = Color.GreenYellow;
                optionsToolStripMenuItem.ForeColor = Color.GreenYellow;
                programToolStripMenuItem.ForeColor = Color.GreenYellow;
                yapımcıToolStripMenuItem.ForeColor = Color.GreenYellow;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.GreenYellow;
                görüşBildirToolStripMenuItem.ForeColor = Color.GreenYellow;
                groupBox1.ForeColor = Color.GreenYellow;
                groupBox2.ForeColor = Color.GreenYellow;
                groupBox3.ForeColor = Color.GreenYellow;
                Settings1.Default.yazirenk = Color.GreenYellow;
                Settings1.Default.Gyazi = Color.GreenYellow;
                Settings1.Default.Save();
              
                duzenlemebekleyenler();
            }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void gizleGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == true)
            {
                groupBox1.Visible = false;
                groupBox3.Visible = true;
                Settings1.Default.kısayol = true;
                Settings1.Default.yoneticipanel = false;
                gizleGösterToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\göster2.png");
            }
            else
            {
                groupBox1.Visible = true;
                groupBox3.Visible = false;
                Settings1.Default.kısayol = false;
                Settings1.Default.yoneticipanel = true;
                gizleGösterToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + @"\gizle2.png");
            }

            Settings1.Default.Save();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            try
            {
                ototmzbaslat.Abort();
                button9.Visible = true;
                button40.Visible = false;
                button40.Enabled = false;
                con.Close();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label9.Text = "";
            }
            catch (Exception)
            {

                ;
            }
            try
            {
                yenile();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void button39_Click_1(object sender, EventArgs e)
        {
            try
            {
                baslat.Abort();
                button4.Visible = true;
                button39.Visible = false;
                button39.Enabled = false;
                con.Close();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label9.Text = "";
            }
            catch (Exception)
            {

                ;
            }
            try
            {
                yenile();
            }
            catch (Exception)
            {

                ;
            }

        }

        private void button41_Click_1(object sender, EventArgs e)
        {
            try
            {
                baslatkont.Abort();
                button14.Visible = true;
                button41.Visible = false;
                button41.Enabled = false;
                con.Close();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                label9.Text = "";
            }
            catch (Exception)
            {

                ;
            }
            try
            {
                yenile();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button43_Click(object sender, EventArgs e)
        {
            gorev gorevata = new gorev();
            gorevata.Show();
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b1!="")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b1.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b2 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b2.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b3 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b3.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b4 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b4.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b5 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b5.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            if (Settings1.Default.b6 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b6.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (Settings1.Default.b7 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b7.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (Settings1.Default.b8 != "")
            {
                combo1doldur();
                comboBox1.Text = Settings1.Default.b8.ToString();
            }
            else
            {
                MessageBox.Show("Bu buton için herhangi bir görev atanmadı. Lütfen görev atayınız.", "İşsiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void oturumuAçıkTutToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                Settings1.Default.siyaharkaplan = true;

            }
            else
            {
                Settings1.Default.siyaharkaplan = false;

            }
            Settings1.Default.Save();
        }

        private void temaSeçToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void kırmızıBeyazToolStripMenuItem_Click(object sender, EventArgs e)
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#8b0000");
                ForeColor = Color.White;


                Settings1.Default.arkaplanrenk = System.Drawing.ColorTranslator.FromHtml("#8b0000");
                Settings1.Default.Garkaplan = System.Drawing.ColorTranslator.FromHtml("#8b0000");
                ForeColor = Color.White;
                girişYapToolStripMenuItem.ForeColor = Color.White;
                çıkışToolStripMenuItem1.ForeColor = Color.White;
                sıralamaToolStripMenuItem.ForeColor = Color.White;
                listeleToolStripMenuItem.ForeColor = Color.White;
                optionsToolStripMenuItem.ForeColor = Color.White;
                programToolStripMenuItem.ForeColor = Color.White;
                yapımcıToolStripMenuItem.ForeColor = Color.White;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.White;
                görüşBildirToolStripMenuItem.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                groupBox2.ForeColor = Color.White;
                groupBox3.ForeColor = Color.White;
                Settings1.Default.yazirenk = Color.White;
                Settings1.Default.Gyazi = Color.White;
                Settings1.Default.Save();

                duzenlemebekleyenler();
            }

            private void ekstraToolStripMenuItem_Click(object sender, EventArgs e)
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#2d002d");
                ForeColor = Color.White;


                Settings1.Default.arkaplanrenk = System.Drawing.ColorTranslator.FromHtml("#2d002d");
                Settings1.Default.Garkaplan = System.Drawing.ColorTranslator.FromHtml("#2d002d");
                ForeColor = Color.White;
                girişYapToolStripMenuItem.ForeColor = Color.White;
                çıkışToolStripMenuItem1.ForeColor = Color.White;
                sıralamaToolStripMenuItem.ForeColor = Color.White;
                listeleToolStripMenuItem.ForeColor = Color.White;
                optionsToolStripMenuItem.ForeColor = Color.White;
                programToolStripMenuItem.ForeColor = Color.White;
                yapımcıToolStripMenuItem.ForeColor = Color.White;
                ağSürücüsüneBağlanToolStripMenuItem.ForeColor = Color.White;
                görüşBildirToolStripMenuItem.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                groupBox2.ForeColor = Color.White;
                groupBox3.ForeColor = Color.White;
                Settings1.Default.yazirenk = Color.White;
                Settings1.Default.Gyazi = Color.White;
                Settings1.Default.Save();

                duzenlemebekleyenler();
            }

            private void checkBox11_CheckedChanged_1(object sender, EventArgs e)
            {

            }

            private void pictureBox2_Click(object sender, EventArgs e)
            {

            }

       
        }
    }




















