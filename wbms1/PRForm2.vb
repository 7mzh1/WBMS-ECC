Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports

Imports System.IO
Imports System.Net
Imports System.Net.Mail
Public Class PRForm2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.STWBMIBDS_PRTableAdapter.Fill(Me.DataSet4.STWBMIBDS_PR, glbvar.vintdocno, glbvar.divcd)
            Dim cr As New firstwtpr
            cr.SetDataSource(Me.DataSet4.Tables(0))
            Me.CrystalReportViewer1.ReportSource = cr
            Me.CrystalReportViewer1.RefreshReport()
            cr.PrintToPrinter(1, True, 1, 1)
            'Try
            '    Dim CrExportOptions As ExportOptions
            '    Dim CrDiskFileDestinationOptions As New  _
            '    DiskFileDestinationOptions()
            '    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            '    CrDiskFileDestinationOptions.DiskFileName = _
            '                                "F:\dd\crystalExport.pdf"
            '    CrExportOptions = cr.ExportOptions
            '    With CrExportOptions
            '        .ExportDestinationType = ExportDestinationType.DiskFile
            '        .ExportFormatType = ExportFormatType.PortableDocFormat
            '        .DestinationOptions = CrDiskFileDestinationOptions
            '        .FormatOptions = CrFormatTypeOptions
            '    End With
            '    cr.Export()
            'Catch ex As Exception
            '    MsgBox(ex.ToString)
            'End Try
            cr.Dispose()
            cr.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
End Class