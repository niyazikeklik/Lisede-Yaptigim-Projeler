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
    public partial class gorev : Form
    {
        public gorev()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=R:\\Katalog\\desenler\\desen_veritabanı.accdb");
        OleDbCommand dm;
        private void gorev_Load(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Bu pencerede tercih edilen seçenekler, sadece bu bilgisayar için kullanılabilir. Her bilgisayarda ihtiyaca göre butonlar farklı görevlendirilebilir.", "Bİlgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox3.Items.Clear();
                con.Open();
                dm = new OleDbCommand("Select * from firmalar", con);
                OleDbDataReader reader = dm.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["firma_adı"].ToString());
                    comboBox2.Items.Add(reader["firma_adı"].ToString());
                    comboBox3.Items.Add(reader["firma_adı"].ToString());
                    comboBox4.Items.Add(reader["firma_adı"].ToString());
                    comboBox5.Items.Add(reader["firma_adı"].ToString());
                    comboBox6.Items.Add(reader["firma_adı"].ToString());
                    comboBox7.Items.Add(reader["firma_adı"].ToString());
                    comboBox8.Items.Add(reader["firma_adı"].ToString());
                }
                con.Close();
                comboBox1.Text = Settings1.Default.b1.ToString();
                comboBox2.Text = Settings1.Default.b2.ToString();
                comboBox3.Text = Settings1.Default.b3.ToString();
                comboBox4.Text = Settings1.Default.b4.ToString();
                comboBox5.Text = Settings1.Default.b5.ToString();
                comboBox6.Text = Settings1.Default.b6.ToString();
                comboBox7.Text = Settings1.Default.b7.ToString();
                comboBox8.Text = Settings1.Default.b8.ToString();
            }
            catch (OleDbException)
            {
                MessageBox.Show("Veritabanına bağlı değilsiniz.Bağlanmadan bu özelliği kullanmazsınız. Otomatik bağlanabilmek için lütfen menü barda wifi simgesine tıklayıp nsa'ya bağlanınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex2) {

                MessageBox.Show(ex2.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings1.Default.b1 = comboBox1.Text;
            Settings1.Default.b2 = comboBox2.Text;
            Settings1.Default.b3 = comboBox3.Text;
            Settings1.Default.b4 = comboBox4.Text;
            Settings1.Default.b5 = comboBox5.Text;
            Settings1.Default.b6 = comboBox6.Text;
            Settings1.Default.b7 = comboBox7.Text;
            Settings1.Default.b8 = comboBox8.Text;
            Settings1.Default.Save();
            MessageBox.Show("Tercihleriniz kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings1.Default.b1 = "";
            Settings1.Default.b2 = "";
            Settings1.Default.b3 = "";
            Settings1.Default.b4 = "";
            Settings1.Default.b5 = "";
            Settings1.Default.b6 = "";
            Settings1.Default.b7 = "";
            Settings1.Default.b8 = "";
            Settings1.Default.Save();
            MessageBox.Show("Tercihleriniz sıfırlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;





        }
    }
}
