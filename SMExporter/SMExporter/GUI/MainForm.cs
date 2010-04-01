using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace SMExporter
{
    public partial class MainForm : Form
    {
        private string mSMfilePath;
        Exporter mExportEngine;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnExport()
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Microsoft Office Access Databases (*.accdb)|*.accdb|" + "Text Documents (*.txt)|*.txt";

            if (rbSingleFile.Checked == true)
            {
                dlg.FileName = Path.GetFileNameWithoutExtension(txtSMfilePath.Text) + "-db";
            }
            else
            {
                dlg.FileName = "stratmon";
            }


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = dlg.FileName;
                TaskType taskType = TaskType.READY;

                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".accdb":
                        taskType = TaskType.EXPORT_TO_ACCESS;
                        Adapter.ExportAssembly("stratmon.accdb", dlg.FileName);
                        break;
                    case ".txt":
                        taskType = TaskType.EXPORT_TO_PLAINTEXT;
                        break;
                }

                if (!bwApp.IsBusy)
                {
                    bwApp.RunWorkerAsync(new WorkerTask(taskType));
                }

            }

        }

        private void BtnOpen()
        {
            if (rbSingleFile.Checked)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Stratagic Monitor files (*.txt)|*.txt";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSMfilePath.Text = dlg.FileName;
                    mSMfilePath = dlg.FileName;
                }
            }
            else if (rbFolder.Checked)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSMfilePath.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            BtnOpen();
        }

        private void btnExportFile_Click(object sender, EventArgs e)
        {
            BtnExport();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " v." + Application.ProductVersion;
            tmrProgress.Start();
        }

        private void bwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask wt = (WorkerTask)e.Argument;

            if (rbSingleFile.Checked == true)
            {
                bwApp.ReportProgress((int)Status.PROCESSING_FILE, Path.GetFileName(mSMfilePath));

                SMParser parser = new SMParser(mSMfilePath);
                mExportEngine = new Exporter(parser.AnalyzeFile());

                switch (wt.MyTaskType)
                {
                    case TaskType.EXPORT_TO_ACCESS:
                        mExportEngine.sExportToDatabase(txtOutputPath.Text);
                        break;
                    case TaskType.EXPORT_TO_PLAINTEXT:
                        mExportEngine.sExportToText(txtOutputPath.Text);
                        break;
                }
            }
            else if (rbFolder.Checked == true)
            {
                wt.FilesList = GetFilesToProcess(Directory.GetFiles(txtSMfilePath.Text, "*.txt"));
                BwProcessFiles(wt);
            }
        }

        private List<string> GetFilesToProcess(string[] files)
        {
            List<string> listFiles = new List<string>();
            foreach (string fp in files)
            {
                if (Path.GetExtension(fp).ToLower() == ".txt")
                {
                    listFiles.Add(fp);
                }
            }
            return listFiles;
        }

        private void BwProcessFiles(WorkerTask arg)
        {
            foreach (string filePath in arg.FilesList)
            {
                string firstLine = null;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    firstLine = sr.ReadLine();
                }

                if (Regex.Split(firstLine, " ")[0] == "Strategic" && Regex.Split(firstLine, " ")[1] == "Monitor")
                {
                    bwApp.ReportProgress((int)Status.PROCESSING_FILE, Path.GetFileName(filePath));

                    SMParser parser = new SMParser(filePath);
                    mExportEngine = new Exporter(parser.AnalyzeFile());

                    switch (arg.MyTaskType)
                    {
                        case TaskType.EXPORT_TO_ACCESS:
                            mExportEngine.sExportToDatabase(txtOutputPath.Text);
                            break;
                        case TaskType.EXPORT_TO_PLAINTEXT:
                            mExportEngine.sExportToText(txtOutputPath.Text);
                            break;
                    }
                }
            }
        }

        private void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Status i = (Status)e.ProgressPercentage;

            switch (i)
            {
                case Status.PROCESSING_FILE:
                    sbarLeft.Text = "Processing " + e.UserState.ToString();
                    break;
            }
        }

        private void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pBar.Value = pBar.Maximum;
            sbarLeft.Text = "Ready.";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright © 2007-2010 Mike Delpach" + Environment.NewLine + "MAIN ROADS Western Australia", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ChangeLog.Show();
            Program.ChangeLog.Focus();
        }

        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            btnOpen.Enabled = !bwApp.IsBusy;
            btnExportFile.Enabled = !string.IsNullOrEmpty(txtSMfilePath.Text) & !bwApp.IsBusy;

            if (bwApp.IsBusy)
            {
                pBar.Maximum = Adapter.MaxValue;

                if (Adapter.CurrentValue < pBar.Maximum)
                {
                    pBar.Style = ProgressBarStyle.Continuous;
                    pBar.Value = Adapter.CurrentValue;
                }
                else
                {
                    pBar.Style = ProgressBarStyle.Marquee;
                }
            }
            else
            {
                pBar.Style = ProgressBarStyle.Continuous;
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);

        }
    }
}
