<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class First
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(First))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.D = New wbms1.DSBIGSCALE()
        Me.DSBIGSCALETableAdapter = New wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSBIGSCALE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.CachedPageNumberPerDoc = 10
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(689, 339)
        Me.CrystalReportViewer1.TabIndex = 0
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "DSBIGSCALE"
        Me.BindingSource1.DataSource = Me.DSBIGSCALE
        '
        'DSBIGSCALE
        '
        Me.DSBIGSCALE.DataSetName = "DSBIGSCALE"
        Me.DSBIGSCALE.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DSBIGSCALETableAdapter
        '
        Me.DSBIGSCALETableAdapter.ClearBeforeFill = True
        '
        'First
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 339)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "First"
        Me.Text = "First Weight"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSBIGSCALE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents D As wbms1.DSBIGSCALE
    Friend WithEvents STWBMIBDSTableAdapter As wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter
    Friend WithEvents DSBIGSCALE As wbms1.DSBIGSCALE
    Friend WithEvents DSBIGSCALETableAdapter As wbms1.DSBIGSCALETableAdapters.DSBIGSCALETableAdapter
End Class
