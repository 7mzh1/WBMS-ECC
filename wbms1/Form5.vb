Public Class Form5

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            Me.STWBMISTableAdapter.Fill(Me.DataSet2.STWBMIS, glbvar.vintdocno)

            Dim cr As New isdisplay

            cr.SetDataSource(Me.DataSet2.Tables(0))

            Me.CrystalReportViewer1.ReportSource = cr

            Me.CrystalReportViewer1.RefreshReport()
            'cr.PrintToPrinter(1, True, 1, 1)
            cr.Dispose()
            cr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class