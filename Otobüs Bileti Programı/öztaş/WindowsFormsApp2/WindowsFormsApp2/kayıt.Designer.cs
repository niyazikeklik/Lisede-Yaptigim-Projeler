namespace WindowsFormsApp2
{
    partial class kayıt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kayıt));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtsoyad = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnkayıtol = new System.Windows.Forms.Button();
            this.txtsifret = new System.Windows.Forms.TextBox();
            this.txtsifre = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txttc = new System.Windows.Forms.TextBox();
            this.txtad = new System.Windows.Forms.TextBox();
            this.lblsifret = new System.Windows.Forms.Label();
            this.lblsifre = new System.Windows.Forms.Label();
            this.lblemail = new System.Windows.Forms.Label();
            this.lbltel = new System.Windows.Forms.Label();
            this.lbltc = new System.Windows.Forms.Label();
            this.lblsoyad = new System.Windows.Forms.Label();
            this.lblad = new System.Windows.Forms.Label();
            this.btnev = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txttel = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 254);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(203, 43);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // txtsoyad
            // 
            this.txtsoyad.Location = new System.Drawing.Point(27, 114);
            this.txtsoyad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtsoyad.Name = "txtsoyad";
            this.txtsoyad.Size = new System.Drawing.Size(163, 22);
            this.txtsoyad.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnkayıtol
            // 
            this.btnkayıtol.BackColor = System.Drawing.Color.Transparent;
            this.btnkayıtol.Image = ((System.Drawing.Image)(resources.GetObject("btnkayıtol.Image")));
            this.btnkayıtol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkayıtol.Location = new System.Drawing.Point(285, 251);
            this.btnkayıtol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnkayıtol.Name = "btnkayıtol";
            this.btnkayıtol.Size = new System.Drawing.Size(121, 46);
            this.btnkayıtol.TabIndex = 6;
            this.btnkayıtol.Text = "Kayıt Ol";
            this.btnkayıtol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnkayıtol.UseVisualStyleBackColor = false;
            this.btnkayıtol.Click += new System.EventHandler(this.btnkayıtol_Click);
            // 
            // txtsifret
            // 
            this.txtsifret.Location = new System.Drawing.Point(12, 138);
            this.txtsifret.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtsifret.Name = "txtsifret";
            this.txtsifret.PasswordChar = '*';
            this.txtsifret.Size = new System.Drawing.Size(163, 22);
            this.txtsifret.TabIndex = 2;
            // 
            // txtsifre
            // 
            this.txtsifre.Location = new System.Drawing.Point(12, 89);
            this.txtsifre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtsifre.Name = "txtsifre";
            this.txtsifre.PasswordChar = '*';
            this.txtsifre.Size = new System.Drawing.Size(163, 22);
            this.txtsifre.TabIndex = 1;
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(12, 43);
            this.txtemail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(163, 22);
            this.txtemail.TabIndex = 0;
            // 
            // txttc
            // 
            this.txttc.Location = new System.Drawing.Point(27, 160);
            this.txttc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txttc.MaxLength = 11;
            this.txttc.Name = "txttc";
            this.txttc.Size = new System.Drawing.Size(132, 22);
            this.txttc.TabIndex = 3;
            this.txttc.TextChanged += new System.EventHandler(this.txttc_TextChanged);
            // 
            // txtad
            // 
            this.txtad.Location = new System.Drawing.Point(27, 69);
            this.txtad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtad.Name = "txtad";
            this.txtad.Size = new System.Drawing.Size(163, 22);
            this.txtad.TabIndex = 1;
            // 
            // lblsifret
            // 
            this.lblsifret.AutoSize = true;
            this.lblsifret.Location = new System.Drawing.Point(8, 119);
            this.lblsifret.Name = "lblsifret";
            this.lblsifret.Size = new System.Drawing.Size(87, 17);
            this.lblsifret.TabIndex = 18;
            this.lblsifret.Text = "Şifre Tekrar:";
            // 
            // lblsifre
            // 
            this.lblsifre.AutoSize = true;
            this.lblsifre.BackColor = System.Drawing.Color.Transparent;
            this.lblsifre.Location = new System.Drawing.Point(8, 70);
            this.lblsifre.Name = "lblsifre";
            this.lblsifre.Size = new System.Drawing.Size(41, 17);
            this.lblsifre.TabIndex = 17;
            this.lblsifre.Text = "Şifre:";
            // 
            // lblemail
            // 
            this.lblemail.AutoSize = true;
            this.lblemail.BackColor = System.Drawing.Color.Transparent;
            this.lblemail.Location = new System.Drawing.Point(8, 25);
            this.lblemail.Name = "lblemail";
            this.lblemail.Size = new System.Drawing.Size(51, 17);
            this.lblemail.TabIndex = 16;
            this.lblemail.Text = "E-Mail:";
            // 
            // lbltel
            // 
            this.lbltel.AutoSize = true;
            this.lbltel.BackColor = System.Drawing.Color.Transparent;
            this.lbltel.Location = new System.Drawing.Point(23, 191);
            this.lbltel.Name = "lbltel";
            this.lbltel.Size = new System.Drawing.Size(60, 17);
            this.lbltel.TabIndex = 15;
            this.lbltel.Text = "Telefon:";
            // 
            // lbltc
            // 
            this.lbltc.AutoSize = true;
            this.lbltc.BackColor = System.Drawing.Color.Transparent;
            this.lbltc.Location = new System.Drawing.Point(23, 142);
            this.lbltc.Name = "lbltc";
            this.lbltc.Size = new System.Drawing.Size(134, 17);
            this.lbltc.TabIndex = 13;
            this.lbltc.Text = "TC Kimlik Numarası:";
            // 
            // lblsoyad
            // 
            this.lblsoyad.AutoSize = true;
            this.lblsoyad.BackColor = System.Drawing.Color.Transparent;
            this.lblsoyad.Location = new System.Drawing.Point(23, 96);
            this.lblsoyad.Name = "lblsoyad";
            this.lblsoyad.Size = new System.Drawing.Size(52, 17);
            this.lblsoyad.TabIndex = 19;
            this.lblsoyad.Text = "Soyad:";
            // 
            // lblad
            // 
            this.lblad.AutoSize = true;
            this.lblad.BackColor = System.Drawing.Color.Transparent;
            this.lblad.Location = new System.Drawing.Point(11, 25);
            this.lblad.Name = "lblad";
            this.lblad.Size = new System.Drawing.Size(29, 17);
            this.lblad.TabIndex = 0;
            this.lblad.Text = "Ad:";
            // 
            // btnev
            // 
            this.btnev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnev.BackgroundImage")));
            this.btnev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnev.Location = new System.Drawing.Point(224, 251);
            this.btnev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnev.Name = "btnev";
            this.btnev.Size = new System.Drawing.Size(60, 46);
            this.btnev.TabIndex = 7;
            this.btnev.UseVisualStyleBackColor = true;
            this.btnev.Click += new System.EventHandler(this.btnev_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblad);
            this.groupBox1.Controls.Add(this.txttel);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(203, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kişisel Bilgiler";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtemail);
            this.groupBox2.Controls.Add(this.lblemail);
            this.groupBox2.Controls.Add(this.lblsifre);
            this.groupBox2.Controls.Add(this.txtsifret);
            this.groupBox2.Controls.Add(this.lblsifret);
            this.groupBox2.Controls.Add(this.txtsifre);
            this.groupBox2.Location = new System.Drawing.Point(224, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(183, 181);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hesap Bilgileri";
            // 
            // txttel
            // 
            this.txttel.Location = new System.Drawing.Point(15, 186);
            this.txttel.Mask = "(999) 000-0000";
            this.txttel.Name = "txttel";
            this.txttel.Size = new System.Drawing.Size(164, 22);
            this.txttel.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "11";
            // 
            // kayıt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 322);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtsoyad);
            this.Controls.Add(this.btnkayıtol);
            this.Controls.Add(this.txttc);
            this.Controls.Add(this.txtad);
            this.Controls.Add(this.lbltel);
            this.Controls.Add(this.lbltc);
            this.Controls.Add(this.lblsoyad);
            this.Controls.Add(this.btnev);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "kayıt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kayıt Ol";
            this.Load += new System.EventHandler(this.kayıt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtsoyad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnkayıtol;
        private System.Windows.Forms.TextBox txtsifret;
        private System.Windows.Forms.TextBox txtsifre;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txttc;
        private System.Windows.Forms.TextBox txtad;
        private System.Windows.Forms.Label lblsifret;
        private System.Windows.Forms.Label lblsifre;
        private System.Windows.Forms.Label lblemail;
        private System.Windows.Forms.Label lbltel;
        private System.Windows.Forms.Label lbltc;
        private System.Windows.Forms.Label lblsoyad;
        private System.Windows.Forms.Label lblad;
        private System.Windows.Forms.Button btnev;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txttel;
        private System.Windows.Forms.Label label1;
    }
}