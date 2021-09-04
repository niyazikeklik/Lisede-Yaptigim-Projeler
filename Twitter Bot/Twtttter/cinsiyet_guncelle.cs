using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Twtttter
{
    public partial class cinsiyet_guncelle : Form
    {
        public cinsiyet_guncelle()
        {
            InitializeComponent();
        }

        public string isim = "";
        public int satirindex;
        private string okunan = "";
        public string profil;

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
        }

        private Anaekran anaform = (Anaekran)Application.OpenForms["Anaekran"];

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton2_MouseHover(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton2_ChangeUICues(object sender, UICuesEventArgs e)
        {
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            TextReader tReader = new StreamReader("isimler.txt");
            okunan = tReader.ReadToEnd();
            tReader.Close();
            StringBuilder sb = new StringBuilder(okunan);
            int indeks = okunan.IndexOf("'" + isim + "'");
            if (indeks == -1)
            {
                StreamWriter SW = File.AppendText("isimler.txt");
                SW.WriteLine("('" + isim + "', 'K')");

                SW.Close();
            }
            else
            {
                sb[indeks + isim.Length + 5] = 'K';
                okunan = sb.ToString();
                TextWriter tWriter = new StreamWriter("isimler.txt");
                tWriter.Write(okunan);
                tWriter.Flush();
                tWriter.Close();
            }
            anaform.VeritabaniGuncelle(profil, "begenenler", "cinsiyet", "Kadın");
            anaform.VeritabaniGuncelle(profil, "takipciler", "cinsiyet", "Kadın");

            anaform.bunifuCustomDataGrid2.Rows[satirindex].Cells[6].Value = "Kadın";
            this.Close();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            TextReader tReader = new StreamReader("isimler.txt");
            okunan = tReader.ReadToEnd();
            tReader.Close();
            StringBuilder sb = new StringBuilder(okunan);
            int indeks = okunan.IndexOf("'" + isim + "'");
            if (indeks == -1)
            {
                StreamWriter SW = File.AppendText("isimler.txt");
                SW.WriteLine("('" + isim + "', 'E')");
                SW.Close();
            }
            else
            {
                sb[indeks + isim.Length + 5] = 'E';
                okunan = sb.ToString();
                TextWriter tWriter = new StreamWriter("isimler.txt");
                tWriter.Write(okunan);
                tWriter.Flush();
                tWriter.Close();
            }
            anaform.VeritabaniGuncelle(profil, "begenenler", "cinsiyet", "Erkek");
            anaform.VeritabaniGuncelle(profil, "takipciler", "cinsiyet", "Erkek");
            anaform.bunifuCustomDataGrid2.Rows[satirindex].Cells[6].Value = "Erkek";
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}