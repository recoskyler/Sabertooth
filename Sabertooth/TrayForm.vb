Public Class TrayForm
    Private Sub AutoOrgItem_Click(sender As Object, e As EventArgs) Handles AutoOrgItem.Click
        My.Settings.autoOrgState = Not My.Settings.autoOrgState
        My.Settings.Save()
        My.Settings.Reload()

        MainForm.Timer1.Enabled = My.Settings.autoOrgState

        If My.Settings.autoOrgState Then
            MainForm.Timer1.Interval = My.Settings.autoOrg
            MainForm.Timer1.Start()
        Else
            MainForm.Timer1.Stop()
        End If

        Me.Close()
        refreshContext()
    End Sub

    Private Sub OrgItem_Click(sender As Object, e As EventArgs) Handles OrgItem.Click
        MainForm.organize()
    End Sub

    Private Sub ExitItem_Click(sender As Object, e As EventArgs) Handles ExitItem.Click
        My.Settings.Save()
        MainForm.NotifyIcon1.Visible = False
        Application.DoEvents()
        Application.Exit()
    End Sub

    Private Sub TrayForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ContextMenuStrip1.Show(Cursor.Position) 'Shows the Right click menu on the cursor position
        Me.Left = ContextMenuStrip1.Left + 1 'Puts the form behind the menu horizontally
        Me.Top = ContextMenuStrip1.Top + 1 'Puts the form behind the menu vertically

        refreshContext()
    End Sub

    Public Sub refreshContext()
        If Not My.Settings.paths.Count > 0 Then
            OrgItem.Image = My.Resources.bwfolder
            OrgItem.Enabled = False
        Else
            OrgItem.Image = My.Resources.folder
            OrgItem.Enabled = True
        End If

        If My.Settings.autoOrgState Then
            AutoOrgItem.Image = My.Resources.on_pic
            AutoOrgItem.Text = "Turn Auto-Organizer OFF"
        Else
            AutoOrgItem.Image = My.Resources.off_pic
            AutoOrgItem.Text = "Turn Auto-Organizer ON"
        End If
    End Sub

    Private Sub OpenItem_Click(sender As Object, e As EventArgs) Handles OpenItem.Click
        MainForm.NotifyIcon1.Visible = False
        MainForm.Show()
    End Sub

    Private Sub TrayForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub
End Class