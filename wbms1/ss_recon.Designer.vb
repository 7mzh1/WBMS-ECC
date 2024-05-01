<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ss_recon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ss_recon))
        Me.DGV_SML = New System.Windows.Forms.DataGridView()
        Me.b_display = New System.Windows.Forms.Button()
        Me.d_postdate = New System.Windows.Forms.DateTimePicker()
        Me.b_exit = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.itmcd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itmnm = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LUOM = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.b_add = New System.Windows.Forms.Button()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.b_save = New System.Windows.Forms.Button()
        Me.b_post = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.i_d = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mesage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.itemcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.itemdesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.post_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.purqty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pur_ded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.salqty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.salded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Recon_qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Recon_ded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trans_mat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Transfer_mat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Transfer_qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btrans_mat = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.btrans_loc = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.docno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGV_SML, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_SML
        '
        Me.DGV_SML.AllowUserToAddRows = False
        Me.DGV_SML.AllowUserToDeleteRows = False
        Me.DGV_SML.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_SML.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.itemcode, Me.itemdesc, Me.post_date, Me.purqty, Me.pur_ded, Me.salqty, Me.salded, Me.Recon_qty, Me.Recon_ded, Me.Diff, Me.trans_mat, Me.Transfer_mat, Me.Transfer_qty, Me.btrans_mat, Me.btrans_loc, Me.docno})
        Me.DGV_SML.Location = New System.Drawing.Point(35, 63)
        Me.DGV_SML.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_SML.Name = "DGV_SML"
        Me.DGV_SML.RowHeadersWidth = 51
        Me.DGV_SML.Size = New System.Drawing.Size(1512, 569)
        Me.DGV_SML.TabIndex = 0
        '
        'b_display
        '
        Me.b_display.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_display.Location = New System.Drawing.Point(817, 33)
        Me.b_display.Margin = New System.Windows.Forms.Padding(4)
        Me.b_display.Name = "b_display"
        Me.b_display.Size = New System.Drawing.Size(100, 28)
        Me.b_display.TabIndex = 1
        Me.b_display.Text = "Display"
        Me.b_display.UseVisualStyleBackColor = True
        '
        'd_postdate
        '
        Me.d_postdate.Location = New System.Drawing.Point(543, 33)
        Me.d_postdate.Margin = New System.Windows.Forms.Padding(4)
        Me.d_postdate.Name = "d_postdate"
        Me.d_postdate.Size = New System.Drawing.Size(265, 22)
        Me.d_postdate.TabIndex = 2
        '
        'b_exit
        '
        Me.b_exit.BackColor = System.Drawing.Color.Gainsboro
        Me.b_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.b_exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_exit.Location = New System.Drawing.Point(1191, 33)
        Me.b_exit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_exit.Name = "b_exit"
        Me.b_exit.Size = New System.Drawing.Size(115, 28)
        Me.b_exit.TabIndex = 95
        Me.b_exit.Text = "Close"
        Me.b_exit.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.LightBlue
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.itmcd, Me.itmnm, Me.LUOM})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(767, 208)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(665, 259)
        Me.ListView1.TabIndex = 96
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'itmcd
        '
        Me.itmcd.Text = "Item Code"
        Me.itmcd.Width = 170
        '
        'itmnm
        '
        Me.itmnm.Text = "Item Name"
        Me.itmnm.Width = 325
        '
        'b_add
        '
        Me.b_add.BackColor = System.Drawing.Color.Gainsboro
        Me.b_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.b_add.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_add.Location = New System.Drawing.Point(924, 33)
        Me.b_add.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_add.Name = "b_add"
        Me.b_add.Size = New System.Drawing.Size(115, 28)
        Me.b_add.TabIndex = 97
        Me.b_add.Text = "Add"
        Me.b_add.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.BackColor = System.Drawing.Color.LightBlue
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(73, 208)
        Me.ListView2.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(665, 259)
        Me.ListView2.TabIndex = 98
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        Me.ListView2.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Item Code"
        Me.ColumnHeader1.Width = 170
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Item Name"
        Me.ColumnHeader2.Width = 325
        '
        'b_save
        '
        Me.b_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.b_save.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_save.Location = New System.Drawing.Point(1044, 33)
        Me.b_save.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_save.Name = "b_save"
        Me.b_save.Size = New System.Drawing.Size(141, 28)
        Me.b_save.TabIndex = 99
        Me.b_save.Text = "Save"
        Me.b_save.UseVisualStyleBackColor = True
        '
        'b_post
        '
        Me.b_post.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.b_post.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_post.Location = New System.Drawing.Point(1311, 33)
        Me.b_post.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_post.Name = "b_post"
        Me.b_post.Size = New System.Drawing.Size(141, 28)
        Me.b_post.TabIndex = 100
        Me.b_post.Text = "Post"
        Me.b_post.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.type, Me.i_d, Me.Number, Me.Mesage})
        Me.DataGridView1.Location = New System.Drawing.Point(974, 645)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(599, 100)
        Me.DataGridView1.TabIndex = 136
        '
        'type
        '
        Me.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.type.HeaderText = "Typed"
        Me.type.MinimumWidth = 6
        Me.type.Name = "type"
        Me.type.Width = 77
        '
        'i_d
        '
        Me.i_d.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.i_d.HeaderText = "Id"
        Me.i_d.MinimumWidth = 6
        Me.i_d.Name = "i_d"
        Me.i_d.Width = 48
        '
        'Number
        '
        Me.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Number.HeaderText = "Number"
        Me.Number.MinimumWidth = 6
        Me.Number.Name = "Number"
        Me.Number.Width = 87
        '
        'Mesage
        '
        Me.Mesage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Mesage.HeaderText = "Message"
        Me.Mesage.MinimumWidth = 6
        Me.Mesage.Name = "Mesage"
        Me.Mesage.Width = 94
        '
        'itemcode
        '
        Me.itemcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.itemcode.FillWeight = 50.0!
        Me.itemcode.Frozen = True
        Me.itemcode.HeaderText = "Mat #"
        Me.itemcode.MinimumWidth = 6
        Me.itemcode.Name = "itemcode"
        Me.itemcode.Width = 125
        '
        'itemdesc
        '
        Me.itemdesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.itemdesc.Frozen = True
        Me.itemdesc.HeaderText = "Material"
        Me.itemdesc.MinimumWidth = 6
        Me.itemdesc.Name = "itemdesc"
        Me.itemdesc.Width = 125
        '
        'post_date
        '
        Me.post_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.post_date.FillWeight = 50.0!
        Me.post_date.Frozen = True
        Me.post_date.HeaderText = "Date"
        Me.post_date.MinimumWidth = 6
        Me.post_date.Name = "post_date"
        Me.post_date.ReadOnly = True
        Me.post_date.Width = 80
        '
        'purqty
        '
        Me.purqty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.purqty.FillWeight = 25.0!
        Me.purqty.Frozen = True
        Me.purqty.HeaderText = "Pur Qty"
        Me.purqty.MinimumWidth = 6
        Me.purqty.Name = "purqty"
        Me.purqty.ReadOnly = True
        Me.purqty.Width = 78
        '
        'pur_ded
        '
        Me.pur_ded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.pur_ded.FillWeight = 25.0!
        Me.pur_ded.Frozen = True
        Me.pur_ded.HeaderText = "Pur Deduct"
        Me.pur_ded.MinimumWidth = 6
        Me.pur_ded.Name = "pur_ded"
        Me.pur_ded.ReadOnly = True
        Me.pur_ded.Width = 77
        '
        'salqty
        '
        Me.salqty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.salqty.FillWeight = 25.0!
        Me.salqty.Frozen = True
        Me.salqty.HeaderText = "Sold Qty"
        Me.salqty.MinimumWidth = 6
        Me.salqty.Name = "salqty"
        Me.salqty.ReadOnly = True
        Me.salqty.Width = 77
        '
        'salded
        '
        Me.salded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.salded.FillWeight = 25.0!
        Me.salded.Frozen = True
        Me.salded.HeaderText = "Sale Deduct"
        Me.salded.MinimumWidth = 6
        Me.salded.Name = "salded"
        Me.salded.ReadOnly = True
        Me.salded.Width = 77
        '
        'Recon_qty
        '
        Me.Recon_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Recon_qty.FillWeight = 25.0!
        Me.Recon_qty.Frozen = True
        Me.Recon_qty.HeaderText = "Recon Qty"
        Me.Recon_qty.MinimumWidth = 6
        Me.Recon_qty.Name = "Recon_qty"
        Me.Recon_qty.ReadOnly = True
        Me.Recon_qty.Width = 78
        '
        'Recon_ded
        '
        Me.Recon_ded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Recon_ded.FillWeight = 25.0!
        Me.Recon_ded.Frozen = True
        Me.Recon_ded.HeaderText = "Recon Ded"
        Me.Recon_ded.MinimumWidth = 6
        Me.Recon_ded.Name = "Recon_ded"
        Me.Recon_ded.ReadOnly = True
        Me.Recon_ded.Width = 77
        '
        'Diff
        '
        Me.Diff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Diff.FillWeight = 25.0!
        Me.Diff.Frozen = True
        Me.Diff.HeaderText = "Difference"
        Me.Diff.MinimumWidth = 6
        Me.Diff.Name = "Diff"
        Me.Diff.ReadOnly = True
        Me.Diff.Width = 77
        '
        'trans_mat
        '
        Me.trans_mat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.trans_mat.FillWeight = 50.0!
        Me.trans_mat.Frozen = True
        Me.trans_mat.HeaderText = "Transfer Mat#"
        Me.trans_mat.MinimumWidth = 6
        Me.trans_mat.Name = "trans_mat"
        Me.trans_mat.Width = 10
        '
        'Transfer_mat
        '
        Me.Transfer_mat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Transfer_mat.Frozen = True
        Me.Transfer_mat.HeaderText = "Transfer Mat"
        Me.Transfer_mat.MinimumWidth = 6
        Me.Transfer_mat.Name = "Transfer_mat"
        Me.Transfer_mat.Width = 10
        '
        'Transfer_qty
        '
        Me.Transfer_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Transfer_qty.FillWeight = 25.0!
        Me.Transfer_qty.Frozen = True
        Me.Transfer_qty.HeaderText = "Transfer Qty"
        Me.Transfer_qty.MinimumWidth = 6
        Me.Transfer_qty.Name = "Transfer_qty"
        Me.Transfer_qty.Width = 77
        '
        'btrans_mat
        '
        Me.btrans_mat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.btrans_mat.FillWeight = 20.0!
        Me.btrans_mat.HeaderText = "Transfer Material"
        Me.btrans_mat.MinimumWidth = 6
        Me.btrans_mat.Name = "btrans_mat"
        Me.btrans_mat.Width = 62
        '
        'btrans_loc
        '
        Me.btrans_loc.FillWeight = 50.0!
        Me.btrans_loc.HeaderText = "Location Transfer"
        Me.btrans_loc.MinimumWidth = 6
        Me.btrans_loc.Name = "btrans_loc"
        Me.btrans_loc.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.btrans_loc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.btrans_loc.Width = 75
        '
        'docno
        '
        Me.docno.HeaderText = "Doc #"
        Me.docno.MinimumWidth = 6
        Me.docno.Name = "docno"
        Me.docno.ReadOnly = True
        Me.docno.Width = 80
        '
        'ss_recon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 753)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.b_post)
        Me.Controls.Add(Me.b_save)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.b_add)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.b_exit)
        Me.Controls.Add(Me.d_postdate)
        Me.Controls.Add(Me.b_display)
        Me.Controls.Add(Me.DGV_SML)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ss_recon"
        Me.Text = "Small Scale Recon"
        CType(Me.DGV_SML, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_SML As System.Windows.Forms.DataGridView
    Friend WithEvents b_display As System.Windows.Forms.Button
    Friend WithEvents d_postdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents b_exit As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents itmcd As System.Windows.Forms.ColumnHeader
    Friend WithEvents itmnm As System.Windows.Forms.ColumnHeader
    Friend WithEvents LUOM As System.Windows.Forms.ColumnHeader
    Friend WithEvents b_add As System.Windows.Forms.Button
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents b_save As System.Windows.Forms.Button
    Friend WithEvents b_post As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents i_d As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mesage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itemcode As DataGridViewTextBoxColumn
    Friend WithEvents itemdesc As DataGridViewTextBoxColumn
    Friend WithEvents post_date As DataGridViewTextBoxColumn
    Friend WithEvents purqty As DataGridViewTextBoxColumn
    Friend WithEvents pur_ded As DataGridViewTextBoxColumn
    Friend WithEvents salqty As DataGridViewTextBoxColumn
    Friend WithEvents salded As DataGridViewTextBoxColumn
    Friend WithEvents Recon_qty As DataGridViewTextBoxColumn
    Friend WithEvents Recon_ded As DataGridViewTextBoxColumn
    Friend WithEvents Diff As DataGridViewTextBoxColumn
    Friend WithEvents trans_mat As DataGridViewTextBoxColumn
    Friend WithEvents Transfer_mat As DataGridViewTextBoxColumn
    Friend WithEvents Transfer_qty As DataGridViewTextBoxColumn
    Friend WithEvents btrans_mat As DataGridViewButtonColumn
    Friend WithEvents btrans_loc As DataGridViewButtonColumn
    Friend WithEvents docno As DataGridViewTextBoxColumn
End Class
