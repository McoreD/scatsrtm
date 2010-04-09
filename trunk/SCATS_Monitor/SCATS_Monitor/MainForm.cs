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

namespace SCATS_Monitor
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private System.Net.Sockets.TcpClient ITSclient = new System.Net.Sockets.TcpClient();
				
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
				/*
			public void MyThread2()
	{
		Connect2SCATScm();
		string PossibleFault = "";
		Random random = new Random();
		int i = 0;
		for (i = 1; i <= 10; i++) {
			Ping = new Ping();
			pReply = Ping.Send("192.168.0." + i);
			int num = random.Next(1000);
			if (pReply.Status.ToString() == "Success") {
				PossibleFault = "Suspected Site Power Failure";
			} else {
				PossibleFault = "Suspected Communications Failure";
			}
			MainGridView.Rows.Add("LM" + num, "", "XM4", "NC,ST", DateAndTime.TimeOfDay.Second, "192.168.0." + i, DateAndTime.TimeOfDay.Second, PossibleFault);
		}
	}


		*/
		private void Connect2SCATScm()
	{
		//Connect to SCATS CM 
		byte[] sendbytes = null;

		//Connect to Central Manager
		try {
		ITSclient.Connect("192.168.30.200", 2012);
		} catch {
		}

		if (ITSclient.Connected) {
			this.Text = "SCATS Intersection Monitor - Connected to SCATS Central Manager";
			//& NewLine

			//Create license key
			NetworkStream networkStream = ITSclient.GetStream();
			if (networkStream.CanWrite & networkStream.CanRead) {
				connected = true;
				string ITSUser = "Ripple";
				string ITSLicense = "_WKfR8agHEXBtsprxOd+RnpdUY_9cRSS";
				int length = (ITSUser + "/" + ITSLicense).Length + 1;
				//Dim sendbytes As [Byte]() = Encoding.ASCII.GetBytes(0 & length & 100 & ITSUser & "/" & ITSLicense)
				sendbytes = Encoding.ASCII.GetBytes(100 + ITSUser + "\\" + ITSLicense);

				//Setup initial bytes
				sendbytes[0] = 0;
				sendbytes[1] = length;
				sendbytes[2] = 100;

				// Send license info to central manager
				networkStream.Write(sendbytes, 0, sendbytes.Length);

				//TextBox1.Text += "Sent ITS License to Central Manager: " & ITSUser & "\" & ITSLicense & NewLine & NewLine

				byte[] recieveBuff = new byte[256];
				networkStream.Read(recieveBuff, 0, 2);
				networkStream.Read(recieveBuff, 0, recieveBuff[1]);

				if (recieveBuff[1] == 0) {
					this.Text = "Connection successful" + Environment.NewLine + Environment.NewLine;
				} else {
					this.Text = "Failed to Connect error code: " + recieveBuff[1] + Environment.NewLine + Environment.NewLine;
				}
			}

		} else {
			this.Text = "SCATS Intersection Monitor - Failed to SCATS Central Manager";
		}

	}
	}
}
