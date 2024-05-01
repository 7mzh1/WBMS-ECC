Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports

Imports System.IO
Imports System.Net
Imports System.Net.Mail
Public Class FARAB1

    Private Sub FARAB1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataTable1TableAdapter.Fill(Me.DataSet3.DataTable1, glbvar.vintdocno, glbvar.divcd)
        Dim cr As New lfirstwt
        cr.SetDataSource(Me.DataSet3.Tables(0))
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

    End Sub
End Class