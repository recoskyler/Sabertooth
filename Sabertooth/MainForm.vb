Imports System.IO

Public Class MainForm

#Region "Variables"

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

#End Region

#Region "Functions"

    Public Sub updateOrgButtonImage(state As Boolean) ' Updates Auto Organize Button Image with animation
        AutoOrgButton.Image = My.Resources.mid
        Essentials.RSleep(20)

        If state Then
            AutoOrgButton.Image = My.Resources.on_pic
        Else
            AutoOrgButton.Image = My.Resources.off_pic
        End If
    End Sub

    Private Sub autoOrgChange()
        My.Settings.autoOrgState = Not My.Settings.autoOrgState
        My.Settings.Save()
        My.Settings.Reload()

        updateOrgButtonImage(My.Settings.autoOrgState)

        Timer1.Enabled = My.Settings.autoOrgState
    End Sub

#End Region

#Region "Move Form"

    Private Sub TitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub TitleBar_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar.MouseUp
        drag = False
    End Sub

    Private Sub Label1_MouseUp(sender As Object, e As MouseEventArgs) Handles Label1.MouseUp
        drag = False
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        drag = False
    End Sub

#End Region

    Private Sub SettingsButton_Click(sender As Object, e As EventArgs)
        SettingsForm.Show()
        Me.Hide()
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        If My.Settings.closeExit Then
            NotifyIcon1.Visible = False
            My.Settings.Save()
            Application.DoEvents()
            Application.Exit()
        Else
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.ShowBalloonTip(1, "Sabertooth is here!", "Sabertooth is minimized to tray. Right click for options, or double click to open Sabertooth.", ToolTipIcon.Info)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SettingsForm.Show()
        SettingsForm.NumericUpDown1.Value = My.Settings.autoOrg
        Me.Hide()
    End Sub

    Private Sub AutoOrgButton_Click(sender As Object, e As EventArgs) Handles AutoOrgButton.Click
        autoOrgChange()
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If My.Settings.startMin Then
            NotifyIcon1.Visible = True
            Me.Hide()
        End If

        If My.Settings.autoOrgState Then
            AutoOrgButton.Image = My.Resources.on_pic
            Timer1.Interval = My.Settings.autoOrg * 1000
            Timer1.Enabled = True
            Timer1.Start()
        Else
            AutoOrgButton.Image = My.Resources.off_pic
            Timer1.Enabled = False
        End If

        If Not My.Settings.paths.Count > 0 Then
            AllButton.Image = My.Resources.bwfolder
        Else
            AllButton.Image = My.Resources.folder
        End If
    End Sub

    Private Sub AllButton_Click(sender As Object, e As EventArgs) Handles AllButton.Click
        organize(True)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim pt As String = FolderBrowserDialog1.SelectedPath()

            If Directory.Exists(pt) Then
                My.Settings.paths.Add(pt)
                My.Settings.pathSettings.Add("01")
                My.Settings.Save()
                My.Settings.Reload()
            End If
        End If
    End Sub

    Private Sub AutoOrgLabel_Click(sender As Object, e As EventArgs) Handles AutoOrgLabel.Click
        autoOrgChange()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        NotifyIcon1.Visible = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If My.Settings.paths.Count > 0 Then
            organize()
        End If
    End Sub

    Public Sub organize(Optional showMessage As Boolean = False)
        If My.Settings.paths.Count > 0 Then
            For i As Integer = 0 To My.Settings.paths.Count - 1
                Dim ps As String = My.Settings.pathSettings(i)
                Dim orgset As String = ps.Substring(0, 1)
                Dim autset As String = ps.Substring(1, 1)

                Dim ofil As Boolean = False
                Dim ofol As Boolean = False

                Select Case orgset
                    Case "0"
                        ofil = True
                        ofol = True
                    Case "1"
                        ofil = True
                    Case "2"
                        ofol = True
                End Select

                If autset = "1" Then
                    Essentials.Organize(My.Settings.paths(i), ofil, ofol)
                End If
            Next
        Else
            If showMessage Then
                MessageBox.Show("Hey! What do you expect me to organize! You must first add a folder to organize. Psst, it's in the settings.", "No folders to organize!?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Right Then
            TrayForm.Show()
            TrayForm.Activate()
            TrayForm.Width = 1
            TrayForm.Height = 1
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.autoUpdate Then
            My.Settings.latestver = checkForUpdates()
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub
End Class
