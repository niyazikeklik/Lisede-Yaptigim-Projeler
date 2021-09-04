using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        int say;
        public void dene()
        {
            DirectoryInfo di = new DirectoryInfo(@"R:\Katalog\desenler");
            FileInfo[] files = di.GetFiles("*.jpg");
            progressBar1.Maximum = files.Length;

            foreach (FileInfo fi in files)
            {
                progressBar1.Value++;
                try
                {
                    Image img = Image.FromFile(fi.FullName);
                    if (radioButton2.Checked)
                    {
                        if (img.Width <= Convert.ToInt32(textBox2.Text) || img.Height <= Convert.ToInt32(textBox3.Text))
                        {
                            say++;
                            textBox1.Text += fi.Name + Environment.NewLine;
                            if (checkBox1.Checked)
                            {
                                System.Diagnostics.Process.Start(fi.FullName);
                            }
                        }
                    }
                    else
                    {
                        if (img.Width <= Convert.ToInt32(textBox2.Text) && img.Height <= Convert.ToInt32(textBox3.Text))
                        {
                            say++;
                            textBox1.Text += fi.Name + Environment.NewLine;
                            if (checkBox1.Checked)
                            {
                                System.Diagnostics.Process.Start(fi.FullName);
                            }
                      
                            
                        }
                    }


                }
                catch (Exception)
                {

                    ;
                }
            }
            progressBar1.Value = 0;
            MessageBox.Show("Toplam: " + say.ToString());

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text=="" ||textBox3.Text=="")
            {
                MessageBox.Show("Boşlukları doldurunuz.", "Null değer girilemez.", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            else
            {
                Thread basla = new Thread(new ThreadStart(dene));
                basla.Start();
            }
         
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "VE";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "VEYA";
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          BackColor = System.Drawing.ColorTranslator.FromHtml("#FF382424");
        }
    }
}
