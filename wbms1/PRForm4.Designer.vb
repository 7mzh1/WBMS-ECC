<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PRForm4
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
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.PRSECWT = New wbms1.PRSECWT()
        Me.PRSECWTTableAdapter = New wbms1.PRSECWTTableAdapters.PRSECWTTableAdapter()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PRSECWT, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(859, 262)
        Me.CrystalReportViewer1.TabIndex = 0
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "PRSECWT"
        Me.BindingSource1.DataSource = Me.PRSECWT
        '
        'PRSECWT
        '
        Me.PRSECWT.DataSetName = "PRSECWT"
        Me.PRSECWT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PRSECWTTableAdapter
        '
        Me.PRSECWTTableAdapter.ClearBeforeFill = True
        '
        'PRForm4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 262)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Name = "PRForm4"
        Me.Text = "Form4"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PRSECWT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents PRSECWT As wbms1.PRSECWT
    Friend WithEvents PRSECWTTableAdapter As wbms1.PRSECWTTableAdapters.PRSECWTTableAdapter
End Class
