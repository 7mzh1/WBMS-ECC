Public Class EQPRINT
    Private Sub ISPRINT_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'TODO: This line of code loads data into the 'EQGPRINT.STWBMIS' table. You can move, or remove it, as needed.
            Me.STWBMISTableAdapter1.Fill(Me.EQGPRINT.STWBMIS, glbvar.vintdocno)
            Dim cr As New eqgprint1

            cr.SetDataSource(Me.EQGPRINT.Tables(0))

            Me.CrystalReportViewer2.ReportSource = cr

            Me.CrystalReportViewer2.RefreshReport()
            'cr.PrintToPrinter(1, True, 1, 1)
            cr.Dispose()
            cr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class