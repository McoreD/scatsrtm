<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    	Me.TextBox1 = New System.Windows.Forms.TextBox
    	Me.Connect = New System.Windows.Forms.Button
    	Me.DateTime = New System.Windows.Forms.Button
    	Me.eventlog = New System.Windows.Forms.Button
    	Me.messagetype = New System.Windows.Forms.GroupBox
    	Me.CheckBox3 = New System.Windows.Forms.CheckBox
    	Me.CheckBox2 = New System.Windows.Forms.CheckBox
    	Me.CheckBox1 = New System.Windows.Forms.CheckBox
    	Me.GroupBox1 = New System.Windows.Forms.GroupBox
    	Me.GroupBox3 = New System.Windows.Forms.GroupBox
    	Me.TextBox4 = New System.Windows.Forms.TextBox
    	Me.Label2 = New System.Windows.Forms.Label
    	Me.Label1 = New System.Windows.Forms.Label
    	Me.TextBox2 = New System.Windows.Forms.TextBox
    	Me.CheckBox7 = New System.Windows.Forms.CheckBox
    	Me.GroupBox2 = New System.Windows.Forms.GroupBox
    	Me.CheckBox8 = New System.Windows.Forms.CheckBox
    	Me.CheckBox6 = New System.Windows.Forms.CheckBox
    	Me.CheckBox5 = New System.Windows.Forms.CheckBox
    	Me.CheckBox4 = New System.Windows.Forms.CheckBox
    	Me.TextBox3 = New System.Windows.Forms.TextBox
    	Me.txtRemoteIp = New System.Windows.Forms.TextBox
    	Me.messagetype.SuspendLayout
    	Me.GroupBox1.SuspendLayout
    	Me.GroupBox3.SuspendLayout
    	Me.GroupBox2.SuspendLayout
    	Me.SuspendLayout
    	'
    	'TextBox1
    	'
    	Me.TextBox1.Location = New System.Drawing.Point(259, 72)
    	Me.TextBox1.Multiline = true
    	Me.TextBox1.Name = "TextBox1"
    	Me.TextBox1.Size = New System.Drawing.Size(294, 267)
    	Me.TextBox1.TabIndex = 0
    	'
    	'Connect
    	'
    	Me.Connect.Location = New System.Drawing.Point(256, 8)
    	Me.Connect.Name = "Connect"
    	Me.Connect.Size = New System.Drawing.Size(149, 56)
    	Me.Connect.TabIndex = 1
    	Me.Connect.Text = "Connect to Central Manager"
    	Me.Connect.UseVisualStyleBackColor = true
    	'
    	'DateTime
    	'
    	Me.DateTime.AutoSize = true
    	Me.DateTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    	Me.DateTime.Location = New System.Drawing.Point(416, 40)
    	Me.DateTime.Name = "DateTime"
    	Me.DateTime.Size = New System.Drawing.Size(130, 23)
    	Me.DateTime.TabIndex = 3
    	Me.DateTime.Text = "Request Date and Time"
    	Me.DateTime.UseVisualStyleBackColor = true
    	'
    	'eventlog
    	'
    	Me.eventlog.Location = New System.Drawing.Point(13, 367)
    	Me.eventlog.Name = "eventlog"
    	Me.eventlog.Size = New System.Drawing.Size(131, 23)
    	Me.eventlog.TabIndex = 4
    	Me.eventlog.Text = "Event log request"
    	Me.eventlog.UseVisualStyleBackColor = true
    	'
    	'messagetype
    	'
    	Me.messagetype.Controls.Add(Me.CheckBox3)
    	Me.messagetype.Controls.Add(Me.CheckBox2)
    	Me.messagetype.Controls.Add(Me.CheckBox1)
    	Me.messagetype.Location = New System.Drawing.Point(6, 19)
    	Me.messagetype.Name = "messagetype"
    	Me.messagetype.Size = New System.Drawing.Size(200, 93)
    	Me.messagetype.TabIndex = 7
    	Me.messagetype.TabStop = false
    	Me.messagetype.Text = "Event log message type"
    	'
    	'CheckBox3
    	'
    	Me.CheckBox3.AutoSize = true
    	Me.CheckBox3.Location = New System.Drawing.Point(18, 67)
    	Me.CheckBox3.Name = "CheckBox3"
    	Me.CheckBox3.Size = New System.Drawing.Size(123, 17)
    	Me.CheckBox3.TabIndex = 2
    	Me.CheckBox3.Text = "Time stamp included"
    	Me.CheckBox3.UseVisualStyleBackColor = true
    	'
    	'CheckBox2
    	'
    	Me.CheckBox2.AutoSize = true
    	Me.CheckBox2.Checked = true
    	Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
    	Me.CheckBox2.Location = New System.Drawing.Point(18, 43)
    	Me.CheckBox2.Name = "CheckBox2"
    	Me.CheckBox2.Size = New System.Drawing.Size(92, 17)
    	Me.CheckBox2.TabIndex = 1
    	Me.CheckBox2.Text = "Text message"
    	Me.CheckBox2.UseVisualStyleBackColor = true
    	'
    	'CheckBox1
    	'
    	Me.CheckBox1.AutoSize = true
    	Me.CheckBox1.Location = New System.Drawing.Point(18, 19)
    	Me.CheckBox1.Name = "CheckBox1"
    	Me.CheckBox1.Size = New System.Drawing.Size(112, 17)
    	Me.CheckBox1.TabIndex = 0
    	Me.CheckBox1.Text = "Operator message"
    	Me.CheckBox1.UseVisualStyleBackColor = true
    	'
    	'GroupBox1
    	'
    	Me.GroupBox1.Controls.Add(Me.GroupBox3)
    	Me.GroupBox1.Controls.Add(Me.GroupBox2)
    	Me.GroupBox1.Controls.Add(Me.messagetype)
    	Me.GroupBox1.Controls.Add(Me.eventlog)
    	Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    	Me.GroupBox1.Name = "GroupBox1"
    	Me.GroupBox1.Size = New System.Drawing.Size(218, 409)
    	Me.GroupBox1.TabIndex = 8
    	Me.GroupBox1.TabStop = false
    	Me.GroupBox1.Text = "Event Log Request"
    	'
    	'GroupBox3
    	'
    	Me.GroupBox3.Controls.Add(Me.TextBox4)
    	Me.GroupBox3.Controls.Add(Me.Label2)
    	Me.GroupBox3.Controls.Add(Me.Label1)
    	Me.GroupBox3.Controls.Add(Me.TextBox2)
    	Me.GroupBox3.Controls.Add(Me.CheckBox7)
    	Me.GroupBox3.Location = New System.Drawing.Point(6, 238)
    	Me.GroupBox3.Name = "GroupBox3"
    	Me.GroupBox3.Size = New System.Drawing.Size(200, 123)
    	Me.GroupBox3.TabIndex = 9
    	Me.GroupBox3.TabStop = false
    	Me.GroupBox3.Text = "Alarm Type"
    	'
    	'TextBox4
    	'
    	Me.TextBox4.Location = New System.Drawing.Point(7, 97)
    	Me.TextBox4.Name = "TextBox4"
    	Me.TextBox4.Size = New System.Drawing.Size(100, 20)
    	Me.TextBox4.TabIndex = 7
    	'
    	'Label2
    	'
    	Me.Label2.AutoSize = true
    	Me.Label2.Location = New System.Drawing.Point(7, 81)
    	Me.Label2.Name = "Label2"
    	Me.Label2.Size = New System.Drawing.Size(81, 13)
    	Me.Label2.TabIndex = 6
    	Me.Label2.Text = "Extended Alarm"
    	'
    	'Label1
    	'
    	Me.Label1.AutoSize = true
    	Me.Label1.Location = New System.Drawing.Point(9, 38)
    	Me.Label1.Name = "Label1"
    	Me.Label1.Size = New System.Drawing.Size(101, 13)
    	Me.Label1.TabIndex = 5
    	Me.Label1.Text = "Enter Alarm Number"
    	'
    	'TextBox2
    	'
    	Me.TextBox2.Location = New System.Drawing.Point(10, 54)
    	Me.TextBox2.Name = "TextBox2"
    	Me.TextBox2.Size = New System.Drawing.Size(100, 20)
    	Me.TextBox2.TabIndex = 4
    	'
    	'CheckBox7
    	'
    	Me.CheckBox7.AutoSize = true
    	Me.CheckBox7.Location = New System.Drawing.Point(10, 18)
    	Me.CheckBox7.Name = "CheckBox7"
    	Me.CheckBox7.Size = New System.Drawing.Size(122, 17)
    	Me.CheckBox7.TabIndex = 3
    	Me.CheckBox7.Text = "Alarm Type included"
    	Me.CheckBox7.UseVisualStyleBackColor = true
    	'
    	'GroupBox2
    	'
    	Me.GroupBox2.Controls.Add(Me.CheckBox8)
    	Me.GroupBox2.Controls.Add(Me.CheckBox6)
    	Me.GroupBox2.Controls.Add(Me.CheckBox5)
    	Me.GroupBox2.Controls.Add(Me.CheckBox4)
    	Me.GroupBox2.Location = New System.Drawing.Point(6, 118)
    	Me.GroupBox2.Name = "GroupBox2"
    	Me.GroupBox2.Size = New System.Drawing.Size(200, 114)
    	Me.GroupBox2.TabIndex = 8
    	Me.GroupBox2.TabStop = false
    	Me.GroupBox2.Text = "Flags"
    	'
    	'CheckBox8
    	'
    	Me.CheckBox8.AutoSize = true
    	Me.CheckBox8.Location = New System.Drawing.Point(7, 91)
    	Me.CheckBox8.Name = "CheckBox8"
    	Me.CheckBox8.Size = New System.Drawing.Size(146, 17)
    	Me.CheckBox8.TabIndex = 4
    	Me.CheckBox8.Text = "Message indicates a fault"
    	Me.CheckBox8.UseVisualStyleBackColor = true
    	'
    	'CheckBox6
    	'
    	Me.CheckBox6.AutoSize = true
    	Me.CheckBox6.Location = New System.Drawing.Point(7, 68)
    	Me.CheckBox6.Name = "CheckBox6"
    	Me.CheckBox6.Size = New System.Drawing.Size(187, 17)
    	Me.CheckBox6.TabIndex = 2
    	Me.CheckBox6.Text = "Send message to event log printer"
    	Me.CheckBox6.UseVisualStyleBackColor = true
    	'
    	'CheckBox5
    	'
    	Me.CheckBox5.AutoSize = true
    	Me.CheckBox5.Location = New System.Drawing.Point(7, 44)
    	Me.CheckBox5.Name = "CheckBox5"
    	Me.CheckBox5.Size = New System.Drawing.Size(171, 17)
    	Me.CheckBox5.TabIndex = 1
    	Me.CheckBox5.Text = "Send message to alarm display"
    	Me.CheckBox5.UseVisualStyleBackColor = true
    	'
    	'CheckBox4
    	'
    	Me.CheckBox4.AutoSize = true
    	Me.CheckBox4.Location = New System.Drawing.Point(7, 20)
    	Me.CheckBox4.Name = "CheckBox4"
    	Me.CheckBox4.Size = New System.Drawing.Size(180, 17)
    	Me.CheckBox4.TabIndex = 0
    	Me.CheckBox4.Text = "Send message to fault log printer"
    	Me.CheckBox4.UseVisualStyleBackColor = true
    	'
    	'TextBox3
    	'
    	Me.TextBox3.Location = New System.Drawing.Point(259, 346)
    	Me.TextBox3.Multiline = true
    	Me.TextBox3.Name = "TextBox3"
    	Me.TextBox3.Size = New System.Drawing.Size(294, 56)
    	Me.TextBox3.TabIndex = 9
    	Me.TextBox3.Text = "Type alarm message here"
    	'
    	'txtRemoteIp
    	'
    	Me.txtRemoteIp.Location = New System.Drawing.Point(416, 16)
    	Me.txtRemoteIp.Name = "txtRemoteIp"
    	Me.txtRemoteIp.Size = New System.Drawing.Size(128, 20)
    	Me.txtRemoteIp.TabIndex = 10
    	Me.txtRemoteIp.Text = "192.168.30.200"
    	'
    	'Form1
    	'
    	Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    	Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    	Me.ClientSize = New System.Drawing.Size(565, 433)
    	Me.Controls.Add(Me.txtRemoteIp)
    	Me.Controls.Add(Me.TextBox3)
    	Me.Controls.Add(Me.GroupBox1)
    	Me.Controls.Add(Me.DateTime)
    	Me.Controls.Add(Me.Connect)
    	Me.Controls.Add(Me.TextBox1)
    	Me.Name = "Form1"
    	Me.Text = "Form1"
    	Me.messagetype.ResumeLayout(false)
    	Me.messagetype.PerformLayout
    	Me.GroupBox1.ResumeLayout(false)
    	Me.GroupBox3.ResumeLayout(false)
    	Me.GroupBox3.PerformLayout
    	Me.GroupBox2.ResumeLayout(false)
    	Me.GroupBox2.PerformLayout
    	Me.ResumeLayout(false)
    	Me.PerformLayout
    End Sub
    Private txtRemoteIp As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Connect As System.Windows.Forms.Button
    Friend WithEvents DateTime As System.Windows.Forms.Button
    Friend WithEvents eventlog As System.Windows.Forms.Button
    Friend WithEvents messagetype As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
