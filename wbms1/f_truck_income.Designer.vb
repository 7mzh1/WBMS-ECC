<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class f_truck_income
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
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.trk_income_entry = New System.Windows.Forms.DataGridView()
        Me.slno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.docdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trailer_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trailer_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sledcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fromRoute = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.toRoute = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Driver_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Driver_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.no_of_trips = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Trip_rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.netamount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.d_docdate = New System.Windows.Forms.DateTimePicker()
        Me.b_save = New System.Windows.Forms.Button()
        Me.b_add = New System.Windows.Forms.Button()
        Me.b_delete = New System.Windows.Forms.Button()
        Me.b_search = New System.Windows.Forms.Button()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.drname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.drcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.trk_income_entry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'trk_income_entry
        '
        Me.trk_income_entry.AllowUserToAddRows = False
        Me.trk_income_entry.AllowUserToDeleteRows = False
        Me.trk_income_entry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.trk_income_entry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.slno, Me.docdate, Me.trailer_no, Me.trailer_code, Me.sledcode, Me.fromRoute, Me.toRoute, Me.Driver_code, Me.Driver_Name, Me.no_of_trips, Me.Trip_rate, Me.netamount, Me.remarks})
        Me.trk_income_entry.Location = New System.Drawing.Point(70, 143)
        Me.trk_income_entry.Name = "trk_income_entry"
        Me.trk_income_entry.Size = New System.Drawing.Size(1289, 446)
        Me.trk_income_entry.TabIndex = 0
        '
        'slno
        '
        Me.slno.HeaderText = "Serial #"
        Me.slno.Name = "slno"
        Me.slno.ReadOnly = True
        Me.slno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'docdate
        '
        Me.docdate.HeaderText = "Date"
        Me.docdate.Name = "docdate"
        '
        'trailer_no
        '
        DataGridViewCellStyle12.NullValue = "0"
        Me.trailer_no.DefaultCellStyle = DataGridViewCellStyle12
        Me.trailer_no.HeaderText = "Trailer #"
        Me.trailer_no.Name = "trailer_no"
        '
        'trailer_code
        '
        DataGridViewCellStyle13.NullValue = "0"
        Me.trailer_code.DefaultCellStyle = DataGridViewCellStyle13
        Me.trailer_code.HeaderText = "Trailer Code"
        Me.trailer_code.Name = "trailer_code"
        '
        'sledcode
        '
        DataGridViewCellStyle14.NullValue = "0"
        Me.sledcode.DefaultCellStyle = DataGridViewCellStyle14
        Me.sledcode.HeaderText = "Supplier/Customer"
        Me.sledcode.Name = "sledcode"
        Me.sledcode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'fromRoute
        '
        DataGridViewCellStyle15.NullValue = "0"
        Me.fromRoute.DefaultCellStyle = DataGridViewCellStyle15
        Me.fromRoute.HeaderText = "From Route"
        Me.fromRoute.Name = "fromRoute"
        Me.fromRoute.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.fromRoute.Width = 200
        '
        'toRoute
        '
        DataGridViewCellStyle16.NullValue = "0"
        Me.toRoute.DefaultCellStyle = DataGridViewCellStyle16
        Me.toRoute.HeaderText = "To Route"
        Me.toRoute.Name = "toRoute"
        '
        'Driver_code
        '
        DataGridViewCellStyle17.NullValue = "0"
        Me.Driver_code.DefaultCellStyle = DataGridViewCellStyle17
        Me.Driver_code.HeaderText = "Driver #"
        Me.Driver_code.Name = "Driver_code"
        '
        'Driver_Name
        '
        DataGridViewCellStyle18.NullValue = "0"
        Me.Driver_Name.DefaultCellStyle = DataGridViewCellStyle18
        Me.Driver_Name.HeaderText = "Driver Name"
        Me.Driver_Name.Name = "Driver_Name"
        '
        'no_of_trips
        '
        DataGridViewCellStyle19.NullValue = "0"
        Me.no_of_trips.DefaultCellStyle = DataGridViewCellStyle19
        Me.no_of_trips.HeaderText = "No of Trips"
        Me.no_of_trips.Name = "no_of_trips"
        '
        'Trip_rate
        '
        DataGridViewCellStyle20.NullValue = "0"
        Me.Trip_rate.DefaultCellStyle = DataGridViewCellStyle20
        Me.Trip_rate.HeaderText = "Trip Rate"
        Me.Trip_rate.Name = "Trip_rate"
        '
        'netamount
        '
        DataGridViewCellStyle21.NullValue = "0"
        Me.netamount.DefaultCellStyle = DataGridViewCellStyle21
        Me.netamount.HeaderText = "Total Amount"
        Me.netamount.Name = "netamount"
        '
        'remarks
        '
        DataGridViewCellStyle22.NullValue = "0"
        Me.remarks.DefaultCellStyle = DataGridViewCellStyle22
        Me.remarks.HeaderText = "Remarks"
        Me.remarks.Name = "remarks"
        '
        'd_docdate
        '
        Me.d_docdate.CustomFormat = "dd/MM/yyyy"
        Me.d_docdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.d_docdate.Location = New System.Drawing.Point(505, 77)
        Me.d_docdate.Name = "d_docdate"
        Me.d_docdate.Size = New System.Drawing.Size(200, 20)
        Me.d_docdate.TabIndex = 2
        '
        'b_save
        '
        Me.b_save.Location = New System.Drawing.Point(580, 615)
        Me.b_save.Name = "b_save"
        Me.b_save.Size = New System.Drawing.Size(243, 50)
        Me.b_save.TabIndex = 3
        Me.b_save.Text = "SAVE"
        Me.b_save.UseVisualStyleBackColor = True
        '
        'b_add
        '
        Me.b_add.Enabled = False
        Me.b_add.Location = New System.Drawing.Point(82, 615)
        Me.b_add.Name = "b_add"
        Me.b_add.Size = New System.Drawing.Size(243, 50)
        Me.b_add.TabIndex = 4
        Me.b_add.Text = "ADD"
        Me.b_add.UseVisualStyleBackColor = True
        '
        'b_delete
        '
        Me.b_delete.Location = New System.Drawing.Point(331, 615)
        Me.b_delete.Name = "b_delete"
        Me.b_delete.Size = New System.Drawing.Size(243, 50)
        Me.b_delete.TabIndex = 5
        Me.b_delete.Text = "DELETE"
        Me.b_delete.UseVisualStyleBackColor = True
        '
        'b_search
        '
        Me.b_search.Location = New System.Drawing.Point(722, 77)
        Me.b_search.Name = "b_search"
        Me.b_search.Size = New System.Drawing.Size(243, 20)
        Me.b_search.TabIndex = 6
        Me.b_search.Text = "SEARCH"
        Me.b_search.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.BackColor = System.Drawing.Color.LightBlue
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.drname, Me.drcode})
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.Location = New System.Drawing.Point(914, 198)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(317, 184)
        Me.ListView2.TabIndex = 246
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        Me.ListView2.Visible = False
        '
        'drname
        '
        Me.drname.Text = "Driver Name"
        Me.drname.Width = 111
        '
        'drcode
        '
        Me.drcode.Text = "Driver Code"
        Me.drcode.Width = 325
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1078, 615)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(243, 50)
        Me.Button1.TabIndex = 247
        Me.Button1.Text = "EXIT"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(829, 615)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(243, 50)
        Me.Button2.TabIndex = 248
        Me.Button2.Text = "CLEAR"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.LightBlue
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(312, 198)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(317, 184)
        Me.ListView1.TabIndex = 249
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Truck #"
        Me.ColumnHeader1.Width = 111
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Truck Code"
        Me.ColumnHeader2.Width = 325
        '
        'f_truck_income
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1371, 830)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.b_search)
        Me.Controls.Add(Me.b_delete)
        Me.Controls.Add(Me.b_add)
        Me.Controls.Add(Me.b_save)
        Me.Controls.Add(Me.d_docdate)
        Me.Controls.Add(Me.trk_income_entry)
        Me.Name = "f_truck_income"
        Me.Text = "Asset Productivity Tracker"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.trk_income_entry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents trk_income_entry As System.Windows.Forms.DataGridView
    Friend WithEvents d_docdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents b_save As System.Windows.Forms.Button
    Friend WithEvents b_add As System.Windows.Forms.Button
    Friend WithEvents b_delete As System.Windows.Forms.Button
    Friend WithEvents b_search As System.Windows.Forms.Button
    Friend WithEvents Sled As wbms1.sled
    Friend WithEvents ACMSLEDGERBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ACMSLEDGERTableAdapter As wbms1.sledTableAdapters.ACMSLEDGERTableAdapter
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents drname As System.Windows.Forms.ColumnHeader
    Friend WithEvents drcode As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents slno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents docdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents trailer_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents trailer_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sledcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fromRoute As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents toRoute As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Driver_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Driver_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents no_of_trips As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Trip_rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents netamount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
End Class
