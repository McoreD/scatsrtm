/*
 * Created by SharpDevelop.
 * User: e80723
 * Date: 9/04/2010
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace SCATS_Monitor
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        DataGridView MainGridView = new DataGridView();
        DataGridView AlarmsGridView = new DataGridView();
        TcpClient ITSclient = new System.Net.Sockets.TcpClient();
        TextBox txtData = new TextBox();
        Ping mPing;
        PingReply mPingReply;
        bool connected = false;

        private BackgroundWorker mBwApp = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            mBwApp.DoWork += new DoWorkEventHandler(mBwApp_DoWork);
            mBwApp.ProgressChanged += new ProgressChangedEventHandler(mBwApp_ProgressChanged);
        }

        void mBwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = (int)e.ProgressPercentage;

            switch (progress)
            {
                case 0:
                    Controller ctl = e.UserState as Controller;
                    MainGridView.Rows.Add(ctl.LM, ctl.IntersectionName, ctl.ControllerType, ctl.SCATSFault, ctl.TimeOfFault, ctl.IPAddress, ctl.SCATSFault, ctl.PossibleFault);
                    break;
            }
        }

        void mBwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            Connect2SCATScm();
            string PossibleFault = "";
            Random random = new Random();
            int i = 0;
            for (i = 1; i <= 10; i++)
            {
                mPing = new Ping();
                mPingReply = mPing.Send("192.168.0." + i);
                int num = random.Next(1000);
                if (mPingReply.Status.ToString() == "Success")
                {
                    PossibleFault = "Suspected Site Power Failure";
                }
                else
                {
                    PossibleFault = "Suspected Communications Failure";
                }

                Controller ctl = new Controller()
                {
                    LM = "LM" + num,
                    IntersectionName = "",
                    ControllerType = "XM4",
                    SCATSFault = "NC,ST",
                    TimeOfFault = DateTime.Now,
                    IPAddress = "192.168.0." + i,
                    CommsTimeChk = DateTime.Now,
                    PossibleFault = PossibleFault
                };

                mBwApp.ReportProgress(0, ctl);
            }
        }

        private void Connect2SCATScm()
        {
            //Connect to SCATS CM 
            byte[] sendbytes = null;

            //Connect to Central Manager
            try
            {
                ITSclient.Connect("172.16.120.129", 2012);
                // ITSclient.Connect("192.168.30.200", 2012);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (ITSclient.Connected)
            {
                this.Text = "SCATS Intersection Monitor - Connected to SCATS Central Manager";

                //Create license key
                NetworkStream networkStream = ITSclient.GetStream();
                if (networkStream.CanWrite & networkStream.CanRead)
                {
                    connected = true;
                    string ITSUser = "Ripple";
                    string ITSLicense = "_WKfR8agHEXBtsprxOd+RnpdUY_9cRSS";
                    int length = (ITSUser + "/" + ITSLicense).Length + 1;
                    //Dim sendbytes As [Byte]() = Encoding.ASCII.GetBytes(0 & length & 100 & ITSUser & "/" & ITSLicense)
                    sendbytes = Encoding.ASCII.GetBytes(100 + ITSUser + "\\" + ITSLicense);

                    //Setup initial bytes
                    sendbytes[0] = 0;
                    sendbytes[1] = (byte)length;
                    sendbytes[2] = 100;

                    // Send license info to central manager
                    networkStream.Write(sendbytes, 0, sendbytes.Length);

                    //TextBox1.Text += "Sent ITS License to Central Manager: " & ITSUser & "\" & ITSLicense & NewLine & NewLine

                    byte[] recieveBuff = new byte[256];
                    networkStream.Read(recieveBuff, 0, 2);
                    networkStream.Read(recieveBuff, 0, recieveBuff[1]);

                    if (recieveBuff[1] == 0)
                    {
                        this.Text = "Connection successful";
                    }
                    else
                    {
                        this.Text = "Failed to Connect error code: " + recieveBuff[1] + Environment.NewLine + Environment.NewLine;
                    }
                }

            }
            else
            {
                this.Text = "SCATS Intersection Monitor - Failed to SCATS Central Manager";
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "SCATS Intersection Monitor";
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            this.WindowState = FormWindowState.Maximized;
            this.Controls.Add(MainGridView);
            this.Controls.Add(AlarmsGridView);
            this.Controls.Add(txtData);

            MainGridView.Columns.Add("LM", "LM");
            MainGridView.Columns.Add("IntersectionName", "IntersectionName");
            MainGridView.Columns.Add("ControllerType", "Controller Type");
            MainGridView.Columns.Add("SCATSFault", "SCATS Fault");
            MainGridView.Columns.Add("TimeOfFault", "TimeOfFault");
            MainGridView.Columns.Add("IPAddress", "IPAddress");
            MainGridView.Columns.Add("CommsTimeChk", "Comms Time Check");
            MainGridView.Columns.Add("PossibleFault", "Possible Fault");

            MainGridView.Height = (this.Height - 300);
            MainGridView.Width = (this.Width - 40);
            MainGridView.Location = new Point(20, 40);
            MainGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MainGridView.BackgroundColor = Color.White;
            MainGridView.GridColor = Color.White;
            MainGridView.RowTemplate.Height = 18;
            MainGridView.RowHeadersVisible = false;

            AlarmsGridView.Columns.Add("VMSName", "VMS Name");
            AlarmsGridView.Columns.Add("Timestamp", "Timestamp");
            AlarmsGridView.Columns.Add("AlarmType", "Alarm Type");
            AlarmsGridView.Columns.Add("AlarmDescription", "Alarm Description");

            AlarmsGridView.Height = (this.Height - MainGridView.Height - 100);
            AlarmsGridView.Width = (this.Width - 10);
            AlarmsGridView.Location = new Point(0, MainGridView.Height + 50);
            AlarmsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AlarmsGridView.BackgroundColor = Color.White;
            AlarmsGridView.GridColor = Color.White;
            AlarmsGridView.RowHeadersVisible = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            mBwApp.RunWorkerAsync();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            MainGridView.Height = (this.Height - 300);
            MainGridView.Width = (this.Width - 40);
        }
    }
}
