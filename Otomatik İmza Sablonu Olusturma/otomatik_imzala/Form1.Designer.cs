namespace otomatik_imzala
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
            this.components = new System.ComponentModel.Container();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.kullanıcıDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mailAdresileriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ceptelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.görevDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calisanlarrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database1DataSet = new otomatik_imzala.Database1DataSet();
            this.button1 = new System.Windows.Forms.Button();
            this.calisanlarrTableAdapter = new otomatik_imzala.Database1DataSetTableAdapters.calisanlarrTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calisanlarrBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Users\\stajyer\\Documents\\Visual " +
                "Studio 2010\\Projects\\otomatik_imzala\\otomatik_imzala\\bin\\Debug\\Database1.accdb\"";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kullanıcıDataGridViewTextBoxColumn,
            this.mailAdresileriDataGridViewTextBoxColumn,
            this.telDataGridViewTextBoxColumn,
            this.ceptelDataGridViewTextBoxColumn,
            this.görevDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.calisanlarrBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(8, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(545, 318);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // kullanıcıDataGridViewTextBoxColumn
            // 
            this.kullanıcıDataGridViewTextBoxColumn.DataPropertyName = "Kullanıcı";
            this.kullanıcıDataGridViewTextBoxColumn.HeaderText = "Kullanıcı";
            this.kullanıcıDataGridViewTextBoxColumn.Name = "kullanıcıDataGridViewTextBoxColumn";
            // 
            // mailAdresileriDataGridViewTextBoxColumn
            // 
            this.mailAdresileriDataGridViewTextBoxColumn.DataPropertyName = "Mail_Adresileri";
            this.mailAdresileriDataGridViewTextBoxColumn.HeaderText = "Mail_Adresileri";
            this.mailAdresileriDataGridViewTextBoxColumn.Name = "mailAdresileriDataGridViewTextBoxColumn";
            // 
            // telDataGridViewTextBoxColumn
            // 
            this.telDataGridViewTextBoxColumn.DataPropertyName = "tel";
            this.telDataGridViewTextBoxColumn.HeaderText = "tel";
            this.telDataGridViewTextBoxColumn.Name = "telDataGridViewTextBoxColumn";
            // 
            // ceptelDataGridViewTextBoxColumn
            // 
            this.ceptelDataGridViewTextBoxColumn.DataPropertyName = "ceptel";
            this.ceptelDataGridViewTextBoxColumn.HeaderText = "ceptel";
            this.ceptelDataGridViewTextBoxColumn.Name = "ceptelDataGridViewTextBoxColumn";
            // 
            // görevDataGridViewTextBoxColumn
            // 
            this.görevDataGridViewTextBoxColumn.DataPropertyName = "görev";
            this.görevDataGridViewTextBoxColumn.HeaderText = "görev";
            this.görevDataGridViewTextBoxColumn.Name = "görevDataGridViewTextBoxColumn";
            // 
            // calisanlarrBindingSource
            // 
            this.calisanlarrBindingSource.DataMember = "calisanlarr";
            this.calisanlarrBindingSource.DataSource = this.database1DataSet;
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(545, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "İMZA ŞABLONLARINI WORDE AKTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // calisanlarrTableAdapter
            // 
            this.calisanlarrTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 376);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calisanlarrBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.OleDb.OleDbConnection oleDbConnection1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource calisanlarrBindingSource;
        private Database1DataSetTableAdapters.calisanlarrTableAdapter calisanlarrTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn kullanıcıDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mailAdresileriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ceptelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn görevDataGridViewTextBoxColumn;
    }
}

