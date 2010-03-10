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
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			// Read  location \\dactst30\Program Files (x86)\SCATS6\Region\SCATSData\History
			//  \\dactst30\SCATSData\History
			// Write location \\tocuat20\SCATSData
		}
				
		void BtnSyncClick(object sender, EventArgs e)
		{
			btnSync.Text = btnSync.Text == "&Start" ? "&Stop" : "&Start";
			
		}
		
		void CopyFiles()
		{
			string src = txtSourceDir.Text; 
			string dest = txtDestDir.Text;
			if (Directory.Exists(src) && Directory.Exists(dest)) 
			{
				string[] files = Directory.GetFiles(src, "*.hist", SearchOption.AllDirectories); 
				
			}
		}
	}
}
