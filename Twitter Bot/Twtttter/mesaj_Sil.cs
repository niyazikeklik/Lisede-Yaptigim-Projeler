using System;
using System.Windows.Forms;

namespace Twtttter {
    public partial class mesaj_Sil : Form {
        public mesaj_Sil() {
            InitializeComponent();
        }

        private Anaekran anaform = (Anaekran) Application.OpenForms["Anaekran"];

        private void pictureBox1_Click(object sender, EventArgs e) {
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e) {
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e) {
            anaform.basilanbuton = 1;
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e) {
            anaform.basilanbuton = 2;
            this.Hide();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e) {
            anaform.KomutCalistir("document.getElementsByClassName('css-1dbjc4n r-97wbjc')[0].children[0].click();");
            if(bunifuFlatButton3.Text == "Takip ediliyor")
                anaform.KomutCalistir("document.querySelectorAll('[data-testid=confirmationSheetConfirm]')[0].click();");
            bunifuFlatButton3.Text = anaform.ReturnKomutCalistir("return document.getElementsByClassName('css-1dbjc4n r-97wbjc')[0].innerText;").ToString();
        }

        private void mesaj_Sil_Load(object sender, EventArgs e) {
            bunifuFlatButton3.Text = anaform.ReturnKomutCalistir("return document.getElementsByClassName('css-1dbjc4n r-97wbjc')[0].innerText;").ToString();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e) {
            anaform.basilanbuton = -1;
            this.Close();
        }
    }
}