using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SMExporter
{
    static class Program
    {
        public static TextViewer ChangeLog = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ChangeLog = new TextViewer(Application.ProductName + " Version History", Adapter.GetText("VersionHistory.txt"));
            Application.Run(new MainForm());
        }

        public static bool IsNumeric(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
