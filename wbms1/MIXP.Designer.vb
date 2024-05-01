<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MIXP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MIXP))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MIXGRID = New System.Windows.Forms.DataGridView()
        Me.PONO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ITEMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TKTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.b_ok = New System.Windows.Forms.Button()
        Me.b_cancel = New System.Windows.Forms.Button()
        Me.tb_netwt = New System.Windows.Forms.TextBox()
        Me.tb_sum = New System.Windows.Forms.TextBox()
        CType(Me.MIXGRID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MIXGRID
        '
        Me.MIXGRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MIXGRID.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PONO, Me.ITEMNO, Me.QTY, Me.DEL, Me.TKTNO})
        Me.MIXGRID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        resources.ApplyResources(Me.MIXGRID, "MIXGRID")
        Me.MIXGRID.Name = "MIXGRID"
        '
        'PONO
        '
        resources.ApplyResources(Me.PONO, "PONO")
        Me.PONO.Name = "PONO"
        '
        'ITEMNO
        '
        DataGridViewCellStyle1.NullValue = "0"
        Me.ITEMNO.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.ITEMNO, "ITEMNO")
        Me.ITEMNO.Name = "ITEMNO"
        '
        'QTY
        '
        DataGridViewCellStyle2.NullValue = "0"
        Me.QTY.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.QTY, "QTY")
        Me.QTY.Name = "QTY"
        '
        'DEL
        '
        resources.ApplyResources(Me.DEL, "DEL")
        Me.DEL.Name = "DEL"
        '
        'TKTNO
        '
        resources.ApplyResources(Me.TKTNO, "TKTNO")
        Me.TKTNO.Name = "TKTNO"
        '
        'b_ok
        '
        resources.ApplyResources(Me.b_ok, "b_ok")
        Me.b_ok.Name = "b_ok"
        Me.b_ok.UseVisualStyleBackColor = True
        '
        'b_cancel
        '
        resources.ApplyResources(Me.b_cancel, "b_cancel")
        Me.b_cancel.Name = "b_cancel"
        Me.b_cancel.UseVisualStyleBackColor = True
        '
        'tb_netwt
        '
        resources.ApplyResources(Me.tb_netwt, "tb_netwt")
        Me.tb_netwt.Name = "tb_netwt"
        '
        'tb_sum
        '
        resources.ApplyResources(Me.tb_sum, "tb_sum")
        Me.tb_sum.Name = "tb_sum"
        '
        'MIX
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tb_sum)
        Me.Controls.Add(Me.tb_netwt)
        Me.Controls.Add(Me.b_cancel)
        Me.Controls.Add(Me.b_ok)
        Me.Controls.Add(Me.MIXGRID)
        Me.Name = "MIX"
        CType(Me.MIXGRID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MIXGRID As System.Windows.Forms.DataGridView
    Friend WithEvents b_ok As System.Windows.Forms.Button
    Friend WithEvents b_cancel As System.Windows.Forms.Button
    Friend WithEvents tb_netwt As System.Windows.Forms.TextBox
    Friend WithEvents tb_sum As System.Windows.Forms.TextBox
    Friend WithEvents PONO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ITEMNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TKTNO As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
