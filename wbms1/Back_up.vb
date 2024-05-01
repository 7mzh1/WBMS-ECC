Public Class Back_up

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a = New TextBox
        Dim b = New TextBox
        a.Text = Today.Date.Day & "_" & Today.Date.Month & "_" & Today.Date.Year
        b.Text = TimeOfDay.Hour & "_" & TimeOfDay.Minute & "_" & TimeOfDay.Second
        Shell("exp userid=accts/accts@xe full=n OBJECT_CONSISTENT=Y file=C:\Backxe\alq_" & a.Text & "_" & b.Text & ".dmp log=C:\Backxe\alq_" & a.Text & "_" & b.Text & ".log")
    End Sub
End Class