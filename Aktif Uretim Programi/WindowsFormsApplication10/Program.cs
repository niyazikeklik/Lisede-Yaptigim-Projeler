using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsApplication10
{
  
    static class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
