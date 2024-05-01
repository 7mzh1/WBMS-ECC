<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EQPRINT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EQPRINT))
        Me.CrystalReportViewer2 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.EQGPRINT = New wbms1.EQGPRINT()
        Me.STWBMISTableAdapter1 = New wbms1.EQGPRINTTableAdapters.STWBMISTableAdapter()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EQGPRINT, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CrystalReportViewer2.Size = New System.Drawing.Size(945, 477)
        Me.CrystalReportViewer2.TabIndex = 0
        '
        'BindingSource2
        '
        Me.BindingSource2.DataMember = "STWBMIS"
        Me.BindingSource2.DataSource = Me.EQGPRINT
        '
        'EQGPRINT
        '
        Me.EQGPRINT.DataSetName = "EQGPRINT"
        Me.EQGPRINT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'STWBMISTableAdapter1
        '
        Me.STWBMISTableAdapter1.ClearBeforeFill = True
        '
        'ISPRINT
        '
        Me.ClientSize = New System.Drawing.Size(945, 477)
        Me.Controls.Add(Me.CrystalReportViewer2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ISPRINT"
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EQGPRINT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet1 As wbms1.DataSet1
    Friend WithEvents STWBMISTableAdapter As wbms1.DataSet1TableAdapters.STWBMISTableAdapter
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents DirectorySearcher1 As System.DirectoryServices.DirectorySearcher
    Friend WithEvents CrystalReportViewer2 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents EQGPRINT As wbms1.EQGPRINT
    Friend WithEvents STWBMISTableAdapter1 As wbms1.EQGPRINTTableAdapters.STWBMISTableAdapter
End Class
