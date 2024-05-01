Public Class REPFRM

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet2.STWBMIBDS_REP' table. You can move, or remove it, as needed.
        Me.STWBMIBDS_REPTableAdapter.Fill(Me.DataSet2.STWBMIBDS_REP)


        Dim cr As New REPBIG

        cr.SetDataSource(Me.DataSet2.Tables(0))

        Me.CrystalReportViewer1.ReportSource = cr

        Me.CrystalReportViewer1.RefreshReport()
        cr.PrintToPrinter(1, True, 1, 1)
        cr.Dispose()
        cr.Close()
    End Sub
End Class