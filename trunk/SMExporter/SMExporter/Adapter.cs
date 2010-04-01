using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SMExporter
{
    public static class Adapter
    {
        public static int MaxValue { get; set; }
        public static int CurrentValue { get; set; }

        public static void ExportAssembly(string name, string outputPath)
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream(myAssembly.GetName().Name + "." + name);
            //MessageBox.Show(folder + "\" + name)
            byte[] byteExeFile = new byte[myStream.Length + 1];
            myStream.Read(byteExeFile, 0, (int)myStream.Length);
            FileStream myTempFile = new FileStream(outputPath, FileMode.Create);
            myTempFile.Write(byteExeFile, 0, (int)myStream.Length);
            myTempFile.Close();
        }

        public static string GetText(string name)
        {
            string text = "";
            try
            {
                System.Reflection.Assembly oAsm = System.Reflection.Assembly.GetExecutingAssembly();

                string fn = "";
                foreach (string n in oAsm.GetManifestResourceNames())
                {
                    if (n.Contains(name))
                    {
                        fn = n;
                        break;
                    }
                }
                Stream oStrm = oAsm.GetManifestResourceStream(fn);
                StreamReader oRdr = new StreamReader(oStrm);
                text = oRdr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting text from resource", ex);
            }

            return text;
        }
    }
}
