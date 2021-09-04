using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        
        OleDbCommand cmd;
        OleDbDataAdapter da;
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                da = new OleDbDataAdapter("Select firma_adı from firmalar", con);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                dataGridView1.DataSource = tbl;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
       

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=R:\\Katalog\\desenler\\desen_veritabanı.accdb");
        OleDbCommand dm;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString() + " Firmasını silmek istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.Open();
                    dm = new OleDbCommand("Delete from firmalar where firma_adı='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", con);
                    dm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Firma Silindi");


                }
                con.Open();
                da = new OleDbDataAdapter("Select firma_adı from firmalar", con);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                dataGridView1.DataSource = tbl;
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
