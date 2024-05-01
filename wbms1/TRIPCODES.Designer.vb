<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tripcode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tripcode))
        Me.dgv_tr = New System.Windows.Forms.DataGridView()
        Me.EBELN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EBELP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LIFNR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MATNR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXZ01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WERKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LGORT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LEWED = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BTN = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.dgv_tr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_po
        '
        Me.dgv_tr.AllowUserToAddRows = False
        Me.dgv_tr.AllowUserToDeleteRows = False
        Me.dgv_tr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_tr.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EBELN, Me.EBELP, Me.LIFNR, Me.MATNR, Me.TXZ01, Me.WERKS, Me.LGORT, Me.LEWED, Me.BTN})
        Me.dgv_tr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_tr.Location = New System.Drawing.Point(0, 0)
        Me.dgv_tr.Name = "dgv_tr"
        Me.dgv_tr.ReadOnly = True
        Me.dgv_tr.Size = New System.Drawing.Size(1196, 312)
        Me.dgv_tr.TabIndex = 0
        '
        'EBELN
        '
        Me.EBELN.HeaderText = "Ref #"
        Me.EBELN.Name = "EBELN"
        Me.EBELN.ReadOnly = True
        '
        'EBELP
        '
        Me.EBELP.HeaderText = "Driver #"
        Me.EBELP.Name = "EBELP"
        Me.EBELP.ReadOnly = True
        '
        'LIFNR
        '
        Me.LIFNR.HeaderText = "Driver Name"
        Me.LIFNR.Name = "LIFNR"
        Me.LIFNR.ReadOnly = True
        '
        'MATNR
        '
        Me.MATNR.HeaderText = "Telephone"
        Me.MATNR.Name = "MATNR"
        Me.MATNR.ReadOnly = True
        '
        'TXZ01
        '
        Me.TXZ01.HeaderText = "Vehicle Plate"
        Me.TXZ01.Name = "TXZ01"
        Me.TXZ01.ReadOnly = True
        '
        'WERKS
        '
        Me.WERKS.HeaderText = "Asset"
        Me.WERKS.Name = "WERKS"
        Me.WERKS.ReadOnly = True
        '
        'LGORT
        '
        Me.LGORT.HeaderText = "Customer"
        Me.LGORT.Name = "LGORT"
        Me.LGORT.ReadOnly = True
        '
        'LEWED
        '
        Me.LEWED.HeaderText = "Material"
        Me.LEWED.Name = "LEWED"
        Me.LEWED.ReadOnly = True
        '
        'BTN
        '
        Me.BTN.HeaderText = "Select"
        Me.BTN.Name = "BTN"
        Me.BTN.ReadOnly = True
        '
        'tripcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1196, 312)
        Me.Controls.Add(Me.dgv_tr)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "tripcode"
        Me.Text = "Trip List"
        CType(Me.dgv_tr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_tr As System.Windows.Forms.DataGridView
    Friend WithEvents EBELN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EBELP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LIFNR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MATNR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TXZ01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WERKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LGORT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LEWED As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BTN As System.Windows.Forms.DataGridViewButtonColumn
End Class
