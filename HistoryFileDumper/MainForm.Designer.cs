/*
 * Created by SharpDevelop.
 * User: e80655
 * Date: 2010-03-05
 * Time: 1:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace HistoryFileDumper
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnSync = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestDir = new System.Windows.Forms.TextBox();
            this.nudFreq = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrApp = new System.Windows.Forms.Timer(this.components);
            this.bwApp = new System.ComponentModel.BackgroundWorker();
            this.txtSourceDir = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(400, 128);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "&Start";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.BtnSyncClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDestDir
            // 
            this.txtDestDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::HistoryFileDumper.Properties.Settings.Default, "DestDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDestDir.Location = new System.Drawing.Point(112, 48);
            this.txtDestDir.Name = "txtDestDir";
            this.txtDestDir.Size = new System.Drawing.Size(363, 20);
            this.txtDestDir.TabIndex = 0;
            this.txtDestDir.Text = global::HistoryFileDumper.Properties.Settings.Default.DestDir;
            // 
            // nudFreq
            // 
            this.nudFreq.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::HistoryFileDumper.Properties.Settings.Default, "Freq", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudFreq.Location = new System.Drawing.Point(112, 80);
            this.nudFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFreq.Name = "nudFreq";
            this.nudFreq.Size = new System.Drawing.Size(120, 20);
            this.nudFreq.TabIndex = 3;
            this.nudFreq.Value = global::HistoryFileDumper.Properties.Settings.Default.Freq;
            this.nudFreq.ValueChanged += new System.EventHandler(this.nudFreqValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Frequency (s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrApp
            // 
            this.tmrApp.Interval = 3000;
            this.tmrApp.Tick += new System.EventHandler(this.TmrAppTick);
            // 
            // bwApp
            // 
            this.bwApp.WorkerReportsProgress = true;
            this.bwApp.WorkerSupportsCancellation = true;
            this.bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwAppDoWork);
            // 
            // txtSourceDir
            // 
            this.txtSourceDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::HistoryFileDumper.Properties.Settings.Default, "SourceDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSourceDir.Location = new System.Drawing.Point(112, 16);
            this.txtSourceDir.Name = "txtSourceDir";
            this.txtSourceDir.Size = new System.Drawing.Size(363, 20);
            this.txtSourceDir.TabIndex = 0;
            this.txtSourceDir.Text = global::HistoryFileDumper.Properties.Settings.Default.SourceDir;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 166);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestDir);
            this.Controls.Add(this.txtSourceDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudFreq);
            this.Controls.Add(this.btnSync);
            this.Name = "MainForm";
            this.Text = "HistoryFileDumper";
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.NumericUpDown nudFreq;
		private System.ComponentModel.BackgroundWorker bwApp;
		private System.Windows.Forms.Timer tmrApp;
		private System.Windows.Forms.Button btnSync;
		private System.Windows.Forms.TextBox txtSourceDir;
		private System.Windows.Forms.TextBox txtDestDir;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
