using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
namespace WindowsFormsApplication2
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        Form1 f1 = new Form1();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text== "neseplastik")
            {
                if (textBox2.Text=="nese1")
                {
                    f1.button38.Enabled = true;
                    f1.backgroundWorker3.RunWorkerAsync();
                    f1.button9.Enabled = true;
                    f1.checkBox7.Enabled = true;
                    f1.checkBox8.Enabled = true;
                    f1.button4.Enabled = true;
                    f1.button2.Visible = false;
                    f1.button5.Visible = true;
                    f1.button9.Enabled = true;
                    f1.textBox2.Enabled = true;
                    f1.label10.Enabled = true;
                    f1.button14.Enabled=true;
                    f1.comboBox3.Enabled = true;
                    f1.button15.Enabled = true;
                    f1.button16.Enabled = true;
                    f1.button17.Enabled =true;
                  
                    f1.checkBox3.Enabled = true;
                    f1.checkBox2.Enabled = true;
                    f1.veritabanıToolStripMenuItem.Enabled = true;
                    f1.Show();
                   
                   
                    this.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            f1.Show();
            this.Close();
            
        }

        private void giris_Load(object sender, EventArgs e)
        {
            Random sayi = new Random();
            sayi2 = sayi.Next(11, 99);
            sayi3 = sayi.Next(11, 99);
            label4.Text = sayi2.ToString() + "x" + sayi3.ToString() + "= ?";
            textBox2.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text=="neseplastik")
            {
                textBox2.Focus();
                textBox1.BackColor = Color.SeaGreen;
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                textBox1.BackColor = Color.IndianRed;
                textBox1.ForeColor = Color.White;

            }

            if (textBox1.Text.Length==0)
            {
                  textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
            if (textBox2.Text=="nese1")
            {
                if (textBox1.Text != "neseplastik")
                {
                    MessageBox.Show("Kullanıcı adınız yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.BackColor = Color.IndianRed;
                    textBox1.ForeColor = Color.White;
                    textBox1.Focus();
                }
                else
                {
                    textBox2.BackColor = Color.SeaGreen;
                    textBox2.ForeColor = Color.Black;
                   f1.backgroundWorker3.RunWorkerAsync();
                   f1.button9.Enabled = true;
                    f1.button38.Enabled = true;
                    f1.button4.Enabled = true;
                    f1.button2.Visible = false;
                    f1.button5.Visible = true;
                    f1.button9.Enabled = true;
                    f1.textBox2.Enabled = true;
                    f1.label10.Enabled = true;
                    f1.button11.Enabled = true;
                    f1.button14.Enabled = true;
                    f1.comboBox3.Enabled = true;
                    f1.button15.Enabled = true;
                    f1.button16.Enabled = true;
                    f1.checkBox7.Enabled = true;
                    f1.checkBox8.Enabled = true;
                    f1.checkBox3.Enabled = true;
                    f1.checkBox2.Enabled = true;
                    f1.veritabanıToolStripMenuItem.Enabled = true;
                    f1.Show();
                    f1.button17.Enabled = true; 
                    this.Close();
                }
                
            }
            else
            {
                textBox2.BackColor = Color.IndianRed;
                textBox2.ForeColor = Color.White;
            }
        }
       
        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (textBox2.PasswordChar == '\0')
            {
                 textBox2.PasswordChar = '*';
            }
            else if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
            }

            
        }
        int i = 0;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                    mesaj.Body = "Desen arama programı giriş için Kullanıcı Adı : ''neseplastik'', Şifresi : ''nese1'' ' dir. Niyazi Keklik "+Environment.NewLine+istekbilgi;
                    NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                    client.Credentials = guvenlik;
                    client.EnableSsl = true;
                    client.Send(mesaj);
                    MessageBox.Show("Mail Başarıyla ''saffetm@neseplastik.com'' Adresine ve ''huseyin@neseplastik.com'' Adresine Gönderildi.", "Mail Gönderme");
                    i++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mesajınız gönderelimedi. Hata Mesajı : " + ex.Message,"Hata",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
	}
            else
            {
                MessageBox.Show("Kısa bir süre içinde ikinci kez mail gönderemezsiniz.","Bot Engel",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

            
            
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {

        }

        private void giris_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
        int sayi2 = 0;
        int sayi3 = 0;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text==(sayi2*sayi3).ToString())
            {
                linkLabel1.Enabled = true;

            }
            else
            {
                linkLabel1.Enabled = false;
            }
        }
    }
}
