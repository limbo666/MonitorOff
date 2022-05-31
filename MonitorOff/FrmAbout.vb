Public Class FrmAbout
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = GetSetting("MonitorOff", "Settings", "Top", 100)
        Me.Left = GetSetting("MonitorOff", "Settings", "Left", 500)
    End Sub
End Class