Imports System.Net.Sockets
Imports System.Text
Imports System.Environment



Public Class Form1
    Dim ITSclient As New System.Net.Sockets.TcpClient()
    Dim connected As Boolean = False

    Private Sub Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Connect.Click
        Dim sendbytes() As Byte

        'Connect to Central Manager
        ITSclient.Connect("192.168.30.200", 2012)
        If ITSclient.Connected Then
            TextBox1.Text = "Connected to SCATS Central Manager" & NewLine
        End If

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

            TextBox1.Text += "Sent ITS License to Central Manager: " & ITSUser & "\" & ITSLicense & NewLine & NewLine

            Dim recieveBuff(255) As Byte
            networkStream.Read(recieveBuff, 0, 2)
            networkStream.Read(recieveBuff, 0, recieveBuff(1))

            If recieveBuff(1) = 0 Then
                TextBox1.Text += "Connection successful" & NewLine & NewLine
            Else
                TextBox1.Text += "Failed to Connect error code: " & recieveBuff(1) & NewLine & NewLine
            End If

        End If


    End Sub

    Private Sub DateTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTime.Click
        'Dim sendbytes(2) As Byte

        'sendbytes(0) = 0
        'sendbytes(1) = 1
        'sendbytes(2) = 3

        'Dim networkStream As NetworkStream = ITSclient.GetStream()
        'networkStream.Write(sendbytes, 0, sendbytes.Length)

        'Dim readBuff(9) As Byte
        'networkStream.Read(readBuff, 0, 10)

        TextBox1.Text += System.DateTime.Now.Second & NewLine

        'TextBox1.Text += "Central Manager Time:" & NewLine & readBuff(6) & "/" & readBuff(5) & "/" & (readBuff(4) + 1792) & _
        '" Time: " & readBuff(7) & ":" & readBuff(8) & ":" & readBuff(9) & NewLine
    End Sub



    Private Sub eventlog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eventlog.Click
        Dim sendbytes() As Byte
        Dim fill As String = ""
        Dim data_length As Byte
        Dim SBflag As Integer = 4

        If connected = False Then
            MsgBox("Please connect to central manager first")
            Exit Sub
        End If

        Dim networkStream As NetworkStream = ITSclient.GetStream()

        'Check alarm text message is within bounds
        If TextBox3.Text.Length > 100 Then
            MsgBox("Alarm text message too large (100 chars max)")
            Exit Sub
        End If

        Try
            If CInt(TextBox2.Text) > 255 Then
                MsgBox("alarms greater than 255 currently unsupported")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("alarm number invalid")
            Exit Sub
        End Try

        Calculate_Data_length(data_length, fill)

        sendbytes = Encoding.ASCII.GetBytes(fill & TextBox3.Text)

        'Data Length
        sendbytes(0) = 0
        sendbytes(1) = data_length
        'message type = 1
        sendbytes(2) = 1

        'Bits 0-6: event log message type
        '3 = Operator message = checkbox1
        '16 = Text message = checkbox2
        'Bit 7: Time Stamp flag = checkbox3
        '0 = No time stamp included, 1 = Time stamp included
        If CheckBox1.CheckState = CheckState.Checked Then
            sendbytes(3) = 3
        Else
            sendbytes(3) = 16
        End If

        If CheckBox3.CheckState = CheckState.Checked Then
            sendbytes(3) += 128
        End If

        'Bits 0-4: Day of month (1 - 31)
        'Bits 5-8: Month (1 - 12)
        'Bits 9-15: Years since 1970
        If CheckBox3.CheckState = CheckState.Checked Then

            sendbytes(4) = System.DateTime.Now.Day
            sendbytes(4) += CByte((System.DateTime.Now.Month * 32))

            If System.DateTime.Now.Month > 8 Then
                sendbytes(5) = 1
            End If

            sendbytes(5) += CByte(((System.DateTime.Now.Year - 1970) * 2))

            'Bits 0-4: seconds/2 (0-29)
            'Bits 5-10: Minutes (0-59)
            'Bits 11-15: Hour (0-23)

            sendbytes(6) = CByte((System.DateTime.Now.Second) / 2)
            Dim minute As Int16 = System.DateTime.Now.Minute
            minute = minute And 56
            sendbytes(6) += CByte(minute)
            sendbytes(7) = CByte((System.DateTime.Now.Minute) / 16)
            sendbytes(7) += CByte((System.DateTime.Now.Hour) * 8)
            'position of sendbytes for flags
            SBflag = 8
        End If

        'Create flags byte bit 7 must be set to indicate this is a flags byte
        sendbytes(SBflag) = 128
        'Fault log flag
        If CheckBox4.CheckState = CheckState.Checked Then
            sendbytes(SBflag) += 1
        End If
        'Alarm display flag
        If CheckBox5.CheckState = CheckState.Checked Then
            sendbytes(SBflag) += 2
        End If
        'Event log flag
        If CheckBox6.CheckState = CheckState.Checked Then
            sendbytes(SBflag) += 4
        End If
        'Alarm type flag
        If CheckBox7.CheckState = CheckState.Checked Then
            sendbytes(SBflag) += 16
            sendbytes(SBflag + 1) = TextBox2.Text

            If TextBox2.Text = 255 Then
                sendbytes(SBflag + 2) = 21
                sendbytes(SBflag + 3) = 25

            End If
        End If
        'Fault flag
        If CheckBox8.CheckState = CheckState.Checked Then
            sendbytes(SBflag) += 64
        End If

        NetworkStream.Write(sendbytes, 0, sendbytes.Length)

        TextBox1.Text += "Event sent to central manager" & NewLine

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.CheckState = CheckState.Checked Then
            CheckBox2.CheckState = CheckState.Unchecked
        Else
            CheckBox2.CheckState = CheckState.Checked
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.CheckState = CheckState.Checked Then
            CheckBox1.CheckState = CheckState.Unchecked
        Else
            CheckBox1.CheckState = CheckState.Checked
        End If

    End Sub

    Private Sub Calculate_Data_length(ByRef data_length As Byte, ByRef fill As String)

        'Determine data length
        data_length = 3
        fill = "aaaaa"

        'Check for need of timestamp
        If CheckBox3.CheckState = CheckState.Checked Then
            data_length += 4
            fill += "aaaa"
        End If

        'check for alarm type flag
        If CheckBox7.CheckState = CheckState.Checked Then
            data_length += 1
            fill += "a"
        End If

        'Support for extended alarm type
        If TextBox2.Text = "255" Then
            data_length += 2
            fill += "aa"
        End If

        'Alarm message size
        data_length += TextBox3.Text.Length

    End Sub

End Class
