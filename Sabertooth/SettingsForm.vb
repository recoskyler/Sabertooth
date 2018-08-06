Imports System.IO
Imports Microsoft.Win32
Imports System.Reflection
Imports Sabertooth.My.Resources

Public Class SettingsForm

    ' Path Settings Format
    '
    ' 2 1
    ' | |
    ' | Auto Organize   (0 - OFF / 1 - ON)
    ' |
    ' Organize Settings (0 - Organize All / 1 - Only Files / 2 - Only Folders)
    '
    '✕✔

    ' Folders Format
    '
    ' [C]foldername ==> Custom
    ' [R]foldername ==> Regular
    '

    Public Sub refreshSettings()
        My.Settings.Save()
        My.Settings.Reload()

        CheckBox1.Checked = My.Settings.autostart
        CheckBox2.Checked = My.Settings.startMin
        CheckBox3.Checked = My.Settings.autoUpdate
        CheckBox4.Checked = My.Settings.autoOrgState
        CheckBox5.Checked = My.Settings.recursive
        CheckBox6.Checked = My.Settings.closeExit
        NumericUpDown1.Value = My.Settings.autoOrg
        NumericUpDown1.Enabled = My.Settings.autoOrgState
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        RadioButton3.Enabled = False
        GroupBox1.Enabled = False
        ListView1.Items.Clear()
        ListView2.Items.Clear()
        ListView3.Items.Clear()
        ListView4.Items.Clear()
        ListView1.View = View.Details
        ListView2.View = View.Details
        ListView3.View = View.Details
        ListView4.View = View.Details
        AutoOrgButton.Image = My.Resources.mid
        AutoOrgLabel.Enabled = False
        AutoOrgButton.Enabled = False
        PictureBox2.Enabled = False
        PictureBox3.Enabled = False
        PictureBox4.Enabled = False
        PictureBox6.Enabled = False
        PictureBox7.Enabled = False
        PictureBox8.Enabled = False
        PictureBox9.Enabled = False
        PictureBox11.Enabled = False
        PictureBox13.Enabled = False
        PictureBox14.Enabled = False
        Label1.Enabled = False
        Label2.Enabled = False
        Label7.Enabled = False
        Label11.Enabled = False
        Label12.Enabled = False
        Label14.Enabled = False
        Label17.Enabled = False
        Label18.Enabled = False
        Label19.Enabled = False
        Label21.Enabled = False
        ComboBox2.Items.Clear()
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox6.Text = ""
        CVL.Text = My.Settings.ver
        LVL.Text = My.Settings.latestver

        If Convert.ToInt32(CVL.Text.Replace(".", "")) < Convert.ToInt32(LVL.Text.Replace(".", "")) Then
            Button3.Enabled = True
            Button3.Text = "Updates are available. Download Here"
        Else
            Button3.Enabled = False
            Button3.Text = "No updates are available"
        End If

        If My.Settings.folders.Count > 0 Then
            For i As Integer = 0 To My.Settings.folders.Count - 1
                Dim itm As New ListViewItem(My.Settings.folders(i).Substring(3))
                ListView3.Items.Add(itm)
                ComboBox2.Items.Add(My.Settings.folders(i).Substring(3))
            Next
        End If

        If My.Settings.whiteList.Count > 0 Then
            For i As Integer = 0 To My.Settings.whiteList.Count - 1
                Dim itm As New ListViewItem(My.Settings.whiteList(i))
                ListView4.Items.Add(itm)
            Next
        End If

        If My.Settings.extensions.Count > 0 Then
            For i As Integer = 0 To (My.Settings.extensions.Count / 2) - 1
                Dim itm As New ListViewItem(My.Settings.extensions(i * 2))
                ListView1.Items.Add(itm)
                ListView1.Items(i).SubItems.Add(My.Settings.extensions((i * 2) + 1).Substring(3))
            Next
        End If

        If My.Settings.paths.Count > 0 Then
            For i As Integer = 0 To My.Settings.paths.Count - 1
                Dim itm As New ListViewItem(My.Settings.paths(i))

                ListView2.Items.Add(itm)

                Dim ps As String = My.Settings.pathSettings(i)
                Dim orgset As String = ps.Substring(0, 1)
                Dim autset As String = ps.Substring(1, 1)

                Dim ofil As String = "✕"
                Dim ofol As String = "✕"
                Dim oaut As String = "✕"

                Select Case orgset
                    Case "0"
                        ofil = "✔"
                        ofol = "✔"
                    Case "1"
                        ofil = "✔"
                    Case "2"
                        ofol = "✔"
                End Select

                Select Case autset
                    Case "0"
                        oaut = "✕"
                    Case "1"
                        oaut = "✔"
                End Select

                ListView2.Items(i).SubItems.Add(ofil)
                ListView2.Items(i).SubItems.Add(ofol)
                ListView2.Items(i).SubItems.Add(oaut)
            Next
        End If

        checkColors()
    End Sub

    Private Sub SettingsForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        MainForm.Show()
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown1.Value = My.Settings.autoOrg
        Application.DoEvents()
        refreshSettings()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", Application.ProductName, Application.ExecutablePath, RegistryValueKind.String)
        Else
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", Application.ProductName, "", RegistryValueKind.String)
        End If

        My.Settings.autostart = CheckBox1.Checked
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.startMin = CheckBox2.Checked
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        My.Settings.autoUpdate = CheckBox3.Checked
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        My.Settings.autoOrgState = CheckBox4.Checked
        My.Settings.autoOrg = NumericUpDown1.Value
        My.Settings.Save()
        My.Settings.Reload()

        MainForm.updateOrgButtonImage(CheckBox4.Checked)
        NumericUpDown1.Enabled = CheckBox4.Checked
        MainForm.Timer1.Enabled = My.Settings.autoOrgState
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim pt As String = FolderBrowserDialog1.SelectedPath()

            If Directory.Exists(pt) Then
                My.Settings.paths.Add(pt)
                My.Settings.pathSettings.Add("01")
                My.Settings.Save()
                My.Settings.Reload()

                refreshSettings()
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        refreshSettings()
    End Sub

    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.SelectedIndexChanged
        If ListView2.SelectedItems.Count = 1 Then
            refreshPathSettings(ListView2.SelectedItems(0).Index)
        Else
            AutoOrgButton.Image = My.Resources.mid
            AutoOrgLabel.Enabled = False
            AutoOrgButton.Enabled = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            RadioButton3.Enabled = False
            GroupBox1.Enabled = False
            PictureBox2.Enabled = False
            PictureBox14.Enabled = False
            Label1.Enabled = False
            Label19.Enabled = False
        End If

        checkColors()
    End Sub

    Private Sub refreshPathSettings(index As Integer)
        AutoOrgButton.Image = My.Resources.mid
        AutoOrgLabel.Enabled = True
        AutoOrgButton.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        RadioButton3.Enabled = True
        GroupBox1.Enabled = True
        PictureBox2.Enabled = True
        Label1.Enabled = True
        PictureBox14.Enabled = True
        Label19.Enabled = True

        If ListView2.Items(index).SubItems(1).Text = ListView2.Items(index).SubItems(2).Text And ListView2.Items(index).SubItems(2).Text = "✔" Then
            RadioButton1.Checked = True
        ElseIf ListView2.Items(index).SubItems(1).Text = "✔" Then
            RadioButton2.Checked = True
        Else
            RadioButton3.Checked = True
        End If

        If ListView2.Items(index).SubItems(3).Text = "✔" Then
            AutoOrgButton.Image = My.Resources.on_pic
        Else
            AutoOrgButton.Image = My.Resources.off_pic
        End If

        checkColors()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If Not ListView2.SelectedItems.Count > 0 Then
            Return
        End If

        Dim index As Integer = ListView2.SelectedItems(0).Index

        If RadioButton1.Checked Then
            ListView2.SelectedItems(0).SubItems(1).Text = "✔"
            ListView2.SelectedItems(0).SubItems(2).Text = "✔"
            updatePathSetting(index)
        End If
    End Sub

    Private Sub updatePathSetting(index As Integer)
        Dim str As String = ""

        If ListView2.SelectedItems(0).SubItems(2).Text = ListView2.SelectedItems(0).SubItems(1).Text And ListView2.SelectedItems(0).SubItems(1).Text = "✔" Then
            str = "0"
        ElseIf ListView2.SelectedItems(0).SubItems(2).Text = "✔" Then
            str = "2"
        Else
            str = "1"
        End If

        If ListView2.SelectedItems(0).SubItems(3).Text = "✔" Then
            str = str & "1"
        Else
            str = str & "0"
        End If

        My.Settings.pathSettings(index) = str
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub AutoOrgButton_Click(sender As Object, e As EventArgs) Handles AutoOrgButton.Click
        autoOrgChange()
    End Sub

    Private Sub AutoOrgLabel_Click(sender As Object, e As EventArgs) Handles AutoOrgLabel.Click
        autoOrgChange()
    End Sub

    Private Sub autoOrgChange()
        Dim index As Integer = ListView2.SelectedItems(0).Index

        If ListView2.SelectedItems(0).SubItems(3).Text = "✔" Then
            ListView2.SelectedItems(0).SubItems(3).Text = "✕"
            updateOrgButtonImage(False)
        Else
            ListView2.SelectedItems(0).SubItems(3).Text = "✔"
            updateOrgButtonImage(True)
        End If

        updatePathSetting(index)
    End Sub

    Public Sub updateOrgButtonImage(state As Boolean) ' Updates Auto Organize Button Image with animation
        AutoOrgButton.Image = My.Resources.mid
        Essentials.RSleep(20)

        If state Then
            AutoOrgButton.Image = My.Resources.on_pic
        Else
            AutoOrgButton.Image = My.Resources.off_pic
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Dim index As Integer = ListView2.SelectedItems(0).Index

        If RadioButton2.Checked Then
            ListView2.SelectedItems(0).SubItems(1).Text = "✔"
            ListView2.SelectedItems(0).SubItems(2).Text = "✕"
            updatePathSetting(index)
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Dim index As Integer = ListView2.SelectedItems(0).Index

        If RadioButton3.Checked Then
            ListView2.SelectedItems(0).SubItems(1).Text = "✕"
            ListView2.SelectedItems(0).SubItems(2).Text = "✔"
            updatePathSetting(index)
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        excludeFolder()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        excludeFolder()
    End Sub

    Private Sub excludeFolder()
        Dim index As Integer = ListView2.SelectedItems(0).Index

        My.Settings.paths.RemoveAt(index)
        My.Settings.pathSettings.RemoveAt(index)
        My.Settings.Save()
        My.Settings.Reload()

        refreshSettings()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Enabled Then
            My.Settings.autoOrg = NumericUpDown1.Value
        End If

        My.Settings.Save()
        My.Settings.Reload()
        MainForm.Timer1.Stop()
        MainForm.Timer1.Interval = My.Settings.autoOrg * 60000
        MainForm.Timer1.Start()

        'Debug.Print(My.Settings.autoOrg)
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        My.Settings.recursive = CheckBox5.Checked
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub resetFilters()
        If MessageBox.Show("Are you sure? All of your filters and sub folders will be gone, this is a complete reset!", "You sure about dis?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
            My.Settings.Reset()
            My.Settings.Save()
            My.Settings.Reload()
            refreshSettings()

            MessageBox.Show("Reset complete.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        resetFilters()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        resetFilters()
    End Sub

    Private Sub checkAdd()
        PictureBox3.Enabled = False
        Label2.Enabled = False

        checkColors()

        If TextBox2.Text = "" Or TextBox2.Text = "." Then
            Return
        End If

        If Not My.Settings.extensions.Contains(TextBox2.Text.ToLower) And Not My.Settings.extensions.Contains("." & TextBox2.Text.ToLower) Then
            If ComboBox2.SelectedIndex >= 0 Then
                PictureBox4.Enabled = True
                Label7.Enabled = True

                checkColors()
            End If
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        PictureBox3.Enabled = False
        Label2.Enabled = False
        GroupBox2.Enabled = False

        checkColors()

        If Not ListView1.SelectedItems.Count > 0 Then
            Return
        End If

        If ListView1.SelectedItems(0).Text = "Folders" Then
            Return
        End If

        PictureBox3.Enabled = True
        Label2.Enabled = True
        GroupBox2.Enabled = True

        checkColors()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        checkAdd()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        checkAdd()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        addFilter()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        addFilter()
    End Sub

    Private Sub addFilter()
        If Not TextBox2.Text.Contains(".") Then
            TextBox2.Text = "." & TextBox2.Text
        End If

        My.Settings.extensions.Add(TextBox2.Text.ToLower())
        My.Settings.extensions.Add(My.Settings.folders(ComboBox2.SelectedIndex))
        My.Settings.Save()
        My.Settings.Reload()

        refreshSettings()
    End Sub

    Private Sub checkColors()
        If PictureBox2.Enabled Then
            PictureBox2.Image = My.Resources.minus
        Else
            PictureBox2.Image = My.Resources.bwminus
        End If

        If PictureBox3.Enabled Then
            PictureBox3.Image = My.Resources.minus
        Else
            PictureBox3.Image = My.Resources.bwminus
        End If

        If PictureBox4.Enabled Then
            PictureBox4.Image = My.Resources.plus
        Else
            PictureBox4.Image = My.Resources.bwplus
        End If

        If PictureBox6.Enabled Then
            PictureBox6.Image = My.Resources.plus
        Else
            PictureBox6.Image = My.Resources.bwplus
        End If

        If PictureBox7.Enabled Then
            PictureBox7.Image = My.Resources.minus
        Else
            PictureBox7.Image = My.Resources.bwminus
        End If

        If PictureBox8.Enabled Then
            PictureBox8.Image = My.Resources.plus
        Else
            PictureBox8.Image = My.Resources.bwplus
        End If

        If PictureBox9.Enabled Then
            PictureBox9.Image = My.Resources.plus
        Else
            PictureBox9.Image = My.Resources.bwplus
        End If

        If PictureBox11.Enabled Then
            PictureBox11.Image = My.Resources.minus
        Else
            PictureBox11.Image = My.Resources.bwminus
        End If

        If PictureBox13.Enabled Then
            PictureBox13.Image = My.Resources.minus
        Else
            PictureBox13.Image = My.Resources.bwminus
        End If

        If PictureBox14.Enabled Then
            PictureBox14.Image = My.Resources.resetFol
        Else
            PictureBox14.Image = My.Resources.resetFolBW
        End If
    End Sub

    Private Sub ListView4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView4.SelectedIndexChanged
        PictureBox11.Enabled = False
        Label18.Enabled = False

        checkColors()

        If Not ListView4.SelectedItems.Count > 0 Then
            Return
        End If

        PictureBox11.Enabled = True
        Label18.Enabled = True

        checkColors()
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        removeWhitelisted()
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        removeWhitelisted()
    End Sub

    Private Sub removeWhitelisted()
        Dim index As Integer = ListView4.SelectedItems(0).Index
        My.Settings.whiteList.RemoveAt(index)
        My.Settings.Save()
        My.Settings.Reload()

        refreshSettings()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Dim ext As String = TextBox5.Text.ToLower

        PictureBox8.Enabled = False
        Label17.Enabled = False

        checkColors()

        If ext = "" Or ext = "." Then
            Return
        End If

        If Not ext.Substring(0, 1) = "." Then
            ext = "." & ext
        End If

        If My.Settings.whiteList.Contains(ext) Or My.Settings.extensions.Contains(ext) Then
            Return
        End If

        PictureBox8.Enabled = True
        Label17.Enabled = True

        checkColors()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        PictureBox6.Enabled = False
        Label11.Enabled = False

        checkColors()

        If My.Settings.folders.Contains(TextBox3.Text) Then
            Return
        End If

        PictureBox6.Enabled = True
        Label11.Enabled = True

        checkColors()
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        addWhitelist()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        addWhitelist()
    End Sub

    Private Sub addWhitelist()
        Dim ext As String = TextBox5.Text.ToLower

        If Not ext.Substring(0, 1) = "." Then
            ext = "." & ext
        End If

        My.Settings.whiteList.Add(ext)
        My.Settings.Save()
        My.Settings.Reload()

        refreshSettings()
    End Sub

    Private Sub ListView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView3.SelectedIndexChanged
        PictureBox7.Enabled = False
        Label14.Enabled = False
        PictureBox13.Enabled = False
        Label21.Enabled = False

        checkColors()

        If Not ListView3.SelectedItems.Count > 0 Then
            Return
        End If

        If My.Settings.folders(ListView3.SelectedItems(0).Index).Substring(0, 3) = "[R]" Then
            PictureBox7.Enabled = True
            Label14.Enabled = True
        Else
            PictureBox13.Enabled = True
            Label21.Enabled = True
        End If

        checkColors()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)
        browseCustom()
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs)
        browseCustom()
    End Sub

    Private Sub browseCustom()
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim pt As String = FolderBrowserDialog1.SelectedPath()

            If Directory.Exists(pt) Then
                My.Settings.folders(ListView3.SelectedItems(0).Index) = pt
                My.Settings.Save()
                My.Settings.Reload()
                ListView3.SelectedItems(0).Text = pt
            End If
        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        addFolder()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        addFolder()
    End Sub

    Private Sub addFolder()
        My.Settings.folders.Add("[R]" & TextBox3.Text)
        My.Settings.Save()
        My.Settings.Reload()
        refreshSettings()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        browseCustomAdd()
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        browseCustomAdd()
    End Sub

    Private Sub browseCustomAdd()
        TextBox6.Text = ""
        PictureBox9.Enabled = False
        Label12.Enabled = False

        checkColors()

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim pt As String = FolderBrowserDialog1.SelectedPath()
            If Directory.Exists(pt) And Not My.Settings.folders.Contains("[C]" & pt) Then
                TextBox6.Text = pt
                PictureBox9.Enabled = True
                Label12.Enabled = True

                checkColors()
            End If
        End If
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        addCustom()
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        addCustom()
    End Sub

    Private Sub addCustom()
        My.Settings.folders.Add("[C]" & TextBox6.Text)
        My.Settings.Save()
        My.Settings.Reload()
        refreshSettings()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        removeFilter()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        removeFilter()
    End Sub

    Private Sub removeFilter()
        My.Settings.extensions.RemoveAt((ListView1.SelectedItems(0).Index * 2) + 1)
        My.Settings.extensions.RemoveAt(ListView1.SelectedItems(0).Index * 2)
        My.Settings.Save()
        My.Settings.Reload()

        refreshSettings()
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        removeFolder()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        removeFolder()
    End Sub

    Private Sub removeFolder()
        If My.Settings.extensions.Contains(My.Settings.folders(ListView3.SelectedItems(0).Index)) Then
            MessageBox.Show("You must remove all the filters that include this sub folder.", "Can't Remove Sub Folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Not ListView3.SelectedItems(0).Text = "Other" And Not ListView3.SelectedItems(0).Text = "Folder" Then
            My.Settings.folders.RemoveAt(ListView3.SelectedItems(0).Index)
            My.Settings.Save()
            My.Settings.Reload()
            refreshSettings()
        Else
            MessageBox.Show("I'm sorry Dave, I'm afraid I can't do that", "Can't Remove Essential Sub Folder", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        removeCustomFolder()
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        removeCustomFolder()
    End Sub

    Private Sub removeCustomFolder()
        If My.Settings.extensions.Contains(My.Settings.folders(ListView3.SelectedItems(0).Index)) Then
            MessageBox.Show("You must remove all the filters that include this custom sub folder.", "Can't remove custom sub folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            My.Settings.folders.RemoveAt(ListView3.SelectedItems(0).Index)
            My.Settings.Save()
            My.Settings.Reload()
            refreshSettings()
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        My.Settings.closeExit = CheckBox6.Checked
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        resetFol()
    End Sub

    Private Sub PictureBox12_Click_1(sender As Object, e As EventArgs) Handles PictureBox12.Click
        resetFol()
    End Sub

    Private Sub resetFol()
        If MessageBox.Show("Are you sure you want to unorganize all the folders?", "You sure about dis?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
            Return
        End If

        For Each p As String In My.Settings.paths
            Essentials.Unorganize(p)
        Next

        MessageBox.Show("Done unorganizing.", "Unorganized", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim urlpath As String = Application.StartupPath
        Process.Start(urlpath & "\help.html")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start("https://drive.google.com/uc?export=download&id=1T5xD_3VWSJSETyWOtcCnzanM_2S4j8pD")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.latestver = checkForUpdates()
        My.Settings.Save()
        My.Settings.Reload()

        LVL.Text = My.Settings.latestver

        refreshSettings()
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        Essentials.Unorganize(My.Settings.paths(ListView2.SelectedItems(0).Index))
    End Sub

    Private Sub Label19_Click_1(sender As Object, e As EventArgs) Handles Label19.Click
        Essentials.Unorganize(My.Settings.paths(ListView2.SelectedItems(0).Index))
    End Sub
End Class