Public Class Form1
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        My.Settings.Save()
        Application.Exit()
    End Sub
End Class
