Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Text
Imports System.Environment
Public Class Form1

    Dim MainGridView As New DataGridView()
    Dim AlarmsGridView As New DataGridView()
    Dim data As New TextBox()
    Dim Ping As Ping
    Dim pReply As PingReply
    Private Strt As System.Threading.Thread

    Dim ITSclient As New System.Net.Sockets.TcpClient()
    Dim connected As Boolean = False

 

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "SCATS Intersection Monitor"
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.WindowState = FormWindowState.Maximized
        Me.Controls.Add(MainGridView)
        Me.Controls.Add(AlarmsGridView)
        Me.Controls.Add(data)


        MainGridView.Columns.Add("LM", "LM")
        MainGridView.Columns.Add("IntersectionName", "IntersectionName")
        MainGridView.Columns.Add("ControllerType", "Controller Type")
        MainGridView.Columns.Add("SCATSFault", "SCATS Fault")
        MainGridView.Columns.Add("TimeOfFault", "TimeOfFault")
        MainGridView.Columns.Add("IPAddress", "IPAddress")
        MainGridView.Columns.Add("CommsTimeChk", "Comms Time Check")
        MainGridView.Columns.Add("PossibleFault", "Possible Fault")


        MainGridView.Height = (Me.Height - 300)
        MainGridView.Width = (Me.Width - 10)
        MainGridView.Location = New Point(0, 0)
        MainGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        MainGridView.BackgroundColor = Color.White
        MainGridView.GridColor = Color.White
        MainGridView.RowTemplate.Height = "18"
        MainGridView.RowHeadersVisible = False


        AlarmsGridView.Columns.Add("VMSName", "VMS Name")
        AlarmsGridView.Columns.Add("Timestamp", "Timestamp")
        AlarmsGridView.Columns.Add("AlarmType", "Alarm Type")
        AlarmsGridView.Columns.Add("AlarmDescription", "Alarm Description")


        AlarmsGridView.Height = (Me.Height - MainGridView.Height - 100)
        AlarmsGridView.Width = (Me.Width - 10)
        AlarmsGridView.Location = New Point(0, MainGridView.Height + 50)
        AlarmsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        AlarmsGridView.BackgroundColor = Color.White
        AlarmsGridView.GridColor = Color.White
        AlarmsGridView.RowHeadersVisible = False

       


    End Sub

    Sub MyThread2()
        Connect2SCATScm()
        Dim PossibleFault As String = ""
        Dim random As New Random()
        Dim i As Integer
        For i = 1 To 10
            Ping = New Ping
            pReply = Ping.Send("192.168.0." & i)
            Dim num As Integer = random.Next(1000)
            If pReply.Status.ToString = "Success" Then
                PossibleFault = "Suspected Site Power Failure"
            Else
                PossibleFault = "Suspected Communications Failure"
            End If
            MainGridView.Rows.Add("LM" & num, "", "XM4", "NC,ST", TimeOfDay.Second, "192.168.0." & i, TimeOfDay.Second, PossibleFault)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim t As Thread
        t = New Thread(AddressOf Me.MyThread2)
        t.Start()
    End Sub

    Private Sub Connect2SCATScm()
        'Connect to SCATS CM 
        Dim sendbytes() As Byte

        'Connect to Central Manager
        Try
            ITSclient.connectionTimeout = 5
            ' ITSclient.Connect("192.168.30.200", 2012)
        Catch
        End Try

        If ITSclient.Connected Then
            Me.Text = "SCATS Intersection Monitor - Connected to SCATS Central Manager" '& NewLine

            'Create license key
            Dim networkStream As NetworkStream = ITSclient.GetStream()
            If networkStream.CanWrite And networkStream.CanRead Then
                connected = True
                Dim ITSUser As String = "Ripple"
                Dim ITSLicense As String = "_WKfR8agHEXBtsprxOd+RnpdUY_9cRSS"
                Dim length As Integer = (ITSUser & "/" & ITSLicense).Length + 1
                'Dim sendbytes As [Byte]() = Encoding.ASCII.GetBytes(0 & length & 100 & ITSUser & "/" & ITSLicense)
                sendbytes = Encoding.ASCII.GetBytes(100 & ITSUser & "\" & ITSLicense)

                'Setup initial bytes
                sendbytes(0) = 0
                sendbytes(1) = length
                sendbytes(2) = 100

                ' Send license info to central manager
                networkStream.Write(sendbytes, 0, sendbytes.Length)

                'TextBox1.Text += "Sent ITS License to Central Manager: " & ITSUser & "\" & ITSLicense & NewLine & NewLine

                Dim recieveBuff(255) As Byte
                networkStream.Read(recieveBuff, 0, 2)
                networkStream.Read(recieveBuff, 0, recieveBuff(1))

                If recieveBuff(1) = 0 Then
                    Me.Text = "Connection successful" & NewLine & NewLine
                Else
                    Me.Text = "Failed to Connect error code: " & recieveBuff(1) & NewLine & NewLine
                End If
            End If

        Else
            Me.Text = "SCATS Intersection Monitor - Failed to SCATS Central Manager"
        End If

    End Sub




End Class

'Dim filetoread As String = "c:\temp\test.txt"
'Dim filestream As StreamReader
'filestream = File.Opentext(filetoread)
'Dim readcontents As String
'readcontents = filestream.ReadToEnd()
'Dim textdelimiter As String
'textdelimiter = ","
'Dim splitout = Split(readcontents, textdelimiter)
''MsgBox(readcontents & "<br>")
'Dim i As Integer
'For i = 0 To UBound(splitout)
'    'data.Text &= "<b>Split </b>" & i + 1 & ")   " & splitout(i) & "<br>"
'    data.Text = ""
'    data.Text &= splitout(i)

'Next
'filestream.Close()

'Dim random As New Random()
'Dim i As Integer
'For i = 1 To 20
'    Ping = New Ping
'    pReply = Ping.Send("192.168.0." & i)
'    Dim num As Integer = random.Next(1000)
'    MainGridView.Rows.Add("192.168.0." & i, "", "", pReply.Status.ToString, "", "")
'Next
