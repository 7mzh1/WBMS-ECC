<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdgv
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fdgv))
        Me.dgv_po = New System.Windows.Forms.DataGridView()
        Me.EBELN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EBELP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LIFNR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MATNR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXZ01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WERKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LGORT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LEWED = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MENGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RMENGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BTN = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.dgv_po, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_po
        '
        Me.dgv_po.AllowUserToAddRows = False
        Me.dgv_po.AllowUserToDeleteRows = False
        Me.dgv_po.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_po.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EBELN, Me.EBELP, Me.LIFNR, Me.MATNR, Me.TXZ01, Me.WERKS, Me.LGORT, Me.LEWED, Me.MENGE, Me.RMENGE, Me.BAL, Me.BTN})
        Me.dgv_po.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_po.Location = New System.Drawing.Point(0, 0)
        Me.dgv_po.Name = "dgv_po"
        Me.dgv_po.ReadOnly = True
        Me.dgv_po.Size = New System.Drawing.Size(1208, 312)
        Me.dgv_po.TabIndex = 0
        '
        'EBELN
        '
        Me.EBELN.HeaderText = "PO"
        Me.EBELN.Name = "EBELN"
        Me.EBELN.ReadOnly = True
        '
        'EBELP
        '
        Me.EBELP.HeaderText = "Line #"
        Me.EBELP.Name = "EBELP"
        Me.EBELP.ReadOnly = True
        '
        'LIFNR
        '
        Me.LIFNR.HeaderText = "Vendor"
        Me.LIFNR.Name = "LIFNR"
        Me.LIFNR.ReadOnly = True
        '
        'MATNR
        '
        Me.MATNR.HeaderText = "Material"
        Me.MATNR.Name = "MATNR"
        Me.MATNR.ReadOnly = True
        '
        'TXZ01
        '
        Me.TXZ01.HeaderText = "Name"
        Me.TXZ01.Name = "TXZ01"
        Me.TXZ01.ReadOnly = True
        '
        'WERKS
        '
        Me.WERKS.HeaderText = "Branch"
        Me.WERKS.Name = "WERKS"
        Me.WERKS.ReadOnly = True
        '
        'LGORT
        '
        Me.LGORT.HeaderText = "Location"
        Me.LGORT.Name = "LGORT"
        Me.LGORT.ReadOnly = True
        '
        'LEWED
        '
        Me.LEWED.HeaderText = "Last Date"
        Me.LEWED.Name = "LEWED"
        Me.LEWED.ReadOnly = True
        '
        'MENGE
        '
        Me.MENGE.HeaderText = "Qty"
        Me.MENGE.Name = "MENGE"
        Me.MENGE.ReadOnly = True
        '
        'RMENGE
        '
        Me.RMENGE.HeaderText = "GR Qty"
        Me.RMENGE.Name = "RMENGE"
        Me.RMENGE.ReadOnly = True
        '
        'BAL
        '
        Me.BAL.HeaderText = "Balance"
        Me.BAL.Name = "BAL"
        Me.BAL.ReadOnly = True
        '
        'BTN
        '
        Me.BTN.HeaderText = "Select"
        Me.BTN.Name = "BTN"
        Me.BTN.ReadOnly = True
        '
        'fdgv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 312)
        Me.Controls.Add(Me.dgv_po)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "fdgv"
        Me.Text = "PO List"
        CType(Me.dgv_po, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_po As System.Windows.Forms.DataGridView
    Friend WithEvents EBELN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EBELP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LIFNR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MATNR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TXZ01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WERKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LGORT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LEWED As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MENGE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RMENGE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BTN As System.Windows.Forms.DataGridViewButtonColumn
End Class
