using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using System.Threading;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
       
        OpenFileDialog excelac = new OpenFileDialog(); 
        FolderBrowserDialog save = new FolderBrowserDialog();
        char degiscek; string konumcuk = ""; int say = 0;
        public static string InnerTrim(string input)
        {
            return input.Trim().Replace(" ", string.Empty);
        } 
        void fordongusu()
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = dataGridView1.Rows.Count + 5;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {


                try
                {
                    pictureBox1.Image = Image.FromFile("R:\\Katalog\\desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                    pictureBox1.Image.Save(save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                    say++;
                }
                catch (FileNotFoundException ex)
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile("R:\\Katalog\\çıkan desenler\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                        pictureBox1.Image.Save(save.SelectedPath + "\\" + textBox3.Text + "\\" + dataGridView1.Rows[i].Cells[0].Value.ToString() + ".jpg");
                        say++;
                    }
                    catch (Exception)
                    {
                        listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + " Bulunamadı Hatası,Yazım Hatası olabilir.");
                        textBox4.Text += dataGridView1.Rows[i].Cells[0].Value.ToString()+Environment.NewLine;
                       
                        continue;
                    }

                  
                }
                catch (ArgumentException ex)
                {
                    listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() +"    |    "+" Geçersiz Karakter Hatası,Yazım Hatası");
                   
                    textBox4.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;
                    continue;

                }
                catch (Exception ex) {
                    listBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "    |    " + ex.Message);
                    textBox4.Text += dataGridView1.Rows[i].Cells[0].Value.ToString() + Environment.NewLine;
                  
                
                }
                progressBar1.Value++;
            } 
            ziple();
        }
        void ziple() {
            pictureBox1.Image = Image.FromFile("zip.png");
            ZipFile zip = new ZipFile();
            label1.Text = "Zip dosyası oluşturuluyor.";
            zip.AddDirectory(save.SelectedPath + "\\" + textBox3.Text);
            if (textBox3.Text!="")
            {
                zip.Save(save.SelectedPath + "\\" +textBox3.Text+"\\"+ textBox3.Text + ".zip");
            }
           
            progressBar1.Value = progressBar1.Maximum;
            label1.Text = "İşlem Tamammlandı.";
            MessageBox.Show("Toplam: " + say + " dosya klasörlendi. İşlem Tamammlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pictureBox1.Image = Image.FromFile("A7VhUP0CUAEwR_y.png");
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            say = 0; listBox1.Items.Clear();
            textBox4.Text = "";
            listBox1.Items.Add("Ürün"+"    |    "+"Hata Raporu");
listBox1.Items.Add("--------------------------------------------------------------------------------------------------------");
            if (textBox3.Text=="")
            {
                MessageBox.Show("Zip dosyası ismi girmediniz.", "Dikkat", MessageBoxButtons.OK,MessageBoxIcon.Stop); 
            }
            else
            {
                label1.Text = "Kaydedilecek konum bekleniyor..";
                save.Description = "Kaydedilecek konumu seçiniz.";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(save.SelectedPath + "\\" + textBox3.Text);
                    label1.Text = "Resimler işleniyor, çekiliyor..";
                    Thread baslat = new Thread(new ThreadStart(fordongusu));
                    baslat.Start();

                }
            }  
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                degiscek = Convert.ToChar(textBox1.Text);

            }
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = InnerTrim(dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(degiscek,'-'));
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(textBox2.Text, "");
                }
            }
          
            
        }
        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                excelac.Filter = "Excel Dosyası|*.xls*| Tüm Desenler|*.*";
                excelac.FilterIndex = 1;
                if (excelac.ShowDialog() == DialogResult.OK)
                {
                    konumcuk = excelac.FileName;
                    OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + konumcuk + "; Extended Properties='Excel 12.0 Xml;HDR=YES'"); //excel_dosya.xlsx kısmını kendi excel dosyanızın adıyla değiştirin.
                    DataTable tablo = new DataTable();
                    xlsxbaglanti.Open();
                    tablo.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", xlsxbaglanti);
                    da.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                    xlsxbaglanti.Close();
                    button1.Enabled = true;
                }
            }
            catch (OleDbException)
            {

                MessageBox.Show("Seçilen veritabanının sayfa isminin Sayfa1 şeklinde olduğuna ve verilerin 1. Sütunda Olduğuna ve ilk satırda Ürün Kodları(vb.) gibi bir başlık olduğuna emin olunuz. Exceli kapatıp tekrar deneyiniz.", "Excel Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("A7VhUP0CUAEwR_y.png");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Application.Exit();
          
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < listBox1.Items.Count; i++)
            {
              Clipboard.SetText( listBox1.Items[i].ToString().Substring(0,6)); 
            }
        }


    }
}

