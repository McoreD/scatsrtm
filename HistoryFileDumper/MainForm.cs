/*
 * Created by SharpDevelop.
 * User: e80655
 * Date: 2010-03-05
 * Time: 1:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace HistoryFileDumper
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            // Read  location \\dactst30\Program Files (x86)\SCATS6\Region\SCATSData\History
            //  			  \\dactst30\SCATSData\History
            // Write location \\tocuat20\SCATSData
        }

        void BtnSyncClick(object sender, EventArgs e)
        {
            btnSync.Text = btnSync.Text == "&Start" ? "&Stop" : "&Start";
            tmrApp.Enabled = !tmrApp.Enabled;
        }

        void CopyFiles()
        {
            string srcDir = txtSourceDir.Text;
            string destDir = txtDestDir.Text;
            if (Directory.Exists(srcDir) && Directory.Exists(destDir))
            {
                string[] files = Directory.GetFiles(srcDir, "*.hist", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    string destFileName = Path.GetFileName(file);
                    string destFilePath = Path.Combine(destDir, destFileName);
                    File.Copy(file, destFilePath, true);
                }
            }
        }

        void TmrAppTick(object sender, EventArgs e)
        {
            if (!bwApp.IsBusy)
            {
                bwApp.RunWorkerAsync();
            }
        }

        void BwAppDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CopyFiles();
        }


        void nudFreqValueChanged(object sender, EventArgs e)
        {
            tmrApp.Interval = 1000 * (int)nudFreq.Value;
        }
    }
}
