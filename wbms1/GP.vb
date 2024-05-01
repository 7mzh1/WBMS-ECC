Public Class GP

    Private Sub First_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.STWBMIBDSTableAdapter.Fill(Me.D.STWBMIBDS, glbvar.vintdocno)

            Dim cr As New gatepass

            cr.SetDataSource(Me.D.Tables(0))

            Me.CrystalReportViewer1.ReportSource = cr

            Me.CrystalReportViewer1.RefreshReport()
            'cr.PrintToPrinter(1, True, 1, 1)
            'cr.Dispose()
            'cr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class