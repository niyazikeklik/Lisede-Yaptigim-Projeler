using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double fv1, fv2, fo1, fv3, fo2, ff, fsonuc;
        double kv1, kv2, kv3, kf, ke, ksonuc;
        double mv1, mv2, mv3, mo1, mf, msonuc;
        double genelort;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fv1 = Convert.ToDouble(textBox1.Text);
                fv2 = Convert.ToDouble(textBox2.Text);
                fo1 = Convert.ToDouble(textBox3.Text);
                fv3 = Convert.ToDouble(textBox4.Text);
                fo2 = Convert.ToDouble(textBox5.Text);
                ff = Convert.ToDouble(textBox6.Text);
                fsonuc = (fv1 * 0.15) + (fv2 * 0.25) + (fo1 * 0.10) + (fv3 * 0.15) + (fo2 * 0.10) + (ff * 0.25);

                if (fsonuc >= 60)
                {
                    label24.ForeColor = Color.Green;
                    label24.Text = "Fizik dersi notunuz: " + fsonuc.ToString() + " Genel ortalamaya verdiği puan: " + (fsonuc * 0.35).ToString();
                }
                else
                {
                    label24.ForeColor = Color.Red;
                    label24.Text = "Fizik dersi notunuz: " + fsonuc.ToString() + " Genel ortalamaya verdiği puan: " + (fsonuc * 0.35).ToString();
                }
                kv1 = Convert.ToDouble(textBox12.Text);
                kv2 = Convert.ToDouble(textBox11.Text);
                kv3 = Convert.ToDouble(textBox10.Text);
                kf = Convert.ToDouble(textBox9.Text);
                ke = Convert.ToDouble(textBox8.Text);
                ksonuc = (kv1 * 0.20) + (kv2 * 0.20) + (kv3 * 0.20) + (kf * 0.40) + (ke * 0.15);

                if (ksonuc >= 60)
                {
                    label25.ForeColor = Color.Green;
                    label25.Text = "Kimya dersi notunuz: " + ksonuc.ToString() + " Genel ortalamaya verdiği puan: " + (ksonuc * 0.20).ToString();
                }
                else
                {
                    label25.ForeColor = Color.Red;
                    label25.Text = "Kimya dersi notunuz: " + ksonuc.ToString() + " Genel ortalamaya verdiği puan: " + (ksonuc * 0.20).ToString();
                }
                mv1 = Convert.ToDouble(textBox7.Text);
                mv2 = Convert.ToDouble(textBox13.Text);
                mv3 = Convert.ToDouble(textBox14.Text);
                mo1 = Convert.ToDouble(textBox15.Text);
                mf = Convert.ToDouble(textBox16.Text);
                msonuc = (mv1 * 0.15) + (mv2 * 0.15) + (mv3 * 0.15) + (mf * 0.40) + (mo1 * 0.15);
                if (msonuc >= 60)
                {
                    label23.ForeColor = Color.Green;
                    label23.Text = "Matematik dersi notunuz: " + msonuc.ToString() + " Genel ortalamaya verdiği puan: " + (msonuc * 0.45).ToString();
                }
                else
                {
                    label23.ForeColor = Color.Red;
                    label23.Text = "Matematik dersi notunuz: " + msonuc.ToString() + " Genel ortalamaya verdiği puan: " + (msonuc * 0.45).ToString();
                }
                genelort = (fsonuc * 0.35) + (msonuc * 0.45) + (ksonuc * 0.20);
                if (genelort >= 60)
                {
                    label27.ForeColor = Color.Green;
                    label27.Text = "Ortalamanız: " + genelort.ToString() + " olarak sınıfı geçiyorsunuz.";
                }
                else
                {
                    label27.ForeColor = Color.Red;
                    label27.Text = "Ortalamanız: " + genelort.ToString() + " olarak sınıf tekrarı";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hiçbir kutucuğu boş bırakmayınız. not gimek istemiyorsanız 0 yazınız.", "Boş kutucuk var", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text="";


        }
    }
}
