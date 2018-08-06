<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TrayForm
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OrgItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoOrgItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Separator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Separator0 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenItem, Me.Separator0, Me.OrgItem, Me.AutoOrgItem, Me.Separator1, Me.ExitItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(233, 126)
        '
        'OrgItem
        '
        Me.OrgItem.Image = Global.Sabertooth.My.Resources.Resources.folder
        Me.OrgItem.Name = "OrgItem"
        Me.OrgItem.Size = New System.Drawing.Size(232, 22)
        Me.OrgItem.Text = "Organize"
        '
        'AutoOrgItem
        '
        Me.AutoOrgItem.Image = Global.Sabertooth.My.Resources.Resources.off_pic
        Me.AutoOrgItem.Name = "AutoOrgItem"
        Me.AutoOrgItem.Size = New System.Drawing.Size(232, 22)
        Me.AutoOrgItem.Text = "Turn Auto Organizer ON"
        '
        'Separator1
        '
        Me.Separator1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(229, 6)
        '
        'ExitItem
        '
        Me.ExitItem.Image = Global.Sabertooth.My.Resources.Resources.close
        Me.ExitItem.Name = "ExitItem"
        Me.ExitItem.Size = New System.Drawing.Size(232, 22)
        Me.ExitItem.Text = "Quit Sabertooth"
        '
        'OpenItem
        '
        Me.OpenItem.Image = Global.Sabertooth.My.Resources.Resources.sabertooth_ICON1
        Me.OpenItem.Name = "OpenItem"
        Me.OpenItem.Size = New System.Drawing.Size(232, 22)
        Me.OpenItem.Text = "Open Sabertooth"
        '
        'Separator0
        '
        Me.Separator0.Name = "Separator0"
        Me.Separator0.Size = New System.Drawing.Size(229, 6)
        '
        'TrayForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(169, 66)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TrayForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents OrgItem As ToolStripMenuItem
    Friend WithEvents ExitItem As ToolStripMenuItem
    Friend WithEvents AutoOrgItem As ToolStripMenuItem
    Friend WithEvents Separator1 As ToolStripSeparator
    Friend WithEvents OpenItem As ToolStripMenuItem
    Friend WithEvents Separator0 As ToolStripSeparator
End Class
