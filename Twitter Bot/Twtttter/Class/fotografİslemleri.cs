using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twtttter;

namespace Tweety.Class
{
   static class fotografİslemleri
    {
        public static string guncellink;
        public static bool fotograf_hatali;
        static Anaekran anaekran = new Anaekran();
        
        public static Bitmap ResimOlustur(string url, string kullaniciadi, Color renk)
        {
            Bitmap HazirResim;
            if (url.Contains("Kilitli")) HazirResim = ResimCiz(Resim(url.Replace("Kilitli", ""), kullaniciadi), kullaniciadi);
            else HazirResim = Resim(url.Replace("Kilitli", ""), kullaniciadi);
            return ClipToCircle(HazirResim, new PointF(HazirResim.Width / 2, HazirResim.Height / 2), HazirResim.Width / 2, renk);
        }

        private static Bitmap ClipToCircle(Image srcImage, PointF center, float radius, Color backGround)
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            try
            {
                using (Graphics g = Graphics.FromImage(dstImage))
                {
                    RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                                                             radius * 2, radius * 2);
                    // enables smoothing of the edge of the circle (less pixelated)
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    // fills background color
                    using (Brush br = new SolidBrush(backGround))
                    {
                        g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                    }
                    // adds the new ellipse & draws the image again
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(r);
                    g.SetClip(path);
                    g.DrawImage(srcImage, 0, 0);
                    return dstImage;
                }
            }
            catch (Exception)
            {
                return dstImage;
            }
        }
        public static Bitmap ScreenShot(IWebDriver driver)
        {
            Screenshot myScreenShot = ((ITakesScreenshot)driver).GetScreenshot();
            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));
            //  Bitmap elemScreenshot = screen.Clone(new Rectangle(elem.Location, elem.Size), screen.PixelFormat);
            //    screen.Dispose();
            return screen;
        }

        private static Bitmap Resim(string Url, string kullanici_id)
        {
            guncellink = "";
            fotograf_hatali = false;
            try
            {
                WebRequest rs = WebRequest.Create(Url.Replace("Kilitli", ""));
                return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
            }
            catch (Exception)
            {
                /* try
                 {
                     WebRequest rs = WebRequest.Create("https://unavatar.now.sh/twitter/" + kullanici_id.Replace("@", ""));
                     return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
                 }
                 catch (Exception)
                 {*/
                fotograf_hatali = true;
                guncellink = anaekran.FotografUrl(kullanici_id);
                if (guncellink == "-1") return (Bitmap)Image.FromFile(@"img\fotograf_errorx96.jpg");
                anaekran.VeritabaniGuncelle(kullanici_id, "begenenler", "fotografyolu", guncellink);
                anaekran.VeritabaniGuncelle(kullanici_id, "takipciler", "fotografyolu", guncellink);
                try
                {
                    WebRequest rs = WebRequest.Create(guncellink.Replace("Kilitli", ""));
                    return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
                }
                catch (Exception)
                {
                    return (Bitmap)Image.FromFile(@"img\fotograf_errorx96.jpg");
                }
                //  } 
            }
        }
        private static Bitmap ResimCiz(Bitmap Resim, string kullanici_id)
        {
            try
            {
                Graphics g = Graphics.FromImage(Resim);
                // g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage((Image)Image.FromFile(@"img\lock.jpg"), new System.Drawing.Point() { X = -10, Y = -10 });
                return Resim;
            }
            catch (Exception)
            {
                WebRequest rs = WebRequest.Create("https://abs.twimg.com/sticky/default_profile_images/default_profile_x96.png");
                return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
            }
        }
    }
}
