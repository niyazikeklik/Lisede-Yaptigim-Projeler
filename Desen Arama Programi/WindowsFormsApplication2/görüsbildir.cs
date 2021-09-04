using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Threading;
namespace WindowsFormsApplication2
{
    public partial class görüsbildir : Form
    {
        public görüsbildir()
        {
            InitializeComponent();
        }
        void gonder() {

            progressBar1.Visible = true;
            try
            {
                label5.Text = "Mesajınız gönderiliyor...";
                Form1 yeni = new Form1();
                string ipAdresi = Dns.GetHostByName(Environment.MachineName.ToString()).AddressList[0].ToString();
                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: " + ipAdresi + Environment.NewLine + "Sürüm: " +yeni.desenV14ToolStripMenuItem.Text;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.To.Add("niyazikeklikk@gmail.com");
                mesaj.To.Add("niyazikeklik@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject = textBox1.Text + " Tarafından Bir Mesajın Var.";
                mesaj.Body = textBox3.Text + " " + istekbilgi + " Mail Adresi: " + textBox2.Text+Environment.NewLine+yildizsayii;
                label5.Text = "Resimler yükleniyor lütfen bekleyiniz...";
                mesaj.Attachments.Add(new Attachment(mresim1));
                mesaj.Attachments.Add(new Attachment(mresim2));
                mesaj.Attachments.Add(new Attachment(mresim3));
                mesaj.Attachments.Add(new Attachment(mresim4));
                NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                client.Credentials = guvenlik;
                client.EnableSsl = true;
                client.Send(mesaj);
                MessageBox.Show("Mail başarıyla yapımcıya iletildi. En kısa sürede mesajınız için gerekenler yapılıcaktır. Geri bildirimde bulunduğunuz için teşekkürler.", "Başarılı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label5.Text = "";
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Mail gönderilemedi bir sorun oluştu lütfen tekrar deneyiniz." + Environment.NewLine + Environment.NewLine + "Hata Mesajı: " + ex.Message, "Başarısız.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Visible = false;
        
        }
        string yildizsayii = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked||radioButton2.Checked||radioButton3.Checked||radioButton4.Checked||radioButton5.Checked)
            {
                Thread baslatt = new Thread(new ThreadStart(gonder));
                baslatt.Start();
            }
            else
            {
                MessageBox.Show("Lütfen programa 5 üzerinden bir puan vererek değerlendiriniz. :)"+Environment.NewLine+" Mesaj gönderilmedi tekrar deneyiniz.", "Tekrar Deneyiniz.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tüm kutucukalr temizlenecek emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            { 
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; 
            }
            else
            {
            ;
	        }
	
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                pictureBox6.Visible = true;
            }
            else
            {
                pictureBox6.Visible = false;
            }
            if (textBox3.TextLength==textBox3.MaxLength)
            {
                MessageBox.Show("Karakter sınırıına ulaştınız. Yazmaya devam etmek için lütfen karakter sınırını arttırınız"); 
            }
            else
            {
                label4.Text = textBox3.TextLength.ToString() + "/" + textBox3.MaxLength.ToString();
            }
         
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.MaxLength += 200;
            label4.Text = textBox3.TextLength.ToString() + "/" + textBox3.MaxLength.ToString();
        }

        private void görüsbildir_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            textBox1.Focus();
        }
        string mresim1 = Application.StartupPath + "\\JD-10-128.png", mresim2 = Application.StartupPath + "\\JD-10-128.png", mresim3 = Application.StartupPath + "\\JD-10-128.png", mresim4 = Application.StartupPath + "\\JD-10-128.png";
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Tüm Dosyalar |*.*";

                file.RestoreDirectory = true;  
                DialogResult result = file.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(file.FileName);
                    mresim1 = file.FileName.ToString();

                }
            }
            catch (Exception mailhatasi)
            {
                MessageBox.Show("Resim eklenirken bir sorun oluştu. Hata Mesajı: "+mailhatasi,"Resim Seçilemedi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Tüm Dosyalar |*.*";
                file.RestoreDirectory = true;  
                DialogResult result = file.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pictureBox3.Image = Image.FromFile(file.FileName);
                    mresim2 = file.FileName.ToString();

                }
            }
            catch (Exception mailhatasi)
            {
                MessageBox.Show("Resim eklenirken bir sorun oluştu. Hata Mesajı: " + mailhatasi, "Resim Seçilemedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Tüm Dosyalar |*.*";
                file.RestoreDirectory = true;  
                DialogResult result = file.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pictureBox4.Image = Image.FromFile(file.FileName);
                    mresim3 = file.FileName.ToString();

                }
            }
            catch (Exception mailhatasi)
            {
                MessageBox.Show("Resim eklenirken bir sorun oluştu. Hata Mesajı: " + mailhatasi, "Resim Seçilemedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Tüm Dosyalar |*.*";
                file.RestoreDirectory = true;  
                DialogResult result = file.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pictureBox5.Image = Image.FromFile(file.FileName);
                    mresim4 = file.FileName.ToString();

                }
            }
            catch (Exception mailhatasi)
            {
                MessageBox.Show("Resim eklenirken bir sorun oluştu. Hata Mesajı: " + mailhatasi, "Resim Seçilemedi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Image.FromFile(Application.StartupPath+"\\JD-10-128.png");
            mresim1 = Application.StartupPath + "\\JD-10-128.png";

        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\JD-10-128.png");
            mresim2 = Application.StartupPath + "\\JD-10-128.png";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\JD-10-128.png");
            mresim3 = Application.StartupPath + "\\JD-10-128.png";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\JD-10-128.png");
            mresim4 = Application.StartupPath + "\\JD-10-128.png";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength>0)
            {
                pictureBox8.Visible = true;
            }
            else
            {
                pictureBox8.Visible = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                pictureBox7.Visible = true;
            }
            else
            {
                pictureBox7.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            yildizsayii = "Verdiği yıldız sayısı: 1 * ";
            pictureBox1.Image = Image.FromFile(Application.StartupPath+@"\1.gif");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            yildizsayii = "Verdiği yıldız sayısı: 2 ** ";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\2.gif");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            yildizsayii = "Verdiği yıldız sayısı: 3 *** ";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\3.gif");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            yildizsayii = "Verdiği yıldız sayısı: 4 **** ";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\4.gif");
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            yildizsayii = "Verdiği yıldız sayısı: 5 ***** ";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\5.gif");
        }
    }
}
