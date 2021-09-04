namespace Twtttter
{
    partial class giris
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(giris));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuImageButton1 = new ns1.BunifuImageButton();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.bunifuCheckbox2 = new ns1.BunifuCheckbox();
            this.bunifuCheckbox1 = new ns1.BunifuCheckbox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.modernTextBox2 = new DevLib.ModernUI.Forms.ModernTextBox();
            this.modernTextBox1 = new DevLib.ModernUI.Forms.ModernTextBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Controls.Add(this.metroLabel7);
            this.panel1.Controls.Add(this.metroLabel3);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Controls.Add(this.bunifuCheckbox2);
            this.panel1.Controls.Add(this.bunifuCheckbox1);
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.modernTextBox2);
            this.panel1.Controls.Add(this.modernTextBox1);
            this.panel1.Controls.Add(this.metroLabel8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 355);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(265, 7);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(32, 32);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.bunifuImageButton1.TabIndex = 19;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 0;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click_1);
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel7.ForeColor = System.Drawing.Color.White;
            this.metroLabel7.Location = new System.Drawing.Point(138, 327);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(159, 19);
            this.metroLabel7.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroLabel7.TabIndex = 16;
            this.metroLabel7.Text = "● niyazikeklik@gmail.com";
            this.metroLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel7.UseCustomBackColor = true;
            this.metroLabel7.UseCustomForeColor = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.ForeColor = System.Drawing.Color.White;
            this.metroLabel3.Location = new System.Drawing.Point(43, 270);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(86, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroLabel3.TabIndex = 15;
            this.metroLabel3.Text = "Şifreyi Göster";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            this.metroLabel3.Click += new System.EventHandler(this.metroLabel3_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.ForeColor = System.Drawing.Color.White;
            this.metroLabel2.Location = new System.Drawing.Point(43, 296);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(190, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroLabel2.TabIndex = 14;
            this.metroLabel2.Text = "Bilgilerimi bilgisayarıma kaydet";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel2.UseCustomBackColor = true;
            this.metroLabel2.UseCustomForeColor = true;
            // 
            // bunifuCheckbox2
            // 
            this.bunifuCheckbox2.BackColor = System.Drawing.Color.White;
            this.bunifuCheckbox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bunifuCheckbox2.ChechedOffColor = System.Drawing.Color.White;
            this.bunifuCheckbox2.Checked = false;
            this.bunifuCheckbox2.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.bunifuCheckbox2.ForeColor = System.Drawing.Color.White;
            this.bunifuCheckbox2.Location = new System.Drawing.Point(17, 270);
            this.bunifuCheckbox2.Name = "bunifuCheckbox2";
            this.bunifuCheckbox2.Size = new System.Drawing.Size(20, 20);
            this.bunifuCheckbox2.TabIndex = 13;
            this.bunifuCheckbox2.OnChange += new System.EventHandler(this.bunifuCheckbox2_OnChange_1);
            // 
            // bunifuCheckbox1
            // 
            this.bunifuCheckbox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.bunifuCheckbox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bunifuCheckbox1.ChechedOffColor = System.Drawing.Color.White;
            this.bunifuCheckbox1.Checked = true;
            this.bunifuCheckbox1.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.bunifuCheckbox1.ForeColor = System.Drawing.Color.White;
            this.bunifuCheckbox1.Location = new System.Drawing.Point(17, 296);
            this.bunifuCheckbox1.Name = "bunifuCheckbox1";
            this.bunifuCheckbox1.Size = new System.Drawing.Size(20, 20);
            this.bunifuCheckbox1.TabIndex = 12;
            // 
            // metroLabel1
            // 
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.ForeColor = System.Drawing.Color.White;
            this.metroLabel1.Location = new System.Drawing.Point(17, 143);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(280, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Twitter Giriş";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.UseCustomBackColor = true;
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(80, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // modernTextBox2
            // 
            this.modernTextBox2.ColorStyle = DevLib.ModernUI.Forms.ModernColorStyle.Blue;
            this.modernTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.modernTextBox2.FontSize = DevLib.ModernUI.Drawing.ModernFontSize.XL;
            this.modernTextBox2.FontWeight = DevLib.ModernUI.Drawing.ModernFontWeight.Light;
            this.modernTextBox2.ForeColor = System.Drawing.Color.Black;
            this.modernTextBox2.Icon = ((System.Drawing.Image)(resources.GetObject("modernTextBox2.Icon")));
            this.modernTextBox2.Lines = new string[0];
            this.modernTextBox2.Location = new System.Drawing.Point(17, 221);
            this.modernTextBox2.MaxLength = 50;
            this.modernTextBox2.Name = "modernTextBox2";
            this.modernTextBox2.PasswordChar = '●';
            this.modernTextBox2.PromptText = "Şifre";
            this.modernTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.modernTextBox2.SelectedText = "";
            this.modernTextBox2.Size = new System.Drawing.Size(280, 43);
            this.modernTextBox2.TabIndex = 9;
            this.modernTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modernTextBox2.ThemeStyle = DevLib.ModernUI.Forms.ModernThemeStyle.Light;
            this.modernTextBox2.UseCustomBackColor = true;
            this.modernTextBox2.UseCustomForeColor = true;
            this.modernTextBox2.UseSelectable = true;
            this.modernTextBox2.UseStyleColors = false;
            this.modernTextBox2.UseSystemPasswordChar = false;
            this.modernTextBox2.WordWrap = true;
            this.modernTextBox2.Click += new System.EventHandler(this.modernTextBox2_Click);
            this.modernTextBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.modernTextBox2_KeyPress_1);
            // 
            // modernTextBox1
            // 
            this.modernTextBox1.ColorStyle = DevLib.ModernUI.Forms.ModernColorStyle.Blue;
            this.modernTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.modernTextBox1.FontSize = DevLib.ModernUI.Drawing.ModernFontSize.XL;
            this.modernTextBox1.FontWeight = DevLib.ModernUI.Drawing.ModernFontWeight.Light;
            this.modernTextBox1.ForeColor = System.Drawing.Color.Black;
            this.modernTextBox1.Icon = ((System.Drawing.Image)(resources.GetObject("modernTextBox1.Icon")));
            this.modernTextBox1.Lines = new string[0];
            this.modernTextBox1.Location = new System.Drawing.Point(17, 173);
            this.modernTextBox1.MaxLength = 15;
            this.modernTextBox1.Name = "modernTextBox1";
            this.modernTextBox1.PasswordChar = '\0';
            this.modernTextBox1.PromptText = "Kullanıcı Adı";
            this.modernTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.modernTextBox1.SelectedText = "";
            this.modernTextBox1.Size = new System.Drawing.Size(280, 43);
            this.modernTextBox1.TabIndex = 8;
            this.modernTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modernTextBox1.ThemeStyle = DevLib.ModernUI.Forms.ModernThemeStyle.Light;
            this.modernTextBox1.UseCustomBackColor = true;
            this.modernTextBox1.UseCustomForeColor = true;
            this.modernTextBox1.UseSelectable = true;
            this.modernTextBox1.UseStyleColors = false;
            this.modernTextBox1.UseSystemPasswordChar = false;
            this.modernTextBox1.WordWrap = true;
            // 
            // metroLabel8
            // 
            this.metroLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel8.ForeColor = System.Drawing.Color.White;
            this.metroLabel8.Location = new System.Drawing.Point(49, 327);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(100, 19);
            this.metroLabel8.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroLabel8.TabIndex = 17;
            this.metroLabel8.Text = "● Niyazi Keklik  ";
            this.metroLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel8.UseCustomBackColor = true;
            this.metroLabel8.UseCustomForeColor = true;
            // 
            // giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(306, 355);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.giris_FormClosing);
            this.Load += new System.EventHandler(this.giris_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private ns1.BunifuCheckbox bunifuCheckbox2;
        private ns1.BunifuCheckbox bunifuCheckbox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevLib.ModernUI.Forms.ModernTextBox modernTextBox2;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private ns1.BunifuImageButton bunifuImageButton1;
        public DevLib.ModernUI.Forms.ModernTextBox modernTextBox1;
    }
}

