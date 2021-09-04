using System;
using System.Windows.Forms;

namespace Twtttter
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new giris());
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
        }
     //   private Anaekran anaekrann = (Anaekran)Application.OpenForms["Anaekran"];
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            MessageBox.Show("kapandı");
        }
    }
}