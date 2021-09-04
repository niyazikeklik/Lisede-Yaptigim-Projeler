
using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tweety.Properties;
namespace Twtttter {
    public partial class giris : Form {
        public giris() {
            InitializeComponent();
        }
        private void bunifuCheckbox2_OnChange_1(object sender, EventArgs e) {
            if(bunifuCheckbox2.Checked == true) {
                modernTextBox2.PasswordChar = '\0'; ;
            }
            else if(bunifuCheckbox2.Checked == false) {
                modernTextBox2.PasswordChar = '●';
            }
        }
        giris2 giris2;
        int donen = -2;
        bool mailizin = false;
        void Giris() {
         
            donen = anaekrann.TwitterAc(yedek_kullaniciadi);
            if(donen == -1) {
                pictureBox1.Image = Image.FromFile(@"img/icon.png");
                giris2.Hide(); 
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                modernTextBox2.Enabled = true;
                metroLabel1.Text = "Hatalı kullanıcı adı veya şifre";
                MessageBox.Show("Girilen kullanıcı adı veya şifre yanlış!", "Hatalı Girdi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if(donen == 0) {
                giris2.Hide(); pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Image.FromFile(@"img/icon.png");
                modernTextBox2.Enabled = true;
                yedek_kullaniciadi = modernTextBox1.Text;
                mailizin = true;
                modernTextBox1.MaxLength += 15;
                Settings.Default.kullaniciadi = modernTextBox1.Text;
                Settings.Default.sifre = modernTextBox2.Text;
                Settings.Default.Save();
                metroLabel1.Text = "Lütfen e-posta veya telefon numarası giriniz.";
                MessageBox.Show("Girilen kullanıcı adı kullanılarak daha önceden çok defa giriş yapıldığından dolayı twitter tarafından kabul edilmiyor. Lütfen e-posta adresinizi veya kayıtlı telefon numaranızı girererk devam ediniz.", "Hatalı Girdi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if(donen == 1) {

                giris2.Show();
                this.Hide();
                anaekrann.baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + anaekrann.kullaniciadi + @"\kayitlarim.accdb;Persist Security Info=True");
                anaekrann.baglanti.Open();
                anaekrann.TwitterAyarla();
                Settings.Default.oturumuaciktut = bunifuCheckbox1.Checked;
                Settings.Default.kullaniciadi = anaekrann.kullaniciadi;
                Settings.Default.sifre = anaekrann.sifre;
                
                Settings.Default.Save();
            }
            else if(donen == 2) {
                metroLabel1.Text = "Onay kodunu giriniz.";
                metroLabel2.Visible = false;
                bunifuCheckbox1.Visible = false;
                modernTextBox1.Visible = false;
                modernTextBox2.Text = "";
                modernTextBox2.Location = new Point(modernTextBox2.Location.X, modernTextBox2.Location.Y - 25);
                modernTextBox2.PromptText = "Onay Kodu";
                modernTextBox2.Enabled = true;
            }
            else if(donen == -2) {
                MessageBox.Show("Yanlış onay kodu");
            }
        }
        string yedek_kullaniciadi = "";
        private void modernTextBox2_KeyPress_1(object sender, KeyPressEventArgs e) {
            try {
                if(e.KeyChar == 13) {
                    if(modernTextBox2.PromptText == "Onay Kodu") {
                        Thread twitterac = new Thread(delegate () {
                            anaekrann.onaykodu = modernTextBox2.Text;
                            Giris();
                        });
                        twitterac.Start();
                    }
                    else {

                        giris2 = new giris2();
                        giris2.Show();
                        giris2.Hide(); pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = Image.FromFile(@"img/loading.gif");
                        metroLabel1.Text = "Oturum Açılıyor.. Lütfen bekleyiniz.";
                        modernTextBox2.Enabled = false;
                        if(modernTextBox2.Text != "" && modernTextBox1.Text != "") {
                            if((modernTextBox1.Text.Length < 15 && modernTextBox1.Text.Length > 4) || mailizin) {
                                if(!Directory.Exists(modernTextBox1.Text) && !mailizin) {
                                    Directory.CreateDirectory(modernTextBox1.Text);
                                    File.Copy(@"sablon\kayitlarim.accdb", modernTextBox1.Text + @"\kayitlarim.accdb");
                                }
                                Thread twitterac = new Thread(delegate () {
                                    anaekrann.kullaniciadi = modernTextBox1.Text;
                                    anaekrann.sifre = modernTextBox2.Text;
                                    Giris();
                                });
                                twitterac.Start();
                            }
                            else MessageBox.Show("Geçersiz Kullanıcı Adı Giriyorsunuz.", "Uyumsuz", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        }
                        else {
                            modernTextBox2.Enabled = true;
                            MessageBox.Show("Şifre ve kullanıcı adı boş geçilemez.", "Şifre Giriniz!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private yukleniyor yeni = new yukleniyor();
        private Anaekran anaekrann = new Anaekran();
        private void giris_Load(object sender, EventArgs e) {
           // System.Diagnostics.Process.Start(@"Yardımcı\Gender-and-Age-Prediction-from-Face-Images-master\src\calistir.bat");
            anaekrann.Show();
            anaekrann.Hide();
            yeni.anaform = anaekrann;
            if(File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe") ||
                File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe")) {
                try {
                    /*  yeni.Show();
                      anaekrann.DriverBaslat();
                      yeni.Hide();*/
                    Thread driverbaslat = new Thread(new ThreadStart(anaekrann.DriverBaslat));
                    driverbaslat.Start();
                    if(bunifuCheckbox2.Checked == true) {
                        modernTextBox2.PasswordChar = '\0'; ;
                    }
                    else if(bunifuCheckbox2.Checked == false) {
                        modernTextBox2.PasswordChar = '●';
                    }
                    if(Settings.Default.oturumuaciktut == true) {
                        bunifuCheckbox1.Checked = true;
                        modernTextBox1.Text = Settings.Default.kullaniciadi;
                        modernTextBox2.Text = Settings.Default.sifre;
                        modernTextBox2.Focus();
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                MessageBox.Show("Lütfen Google Chrome YÜKLEYİN!");
                Close();
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e) {
                Application.Exit();
        }
        private void panel1_Paint(object sender, PaintEventArgs e) {
        }
        private void bunifuImageButton1_Click_1(object sender, EventArgs e) {
                Application.Exit();
        }

        private void metroLabel3_Click(object sender, EventArgs e) {

        }

        private void modernTextBox2_Click(object sender, EventArgs e) {

        }

        private void giris_FormClosing(object sender, FormClosingEventArgs e) {
            anaekrann.programkapat();
        }
    }
}