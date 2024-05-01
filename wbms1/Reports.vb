Public Class Reports

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        REPFRM.Show()
    End Sub

    Private Sub Reports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        usermenu.Show()
        Me.Close()
    End Sub

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class