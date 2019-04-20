<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.TitleBar = New System.Windows.Forms.Panel()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.AllButton = New System.Windows.Forms.PictureBox()
        Me.AutoOrgButton = New System.Windows.Forms.PictureBox()
        Me.AutoOrgLabel = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TitleBar.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AllButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AutoOrgButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBar
        '
        Me.TitleBar.BackColor = System.Drawing.Color.White
        Me.TitleBar.Controls.Add(Me.CloseButton)
        Me.TitleBar.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.TitleBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar.Name = "TitleBar"
        Me.TitleBar.Size = New System.Drawing.Size(318, 29)
        Me.TitleBar.TabIndex = 0
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.BackgroundImage = Global.Sabertooth.My.Resources.Resources.close
        Me.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CloseButton.FlatAppearance.BorderSize = 0
        Me.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseButton.Location = New System.Drawing.Point(290, 2)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(25, 25)
        Me.CloseButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.CloseButton, "Close")
        Me.CloseButton.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Sabertooth.My.Resources.Resources.sabertooth_ICON1
        Me.PictureBox1.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "RAWR")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(54, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 32)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Sabertooth"
        Me.ToolTip1.SetToolTip(Me.Label1, "Her name is actually BIDIK")
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImage = Global.Sabertooth.My.Resources.Resources.settings
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(12, 75)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 60)
        Me.Button1.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.Button1, "Settings")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImage = Global.Sabertooth.My.Resources.Resources.plus
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(12, 215)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 60)
        Me.Button2.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.Button2, "Add Folder to Organize")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'AllButton
        '
        Me.AllButton.BackColor = System.Drawing.Color.Transparent
        Me.AllButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AllButton.Image = Global.Sabertooth.My.Resources.Resources.folder
        Me.AllButton.Location = New System.Drawing.Point(106, 75)
        Me.AllButton.Name = "AllButton"
        Me.AllButton.Size = New System.Drawing.Size(200, 200)
        Me.AllButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.AllButton.TabIndex = 1
        Me.AllButton.TabStop = False
        '
        'AutoOrgButton
        '
        Me.AutoOrgButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AutoOrgButton.Image = Global.Sabertooth.My.Resources.Resources.on_pic
        Me.AutoOrgButton.Location = New System.Drawing.Point(254, 329)
        Me.AutoOrgButton.Name = "AutoOrgButton"
        Me.AutoOrgButton.Size = New System.Drawing.Size(52, 44)
        Me.AutoOrgButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.AutoOrgButton.TabIndex = 4
        Me.AutoOrgButton.TabStop = False
        '
        'AutoOrgLabel
        '
        Me.AutoOrgLabel.AutoSize = True
        Me.AutoOrgLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AutoOrgLabel.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoOrgLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.AutoOrgLabel.Location = New System.Drawing.Point(9, 342)
        Me.AutoOrgLabel.Name = "AutoOrgLabel"
        Me.AutoOrgLabel.Size = New System.Drawing.Size(276, 18)
        Me.AutoOrgLabel.TabIndex = 5
        Me.AutoOrgLabel.Text = "Auto-Organizer                        "
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Sabertooth"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'Timer1
        '
        Me.Timer1.Interval = 30000
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 300)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(318, 10)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 8
        '
        'MainForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(318, 393)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AutoOrgButton)
        Me.Controls.Add(Me.AllButton)
        Me.Controls.Add(Me.TitleBar)
        Me.Controls.Add(Me.AutoOrgLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sabertooth"
        Me.TitleBar.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AllButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AutoOrgButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label1 As Label
    Friend WithEvents AllButton As PictureBox
    Friend WithEvents AutoOrgButton As PictureBox
    Friend WithEvents AutoOrgLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Button2 As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
