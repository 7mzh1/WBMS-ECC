<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Second
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.CrystalReportViewer2 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSBIGSCALE1 = New wbms1.DSBIGSCALE()
        Me.DSBIGSCALETableAdapter = New wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSBIGSCALE1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer2
        '
        Me.CrystalReportViewer2.ActiveViewIndex = -1
        Me.CrystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer2.CachedPageNumberPerDoc = 10
        Me.CrystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer2.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer2.Name = "CrystalReportViewer2"
        Me.CrystalReportViewer2.Size = New System.Drawing.Size(747, 400)
        Me.CrystalReportViewer2.TabIndex = 0
        '
        'BindingSource2
        '
        Me.BindingSource2.DataMember = "DSBIGSCALE"
        Me.BindingSource2.DataSource = Me.DSBIGSCALE1
        '
        'DSBIGSCALE1
        '
        Me.DSBIGSCALE1.DataSetName = "DSBIGSCALE"
        Me.DSBIGSCALE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DSBIGSCALETableAdapter
        '
        Me.DSBIGSCALETableAdapter.ClearBeforeFill = True
        '
        'Second
        '
        Me.ClientSize = New System.Drawing.Size(747, 400)
        Me.Controls.Add(Me.CrystalReportViewer2)
        Me.Name = "Second"
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSBIGSCALE1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DSBIGSCALE As wbms1.DSBIGSCALE
    Friend WithEvents STWBMIBDSTableAdapter As wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter
    Friend WithEvents CrystalReportViewer2 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents DSBIGSCALE1 As wbms1.DSBIGSCALE
    Friend WithEvents DSBIGSCALETableAdapter As wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter
End Class
