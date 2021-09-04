using AngleSharp;
using Microsoft.Win32;
using net.zemberek.erisim;
using net.zemberek.tr.yapi;
using ns1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tweety;
using Tweety.Properties;
namespace Twtttter {
    public partial class Anaekran : Form {
        public string kullaniciadi = "", sifre;
        public string onaykodu = "";
        public IWebDriver driver;
        public IWebDriver driver2;
        public IJavaScriptExecutor js;
        public IJavaScriptExecutor js2;
        private Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        public ArrayList kontroledildi = new ArrayList();
        public Komutlarr Komutlar = new Komutlarr();
        private Random rnd1 = new Random();
        private yukleniyor yeni = new yukleniyor();
        private buyut yeni2;
        public bool performans = false, kayittangetir = false;
        public bool takipci_filtresi = true, veritabanındancek = true, islemyapildimi;
        private bool detayvisible = true;
        private bool fotograf_hatali;
        public OleDbConnection baglanti;
        private OleDbCommand komut;
        private OleDbCommand cmd = new OleDbCommand();
        private DateTime baslamaTarihi;
        private DateTime bitisTarihi;
        private TimeSpan kalangun;
        private ArrayList tum_tweetleri_begenenler = new ArrayList();
        private ArrayList tweetler = new ArrayList();
        private ArrayList gecerli_tweeti_begenenler = new ArrayList();
        private ArrayList takipciler = new ArrayList();
        public int listesecenek = -1;
        public int kontrol_edilecek_tweet_sayisi = 0, basilanbuton = -1;
        private int kontrol_edilen_tweet_Sayisi = 0, begenisay, count = 1;
        private int tweet_sayisi = 0, sayfadaki_classsayisi, kacinciay, yil;
        private string siradaki_tweet = "", siradaki_begenen = "", siradaki_begenen_ad = "";
        private string fotograf_url = "", takip_ediyormu = "", takipedilmedurumu = "";
        private string kayittarihi = "", tweetsayisi = "", biyografi = "";
        private string takipedilen = "", takipcisayisi = "", begenisayisi = "";
        private string konum = "", cinsiyet = "Belirsiz", fotograf = "", ayadi;
        private string kapsayiciHTML = "", kapsayiciText = "";
        private string guncellink = "", line;
        private string[,] begenenler_matris;
        private string[,] takipciler_matris;
        private double toplamGun = 0, gunlukbegeni, gunluktweet;

