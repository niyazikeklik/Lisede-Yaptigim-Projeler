using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweety.Class;
using Twtttter;

namespace Tweety
{
    public partial class Listelistele : Form
    {
        public Listelistele()
        {
            InitializeComponent();
        }

        private void Listelistele_Load(object sender, EventArgs e)
        {

        }
        public Anaekran anaform;
        ArrayList kontroledilenler = new ArrayList();
        void ListGridOlustur()
        {
            bunifuCustomDataGrid2.Columns.Clear();
            
            DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn
            {
                Name = "Sıra",
                Width = 45
            };
            bunifuCustomDataGrid2.Columns.Add(sira);
            DataGridViewImageColumn imgg = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Stretch,
                HeaderText = "Profil Fotoğrafı",
                Width = 96
            };
            imgg.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(imgg);
            DataGridViewTextBoxColumn ad = new DataGridViewTextBoxColumn
            {
                Name = "İsmi",
                Width = 140
            }; ad.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //  ad.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(ad);
            DataGridViewLinkColumn ProfilBilgileri = new DataGridViewLinkColumn
            {
                Name = "Profili",
                Width = 135
            };
            ProfilBilgileri.DefaultCellStyle.SelectionForeColor = Color.White;
            //ProfilBilgileri.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(ProfilBilgileri);
            DataGridViewTextBoxColumn begenisayisi = new DataGridViewTextBoxColumn
            {
                Name = "Liste",
                Width = 55,
            };
            // begenisayisi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(begenisayisi);
            DataGridViewTextBoxColumn fotografyolu = new DataGridViewTextBoxColumn
            {
                Name = "Fotoğraf Yolu",
                Width = 0,
                Visible = false
            };
            bunifuCustomDataGrid2.Columns.Add(fotografyolu);
            DataGridViewTextBoxColumn takipedilme = new DataGridViewTextBoxColumn
            {
                Name = "Takip Edilme",
                Width = 90,
            };
            bunifuCustomDataGrid2.Columns.Add(takipedilme);
            DataGridViewTextBoxColumn takip = new DataGridViewTextBoxColumn
            {
                Name = "Takip",
                Width = 90,
            };
            // begenisayisi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(takip);
        }
        void Listele() {
         //   anaform.driver.Navigate().GoToUrl("https://mobile.twitter.com/" + anaform.kullaniciadi + "/lists");
          //  anaform.SayfaLoadBekle();
         ///   Thread.Sleep(2000);
           // string listeurl = anaform.ListeUrlBulucu(modernTextBox2.Text).Replace("info", "members");
          string listeurl = modernTextBox2.Text + "/members";
            
            if (listeurl != "")
            {
                anaform.driver.Navigate().GoToUrl(listeurl);
                anaform.SayfaLoadBekle();
                Thread.Sleep(2000);
                string kapsayiciHTML;
                string kapsayiciText;
                bool control = true;
                while (control)
                {
                    int listelenen_kullanicisayisi = Convert.ToInt32(anaform.ReturnKomutCalistir("return document.querySelectorAll('[data-testid=UserCell]').length;"));
                    for (int i = 0; i < listelenen_kullanicisayisi; i++)
                    {
                        try
                        {
                            kapsayiciHTML = anaform.js.ExecuteScript(anaform.Komutlar.KapsayiciHTML.Replace("xxxx", i.ToString())).ToString();
                            kapsayiciText = anaform.js.ExecuteScript(anaform.Komutlar.kapsayiciText.Replace("xxxx", i.ToString())).ToString();
                        }
                        catch (Exception) { Thread.Sleep(120); continue; }
                        string[] tektek = kapsayiciText.Split('\n');
                        string kullaniciadi = tektek[1];
                        kapsayiciText = kapsayiciText.Replace(kullaniciadi, "");
                        if (!kontroledilenler.Contains(kullaniciadi))
                        {
                            kontroledilenler.Add(kullaniciadi);
                            string fotograf_url = anaform.Bul(kapsayiciHTML, "(&quot;", "&quot;");
                            if (kapsayiciHTML.Contains("Korumalı hesap")) fotograf_url = "Kilitli" + fotograf_url;
                            string isim = tektek[0];
                            string takipedilmedurumu = "";
                            string takipetmedurumu = "";
                            if (detayToolStripMenuItem.Checked)
                            {
                                anaform.js.ExecuteScript(anaform.Komutlar.KullanıcıProfilineGiris.Replace("xxxx", i.ToString()));
                                string profil = anaform.ReturnKomutCalistir("return document.getElementsByClassName(\"css-1dbjc4n r-ku1wi2 r-1j3t67a r-1b3ntt7\")[0].innerText;");
                                if (profil.Contains("Takip ediliyor")) takipedilmedurumu = "Takip ediliyor";
                                else takipedilmedurumu = "Takip et";
                                if (profil.Contains("Seni takip ediyor")) takipetmedurumu = "Seni takip ediyor";
                                else takipetmedurumu = "Takip etmiyor";
                                anaform.js.ExecuteScript(anaform.Komutlar.GeriCik);
                            }
                           
                            bunifuCustomDataGrid2.Rows.Add(bunifuCustomDataGrid2.Rows.Count + 1, fotografİslemleri.ResimOlustur(fotograf_url, kullaniciadi, Color.White), isim, kullaniciadi, modernTextBox2.Text, fotograf_url,takipetmedurumu,takipedilmedurumu);
                        }
                    }
                    double onceki_konum = Convert.ToDouble(anaform.js.ExecuteScript(anaform.Komutlar.ScrollBarKonumu));
                    anaform.js.ExecuteScript(anaform.Komutlar.SayfaKaydir.Replace("1000","500")); Thread.Sleep(200);
                    double sonraki_konum = Convert.ToDouble(anaform.js.ExecuteScript(anaform.Komutlar.ScrollBarKonumu));
                    if (sonraki_konum == onceki_konum) control = false;
                }

            }
            else
            {
                MessageBox.Show("Liste Bulunamadı");
            }
        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            ListGridOlustur();
            Thread baslat = new Thread(new ThreadStart(Listele));
            baslat.Start();

            





            /**/
        }

