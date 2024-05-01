<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class f_gp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(f_gp))
        Me.tb_visitor = New System.Windows.Forms.TextBox()
        Me.cb_plate = New System.Windows.Forms.ComboBox()
        Me.tb_iqama = New System.Windows.Forms.TextBox()
        Me.tb_mobile = New System.Windows.Forms.TextBox()
        Me.tb_purpose = New System.Windows.Forms.TextBox()
        Me.tb_intime = New System.Windows.Forms.TextBox()
        Me.tb_outtime = New System.Windows.Forms.TextBox()
        Me.tb_sttime = New System.Windows.Forms.TextBox()
        Me.l_date = New System.Windows.Forms.Label()
        Me.l_visitor = New System.Windows.Forms.Label()
        Me.l_plate = New System.Windows.Forms.Label()
        Me.l_purpose = New System.Windows.Forms.Label()
        Me.l_mobile = New System.Windows.Forms.Label()
        Me.l_iqama = New System.Windows.Forms.Label()
        Me.l_sttime = New System.Windows.Forms.Label()
        Me.l_outtime = New System.Windows.Forms.Label()
        Me.l_intime = New System.Windows.Forms.Label()
        Me.d_newdate = New System.Windows.Forms.DateTimePicker()
        Me.b_save = New System.Windows.Forms.Button()
        Me.b_intime = New System.Windows.Forms.Button()
        Me.b_outtime = New System.Windows.Forms.Button()
        Me.b_exit = New System.Windows.Forms.Button()
        Me.b_in = New System.Windows.Forms.Button()
        Me.rb_alq = New System.Windows.Forms.RadioButton()
        Me.rb_out = New System.Windows.Forms.RadioButton()
        Me.gb_vehicle = New System.Windows.Forms.GroupBox()
        Me.b_out = New System.Windows.Forms.Button()
        Me.tb_search = New System.Windows.Forms.TextBox()
        Me.dgv1 = New System.Windows.Forms.DataGridView()
        Me.intdocno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.visitor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.plate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.iqama = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mobile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.purpose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.intime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.outtime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.staytime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vtype = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VISITORBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VIS = New wbms1.VIS()
        Me.VISITORTableAdapter = New wbms1.VISTableAdapters.VISITORTableAdapter()
        Me.VISITORDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PLATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IQAMADataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MOBILEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PURPOSEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INTIMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OUTTIMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STAYTIMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INTDOCNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VTYPEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STATUSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gb_vehicle.SuspendLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VISITORBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tb_visitor
        '
        Me.tb_visitor.Location = New System.Drawing.Point(145, 198)
        Me.tb_visitor.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_visitor.Name = "tb_visitor"
        Me.tb_visitor.Size = New System.Drawing.Size(220, 22)
        Me.tb_visitor.TabIndex = 1
        '
        'cb_plate
        '
        Me.cb_plate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cb_plate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cb_plate.Location = New System.Drawing.Point(145, 234)
        Me.cb_plate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cb_plate.Name = "cb_plate"
        Me.cb_plate.Size = New System.Drawing.Size(220, 24)
        Me.cb_plate.TabIndex = 2
        '
        'tb_iqama
        '
        Me.tb_iqama.Location = New System.Drawing.Point(145, 270)
        Me.tb_iqama.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_iqama.Name = "tb_iqama"
        Me.tb_iqama.Size = New System.Drawing.Size(220, 22)
        Me.tb_iqama.TabIndex = 3
        '
        'tb_mobile
        '
        Me.tb_mobile.Location = New System.Drawing.Point(145, 305)
        Me.tb_mobile.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_mobile.Name = "tb_mobile"
        Me.tb_mobile.Size = New System.Drawing.Size(220, 22)
        Me.tb_mobile.TabIndex = 4
        '
        'tb_purpose
        '
        Me.tb_purpose.Location = New System.Drawing.Point(145, 341)
        Me.tb_purpose.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_purpose.Name = "tb_purpose"
        Me.tb_purpose.Size = New System.Drawing.Size(220, 22)
        Me.tb_purpose.TabIndex = 5
        '
        'tb_intime
        '
        Me.tb_intime.Location = New System.Drawing.Point(145, 375)
        Me.tb_intime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_intime.Name = "tb_intime"
        Me.tb_intime.Size = New System.Drawing.Size(220, 22)
        Me.tb_intime.TabIndex = 6
        '
        'tb_outtime
        '
        Me.tb_outtime.Location = New System.Drawing.Point(145, 407)
        Me.tb_outtime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_outtime.Name = "tb_outtime"
        Me.tb_outtime.Size = New System.Drawing.Size(220, 22)
        Me.tb_outtime.TabIndex = 7
        '
        'tb_sttime
        '
        Me.tb_sttime.Location = New System.Drawing.Point(145, 438)
        Me.tb_sttime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_sttime.Name = "tb_sttime"
        Me.tb_sttime.Size = New System.Drawing.Size(220, 22)
        Me.tb_sttime.TabIndex = 8
        '
        'l_date
        '
        Me.l_date.AutoSize = True
        Me.l_date.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_date.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_date.Location = New System.Drawing.Point(19, 169)
        Me.l_date.Name = "l_date"
        Me.l_date.Size = New System.Drawing.Size(39, 16)
        Me.l_date.TabIndex = 9
        Me.l_date.Text = "Date"
        '
        'l_visitor
        '
        Me.l_visitor.AutoSize = True
        Me.l_visitor.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_visitor.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_visitor.Location = New System.Drawing.Point(19, 202)
        Me.l_visitor.Name = "l_visitor"
        Me.l_visitor.Size = New System.Drawing.Size(48, 16)
        Me.l_visitor.TabIndex = 10
        Me.l_visitor.Text = "Name"
        '
        'l_plate
        '
        Me.l_plate.AutoSize = True
        Me.l_plate.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_plate.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_plate.Location = New System.Drawing.Point(19, 238)
        Me.l_plate.Name = "l_plate"
        Me.l_plate.Size = New System.Drawing.Size(54, 16)
        Me.l_plate.TabIndex = 11
        Me.l_plate.Text = "Plate #"
        '
        'l_purpose
        '
        Me.l_purpose.AutoSize = True
        Me.l_purpose.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_purpose.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_purpose.Location = New System.Drawing.Point(19, 343)
        Me.l_purpose.Name = "l_purpose"
        Me.l_purpose.Size = New System.Drawing.Size(63, 16)
        Me.l_purpose.TabIndex = 14
        Me.l_purpose.Text = "Purpose"
        '
        'l_mobile
        '
        Me.l_mobile.AutoSize = True
        Me.l_mobile.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_mobile.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_mobile.Location = New System.Drawing.Point(19, 309)
        Me.l_mobile.Name = "l_mobile"
        Me.l_mobile.Size = New System.Drawing.Size(66, 16)
        Me.l_mobile.TabIndex = 13
        Me.l_mobile.Text = "Mobile #"
        '
        'l_iqama
        '
        Me.l_iqama.AutoSize = True
        Me.l_iqama.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_iqama.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_iqama.Location = New System.Drawing.Point(19, 272)
        Me.l_iqama.Name = "l_iqama"
        Me.l_iqama.Size = New System.Drawing.Size(62, 16)
        Me.l_iqama.TabIndex = 12
        Me.l_iqama.Text = "Iqama #"
        '
        'l_sttime
        '
        Me.l_sttime.AutoSize = True
        Me.l_sttime.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_sttime.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_sttime.Location = New System.Drawing.Point(19, 442)
        Me.l_sttime.Name = "l_sttime"
        Me.l_sttime.Size = New System.Drawing.Size(74, 16)
        Me.l_sttime.TabIndex = 17
        Me.l_sttime.Text = "Stay Time"
        '
        'l_outtime
        '
        Me.l_outtime.AutoSize = True
        Me.l_outtime.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_outtime.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_outtime.Location = New System.Drawing.Point(19, 412)
        Me.l_outtime.Name = "l_outtime"
        Me.l_outtime.Size = New System.Drawing.Size(69, 16)
        Me.l_outtime.TabIndex = 16
        Me.l_outtime.Text = "Out Time"
        '
        'l_intime
        '
        Me.l_intime.AutoSize = True
        Me.l_intime.Font = New System.Drawing.Font("Leelawadee", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_intime.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.l_intime.Location = New System.Drawing.Point(19, 380)
        Me.l_intime.Name = "l_intime"
        Me.l_intime.Size = New System.Drawing.Size(57, 16)
        Me.l_intime.TabIndex = 15
        Me.l_intime.Text = "In Time"
        '
        'd_newdate
        '
        Me.d_newdate.Location = New System.Drawing.Point(145, 165)
        Me.d_newdate.Margin = New System.Windows.Forms.Padding(4)
        Me.d_newdate.Name = "d_newdate"
        Me.d_newdate.Size = New System.Drawing.Size(220, 22)
        Me.d_newdate.TabIndex = 98
        '
        'b_save
        '
        Me.b_save.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_save.ForeColor = System.Drawing.SystemColors.Highlight
        Me.b_save.Location = New System.Drawing.Point(145, 475)
        Me.b_save.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_save.Name = "b_save"
        Me.b_save.Size = New System.Drawing.Size(220, 23)
        Me.b_save.TabIndex = 99
        Me.b_save.Text = "SAVE"
        Me.b_save.UseVisualStyleBackColor = True
        '
        'b_intime
        '
        Me.b_intime.Enabled = False
        Me.b_intime.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_intime.ForeColor = System.Drawing.SystemColors.Highlight
        Me.b_intime.Location = New System.Drawing.Point(371, 375)
        Me.b_intime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_intime.Name = "b_intime"
        Me.b_intime.Size = New System.Drawing.Size(103, 23)
        Me.b_intime.TabIndex = 100
        Me.b_intime.Text = "In Time"
        Me.b_intime.UseVisualStyleBackColor = True
        '
        'b_outtime
        '
        Me.b_outtime.Enabled = False
        Me.b_outtime.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_outtime.ForeColor = System.Drawing.SystemColors.Highlight
        Me.b_outtime.Location = New System.Drawing.Point(371, 407)
        Me.b_outtime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_outtime.Name = "b_outtime"
        Me.b_outtime.Size = New System.Drawing.Size(103, 23)
        Me.b_outtime.TabIndex = 101
        Me.b_outtime.Text = "Out Time"
        Me.b_outtime.UseVisualStyleBackColor = True
        '
        'b_exit
        '
        Me.b_exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_exit.ForeColor = System.Drawing.SystemColors.Highlight
        Me.b_exit.Location = New System.Drawing.Point(145, 511)
        Me.b_exit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.b_exit.Name = "b_exit"
        Me.b_exit.Size = New System.Drawing.Size(220, 23)
        Me.b_exit.TabIndex = 102
        Me.b_exit.Text = "EXIT"
        Me.b_exit.UseVisualStyleBackColor = True
        '
        'b_in
        '
        Me.b_in.Location = New System.Drawing.Point(145, 127)
        Me.b_in.Name = "b_in"
        Me.b_in.Size = New System.Drawing.Size(99, 23)
        Me.b_in.TabIndex = 103
        Me.b_in.Text = "IN"
        Me.b_in.UseVisualStyleBackColor = True
        '
        'rb_alq
        '
        Me.rb_alq.AutoSize = True
        Me.rb_alq.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.rb_alq.Location = New System.Drawing.Point(6, 47)
        Me.rb_alq.Name = "rb_alq"
        Me.rb_alq.Size = New System.Drawing.Size(93, 21)
        Me.rb_alq.TabIndex = 104
        Me.rb_alq.TabStop = True
        Me.rb_alq.Text = "Al-Qaryan"
        Me.rb_alq.UseVisualStyleBackColor = True
        '
        'rb_out
        '
        Me.rb_out.AutoSize = True
        Me.rb_out.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.rb_out.Location = New System.Drawing.Point(127, 47)
        Me.rb_out.Name = "rb_out"
        Me.rb_out.Size = New System.Drawing.Size(78, 21)
        Me.rb_out.TabIndex = 105
        Me.rb_out.TabStop = True
        Me.rb_out.Text = "Outside"
        Me.rb_out.UseVisualStyleBackColor = True
        '
        'gb_vehicle
        '
        Me.gb_vehicle.Controls.Add(Me.rb_out)
        Me.gb_vehicle.Controls.Add(Me.rb_alq)
        Me.gb_vehicle.Location = New System.Drawing.Point(145, 12)
        Me.gb_vehicle.Name = "gb_vehicle"
        Me.gb_vehicle.Size = New System.Drawing.Size(220, 100)
        Me.gb_vehicle.TabIndex = 106
        Me.gb_vehicle.TabStop = False
        '
        'b_out
        '
        Me.b_out.Location = New System.Drawing.Point(272, 127)
        Me.b_out.Name = "b_out"
        Me.b_out.Size = New System.Drawing.Size(93, 23)
        Me.b_out.TabIndex = 107
        Me.b_out.Text = "OUT"
        Me.b_out.UseVisualStyleBackColor = True
        '
        'tb_search
        '
        Me.tb_search.Location = New System.Drawing.Point(371, 128)
        Me.tb_search.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tb_search.Name = "tb_search"
        Me.tb_search.Size = New System.Drawing.Size(103, 22)
        Me.tb_search.TabIndex = 108
        '
        'dgv1
        '
        Me.dgv1.AllowUserToAddRows = False
        Me.dgv1.AllowUserToDeleteRows = False
        Me.dgv1.AutoGenerateColumns = False
        Me.dgv1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.intdocno, Me.idate, Me.visitor, Me.plate, Me.iqama, Me.mobile, Me.purpose, Me.intime, Me.outtime, Me.staytime, Me.vtype, Me.status, Me.VISITORDataGridViewTextBoxColumn, Me.PLATEDataGridViewTextBoxColumn, Me.IQAMADataGridViewTextBoxColumn, Me.MOBILEDataGridViewTextBoxColumn, Me.PURPOSEDataGridViewTextBoxColumn, Me.INTIMEDataGridViewTextBoxColumn, Me.OUTTIMEDataGridViewTextBoxColumn, Me.STAYTIMEDataGridViewTextBoxColumn, Me.INTDOCNODataGridViewTextBoxColumn, Me.VTYPEDataGridViewTextBoxColumn, Me.STATUSDataGridViewTextBoxColumn})
        Me.dgv1.DataSource = Me.VISITORBindingSource
        Me.dgv1.Location = New System.Drawing.Point(480, 87)
        Me.dgv1.Name = "dgv1"
        Me.dgv1.ReadOnly = True
        Me.dgv1.RowHeadersWidth = 51
        Me.dgv1.RowTemplate.Height = 24
        Me.dgv1.Size = New System.Drawing.Size(1080, 519)
        Me.dgv1.TabIndex = 109
        '
        'intdocno
        '
        Me.intdocno.DataPropertyName = "INTDOCNO"
        Me.intdocno.HeaderText = "Entry #"
        Me.intdocno.MinimumWidth = 6
        Me.intdocno.Name = "intdocno"
        Me.intdocno.ReadOnly = True
        Me.intdocno.Width = 60
        '
        'idate
        '
        Me.idate.HeaderText = "Date"
        Me.idate.MinimumWidth = 6
        Me.idate.Name = "idate"
        Me.idate.ReadOnly = True
        Me.idate.Width = 125
        '
        'visitor
        '
        Me.visitor.DataPropertyName = "VISITOR"
        Me.visitor.HeaderText = "Name"
        Me.visitor.MinimumWidth = 6
        Me.visitor.Name = "visitor"
        Me.visitor.ReadOnly = True
        Me.visitor.Width = 150
        '
        'plate
        '
        Me.plate.DataPropertyName = "PLATE"
        Me.plate.HeaderText = "Plate #"
        Me.plate.MinimumWidth = 6
        Me.plate.Name = "plate"
        Me.plate.ReadOnly = True
        Me.plate.Width = 60
        '
        'iqama
        '
        Me.iqama.DataPropertyName = "IQAMA"
        Me.iqama.HeaderText = "Iqama"
        Me.iqama.MinimumWidth = 6
        Me.iqama.Name = "iqama"
        Me.iqama.ReadOnly = True
        Me.iqama.Width = 90
        '
        'mobile
        '
        Me.mobile.DataPropertyName = "MOBILE"
        Me.mobile.HeaderText = "Mobile"
        Me.mobile.MinimumWidth = 6
        Me.mobile.Name = "mobile"
        Me.mobile.ReadOnly = True
        Me.mobile.Width = 90
        '
        'purpose
        '
        Me.purpose.DataPropertyName = "PURPOSE"
        Me.purpose.HeaderText = "Purpose"
        Me.purpose.MinimumWidth = 6
        Me.purpose.Name = "purpose"
        Me.purpose.ReadOnly = True
        Me.purpose.Width = 200
        '
        'intime
        '
        Me.intime.DataPropertyName = "INTIME"
        Me.intime.HeaderText = "In Time"
        Me.intime.MinimumWidth = 6
        Me.intime.Name = "intime"
        Me.intime.ReadOnly = True
        Me.intime.Width = 125
        '
        'outtime
        '
        Me.outtime.DataPropertyName = "OUTTIME"
        Me.outtime.HeaderText = "Out Time"
        Me.outtime.MinimumWidth = 6
        Me.outtime.Name = "outtime"
        Me.outtime.ReadOnly = True
        Me.outtime.Width = 125
        '
        'staytime
        '
        Me.staytime.DataPropertyName = "STAYTIME"
        Me.staytime.HeaderText = "Duration"
        Me.staytime.MinimumWidth = 6
        Me.staytime.Name = "staytime"
        Me.staytime.ReadOnly = True
        Me.staytime.Width = 125
        '
        'vtype
        '
        Me.vtype.DataPropertyName = "VTYPE"
        Me.vtype.HeaderText = "Vehicle"
        Me.vtype.MinimumWidth = 6
        Me.vtype.Name = "vtype"
        Me.vtype.ReadOnly = True
        Me.vtype.Width = 60
        '
        'status
        '
        Me.status.DataPropertyName = "STATUS"
        Me.status.HeaderText = "Status"
        Me.status.MinimumWidth = 6
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Width = 40
        '
        'VISITORBindingSource
        '
        Me.VISITORBindingSource.DataMember = "VISITOR"
        Me.VISITORBindingSource.DataSource = Me.VIS
        '
        'VIS
        '
        Me.VIS.DataSetName = "VIS"
        Me.VIS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'VISITORTableAdapter
        '
        Me.VISITORTableAdapter.ClearBeforeFill = True
        '
        'VISITORDataGridViewTextBoxColumn
        '
        Me.VISITORDataGridViewTextBoxColumn.DataPropertyName = "VISITOR"
        Me.VISITORDataGridViewTextBoxColumn.HeaderText = "VISITOR"
        Me.VISITORDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.VISITORDataGridViewTextBoxColumn.Name = "VISITORDataGridViewTextBoxColumn"
        Me.VISITORDataGridViewTextBoxColumn.ReadOnly = True
        Me.VISITORDataGridViewTextBoxColumn.Width = 125
        '
        'PLATEDataGridViewTextBoxColumn
        '
        Me.PLATEDataGridViewTextBoxColumn.DataPropertyName = "PLATE"
        Me.PLATEDataGridViewTextBoxColumn.HeaderText = "PLATE"
        Me.PLATEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.PLATEDataGridViewTextBoxColumn.Name = "PLATEDataGridViewTextBoxColumn"
        Me.PLATEDataGridViewTextBoxColumn.ReadOnly = True
        Me.PLATEDataGridViewTextBoxColumn.Width = 125
        '
        'IQAMADataGridViewTextBoxColumn
        '
        Me.IQAMADataGridViewTextBoxColumn.DataPropertyName = "IQAMA"
        Me.IQAMADataGridViewTextBoxColumn.HeaderText = "IQAMA"
        Me.IQAMADataGridViewTextBoxColumn.MinimumWidth = 6
        Me.IQAMADataGridViewTextBoxColumn.Name = "IQAMADataGridViewTextBoxColumn"
        Me.IQAMADataGridViewTextBoxColumn.ReadOnly = True
        Me.IQAMADataGridViewTextBoxColumn.Width = 125
        '
        'MOBILEDataGridViewTextBoxColumn
        '
        Me.MOBILEDataGridViewTextBoxColumn.DataPropertyName = "MOBILE"
        Me.MOBILEDataGridViewTextBoxColumn.HeaderText = "MOBILE"
        Me.MOBILEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.MOBILEDataGridViewTextBoxColumn.Name = "MOBILEDataGridViewTextBoxColumn"
        Me.MOBILEDataGridViewTextBoxColumn.ReadOnly = True
        Me.MOBILEDataGridViewTextBoxColumn.Width = 125
        '
        'PURPOSEDataGridViewTextBoxColumn
        '
        Me.PURPOSEDataGridViewTextBoxColumn.DataPropertyName = "PURPOSE"
        Me.PURPOSEDataGridViewTextBoxColumn.HeaderText = "PURPOSE"
        Me.PURPOSEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.PURPOSEDataGridViewTextBoxColumn.Name = "PURPOSEDataGridViewTextBoxColumn"
        Me.PURPOSEDataGridViewTextBoxColumn.ReadOnly = True
        Me.PURPOSEDataGridViewTextBoxColumn.Width = 125
        '
        'INTIMEDataGridViewTextBoxColumn
        '
        Me.INTIMEDataGridViewTextBoxColumn.DataPropertyName = "INTIME"
        Me.INTIMEDataGridViewTextBoxColumn.HeaderText = "INTIME"
        Me.INTIMEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.INTIMEDataGridViewTextBoxColumn.Name = "INTIMEDataGridViewTextBoxColumn"
        Me.INTIMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.INTIMEDataGridViewTextBoxColumn.Width = 125
        '
        'OUTTIMEDataGridViewTextBoxColumn
        '
        Me.OUTTIMEDataGridViewTextBoxColumn.DataPropertyName = "OUTTIME"
        Me.OUTTIMEDataGridViewTextBoxColumn.HeaderText = "OUTTIME"
        Me.OUTTIMEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.OUTTIMEDataGridViewTextBoxColumn.Name = "OUTTIMEDataGridViewTextBoxColumn"
        Me.OUTTIMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.OUTTIMEDataGridViewTextBoxColumn.Width = 125
        '
        'STAYTIMEDataGridViewTextBoxColumn
        '
        Me.STAYTIMEDataGridViewTextBoxColumn.DataPropertyName = "STAYTIME"
        Me.STAYTIMEDataGridViewTextBoxColumn.HeaderText = "STAYTIME"
        Me.STAYTIMEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.STAYTIMEDataGridViewTextBoxColumn.Name = "STAYTIMEDataGridViewTextBoxColumn"
        Me.STAYTIMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.STAYTIMEDataGridViewTextBoxColumn.Width = 125
        '
        'INTDOCNODataGridViewTextBoxColumn
        '
        Me.INTDOCNODataGridViewTextBoxColumn.DataPropertyName = "INTDOCNO"
        Me.INTDOCNODataGridViewTextBoxColumn.HeaderText = "INTDOCNO"
        Me.INTDOCNODataGridViewTextBoxColumn.MinimumWidth = 6
        Me.INTDOCNODataGridViewTextBoxColumn.Name = "INTDOCNODataGridViewTextBoxColumn"
        Me.INTDOCNODataGridViewTextBoxColumn.ReadOnly = True
        Me.INTDOCNODataGridViewTextBoxColumn.Width = 125
        '
        'VTYPEDataGridViewTextBoxColumn
        '
        Me.VTYPEDataGridViewTextBoxColumn.DataPropertyName = "VTYPE"
        Me.VTYPEDataGridViewTextBoxColumn.HeaderText = "VTYPE"
        Me.VTYPEDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.VTYPEDataGridViewTextBoxColumn.Name = "VTYPEDataGridViewTextBoxColumn"
        Me.VTYPEDataGridViewTextBoxColumn.ReadOnly = True
        Me.VTYPEDataGridViewTextBoxColumn.Width = 125
        '
        'STATUSDataGridViewTextBoxColumn
        '
        Me.STATUSDataGridViewTextBoxColumn.DataPropertyName = "STATUS"
        Me.STATUSDataGridViewTextBoxColumn.HeaderText = "STATUS"
        Me.STATUSDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.STATUSDataGridViewTextBoxColumn.Name = "STATUSDataGridViewTextBoxColumn"
        Me.STATUSDataGridViewTextBoxColumn.ReadOnly = True
        Me.STATUSDataGridViewTextBoxColumn.Width = 125
        '
        'f_gp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.Teal
        Me.ClientSize = New System.Drawing.Size(1595, 661)
        Me.Controls.Add(Me.dgv1)
        Me.Controls.Add(Me.tb_search)
        Me.Controls.Add(Me.b_out)
        Me.Controls.Add(Me.gb_vehicle)
        Me.Controls.Add(Me.b_in)
        Me.Controls.Add(Me.b_exit)
        Me.Controls.Add(Me.b_outtime)
        Me.Controls.Add(Me.b_intime)
        Me.Controls.Add(Me.b_save)
        Me.Controls.Add(Me.d_newdate)
        Me.Controls.Add(Me.l_sttime)
        Me.Controls.Add(Me.l_outtime)
        Me.Controls.Add(Me.l_intime)
        Me.Controls.Add(Me.l_purpose)
        Me.Controls.Add(Me.l_mobile)
        Me.Controls.Add(Me.l_iqama)
        Me.Controls.Add(Me.l_plate)
        Me.Controls.Add(Me.l_visitor)
        Me.Controls.Add(Me.l_date)
        Me.Controls.Add(Me.tb_sttime)
        Me.Controls.Add(Me.tb_outtime)
        Me.Controls.Add(Me.tb_intime)
        Me.Controls.Add(Me.tb_purpose)
        Me.Controls.Add(Me.tb_mobile)
        Me.Controls.Add(Me.tb_iqama)
        Me.Controls.Add(Me.cb_plate)
        Me.Controls.Add(Me.tb_visitor)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "f_gp"
        Me.Text = "Gate Pass"
        Me.gb_vehicle.ResumeLayout(False)
        Me.gb_vehicle.PerformLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VISITORBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_visitor As TextBox
    Friend WithEvents tb_iqama As TextBox
    Friend WithEvents tb_mobile As TextBox
    Friend WithEvents tb_purpose As TextBox
    Friend WithEvents tb_intime As TextBox
    Friend WithEvents tb_outtime As TextBox
    Friend WithEvents tb_sttime As TextBox
    Friend WithEvents l_date As Label
    Friend WithEvents l_visitor As Label
    Friend WithEvents l_plate As Label
    Friend WithEvents l_purpose As Label
    Friend WithEvents l_mobile As Label
    Friend WithEvents l_iqama As Label
    Friend WithEvents l_sttime As Label
    Friend WithEvents l_outtime As Label
    Friend WithEvents l_intime As Label
    Friend WithEvents d_newdate As DateTimePicker
    Friend WithEvents b_save As Button
    Friend WithEvents b_intime As Button
    Friend WithEvents b_outtime As Button
    Friend WithEvents b_exit As Button
    Friend WithEvents b_in As Button
    Friend WithEvents rb_alq As RadioButton
    Friend WithEvents rb_out As RadioButton
    Friend WithEvents gb_vehicle As GroupBox
    Friend WithEvents b_out As Button
    Friend WithEvents tb_search As TextBox
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents cb_plate As ComboBox
    Friend WithEvents VIS As VIS
    Friend WithEvents VISITORBindingSource As BindingSource
    Friend WithEvents VISITORTableAdapter As VISTableAdapters.VISITORTableAdapter
    Friend WithEvents intdocno As DataGridViewTextBoxColumn
    Friend WithEvents idate As DataGridViewTextBoxColumn
    Friend WithEvents visitor As DataGridViewTextBoxColumn
    Friend WithEvents plate As DataGridViewTextBoxColumn
    Friend WithEvents iqama As DataGridViewTextBoxColumn
    Friend WithEvents mobile As DataGridViewTextBoxColumn
    Friend WithEvents purpose As DataGridViewTextBoxColumn
    Friend WithEvents intime As DataGridViewTextBoxColumn
    Friend WithEvents outtime As DataGridViewTextBoxColumn
    Friend WithEvents staytime As DataGridViewTextBoxColumn
    Friend WithEvents vtype As DataGridViewTextBoxColumn
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents VISITORDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PLATEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents IQAMADataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MOBILEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PURPOSEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INTIMEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents OUTTIMEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents STAYTIMEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INTDOCNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents VTYPEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents STATUSDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
