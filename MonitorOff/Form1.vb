


Imports System.Runtime.InteropServices

Public Class Form1
    ' Taken from  https://stackoverflow.com/questions/6221787/how-to-turn-off-a-monitor-using-vb-net-code
    Dim TurnOffTime As String
    Public WM_SYSCOMMAND As Integer = &H112
    Public SC_MONITORPOWER As Integer = &HF170
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SendMessage(ByVal hWnd As Integer, ByVal hMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '  SendMessage(Me.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2)
        '   TurnOffScreen()

        Dim timeNow As String = Now.ToString("HH:mm")
        Dim timeOff As String = NumHours.Value.ToString & ":" & NumMins.Value.ToString

        If timeNow = timeOff Then

            '    Button1_Click(Nothing, Nothing)
            TurnOffScreen()
        End If

    End Sub

    Sub TurnOffScreen()
        SendMessage(Me.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim timeNow As String = Now.ToString("HH:mm:ss")
        'If timeNow >= "06:00:00" And timeNow <= "11:00:00" Then
        '    'Do something`
        'End If
        '   If timeNow = "10:11:30" Then
        If timeNow = TurnOffTime Then

            '    Button1_Click(Nothing, Nothing)
            TurnOffScreen()
        End If

        'If timeNow = "10:12:30" Then
        '    MsgBox(timeNow)
        'End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Me.ShowInTaskbar = False
        NotifyIcon1.Visible = True


        NumHours.Value = GetSetting("MonitorOff", "Settings", "Hour", "16")
        NumMins.Value = GetSetting("MonitorOff", "Settings", "Min", "30")
        NumSecs.Value = GetSetting("MonitorOff", "Settings", "Sec", "00")
        CheckBox1.Checked = GetSetting("MonitorOff", "Settings", "Enabled", False)

        Me.Top = GetSetting("MonitorOff", "Settings", "Top", 100)
        Me.Left = GetSetting("MonitorOff", "Settings", "Left", 500)

        Timer1.Enabled = True
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size


        GenerateTime()
        UpdateNotifyIconText()
    End Sub
    Sub UpdateNotifyIconText()
        If CheckBox1.Checked = True Then
            NotifyIcon1.Text = "Enabled" & vbCrLf & TurnOffTime
        Else
            NotifyIcon1.Text = "Disabled"
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Timer1.Enabled = CheckBox1.Checked
        UpdateNotifyIconText()

    End Sub
    Sub GenerateTime()
        TurnOffTime = NumHours.Value.ToString.PadLeft(2, "0") & ":" & NumMins.Value.ToString.PadLeft(2, "0") & ":" & NumSecs.Value.ToString.PadLeft(2, "0")
    End Sub

    Private Sub NumHours_ValueChanged(sender As Object, e As EventArgs) Handles NumHours.ValueChanged
        GenerateTime()
        UpdateNotifyIconText()
    End Sub

    Private Sub NumMins_ValueChanged(sender As Object, e As EventArgs) Handles NumMins.ValueChanged
        GenerateTime()
        UpdateNotifyIconText()
    End Sub

    Private Sub NumSecs_ValueChanged(sender As Object, e As EventArgs) Handles NumSecs.ValueChanged
        GenerateTime()
        UpdateNotifyIconText()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting("MonitorOff", "Settings", "Hour", NumHours.Value)
        SaveSetting("MonitorOff", "Settings", "Min", NumMins.Value)
        SaveSetting("MonitorOff", "Settings", "Sec", NumSecs.Value)
        SaveSetting("MonitorOff", "Settings", "Enabled", CheckBox1.Checked)

        If Me.Visible = True Then
            SaveSetting("MonitorOff", "Settings", "Top", Me.Top)
            SaveSetting("MonitorOff", "Settings", "Left", Me.Left)
        End If
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        Me.ShowInTaskbar = True
        Me.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.ShowInTaskbar = True
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()
    End Sub


End Class