        private void listeListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void listeSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            buyut yeni2 = new buyut();
            string fotograf = "";
            if (e.ColumnIndex == 1)
            {
                try
                {
          
                    yeni2.Hide();
                    int x, y;
                    x = Cursor.Position.X;
                    y = Cursor.Position.Y;
                    yeni2.Width = 500;
                    yeni2.Height = 500;
                    if (y < Screen.PrimaryScreen.Bounds.Height / 2) yeni2.Location = new System.Drawing.Point(x, y);
                    else yeni2.Location = new System.Drawing.Point(x, y - 500);
                    fotograf = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("x96", "400x400");
                    if (fotograf.Contains("Kilitli"))
                    {
                       
                        fotograf = fotograf.Replace("Kilitli", "");
                    }
                    yeni2.pictureBox1.Load(fotograf);
                    yeni2.ShowDialog();
                }
                catch (ArgumentOutOfRangeException)
                {
                    ;
                }

            }
            else if (e.ColumnIndex == 3)
            {
                try
                {
                    string lnk = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    lnk = "https://www.twitter.com/" + lnk;
                    ProcessStartInfo psinfo = new ProcessStartInfo(lnk);
                    Process.Start(psinfo);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ;
                }
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
           
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from beyazliste", anaform.baglanti);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            bunifuCustomDataGrid2.DataSource = da;
        }

        private void listedenKaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                string kaldıralacakkisi = drow.Cells[3].Value.ToString();
                anaform.js.ExecuteScript(anaform.Komutlar.SurayaKaydir.Replace("xxx", "0"));
                bool control = true;
                while (control)
                {
                    string kapsayiciText;
                    int listelenen_kullanicisayisi = Convert.ToInt32(anaform.ReturnKomutCalistir("return document.querySelectorAll('[data-testid=UserCell]').length;"));
                    for (int i = 0; i < listelenen_kullanicisayisi; i++)
                    {
                        try
                        {
                            kapsayiciText = anaform.js.ExecuteScript(anaform.Komutlar.kapsayiciText.Replace("xxxx", i.ToString())).ToString();
                        }
                        catch (Exception) { Thread.Sleep(120); continue; }
                        string[] tektek = kapsayiciText.Split('\n');
                        string kullaniciadi = tektek[1];
                        if (kullaniciadi == kaldıralacakkisi)
                        {
                            anaform.KomutCalistir("document.querySelectorAll('[data-testid=UserCell]')[" + i + "].children[0].children[1].children[0].children[1].click();");
                            control = false;
                            break; 
                        }
                    }
                    if (!control) {  break; }
                    double onceki_konum = Convert.ToDouble(anaform.js.ExecuteScript(anaform.Komutlar.ScrollBarKonumu));
                    anaform.js.ExecuteScript(anaform.Komutlar.SayfaKaydir); Thread.Sleep(750);
                    double sonraki_konum = Convert.ToDouble(anaform.js.ExecuteScript(anaform.Komutlar.ScrollBarKonumu));
                    if (sonraki_konum == onceki_konum) { MessageBox.Show("Kişi bulunamadı: " + kaldıralacakkisi); control = false; }
                }

                
            }
            MessageBox.Show("Kaldırıldı!");
         
          

           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          
        }

        private void detayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaform.CheckBoxDonustur((ToolStripMenuItem)sender);
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void simgeDurumuToolStripMenuItem_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
