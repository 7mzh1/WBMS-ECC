Public Class First

    Private Sub First_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DSBIGSCALETableAdapter.Fill(Me.D.DSBIGSCALEDataTable, glbvar.vintdocno)


        Dim cr As New firstwt

        cr.SetDataSource(Me.D.Tables(0))

        Me.CrystalReportViewer1.ReportSource = cr

        Me.CrystalReportViewer1.RefreshReport()
        cr.PrintToPrinter(1, True, 1, 1)
        cr.Dispose()
        cr.Close()
    End Sub
End Class