namespace Twtttter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.modernLabel1 = new DevLib.ModernUI.Forms.ModernLabel();
            this.modernTextBox3 = new DevLib.ModernUI.Forms.ModernTextBox();
            this.SuspendLayout();
            // 
            // modernLabel1
            // 
            this.modernLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.modernLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.modernLabel1.Location = new System.Drawing.Point(0, 0);
            this.modernLabel1.Name = "modernLabel1";
            this.modernLabel1.Size = new System.Drawing.Size(288, 30);
            this.modernLabel1.TabIndex = 4;
            this.modernLabel1.Text = "Hangi Listeye Eklenecek";
            this.modernLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.modernLabel1.UseCompatibleTextRendering = true;
            this.modernLabel1.UseCustomBackColor = true;
            this.modernLabel1.UseCustomForeColor = true;
            this.modernLabel1.UseStyleColors = false;
            // 
            // modernTextBox3
            // 
            this.modernTextBox3.ColorStyle = DevLib.ModernUI.Forms.ModernColorStyle.Blue;
            this.modernTextBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.modernTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.modernTextBox3.FontSize = DevLib.ModernUI.Drawing.ModernFontSize.Large;
            this.modernTextBox3.FontWeight = DevLib.ModernUI.Drawing.ModernFontWeight.Light;
            this.modernTextBox3.ForeColor = System.Drawing.Color.White;
            this.modernTextBox3.Icon = ((System.Drawing.Image)(resources.GetObject("modernTextBox3.Icon")));
            this.modernTextBox3.Lines = new string[0];
            this.modernTextBox3.Location = new System.Drawing.Point(0, 30);
            this.modernTextBox3.MaxLength = 2147483647;
            this.modernTextBox3.Name = "modernTextBox3";
            this.modernTextBox3.PasswordChar = '\0';
            this.modernTextBox3.PromptText = "Liste Adı Giriniz";
            this.modernTextBox3.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.modernTextBox3.SelectedText = "";
            this.modernTextBox3.Size = new System.Drawing.Size(288, 43);
            this.modernTextBox3.TabIndex = 26;
            this.modernTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modernTextBox3.ThemeStyle = DevLib.ModernUI.Forms.ModernThemeStyle.Dark;
            this.modernTextBox3.UseCustomBackColor = true;
            this.modernTextBox3.UseCustomForeColor = true;
            this.modernTextBox3.UseSelectable = true;
            this.modernTextBox3.UseStyleColors = false;
            this.modernTextBox3.UseSystemPasswordChar = false;
            this.modernTextBox3.WordWrap = true;
            this.modernTextBox3.Click += new System.EventHandler(this.modernTextBox3_Click);
            this.modernTextBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.modernTextBox3_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(288, 76);
            this.Controls.Add(this.modernTextBox3);
            this.Controls.Add(this.modernLabel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevLib.ModernUI.Forms.ModernLabel modernLabel1;
        public DevLib.ModernUI.Forms.ModernTextBox modernTextBox3;
    }
}