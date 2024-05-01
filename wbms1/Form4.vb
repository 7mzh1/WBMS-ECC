Public Class Form4

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataTable1TableAdapter.Fill(Me.DataSet3.DataTable1, glbvar.vintdocno, glbvar.divcd)
        Dim cr As New secondwt
        cr.SetDataSource(Me.DataSet3.Tables(0))
        Me.CrystalReportViewer1.ReportSource = cr
        Me.CrystalReportViewer1.RefreshReport()
        cr.PrintToPrinter(1, True, 1, 1)

        cr.Dispose()
        cr.Close()
    End Sub
End Class