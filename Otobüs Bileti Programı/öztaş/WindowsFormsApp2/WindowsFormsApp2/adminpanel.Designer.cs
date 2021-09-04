namespace WindowsFormsApp2
{
    partial class adminpanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminpanel));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tablo2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.yapımcıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yağmurOzkanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtreleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadeceKadınlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadeceErkeklerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kapatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yenidenBaşlatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oturumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kapatToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablo2BindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 127);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1040, 374);
            this.dataGridView1.TabIndex = 3;
            // 
            // tablo2BindingSource
            // 
            this.tablo2BindingSource.DataMember = "Tablo2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "TC Kimlik No Giriniz";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(188, 51);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(173, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "TC Kimlik Numarasına Göre Ara";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(187, 74);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(92, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "İsme Göre Ara";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yapımcıToolStripMenuItem,
            this.filtreleToolStripMenuItem,
            this.programToolStripMenuItem,
            this.oturumToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1040, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // yapımcıToolStripMenuItem
            // 
            this.yapımcıToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yağmurOzkanToolStripMenuItem});
            this.yapımcıToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("yapımcıToolStripMenuItem.Image")));
            this.yapımcıToolStripMenuItem.Name = "yapımcıToolStripMenuItem";
            this.yapımcıToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.yapımcıToolStripMenuItem.Text = "Yapımcı";
            // 
            // yağmurOzkanToolStripMenuItem
            // 
            this.yağmurOzkanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("yağmurOzkanToolStripMenuItem.Image")));
            this.yağmurOzkanToolStripMenuItem.Name = "yağmurOzkanToolStripMenuItem";
            this.yağmurOzkanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.yağmurOzkanToolStripMenuItem.Text = "....... ......";
            // 
            // filtreleToolStripMenuItem
            // 
            this.filtreleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sadeceKadınlarToolStripMenuItem,
            this.sadeceErkeklerToolStripMenuItem});
            this.filtreleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("filtreleToolStripMenuItem.Image")));
            this.filtreleToolStripMenuItem.Name = "filtreleToolStripMenuItem";
            this.filtreleToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.filtreleToolStripMenuItem.Text = "Filtrele";
            // 
            // sadeceKadınlarToolStripMenuItem
            // 
            this.sadeceKadınlarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sadeceKadınlarToolStripMenuItem.Image")));
            this.sadeceKadınlarToolStripMenuItem.Name = "sadeceKadınlarToolStripMenuItem";
            this.sadeceKadınlarToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sadeceKadınlarToolStripMenuItem.Text = "Sadece Kadınlar";
            this.sadeceKadınlarToolStripMenuItem.Click += new System.EventHandler(this.sadeceKadınlarToolStripMenuItem_Click);
            // 
            // sadeceErkeklerToolStripMenuItem
            // 
            this.sadeceErkeklerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sadeceErkeklerToolStripMenuItem.Image")));
            this.sadeceErkeklerToolStripMenuItem.Name = "sadeceErkeklerToolStripMenuItem";
            this.sadeceErkeklerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sadeceErkeklerToolStripMenuItem.Text = "Sadece Erkekler";
            this.sadeceErkeklerToolStripMenuItem.Click += new System.EventHandler(this.sadeceErkeklerToolStripMenuItem_Click);
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kapatToolStripMenuItem,
            this.yenidenBaşlatToolStripMenuItem});
            this.programToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("programToolStripMenuItem.Image")));
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // kapatToolStripMenuItem
            // 
            this.kapatToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("kapatToolStripMenuItem.Image")));
            this.kapatToolStripMenuItem.Name = "kapatToolStripMenuItem";
            this.kapatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.kapatToolStripMenuItem.Text = "Kapat";
            this.kapatToolStripMenuItem.Click += new System.EventHandler(this.kapatToolStripMenuItem_Click);
            // 
            // yenidenBaşlatToolStripMenuItem
            // 
            this.yenidenBaşlatToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("yenidenBaşlatToolStripMenuItem.Image")));
            this.yenidenBaşlatToolStripMenuItem.Name = "yenidenBaşlatToolStripMenuItem";
            this.yenidenBaşlatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.yenidenBaşlatToolStripMenuItem.Text = "Yeniden Başlat";
            this.yenidenBaşlatToolStripMenuItem.Click += new System.EventHandler(this.yenidenBaşlatToolStripMenuItem_Click);
            // 
            // oturumToolStripMenuItem
            // 
            this.oturumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kapatToolStripMenuItem1});
            this.oturumToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oturumToolStripMenuItem.Image")));
            this.oturumToolStripMenuItem.Name = "oturumToolStripMenuItem";
            this.oturumToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.oturumToolStripMenuItem.Text = "Oturum";
            // 
            // kapatToolStripMenuItem1
            // 
            this.kapatToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("kapatToolStripMenuItem1.Image")));
            this.kapatToolStripMenuItem1.Name = "kapatToolStripMenuItem1";
            this.kapatToolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.kapatToolStripMenuItem1.Text = "Kapat";
            this.kapatToolStripMenuItem1.Click += new System.EventHandler(this.kapatToolStripMenuItem1_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(285, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ara";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(900, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "Seçili yolculuğu sil";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // adminpanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 501);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "adminpanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Panel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.adminpanel_FormClosed);
            this.Load += new System.EventHandler(this.adminpanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablo2BindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem yapımcıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yağmurOzkanToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        
        private System.Windows.Forms.BindingSource tablo2BindingSource;
       
     
        private System.Windows.Forms.ToolStripMenuItem filtreleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sadeceKadınlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sadeceErkeklerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kapatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yenidenBaşlatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oturumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kapatToolStripMenuItem1;
    }
}