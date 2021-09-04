using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using aejw.Network;
using System.Net.Mail;
namespace ConsoleApplication1
{

    class Program
    {
        public class Shortcut
        {

            private static Type m_type = Type.GetTypeFromProgID("WScript.Shell");
            private static object m_shell = Activator.CreateInstance(m_type);
            [ComImport, TypeLibType((short)0x1040), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
            private interface IWshShortcut
            {
                [DispId(0)]
                string FullName { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0)] get; }
                [DispId(0x3e8)]
                string Arguments { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] set; }
                [DispId(0x3e9)]
                string Description { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] set; }
                [DispId(0x3ea)]
                string Hotkey { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] set; }
                [DispId(0x3eb)]
                string IconLocation { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] set; }
                [DispId(0x3ec)]
                string RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ec)] set; }
                [DispId(0x3ed)]
                string TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] set; }
                [DispId(0x3ee)]
                int WindowStyle { [DispId(0x3ee)] get; [param: In] [DispId(0x3ee)] set; }
                [DispId(0x3ef)]
                string WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] set; }
                [TypeLibFunc((short)0x40), DispId(0x7d0)]
                void Load([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
                [DispId(0x7d1)]
                void Save();
            }
            public static void Create(string fileName, string targetPath, string arguments, string workingDirectory, string description, string hotkey, string iconPath)
            {
                IWshShortcut shortcut = (IWshShortcut)m_type.InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, m_shell, new object[] { fileName });
                shortcut.Description = description;
                shortcut.Hotkey = hotkey;
                shortcut.TargetPath = targetPath;
                shortcut.WorkingDirectory = workingDirectory;
                shortcut.Arguments = arguments;
                if (!string.IsNullOrEmpty(iconPath))
                    shortcut.IconLocation = iconPath;
                shortcut.Save();
            }
        }
       
        public static void basi(string kaynak)
        {
            

            string hedef = "C:\\Desen Arama\\";
            Directory.CreateDirectory("C:\\Desen Arama");
            TextWriter dosya2 = new StreamWriter(@"C:\Desen Arama\kapat.txt");
            Console.WriteLine("6 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            Console.WriteLine("5 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            Console.WriteLine("4 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            Console.WriteLine("3 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            Console.WriteLine("2 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            Console.WriteLine("1 Saniye bekleyiniz.");
            Thread.Sleep(1000);
            for (int i = 0; i < 5;)
            {
            try
            {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var dosyalar = new DirectoryInfo(kaynak).GetFiles("*.*");
            Console.WriteLine("C:\\Desen Arama\\     Dizinine Kopyalanıyorr...");
            foreach (FileInfo dosya in dosyalar)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                dosya.CopyTo(hedef + dosya.Name, true);
                Console.WriteLine(dosya.Name + " Kopyalandı." );

            }
            
            dosya2.Close();
            File.Delete(@"C:\Desen Arama\kapat.txt");
                    break;
                }
               
            catch (DirectoryNotFoundException)
            {
                NetworkDrive oNetDrive = new NetworkDrive(); //kullanacağımız class ımızın ismi
                try
                {

                    oNetDrive.Force = true;
                    oNetDrive.Persistent = true;/*bağlanacağımız ağ sürücüsü kalıcı mı olsun yani bilgisayarı açıp kapattığımızda tekrar bağlansın mı*/
                    oNetDrive.LocalDrive = "R"; //bağalanacağımız sürücüye vereceğimiz isim oNetDrive.PromptForCredentials = false; 
                    oNetDrive.ShareName = @"\\192.168.2.253\public"; /*bağlanacağımız bilgisayarın ip si veya adı + klasörün yolu */
                    oNetDrive.SaveCredentials = false;
                    oNetDrive.MapDrive();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("SUNUCUYA BAĞLI DEĞİLDİNİZ. OTOMATİK BAĞLANILDI.");
                    continue;
                
                }
                catch (Exception EX) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Hata mesajı; " + EX.Message + "Bu hatayı sık görüyorsanız lütfen nsa serverine R:\\\\nsa220\\public şeklinde bağlanınız. "); }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Devam Etmek İçin Bir Tuşa Basınız");
                Console.ReadKey();
                continue;

            }
            }
           



        }


    
        public static void devami()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string uygulama_dizin = "";
            uygulama_dizin = "C:\\Desen Arama\\Desen Arama.exe";// kısayolu oluşturulacak dizin
        
            string kisayol_adi = "Desen Arama"; // kısayolu oluşturulacak uygulamanın masaüzerindeki adı
            string masaustu_dizin = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + kisayol_adi + ".lnk"; // masaüstü dizini ve birleştirildi.
            Shortcut.Create(masaustu_dizin, "C:\\Desen Arama\\Desen Arama.exe", "", null, kisayol_adi, "", null); // kısayol oluşturma fonksiyonu kullanıldı.
            Console.WriteLine("Masaüstüne Kısayol Oluşturuldu." + Environment.NewLine);

            Console.WriteLine("Program Güncellendi !" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Bitirmek ve programı başlatmak için bir tuşa basınız.");
            Console.ReadKey();
            Console.WriteLine("Desen Arama.exe Çalıştırılıyor.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            
         
            string myPath = @"C:\Desen Arama\Desen Arama.exe";
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            Thread.Sleep(1000);
            prc.Start();
            Console.WriteLine("Desen Arama.exe Çalıştırıldı.");
            Thread.Sleep(2000);






        }

       public static void guncellemailgonder()
        {
            try
            {

                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: ";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject = "Desen Arama Programını GÜNCELLEDİ ! : (" + Environment.UserName.ToString() + ") ";
                mesaj.Body = "Desen Arama Programını GÜNCELLLEDİ Sürüm: V5.32 " + Environment.NewLine + istekbilgi;
                NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                client.Credentials = guvenlik;
                client.EnableSsl = true;
                client.Send(mesaj);


            }
            catch (Exception ex)
            {
                ;
            }
        }
      public static  void yuklemailgonder() {
            try
            {

                string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: ";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mesaj = new MailMessage();
                mesaj.To.Add("odevimp@gmail.com");
                mesaj.From = new MailAddress("odevimp@gmail.com");
                mesaj.Subject = "Desen Arama Programını YÜKLEDİ ! : (" + Environment.UserName.ToString() + ") ";
                mesaj.Body = "Desen Arama Programını YÜKLEDİ Sürüm: V5.32 " + Environment.NewLine + istekbilgi ;
                NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                client.Credentials = guvenlik;
                client.EnableSsl = true;
                client.Send(mesaj);


            }
            catch (Exception ex)
            {
                ;
            }


        }
        static void Main(string[] args)
        {
            for (int j = 0; j < 1; )
            {

                    Console.Clear();
              
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Programın açık olup olmadığı denetleniyor.");
                    Console.Title = "Desen Arama Update";
                    Thread.Sleep(1000);
                try
                {



                    Process[] p;
                    p = Process.GetProcessesByName("Desen Arama");
                    if (p.Length > 0)
                    {
                        try
                        {
                            foreach (Process process in p)
                            {
                                process.Kill();
                                Console.WriteLine("Program Kapatıldı.");
                            }

                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Yönetici Olmadığınız İçin program kapatılmadı hata alırsanız programı kendiniz kapatınız..");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Program zaten açık değil.");
                    }
                   

                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                
                    if (File.Exists("C:\\Desen Arama\\32bit.txt") == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("32Bit Sürümü Yükleniyor.." + Environment.NewLine);
                        Thread gonder = new Thread(new ThreadStart(guncellemailgonder));
                        gonder.Start();
                        basi("R:\\Katalog\\Desen Arama\\Desen Arama 32Bit");
                        devami();
                    }
                    else if (File.Exists("C:\\Desen Arama\\64bit.txt") == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("64Bit Sürümü Yükleniyor.." + Environment.NewLine);
                        Thread gonder = new Thread(new ThreadStart(guncellemailgonder));
                        gonder.Start();
                        basi("R:\\Katalog\\Desen Arama\\Desen Arama 64Bit");
                        devami();
                    }
                    else
                    {
                        int bit;
                        for (int i = 0; i < 5; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Programınızın hangi formatlamayı kullandığı tepit edilemedi. Lütfen:" + Environment.NewLine + "32Bit Program için (Normal şartlarda Önerilir.)'32'" + Environment.NewLine + "64Bit Program için (32Bit Çalışmaz ise) '64' " + Environment.NewLine + "Yazıp enter tuşuna basınız.");

                            bit = Convert.ToInt32(Console.ReadLine());

                            if (bit == 32)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("32Bit Sürümü Yükleniyor.." + Environment.NewLine);
                                basi("R:\\Katalog\\Desen Arama\\Desen Arama 32Bit");
                                Thread gonder = new Thread(new ThreadStart(yuklemailgonder));
                                gonder.Start();
                                devami();
                                break;
                            }
                            else if (bit == 64)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("64Bit Sürümü Yükleniyor.." + Environment.NewLine);
                                Thread gonder = new Thread(new ThreadStart(yuklemailgonder));
                                gonder.Start();
                                basi("R:\\Katalog\\Desen Arama\\Desen Arama 64Bit");
                                devami();
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Eksik veya yanlış tuşladınız." + Environment.NewLine);
                                Console.WriteLine("Yeniden denemek için bir tuşa basınız." + Environment.NewLine + " Yeniden deneme hakkınız :" + (5 - i).ToString() + Environment.NewLine);
                                Console.ReadKey();
                                continue;
                            }

                        }
                       
                    }

            break;
            }
                catch (Exception ex)
            {
                try
                {

                    string istekbilgi = Environment.NewLine + Environment.NewLine + "İstekte bulunan kişinin Kullanıcı Adı: " + Environment.UserName.ToString() + Environment.NewLine + "Bilgisayar Adı: " + Environment.MachineName.ToString() + Environment.NewLine + "Kullanıcı Domain Adı :" + Environment.UserDomainName.ToString() + Environment.NewLine + "İstekte bulunan kişinin İP Adresi: ";

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    MailMessage mesaj = new MailMessage();
                    mesaj.To.Add("odevimp@gmail.com");
                    mesaj.From = new MailAddress("odevimp@gmail.com");
                    mesaj.Subject = "Desen Arama Programını YÜKLERKEN SORUN MEYDANA GELDİ: (" + Environment.UserName.ToString() + ") ";
                    mesaj.Body = "Desen Arama Programını YÜKLERKEN SORUN MEYDANA GELDİ Sürüm: V5.32 " + Environment.NewLine + "Hata Mesajı: " + ex.Message + istekbilgi + " KAYNAK: 32bit ";
                    NetworkCredential guvenlik = new NetworkCredential("odevimp@gmail.com", "niyazi12345");
                    client.Credentials = guvenlik;
                    client.EnableSsl = true;
                    client.Send(mesaj);


                }
                catch (Exception exX)
                {
                    ;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(Environment.NewLine + "BİR SORUN MEYDANA GELDİ SORUN MESAJI :");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Environment.NewLine + "Sorun Mesajını Okuyup,Tekrar Denemek için Bir Tuşa Basınız.");
                Console.ReadKey();
                continue;
            }
        }

        }
    }
}
