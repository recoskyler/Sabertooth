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
            Timer1.Enabled = False
            Timer1.Interval = My.Settings.autoOrg * 60000
            Timer1.Enabled = True
            Timer1.Start()

            Debug.Print(Timer1.Interval)
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
        Debug.Print("AUTO-ORG")

        If My.Settings.paths.Count > 0 Then
            Debug.Print("AUTO-ORG...")
            organize()
        End If

        Debug.Print("AUTO-ORG-")
    End Sub

    Private Sub calcProg()
        Dim mx As Integer = 0
        Dim filc As Integer = 0
        Dim folc As Integer = 0

        For i As Integer = 0 To My.Settings.paths.Count - 1
            Dim ps As String = My.Settings.pathSettings(i)
            Dim orgset As String = ps.Substring(0, 1)
            Dim autset As String = ps.Substring(1, 1)

            Try
                Dim flst As IEnumerable(Of String) = Directory.EnumerateFiles(My.Settings.paths(i), "*.*")

                For Each fl As String In flst ' Get all file dirs
                    Dim ext As String = Path.GetExtension(fl).ToLower().Replace("ı", "i")
                    Dim filename As String = Path.GetFileName(fl)

                    If My.Settings.whiteList.Contains(ext) Then ' Check if extension is whitelisted
                        Continue For
                    End If

                    filc = filc + 1
                Next

                If My.Settings.recursive Then
                    For Each fold As String In My.Settings.folders
                        For Each fl As String In Directory.EnumerateFiles(My.Settings.paths(i) & "\" & fold.Substring(3), "*.*")
                            Dim ext As String = Path.GetExtension(fl).ToLower().Replace("ı", "i")
                            Dim filename As String = Path.GetFileName(fl)

                            If My.Settings.whiteList.Contains(ext) Then ' Check if extension is whitelisted
                                Continue For
                            End If

                            filc = filc + 1
                        Next
                    Next
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try

            Try
                For Each fl As String In Directory.EnumerateDirectories(My.Settings.paths(i), "*") ' Get all folder dirs
                    Dim foldername As String = fl.Substring(fl.LastIndexOf("\") + 1)
                    If My.Settings.folders.Contains("[R]" & foldername) Or My.Settings.folders.Contains("[C]" & foldername) Then ' Check if folder is whitelisted
                        Continue For
                    End If
                    folc = folc + 1
                Next

                If My.Settings.recursive Then
                    For Each fold As String In My.Settings.folders
                        For Each fl As String In Directory.EnumerateDirectories(My.Settings.paths(i) & "\" & fold.Substring(3), "*")
                            folc = folc + 1
                        Next
                    Next
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try

            Select Case orgset
                Case "0"
                    mx = mx + filc + folc
                Case "1"
                    mx = mx + filc
                Case "2"
                    mx = mx + folc
            End Select
        Next

        Debug.Print(mx)

        ProgressBar1.Maximum = mx
        ProgressBar1.Value = 0
    End Sub

    Public Sub organize(Optional showMessage As Boolean = False)
        If My.Settings.paths.Count > 0 Then
            calcProg()

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

                Debug.Print("Organizing: [" & My.Settings.paths(i) & "] [" & ofil.ToString & "] [" & ofol.ToString & "]")
                Essentials.Organize(My.Settings.paths(i), ofil, ofol)
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

    Private Sub AllButton_MouseHover(sender As Object, e As EventArgs) Handles AllButton.MouseHover
        If My.Settings.paths.Count > 0 Then
            'changeSizeLoc(AllButton, 0)
        End If
    End Sub

    Private Sub AllButton_MouseLeave(sender As Object, e As EventArgs) Handles AllButton.MouseLeave
        If My.Settings.paths.Count > 0 Then
            'changeSizeLoc(AllButton, 1)
        End If
    End Sub

    Private Sub AllButton_MouseDown(sender As Object, e As MouseEventArgs) Handles AllButton.MouseDown
        If My.Settings.paths.Count > 0 Then
            changeSizeLoc(AllButton, 2)
        End If
    End Sub

    Private Sub changeSizeLoc(ByRef o As Object, ByVal mode As Integer)
        If mode = 0 Then ' HOVER
            For i As Integer = 1 To 3
                o.Size = New Size(o.Size.Width + (i * 2), o.Size.Height + (i * 2))
                o.Location = New Point(o.Location.X - i, o.Location.Y - i)
                RSleep(1)
            Next
        ElseIf mode = 1 Then ' LEAVE
            For i As Integer = 1 To 3
                o.Size = New Size(o.Size.Width - (i * 2), o.Size.Height - (i * 2))
                o.Location = New Point(o.Location.X + i, o.Location.Y + i)
                RSleep(1)
            Next
        ElseIf mode = 2 Then ' CLICK
            For i As Integer = 1 To 3
                o.Size = New Size(o.Size.Width - (i * 4), o.Size.Height - (i * 4))
                o.Location = New Point(o.Location.X + (i * 2), o.Location.Y + (i * 2))
                RSleep(1)
            Next

            RSleep(2)

            For i As Integer = 1 To 3
                o.Size = New Size(o.Size.Width + (i * 4), o.Size.Height + (i * 4))
                o.Location = New Point(o.Location.X - (i * 2), o.Location.Y - (i * 2))
                RSleep(1)
            Next
        End If
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        'changeSizeLoc(Button1, 0)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        'changeSizeLoc(Button1, 1)
    End Sub

    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        'changeSizeLoc(Button2, 0)
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        'changeSizeLoc(Button2, 1)
    End Sub

    Private Sub Button2_MouseDown(sender As Object, e As MouseEventArgs) Handles Button2.MouseDown
        'changeSizeLoc(Button2, 2)
    End Sub

    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        'changeSizeLoc(button1, 2)
    End Sub
End Class
