﻿namespace WindowsFormsApp2
{
    partial class kullanıcıgiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kullanıcıgiris));
            this.btnkullanıcıgiriş = new System.Windows.Forms.Button();
            this.txtsifre = new System.Windows.Forms.TextBox();
            this.txtkullanıcı = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnev1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnkullanıcıgiriş
            // 
            this.btnkullanıcıgiriş.BackColor = System.Drawing.Color.Transparent;
            this.btnkullanıcıgiriş.Image = ((System.Drawing.Image)(resources.GetObject("btnkullanıcıgiriş.Image")));
            this.btnkullanıcıgiriş.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkullanıcıgiriş.Location = new System.Drawing.Point(65, 208);
            this.btnkullanıcıgiriş.Margin = new System.Windows.Forms.Padding(2);
            this.btnkullanıcıgiriş.Name = "btnkullanıcıgiriş";
            this.btnkullanıcıgiriş.Size = new System.Drawing.Size(78, 41);
            this.btnkullanıcıgiriş.TabIndex = 2;
            this.btnkullanıcıgiriş.Text = "Giriş";
            this.btnkullanıcıgiriş.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnkullanıcıgiriş.UseVisualStyleBackColor = false;
            this.btnkullanıcıgiriş.Click += new System.EventHandler(this.btnkullanıcıgiriş_Click);
            // 
            // txtsifre
            // 
            this.txtsifre.Location = new System.Drawing.Point(17, 184);
            this.txtsifre.Margin = new System.Windows.Forms.Padding(2);
            this.txtsifre.Multiline = true;
            this.txtsifre.Name = "txtsifre";
            this.txtsifre.PasswordChar = '*';
            this.txtsifre.Size = new System.Drawing.Size(126, 20);
            this.txtsifre.TabIndex = 1;
            // 
            // txtkullanıcı
            // 
            this.txtkullanıcı.Location = new System.Drawing.Point(17, 145);
            this.txtkullanıcı.Margin = new System.Windows.Forms.Padding(2);
            this.txtkullanıcı.Multiline = true;
            this.txtkullanıcı.Name = "txtkullanıcı";
            this.txtkullanıcı.Size = new System.Drawing.Size(126, 20);
            this.txtkullanıcı.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 167);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Şifre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "TC Kimlik Numarası Giriniz";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(17, -1);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 118);
            this.label2.TabIndex = 7;
            // 
            // btnev1
            // 
            this.btnev1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnev1.Image = ((System.Drawing.Image)(resources.GetObject("btnev1.Image")));
            this.btnev1.Location = new System.Drawing.Point(17, 208);
            this.btnev1.Margin = new System.Windows.Forms.Padding(2);
            this.btnev1.Name = "btnev1";
            this.btnev1.Size = new System.Drawing.Size(44, 41);
            this.btnev1.TabIndex = 3;
            this.btnev1.UseVisualStyleBackColor = true;
            this.btnev1.Click += new System.EventHandler(this.btnev1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(161, 130);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // kullanıcıgiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(302, 264);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnkullanıcıgiriş);
            this.Controls.Add(this.txtsifre);
            this.Controls.Add(this.txtkullanıcı);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnev1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "kullanıcıgiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcı Giriş";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnkullanıcıgiriş;
        private System.Windows.Forms.TextBox txtsifre;
        private System.Windows.Forms.TextBox txtkullanıcı;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnev1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}