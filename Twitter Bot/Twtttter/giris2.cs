using System;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class giris2 : Form
    {
        public giris2()
        {
            InitializeComponent();
        }

        private void bunifuCheckbox3_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox3.Checked == true)
            {
                MessageBox.Show("Bu seçeneği seçmeniz durumunda kullanıcıların: beğeni sayısı, günlük beğeni, günlük tweet sayısı, takipçi, takip edilen sayıları, konum ve üyelik süreleri gibi bilgiler gösterilmeyecektir. Avantaj olarak hız konusunda olumlu fark yaratacaktır. sadece sizi en çok beğenen hesapları listelemek istiyorsanız bu seçeneği işaretlemeniz tavsiye edilir.", "Performans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                anaekrann.performans = true;
            }
            else
            {
                anaekrann.performans = false;
            }
        }
    
        private giris giris1 = new giris();

        private void bunifuCheckbox5_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox5.Checked == true)
            {
                anaekrann.takipci_filtresi = true;
            }
            else
            {
                anaekrann.takipci_filtresi = false;
            }
        }

        private void bunifuCheckbox6_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox6.Checked == true)
            {
                anaekrann.veritabanındancek = true;
            }
            else
            {
                anaekrann.veritabanındancek = false;
            }
        }

        private void modernTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                try
                {
                    anaekrann.kontrol_edilecek_tweet_sayisi = Convert.ToInt32(modernTextBox3.Text);
                    if (anaekrann.kontrol_edilecek_tweet_sayisi >= 0)
                    {
                        Thread begeniler = new Thread(new ThreadStart(anaekrann.BegenileriKontrol));
                        begeniler.Start();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen pozitif bir tam sayı giriniz.", "Geçersiz Veri", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen bir tam sayı giriniz.", "Geçersiz Veri", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void modernTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                ;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ;
            }
            else
            {
                if (!(e.KeyCode == Keys.D1 || e.KeyCode == Keys.D7 || e.KeyCode == Keys.D6 || e.KeyCode == Keys.D5 || e.KeyCode == Keys.D4 || e.KeyCode == Keys.D3 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D8 || e.KeyCode == Keys.D9 || e.KeyCode == Keys.D0))
                {
                    MessageBox.Show("Girdiğiniz karakter rakam değildi.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    modernTextBox3.Text = "";
                }
            }
        }
        private Anaekran anaekrann = (Anaekran)Application.OpenForms["Anaekran"];

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            OleDbDataAdapter da = new OleDbDataAdapter("Select * from begenenler", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + anaekrann.kullaniciadi  + @"\kayitlarim.accdb;Persist Security Info=True");
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            if (tbl.Rows.Count == 0)
            {
                MessageBox.Show("Eski kayıt bulunamadı.");
            }
            else
            {
                anaekrann.kayittangetir = true;
                Thread begenictrl = new Thread(new ThreadStart(anaekrann.BegenileriKontrolEtVeritabanı));
                begenictrl.Start();
             
              
            
                this.Hide();
            }
        }

        private void giris2_Load(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }
    }
}
