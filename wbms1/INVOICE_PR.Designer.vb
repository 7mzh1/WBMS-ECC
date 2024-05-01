<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class INVOICE_PR
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(INVOICE_PR))
        Me.INVGRID = New System.Windows.Forms.DataGridView()
        Me.selectitem = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.INVSLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SCALE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INTDOCNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ticketno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sledcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sleddesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ITEMcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.itemdesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dateout = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.firstqty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Secondqty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.priceton = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vbelns = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vbelnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vbelni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INVDOCNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POST_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trcharge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.penalty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.machcharge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.labcharge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.b_display = New System.Windows.Forms.Button()
        Me.b_save = New System.Windows.Forms.Button()
        Me.tb_docno = New System.Windows.Forms.TextBox()
        Me.b_searchdoc = New System.Windows.Forms.Button()
        Me.tb_rdocno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.b_gen = New System.Windows.Forms.Button()
        Me.tb_searchbyno = New System.Windows.Forms.TextBox()
        Me.cb_sledcode = New System.Windows.Forms.ComboBox()
        Me.tb_sledesc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.loadven = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.b_crinv = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.i_d = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mesage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tb_vbelni = New System.Windows.Forms.TextBox()
        Me.b_exit = New System.Windows.Forms.Button()
        Me.d_date = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Tb_eqpchrgs = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Tb_labourcharges = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Tb_penalty = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Tb_transp = New System.Windows.Forms.TextBox()
        CType(Me.INVGRID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INVGRID
        '
        Me.INVGRID.AllowUserToAddRows = False
        Me.INVGRID.AllowUserToDeleteRows = False
        Me.INVGRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.INVGRID.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.selectitem, Me.INVSLNO, Me.SCALE, Me.INTDOCNO, Me.Ticketno, Me.sledcode, Me.sleddesc, Me.SLNO, Me.ITEMcode, Me.itemdesc, Me.dateout, Me.firstqty, Me.Secondqty, Me.QTY, Me.priceton, Me.rate, Me.Total_price, Me.vbelns, Me.vbelnd, Me.vbelni, Me.INVDOCNO, Me.POST_DATE, Me.trcharge, Me.penalty, Me.machcharge, Me.labcharge})
        Me.INVGRID.Location = New System.Drawing.Point(12, 294)
        Me.INVGRID.Name = "INVGRID"
        Me.INVGRID.Size = New System.Drawing.Size(1502, 343)
        Me.INVGRID.TabIndex = 0
        '
        'selectitem
        '
        Me.selectitem.HeaderText = "Remove"
        Me.selectitem.Name = "selectitem"
        Me.selectitem.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.selectitem.Width = 50
        '
        'INVSLNO
        '
        Me.INVSLNO.HeaderText = "Line Item"
        Me.INVSLNO.Name = "INVSLNO"
        Me.INVSLNO.ReadOnly = True
        Me.INVSLNO.Width = 75
        '
        'SCALE
        '
        Me.SCALE.HeaderText = "Scale Type"
        Me.SCALE.Name = "SCALE"
        Me.SCALE.ReadOnly = True
        '
        'INTDOCNO
        '
        Me.INTDOCNO.HeaderText = "INTDOCNO"
        Me.INTDOCNO.Name = "INTDOCNO"
        Me.INTDOCNO.ReadOnly = True
        Me.INTDOCNO.Visible = False
        '
        'Ticketno
        '
        Me.Ticketno.HeaderText = "Ticket #"
        Me.Ticketno.Name = "Ticketno"
        Me.Ticketno.ReadOnly = True
        Me.Ticketno.Width = 75
        '
        'sledcode
        '
        Me.sledcode.HeaderText = "Vendor #"
        Me.sledcode.Name = "sledcode"
        Me.sledcode.ReadOnly = True
        Me.sledcode.Visible = False
        '
        'sleddesc
        '
        Me.sleddesc.HeaderText = "Vendor Name"
        Me.sleddesc.Name = "sleddesc"
        Me.sleddesc.ReadOnly = True
        Me.sleddesc.Visible = False
        '
        'SLNO
        '
        Me.SLNO.HeaderText = "SL #"
        Me.SLNO.Name = "SLNO"
        Me.SLNO.ReadOnly = True
        Me.SLNO.Width = 60
        '
        'ITEMcode
        '
        Me.ITEMcode.HeaderText = "Material #"
        Me.ITEMcode.Name = "ITEMcode"
        Me.ITEMcode.ReadOnly = True
        '
        'itemdesc
        '
        Me.itemdesc.HeaderText = "Material"
        Me.itemdesc.Name = "itemdesc"
        Me.itemdesc.ReadOnly = True
        Me.itemdesc.Width = 200
        '
        'dateout
        '
        Me.dateout.HeaderText = "Date"
        Me.dateout.Name = "dateout"
        Me.dateout.ReadOnly = True
        Me.dateout.Width = 75
        '
        'firstqty
        '
        Me.firstqty.HeaderText = "First WT"
        Me.firstqty.Name = "firstqty"
        Me.firstqty.ReadOnly = True
        Me.firstqty.Width = 75
        '
        'Secondqty
        '
        Me.Secondqty.HeaderText = "Second WT"
        Me.Secondqty.Name = "Secondqty"
        Me.Secondqty.ReadOnly = True
        Me.Secondqty.Width = 90
        '
        'QTY
        '
        Me.QTY.HeaderText = "Net Qty"
        Me.QTY.Name = "QTY"
        Me.QTY.ReadOnly = True
        Me.QTY.Width = 70
        '
        'priceton
        '
        Me.priceton.HeaderText = "Price"
        Me.priceton.Name = "priceton"
        Me.priceton.ReadOnly = True
        Me.priceton.Width = 60
        '
        'rate
        '
        Me.rate.HeaderText = "Rate"
        Me.rate.Name = "rate"
        Me.rate.ReadOnly = True
        Me.rate.Width = 60
        '
        'Total_price
        '
        Me.Total_price.HeaderText = "Total"
        Me.Total_price.Name = "Total_price"
        Me.Total_price.ReadOnly = True
        Me.Total_price.Width = 75
        '
        'vbelns
        '
        Me.vbelns.HeaderText = "Order #"
        Me.vbelns.Name = "vbelns"
        Me.vbelns.ReadOnly = True
        '
        'vbelnd
        '
        Me.vbelnd.HeaderText = "GR #"
        Me.vbelnd.Name = "vbelnd"
        Me.vbelnd.ReadOnly = True
        '
        'vbelni
        '
        Me.vbelni.HeaderText = "Invoice"
        Me.vbelni.Name = "vbelni"
        Me.vbelni.ReadOnly = True
        '
        'INVDOCNO
        '
        Me.INVDOCNO.HeaderText = "DOCNO"
        Me.INVDOCNO.Name = "INVDOCNO"
        Me.INVDOCNO.Visible = False
        '
        'POST_DATE
        '
        Me.POST_DATE.HeaderText = "POST_DATE"
        Me.POST_DATE.Name = "POST_DATE"
        Me.POST_DATE.ReadOnly = True
        Me.POST_DATE.Visible = False
        '
        'trcharge
        '
        DataGridViewCellStyle5.NullValue = "0"
        Me.trcharge.DefaultCellStyle = DataGridViewCellStyle5
        Me.trcharge.HeaderText = "Transport Charges"
        Me.trcharge.Name = "trcharge"
        Me.trcharge.Visible = False
        '
        'penalty
        '
        DataGridViewCellStyle6.NullValue = "0"
        Me.penalty.DefaultCellStyle = DataGridViewCellStyle6
        Me.penalty.HeaderText = "Penalty"
        Me.penalty.Name = "penalty"
        Me.penalty.Visible = False
        '
        'machcharge
        '
        DataGridViewCellStyle7.NullValue = "0"
        Me.machcharge.DefaultCellStyle = DataGridViewCellStyle7
        Me.machcharge.HeaderText = "Machine Charges"
        Me.machcharge.Name = "machcharge"
        Me.machcharge.Visible = False
        '
        'labcharge
        '
        DataGridViewCellStyle8.NullValue = "0"
        Me.labcharge.DefaultCellStyle = DataGridViewCellStyle8
        Me.labcharge.HeaderText = "Labor Charge"
        Me.labcharge.Name = "labcharge"
        Me.labcharge.Visible = False
        '
        'b_display
        '
        Me.b_display.Enabled = False
        Me.b_display.Location = New System.Drawing.Point(798, 75)
        Me.b_display.Name = "b_display"
        Me.b_display.Size = New System.Drawing.Size(75, 23)
        Me.b_display.TabIndex = 2
        Me.b_display.Text = "Open Items"
        Me.b_display.UseVisualStyleBackColor = True
        '
        'b_save
        '
        Me.b_save.Enabled = False
        Me.b_save.Location = New System.Drawing.Point(349, 659)
        Me.b_save.Name = "b_save"
        Me.b_save.Size = New System.Drawing.Size(75, 23)
        Me.b_save.TabIndex = 3
        Me.b_save.Text = "Save"
        Me.b_save.UseVisualStyleBackColor = True
        '
        'tb_docno
        '
        Me.tb_docno.Enabled = False
        Me.tb_docno.Location = New System.Drawing.Point(153, 55)
        Me.tb_docno.Name = "tb_docno"
        Me.tb_docno.Size = New System.Drawing.Size(210, 20)
        Me.tb_docno.TabIndex = 4
        '
        'b_searchdoc
        '
        Me.b_searchdoc.Location = New System.Drawing.Point(707, 12)
        Me.b_searchdoc.Name = "b_searchdoc"
        Me.b_searchdoc.Size = New System.Drawing.Size(138, 23)
        Me.b_searchdoc.TabIndex = 5
        Me.b_searchdoc.Text = "Open Details"
        Me.b_searchdoc.UseVisualStyleBackColor = True
        '
        'tb_rdocno
        '
        Me.tb_rdocno.Location = New System.Drawing.Point(491, 14)
        Me.tb_rdocno.Name = "tb_rdocno"
        Me.tb_rdocno.Size = New System.Drawing.Size(210, 20)
        Me.tb_rdocno.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(106, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Vendor"
        '
        'b_gen
        '
        Me.b_gen.Location = New System.Drawing.Point(153, 12)
        Me.b_gen.Name = "b_gen"
        Me.b_gen.Size = New System.Drawing.Size(129, 23)
        Me.b_gen.TabIndex = 8
        Me.b_gen.Text = "Generate"
        Me.b_gen.UseVisualStyleBackColor = True
        '
        'tb_searchbyno
        '
        Me.tb_searchbyno.Enabled = False
        Me.tb_searchbyno.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_searchbyno.Location = New System.Drawing.Point(153, 120)
        Me.tb_searchbyno.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_searchbyno.Name = "tb_searchbyno"
        Me.tb_searchbyno.Size = New System.Drawing.Size(211, 23)
        Me.tb_searchbyno.TabIndex = 346
        '
        'cb_sledcode
        '
        Me.cb_sledcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cb_sledcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cb_sledcode.BackColor = System.Drawing.Color.White
        Me.cb_sledcode.Enabled = False
        Me.cb_sledcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cb_sledcode.FormattingEnabled = True
        Me.cb_sledcode.Location = New System.Drawing.Point(153, 76)
        Me.cb_sledcode.Name = "cb_sledcode"
        Me.cb_sledcode.Size = New System.Drawing.Size(402, 21)
        Me.cb_sledcode.TabIndex = 345
        '
        'tb_sledesc
        '
        Me.tb_sledesc.BackColor = System.Drawing.Color.White
        Me.tb_sledesc.Enabled = False
        Me.tb_sledesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_sledesc.Location = New System.Drawing.Point(153, 97)
        Me.tb_sledesc.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_sledesc.Name = "tb_sledesc"
        Me.tb_sledesc.Size = New System.Drawing.Size(211, 23)
        Me.tb_sledesc.TabIndex = 344
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(69, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 347
        Me.Label2.Text = "Search Vendor"
        '
        'loadven
        '
        Me.loadven.BackColor = System.Drawing.Color.LightBlue
        Me.loadven.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.loadven.FullRowSelect = True
        Me.loadven.GridLines = True
        Me.loadven.Location = New System.Drawing.Point(152, 148)
        Me.loadven.Name = "loadven"
        Me.loadven.Size = New System.Drawing.Size(540, 140)
        Me.loadven.TabIndex = 348
        Me.loadven.UseCompatibleStateImageBehavior = False
        Me.loadven.View = System.Windows.Forms.View.Details
        Me.loadven.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Vendor Code"
        Me.ColumnHeader1.Width = 170
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Vendor Name"
        Me.ColumnHeader2.Width = 325
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(51, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 349
        Me.Label3.Text = "Document Number"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(96, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 350
        Me.Label4.Text = "Vendor #"
        '
        'b_crinv
        '
        Me.b_crinv.Enabled = False
        Me.b_crinv.Location = New System.Drawing.Point(480, 659)
        Me.b_crinv.Name = "b_crinv"
        Me.b_crinv.Size = New System.Drawing.Size(365, 23)
        Me.b_crinv.TabIndex = 351
        Me.b_crinv.Text = "Create Invoice"
        Me.b_crinv.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.type, Me.i_d, Me.Number, Me.Mesage})
        Me.DataGridView1.Location = New System.Drawing.Point(863, 643)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(651, 145)
        Me.DataGridView1.TabIndex = 360
        '
        'type
        '
        Me.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.type.HeaderText = "Typed"
        Me.type.Name = "type"
        Me.type.Width = 62
        '
        'i_d
        '
        Me.i_d.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.i_d.HeaderText = "Id"
        Me.i_d.Name = "i_d"
        Me.i_d.Width = 41
        '
        'Number
        '
        Me.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Number.HeaderText = "Number"
        Me.Number.Name = "Number"
        Me.Number.Width = 69
        '
        'Mesage
        '
        Me.Mesage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Mesage.HeaderText = "Message"
        Me.Mesage.Name = "Mesage"
        Me.Mesage.Width = 75
        '
        'tb_vbelni
        '
        Me.tb_vbelni.Location = New System.Drawing.Point(817, 148)
        Me.tb_vbelni.Name = "tb_vbelni"
        Me.tb_vbelni.Size = New System.Drawing.Size(157, 20)
        Me.tb_vbelni.TabIndex = 361
        '
        'b_exit
        '
        Me.b_exit.BackColor = System.Drawing.Color.Thistle
        Me.b_exit.BackgroundImage = CType(resources.GetObject("b_exit.BackgroundImage"), System.Drawing.Image)
        Me.b_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.b_exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, CType(0, Byte))
        Me.b_exit.Location = New System.Drawing.Point(979, 148)
        Me.b_exit.Margin = New System.Windows.Forms.Padding(2)
        Me.b_exit.Name = "b_exit"
        Me.b_exit.Size = New System.Drawing.Size(85, 46)
        Me.b_exit.TabIndex = 362
        Me.b_exit.UseVisualStyleBackColor = False
        '
        'd_date
        '
        Me.d_date.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.d_date.Location = New System.Drawing.Point(558, 75)
        Me.d_date.Name = "d_date"
        Me.d_date.Size = New System.Drawing.Size(237, 23)
        Me.d_date.TabIndex = 363
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(761, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 364
        Me.Label5.Text = "Invoice #"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(1005, 244)
        Me.Label40.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(111, 17)
        Me.Label40.TabIndex = 372
        Me.Label40.Text = "Equipment Chgs"
        '
        'Tb_eqpchrgs
        '
        Me.Tb_eqpchrgs.BackColor = System.Drawing.Color.White
        Me.Tb_eqpchrgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_eqpchrgs.Location = New System.Drawing.Point(1122, 241)
        Me.Tb_eqpchrgs.Margin = New System.Windows.Forms.Padding(2)
        Me.Tb_eqpchrgs.Name = "Tb_eqpchrgs"
        Me.Tb_eqpchrgs.Size = New System.Drawing.Size(159, 23)
        Me.Tb_eqpchrgs.TabIndex = 371
        Me.Tb_eqpchrgs.Text = "0"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(1005, 222)
        Me.Label39.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(110, 17)
        Me.Label39.TabIndex = 370
        Me.Label39.Text = "Labour Charges"
        '
        'Tb_labourcharges
        '
        Me.Tb_labourcharges.BackColor = System.Drawing.Color.White
        Me.Tb_labourcharges.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_labourcharges.Location = New System.Drawing.Point(1122, 218)
        Me.Tb_labourcharges.Margin = New System.Windows.Forms.Padding(2)
        Me.Tb_labourcharges.Name = "Tb_labourcharges"
        Me.Tb_labourcharges.Size = New System.Drawing.Size(159, 23)
        Me.Tb_labourcharges.TabIndex = 369
        Me.Tb_labourcharges.Text = "0"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(744, 241)
        Me.Label37.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(55, 17)
        Me.Label37.TabIndex = 368
        Me.Label37.Text = "Penalty"
        '
        'Tb_penalty
        '
        Me.Tb_penalty.BackColor = System.Drawing.Color.White
        Me.Tb_penalty.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_penalty.Location = New System.Drawing.Point(863, 239)
        Me.Tb_penalty.Margin = New System.Windows.Forms.Padding(2)
        Me.Tb_penalty.Name = "Tb_penalty"
        Me.Tb_penalty.Size = New System.Drawing.Size(135, 23)
        Me.Tb_penalty.TabIndex = 367
        Me.Tb_penalty.Text = "0"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(744, 219)
        Me.Label36.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(105, 17)
        Me.Label36.TabIndex = 366
        Me.Label36.Text = "TRN Deduction"
        '
        'Tb_transp
        '
        Me.Tb_transp.BackColor = System.Drawing.Color.White
        Me.Tb_transp.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.Tb_transp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_transp.Location = New System.Drawing.Point(863, 216)
        Me.Tb_transp.Margin = New System.Windows.Forms.Padding(2)
        Me.Tb_transp.Name = "Tb_transp"
        Me.Tb_transp.Size = New System.Drawing.Size(135, 23)
        Me.Tb_transp.TabIndex = 365
        Me.Tb_transp.Text = "0"
        '
        'INVOICE_PR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1526, 790)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.Tb_eqpchrgs)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.Tb_labourcharges)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.Tb_penalty)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Tb_transp)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.d_date)
        Me.Controls.Add(Me.b_exit)
        Me.Controls.Add(Me.tb_vbelni)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.b_crinv)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.loadven)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_searchbyno)
        Me.Controls.Add(Me.cb_sledcode)
        Me.Controls.Add(Me.tb_sledesc)
        Me.Controls.Add(Me.b_gen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tb_rdocno)
        Me.Controls.Add(Me.b_searchdoc)
        Me.Controls.Add(Me.tb_docno)
        Me.Controls.Add(Me.b_save)
        Me.Controls.Add(Me.b_display)
        Me.Controls.Add(Me.INVGRID)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "INVOICE_PR"
        Me.Text = "INVOICE"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.INVGRID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INVGRID As System.Windows.Forms.DataGridView
    Friend WithEvents b_display As System.Windows.Forms.Button
    Friend WithEvents b_save As System.Windows.Forms.Button
    Friend WithEvents tb_docno As System.Windows.Forms.TextBox
    Friend WithEvents b_searchdoc As System.Windows.Forms.Button
    Friend WithEvents tb_rdocno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents b_gen As System.Windows.Forms.Button
    Friend WithEvents tb_searchbyno As System.Windows.Forms.TextBox
    Friend WithEvents cb_sledcode As System.Windows.Forms.ComboBox
    Friend WithEvents tb_sledesc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents loadven As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents b_crinv As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents i_d As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mesage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tb_vbelni As System.Windows.Forms.TextBox
    Friend WithEvents b_exit As System.Windows.Forms.Button
    Friend WithEvents d_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Tb_eqpchrgs As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Tb_labourcharges As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Tb_penalty As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Tb_transp As System.Windows.Forms.TextBox
    Friend WithEvents selectitem As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents INVSLNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SCALE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INTDOCNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ticketno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sledcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sleddesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SLNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ITEMcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itemdesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dateout As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents firstqty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Secondqty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents priceton As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vbelns As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vbelnd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vbelni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INVDOCNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents POST_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents trcharge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents penalty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents machcharge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labcharge As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
