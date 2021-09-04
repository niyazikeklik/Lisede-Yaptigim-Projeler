using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;
using wordeaktar = Microsoft.Office.Interop.Word;

namespace otomatik_imzala
{

    
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }
        private string IlkHarfleriBuyut(string metin)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(metin);
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");

        public string titleModu(string text)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int sayac = 0;
                  
            


            wordeaktar.Application wordapp = new wordeaktar.Application();
            wordapp.Visible = true;
            wordeaktar.Document worddoc;
            object wordobj = System.Reflection.Missing.Value;
            worddoc = wordapp.Documents.Add(ref wordobj);
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (i % 3 ==0)
                {
                    wordapp.Selection.InsertNewPage();

                }
                wordapp.Selection.TypeText("Saygılarımla," + Environment.NewLine);
                wordapp.Selection.TypeText(Environment.NewLine+titleModu(dataGridView1.Rows[i].Cells[0].Value.ToString())+Environment.NewLine);
                wordapp.Selection.TypeText(titleModu(dataGridView1.Rows[i].Cells[4].Value.ToString().Replace(".",". " ))+ Environment.NewLine);
                wordapp.Selection.TypeText(""+Environment.NewLine);
                wordapp.Selection.InlineShapes.AddPictureBullet(Application.StartupPath + "\\neselogo.png", Type.Missing);
                wordapp.Selection.TypeText(Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("TOSB TAYSAD 5. Cadde No:3" + Environment.NewLine);
                wordapp.Selection.TypeText("Çayırova - Kocaeli" + Environment.NewLine);
                wordapp.Selection.TypeText("41423 - TÜRKİYE" + Environment.NewLine);
                wordapp.Selection.TypeText("Mail: " + dataGridView1.Rows[i].Cells[1].Value.ToString() + "@neseplastik.com" + Environment.NewLine);
                wordapp.Selection.TypeText("Tel: +90 262 658-1090"+ Environment.NewLine);
                wordapp.Selection.TypeText("Cep: "+dataGridView1.Rows[i].Cells[3].Value.ToString() + Environment.NewLine);
                wordapp.Selection.TypeText("www.neseplastik.com");
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                wordapp.Selection.TypeText("" + Environment.NewLine);
                sayac++;
              
                
            }
            wordapp = null;
            MessageBox.Show(sayac.ToString() + " Kişi");
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
         

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.calisanlarr' table. You can move, or remove it, as needed.
            this.calisanlarrTableAdapter.Fill(this.database1DataSet.calisanlarr);

        }
    }
    public static class ExtensionManager
    {
        public static string ToTitleCase(this string Text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text);
        }
    }
}
