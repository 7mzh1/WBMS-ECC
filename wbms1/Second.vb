Public Class Second

    Private Sub Second_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DSBIGSCALETableAdapter.Fill(Me.DSBIGSCALE.DSBIGSCALEDataTable, glbvar.vintdocno)
        Dim cr As New secondwt
        cr.SetDataSource(Me.DSBIGSCALE.Tables(0))
        Me.CrystalReportViewer1.ReportSource = cr
        Me.CrystalReportViewer1.RefreshReport()
        cr.PrintToPrinter(1, True, 1, 1)

        cr.Dispose()
        cr.Close()
    End Sub
End Class