Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System.Timers
Public Class f_gp
    Private comm As New CommManager()
    Private comm1 As New CommManager2()
    Private commett As New CommManagerMet1()
    Private commett1 As New CommManagerMet2()
    Private commetty As New CommManagerYNB1()
    Private commetty1 As New CommManagerYNB2()
    Private transType As String = String.Empty
    Dim constr, constrd As String
    Dim conn As New OracleConnection
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dadm As OracleDataAdapter
    Dim dpr As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Dim sql As String
    Dim vsql As String
    Dim dsql As String
    Public ds As New DataSet
    Dim ds1 As New DataSet
    Dim tmode As Integer
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dgasld As New OracleDataAdapter
    Dim dgssld As New DataSet
    Dim dach As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim dabuy As New OracleDataAdapter
    Dim dsbuy As New DataSet
    Dim omdasld As New OracleDataAdapter
    Dim omdssld As New DataSet
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim dadoc As New OracleDataAdapter
    Dim dsdoc As New DataSet
    Dim dfitm As New DataSet
    Dim dadr As New OracleDataAdapter
    Dim dsdr As New DataSet
    Dim dacdr As New OracleDataAdapter
    Dim dscdr As New DataSet
    Dim id() As String
    Dim typ() As String
    Dim nmbr() As Integer
    Dim mesg() As String
    Dim tkt() As Long

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles b_intime.Click

        Me.tb_intime.Text = DateAndTime.Now
    End Sub

    Private Sub b_outtime_Click(sender As Object, e As EventArgs) Handles b_outtime.Click
        Try
            Me.tb_outtime.Text = DateAndTime.Now
            Dim diff As TimeSpan = DateTime.Parse(tb_outtime.Text) - DateTime.Parse(tb_intime.Text)
            Dim days = Math.Floor(diff.Days)
            Dim hrs = Math.Floor(diff.Hours)
            Dim min = Math.Floor(diff.Minutes)
            If days <> 0 Then


                Me.tb_sttime.Text = days & " Day(s) " & hrs & " Hour(s) " & min & " Minute(s)"
            ElseIf days = 0 And hrs <> 0 Then
                Me.tb_sttime.Text = hrs & " Hour(s) " & min & " Minute(s)"
            ElseIf days = 0 And hrs = 0 And min <> 0 Then
                Me.tb_sttime.Text = min & " Minute(s)"

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub b_exit_Click(sender As Object, e As EventArgs) Handles b_exit.Click
        usermenu.Show()
        Me.Close()
    End Sub
    Private Sub clear_scr()
        'Me.rb_alq.Checked = False
        'Me.rb_out.Checked = False
        If rb_out.Checked = True Then
            Me.cb_plate.DropDownStyle = ComboBoxStyle.Simple
        End If

        If tmode <> 2 Then
            Me.tb_search.Text = ""
        End If
        Me.tb_iqama.Text = ""
        Me.tb_visitor.Text = ""
        Me.cb_plate.Text = ""
        Me.tb_mobile.Text = ""
        Me.tb_purpose.Text = ""
        Me.tb_intime.Text = ""
        Me.tb_outtime.Text = ""
        Me.tb_sttime.Text = ""
        Me.tb_visitor.ReadOnly = False
        Me.tb_iqama.ReadOnly = False
        Me.tb_mobile.ReadOnly = False

    End Sub
    Private Sub freeze_scr()
        Me.rb_alq.Enabled = False
        Me.rb_out.Enabled = False
        'Me.tb_search.Enabled = False
        Me.tb_iqama.Enabled = False
        Me.tb_visitor.Enabled = False
        Me.cb_plate.Enabled = False
        Me.tb_mobile.Enabled = False
        Me.tb_purpose.Enabled = False
        Me.tb_intime.Enabled = False
        Me.tb_outtime.Enabled = False
        Me.tb_sttime.Enabled = False
        Me.b_save.Enabled = False
    End Sub
    Private Sub unfreeze_scr()
        Me.rb_alq.Enabled = True
        Me.rb_out.Enabled = True
        'Me.tb_search.Enabled = False
        Me.tb_iqama.Enabled = True
        Me.tb_visitor.Enabled = True
        Me.cb_plate.Enabled = True
        Me.tb_mobile.Enabled = True
        Me.tb_purpose.Enabled = True
        Me.tb_intime.Enabled = True
        Me.tb_outtime.Enabled = True
        Me.tb_sttime.Enabled = True
        Me.b_save.Enabled = True
    End Sub
    Private Sub b_save_Click(sender As Object, e As EventArgs) Handles b_save.Click
        Try
            If tmode = 1 Then



                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                'Me.tb_ticketno.Text = 61000005
                'Me.tb_FIRSTQTY.Text = 1234
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd.i_gatepass"
                cmd.CommandType = CommandType.StoredProcedure
                'Try
                Dim ndt As Date = FormatDateTime(Me.d_newdate.Text, DateFormat.GeneralDate)
                cmd.Parameters.Add(New OracleParameter("pidate", OracleDbType.Date)).Value = ndt
                cmd.Parameters.Add(New OracleParameter("pvisitor", OracleDbType.Varchar2)).Value = Me.tb_visitor.Text
                cmd.Parameters.Add(New OracleParameter("pplate", OracleDbType.Varchar2)).Value = Me.cb_plate.Text
                cmd.Parameters.Add(New OracleParameter("piqama", OracleDbType.Varchar2)).Value = Me.tb_iqama.Text
                cmd.Parameters.Add(New OracleParameter("pmobile", OracleDbType.Varchar2)).Value = Me.tb_mobile.Text
                cmd.Parameters.Add(New OracleParameter("ppurpose", OracleDbType.Varchar2)).Value = Me.tb_purpose.Text
                cmd.Parameters.Add(New OracleParameter("pintime", OracleDbType.TimeStamp)).Value = DateTime.Parse(tb_intime.Text)
                'cmd.Parameters.Add(New OracleParameter("pouttime", OracleDbType.TimeStamp)).Value = DateTime.Parse(Me.tb_outtime.Text)
                'cmd.Parameters.Add(New OracleParameter("psttime", OracleDbType.Varchar2)).Value = Me.tb_sttime.Text
                If rb_alq.Checked Then
                    cmd.Parameters.Add(New OracleParameter("pvtype", OracleDbType.Char)).Value = "A"
                ElseIf rb_out.Checked Then
                    cmd.Parameters.Add(New OracleParameter("pvtype", OracleDbType.Char)).Value = "O"
                End If
                cmd.Parameters.Add(New OracleParameter("pstatus", OracleDbType.Char)).Value = "I"
                cmd.ExecuteNonQuery()
                conn.Close()
            ElseIf tmode = 2 Then
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd.u_gatepass"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("pintdocno", OracleDbType.Int32)).Value = Me.tb_search.Text
                cmd.Parameters.Add(New OracleParameter("pouttime", OracleDbType.TimeStamp)).Value = DateTime.Parse(Me.tb_outtime.Text)
                cmd.Parameters.Add(New OracleParameter("psttime", OracleDbType.Varchar2)).Value = Me.tb_sttime.Text
                cmd.Parameters.Add(New OracleParameter("pstatus", OracleDbType.Char)).Value = "O"
                cmd.ExecuteNonQuery()
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally
            MsgBox("Record Saved")
            Me.VISITORTableAdapter.Fill(Me.VIS.VISITOR)
            Me.dgv1.Refresh()
            'clear_scr()
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles b_in.Click
        If rb_alq.Checked = True Or rb_out.Checked = True Then
            unfreeze_scr()
            clear_scr()
            Me.b_outtime.Enabled = False
            Me.b_intime.Enabled = True
            tmode = 1
            If rb_alq.Checked = True Then
                Me.tb_visitor.ReadOnly = True
                Me.tb_iqama.ReadOnly = True
                Me.tb_mobile.ReadOnly = True
                Me.cb_plate.DropDownStyle = ComboBoxStyle.DropDownList
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.vmast"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    dssld.Clear()
                    dasld = New OracleDataAdapter(cmd)
                    dasld.TableMappings.Add("Table", "vm")
                    dasld.Fill(dssld)
                    cb_plate.DataSource = dssld.Tables("vm")
                    cb_plate.DisplayMember = dssld.Tables("vm").Columns("PLATE").ToString
                    cb_plate.ValueMember = dssld.Tables("vm").Columns("PLATE").ToString
                    'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Me.cb_plate.Text = "Enter Plate #"
                'cb_plate_SelectedIndexChanged(sender, e)
            End If
        Else
            MsgBox("Please select Vehicle Type")
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles b_out.Click
        tmode = 2
        Try

            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "Select " _
                         & "V.IDATE, V.VISITOR, V.PLATE," _
                         & " V.IQAMA, V.MOBILE, V.PURPOSE," _
                         & " V.INTIME, V.OUTTIME, V.STAYTIME," _
                         & " V.INTDOCNO, V.VTYPE, V.STATUS" _
                         & " from VISITOR V where V.INTDOCNO = " & Me.tb_search.Text
            clear_scr()
            da = New OracleDataAdapter(sql, conn)

            Dim ds As New DataSet
            da.Fill(ds)
            conn.Close()
            If ds.Tables(0).Rows.Count > 0 Then
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IDATE"))) Then
                    Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("IDATE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VISITOR"))) Then
                    Me.tb_visitor.Text = ds.Tables(0).Rows(0).Item("VISITOR")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PLATE"))) Then
                    Me.cb_plate.Text = ds.Tables(0).Rows(0).Item("PLATE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IQAMA"))) Then
                    Me.tb_iqama.Text = ds.Tables(0).Rows(0).Item("IQAMA")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MOBILE"))) Then
                    Me.tb_mobile.Text = ds.Tables(0).Rows(0).Item("MOBILE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PURPOSE"))) Then
                    Me.tb_purpose.Text = ds.Tables(0).Rows(0).Item("PURPOSE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTIME"))) Then
                    Me.tb_intime.Text = ds.Tables(0).Rows(0).Item("INTIME")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OUTTIME"))) Then
                    Me.tb_outtime.Text = ds.Tables(0).Rows(0).Item("OUTTIME")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("STAYTIME"))) Then
                    Me.tb_sttime.Text = ds.Tables(0).Rows(0).Item("STAYTIME")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VTYPE"))) Then
                    If ds.Tables(0).Rows(0).Item("VTYPE") = "A" Then
                        Me.rb_alq.Checked = True
                    ElseIf ds.Tables(0).Rows(0).Item("VTYPE") = "O" Then
                        Me.rb_out.Checked = True
                    End If
                End If
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                'Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                'Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                'Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                'Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                'Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                'End If
            End If
            If Me.tb_sttime.Text <> "" Then
                freeze_scr()
            Else
                Me.b_save.Enabled = True
                Me.b_outtime.Enabled = True
                Me.b_intime.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try


    End Sub

    Private Sub cb_plate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_plate.SelectedIndexChanged
        If Me.cb_plate.SelectedIndex <> -1 Then
            If tmode <> 2 Then

                Try
                    Me.cb_plate.Text = Me.cb_plate.SelectedValue.ToString
                    Dim foundrow() As DataRow
                    Dim expression As String = "PLATE = '" & Me.cb_plate.Text & "'" & ""
                    foundrow = dssld.Tables("vm").Select(expression)
                    If foundrow.Count > 0 Then
                        If Not IsDBNull(foundrow(0).ItemArray(1)) Then
                            Me.tb_visitor.Text = foundrow(0).ItemArray(1)
                        End If
                        If Not IsDBNull(foundrow(0).ItemArray(3)) Then
                            Me.tb_iqama.Text = foundrow(0).ItemArray(3)
                        End If
                        If Not IsDBNull(foundrow(0).ItemArray(4)) Then
                            Me.tb_mobile.Text = foundrow(0).ItemArray(4)
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub rb_alq_CheckedChanged(sender As Object, e As EventArgs) Handles rb_alq.CheckedChanged
        If tmode = 1 Then
            clear_scr()
        End If
    End Sub

    Private Sub rb_out_CheckedChanged(sender As Object, e As EventArgs) Handles rb_out.CheckedChanged
        If tmode = 1 Then
            clear_scr()
            Button1_Click_1(sender, e)
        End If

    End Sub



    Private Sub f_gp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'VIS.VISITOR' table. You can move, or remove it, as needed.
        Me.VISITORTableAdapter.Fill(Me.VIS.VISITOR)
        'TODO: This line of code loads data into the 'DataSet6.VISITOR' table. You can move, or remove it, as needed.
        'Me.VISITORTableAdapter.Fill(Me.DataSet6.VISITOR)


        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource &
                          ";User Id=" + connparam.username &
                          ";Password=" + connparam.paswwd &
                          ";Pooling=false"
    End Sub
End Class