using System;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            anaform.driver.Navigate().GoToUrl("https://mobile.twitter.com/" + anaform.kullaniciadi + "/lists");
            anaform.SayfaLoadBekle();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
        }

        private void bunifuTextbox1_KeyPress(object sender, EventArgs e)
        {
        }

        private void modernTextBox3_Click(object sender, EventArgs e)
        {
        }

        private Anaekran anaform = (Anaekran)Application.OpenForms["Anaekran"];

        private void modernTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string listeurl = anaform.ListeUrlBulucu(modernTextBox3.Text);
                if (listeurl != "")
                {
                    anaform.driver.Navigate().GoToUrl(listeurl);
                    if (anaform.listesecenek==1) anaform.driver.Navigate().GoToUrl(listeurl.Replace("info","members"));
                    else
                    {
                    yenidendene:
                        try
                        {
                            anaform.js.ExecuteScript(anaform.Komutlar.ListeDuzenle);
                        }
                        catch (Exception)
                        {
                            goto yenidendene;
                        }

                    yenidendene1:
                        try
                        {
                            anaform.js.ExecuteScript(anaform.Komutlar.ListeUyeleriYonet);
                        }
                        catch (Exception)
                        {
                            goto yenidendene1;
                        }

                    yenidendene21:
                        try
                        {
                            anaform.js.ExecuteScript(anaform.Komutlar.ListeOnerilenler);
                        }
                        catch (Exception)
                        {
                            goto yenidendene21;
                        }
                    }
               

                    Close();
                }
                else
                {
                    MessageBox.Show("Liste Bulunamadı");
                }
            }
        }
    }
}