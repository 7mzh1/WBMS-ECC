<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class multi_itm_pr
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(multi_itm_pr))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.It_num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.itmcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.itmdes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fwt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.swt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tot_price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OMPRICE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.packded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.itmcd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itmnm = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.tb_sum = New System.Windows.Forms.TextBox()
        Me.tb_sumprice = New System.Windows.Forms.TextBox()
        Me.tb_totalded = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.Green
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.It_num, Me.itmcode, Me.itmdes, Me.pct, Me.Qty, Me.fwt, Me.swt, Me.tot_price, Me.OMPRICE, Me.price, Me.Ded, Me.packded})
        Me.DataGridView1.Location = New System.Drawing.Point(-4, 43)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1218, 294)
        Me.DataGridView1.TabIndex = 0
        '
        'It_num
        '
        Me.It_num.FillWeight = 52.08172!
        Me.It_num.HeaderText = "Item #"
        Me.It_num.Name = "It_num"
        '
        'itmcode
        '
        Me.itmcode.FillWeight = 138.941!
        Me.itmcode.HeaderText = "Item Code"
        Me.itmcode.Name = "itmcode"
        Me.itmcode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'itmdes
        '
        Me.itmdes.FillWeight = 263.5948!
        Me.itmdes.HeaderText = "Item Name"
        Me.itmdes.Name = "itmdes"
        '
        'pct
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N4"
        DataGridViewCellStyle1.NullValue = "0"
        Me.pct.DefaultCellStyle = DataGridViewCellStyle1
        Me.pct.FillWeight = 58.16162!
        Me.pct.HeaderText = "Percentage"
        Me.pct.Name = "pct"
        '
        'Qty
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = "0"
        Me.Qty.DefaultCellStyle = DataGridViewCellStyle2
        Me.Qty.FillWeight = 59.42938!
        Me.Qty.HeaderText = "Quantity"
        Me.Qty.Name = "Qty"
        '
        'fwt
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.NullValue = "0"
        Me.fwt.DefaultCellStyle = DataGridViewCellStyle3
        Me.fwt.FillWeight = 79.29382!
        Me.fwt.HeaderText = "First Weight"
        Me.fwt.Name = "fwt"
        '
        'swt
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = "0"
        Me.swt.DefaultCellStyle = DataGridViewCellStyle4
        Me.swt.FillWeight = 79.80517!
        Me.swt.HeaderText = "Second Weight"
        Me.swt.Name = "swt"
        '
        'tot_price
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.NullValue = "0"
        Me.tot_price.DefaultCellStyle = DataGridViewCellStyle5
        Me.tot_price.FillWeight = 59.71724!
        Me.tot_price.HeaderText = "Price List"
        Me.tot_price.Name = "tot_price"
        Me.tot_price.ReadOnly = True
        '
        'OMPRICE
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.NullValue = "0"
        Me.OMPRICE.DefaultCellStyle = DataGridViewCellStyle6
        Me.OMPRICE.FillWeight = 60.9562!
        Me.OMPRICE.HeaderText = "OM Price"
        Me.OMPRICE.Name = "OMPRICE"
        '
        'price
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.NullValue = "0"
        Me.price.DefaultCellStyle = DataGridViewCellStyle7
        Me.price.FillWeight = 60.54663!
        Me.price.HeaderText = "Rate"
        Me.price.Name = "price"
        '
        'Ded
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.NullValue = "0"
        Me.Ded.DefaultCellStyle = DataGridViewCellStyle8
        Me.Ded.FillWeight = 62.38294!
        Me.Ded.HeaderText = "Deduction"
        Me.Ded.Name = "Ded"
        '
        'packded
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.NullValue = "0"
        Me.packded.DefaultCellStyle = DataGridViewCellStyle9
        Me.packded.FillWeight = 63.65297!
        Me.packded.HeaderText = "Pack Deduction"
        Me.packded.Name = "packded"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.itmcd, Me.itmnm, Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(254, 112)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(499, 211)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'itmcd
        '
        Me.itmcd.Text = "Item Code"
        Me.itmcd.Width = 150
        '
        'itmnm
        '
        Me.itmnm.Text = "Item Name"
        Me.itmnm.Width = 350
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(436, 365)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(41, 23)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(195, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(519, 365)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tb_sum
        '
        Me.tb_sum.Location = New System.Drawing.Point(605, 368)
        Me.tb_sum.Name = "tb_sum"
        Me.tb_sum.Size = New System.Drawing.Size(148, 20)
        Me.tb_sum.TabIndex = 5
        Me.tb_sum.Text = "0"
        Me.tb_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tb_sumprice
        '
        Me.tb_sumprice.Location = New System.Drawing.Point(759, 368)
        Me.tb_sumprice.Name = "tb_sumprice"
        Me.tb_sumprice.Size = New System.Drawing.Size(148, 20)
        Me.tb_sumprice.TabIndex = 6
        Me.tb_sumprice.Text = "0"
        Me.tb_sumprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tb_totalded
        '
        Me.tb_totalded.Location = New System.Drawing.Point(913, 368)
        Me.tb_totalded.Name = "tb_totalded"
        Me.tb_totalded.Size = New System.Drawing.Size(148, 20)
        Me.tb_totalded.TabIndex = 7
        Me.tb_totalded.Text = "0"
        Me.tb_totalded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(638, 347)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Total Quantity"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(799, 347)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Total Price"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(938, 347)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Quantity Deducted"
        '
        'multi_itm_pr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1226, 411)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tb_totalded)
        Me.Controls.Add(Me.tb_sumprice)
        Me.Controls.Add(Me.tb_sum)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "multi_itm_pr"
        Me.Text = "Multiple Items"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents itmcd As System.Windows.Forms.ColumnHeader
    Friend WithEvents itmnm As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents tb_sum As System.Windows.Forms.TextBox
    Friend WithEvents tb_sumprice As System.Windows.Forms.TextBox
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents tb_totalded As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents It_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itmcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itmdes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fwt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents swt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tot_price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OMPRICE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ded As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents packded As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
