using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
          
          
            InitializeComponent();
        }
OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=R:\\İzinler Veritabanı\\personel.accdb");
OleDbCommand cmd;
OleDbDataAdapter da;
DataSet ds;
OleDbDataReader dr;
string adsoyad = "";
DateTime giris,hakedis;
int gecmisizin = 0,donemizin=0,kalangun=0;


        private void button1_Click(object sender, EventArgs e)
{
    if (textBox1.Text == "")
    {
        MessageBox.Show("Neyi arıyoruz ? Bakma öyle kutuyu boş geçirtmem.","Halledemiyorum",MessageBoxButtons.OK,MessageBoxIcon.Information);
    }
    cmd = new OleDbCommand();
    con.Open();
    cmd.Connection = con;
    cmd.CommandText = "SELECT * FROM kisiler where sicilno = @sicilno ";
    cmd.Parameters.AddWithValue("@sicilno",textBox1.Text);
    dr = cmd.ExecuteReader();

            while (dr.Read())
{
    label9.Text = adsoyad = dr["adsoyad"].ToString();
    giris = Convert.ToDateTime(dr["giristarihi"]);
    hakedis = Convert.ToDateTime(dr["izinhakedis"]);
    gecmisizin = Convert.ToInt32(dr["gecmisizinhak"]);
    donemizin = Convert.ToInt32(dr["budonemizinhak"]);
    kalangun = Convert.ToInt32(dr["kalangun"]);

    label10.Text=giris.ToString();
    label11.Text=hakedis.ToString();
    label13.Text =gecmisizin.ToString() + " Gün Kalmıştır.";
    label14.Text=donemizin.ToString()+" Gün Kalmıştır.";
    label17.Text = kalangun.ToString()+" Gün Kalmıştır.";

    int durum = int.Parse(dr["kalangun"].ToString());
    if (durum <=0)
    {
        label18.Text = "İzin Hakkı Kalmamıştır.";
    }
    else if (durum >= 1)
    {
        label18.Text = "İzne Çıkabilir";
        button2.Enabled = true;
        button3.Enabled = true;
        button7.Enabled = true;
    }

 
}
dr.Close();        
con.Close();

}

        private void button3_Click(object sender, EventArgs e)
        {
            if (label8.Text=="İzin Hakkı Kalmamıştır." || textBox2.Text=="")
            {
                MessageBox.Show("İzin Hakkı Kalmamış olabilir. Veya kutucuğu doldurmanız lazım.","Bir sorunla Karşılaştık",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
	{
            try
            {
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into izin (sicilino,cıkıstarihi,izinsuresi,donustarihi) values ('" + textBox1.Text + "','" + dateTimePicker1.Value + "'," + int.Parse(textBox2.Text) + ",'" + dateTimePicker2.Value + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Her şey yolunda kaydettik.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException)
            {
                MessageBox.Show("Büyük ihtimal Sicil No hiçbir personele ait değil. Ama başka bir şeyde olabilir her şeyi bilemezsin.", "'Yalnış'bir şeyler var", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            } 
	}
            
           
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            int isgunu = 0;
            string gun = "",gun2 = "";
            string tarih = "",tarih2 = "";
            int gunsayisi = Convert.ToInt32(textBox2.Text);
            DateTime baslangic = dateTimePicker1.Value;
          int i = 0;

            listBox1.Items.Clear();
            for (i=0; i <= gunsayisi; )
            {
                listBox1.Items.Add(i+" "+tarih);
                gun = baslangic.ToString("dddd");
                tarih = baslangic.ToString("dd/MMMM");
                if (gun == "Cumartesi")
                {
                    baslangic = Microsoft.VisualBasic.DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, 2, baslangic);
                }

                else if (gun == "Pazar"){
                    baslangic = Microsoft.VisualBasic.DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, baslangic);  
                }

              else if (tarih == "23.Nisan"
              || tarih == "1.Mayıs"
              || tarih == "19.Mayıs"
              || tarih == "29.Ekim"
              || tarih == "30.Ağustos"
              || tarih == "15.Temmuz"
              || tarih == "1.Ocak")
                {
                    baslangic = Microsoft.VisualBasic.DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, baslangic);
                }

                else if (tarih == "23.Nisan" 
                    || tarih == "1.Mayıs" 
                    || tarih == "19.Mayıs"
                    || tarih == "21.Mayıs"
                    || tarih == "29.Ekim" 
                    || tarih == "30.Ağustos"
                    || tarih == "15.Temmuz"
                    || tarih == "1.Ocak" && gun == "Cuma")
	            {
                    baslangic = Microsoft.VisualBasic.DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, 3, baslangic);
	            }
              
                else
                {
                    if (i == gunsayisi)
                    {
                        i++;
                    }
                    else
                    {
                        i++;
                        baslangic = Microsoft.VisualBasic.DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, baslangic);
                    }
                    
                    
                }


            }
          
            dateTimePicker2.Value = baslangic; 
            label30.Text = tarih ;
                 
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker11.Text = "";
            dateTimePicker1.Value = Convert.ToDateTime("");
        }

      

        }
            
        }
    



/////