        public Anaekran() {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private Bitmap ClipToCircle(Image srcImage, PointF center, float radius, Color backGround) {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            try {
                using(Graphics g = Graphics.FromImage(dstImage)) {
                    RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                                                             radius * 2, radius * 2);
                    // enables smoothing of the edge of the circle (less pixelated)
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    // fills background color
                    using(Brush br = new SolidBrush(backGround)) {
                        g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                    }
                    // adds the new ellipse & draws the image again
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(r);
                    g.SetClip(path);
                    g.DrawImage(srcImage, 0, 0);
                    return dstImage;
                }
            }
            catch(Exception) {
                return dstImage;
            }
        }
        private Bitmap ScreenShot(IWebDriver driver) {
            Screenshot myScreenShot = ((ITakesScreenshot) driver).GetScreenshot();
            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));
            //  Bitmap elemScreenshot = screen.Clone(new Rectangle(elem.Location, elem.Size), screen.PixelFormat);
            //    screen.Dispose();
            return screen;
        }
        private void run_cmd(string cmd, string args) {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = cmd;//cmd is full path to python.exe
            start.Arguments = args;//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = false;
           
            using(Process process = Process.Start(start)) {
                using(StreamReader reader = process.StandardOutput) {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        public Bitmap ResimOlustur(string url, string kullaniciadi, Color renk) {
            Bitmap HazirResim;
            if(url.Contains("Kilitli")) HazirResim = ResimCiz(Resim(url.Replace("Kilitli", ""), kullaniciadi), kullaniciadi);
            else HazirResim = Resim(url.Replace("Kilitli", ""), kullaniciadi);
            //HazirResim.Save(@"Yardımcı\Gender-and-Age-Prediction-from-Face-Images-master\src\input\tespit.jpg");
            //System.Diagnostics.Process.Start(@"Yardımcı\Gender-and-Age-Prediction-from-Face-Images-master\src\calistir.bat");
            //      Thread.Sleep(3000);
           // run_cmd(@"C:\Users\niyaz\AppData\Local\Programs\Python\Python39\python.exe", "D:\\Google Drive Otomatik Yedekleme\\Projeler\\Açık Kaynak Kodları\\C# Windows Forms Application\\Twtttter\\Twtttter\\bin\\Debug\\Yardımcı\\Gender-and-Age-Prediction-from-Face-Images-master\\src\\sample.py --input=input/tespit.jpg --output=deneme.png");
           // string path = "Yardımcı/Gender-and-Age-Prediction-from-Face-Images-master/src/output/";
           // string dosyaAdi = Path.GetFileName(Directory.GetFiles(path)[0]);
           // run_cmd(@"C:\Users\niyaz\AppData\Local\Programs\Python\Python39\python.exe", "D:\\Google Drive Otomatik Yedekleme\\Projeler\\Açık Kaynak Kodları\\C# Windows Forms Application\\Twtttter\\Twtttter\\bin\\Debug\\Yardımcı\\Gender-and-Age-Prediction-from-Face-Images-master\\src\\sample.py --input=input/tespit.jpg --output=deneme.png");
          //  MessageBox.Show(dosyaAdi);
       //     File.Delete(path + "/" + dosyaAdi);
     //       File.Delete(@"Yardımcı\Gender-and-Age-Prediction-from-Face-Images-master\src\input\tespit.jpg");
            return ClipToCircle(HazirResim, new PointF(HazirResim.Width / 2, HazirResim.Height / 2), HazirResim.Width / 2, renk);
        }
        private Bitmap Resim(string Url, string kullanici_id) {
            guncellink = "";
            fotograf_hatali = false;
            try {
                WebRequest rs = WebRequest.Create(Url.Replace("Kilitli", ""));
                return (Bitmap) Bitmap.FromStream(rs.GetResponse().GetResponseStream());
            }
            catch(Exception) {
                /* try
                 {
                     WebRequest rs = WebRequest.Create("https://unavatar.now.sh/twitter/" + kullanici_id.Replace("@", ""));
                     return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
                 }
                 catch (Exception)
                 {*/
                fotograf_hatali = true;
                guncellink = FotografUrl(kullanici_id);
                if(guncellink == "-1") return (Bitmap) Image.FromFile(@"img\fotograf_errorx96.jpg");
                VeritabaniGuncelle(kullanici_id, "begenenler", "fotografyolu", guncellink);
                VeritabaniGuncelle(kullanici_id, "takipciler", "fotografyolu", guncellink);
                try {
                    WebRequest rs = WebRequest.Create(guncellink.Replace("Kilitli", ""));
                    return (Bitmap) Bitmap.FromStream(rs.GetResponse().GetResponseStream());
                }
                catch(Exception) {
                    return (Bitmap) Image.FromFile(@"img\fotograf_errorx96.jpg");
                }
                //  } 
            }
        }
        private Bitmap ResimCiz(Bitmap Resim, string kullanici_id) {
            try {
                Graphics g = Graphics.FromImage(Resim);
                // g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage((Image) Image.FromFile(@"img\lock.jpg"), new System.Drawing.Point() { X = -10, Y = -10 });
                return Resim;
            }
            catch(Exception) {
                WebRequest rs = WebRequest.Create("https://abs.twimg.com/sticky/default_profile_images/default_profile_x96.png");
                return (Bitmap) Bitmap.FromStream(rs.GetResponse().GetResponseStream());
            }
        }
        private string Donustur(string metin) {
            metin = metin.Replace(" ", "");
            if(metin.IndexOf(',') == -1) {
                metin = metin.Replace("B", "000");
            }
            else {
                metin = metin.Replace("B", "00");
            }
            metin = metin.Replace(",", "");
            metin = metin.Replace(".", "").Replace(" ", "");
            return metin;
        }
        private string CinsiyetBul(string isim1) {

          
            string cins = "Belirsiz";
            string[] isimler = isim1.Split(' ');
            string isim = isimler[0].Replace("'", "").Replace(".", "").Replace(",", "");
            int index = line.IndexOf("'" + isim + "'");
            if(index == -1 || isim.Length <= 1) {
                return cins;
                /* if (isim.Length >= 2)
                   {
                       var suggestions = zemberek.oner(isim);
                       if (suggestions.Any())
                       {
                           for (int i = 0; i < suggestions.Length; i++)
                           {
                               index = line.IndexOf("'" + suggestions[i] + "'");
                               if (index != -1) return CinsiyetBul(suggestions[i]);
                           }
                           return cins;
                       }
                       else return cins;
                   }
                   else return cins;*/
            }
            else {
                cins = line.Substring(index + isim.Length + 5, 1);
                if(cins == "k") cins = "Kadın";
                else if(cins == "e") cins = "Erkek";
                else if(cins == "u") cins = "Unisex";
                return cins;
            }
        }
        public string Bul(string kapsayici, string oncesi, string sonrasi = "</span>") {
            try {
                int baslangic = kapsayici.IndexOf(oncesi) + oncesi.Length;
                int bitis = kapsayici.IndexOf(sonrasi, baslangic);
                return kapsayici.Substring(baslangic, bitis - baslangic);
            }
            catch(Exception) {
                if(kapsayici.Contains("Böyle bir hesap yok")) {
                    return "-1";
                }
                MessageBox.Show("İstenilen metin bulunamadı. Hata CODE X100 ");
                return "-1";
            }
        }
        public string ListeUrlBulucu(string bulunacakliste) {
            int listesayisi = Convert.ToInt32(js.ExecuteScript("return document.getElementsByClassName(\"" + Komutlar.ClassNameListe + "\").length"));
            for(int i = 0; i < listesayisi; i++) {
                if(js.ExecuteScript("return document.getElementsByClassName(\"" + Komutlar.ClassNameListe + "\")[" + i.ToString() + "].innerText;").ToString().Contains(bulunacakliste))
                    return "https://mobile.twitter.com" + js.ExecuteScript("return document.getElementsByClassName(\"" + Komutlar.ClassNameListe + "\")[" + i + "].getAttribute(\"href\");").ToString() + "/info";
            }
            return "";
        }
        public string FotografUrl(string UserName) {
            int count = 0;
        tekrarbirdene:
            try {
                js2.ExecuteScript("document.location.href = \"" + UserName + "\";");
                profilYuklendiMiDrv2();
                string url = driver2.FindElement(By.CssSelector("#react-root > div > div > div.css-1dbjc4n.r-13qz1uu.r-417010 > main > div > div > div > div > div > div > div > div > div:nth-child(2) > div > div > div:nth-child(1) > div > div.css-1dbjc4n.r-obd0qt.r-18u37iz.r-1w6e6rj.r-1wtj0ep > a > div.css-1dbjc4n.r-1adg3ll.r-1udh08x > div.r-1p0dtai.r-1pi2tsx.r-1d2f490.r-u8s1d.r-ipm5af.r-13qz1uu > div > img")).GetAttribute("src").Replace("200x200", "x96");
                return url;
            }
            catch(Exception) {
                count++;
                if(count < 10) {
                    Thread.Sleep(50);
                    goto tekrarbirdene;
                }
                else
                    return "-1";
            }
        }
        public string VarsayılanTarayıcı() {
            string browserName = "iexplore.exe";
            using(RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")) {
                if(userChoiceKey != null) {
                    object progIdValue = userChoiceKey.GetValue("Progid");
                    if(progIdValue != null) {
                        if(progIdValue.ToString().ToLower().Contains("chrome"))
                            browserName = "chrome.exe";
                        else if(progIdValue.ToString().ToLower().Contains("firefox"))
                            browserName = "firefox.exe";
                        else if(progIdValue.ToString().ToLower().Contains("safari"))
                            browserName = "safari.exe";
                        else if(progIdValue.ToString().ToLower().Contains("opera"))
                            browserName = "opera.exe";
                        else if(progIdValue.ToString().ToLower().Contains("edge"))
                            browserName = "edge.exe";
                        return browserName;
                    }
                    return browserName;
                }
                return browserName;
            }
        }
        public string YukluTarayıcılar() {
            string yuklutarayıcılar = "";
            if(dosyakontrol(@"Microsoft\Edge\Application\msedge.exe")) yuklutarayıcılar += "Edge";
            if(dosyakontrol(@"Google\Chrome\Application\chrome.exe")) yuklutarayıcılar += "Chrome";
            if(dosyakontrol(@"Mozilla Firefox\firefox.exe")) yuklutarayıcılar += "Firefox";
            if(File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Programs\Opera\launcher.exe")) yuklutarayıcılar += "Opera";
            return yuklutarayıcılar;
        }
        public string ReturnKomutCalistir(string komut) {
            int calisma_sayisi = 0;
        ogeyibekle1:
            try {
                return js.ExecuteScript(komut).ToString();
            }
            catch(Exception ex) {
                if(calisma_sayisi != 150) {
                    Thread.Sleep(5);
                    calisma_sayisi++;
                    goto ogeyibekle1;
                }
                else {
                    MessageBox.Show("Sonsuz döngüye girildi." + Environment.NewLine + "Hata Mesajı: " + ex.Message);
                    return "-1";
                }
            }
        }
        private void VeritabanıTemizle(string temizle_tablo) {
            cmd.Connection = baglanti;
            cmd.CommandText = "delete from " + temizle_tablo + "\"";
            cmd.ExecuteNonQuery();
        }
        private void VeritabanındanGetir(string tabloadi) {
            //string bozukurl = "";
            bunifuCustomDataGrid2.Rows.Clear();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from " + tabloadi + "\"", baglanti);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            yeni.modernLabel1.Text = "Veritabanından kayıtlar çekildi.";
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.bunifuCircleProgressbar1.MaxValue = tbl.Rows.Count + 1;
            for(int i = 0; i < tbl.Rows.Count; i++) {
                try {
                    GridSatirEkle(
                        Convert.ToInt32(tbl.Rows[i]["sira"]),
                        tbl.Rows[i]["ismi"].ToString(),
                        tbl.Rows[i]["profili"].ToString(),
                        Convert.ToInt32(tbl.Rows[i]["begeni"]),
                        tbl.Rows[i]["fotografyolu"].ToString(),
                        tbl.Rows[i]["cinsiyet"].ToString(),
                        tbl.Rows[i]["biyografi"].ToString(),
                        tbl.Rows[i]["konumu"].ToString(),
                        tbl.Rows[i]["following"].ToString(),
                        tbl.Rows[i]["followers"].ToString(),
                        Convert.ToInt32(tbl.Rows[i]["gunduruye"]),
                        Convert.ToDouble(tbl.Rows[i]["gunluklike"]),
                        tbl.Rows[i]["tweet"].ToString(),
                        Convert.ToDouble(tbl.Rows[i]["gunluktweet"]),
                        tbl.Rows[i]["takipdurumu"].ToString(),
                        tbl.Rows[i]["takipet"].ToString()
                        );
                    yeni.bunifuCircleProgressbar1.Value++;
                    yeni.modernLabel2.Text = "Kayıtlar İşleniyor.. " + i.ToString() + "/" + (tbl.Rows.Count).ToString();
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            if(tabloadi == "begenenler") {
                OlusturBegenenlerMatris();
            }
            else if(tabloadi == "takipciler") {
                OlusturTakipcilerMatris();
            }
        }
        string driverurl = "";
        private void TakipEttiklerim() {
            modernLabel4.Text = TakipEdilenSayisi(kullaniciadi).ToString();
            driver.Navigate().GoToUrl("https://mobile.twitter.com/" + kullaniciadi + "/following");
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.None;
            yeni.Show();
            this.Hide();
            if(MessageBox.Show("Kayıttan getirilirsin mi?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                VeritabanındanGetir("takipciler");
            else {
                OleDbDataAdapter da = new OleDbDataAdapter("Select * from takipciler ", baglanti);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                count = 1;
                int index;
                Thread.Sleep(2000);
                driverurl = driver.Url;
                int sayfadaki_classsayisi;
                yeni.bunifuCircleProgressbar1.MaxValue = TakipEdilenSayisi(kullaniciadi) + 1;
                yeni.bunifuCircleProgressbar1.Value = 1;
                yeni.modernLabel2.Text = "...";
                takipciler.Clear();
                DegiskenleriTemizle();
            ram_bosalt:
                do {
                    sayfadaki_classsayisi = Convert.ToInt32(js.ExecuteScript(Komutlar.KullanıcıDivSayısı));
                    for(int x = 0; x < sayfadaki_classsayisi; x++) {
                        if(x >= 14) {
                            ;
                        }
                        try {
                            kapsayiciHTML = js.ExecuteScript(Komutlar.KapsayiciHTML.Replace("xxxx", x.ToString())).ToString();
                            kapsayiciText = js.ExecuteScript(Komutlar.kapsayiciText.Replace("xxxx", x.ToString())).ToString();
                        }
                        catch(Exception) {
                            Thread.Sleep(100);
                            continue;
                        }
                        siradaki_begenen = "@" + Bul(kapsayiciHTML, ">@", "</span>");
                        kapsayiciText = kapsayiciText.Replace(siradaki_begenen, "");

                        count = RamControl(count);
                        if(Limit(driverurl)) goto ram_bosalt;
                        if(!takipciler.Contains(siradaki_begenen)) {
                            KaraListe(siradaki_begenen);
                            index = BegenenlerMatristeAra(siradaki_begenen);
                            if(index != -1) {
                                GridSatirEkle(
                                    bunifuCustomDataGrid2.Rows.Count + 1,
                                    begenenler_matris[index, 2],
                                    begenenler_matris[index, 3],
                                    Convert.ToInt32(begenenler_matris[index, 4]),
                                    begenenler_matris[index, 5],
                                    begenenler_matris[index, 6],
                                    begenenler_matris[index, 7],
                                    begenenler_matris[index, 8],
                                    begenenler_matris[index, 9],
                                    begenenler_matris[index, 10],
                                    Convert.ToInt32(begenenler_matris[index, 11]),
                                    Convert.ToDouble(begenenler_matris[index, 12]),
                                    begenenler_matris[index, 13],
                                    Convert.ToDouble(begenenler_matris[index, 14]),
                                    begenenler_matris[index, 15],
                                    begenenler_matris[index, 16]);
                            }
                            else {
                                DataRow[] filteredRows = tbl.Select(string.Format("{0} LIKE '%{1}%'", "profili", siradaki_begenen));
                                if(filteredRows.Length > 0 && veritabanındancek) {
                                    foreach(DataRow dr in filteredRows) {
                                        GridSatirEkle(
                                          bunifuCustomDataGrid2.Rows.Count + 1,
                                         dr["ismi"].ToString(), dr["profili"].ToString(), Convert.ToInt32(dr["begeni"]),
                                         dr["fotografyolu"].ToString(), dr["cinsiyet"].ToString(),
                                         dr["biyografi"].ToString(), dr["konumu"].ToString(),
                                          dr["following"].ToString(), dr["followers"].ToString(),
                                          Convert.ToInt32(dr["gunduruye"]), Convert.ToDouble(dr["gunluklike"]),
                                          dr["tweet"].ToString(), Convert.ToDouble(dr["gunluktweet"]),
                                         dr["takipdurumu"].ToString(), dr["takipet"].ToString());
                                    }
                                }
                                else {
                                    TemelBilgiler(x, "takipcicek", kapsayiciHTML, kapsayiciText);
                                }
                            }
                            takipciler.Add(siradaki_begenen);
                            yeni.bunifuCircleProgressbar1.Value++;
                            yeni.modernLabel1.Text = "Takipçiler Kontrol Ediliyor " + takipciler.Count + "/" + modernLabel4.Text;
                            if(takipciler.Count == Convert.ToInt32(modernLabel4.Text)) break;
                        }
                    }
                    Kaydir(kullaniciadi);
                } while(takipciler.Count < Convert.ToInt32(modernLabel4.Text));
                OlusturTakipcilerMatris();
                if(MessageBox.Show("Takipçiler kaydedilsin mi? (Eski kayıt varsa silinir.)", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) TakipEttiklerimKaydet();
            }
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.Both;
            this.Show(); yeni.Hide();
        }
        private void OlusturBegenenlerMatris() {
            begenenler_matris = new string[bunifuCustomDataGrid2.Rows.Count, bunifuCustomDataGrid2.Columns.Count];
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                for(int j = 0; j < bunifuCustomDataGrid2.Columns.Count; j++) {
                    begenenler_matris[i, j] = bunifuCustomDataGrid2.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        private void OlusturTakipcilerMatris() {
            takipciler_matris = new string[bunifuCustomDataGrid2.Rows.Count, bunifuCustomDataGrid2.Columns.Count];
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                for(int j = 0; j < bunifuCustomDataGrid2.Columns.Count; j++) {
                    takipciler_matris[i, j] = bunifuCustomDataGrid2.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        private void Kaydir(string kullanici_id) {
            double onceki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
            js.ExecuteScript(Komutlar.SayfaKaydir); Thread.Sleep(750);
            double sonraki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
            if(sonraki_konum == onceki_konum) {
                modernLabel4.Text = TakipEdilenSayisi(kullanici_id).ToString();
                js.ExecuteScript(Komutlar.SurayaKaydir.Replace("xxx", "100"));
                Thread.Sleep(200);
            }
        }
        private void TakipEttiklerimKaydet() {
            try {
                VeritabanıTemizle("takipciler");
                string ekle = "INSERT into takipciler (sira,ismi,profili,begeni,fotografyolu,cinsiyet,biyografi,konumu,following,followers,gunduruye,gunluklike,tweet,gunluktweet,takipdurumu,takipet) Values (@sira,@ismi,@profili,@begeni,@fotografyolu,@cinsiyet,@biyografi,@konumu,@following,@followers,@gunduruye,@gunluklike,@tweet,@gunluktweet,@takipdurumu,@takipet)";
                OleDbCommand komut;
                for(int i = 0; i < takipciler_matris.Length / 17; i++) {
                    komut = new OleDbCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@sira", Convert.ToInt32(takipciler_matris[i, 0]));
                    komut.Parameters.AddWithValue("@ismi", takipciler_matris[i, 2]);
                    komut.Parameters.AddWithValue("@profili", takipciler_matris[i, 3]);
                    komut.Parameters.AddWithValue("@begeni", Convert.ToInt32(takipciler_matris[i, 4]));
                    komut.Parameters.AddWithValue("@fotografyolu", takipciler_matris[i, 5]);
                    komut.Parameters.AddWithValue("@cinsiyet", takipciler_matris[i, 6]);
                    komut.Parameters.AddWithValue("@biyografi", takipciler_matris[i, 7]);
                    komut.Parameters.AddWithValue("@konumu", takipciler_matris[i, 8]);
                    komut.Parameters.AddWithValue("@following", Convert.ToInt32(takipciler_matris[i, 9]));
                    komut.Parameters.AddWithValue("@followers", Convert.ToInt32(takipciler_matris[i, 10]));
                    komut.Parameters.AddWithValue("@gunduruye", Convert.ToInt32(takipciler_matris[i, 11]));
                    komut.Parameters.AddWithValue("@gunluklike", Convert.ToDouble(takipciler_matris[i, 12]));
                    komut.Parameters.AddWithValue("@tweet", Convert.ToInt32(takipciler_matris[i, 13]));
                    komut.Parameters.AddWithValue("@gunluktweet", Convert.ToDouble(takipciler_matris[i, 14]));
                    komut.Parameters.AddWithValue("@takipdurumu", takipciler_matris[i, 15]);
                    komut.Parameters.AddWithValue("@takipet", takipciler_matris[i, 16]);
                    komut.ExecuteNonQuery();
                }
                MessageBox.Show("Başarıyla kaydedildi.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex) { MessageBox.Show("Kaydedilemedi." + ex.Message, "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            /* Directory.CreateDirectory(Application.StartupPath + @"\" + DateTime.Today);
             File.Copy(Application.StartupPath + @"\sablon", Application.StartupPath + @"\" + DateTime.Today);*/
        }
        private void TakiptenCik() {
            int count = 0;
            int Takipedilmiyor_count = 0;
            int beyazListe_count = 0;
            int secilikisisayisi = bunifuCustomDataGrid2.SelectedRows.Count;
            OleDbDataAdapter da = new OleDbDataAdapter("Select kullaniciad from beyazliste ", baglanti);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                DataRow[] filteredRows = tbl.Select(string.Format("{0} LIKE '%{1}%'", "kullaniciad", drow.Cells[3].Value.ToString()));
                if(drow.Cells[16].Value.ToString() == "Takip ediliyor" && ((filteredRows.Length == 0 && beyazListedekiKullanıcılarıTakiptenÇıkmaToolStripMenuItem.Checked) || !beyazListedekiKullanıcılarıTakiptenÇıkmaToolStripMenuItem.Checked)) {
                    driver.Navigate().GoToUrl("https://www.twitter.com/" + drow.Cells[3].Value.ToString());
                    if(ReturnKomutCalistir(Komutlar.TakipEtTakipEdilenKutucukText) == "Takip ediliyor") {
                        if(KomutCalistir(Komutlar.TakiptenCik1)) {
                            if(KomutCalistir(Komutlar.TakiptenCikAcilirPencere)) {
                                count++;
                                drow.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#c41b4d");
                                drow.DefaultCellStyle.ForeColor = Color.White;
                                drow.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#c41b4d");
                                drow.DefaultCellStyle.SelectionForeColor = Color.White;
                                drow.Cells[16].Value = "Takip et";
                                KaraListe(drow.Cells[3].Value.ToString());
                                VeritabaniGuncelle(drow.Cells[3].Value.ToString(), "takipciler", "takipet", "Takip et");
                                VeritabaniGuncelle(drow.Cells[3].Value.ToString(), "begenenler", "takipet", "Takip et");
                                if(toolStripMenuItem18.Checked) Thread.Sleep(rnd1.Next(3, 10) * 1000);
                            }
                        }
                    }
                    else Takipedilmiyor_count++;
                }
                else beyazListe_count++;
            }
            MessageBox.Show(secilikisisayisi + " seçili kişiden, " + count + " kişi takipten çıkıldı." + Environment.NewLine + Environment.NewLine +
                Takipedilmiyor_count + " kişi zaten takip edilmiyordu." + Environment.NewLine + beyazListe_count + " kişi beyaz listede ekli veya veritabanında takip edilmiyor gözüküyordu.", "İşlem bitti");
        }
        private void profilYuklendiMiDrv2() {
            int say = 0;
            while(true) {
                if(js2.ExecuteScript("return document.getElementsByClassName(\"css-1dbjc4n r-1cad53l r-779j7e r-1b9bua6\").length;").ToString() == "1")
                    break;
                if(js2.ExecuteScript("return document.body.innerText;").ToString().Contains("Böyle bir hesap yok"))
                    break;
                say++;
                if(say == 50)
                    break;
                Thread.Sleep(20);
            }
        }
        private void TemelBilgiler(int x, string cagiran, string kapsayiciHTML, string kapsayiciText) {
            string[] tektek = kapsayiciText.Split('\n');
            if(kapsayiciText.Contains("Takip et")) takipedilmedurumu = "Takip et";
            else if(kapsayiciText.Contains("Takip ediliyor")) takipedilmedurumu = "Takip ediliyor";
            else { kapsayiciText = "Takip ediliyor"; takipedilmedurumu = "Takip ediliyor"; }
            kapsayiciText = kapsayiciText.Replace(takipedilmedurumu, "");


            if(takipedilmedurumu != "Takip et" && takipci_filtresi || !takipci_filtresi) {
                fotograf_url = Bul(kapsayiciHTML, "x96.jpg&quot;);\"></div><img alt=\"\" draggable=\"true\" src=\"", "\"");
                if(kapsayiciText.Contains("Seni takip ediyor")) { takip_ediyormu = "Seni takip ediyor"; }
                else takip_ediyormu = "Takip etmiyor";
                kapsayiciText = kapsayiciText.Replace("Seni takip ediyor", "");
                siradaki_begenen_ad = tektek[0].Replace("\r", "").Replace("\n", "");
                cinsiyet = CinsiyetBul(siradaki_begenen_ad.ToLower());
                kapsayiciText = kapsayiciText.Replace(tektek[0], "");
                kapsayiciText = kapsayiciText.Replace("\r", "").Replace("\n", "");
                if(kapsayiciText.Length > 0) biyografi = kapsayiciText;
                else biyografi = "Biyografi yok";
                if(biyografi.Length > 160) biyografi = "Biyografi yok";
                if(!performans) DetayBilgiler(x);

                if(cagiran == "takipcicek") begenisay = 0;
                else {
                    tum_tweetleri_begenenler.Add(siradaki_begenen);
                    tum_tweetleri_begenenler.Add("1");
                    begenisay = 1;
                }
                if(kapsayiciHTML.Contains("Korumalı hesap")) { fotograf_url = "Kilitli" + fotograf_url; }
                yeni.modernLabel2.Text = siradaki_begenen;
                gunlukbegeni = Math.Round(int.Parse(begenisayisi) / toplamGun, 1);
                gunluktweet = Math.Round(int.Parse(tweetsayisi) / toplamGun, 1);
                GridSatirEkle((bunifuCustomDataGrid2.Rows.Count + 1),
                                siradaki_begenen_ad, siradaki_begenen,
                                begenisay, fotograf_url, cinsiyet,
                                biyografi, konum,
                               takipedilen, takipcisayisi,
                                toplamGun, gunlukbegeni,
                                tweetsayisi, gunluktweet,
                                takip_ediyormu, takipedilmedurumu);

            }
        }
        private void BegenenleriKontrolEt() {
            yeni.bunifuCircleProgressbar1.MaxValue = tweetler.Count + 1;
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.modernLabel2.Text = "...";
            sayfadaki_classsayisi = 0;
            for(int i = 0; i < tweetler.Count; i++) {
                yeni.modernLabel1.Text = "Beğeniler Kontrol Ediliyor " + (i + 1).ToString() + "/" + tweetler.Count.ToString();
                if(!BegeniKontrol(tweetler[i].ToString())) continue;
                yeni.bunifuCircleProgressbar1.Value++;
            }
            yeni.modernLabel1.Text = "İşlemler Bitti.";
            yeni.modernLabel1.Text = "Begeniler Listeleniyor..";
            OlusturBegenenlerMatris();
            if(performans) DetayVisible();
            CenterToScreen();
            if(MessageBox.Show("Beğenenler kaydedilsin mi? (Eski kayıt varsa silinir.)", "Kayıt", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes) {
                BegenenleriKaydet();
                this.Show();
                yeni.Hide();
            }
            else {
                this.Show();
                yeni.Hide();
            }
        }

        private void BaskasinininListesi(int kackisibulunsun) {
            Random random = new Random();
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.None;
            OleDbDataAdapter da = new OleDbDataAdapter("Select profili from gtyapmayanlar ", baglanti);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            string driverurl = driver.Url;
            bunifuCustomDataGrid2.Rows.Clear();
            int sayfadaki_classsayisi;
            yeni.bunifuCircleProgressbar1.MaxValue = int.Parse(toolStripTextBox13.Text) + 1;
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.modernLabel2.Text = "...";
            yeni.Show();
            this.Hide();
            takipciler.Clear();
            count = 0;
            int ram_control_count = 1;
        ram_bosalt:
            while(count < kackisibulunsun) {
                sayfadaki_classsayisi = Convert.ToInt32(js.ExecuteScript(Komutlar.KullanıcıDivSayısı));
                for(int x = 0; x < sayfadaki_classsayisi; x++) {
                    DegiskenleriTemizle();
                    if(Limit(driverurl)) goto ram_bosalt;
                    if(x == 10) {
                        ;
                    }
                    try {
                        kapsayiciHTML = js.ExecuteScript(Komutlar.KapsayiciHTML.Replace("xxxx", x.ToString())).ToString();
                        kapsayiciText = js.ExecuteScript(Komutlar.kapsayiciText.Replace("xxxx", x.ToString())).ToString();
                    }
                    catch(Exception) { Thread.Sleep(120); continue; }
                    siradaki_begenen = "@" + Bul(kapsayiciHTML, ">@", "</span>");
                    kapsayiciText = kapsayiciText.Replace(siradaki_begenen, "");
                    ram_control_count = RamControl(ram_control_count);
                    DataRow[] filteredRows = tbl.Select(string.Format("{0} LIKE '%{1}%'", "profili", siradaki_begenen));
                    if(!takipciler.Contains(siradaki_begenen)) {
                        takipciler.Add(siradaki_begenen);
                        if(!kapsayiciHTML.Contains("Korumalı hesap") && toolStripMenuItem7.Checked) continue;
                        if(kapsayiciHTML.Contains("Korumalı hesap") && toolStripMenuItem8.Checked) continue;
                        if(öncedenGTYapmayanlarıAtlaToolStripMenuItem.Checked && filteredRows.Length > 0) continue;
                        string[] tektek = kapsayiciText.Split('\n');
                        if(kapsayiciText.Contains("Takip et")) takipedilmedurumu = "Takip et";
                        else if(kapsayiciText.Contains("Takip ediliyor")) takipedilmedurumu = "Takip ediliyor";
                        else if(kapsayiciText.Contains("Engellendi")) takipedilmedurumu = "Engellendi";
                        else if(kapsayiciText.Contains("Beklemede")) takipedilmedurumu = "Beklemede";
                        else takipedilmedurumu = "BELİRSİZ";
                        kapsayiciText = kapsayiciText.Replace(takipedilmedurumu, "");
                        if(takipEtmediklerimToolStripMenuItem1.Checked && takipedilmedurumu != "Takip et") continue;
                        fotograf_url = Bul(kapsayiciHTML, "(&quot;", "&quot;");
                        if(toolStripMenuItem15.Checked && fotograf_url.Contains("default")) continue;
                        if(kapsayiciText.Contains("Seni takip ediyor")) {
                            takip_ediyormu = "Seni takip ediyor";
                            if(beniTakipEtmeyenlerToolStripMenuItem1.Checked) continue;
                        }
                        else takip_ediyormu = "Takip etmiyor";
                        kapsayiciText = kapsayiciText.Replace("Seni takip ediyor", "");
                        siradaki_begenen_ad = tektek[0].Replace("\r", "").Replace("\n", "");
                        cinsiyet = CinsiyetBul(siradaki_begenen_ad.ToLower());
                        if(!Filtre_Cinsiyet(cinsiyet)) continue;
                        kapsayiciText = kapsayiciText.Replace(tektek[0], "");
                        kapsayiciText = kapsayiciText.Replace("\r", "").Replace("\n", "");
                        if(kapsayiciText.Length > 0) biyografi = kapsayiciText;
                        else biyografi = "Biyografi yok";
                        if(!detayİstemiyorumHızlıOlToolStripMenuItem.Checked && !kapsayiciHTML.Contains("Korumalı hesap")) {
                            ram_control_count++;
                            js.ExecuteScript(Komutlar.KullanıcıProfilineGiris.Replace("xxxx", x.ToString()));
                            string kapsayici_detay = js.ExecuteScript(Komutlar.KullaniciProfiliHTML).ToString();
                            if(kapsayici_detay.Contains(Komutlar.KonumSimgesi)) { Bul(kapsayici_detay, "\"></path></g></svg><span class=\"css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0\"><span class=\"css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0\">"); }
                            else { konum = "Yok"; }
                            if(konumuToolStripMenuItem.Checked) {
                                string[] filtre_konumlar = ankaraİstanbulPendikToolStripMenuItem.Text.Split(';');
                                bool varmi = false;
                                for(int i = 0; i < filtre_konumlar.Length; i++) {
                                    if(konum.ToLower().IndexOf(filtre_konumlar[i].ToLower()) != -1 || biyografi.ToLower().IndexOf(filtre_konumlar[i].ToLower()) != -1) { varmi = true; break; }
                                }
                                if(!varmi) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            }
                            kayittarihi = ReturnKomutCalistir("return document.querySelector('[data-testid=UserProfileHeader_Items]').lastElementChild.innerText;");
                            toplamGun = KayıtTarihi(kayittarihi);
                            if(kayıtTarihiToolStripMenuItem.Checked && toplamGun < int.Parse(toolStripTextBox6.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            tweetsayisi = js.ExecuteScript(Komutlar.TweetveBegenmeSayisi).ToString();
                            tweetsayisi = tweetsayisi.Replace("Tweetler", "").Replace("Tweet", "");
                            tweetsayisi = Donustur(tweetsayisi);
                            if(tweetSayısıToolStripMenuItem.Checked && fazlaOlanlarToolStripMenuItem.Checked && int.Parse(tweetsayisi) < int.Parse(toolStripTextBox7.Text)) {
                                js.ExecuteScript(Komutlar.GeriCik); continue;
                            }
                            else if(tweetSayısıToolStripMenuItem.Checked && azOlanlarToolStripMenuItem.Checked && int.Parse(tweetsayisi) > int.Parse(toolStripTextBox7.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            takipcisayisi = takipcisayisi = ReturnKomutCalistir(Komutlar.TakipciSayisiJSkodu).Replace(".", "");
                            takipedilen = takipcisayisi = ReturnKomutCalistir(Komutlar.TakipEdilenSayisiJSkodu).Replace(".", "");
                            if(takipEdilenSayısıToolStripMenuItem.Checked && toolStripMenuItem13.Checked && int.Parse(takipedilen) < int.Parse(toolStripTextBox5.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            else if(takipEdilenSayısıToolStripMenuItem.Checked && toolStripMenuItem14.Checked && int.Parse(takipedilen) > int.Parse(toolStripTextBox5.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            if(takipciSayısıToolStripMenuItem.Checked && toolStripMenuItem10.Checked && int.Parse(takipcisayisi) < int.Parse(toolStripTextBox4.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            else if(takipciSayısıToolStripMenuItem.Checked && toolStripMenuItem11.Checked && int.Parse(takipcisayisi) > int.Parse(toolStripTextBox4.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            KomutCalistir(Komutlar.BegenilereTikla);
                            begenisayisi = js.ExecuteScript(Komutlar.TweetveBegenmeSayisi).ToString();
                            begenisayisi = begenisayisi.Replace("Beğeniler", "").Replace("Beğeni", "").Replace("Beğen", "");
                            begenisayisi = Donustur(begenisayisi);
                            if(begenisayisi.Contains("Twe")) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            if(beğeniSayısıToolStripMenuItem.Checked && toolStripMenuItem10.Checked && int.Parse(begenisayisi) < int.Parse(toolStripTextBox10.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            else if(beğeniSayısıToolStripMenuItem.Checked && toolStripMenuItem11.Checked && int.Parse(begenisayisi) > int.Parse(toolStripTextBox10.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            gunlukbegeni = Math.Round(int.Parse(begenisayisi) / toplamGun, 2);
                            if(günlükBeğeniSayısıToolStripMenuItem.Checked && fazlaOlanlarToolStripMenuItem3.Checked && gunlukbegeni < int.Parse(toolStripTextBox12.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            else if(günlükBeğeniSayısıToolStripMenuItem.Checked && azOlanlarToolStripMenuItem3.Checked && gunlukbegeni > int.Parse(toolStripTextBox12.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            gunluktweet = Math.Round(int.Parse(tweetsayisi) / toplamGun, 2);
                            if(günlükTweetSayısıToolStripMenuItem1.Checked && fazlaOlanlarToolStripMenuItem2.Checked && gunluktweet < int.Parse(toolStripTextBox12.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            else if(günlükTweetSayısıToolStripMenuItem1.Checked && azOlanlarToolStripMenuItem2.Checked && gunluktweet > int.Parse(toolStripTextBox12.Text)) { js.ExecuteScript(Komutlar.GeriCik); continue; }
                            js.ExecuteScript(Komutlar.GeriCik);
                        }
                        if(bulurkenTakipEtToolStripMenuItem.Checked) {
                            try {
                                js.ExecuteScript(Komutlar.TakipEtClickListe.Replace("xxxx", x.ToString()));
                                takipedilmedurumu = "Takip ediliyor";
                                Thread.Sleep(random.Next(3, 10) * 1000);
                            }
                            catch(Exception) {; }
                        }
                        if(kapsayiciHTML.Contains("Korumalı hesap"))
                            fotograf_url = "Kilitli" + fotograf_url;
                        GridSatirEkle(bunifuCustomDataGrid2.Rows.Count + 1, siradaki_begenen_ad,
                                   siradaki_begenen, begenisay, fotograf_url, cinsiyet,
                                   biyografi, konum, takipedilen, takipcisayisi,
                                   toplamGun, gunlukbegeni, tweetsayisi, gunluktweet,
                                   takip_ediyormu, takipedilmedurumu);
                        count++;
                        yeni.bunifuCircleProgressbar1.Value++;
                        yeni.modernLabel1.Text = "Takipçiler Kontrol Ediliyor " + count.ToString() + "/" +
                           kackisibulunsun.ToString();
                        if(count == kackisibulunsun) break;
                    }
                }
                double onceki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
                js.ExecuteScript(Komutlar.SayfaKaydir); Thread.Sleep(250);
                double sonraki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
                if(sonraki_konum == onceki_konum) {
                    MessageBox.Show("Seçtiğiniz kriterlere uygun maksimum kişi bulundu.");
                    break;
                }
            }
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.Both; this.Show(); yeni.Hide();
        }
        private void BegenenleriKaydet() {
            try {
                VeritabanıTemizle("begenenler");
                string ekle = "INSERT into begenenler (sira,ismi,profili,begeni,fotografyolu,cinsiyet,biyografi,konumu,following,followers,gunduruye,gunluklike,tweet,gunluktweet,takipdurumu,takipet) Values (@sira,@ismi,@profili,@begeni,@fotografyolu,@cinsiyet,@biyografi,@konumu,@following,@followers,@gunduruye,@gunluklike,@tweet,@gunluktweet,@takipdurumu,@takipet)";
                OleDbCommand komut;
                for(int i = 0; i < begenenler_matris.Length / 17; i++) {
                    komut = new OleDbCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@sira", Convert.ToInt32(begenenler_matris[i, 0]));
                    komut.Parameters.AddWithValue("@ismi", begenenler_matris[i, 2]);
                    komut.Parameters.AddWithValue("@profili", begenenler_matris[i, 3]);
                    komut.Parameters.AddWithValue("@begeni", Convert.ToInt32(begenenler_matris[i, 4]));
                    komut.Parameters.AddWithValue("@fotografyolu", begenenler_matris[i, 5]);
                    komut.Parameters.AddWithValue("@cinsiyet", begenenler_matris[i, 6]);
                    komut.Parameters.AddWithValue("@biyografi", begenenler_matris[i, 7]);
                    komut.Parameters.AddWithValue("@konumu", begenenler_matris[i, 8]);
                    komut.Parameters.AddWithValue("@following", Convert.ToInt32(begenenler_matris[i, 9]));
                    komut.Parameters.AddWithValue("@followers", Convert.ToInt32(begenenler_matris[i, 10]));
                    komut.Parameters.AddWithValue("@gunduruye", Convert.ToInt32(begenenler_matris[i, 11]));
                    komut.Parameters.AddWithValue("@gunluklike", Convert.ToDouble(begenenler_matris[i, 12]));
                    komut.Parameters.AddWithValue("@tweet", Convert.ToInt32(begenenler_matris[i, 13]));
                    komut.Parameters.AddWithValue("@gunluktweet", Convert.ToDouble(begenenler_matris[i, 14]));
                    komut.Parameters.AddWithValue("@takipdurumu", begenenler_matris[i, 15]);
                    komut.Parameters.AddWithValue("@takipet", begenenler_matris[i, 16]);
                    komut.ExecuteNonQuery();
                }
                Settings.Default.kontroledilentweet = kontrol_edilecek_tweet_sayisi;
                Settings.Default.Save();
                MessageBox.Show("Başarıyla kaydedildi.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex) { MessageBox.Show("Kaydedilemedi." + ex.Message, "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void DetayVisible() {
            bool visible = false;
            if(detayvisible) {
                visible = false;
            }
            else {
                visible = true;
            }
            for(int i = 8; i < 15; i++) {
                bunifuCustomDataGrid2.Columns[i].Visible = visible;
            }
            int uzunluk = GorunurColumnsWidth();
            bunifuCustomDataGrid2.Width = uzunluk;
            bunifuGradientPanel2.Width = uzunluk;
            menuStrip1.Width = uzunluk;
            this.Width = bunifuGradientPanel11.Width + uzunluk + 20;
            CenterToScreen();
            detayvisible = visible;
        }
        private void ProfilBilgileri() {
            driver.Navigate().GoToUrl("https://mobile.twitter.com/" + kullaniciadi + "/likes");
        yenidendene:
            // driver2Ac();
            if(js.ExecuteScript(Komutlar.LoginButonuGozukuyormu).ToString() == "1") {
                driver.Navigate().Refresh();
                SayfaLoadBekle();
                goto yenidendene;
            }
            yeni.modernLabel1.Text = "Profil Bilgileri Çekiliyor";
            string ProfilBilgileri_takipedilen;
            string ProfilBilgileri_uyelik;
            string ProfilBilgileri_begeni;
            string ProfilBilgileri_tweetsaysi;
            string ProfilBilgileri_takipci;
            double toplamGun2;
            ProfilBilgileri_begeni = ReturnKomutCalistir(Komutlar.TweetveBegenmeSayisi); //
            yeni.modernLabel2.Text = "Beğeni Sayısı Çekildi";
            KomutCalistir(Komutlar.TweetlerButonuClick);
            ProfilBilgileri_uyelik = ReturnKomutCalistir(Komutlar.UyelikTarihiniBul).ToString();
            yeni.modernLabel2.Text = "Üyelik süresi Çekildi";
            toplamGun2 = KayıtTarihi(ProfilBilgileri_uyelik);
            ProfilBilgileri_tweetsaysi = ReturnKomutCalistir(Komutlar.TweetveBegenmeSayisi).Replace("Tweetler", "").Replace("Tweet", "");
            yeni.modernLabel2.Text = "Tweet Sayısı Çekildi";
            ProfilBilgileri_tweetsaysi = Donustur(ProfilBilgileri_tweetsaysi);
            ProfilBilgileri_takipedilen = js.ExecuteScript(Komutlar.TakipEdilenSayisiJSkodu).ToString();
            yeni.modernLabel2.Text = "Takip Edilen Sayısı Çekildi";
            ProfilBilgileri_takipci = ReturnKomutCalistir(Komutlar.TakipciSayisiJSkodu).ToString();
            yeni.modernLabel2.Text = "Takipçi Sayısı Çekildi";
            pictureBox1.Image = ResimOlustur(js.ExecuteScript(Komutlar.Profilden200xProfilFotografi).ToString().Replace("200x200", "400x400"), kullaniciadi, Color.FromArgb(11, 7, 17));
            yeni.modernLabel2.Text = "Profil Fotoğrafı Çekildi";
            ProfilBilgileri_begeni = Donustur(ProfilBilgileri_begeni.Replace("Beğeniler", "").Replace("Beğeni", "").Replace("Beğen", ""));
            modernLabel13.Text = ProfilBilgileri_takipci;
            modernLabel4.Text = ProfilBilgileri_takipedilen.Replace(".", "");
            float sonuc_gunlukbegeni = (float.Parse(ProfilBilgileri_begeni) / float.Parse(toplamGun2.ToString()));
            float sonuc_gunluktweet = (float.Parse(ProfilBilgileri_tweetsaysi) / float.Parse(toplamGun2.ToString()));
            float sonuc_takipciorani = (float.Parse(ProfilBilgileri_takipci) / float.Parse(ProfilBilgileri_takipedilen));
            modernLabel10.Text = Math.Round(sonuc_gunluktweet, 1).ToString();
            modernLabel2.Text = Math.Round(sonuc_gunlukbegeni, 1).ToString();
            modernLabel6.Text = Math.Round(sonuc_takipciorani, 1).ToString();
            modernLabel8.Text = toplamGun2.ToString();
        }
        private void KaranlikMod() {
            Thread.Sleep(250);
            driver.Navigate().GoToUrl("https://mobile.twitter.com/i/display");
            Thread.Sleep(250);
            /*  KomutCalistir("document.querySelectorAll('[data-testid=DashButton_ProfileIcon_Link]')[0].click();");
              KomutCalistir("document.querySelectorAll('[title=Ekran]')[0].click();");*/
            KomutCalistir("document.querySelector('[aria-label=Loş]').click();");
            KomutCalistir("document.getElementsByClassName('css-18t94o4 css-1dbjc4n r-urgr8i r-42olwf r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-1nrc83j r-bxzshc r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr')[0].click();");
        }
        private void GeriTakipYapmayanlar() {
            modernLabel4.Text = TakipEdilenSayisi(kullaniciadi).ToString();
            driver.Navigate().GoToUrl("https://mobile.twitter.com/" + kullaniciadi + "/following");
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.None;
            ArrayList gt_yapmayan_kontrol = new ArrayList();
            int say = 0;
            SayfaLoadBekle();
            int sayfadaki_classsayisi;
            yeni.bunifuCircleProgressbar1.MaxValue = Convert.ToInt32(modernLabel4.Text) + 1;
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.modernLabel2.Text = "...";
            yeni.Show();
            this.Hide();
            //  DetayVisible();
            gt_yapmayan_kontrol.Clear();
            DegiskenleriTemizle();
            yeni.modernLabel1.Text = "GT Yapmayanlar Kontrol Ediliyor";
            do {
                sayfadaki_classsayisi = Convert.ToInt32(js.ExecuteScript(Komutlar.KullanıcıDivSayısı));
                for(int x = 0; x < sayfadaki_classsayisi; x++) {
                    try {
                        kapsayiciHTML = js.ExecuteScript(Komutlar.KapsayiciHTML.Replace("xxxx", x.ToString())).ToString();
                        kapsayiciText = js.ExecuteScript(Komutlar.kapsayiciText.Replace("xxxx", x.ToString())).ToString();
                    }
                    catch(Exception) {
                        Thread.Sleep(100);
                        continue;
                    }
                    siradaki_begenen = "@" + Bul(kapsayiciHTML, ">@", "</span>");
                    kapsayiciText = kapsayiciText.Replace(siradaki_begenen, "");
                    if(!gt_yapmayan_kontrol.Contains(siradaki_begenen)) {
                        if(kapsayiciText.Contains("Seni takip ediyor")) {
                            takip_ediyormu = "Seni takip ediyor";
                            say++;
                            yeni.bunifuCircleProgressbar1.Value++;
                            gt_yapmayan_kontrol.Add(siradaki_begenen);
                            continue;
                        }
                        else {
                            string[] tektek = kapsayiciText.Split('\n');
                            takip_ediyormu = "Takip etmiyor";
                            KaraListe(siradaki_begenen);
                            if(kapsayiciText.Contains("Takip et")) takipedilmedurumu = "Takip et";
                            else if(kapsayiciText.Contains("Takip ediliyor")) takipedilmedurumu = "Takip ediliyor";
                            kapsayiciText = kapsayiciText.Replace(takipedilmedurumu, "");
                            if(toolStripMenuItem16.Checked) {
                                if(KomutCalistir("document.querySelectorAll('[data-testid=UserCell]')[" + x + "].children[0].children[1].children[0].children[1].children[0].click();")) {
                                    KomutCalistir("document.querySelectorAll('[data-testid=confirmationSheetConfirm]')[0].click();"); takipedilmedurumu = "Takip et";
                                    if(toolStripMenuItem18.Checked) Thread.Sleep(rnd1.Next(3, 10) * 1000);
                                }
                            }
                            fotograf_url = Bul(kapsayiciHTML, "(&quot;", "&quot;");
                            siradaki_begenen_ad = tektek[0].Replace("\r", "").Replace("\n", "");
                            cinsiyet = CinsiyetBul(siradaki_begenen_ad.ToLower());
                            kapsayiciText = kapsayiciText.Replace(tektek[0], "");
                            kapsayiciText = kapsayiciText.Replace("\r", "").Replace("\n", "");
                            if(kapsayiciHTML.Contains("Korumalı")) fotograf_url = "Kilitli" + fotograf_url;
                            if(kapsayiciText.Length > 0) biyografi = kapsayiciText;
                            else biyografi = "Biyografi yok";
                            GridSatirEkle(
                                    bunifuCustomDataGrid2.Rows.Count + 1,
                                    siradaki_begenen_ad, siradaki_begenen, 0,
                                   fotograf_url, cinsiyet,
                                    biyografi, konum,
                                    "0", "0",
                                   0, 0,
                                    "0", 0,
                                  takip_ediyormu, takipedilmedurumu);
                            gt_yapmayan_kontrol.Add(siradaki_begenen);
                            say++;
                            yeni.bunifuCircleProgressbar1.Value++;
                            yeni.modernLabel2.Text = "Bulunan: " + bunifuCustomDataGrid2.Rows.Count.ToString();
                            if(gt_yapmayan_kontrol.Count == Convert.ToInt32(modernLabel4.Text)) break;
                        }
                    }
                }
                Kaydir(kullaniciadi);
            } while(gt_yapmayan_kontrol.Count < Convert.ToInt32(modernLabel4.Text));
            this.Show();
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.Both; yeni.Hide();
        }
        private void GridOlustur() {
            /* DataGridViewCheckBoxColumn sec = new DataGridViewCheckBoxColumn();
             sec.Name = "Seç";
             sec.Width = 15;
             bunifuCustomDataGrid2.Columns.Add(sec);*/
            DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn {
                Name = "Sıra",
                Width = 45
            };
            bunifuCustomDataGrid2.Columns.Add(sira);
            DataGridViewImageColumn imgg = new DataGridViewImageColumn {
                ImageLayout = DataGridViewImageCellLayout.Stretch,
                HeaderText = "Profil Fotoğrafı",
                Width = 96
            };
            imgg.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(imgg);
            DataGridViewTextBoxColumn ad = new DataGridViewTextBoxColumn {
                Name = "İsmi",
                Width = 140
            }; ad.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //  ad.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(ad);
            DataGridViewLinkColumn ProfilBilgileri = new DataGridViewLinkColumn {
                Name = "Profili",
                Width = 135
            };
            ProfilBilgileri.DefaultCellStyle.SelectionForeColor = Color.White;
            //ProfilBilgileri.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(ProfilBilgileri);
            DataGridViewTextBoxColumn begenisayisi = new DataGridViewTextBoxColumn {
                Name = "Beğeni Sayısı",
                Width = 55
            };
            // begenisayisi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(begenisayisi);
            DataGridViewTextBoxColumn fotografyolu = new DataGridViewTextBoxColumn {
                Name = "Fotoğraf Yolu",
                Width = 0,
                Visible = false
            };
            bunifuCustomDataGrid2.Columns.Add(fotografyolu);
            DataGridViewTextBoxColumn cinsiyet = new DataGridViewTextBoxColumn {
                Name = "Cinsiyet",
                Width = 70
            };
            // cinsiyet.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(cinsiyet);
            DataGridViewTextBoxColumn biosu = new DataGridViewTextBoxColumn {
                Name = "Biyografi",
                Width = 215
            };
            //     biosu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            biosu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(biosu);
            DataGridViewTextBoxColumn konum = new DataGridViewTextBoxColumn {
                Name = "Konumu",
                Width = 110
            };
            //  konum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            konum.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(konum);
            DataGridViewTextBoxColumn takipettigi = new DataGridViewTextBoxColumn {
                Name = "Following",
                Width = 75
            };
            //    takipettigi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(takipettigi);
            DataGridViewTextBoxColumn takipcisi = new DataGridViewTextBoxColumn {
                Name = "Followers",
                Width = 75
            };
            //    takipcisi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(takipcisi);
            DataGridViewTextBoxColumn aktifgun = new DataGridViewTextBoxColumn {
                Name = "Gündür Üye",
                Width = 70
            };
            //    aktifgun.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(aktifgun);
            DataGridViewTextBoxColumn begenioran = new DataGridViewTextBoxColumn {
                Name = "Günlük like",
                Width = 70
            };
            //  begenioran.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(begenioran);
            DataGridViewTextBoxColumn twit = new DataGridViewTextBoxColumn {
                Name = "Tweet",
                Width = 60
            };
            //  twit.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bunifuCustomDataGrid2.Columns.Add(twit);
            DataGridViewTextBoxColumn twitoran = new DataGridViewTextBoxColumn {
                Name = "Günlük Tweet",
                Width = 60
            };
            //   twitoran.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            twitoran.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(twitoran);
            DataGridViewTextBoxColumn takip_durumu = new DataGridViewTextBoxColumn {
                Name = "Takip Durumu",
                Width = 60
            };
            // takip_durumu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            takip_durumu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(takip_durumu);
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn {
                Name = "Takip et/çık",
                Width = 70
            };
            btn.FlatStyle = FlatStyle.Flat;
            // btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.;
            btn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            bunifuCustomDataGrid2.Columns.Add(btn);
            this.Width = bunifuGradientPanel11.Width + GorunurColumnsWidth() + 20;
            int x; double sabit = 1.1;
            x = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height / sabit);
            this.Height = x;
            // OranAl();
            // WithAl();
        }
        private void GridViewAnaliz() {
            modernPanel1.Height = 338;
            if(modernPanel1.Visible) {
                GridAnalizButon.Iconimage = Image.FromFile(@"img\open.png");
                modernPanel1.Visible = false;
            }
            else {
                bunifuCustomDataGrid1.Rows.Clear();
                int newsatir_takipeden, newsatir_takipetmeyen, newsatir_takipedilen, newsatir_takipedilmeyen, newsatir_begenisayisi, newsatir_erkeksayisi, new_satirkadinsayisi, newsatir_unisexsayisi, newsatir_belirsizsayisi, newsatir_gtyapmayan;
                double newsatir_gunluklike, newsatir_gunluktweet;
                int count = 0;
                newsatir_takipeden = 0; newsatir_takipetmeyen = 0; newsatir_takipedilen = 0; newsatir_takipedilmeyen = 0; newsatir_begenisayisi = 0; newsatir_erkeksayisi = 0; new_satirkadinsayisi = 0; newsatir_unisexsayisi = 0; newsatir_belirsizsayisi = 0; newsatir_gunluklike = 0; newsatir_gunluktweet = 0; newsatir_gtyapmayan = 0;
                for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                    if(bunifuCustomDataGrid2.Rows[i].Visible) {
                        count++;
                        newsatir_begenisayisi += Convert.ToInt32(bunifuCustomDataGrid2.Rows[i].Cells[4].Value);
                        if(bunifuCustomDataGrid2.Rows[i].Cells[6].Value.ToString() == "Erkek") {
                            newsatir_erkeksayisi++;
                        }
                        else if(bunifuCustomDataGrid2.Rows[i].Cells[6].Value.ToString() == "Kadın") {
                            new_satirkadinsayisi++;
                        }
                        else if(bunifuCustomDataGrid2.Rows[i].Cells[6].Value.ToString() == "Unisex") {
                            newsatir_unisexsayisi++;
                        }
                        else {
                            newsatir_belirsizsayisi++;
                        }
                        newsatir_gunluklike += Convert.ToDouble(bunifuCustomDataGrid2.Rows[i].Cells[12].Value);
                        newsatir_gunluktweet += Convert.ToDouble(bunifuCustomDataGrid2.Rows[i].Cells[14].Value);
                        if(bunifuCustomDataGrid2.Rows[i].Cells[15].Value.ToString() == "Seni takip ediyor") {
                            newsatir_takipeden++;
                        }
                        else {
                            newsatir_takipetmeyen++;
                        }
                        if(bunifuCustomDataGrid2.Rows[i].Cells[16].Value.ToString() == "Takip ediliyor") {
                            newsatir_takipedilen++;
                        }
                        else if(bunifuCustomDataGrid2.Rows[i].Cells[16].Value.ToString() == "Takip et") {
                            newsatir_takipedilmeyen++;
                        }
                        if(bunifuCustomDataGrid2.Rows[i].Cells[16].Value.ToString() == "Takip ediliyor" && bunifuCustomDataGrid2.Rows[i].Cells[15].Value.ToString() != "Seni takip ediyor") {
                            newsatir_gtyapmayan++;
                        }
                    }
                }

                if(kontrol_edilecek_tweet_sayisi == 0) {
                    bunifuCustomDataGrid1.Rows.Add("Kontrol Edilen Tweet");
                    bunifuCustomDataGrid1.Rows.Add(Settings.Default.kontroledilentweet.ToString());
                    bunifuCustomDataGrid1.Rows.Add("Tweet Başı Beğeni");
                    if(kontrol_edilen_tweet_Sayisi == 0) kontrol_edilen_tweet_Sayisi = 1;
                    bunifuCustomDataGrid1.Rows.Add((newsatir_begenisayisi / kontrol_edilen_tweet_Sayisi).ToString());
                }
                else {
                    bunifuCustomDataGrid1.Rows.Add("Kontrol Edilen Tweet");
                    bunifuCustomDataGrid1.Rows.Add(kontrol_edilecek_tweet_sayisi.ToString());
                    bunifuCustomDataGrid1.Rows.Add("Tweet Başı Beğeni");
                    bunifuCustomDataGrid1.Rows.Add((newsatir_begenisayisi / kontrol_edilecek_tweet_sayisi).ToString());
                }
                bunifuCustomDataGrid1.Rows.Add("Geri Takip Yapmayan");
                bunifuCustomDataGrid1.Rows.Add(newsatir_gtyapmayan.ToString());
                bunifuCustomDataGrid1.Rows.Add("Takip Edilmeyen");
                bunifuCustomDataGrid1.Rows.Add(newsatir_takipedilmeyen.ToString());
                bunifuCustomDataGrid1.Rows.Add("Beğeni Sayısı");
                bunifuCustomDataGrid1.Rows.Add(newsatir_begenisayisi.ToString());
                bunifuCustomDataGrid1.Rows.Add("Takip Etmeyen");
                bunifuCustomDataGrid1.Rows.Add(newsatir_takipetmeyen.ToString());
                bunifuCustomDataGrid1.Rows.Add("Takip Edilen");
                bunifuCustomDataGrid1.Rows.Add(newsatir_takipedilen.ToString());
                bunifuCustomDataGrid1.Rows.Add("Kişi Sayısı");
                bunifuCustomDataGrid1.Rows.Add(count.ToString());
                bunifuCustomDataGrid1.Rows.Add("Takip Eden");
                bunifuCustomDataGrid1.Rows.Add(newsatir_takipeden.ToString());
                bunifuCustomDataGrid1.Rows.Add("Beğeni Aktifliği");
                bunifuCustomDataGrid1.Rows.Add(Math.Round((newsatir_gunluklike / count), 1).ToString() + "/" + "30");
                bunifuCustomDataGrid1.Rows.Add("Tweet Aktifliği");
                bunifuCustomDataGrid1.Rows.Add(Math.Round((newsatir_gunluktweet / count), 1).ToString() + "/" + "5");
                bunifuCustomDataGrid1.Rows.Add("Kadın");
                bunifuCustomDataGrid1.Rows.Add("%" + Math.Round((new_satirkadinsayisi / Convert.ToDouble(count)) * 100, 0).ToString());
                bunifuCustomDataGrid1.Rows.Add("Erkek");
                bunifuCustomDataGrid1.Rows.Add("%" + Math.Round((newsatir_erkeksayisi / Convert.ToDouble(count)) * 100, 0).ToString());
                bunifuCustomDataGrid1.Rows.Add("Unisex");
                bunifuCustomDataGrid1.Rows.Add("%" + Math.Round((newsatir_unisexsayisi / Convert.ToDouble(count)) * 100, 0).ToString());
                bunifuCustomDataGrid1.Rows.Add("Belirsiz");
                bunifuCustomDataGrid1.Rows.Add("%" + Math.Round((newsatir_belirsizsayisi / Convert.ToDouble(count)) * 100, 0).ToString());
                for(int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++) {
                    if(i % 2 == 0) {
                        bunifuCustomDataGrid1.Rows[i].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11.5F, FontStyle.Bold);
                    }
                    else {
                        bunifuCustomDataGrid1.Rows[i].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    }
                }
                GridAnalizButon.Iconimage = Image.FromFile(@"img\closed.png"); modernPanel1.Visible = true;
            }
        }
        private void DegiskenleriTemizle() {
            siradaki_begenen = "";
            siradaki_begenen_ad = "";
            begenisay = 0;
            fotograf_url = "";
            takip_ediyormu = "";
            takipedilmedurumu = "";
            biyografi = "";
            cinsiyet = "";
            //DRİVER2
            kayittarihi = "";
            tweetsayisi = "0";
            takipedilen = "0";
            takipcisayisi = "0";
            begenisayisi = "0";
            konum = "";
            toplamGun = 0;
            gunluktweet = 0;
            gunlukbegeni = 0;
        }

        private void DetayBilgiler(int x) {
            count++;
            if(KomutCalistir(Komutlar.KullanıcıProfilineGiris.Replace("xxxx", x.ToString()))) {

            // driver2.Navigate().GoToUrl("https://mobile.twitter.com/" + siradaki_begenen.Substring(1, siradaki_begenen.Length - 1));
            yuklemeyibekle:
                try {

                    string kapsayici_detay = js.ExecuteScript(Komutlar.KullaniciProfiliHTML).ToString();
                    string kapsayici_detayText = js.ExecuteScript(Komutlar.KullaniciProfiliText).ToString();
                    kayittarihi = ReturnKomutCalistir("return document.querySelector('[data-testid=UserProfileHeader_Items]').lastElementChild.innerText;");
                    toplamGun = KayıtTarihi(kayittarihi);
                    if(kapsayici_detay.Contains(Komutlar.KonumSimgesi)) { konum = Bul(kapsayici_detay, "\"></path></g></svg><span class=\"css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0\"><span class=\"css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0\">"); if(konum.Length > 30) konum = "Yok"; }
                    else { konum = "Yok"; }
                    takipcisayisi = ReturnKomutCalistir(Komutlar.TakipciSayisiJSkodu).Replace(".", "");
                    takipcisayisi = Donustur(takipcisayisi);
                    takipedilen = ReturnKomutCalistir(Komutlar.TakipEdilenSayisiJSkodu).Replace(".", "");
                    takipedilen = Donustur(takipedilen);
                    tweetsayisi = Donustur(ReturnKomutCalistir(Komutlar.TweetveBegenmeSayisi).ToString().Replace("Tweetler", "").Replace("Tweet", ""));
                    KomutCalistir(Komutlar.BegenilereTikla);
                    begenisayisi = Donustur(ReturnKomutCalistir(Komutlar.TweetveBegenmeSayisi).ToString().Replace("Beğeniler", "").Replace("Beğeni", "").Replace("Beğen", ""));
                    KomutCalistir(Komutlar.GeriCik);

                }
                catch(Exception) {
                    if(js.ExecuteScript("return document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-1niwhzg r-p1n3y5 r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-1jgb5lz r-1444osr r-145lgeb r-9u3a9d r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\").length;").ToString() == "1") {
                        js.ExecuteScript("document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-1niwhzg r-p1n3y5 r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-1jgb5lz r-1444osr r-145lgeb r-9u3a9d r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\")[0].click();");
                    }
                    goto yuklemeyibekle;
                }


            }


        }
        private void TakipetEt() {
            foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                if(drow.Cells[16].Value.ToString() == "Takip et") {
                    driver.Navigate().GoToUrl("https://www.twitter.com/" + drow.Cells[3].Value.ToString());
                    if(ReturnKomutCalistir(Komutlar.TakipEtTakipEdilenKutucukText) == "Takip et") {
                        if(KomutCalistir(Komutlar.TakipEtClickProfil)) {
                            drow.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#719663");
                            drow.DefaultCellStyle.ForeColor = Color.White;
                            drow.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#719663");
                            drow.DefaultCellStyle.SelectionForeColor = Color.White;
                            drow.Cells[16].Value = "Takip ediliyor";
                            KaraListe(drow.Cells[3].Value.ToString());
                            VeritabaniGuncelle(drow.Cells[3].Value.ToString(), "takipciler", "takipet", "Takip ediliyor");
                            VeritabaniGuncelle(drow.Cells[3].Value.ToString(), "begenenler", "takipet", "Takip ediliyor");
                            if(toolStripMenuItem18.Checked) Thread.Sleep(rnd1.Next(3, 10) * 1000);
                        }
                    }
                }
            }
            MessageBox.Show("Seçili Kullanıcılar takip edildi.", "İşlem bitti");
        }
        private void TumMesajlarıSil() {
            yeni.Show();
            this.Hide();
            yeni.modernLabel1.Text = "Mesajlar siliniyor.";
            yeni.modernLabel2.Text = "...";
            basilanbuton = -1;
            string siradakikisi = "";
            int silinenMesajCount = 0;
            int beyazListCount = 0;
            while(ReturnKomutCalistir("return document.querySelectorAll('[data-testid=conversation]').length;") != "0") {
                siradakikisi = js.ExecuteScript("return document.getElementsByClassName('css-901oao css-bfa6kz r-111h2gw r-18u37iz r-1qd0xha r-1b43r93 r-16dba41 r-ad9z0x r-bcqeeo r-qvutc0')[0].children[0].innerText;").ToString();
                if(!kontroledildi.Contains(siradakikisi)) {
                    if(KomutCalistir("document.querySelectorAll('[data-testid=conversation]')["+ beyazListCount + "].click();"))
                        if(KomutCalistir("document.getElementsByClassName('css-1dbjc4n r-1awozwy r-18u37iz r-ahm1il r-1777fci r-1jgb5lz r-13qz1uu')[0].children[2].children[0].children[0].click();"))
                            if(KomutCalistir("document.getElementsByClassName('css-901oao r-daml9f r-1qd0xha r-1b43r93 r-16dba41 r-ad9z0x r-bcqeeo r-q4m81j r-qvutc0')[0].click();"))
                                if(KomutCalistir("document.querySelector('[data-testid=confirmationSheetConfirm]').click();")) {
                                    silinenMesajCount++;
                                    yeni.modernLabel2.Text = silinenMesajCount + " mesaj silindi.";
                                    Thread.Sleep(500);
                                }
                }
                else beyazListCount++;

            }

            this.Show();
            yeni.Hide();
        }
        private void MesajSil(string cagiran) {
            double onceki_konum;
            double sonraki_konum;
            bool kir = false;
            do {
                string siradakikisi;
                string takipdurum = "Takip ediliyor";
                basilanbuton = -1;
                mesaj_Sil mesajsil = new mesaj_Sil();
                sayfadaki_classsayisi = Convert.ToInt32(ReturnKomutCalistir("return document.querySelectorAll('[data-testid=conversation]').length;"));
                for(int i = 0; i < sayfadaki_classsayisi; i++) {
                    siradakikisi = ReturnKomutCalistir("return document.querySelectorAll('[data-testid=conversation]')["+i+"].children[0].children[1].children[0].children[0].children[0].children[0].children[1].innerText;").ToString();
                    if(siradakikisi == "-1") continue;
                    if(!kontroledildi.Contains(siradakikisi)) {
                       // int giris = Convert.ToInt32(ReturnKomutCalistir("return window.scrollY"));
                        KomutCalistir("document.querySelectorAll('[data-testid=conversation]')[" + i + "].click();");
                        mesajsil.pictureBox1.Image = ScreenShot(driver);//Image.FromFile(@"mesaj.png");
                        KomutCalistir("document.getElementsByClassName('css-4rbku5 css-18t94o4 css-1dbjc4n r-1niwhzg r-42olwf r-sdzlij r-1phboty r-rs99b7 r-1loqt21 r-1w2pmg r-1nrc83j r-1524zjh r-1ny4l3l r-mk0yit r-o7ynqc r-6416eg r-lrvibr')[0].click();");
                        if(cagiran == "secerek") {
                            basilanbuton = -1;
                            mesajsil.ShowDialog();
                            if(basilanbuton == -1) { kir = true; break; }

                        }
                        else if(cagiran == "takip") {
                            yeni.modernLabel2.Text = siradakikisi + " Kontrol ediliyor..";
                            takipdurum = ReturnKomutCalistir("return document.getElementsByClassName('css-1dbjc4n r-97wbjc')[0].innerText;").ToString();
                        }
                        if(takipdurum == "Takip et" || basilanbuton == 1) {
                            yeni.modernLabel2.Text = siradakikisi + " Siliniyor..";
                            KomutCalistir("document.getElementsByClassName('css-901oao r-daml9f r-1qd0xha r-1b43r93 r-16dba41 r-hjklzo r-bcqeeo r-q4m81j r-qvutc0')[0].click();");
                            KomutCalistir("document.querySelector('[data-testid=confirmationSheetConfirm]').click();");
                        }
                        else {
                            KomutCalistir("document.querySelectorAll('[aria-label=Arka')[0].click();");
                            KomutCalistir("document.querySelectorAll('[aria-label=Arka')[0].click();");
                        }
                  //      KomutCalistir("window.scrollTo(0, "+giris+ ");");
                        Thread.Sleep(200);
                        kontroledildi.Add(siradakikisi);
                    }
                }
                if(kir) { break; }
                onceki_konum = Convert.ToDouble(ReturnKomutCalistir("return window.scrollY"));
                KomutCalistir("window.scrollBy(0, 500);");
                sonraki_konum = Convert.ToDouble(ReturnKomutCalistir("return window.scrollY"));
                int tekrarCount = 0;
                while(sonraki_konum == onceki_konum && tekrarCount < 10) {
                    tekrarCount++;
                    KomutCalistir("window.scrollBy(0, 1000);");
                    Thread.Sleep(1000);
                    sonraki_konum = Convert.ToDouble(ReturnKomutCalistir("return window.scrollY"));
                }

            } while(sonraki_konum != onceki_konum);

        }
        private void TakipEtmedigimMesajlarıSil() {
            yeni.Show();
            this.Hide();
            yeni.modernLabel1.Text = "Mesajlar siliniyor.";
            yeni.modernLabel2.Text = "...";
            MesajSil("takip");
            this.Show();
            yeni.Hide();
        }
        private void GridColumnGizle(ToolStripMenuItem menuitem, int columnindex) {
            if(menuitem.Checked) {
                bunifuCustomDataGrid2.Columns[columnindex].Visible = true;
                menuitem.Checked = false;
            }
            else {
                bunifuCustomDataGrid2.Columns[columnindex].Visible = false;
                menuitem.Checked = true;
            }
        }
        private void ChechkIcon(ToolStripMenuItem nesne) {
            if(nesne.Checked) {
                nesne.Image = Image.FromFile(@"img\checked.png");
            }
            else {
                nesne.Image = Image.FromFile(@"img\unchecked.png");
            }
        }
        private void listEkleTemizle() {
        yenidendene3:
            try {
                driver.FindElement(By.XPath("//*[@id=\"react-root\"]/div/div/div[2]/main/div/div/div[2]/div/div[1]/div/form/div[1]/div/div/div[2]/input")).Clear();
            }
            catch(Exception) {
                goto yenidendene3;
            }
        }
        private void arasiBegenenler(int kucuk, int buyuk) {
            try {
                for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                    int veri = Convert.ToInt32(bunifuCustomDataGrid2.Rows[i].Cells[4].Value);
                    if(veri >= kucuk && veri <= buyuk) {
                        bunifuCustomDataGrid2.Rows[i].Visible = true;
                    }
                    else {
                        bunifuCustomDataGrid2.Rows[i].Visible = false;
                    }
                }
                /*hicbegenmeyenler_matris = new string[bunifuCustomDataGrid2.Rows.Count, bunifuCustomDataGrid2.Columns.Count];
                for (int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++)
                {
                    if (bunifuCustomDataGrid2.Rows[i].Visible)
                    {
                        for (int j = 0; j < bunifuCustomDataGrid2.Columns.Count; j++)
                        {
                            hicbegenmeyenler_matris[i, j] = bunifuCustomDataGrid2.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }*/
            }
            catch(Exception) {
                MessageBox.Show("Önce takipçi listesi analiz edilmelidir!");
            }
        }
        private void BaskasiniBegenenler() {
            TweetleriCek(modernTextBox3.Text, 100);
            BegenenleriKontrolEt();
        }
        private void ListeyiListeyeEkle(string listcolor) {
            int count = 0;
            bool control = true;
            while(control) {
                int listelenen_kullanicisayisi = Convert.ToInt32(ReturnKomutCalistir("return document.querySelectorAll('[data-testid=UserCell]').length;"));
                for(int i = 0; i < listelenen_kullanicisayisi; i++) {
                    string kullanici = ReturnKomutCalistir("return document.querySelectorAll('[data-testid=UserCell]')[" + i + "].children[0].children[1].children[0].children[0].children[0].children[0].children[1].innerText");
                    if(listcolor == "Beyaz") count += BeyazListe(kullanici);
                    else
                    if(listcolor == "Kara") count += KaraListe(kullanici);
                }
                double onceki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
                js.ExecuteScript(Komutlar.SayfaKaydir); Thread.Sleep(750);
                double sonraki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
                if(sonraki_konum == onceki_konum) control = false;
            }
            MessageBox.Show(count + " Kişi, " + listcolor + " listeye eklendi! ");
        }
        private void ListeyeKisiEkle(string eklenecek_kisi) {
            int count1 = 0;
            bool calis1 = true;
            listEkleTemizle();
            driver.FindElement(By.XPath("//*[@id=\"react-root\"]/div/div/div[2]/main/div/div/div[2]/div/div[1]/div/form/div[1]/div/div/div[2]/input")).SendKeys(eklenecek_kisi);
            string kisisayisi = ReturnKomutCalistir("return document.querySelectorAll('[data-testid=TypeaheadUser]').length;").ToString();
            while(!ReturnKomutCalistir("return document.getElementsByClassName(\"css-901oao css-bfa6kz r-111h2gw r-18u37iz r-1qd0xha r-1b43r93 r-16dba41 r-ad9z0x r-bcqeeo r-qvutc0\")[" + count1 + "].innerText;").ToString().ToLower().Contains(eklenecek_kisi.ToLower())) {
                if(kisisayisi == count1.ToString()) { calis1 = false; break; }
                count1++;
            }
            if(calis1 && ReturnKomutCalistir("return document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-1niwhzg r-p1n3y5 r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-174vidy r-ydfevp r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\")[" + count1 + "].innerText;").ToString().Contains("Ekle"))
            //
            {
                KomutCalistir("document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-1niwhzg r-p1n3y5 r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-174vidy r-ydfevp r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\")[" + count1 + "].click();");
                listEkleTemizle();
            }
        }
        private void TweetleriCek(string kullaniciadi, int kontrol_edilecek_tweet_sayisi) {



          
            
            tweetler.Clear();
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.bunifuCircleProgressbar1.MaxValue = kontrol_edilecek_tweet_sayisi + 1;
            yeni.modernLabel2.Text = "Tweet'ler kontrol ediliyor..." + kontrol_edilen_tweet_Sayisi.ToString() + "/" + kontrol_edilecek_tweet_sayisi.ToString();


        yenidendene:
            if(js.ExecuteScript(Komutlar.LoginButonuGozukuyormu).ToString() == "1") {
                driver.Navigate().Refresh();
                Thread.Sleep(1000);
                goto yenidendene;
            }


            tweet_sayisi = 0;
            siradaki_tweet = "";
            while(kontrol_edilecek_tweet_sayisi > kontrol_edilen_tweet_Sayisi + 1) {
                tweet_sayisi = Convert.ToInt32(js.ExecuteScript(Komutlar.TweetClassSayisi));
                for(var i = 0; i < tweet_sayisi; i++) {
                    try { siradaki_tweet = js.ExecuteScript(Komutlar.SiradakiTweetLinki.Replace("iiii", i.ToString())).ToString(); }
                    catch(Exception) {; }
                    if(!tweetler.Contains(siradaki_tweet) && siradaki_tweet.Substring(1, kullaniciadi.Length) == kullaniciadi) {
                        if(kontrol_edilecek_tweet_sayisi < kontrol_edilen_tweet_Sayisi + 1) break;
                        else {
                            tweetler.Add(siradaki_tweet);
                            kontrol_edilen_tweet_Sayisi++;
                            yeni.bunifuCircleProgressbar1.Value++;
                            yeni.modernLabel2.Text = "Tweet'ler kontrol ediliyor..." + kontrol_edilen_tweet_Sayisi.ToString() + "/" + kontrol_edilecek_tweet_sayisi.ToString();
                        }
                    }
                    else continue;
                }
                js.ExecuteScript(Komutlar.SayfaKaydir);
                Thread.Sleep(300);
            }
        }
        public void Driver2Baslat() {
            ChromeOptions options2 = new ChromeOptions();
            options2.AddArgument("--disable-features=IsolateOrigins,site-per-process");
            options2.AddArguments("--disable-extensions");
            options2.AddArgument("test-type");
            options2.AddArgument("--ignore-certificate-errors");
            options2.AddArgument("no-sandbox");
            options2.AddArgument("--headless");//hide browser
            options2.AddArgument("--blink-settings=imagesEnabled=false");
            options2.EnableMobileEmulation("Pixel 2 XL");
            ChromeDriverService service2 = ChromeDriverService.CreateDefaultService();
            service2.HideCommandPromptWindow = true;
            service2.SuppressInitialDiagnosticInformation = true;
            driver2 = new ChromeDriver(service2, options2);
            js2 = (IJavaScriptExecutor) driver2;
            try {
                driver2.Navigate().GoToUrl("https://twitter.com/alienation");
            }
            catch(Exception) {
                MessageBox.Show("İnternet bağlantınızda bir sorun var gibi gözüküyor.");
                Application.Exit();
            }
            bool tıklanma = false;
            while(tıklanma == false) {
                int say = 0;
                try {
                    driver2.FindElement(By.CssSelector("#layers > div:nth-child(2) > div > div > div > div.css-1dbjc4n.r-18u37iz.r-1w6e6rj.r-1wtj0ep.r-wgs6xk.r-thmkab > div.css-18t94o4.css-1dbjc4n.r-1niwhzg.r-11mg6pl.r-sdzlij.r-1phboty.r-rs99b7.r-1w2pmg.r-d0pm55.r-145lgeb.r-9u3a9d.r-1ny4l3l.r-1fneopy.r-o7ynqc.r-6416eg.r-lrvibr")).Click();
                    tıklanma = true;
                }
                catch(Exception) {
                    say++;
                    if(say > 10) tıklanma = true;
                    Thread.Sleep(150);
                }
            }
        }
        public void DriverBaslat() {
            Thread driver2Start = new Thread(new ThreadStart(Driver2Baslat));
            driver2Start.Start();
            yeni.modernLabel1.Text = "Giriş Yapılıyor...";
            yeni.bunifuCircleProgressbar1.MaxValue = 100;
            yeni.modernLabel2.Text = "Tarayıcı Başlatılıyor";
              kontrol_edilen_tweet_Sayisi = 0;
            string Yuklutarayicilar = YukluTarayıcılar();
            if(Yuklutarayicilar.Contains("Chrome")) {
                try {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--disable-features=IsolateOrigins,site-per-process");
                    //  chromeOptions.AddArgument("--headless");
                    chromeOptions.EnableMobileEmulation("Pixel 2 XL");
                    // chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;
                    driver = new ChromeDriver(service, chromeOptions);
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                MessageBox.Show("Bu programı kullanabilmek için Chrome tarayıcısına sahip olmanız gerekir.");
                Application.Exit();
            }
            js = (IJavaScriptExecutor) driver;
            yeni.modernLabel2.Text = "Twitter Açılıyor";
            yeni.bunifuCircleProgressbar1.Value = 35;
        tekrarla1:
            try {
                driver.Navigate().GoToUrl(@"https://twitter.com/login");
                yeni.bunifuCircleProgressbar1.Value = 40;
            }
            catch(WebDriverException) {
                MessageBox.Show("Adrese bağlanmak 60'saniyeden fazla sürdü. İnternet bağlantınızı kontrol ediniz. " +
                    "Sorun siz kaynaklı değilse twitter.com sunucuları tarafından bir sorun olabilir, lütfen daha sonra tekrar deneyiniz. ");
                yeni.modernLabel1.Text = "Hata Meydana Geldi";
                yeni.modernLabel2.Text = "Tekrar Deneniyor";
                goto tekrarla1;
            }
        }
        public void VeritabaniGuncelle(string g_kisi, string g_tablo, string g_sutun, string g_veri, string convert = "string") {
            cmd.Connection = baglanti;
            if(convert == "string") {
                cmd.CommandText = "UPDATE " + g_tablo + " SET " + g_sutun + "=@guncelint WHERE profili=@guncelkisi";
                cmd.Parameters.AddWithValue("@guncelint", g_veri);
                cmd.Parameters.AddWithValue("@guncelkisi", g_kisi);
            }
            else if(convert == "int") {
                cmd.CommandText = "update " + g_tablo + " set " + g_sutun + "= @guncelint where profili=" + g_kisi + "";
                cmd.Parameters.AddWithValue("@guncelint", Convert.ToInt32(g_veri));
            }
            else if(convert == "double ") {
                cmd.CommandText = "update" + g_tablo + " set " + g_sutun + "=@guncelint where profili=" + g_kisi + "";
                cmd.Parameters.AddWithValue("@guncelint", Convert.ToDouble(g_veri));
            }
        tekrardene:
            try {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) {
                if(MessageBox.Show(ex.Message + " Tekrar dene?", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry) goto tekrardene;
            }
        }
        public void SayfaLoadBekle() {
            do {
                Thread.Sleep(20);
            } while(!js.ExecuteScript("return document.readyState").Equals("complete"));
        }
        public void TwitterAyarla() {
            yeni.modernLabel2.Text = "Mod Ayarlanıyor";
            KaranlikMod();
            yeni.bunifuCircleProgressbar1.Value = 67;
            yeni.modernLabel1.Text = "Giriş Yapıldı";
            yeni.modernLabel2.Text = "Profil Açılıyor";
            yeni.bunifuCircleProgressbar1.Value = 98;
            ProfilBilgileri();
            SayfaLoadBekle();
            yeni.modernLabel1.Text = "Profil Bilgileri Çekildi";
            yeni.modernLabel2.Text = "...";
            yeni.bunifuCircleProgressbar1.Value = 1;
            yeni.bunifuCircleProgressbar1.MaxValue = kontrol_edilecek_tweet_sayisi + 1;
        }
        public void BegenileriKontrolEtVeritabanı() {
            yeni.Show();
            this.Hide();
        yeniden:
            if(yeni.bunifuCircleProgressbar1.Value == 1) {
                yeni.modernLabel1.Text = "Veritabanından kayıtlar çekiliyor..";
                VeritabanındanGetir("begenenler");
                if(performans) DetayVisible();
                CenterToScreen();
                this.Show();
                yeni.Hide();
            }
            else goto yeniden;
        }
        public void BegenileriKontrol() {
            yeni.Show();
            this.Hide();
        yeniden:
            if(yeni.bunifuCircleProgressbar1.Value == 1) {
                TweetleriCek(kullaniciadi, kontrol_edilecek_tweet_sayisi);
                BegenenleriKontrolEt();
            }
            else goto yeniden;
        }
        public void SSal() {
            Screenshot ss = ((ITakesScreenshot) driver).GetScreenshot();
            ss.SaveAsFile("C://Image.png",
            ScreenshotImageFormat.Png);
        }
        public void programkapat() {
            driver.Quit();
            driver2.Quit();
        }
        public void CheckBoxDonustur(ToolStripMenuItem nesne) {
            nesne.Checked = !nesne.Checked;
        }
        private int BegenenlerMatristeAra(string siradaki) {
            int index = -1;
            for(int i = 0; i < begenenler_matris.Length / 17; i++) {
                if(begenenler_matris[i, 3] == siradaki) { index = i; break; }
            }
            return index;
        }
        private int KaraListe(string profil) {
            komut = new OleDbCommand("INSERT into gtyapmayanlar (profili) Values (@profili)", baglanti);
            komut.Parameters.AddWithValue("@profili", profil);
            try {
                komut.ExecuteNonQuery();
                return 1;
            }
            catch(OleDbException ex) {
                if(ex.Message != "Tabloda yapılmasını istediğiniz değişiklikler, dizinde, birincil anahtarda veya ilişkide yinelenen değerler oluşturdukları için başarısız oldu. Yinelenen verileri içeren alan veya alanlardaki verileri değiştirin, dizini kaldırın veya dizini, yinelenen girdilere izin verecek şekilde yeniden tanımlayın ve yeniden deneyin.") MessageBox.Show(ex.Message);
                return 0;
            }
        }
        private int BeyazListe(string profil) {
            komut = new OleDbCommand("INSERT into beyazliste (kullaniciad) Values (@kullaniciadi)", baglanti);
            komut.Parameters.AddWithValue("@kullaniciadi", profil);
            try {
                komut.ExecuteNonQuery();
                return 1;
            }
            catch(OleDbException ex) {
                if(ex.Message != "Tabloda yapılmasını istediğiniz değişiklikler, dizinde, birincil anahtarda veya ilişkide yinelenen değerler oluşturdukları için başarısız oldu. Yinelenen verileri içeren alan veya alanlardaki verileri değiştirin, dizini kaldırın veya dizini, yinelenen girdilere izin verecek şekilde yeniden tanımlayın ve yeniden deneyin.") MessageBox.Show(ex.Message);
                return 0;
            }
        }
        private int GorunurColumnWidth() {
            int toplam = 0;
            for(int i = 0; i < bunifuCustomDataGrid2.Columns.Count; i++) {
                if(bunifuCustomDataGrid2.Columns[i].Visible) {
                    toplam += bunifuCustomDataGrid2.Columns[i].Width;
                }
            }
            return toplam;
        }
        private int GorunurColumnsWidth() {
            int uzunluk = 0;
            for(int i = 0; i < bunifuCustomDataGrid2.Columns.Count; i++) {
                if(bunifuCustomDataGrid2.Columns[i].Visible) {
                    uzunluk += bunifuCustomDataGrid2.Columns[i].Width;
                }
            }
            return uzunluk;
        }
        private int RamControl(int count) {
         
            if(count % 75 == 0) {
                int girisScroll =   int.Parse( ReturnKomutCalistir("return window.scrollY;"));
                yeni.modernLabel2.Text = "Bellek temizleniyor 15sn.";
                driver.Navigate().Refresh();
                if (!Limit(driverurl)) {
                    Thread.Sleep(10000);
                    yeni.modernLabel2.Text = "Bellek temizleniyor 5sn.";
                    driver.Navigate().Refresh();
                    Thread.Sleep(5000);

                } 
                yeni.modernLabel2.Text = "Bellek temizlendi. Kaydırılıyor...";
           
                    while (int.Parse(ReturnKomutCalistir("return window.scrollY;")) < girisScroll - 1300) {
                    KomutCalistir("window.scrollTo(0,"+(girisScroll - 1300) +")");
                    Thread.Sleep(50);
                }
                return count += 1;
            }
            return count;
        }
        public int TwitterAc(string yedekkullaniciadi) {
            yeni.modernLabel2.Text = "Hesaba Giriş Yapılıyor";
        yukle:
            try {
                if(yeni.bunifuCircleProgressbar1.Value == 40) {
                    if(onaykodu.Length > 0) {
                        driver.FindElement(By.Id("challenge_response")).SendKeys(onaykodu);
                        KomutCalistir("document.getElementsByClassName(\"EdgeButton EdgeButton--primary\")[0].click();");
                    }
                    else {
                        driver.FindElement(By.Name("session[username_or_email]")).SendKeys(kullaniciadi);
                        driver.FindElement(By.Name("session[password]")).SendKeys(sifre);
                        KomutCalistir(Komutlar.LoginButonuTikla);
                    }
                    if(yedekkullaniciadi != "") kullaniciadi = yedekkullaniciadi;
                    if("https://mobile.twitter.com/login/error?username_or_email=" + kullaniciadi + "&redirect_after_login=%2F" == driver.Url) {
                        yeni.modernLabel1.Text = "Hatalı Kullanıcı Adı veya Şifre Girdiniz.";
                        if(File.GetCreationTime(kullaniciadi) > DateTime.Now.AddMinutes(-10))
                            File.Delete(kullaniciadi);
                        driver.FindElement(By.Name("session[username_or_email]")).Clear();
                        return -1;
                    }
                    else if(driver.Url == "https://mobile.twitter.com/login?username_disabled=true&redirect_after_login=%2F") return 0;
                    else if(driver.Url.Contains("login_verification")) { return 2; }
                }
                else goto yukle;
                return 1;
            }
            catch(NoSuchElementException) {
                Thread.Sleep(50);
                goto yukle;
            }
            catch(Exception) {
                Thread.Sleep(50);
                goto yukle;
            }
        }
        public int TakipEdilenSayisi(string kullanici_id) {
            // if(!Limit(driverurl)) {
           
                js2.ExecuteScript("document.location.href = \"" + kullanici_id + "\";");
                profilYuklendiMiDrv2();
                string sayi = js2.ExecuteScript("return document.getElementsByClassName(\"css-1dbjc4n r-13awgt0 r-18u37iz r-1w6e6rj\")[0].innerText;").ToString();
                int sayison = sayi.IndexOf(" F");
                sayi = sayi.Remove(sayison, sayi.Length - sayison);
                sayi = Donustur(sayi);
                return Convert.ToInt32(sayi);
            
            // }

        }
        string formatDonustur(string kelime, bool calistir) {
            kelime = kelime.ToLower();
            char[] harfler = { 'ı', 'i', 'ü', 'u', 'ö', 'o', 'ş', 's', 'ç', 'c', 'ğ', 'g' };
            char[] noktalamaisaretleri = { '.', ',', ':', ';', '?', '!', '\"', '\'', '(', ')', '-', '/', '{', '}' };
            string olustur1 = "";
            for(int i = 0; i < kelime.Length; i++) {
                for(int j = 0; j < harfler.Length; j += 2) {
                    if(kelime[i] == harfler[j]) {
                        olustur1 += harfler[j + 1];
                        break;
                    }
                }
                if(olustur1.Length == i) olustur1 += kelime[i];
            }
            if(calistir) {
                string olustur2 = "";
                bool noktalamaisaretimi = false;
                for(int i = 0; i < olustur1.Length; i++) {
                    for(int j = 0; j < noktalamaisaretleri.Length; j++) {
                        if(olustur1[i] == noktalamaisaretleri[j] && ((i + 1 < olustur1.Length) && olustur1[i + 1] != ' ')) {
                            olustur2 += ' ';
                            noktalamaisaretimi = true;
                            break;
                        }
                        else if(olustur1[i] == noktalamaisaretleri[j]) {
                            noktalamaisaretimi = true;
                            break;
                        }

                    }
                    if(!noktalamaisaretimi) {
                        olustur2 += olustur1[i];
                        noktalamaisaretimi = false;
                    }
                }
                return olustur2;
            }
            return olustur1;

        }
        void ArgoTweet(string kullaniciid) {
            FileStream fs = new FileStream("D:\\" + kullaniciid + "_" + ".txt", FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);

            driver.Navigate().GoToUrl("https://twitter.com/" + kullaniciid + "/with_replies");
            Thread.Sleep(2000);
            string tweet;
            string link;
            int countToplam = 0;
            int countBulunan = 0;
            string argolar = "";
            int oncekikonum = 0;
            int sonrakikonum = 1;
            using(StreamReader sr = new StreamReader("argo.txt")) {
                argolar = sr.ReadToEnd();
            }
            string[] argolarDizi = argolar.Split('\n');
            string log;
            string bulunanargolarCumle = "";
            argolar = formatDonustur(argolar, false);
            ArrayList kontroledildi = new ArrayList();
            ArrayList bulunanargolar = new ArrayList();
            yeni.Show();
            this.Hide();
        yenidendene:
            if(js.ExecuteScript(Komutlar.LoginButonuGozukuyormu).ToString() == "1") {
                driver.Navigate().Refresh();
                Thread.Sleep(1000);
                goto yenidendene;
            }
            while(oncekikonum != sonrakikonum) {
                int sayfadakiTweet = Convert.ToInt32(ReturnKomutCalistir("return document.querySelectorAll('[data-testid = \"tweet\"]').length;"));
                for(var i = 0; i < sayfadakiTweet; i++) {
                    link = ReturnKomutCalistir("return document.querySelectorAll('[data-testid = \"tweet\"]')[" + i + "].children[1].children[0].children[0].children[0].children[0].children[2].getAttribute(\"href\"); ");
                    if(!kontroledildi.Contains(link) && link.Contains(kullaniciid)) {
                        if(ReturnKomutCalistir("return document.querySelectorAll('[data-testid = \"tweet\"]')[" + i + "].children[1].children[1].children[0].childElementCount; ") != "0") {
                            tweet = ReturnKomutCalistir("return document.querySelectorAll('[data-testid = \"tweet\"]')[" + i + "].children[1].children[1].children[0].children[0].innerText; ");
                            string formatUygunTweet = formatDonustur(tweet, true);
                            string[] parcala = formatUygunTweet.Split(' ');
                            bulunanargolar.Clear();
                            for(int j = 0; j < parcala.Length; j++) {
                                foreach(string argo in argolarDizi) {
                                    if(parcala[j] == argo) {
                                        bulunanargolar.Add(parcala[j]);
                                        break;
                                    }
                                }

                            }
                            if(bulunanargolar.Count > 0) {
                                bulunanargolarCumle = "";
                                for(int k = 0; k < bulunanargolar.Count; k++) bulunanargolarCumle += (k + 1) + ")" + bulunanargolar[k] + "\n";
                                countBulunan++;
                                sw.WriteLine("-------------------------------------------------- \n\n");
                                log = "Tweet: \n" + tweet + "\n\n" + "Bulunan Kelimeler: \n " + bulunanargolarCumle + "\n" + "Tweet linki: \n https://twitter.com" + link;
                                sw.WriteLine(log);
                                sw.WriteLine("\n\n--------------------------------------------------");

                            }
                        }

                        kontroledildi.Add(link);
                        countToplam++;
                        yeni.modernLabel2.Text = "Tweet Kontrol: " + countToplam.ToString() + " Bulunan: " + countBulunan;

                    }

                }
                oncekikonum = Convert.ToInt32(js.ExecuteScript(Komutlar.ScrollBarKonumu));
                js.ExecuteScript(Komutlar.SayfaKaydir);
                Thread.Sleep(1000);
                sonrakikonum = Convert.ToInt32(js.ExecuteScript(Komutlar.ScrollBarKonumu));
            }
            sw.WriteLine("\n\nTweet Kontrol: " + countToplam.ToString() + " Bulunan: " + countBulunan);
            sw.Close();
            this.Show();
            yeni.Hide();

        }
        private bool Limit(string yenileurl) {
            if(driver.Url == "https://mobile.twitter.com/i/rate-limited" || js.ExecuteScript("return document.getElementsByClassName(\"css-901oao r-jwli3a r-1wbh5a2 r-1qd0xha r-1b43r93 r-16dba41 r-ad9z0x r-bcqeeo r-5f36wq r-qvutc0\").length;").ToString() == "1" || js.ExecuteScript("return document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-urgr8i r-42olwf r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-145lgeb r-9u3a9d r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\").length;").ToString() != "0" || js.ExecuteScript("return document.getElementsByClassName('css-901oao r-jwli3a r-1wbh5a2 r-1qd0xha r-1b43r93 r-16dba41 r-hjklzo r-bcqeeo r-1qfz7tf r-qvutc0').length;").ToString() == "1") {
                yeni.modernLabel2.Text = "Limite takıldı. 5dk'a bekleyin. A11 ";
                yeni.modernLabel1.Text = "Başlangıç: " + DateTime.Now.ToString("HH:mm:ss") + " Bitiş: " + DateTime.Now.AddMinutes(5).ToString("HH:mm:ss");
                Thread.Sleep(300000);
                driver.Navigate().GoToUrl(yenileurl);
                yeni.modernLabel2.Text = "...";
                yeni.modernLabel1.Text = "Bekleyin";
                Thread.Sleep(1000);
                return true;
            }
            else return false;

        }
        private bool dosyakontrol(string yol) {
            if(File.Exists(@"C:\Program Files (x86)\" + yol) || File.Exists(@"C:\Program Files\" + yol)) {
                return true;
            }
            else return false;
        }
        private bool Filtre_Cinsiyet(string Cinsiyet) {
            if(sadeceErkeklerToolStripMenuItem.Checked && cinsiyet != "Erkek") return false;
            else if(sadeceKadınlarToolStripMenuItem.Checked && cinsiyet != "Kadın") return false;
            else if(toolStripMenuItem17.Checked && cinsiyet == "Erkek") return false;
            return true;
        }
        private bool BegeniKontrol(string tweetlink) {
        limittendon:
            driver.Navigate().GoToUrl("https://mobile.twitter.com" + tweetlink + "/likes");
            gecerli_tweeti_begenenler.Clear();
        kaydiranam:
            if(Convert.ToInt32(js.ExecuteScript(Komutlar.TweetBegenisiVarmi)) == 1) return false;
            sayfadaki_classsayisi = Convert.ToInt32(js.ExecuteScript(Komutlar.KullanıcıDivSayısı));
            Thread.Sleep(300);
            if(sayfadaki_classsayisi == 0) {
                try { js.ExecuteScript("document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-urgr8i r-42olwf r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-145lgeb r-9u3a9d r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\")[0].click();"); } catch(Exception) {; } /*Yenile butonu tıklatma */
                goto kaydiranam;
            }
            DegiskenleriTemizle();
            for(var x = 0; x < sayfadaki_classsayisi; x++) {
                try {
                    kapsayiciHTML = js.ExecuteScript(Komutlar.KapsayiciHTML.Replace("xxxx", x.ToString())).ToString();
                    kapsayiciText = js.ExecuteScript(Komutlar.kapsayiciText.Replace("xxxx", x.ToString())).ToString();
                }
                catch(Exception) { Thread.Sleep(120); continue; }
                siradaki_begenen = "@" + Bul(kapsayiciHTML, ">@", "</span>");
                kapsayiciText = kapsayiciText.Replace(siradaki_begenen, "");
                if(Limit("https://mobile.twitter.com" + tweetlink + "/likes")) goto limittendon;
                if(!gecerli_tweeti_begenenler.Contains(siradaki_begenen)) {
                    int indeks = tum_tweetleri_begenenler.IndexOf(siradaki_begenen);
                    if(indeks == -1) {
                        TemelBilgiler(x, "BegenenleriKontrolEt", kapsayiciHTML, kapsayiciText);
                    }
                    else {
                        bunifuCustomDataGrid2.Rows[(indeks / 2)].Cells[4].Value = Convert.ToInt32(bunifuCustomDataGrid2.Rows[(indeks / 2)].Cells[4].Value) + 1;
                        tum_tweetleri_begenenler[indeks + 1] = Convert.ToInt64(tum_tweetleri_begenenler[indeks + 1]) + 1;
                        yeni.modernLabel2.Text = siradaki_begenen + " x" + tum_tweetleri_begenenler[indeks + 1].ToString(); ;
                    }
                    gecerli_tweeti_begenenler.Add(siradaki_begenen);
                }
                else continue;
            }
            double onceki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
            js.ExecuteScript(Komutlar.SayfaKaydir.Replace("1000", "400"));
            double sonraki_konum = Convert.ToDouble(js.ExecuteScript(Komutlar.ScrollBarKonumu));
            if(sonraki_konum != onceki_konum) goto kaydiranam;
            return true;
        }
        public bool KomutCalistir(string komut) {
            int calisma_sayisi = 0;
        ogeyibekle1:
            try {
                js.ExecuteScript(komut);
                return true;
            }
            catch(Exception) {
                if(calisma_sayisi != 40) {
                    Thread.Sleep(30);
                    calisma_sayisi++;
                    goto ogeyibekle1;
                }
                else {
                    return false;
                }
            }
        }
        private double KayıtTarihi(string kayittarihi) {
            ayadi = kayittarihi.Substring(0, kayittarihi.IndexOf(' '));
            yil = Convert.ToInt32(kayittarihi.Substring(kayittarihi.IndexOf(' '), 5));
            kacinciay = 01;
            switch(ayadi) {
                case "Ocak": kacinciay = 01; break;
                case "Şubat": kacinciay = 02; break;
                case "Mart": kacinciay = 03; break;
                case "Nisan": kacinciay = 04; break;
                case "Mayıs": kacinciay = 05; break;
                case "Haziran": kacinciay = 06; break;
                case "Temmuz": kacinciay = 07; break;
                case "Ağustos": kacinciay = 08; break;
                case "Eylül": kacinciay = 09; break;
                case "Ekim": kacinciay = 10; break;
                case "Kasım": kacinciay = 11; break;
                case "Aralık": kacinciay = 12; break;
                default: break;
            }
            baslamaTarihi = new DateTime(yil, kacinciay, 01);
            bitisTarihi = DateTime.Today;
            kalangun = bitisTarihi - baslamaTarihi;//Sonucu zaman olarak döndürür
            return kalangun.TotalDays;// kalanGun den TotalDays ile sadece toplam gun değerini çekiyoruz.
        }
        private void GridSatirEkle(int p_sira, string p_siradaki_begenen_ad, string p_siradaki_begenen, int p_begenisay, string p_fotograf_url, string p_cinsiyet, string p_biyografi, string p_konum, string p_takipedilen, string p_takipcisayisi, double p_toplamGun, double p_gunlukbegeni, string p_tweetsayisi, double p_gunluktweet, string p_takip_ediyormu, string p_takipedilmedurumu) {
            Bitmap Resim = ResimOlustur(p_fotograf_url, p_siradaki_begenen, Color.White);
            if(fotograf_hatali) p_fotograf_url = guncellink;
            bunifuCustomDataGrid2.Rows.Add(
                               p_sira, Resim,
                               p_siradaki_begenen_ad, p_siradaki_begenen,
                               Convert.ToInt32(p_begenisay), p_fotograf_url, p_cinsiyet,
                               p_biyografi, p_konum,
                               Convert.ToInt32(p_takipedilen), Convert.ToInt32(p_takipcisayisi),
                               Convert.ToInt32(p_toplamGun), p_gunlukbegeni,
                               Convert.ToInt32(p_tweetsayisi), p_gunluktweet,
                               p_takip_ediyormu, p_takipedilmedurumu);
        }
        private void Anaekran_Load(object sender, EventArgs e) {
            ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem.Checked = performans;
            toolStripMenuItem6.Checked = veritabanındancek;
            takipEtmediğimKişileriSaymaToolStripMenuItem.Checked = takipci_filtresi;
            using(StreamReader sr = new StreamReader("isimler.txt")) {
                line = sr.ReadToEnd();
            }
            line = line.ToLower();
            //
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.None;
            this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, 1050);
            this.Invoke((MethodInvoker) delegate {
                bunifuCustomDataGrid2.ScrollBars = ScrollBars.Both; // runs on UI thread
            });
            bunifuCustomDataGrid2.ScrollBars = ScrollBars.Both;
            //
            modernPanel1.Visible = true;
            modernPanel1.Visible = false; this.Hide();
            GridOlustur();
            CenterToScreen();
        }
        private void azOlanlarToolStripMenuItem_Click(object sender, EventArgs e) {
            if(azOlanlarToolStripMenuItem.Checked) {
                azOlanlarToolStripMenuItem.Checked = false;
            }
            else {
                tweetSayısıToolStripMenuItem.Checked = true;
                azOlanlarToolStripMenuItem.Checked = true;
                if(fazlaOlanlarToolStripMenuItem.Checked) {
                    fazlaOlanlarToolStripMenuItem.Checked = false;
                }
            }
        }
        private void azOlanlarToolStripMenuItem1_Click(object sender, EventArgs e) {
            if(azOlanlarToolStripMenuItem1.Checked) {
                azOlanlarToolStripMenuItem1.Checked = false;
            }
            else {
                beğeniSayısıToolStripMenuItem1.Checked = true;
                azOlanlarToolStripMenuItem1.Checked = true;
                if(fazlaOlanlarToolStripMenuItem1.Checked) {
                    fazlaOlanlarToolStripMenuItem1.Checked = false;
                }
            }
        }
        private void azOlanlarToolStripMenuItem2_Click(object sender, EventArgs e) {
            if(azOlanlarToolStripMenuItem2.Checked) {
                azOlanlarToolStripMenuItem2.Checked = false;
            }
            else {
                günlükTweetSayısıToolStripMenuItem1.Checked = true;
                azOlanlarToolStripMenuItem2.Checked = true;
                if(fazlaOlanlarToolStripMenuItem2.Checked) {
                    fazlaOlanlarToolStripMenuItem2.Checked = false;
                }
            }
        }
        private void azOlanlarToolStripMenuItem3_Click(object sender, EventArgs e) {
            if(azOlanlarToolStripMenuItem3.Checked) {
                azOlanlarToolStripMenuItem3.Checked = false;
            }
            else {
                günlükBeğeniSayısıToolStripMenuItem.Checked = true;
                azOlanlarToolStripMenuItem3.Checked = true;
                if(fazlaOlanlarToolStripMenuItem3.Checked) {
                    fazlaOlanlarToolStripMenuItem3.Checked = false;
                }
            }
        }
       
        private void beğeniSayısıToolStripMenuItem1_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void beniTakipEtmeyenlerToolStripMenuItem_Click(object sender, EventArgs e) {
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                if(bunifuCustomDataGrid2.Rows[i].Cells[15].Value.ToString() != "Seni takip ediyor") {
                    bunifuCustomDataGrid2.Rows[i].Visible = true;
                }
                else {
                    bunifuCustomDataGrid2.Rows[i].Visible = false;
                }
            }
        }
        private void beniTakipEtmeyenlerToolStripMenuItem1_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void birdenFazlaSeçenekİçinArasınaKoyunToolStripMenuItem_Click(object sender, EventArgs e) {
            konumuToolStripMenuItem.Checked = true;
        }
        private void bunifuCustomDataGrid2_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {
            yeni2 = new buyut();
            if(e.ColumnIndex == 1) {
                bool kilitlihesap = false;
                yeni2.Hide();
                int x, y;
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
                yeni2.Width = 500;
                yeni2.Height = 500;
                if(y < Screen.PrimaryScreen.Bounds.Height / 2) {
                    yeni2.Location = new System.Drawing.Point(x, y);
                }
                else {
                    yeni2.Location = new System.Drawing.Point(x, y - 500);
                }
                try {
                    fotograf = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("x96", "400x400");
                    if(fotograf.Contains("Kilitli")) {
                        kilitlihesap = true;
                        fotograf = fotograf.Replace("Kilitli", "");
                    }
                }
                catch(ArgumentOutOfRangeException) {
                    fotograf = "";
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                try {
                    yeni2.pictureBox1.Load(fotograf);
                    yeni2.ShowDialog();
                }
                catch(WebException) {
                    string lnk = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    int yenidensayisi = 0;
                yeniden:
                    try {
                        string yeni_url = FotografUrl(lnk);
                        if(kilitlihesap) yeni_url = "Kilitli" + yeni_url;
                        VeritabaniGuncelle(lnk, "begenenler", "fotografyolu", yeni_url);
                        VeritabaniGuncelle(lnk, "takipciler", "fotografyolu", yeni_url);
                        bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[5].Value = yeni_url;
                        bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[1].Value = ResimOlustur(yeni_url, lnk, Color.White);
                        fotograf = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("x96", "400x400").Replace("Kilitli", "");
                        yeni2.pictureBox1.Load(fotograf);
                        yeni2.ShowDialog();
                    }
                    catch(Exception) {
                        yenidensayisi++;
                        if(yenidensayisi > 15) {
                            MessageBox.Show("Kullanıcı Bulunamadı");
                        }
                        else {
                            Thread.Sleep(100);
                            goto yeniden;
                        }
                    }
                }
                catch(FileNotFoundException) { MessageBox.Show("Kullanıcı Bulunamadı! Hesap adını değiştirmiş veya hesabını kapatmış olabilir."); }
                catch(InvalidOperationException) {; }
            }
            else if(e.ColumnIndex == 3) {
                try {
                    string lnk = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    lnk = "https://www.twitter.com/" + lnk;
                    ProcessStartInfo psinfo = new ProcessStartInfo(lnk);
                    Process.Start(psinfo);
                }
                catch(ArgumentOutOfRangeException) {
                    ;
                }
            }
            else if(e.ColumnIndex == 6) {
                try {
                    cinsiyet_guncelle yeni22 = new cinsiyet_guncelle();
                    yeni22.Hide();
                    int x, y;
                    x = Cursor.Position.X;
                    y = Cursor.Position.Y;
                    yeni22.isim = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    yeni22.satirindex = e.RowIndex;
                    yeni22.profil = bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    yeni22.Location = new System.Drawing.Point(x, y);
                    // yeni22.Size = new Size(100, 50);
                    yeni22.ShowDialog();
                }
                catch(ArgumentOutOfRangeException) {
                    ;
                }
            }
            else if(e.ColumnIndex == 16) {
                try {
                    driver.Navigate().GoToUrl("https://www.twitter.com/" + bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString());
                    if(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Takip et") {
                        KomutCalistir("var mother = document.getElementsByClassName(\"css-18t94o4 css-1dbjc4n r-1niwhzg r-p1n3y5 r-sdzlij r-1phboty r-rs99b7 r-1w2pmg r-145lgeb r-9u3a9d r-1ny4l3l r-1fneopy r-o7ynqc r-6416eg r-lrvibr\")[0]; mother.click();");
                        KaraListe(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString());
                        VeritabaniGuncelle(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString(), "takipciler", "takipet", "Takip ediliyor");
                        VeritabaniGuncelle(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString(), "begenenler", "takipet", "Takip ediliyor");
                        bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#719663");
                        bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#719663");
                        bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                        bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Takip ediliyor";
                    }
                    else {
                        OleDbDataAdapter da = new OleDbDataAdapter("Select kullaniciad from beyazliste ", baglanti);
                        DataTable tbl = new DataTable();
                        da.Fill(tbl);
                        DataRow[] filteredRows = tbl.Select(string.Format("{0} LIKE '%{1}%'", "kullaniciad", bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString()));
                        if((filteredRows.Length == 0 && beyazListedekiKullanıcılarıTakiptenÇıkmaToolStripMenuItem.Checked) || !beyazListedekiKullanıcılarıTakiptenÇıkmaToolStripMenuItem.Checked) {
                            if(KomutCalistir(Komutlar.TakiptenCik1Text) && KomutCalistir(Komutlar.TakiptenCik1)) KomutCalistir(Komutlar.TakiptenCikAcilirPencere);
                            KaraListe(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString());
                            VeritabaniGuncelle(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString(), "takipciler", "takipet", "Takip et");
                            VeritabaniGuncelle(bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[3].Value.ToString(), "begenenler", "takipet", "Takip et");
                            bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#c41b4d");
                            bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#c41b4d");
                            bunifuCustomDataGrid2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                            bunifuCustomDataGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Takip et";
                        }
                    }
                }
                catch(ArgumentOutOfRangeException) {
                    ;
                }
            }
        }
        private void bunifuCustomDataGrid2_SelectionChanged(object sender, EventArgs e) {
            toolStripMenuItem2.Text = bunifuCustomDataGrid2.SelectedRows.Count.ToString();
            toolStripMenuItem1.Text = bunifuCustomDataGrid2.SelectedRows.Count.ToString();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e) {
            bunifuFlatButton1.Normalcolor = Color.BlueViolet;
            bunifuFlatButton1.IconRightVisible = true;
            bunifuFlatButton1.IconVisible = false;
            bunifuFlatButton1.TextAlign = ContentAlignment.MiddleRight;
            bunifuFlatButton5.Normalcolor = Color.Red;
            bunifuFlatButton5.IconRightVisible = false;
            bunifuFlatButton5.IconVisible = true;
            bunifuFlatButton5.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void bunifuFlatButton1_Click_2(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                yeni.Show();
                this.Hide();
                driver.Navigate().GoToUrl("https://twitter.com/" + modernTextBox3.Text);
                SayfaLoadBekle();
                Thread baskasinibegenenler = new Thread(new ThreadStart(BaskasiniBegenenler));
                baskasinibegenenler.Start();
            }
            else {
                MessageBox.Show("Kullanıcı Adı Giriniz!");
            }
        }
        private void bunifuFlatButton3_Click_1(object sender, EventArgs e) {
            bunifuCustomDataGrid2.Rows.Clear();
            tum_tweetleri_begenenler.Clear();
            gecerli_tweeti_begenenler.Clear();
            DegiskenleriTemizle();
            Thread takiplistem = new Thread(new ThreadStart(TakipEttiklerim));
            takiplistem.Start();
        }
        private void bunifuFlatButton4_Click_1(object sender, EventArgs e) {
        }
        private void bunifuFlatButton5_Click(object sender, EventArgs e) {
            bunifuCustomDataGrid2.Rows.Clear();
            DegiskenleriTemizle();
            Thread takiplistem = new Thread(new ThreadStart(GeriTakipYapmayanlar));
            takiplistem.Start();
        }
        private void bunifuFlatButton6_Click_1(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                driver.Navigate().GoToUrl("https://mobile.twitter.com/" + modernTextBox3.Text + "/followers");
                Thread.Sleep(2000);
                Thread basksaininlistesi = new Thread(() => {
                    BaskasinininListesi(Convert.ToInt32(toolStripTextBox13.Text));
                });
                basksaininlistesi.Start();
            }
            else {
                MessageBox.Show("Kullanıcı Adı Giriniz!");
            }
        }
        private void bunifuFlatButton7_Click(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                driver.Navigate().GoToUrl("https://mobile.twitter.com/" + modernTextBox3.Text + "/following");
                Thread.Sleep(2000);
                Thread basksaininlistesi = new Thread(() => {
                    BaskasinininListesi(Convert.ToInt32(toolStripTextBox13.Text));
                });
                basksaininlistesi.Start();
            }
            else {
                MessageBox.Show("Kullanıcı Adı Giriniz!");
            }
        }
        private void bunifuFlatButton8_Click(object sender, EventArgs e) {
            kontroledildi.Clear();
            driver.Navigate().GoToUrl("https://mobile.twitter.com/messages");
            mesajsilsec secenekler = new mesajsilsec();
            secenekler.ShowDialog();
            if(basilanbuton == 1) {
                Thread tumemsajlar = new Thread(new ThreadStart(TumMesajlarıSil));
                tumemsajlar.Start();
            }
            else if(basilanbuton == 2) {
                Thread secilimesajlar = new Thread(new ThreadStart(() => { MesajSil("secerek"); }));
                secilimesajlar.Start();
            }
            else if(basilanbuton == 3) {
                Thread takipetmedigimmesajlar = new Thread(new ThreadStart(TakipEtmedigimMesajlarıSil));
                takipetmedigimmesajlar.Start();
            }
        }
        private void bunifuFlatButton9_Click(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                //  BegeniKontrol();
            }
            else {
                MessageBox.Show("Kullanıcı Adı Giriniz!");
            }
        }
        private void büyütToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!bunifuGradientPanel11.Visible) {
                bunifuGradientPanel11.Visible = true;
                büyükToolStripMenuItem.Text = "Büyüt";
                for(int i = 7; i < 14; i++) {
                    if(i == 12) { continue; }
                    if(i == 10) {
                        bunifuCustomDataGrid2.Columns[i].Width -= 50;
                    }
                    else {
                        bunifuCustomDataGrid2.Columns[i].Width -= 30;
                    }
                }
            }
            else {
                bunifuGradientPanel11.Visible = false;
                büyükToolStripMenuItem.Text = "Küçült";
                for(int i = 7; i < 14; i++) {
                    if(i == 12) { continue; }
                    if(i == 10) {
                        bunifuCustomDataGrid2.Columns[i].Width += 50;
                    }
                    else {
                        bunifuCustomDataGrid2.Columns[i].Width += 30;
                    }
                }
            }
        }
        private void detayİstemiyorumHızlıOlToolStripMenuItem_Click(object sender, EventArgs e) {
            DetayVisible();
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void fazlaOlanlarToolStripMenuItem_Click(object sender, EventArgs e) {
            if(fazlaOlanlarToolStripMenuItem.Checked) {
                fazlaOlanlarToolStripMenuItem.Checked = false;
            }
            else {
                tweetSayısıToolStripMenuItem.Checked = true;
                fazlaOlanlarToolStripMenuItem.Checked = true;
                if(azOlanlarToolStripMenuItem.Checked) {
                    azOlanlarToolStripMenuItem.Checked = false;
                }
            }
        }
        private void fazlaOlanlarToolStripMenuItem1_Click(object sender, EventArgs e) {
            if(fazlaOlanlarToolStripMenuItem1.Checked) {
                fazlaOlanlarToolStripMenuItem1.Checked = false;
            }
            else {
                beğeniSayısıToolStripMenuItem1.Checked = true;
                fazlaOlanlarToolStripMenuItem1.Checked = true;
                if(azOlanlarToolStripMenuItem1.Checked) {
                    azOlanlarToolStripMenuItem1.Checked = false;
                }
            }
        }
        private void fazlaOlanlarToolStripMenuItem2_Click(object sender, EventArgs e) {
            if(fazlaOlanlarToolStripMenuItem2.Checked) {
                fazlaOlanlarToolStripMenuItem2.Checked = false;
            }
            else {
                günlükTweetSayısıToolStripMenuItem1.Checked = true;
                fazlaOlanlarToolStripMenuItem2.Checked = true;
                if(azOlanlarToolStripMenuItem2.Checked) {
                    azOlanlarToolStripMenuItem2.Checked = false;
                }
            }
        }
        private void fazlaOlanlarToolStripMenuItem3_Click(object sender, EventArgs e) {
            if(fazlaOlanlarToolStripMenuItem3.Checked) {
                fazlaOlanlarToolStripMenuItem3.Checked = false;
            }
            else {
                günlükBeğeniSayısıToolStripMenuItem.Checked = true;
                fazlaOlanlarToolStripMenuItem3.Checked = true;
                if(azOlanlarToolStripMenuItem3.Checked) {
                    azOlanlarToolStripMenuItem3.Checked = false;
                }
            }
        }
        private void gündenFazlaToolStripMenuItem_Click(object sender, EventArgs e) {
            kayıtTarihiToolStripMenuItem.Checked = true;
        }
        private void günlükBeğeniSayısıToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void günlükTweetSayısıToolStripMenuItem1_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void hepsiToolStripMenuItem_Click(object sender, EventArgs e) {
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                bunifuCustomDataGrid2.Rows[i].Visible = true;
            }
        }
        private void kapatToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        private void kayıtTarihiToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void konumuToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void takipEtmediğimKişileriSaymaToolStripMenuItem_Click(object sender, EventArgs e) {
            if(takipEtmediğimKişileriSaymaToolStripMenuItem.Checked) {
                takipEtmediğimKişileriSaymaToolStripMenuItem.Checked = false;
                takipci_filtresi = false;
            }
            else {
                takipEtmediğimKişileriSaymaToolStripMenuItem.Checked = true;
                takipci_filtresi = true;
            }
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e) {
            if(toolStripMenuItem6.Checked) {
                toolStripMenuItem6.Checked = false;
                veritabanındancek = false;
            }
            else {
                toolStripMenuItem6.Checked = true;
                veritabanındancek = true;
            }
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem.Checked) {
                MessageBox.Show("Bu seçeneği seçmeniz durumunda kullanıcıların: beğeni sayısı, günlük beğeni, günlük tweet sayısı, takipçi, takip edilen sayıları, konum ve üyelik süreleri gibi bilgiler gösterilmeyecektir. Avantaj olarak hız konusunda olumlu fark yaratacaktır. sadece sizi en çok beğenen hesapları listelemek istiyorsanız bu seçeneği işaretlemeniz tavsiye edilir.", "Performans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                performans = true;
                DetayVisible();
                ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem.Checked = true;
            }
            else {
                ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem.Checked = false;
                performans = false;
                DetayVisible();
            }
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void siraToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 0);
        }
        private void profilFotoğrafıToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 1);
        }
        private void ismiToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 2);
        }
        private void profiliToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 3);
        }
        private void beğeniSayısıToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 4);
        }
        private void konumToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 8);
        }
        private void cinsiyetToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 6);
        }
        private void biyografiToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 7);
        }
        private void takipEttiğiToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 9);
        }
        private void takipçiToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 10);
        }
        private void günlükBeğeniToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 12);
        }
        private void günlükTweetToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 14);
        }
        private void takipEtmeDurumuToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 15);
        }
        private void takipEdilmeDurumuToolStripMenuItem_Click(object sender, EventArgs e) {
            GridColumnGizle((ToolStripMenuItem) sender, 16);
        }
        private void öncedenGTYapmayanlarıAtlaToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void bunifuCustomDataGrid2_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            //Skip the Column and Row headers
            if(e.ColumnIndex < 0 || e.RowIndex < 0) {
                return;
            }
            var dataGridView = (sender as DataGridView);
            //Check the condition as per the requirement casting the cell value to the appropriate type
            if(e.ColumnIndex == 16 || e.ColumnIndex == 1 || e.ColumnIndex == 6)
                dataGridView.Cursor = Cursors.Hand;
            else
                dataGridView.Cursor = Cursors.Default;
        }
        private void listeSeçToolStripMenuItem_Click(object sender, EventArgs e) {
            listesecenek = 0;
            Form1 listeismial = new Form1();
            listeismial.ShowDialog();
        }
        private void seçiliListeyeEkleToolStripMenuItem_Click(object sender, EventArgs e) {
            if(driver.Url.Contains("/members/suggested")) {
                foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
                {
                    ListeyeKisiEkle(drow.Cells[3].Value.ToString());
                    drow.DefaultCellStyle.BackColor = Color.SkyBlue;
                    drow.DefaultCellStyle.ForeColor = Color.Black;
                    drow.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                    drow.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
            else {
                MessageBox.Show("Liste seçmediniz.");
            }
        }
        private void toolStripMenuItem15_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void modernLabel4_TextChanged(object sender, EventArgs e) {
            try {
                int sayi1 = Convert.ToInt32(modernLabel13.Text);
                int sayi2 = Convert.ToInt32(modernLabel4.Text);
                modernLabel6.Text = (Convert.ToDouble(sayi1) / Convert.ToDouble(sayi2)).ToString();
            }
            catch(Exception) {
                ;
            }
        }
        private void detayGizleToolStripMenuItem_Click(object sender, EventArgs e) {
            if(detayGizleToolStripMenuItem.Checked) {
                detayGizleToolStripMenuItem.Checked = false;
                DetayVisible();
            }
            else {
                detayGizleToolStripMenuItem.Checked = true;
                DetayVisible();
            }
        }
        private void toolStripMenuItem16_Click_1(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void bunifuImageButton1_Click_1(object sender, EventArgs e) {
            if(bunifuGradientPanel8.Visible) {
                bunifuGradientPanel8.Visible = false;
                ((BunifuImageButton) sender).Image = Image.FromFile(@"img\383150-32 (1).png");
            }
            else {
                bunifuGradientPanel8.Visible = true;
                ((BunifuImageButton) sender).Image = Image.FromFile(@"img\383150-32.png");
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e) {
            Thread yenile = new Thread(new ThreadStart(GridViewAnaliz));
            yenile.Start();
        }
        private void toolStripMenuItem17_Click(object sender, EventArgs e) {
            if(toolStripMenuItem17.Checked) {
                toolStripMenuItem17.Checked = false;
            }
            else {
                toolStripMenuItem5.Checked = false;
                toolStripMenuItem17.Checked = true;
                sadeceKadınlarToolStripMenuItem.Checked = false;
                sadeceErkeklerToolStripMenuItem.Checked = false;
            }
        }
        private void listedekiKullanıcılarıBeyazListeyeEkleToolStripMenuItem_Click(object sender, EventArgs e) {
            listesecenek = 1;
            Form1 listeismial = new Form1();
            listeismial.ShowDialog();
            Thread.Sleep(2500);
            if(driver.Url.Contains("/members")) {
                ListeyiListeyeEkle("Beyaz");
            }
            else {
                MessageBox.Show("Liste seçmediniz.");
            }
        }
        private void listedekiKullanıcılarıKaraListeyeEkleToolStripMenuItem_Click(object sender, EventArgs e) {
            listesecenek = 1;
            Form1 listeismial = new Form1();
            listeismial.ShowDialog();
            Thread.Sleep(2500);
            if(driver.Url.Contains("/members")) {
                ListeyiListeyeEkle("Kara");
            }
            else {
                MessageBox.Show("Liste seçmediniz.");
            }
        }
        private void bunifuFlatButton10_Click(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                driver.Navigate().GoToUrl("https://mobile.twitter.com/" + modernTextBox3.Text + "/followers_you_follow");
                Thread.Sleep(2000);
                Thread basksaininlistesi = new Thread(() => {
                    BaskasinininListesi(Convert.ToInt32(toolStripTextBox13.Text));
                });
                basksaininlistesi.Start();
            }
            else {
                MessageBox.Show("Kullanıcı Adı Giriniz!");
            }
        }
        private void listeListeleToolStripMenuItem_Click(object sender, EventArgs e) {
            Listelistele lis = new Listelistele();
            lis.anaform = this;
            lis.ShowDialog();
        }
        private void seçilenKişileriBeyazListeyeEkleToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                Thread t = new Thread(() => BeyazListe(drow.Cells[16].Value.ToString()));
                t.Start();
            }
        }
        private void beyazListedekiKullanıcılarıTakiptenÇıkmaToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void bunifuFlatButton11_Click(object sender, EventArgs e) {
            int txt2 = Convert.ToInt32(modernTextBox2.Text);
            int txt5 = Convert.ToInt32(modernTextBox5.Text);
            if(txt2 <= txt5)
                arasiBegenenler(txt2, txt5);
            else
                MessageBox.Show("Soldaki kutucuk, sağdaki kutucuktan büyük olamaz.");
        }
        private void güvenliModToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void toolStripMenuItem19_Click(object sender, EventArgs e) {
            ((ToolStripMenuItem) sender).Checked = !detayvisible;
            DetayVisible();
        }
        private void toolStripMenuItem18_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void modernTextBox3_Click(object sender, EventArgs e) {
            bunifuFlatButton1.Enabled = true;
        }
        private void modernTextBox3_TextChanged(object sender, EventArgs e) {
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton6.Enabled = true;
            bunifuFlatButton7.Enabled = true;
            bunifuFlatButton9.Enabled = true;
        }
        private void karaListeToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                Thread t = new Thread(() => KaraListe(drow.Cells[16].Value.ToString()));
                t.Start();
            }
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e) {
            if(toolStripMenuItem7.Checked) {
                toolStripMenuItem7.Checked = false;
            }
            else {
                toolStripMenuItem8.Checked = false;
                toolStripMenuItem7.Checked = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e) {
            if(modernTextBox3.Text != "") {
                Thread argobulucu = new Thread(() => {
                    ArgoTweet(modernTextBox3.Text);
                });
                argobulucu.Start();
            }
            else MessageBox.Show("Kullanıcı adı girmediniz.");

        }

        private void şuKişileriEngelleToolStripMenuItem_Click(object sender, EventArgs e) {
            string engellenecekler = "@Nurullh_aktss @antikralice @torpillipstick @newendyy @emirrtimurr @llillmommy @melikerbas_ @yarenergvn @cansusuzluk @konusanlarhasan @kadin_cinayeti @wesligs @madrigalofc @daddyl0ngglegss @taladrovevo @yasirbrk @stelleuna @busrayamnn @toskofacts @xautopsyx_ @regloloji @Enes_Cingoz @ggestordpunk @OwL1804 @melisadoqann @benipellaa @birfrederick @Esnafthefuckkk @mertrobooming @YildizzTilbee @thanksnextbeyb @ozeeronurr @lldylic @vjbulentt @punkbimbo @fzaofficial @astrologelvan @geminedim @lebrizim @dizyella @cycynz1 @sessizsiir63 @emrediyosun @siiristh @Dayi @xikayar @fuckburc @leldamnright @ferhatdemebana @PoyrazKarayeel2 @ononkonser @miyavlatsana @13burctahmin @kullnmyomkanks @kelimeburc @sansursuzprof @YildizTiIbe @furkandurmazw @KuzeyGuney @netameliherr @caglarrcgrr @mupteezel @hasanbasrikck @rakiigunlugu @berkayprofil @BarbieSozler @elyvemavi @aliaktasch @aurasgood @alperk55 @vevoDuman @rasitbostanci @sorunluyumbeybi @Sozyuku @Dolukadehden @iamcardib @Benibrahimmm @kediefy @moodegram @biidurunbeee @zatenyanmisim @nolurbisev @Tugkanderrki @lanbiisus @anlatyawrum @isheniz @Hephaklliyiim @aysenimsi_2 @Sezenimderki @venusmuyumxXx @hictakmiyorum @batuyavuzzz @theilkerjk @beybigunes @dalincaa @theistumblr @grahamlicc @lisanslibuyucu @b4rbiesgun @Suatkkocax @cinkopilsuat @hayvanlardan @minmarpia @cagritaner @wtfderin @senikonusalim @yunusceliik @lutfensakinles @tinercimayki @ftmglgnn @kadrajmagazin @seldaaydnn_ @fatmalakusx @mitokondrivari @avnicnbalta @ozgunhdr @dilaaree @sergulyilmaz @holyemotions @devrimkutluu @SinanKcarslan @VBekdas";
            string[] hesaplar = engellenecekler.Split(' ');
            for(int i = 0; i < hesaplar.Length; i++) {
                driver.Navigate().GoToUrl("https://mobile.twitter.com/" + hesaplar[i]);
                Thread.Sleep(1500);
                if(ReturnKomutCalistir("return document.querySelector('[data-testid=placementTracking]').innerText;") != "Engellendi") {
                    KomutCalistir("document.querySelector('[data-testid=userActions]').click();");
                    KomutCalistir("document.querySelector('[data-testid=block]').click();");
                    KomutCalistir("document.querySelector('[data-testid=confirmationSheetConfirm]').click();");
                }
            }
        }

        private void şuKeliemeleriSessizeAlToolStripMenuItem_Click(object sender, EventArgs e) {
            string kelimeler = "lgbtq-eşcinsel-LGBT-için adalet-regl-rt edermisiniz-rt eder misiniz-rt yaparmısınız-rt yapar mısınız-yuva arıyoruz-destek olalım-kadın-kadın hak-feminist-feminizm-sma'lı-sma-kadın cinayet-hayvan hakları-kadına şiddet-erkek cinayetleri-katil-kadın cinayetleri-sma hastası-en güzel halinle-Başka bir evrende-seni dert etmeler-madrigal-golllll-temas bagimlisi-temas bağımlısı-karıya kıza doymuş erkek-kariya kiza doymus erkek-14 şubat-sevgililer günü-çekiliş-Ölmek-Emine-Cinayet-@emniyetGM-norm ender-fero-ezhel-reynmen-gluk gluk-hapis-neyim ben tw'nin-neyim ben twnin-neyim ben twitterın-neyim ben-neyim ben twitter'ın en-sapık-taciz-hakem-kale-gooooooooooooool-goooooooooooool-gooooooooooool-goooooooooool-gooooooooool-goooooooool-gooooooool-goooooool-gooooool-goooool-gooool-goool-gool-fatih hoca-fatih terim-fener-gs-fb-bjk-beşiktaş-tecavüz-kadın hakları-hakan-sahra-gönüllüler-ünlüler-murat-hilmicem-nihat-turabi-survivor-acun-derbi-gol-Fenerbahçe-galatasaray-cüneyt çakır-çakır-cüneyt-futbol-maç-yakışıklı-";
            string[] kelimelerayikla = kelimeler.Split('-');

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e) {
            if(toolStripMenuItem8.Checked) {
                toolStripMenuItem8.Checked = false;
            }
            else {
                toolStripMenuItem7.Checked = false;
                toolStripMenuItem8.Checked = true;
            }
        }
        private void GridAnalizButon_Click(object sender, EventArgs e) {
            GridViewAnaliz();
        }
        private void karaListeToolStripMenuItem_Click_1(object sender, EventArgs e) {
            foreach(DataGridViewRow drow in bunifuCustomDataGrid2.SelectedRows)  //Seçili Satırları Silme
            {
                KaraListe(drow.Cells[3].Value.ToString());
            }
            MessageBox.Show("Seçili Kişiler Kara Listeye Eklendi");
        }
        private void gTKontrolüYaparkenOtoTakiptenÇıkToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void takipEtmediğimKişileriSaymaToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void toolStripMenuItem6_CheckedChanged(object sender, EventArgs e) {
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void ayrıntılarıBoşverHızlıOlmanDahaÖnemliToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
            ChechkIcon((ToolStripMenuItem) sender);
        }
        private void sadeceErkeklerToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!sadeceErkeklerToolStripMenuItem.Checked) {
                sadeceErkeklerToolStripMenuItem.Checked = true;
                toolStripMenuItem5.Checked = false;
                sadeceKadınlarToolStripMenuItem.Checked = false;
                toolStripMenuItem17.Checked = false;
            }
            else {
                sadeceErkeklerToolStripMenuItem.Checked = false;
            }
        }
        private void sadeceKadınlarToolStripMenuItem_Click(object sender, EventArgs e) {
            if(sadeceKadınlarToolStripMenuItem.Checked) {
                sadeceKadınlarToolStripMenuItem.Checked = false;
            }
            else {
                toolStripMenuItem5.Checked = false;
                sadeceKadınlarToolStripMenuItem.Checked = true;
                toolStripMenuItem17.Checked = false;
                sadeceErkeklerToolStripMenuItem.Checked = false;
            }
        }
        private void sadeceTakipEttiklerimToolStripMenuItem_Click_1(object sender, EventArgs e) {
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                if(bunifuCustomDataGrid2.Rows[i].Cells[16].Value.ToString() == "Takip ediliyor") {
                    bunifuCustomDataGrid2.Rows[i].Visible = true;
                }
                else {
                    bunifuCustomDataGrid2.Rows[i].Visible = false;
                }
            }
        }
        private void simgeDurumuToolStripMenuItem_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }
        private void takipEtmediklerimToolStripMenuItem_Click(object sender, EventArgs e) {
            for(int i = 0; i < bunifuCustomDataGrid2.Rows.Count; i++) {
                if(bunifuCustomDataGrid2.Rows[i].Cells[16].Value.ToString() == "Takip et") {
                    bunifuCustomDataGrid2.Rows[i].Visible = true;
                }
                else {
                    bunifuCustomDataGrid2.Rows[i].Visible = false;
                }
            }
        }
        private void takipEtmediklerimToolStripMenuItem1_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            Thread TakiptenCik1 = new Thread(new ThreadStart(TakiptenCik));
            TakiptenCik1.Start();
        }
        private void toolStripMenuItem10_Click(object sender, EventArgs e) {
            if(toolStripMenuItem10.Checked) {
                toolStripMenuItem10.Checked = false;
            }
            else {
                toolStripMenuItem9.Checked = true;
                toolStripMenuItem10.Checked = true;
                if(toolStripMenuItem11.Checked) {
                    toolStripMenuItem11.Checked = false;
                }
            }
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e) {
            if(toolStripMenuItem11.Checked) {
                toolStripMenuItem11.Checked = false;
            }
            else {
                toolStripMenuItem9.Checked = true;
                toolStripMenuItem11.Checked = true;
                if(toolStripMenuItem10.Checked) {
                    toolStripMenuItem10.Checked = false;
                }
            }
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e) {
            if(!toolStripMenuItem13.Checked) {
                toolStripMenuItem13.Checked = true;
                if(toolStripMenuItem14.Checked) {
                    toolStripMenuItem14.Checked = false;
                }
            }
            else {
                toolStripMenuItem13.Checked = false;
            }
        }
        private void toolStripMenuItem14_Click(object sender, EventArgs e) {
            if(!toolStripMenuItem14.Checked) {
                toolStripMenuItem14.Checked = true;
                if(toolStripMenuItem13.Checked) {
                    toolStripMenuItem13.Checked = false;
                }
            }
            else {
                toolStripMenuItem14.Checked = false;
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            Thread takipet1 = new Thread(new ThreadStart(TakipetEt));
            takipet1.Start();
        }
        private void toolStripMenuItem27_Click(object sender, EventArgs e) {
            if(this.WindowState == FormWindowState.Maximized) {
                this.WindowState = FormWindowState.Normal;
                int fazlalık = (GorunurColumnWidth() + 20) - bunifuCustomDataGrid2.Width;
                bunifuCustomDataGrid2.Columns[2].Width -= Convert.ToInt32((fazlalık * 0.15));
                bunifuCustomDataGrid2.Columns[7].Width -= Convert.ToInt32((fazlalık * 0.45));
                bunifuCustomDataGrid2.Columns[8].Width -= Convert.ToInt32((fazlalık * 0.15));
                bunifuCustomDataGrid2.Columns[15].Width -= Convert.ToInt32((fazlalık * 0.125));
                bunifuCustomDataGrid2.Columns[16].Width -= Convert.ToInt32((fazlalık * 0.125));
            }
            else {
                this.WindowState = FormWindowState.Maximized;
                int bosluk = bunifuCustomDataGrid2.Width - (GorunurColumnWidth() + 20);
                bunifuCustomDataGrid2.Columns[2].Width += Convert.ToInt32((bosluk * 0.15));
                bunifuCustomDataGrid2.Columns[7].Width += Convert.ToInt32((bosluk * 0.45));
                bunifuCustomDataGrid2.Columns[8].Width += Convert.ToInt32((bosluk * 0.15));
                bunifuCustomDataGrid2.Columns[15].Width += Convert.ToInt32((bosluk * 0.125));
                bunifuCustomDataGrid2.Columns[16].Width += Convert.ToInt32((bosluk * 0.125));
            }
        }
        private void toolStripMenuItem5_Click_1(object sender, EventArgs e) {
            if(toolStripMenuItem5.Checked) {
                toolStripMenuItem5.Checked = false;
            }
            else {
                toolStripMenuItem5.Checked = true;
                toolStripMenuItem17.Checked = false;
                sadeceKadınlarToolStripMenuItem.Checked = false;
                sadeceErkeklerToolStripMenuItem.Checked = false;
            }
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
        private void toolStripTextBox2_Click(object sender, EventArgs e) {
            if(toolStripTextBox2.Text == "Takipci sayısı giriniz.") {
                toolStripTextBox2.Text = "";
            }
        }
        private void tweetSayısıToolStripMenuItem_Click(object sender, EventArgs e) {
            CheckBoxDonustur((ToolStripMenuItem) sender);
        }
    }
}