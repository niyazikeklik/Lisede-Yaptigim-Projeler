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
    public partial class ulas : Form
    {
        public ulas()
        {
            InitializeComponent();
        }
        public bool Gonder()
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("odevimp@gmail.com");
            ePosta.To.Add("odevimp@gmail.com");
            ePosta.Subject = textBox3.Text;
            ePosta.Body = textBox4.Text + "\n" + "\n" + "\n" + "\n" + " Kullanıcı E-posta Adresi : " + textBox2.Text + "\n" + " Kullanıcı Adı Soyadı : " + textBox1.Text;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("odevimp@gmail.com", "*****");
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
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("Lütfen Bütün Alanları Doldurunuz.", "Eksik ALan Var", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Gonder();
                MessageBox.Show("Mailiniz alınmıştır. Bize geri bildirimde bulunduğunuz için teşekkür ederiz.", "Teşekkür Ederiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
          
            
            yoneticipanel anasayfa = new yoneticipanel();
            this.Close();
            anasayfa.Show();
            
        }

        private void ulas_Load(object sender, EventArgs e)
        {
        
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hakkımızda ulas = new hakkımızda();
            this.Close();
            ulas.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://instagram.com/niyazikeklk"); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/inteycirexs"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WhatsApp'dan ulaşmak için : +905346861675","WhatsApp",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://plus.google.com/+NiyaziKeklik/posts"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/profile.php?id=100003032160606"); 
        }
    }
}
