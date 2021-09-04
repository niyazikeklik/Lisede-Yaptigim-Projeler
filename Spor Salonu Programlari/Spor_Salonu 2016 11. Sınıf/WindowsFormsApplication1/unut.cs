using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
namespace WindowsFormsApplication1
{
    public partial class unut : Form
    {
        public unut()
        {
            InitializeComponent();
        }
        public bool Gonder(string isim,string alan)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("odevimp@gmail.com");
            
            ePosta.To.Add(alan);
            
            
            
            ePosta.Subject = "Sayın "+isim+" Program Giriş Şifreniz";
            
            ePosta.Body = "Sayın "+isim+" Program Kullanıcı Adınız = Admin , Parolanız = 12345 ' dir. ";
            
            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new System.Net.NetworkCredential("odevimp@gmail.com", "******");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
            }
            return kontrol;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Gonder(textBox2.Text, textBox1.Text);
            MessageBox.Show("Kullanıcı Adı Ve Şifre Mailinize Gödnerilmiştir Lütfen Kontrol Ediniz.", "Mail Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void unut_Load(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
    }
}
