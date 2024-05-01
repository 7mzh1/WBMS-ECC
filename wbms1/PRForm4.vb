Public Class PRForm4

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.PRSECWTTableAdapter.Fill(Me.PRSECWT._PRSECWT, glbvar.vintdocno, glbvar.divcd)
            Dim cr As New secondwtpr
            cr.SetDataSource(Me.PRSECWT.Tables(0))
            Me.CrystalReportViewer1.ReportSource = cr
            Me.CrystalReportViewer1.RefreshReport()
            cr.PrintToPrinter(1, True, 1, 1)
            cr.Dispose()
            cr.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
End Class