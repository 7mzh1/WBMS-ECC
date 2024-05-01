Imports System.Data
'Imports System.Data.OracleClient
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Public Class VALUATION
    Private comm As New CommManager()
    Private transType As String = String.Empty
    Dim constr, constrd As String
    Dim conn As New OracleConnection
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dpr As OracleDataAdapter
    Dim sql As String
    Public ds As New DataSet
    Dim ds1 As New DataSet
    Dim tmode As Integer
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim dfitm As New DataSet
    Dim dadr As New OracleDataAdapter
    Dim dsdr As New DataSet



    Private Sub WBMS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
        Catch ex As Exception
            MsgBox(ex.Message)
            comm.ClosePort()
            compselect.Show()
        End Try
        comm.ClosePort()
        compselect.Show()
    End Sub
    Private Sub WBMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd
        'cmbloading()
        comm.CurrentTransmissionType = CommManager.TransmissionType.Text
        Me.tb_FIELD1.Text = glbvar.userid
        tb_edittktn.Hide()
        b_edittktn.Hide()
    End Sub
    Private Sub b_newveh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_newveh.Click
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS WBM WHERE INOUTTYPE = 'I' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

        'If dstk.Tables(0).Rows.Count = 0 Then
        'tb_container.Focus()

        'Me.tb_ticketno.Focus()
        'Else

        Try
            If cb_sledcode.Visible = False Then
                cb_sledcode.Show()
            End If
            If tb_sledesc.Visible = False Then
                tb_sledesc.Show()
            End If
            If cb_fritem.Visible = True Then
                cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                tb_fritemdesc.Hide()
            End If
            Label6.Text = "Supplier"
            Label7.Text = "Product"
            cmbloading()
            Me.cb_sledcode.Text = "224010 001 0554"
            Me.tb_sledesc.Text = "Other Supplier"
            Me.tb_itemdesc.Text = "00000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            cb_inouttype.SelectedValue = "I"
            'Me.cb_sledcode.Text = "224010 00"
            b_genis.Visible = False
            b_gends.Visible = False
            b_genst.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_outveh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_outveh.Click
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS WBM WHERE INOUTTYPE = 'O' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Try
            If cb_sledcode.Visible = False Then
                cb_sledcode.Show()
            End If
            If tb_sledesc.Visible = False Then
                tb_sledesc.Show()
            End If
            If cb_fritem.Visible = True Then
                cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                tb_fritemdesc.Hide()
            End If
            Label6.Text = "Customer"
            Label7.Text = "Product"
            cmbloading1()
            Me.cb_sledcode.Text = "113040 001 0074"
            Me.tb_sledesc.Text = "Other Customer"
            Me.tb_itemdesc.Text = "00000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            cb_inouttype.SelectedValue = "O"
            b_genis.Visible = False
            b_gends.Visible = False
            b_genst.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_stransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_stransfer.Click
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS WBM WHERE INOUTTYPE = 'T' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Try
            cb_sledcode.Hide()
            tb_sledesc.Hide()
            If cb_fritem.Visible = False Then
                cb_fritem.Show()
            End If
            If tb_fritemdesc.Visible = False Then
                tb_fritemdesc.Show()
            End If
            Label6.Text = "From Item"
            Label7.Text = "To Item"
            cmbloading2()
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            cb_inouttype.SelectedValue = "T"
            b_genis.Visible = False
            b_gends.Visible = False
            b_genst.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_firstwt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_firstwt.Click
        Me.tb_FIRSTQTY.Text = Me.rtbDisplay.Text
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_TIMEIN.Text = Now.ToShortTimeString
        Me.tb_ticketno.Focus()
        'If divcd = "QNB" Then
        'If cb_inouttype.SelectedValue = "I" Then
        'Me.tb_ticketno.Text = 31
        'ElseIf cb_inouttype.SelectedValue = "O" Then
        'Me.tb_ticketno.Text = 32
        'ElseIf cb_inouttype.SelectedValue = "S" Then
        'Me.tb_ticketno.Text = 36
        'ElseIf cb_inouttype.SelectedValue = "T" Then
        'Me.tb_ticketno.Text = 35
        'End If
        'End If
    End Sub
    Private Sub b_secondwt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_secondwt.Click
        Try
            Me.tb_SECONDQTY.Text = Me.rtbDisplay.Text
            Me.tb_DATEOUT.Text = Today.Date
            Me.tb_TIMOUT.Text = Now.ToShortTimeString
            Dim sq As Integer = Convert.ToDecimal(Trim(Me.tb_SECONDQTY.Text))
            If cb_inouttype.Text = "I" Then
                Me.tb_QTY.Text = CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text)
            ElseIf cb_inouttype.Text = "O" Then
                Me.tb_QTY.Text = sq - CDec(Me.tb_FIRSTQTY.Text) - CDec(Me.tb_DEDUCTIONWT.Text)
            ElseIf cb_inouttype.Text = "T" Then
                Me.tb_QTY.Text = CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text)
            ElseIf cb_inouttype.Text = "S" Then
                Me.tb_QTY.Text = CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cmbloading()
        Dim arry As New ArrayList
        With arry
            .Add(New cmbload("", ""))
            .Add(New cmbload("Incoming Goods", "I"))
            .Add(New cmbload("Outgoing Goods", "O"))
            .Add(New cmbload("Stock Transfer", "T"))
            .Add(New cmbload("Scale Only Tickets", "S"))
        End With
        With cb_inouttype
            .DataSource = arry
            .DisplayMember = "Names"
            .ValueMember = "Ids"
        End With
        'Supplier
        'Dim constr As String = My.Settings.Item("ConnString")
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.sledmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dssld.Clear()
            dasld = New OracleDataAdapter(cmd)
            dasld.TableMappings.Add("Table", "sled")
            dasld.Fill(dssld)
            cb_sledcode.DataSource = dssld.Tables("sled")
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsitm.Clear()
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.drmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsdr.Clear()
            dadr = New OracleDataAdapter(cmd)
            dadr.TableMappings.Add("Table", "drv")
            dadr.Fill(dsdr)
            cb_dcode.DataSource = dsdr.Tables("drv")
            cb_dcode.DisplayMember = dsdr.Tables("drv").Columns("EMPNAME").ToString
            cb_dcode.ValueMember = dsdr.Tables("drv").Columns("EMPCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub cmbloading1()
        Dim arry As New ArrayList
        With arry
            .Add(New cmbload("", ""))
            .Add(New cmbload("Incoming Goods", "I"))
            .Add(New cmbload("Outgoing Goods", "O"))
            .Add(New cmbload("Stock Transfer", "T"))
            .Add(New cmbload("Scale Only Tickets", "S"))
        End With
        With cb_inouttype
            .DataSource = arry
            .DisplayMember = "Names"
            .ValueMember = "Ids"
        End With
        'Supplier
        'Dim constr As String = My.Settings.Item("ConnString")
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.custmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dssld.Clear()
            dasld = New OracleDataAdapter(cmd)
            dasld.TableMappings.Add("Table", "sled")
            dasld.Fill(dssld)
            cb_sledcode.DataSource = dssld.Tables("sled")
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dsitm.Clear()
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub cmbloading2()
        Dim arry As New ArrayList
        With arry
            .Add(New cmbload("", ""))
            .Add(New cmbload("Incoming Goods", "I"))
            .Add(New cmbload("Outgoing Goods", "O"))
            .Add(New cmbload("Stock Transfer", "T"))
            .Add(New cmbload("Scale Only Tickets", "S"))
        End With
        With cb_inouttype
            .DataSource = arry
            .DisplayMember = "Names"
            .ValueMember = "Ids"
        End With
        'Supplier
        'Dim constr As String = My.Settings.Item("ConnString")
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dfitm.Clear()
            daitm.Fill(dfitm)
            cb_fritem.DataSource = dfitm.Tables("itm")
            cb_fritem.DisplayMember = dfitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_fritem.ValueMember = dfitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dsitm.Clear()
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub
    Private Sub b_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_connect.Click
        Try
            comm.Parity = "None"
            comm.StopBits = 1
            comm.DataBits = 7
            comm.BaudRate = 9600
            comm.DisplayWindow = rtbDisplay
            comm.OpenPort()
            b_Disconnect.Visible = True
            b_connect.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub

    Private Sub b_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_edit.Click
        Try

            tmode = 2
            Me.tb_ticketno.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_ticketno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_ticketno.LostFocus
        If tmode = 1 Then
            'Dim tkt As Exception
            'check for duplicate
            'Dim constr As String = My.Settings.Item("ConnString")
            Try
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
            sql = "SELECT   WBM.TICKETNO" _
                & "  FROM   STWBMIBDS WBM" _
                & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text _
                & " and status in (1,2,3)"

            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            Try

                da.Fill(dstk)
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

            'If dstk.Tables(0).Rows.Count = 0 Then
            'tb_container.Focus()

            'Me.tb_ticketno.Focus()
            'Else
            Try
                If dstk.Tables(0).Rows.Count > 0 Then
                    MsgBox("Ticket number Already used")
                    Me.tb_ticketno.Text = "0"
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            'If dstk.Tables(0).Rows.Count > 0 Then
            'Me.tb_ticketno.Focus()
            'End If
            conn.Close()
            'check the ticketnumber for whther within range
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "curspkg_join.tktrng"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
            cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
            If cb_inouttype.SelectedValue = "I" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "IWB"
            ElseIf cb_inouttype.SelectedValue = "O" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "DLS"
            ElseIf cb_inouttype.SelectedValue = "T" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SNA"
            ElseIf cb_inouttype.SelectedValue = "S" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCL"
            End If
            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                Dim dsrng As New DataSet
                Dim darng As New OracleDataAdapter(cmd)
                darng.TableMappings.Add("Table", "tktrng")
                darng.Fill(dsrng)
                If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                    Me.tb_container.Focus()
                Else
                    MsgBox("Ticket number not in range should be within " & dsrng.Tables("tktrng").Rows(0).Item("STARTNO") & " - " & dsrng.Tables("tktrng").Rows(0).Item("ENDNO"))
                    Me.tb_ticketno.Focus()
                End If
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try

        ElseIf tmode = 2 Then

            'Dim constr As String = My.Settings.Item("ConnString")
            Try
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                     & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                     & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,REMARKS ,IBDSNO," _
                     & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS" _
                     & " from STWBMIBDS where TICKETNO = " & Me.tb_ticketno.Text _
                     & " and status in (1,2,3)"
                da = New OracleDataAdapter(sql, conn)
                'da.TableMappings.Add("Table", "mlt")
                Dim ds As New DataSet
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then

                    Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                    Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                    Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                    Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                        Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                        Me.tb_container.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                    End If
                    If Me.cb_inouttype.Text = "T" Then
                        cb_sledcode.Hide()
                        tb_sledesc.Hide()
                        cb_fritem.Show()
                        tb_fritemdesc.Show()
                        Label6.Text = "From Item"
                        Label7.Text = "To Item"
                        Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                        Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                        Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                    ElseIf Me.cb_inouttype.Text = "I" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        Label6.Text = "Supplier"
                        Label7.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        Label6.Text = "Customer"
                        Label7.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "S" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        Label6.Text = "Supplier"
                        Label7.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    End If
                    Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                    Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                    Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")

                    Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                    Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                    Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                        Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                        Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")

                        Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                        Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                        Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("FIRSTQTY"))) Then
                        Me.tb_FIRSTQTY.Text = ds.Tables(0).Rows(0).Item("FIRSTQTY")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SECONDQTY"))) Then
                        Me.tb_SECONDQTY.Text = ds.Tables(0).Rows(0).Item("SECONDQTY")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("QTY"))) Then
                        Me.tb_QTY.Text = ds.Tables(0).Rows(0).Item("QTY")
                    End If
                    Me.tb_DATEIN.Text = ds.Tables(0).Rows(0).Item("DATEIN")
                    Me.tb_TIMEIN.Text = ds.Tables(0).Rows(0).Item("TIMEIN")
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                        Me.tb_DATEOUT.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TIMOUT"))) Then
                        Me.tb_TIMOUT.Text = ds.Tables(0).Rows(0).Item("TIMOUT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DEDUCTIONWT"))) Then
                        Me.tb_DEDUCTIONWT.Text = ds.Tables(0).Rows(0).Item("DEDUCTIONWT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PACKDED"))) Then
                        Me.tb_packded.Text = ds.Tables(0).Rows(0).Item("PACKDED")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DED"))) Then
                        Me.tb_ded.Text = ds.Tables(0).Rows(0).Item("DED")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                        Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TOTALPRICE"))) Then
                        Me.tb_TOTALPRICE.Text = ds.Tables(0).Rows(0).Item("TOTALPRICE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("REMARKS"))) Then
                        Me.tb_comments.Text = ds.Tables(0).Rows(0).Item("REMARKS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IBDSNO"))) Then
                        Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("IBDSNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTIBDSNO"))) Then
                        Me.tb_INTIBDSNO.Text = ds.Tables(0).Rows(0).Item("INTIBDSNO")
                    End If
                    Me.tb_STATUS.Text = ds.Tables(0).Rows(0).Item("STATUS")
                    'update data table in case of multiple items.
                    Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                    & "FIRSTQTY, SECONDQTY, QTY" _
                    & " from(STWBMIBDS_MULTI)" _
                    & " where(INTDOCNO =" & Me.Tb_intdocno.Text & ")"
                    Dim da1 As New OracleDataAdapter(sql, conn)
                    da1.Fill(ds1)
                    If Me.tb_IBDSNO.Text = "" Then
                        If Me.cb_inouttype.Text = "I" Then
                            Me.b_genis.Visible = True
                            Me.b_gends.Visible = False
                            Me.b_genst.Visible = False
                        ElseIf Me.cb_inouttype.Text = "O" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = True
                            Me.b_genst.Visible = False
                        ElseIf Me.cb_inouttype.Text = "T" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            Me.b_genst.Visible = True
                        End If
                    Else
                        Me.b_gends.Visible = False
                        Me.b_genis.Visible = False
                        Me.b_genst.Visible = False
                    End If
                    Me.b_firstwt.Enabled = False
                    If Me.tb_SECONDQTY.Text = 0 Then
                        Me.b_secondwt.Enabled = True
                    End If


                    'conn = New OracleConnection(constr)
                    'If conn.State = ConnectionState.Closed Then
                    'conn.Open()
                    'End If
                    'Dim cmd As New OracleCommand
                    'cmd.Connection = conn
                    'cmd.Parameters.Clear()
                    'cmd.CommandText = "curspkg_join.itmmst"
                    'cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                    'cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
                    'cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                    'Try
                    'daitm = New OracleDataAdapter(cmd)
                    'daitm.TableMappings.Add("Table", "itm")
                    'daitm.Fill(dsitm)
                    'cb_itemcode.DataSource = dsitm.Tables("itm")
                    'cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
                    'cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
                    ''cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

                    'Catch ex As Exception
                    'MsgBox(ex.Message)
                    'End Try
                    'sl_item_driv_load()
                    conn.Close()
                Else
                    MsgBox("No Records Found for this ticket #", MsgBoxStyle.Information)
                    Me.tb_ticketno.Focus()
                End If



                If cb_inouttype.Text = "I" Then
                    glbvar.temp_suppcode = Me.cb_sledcode.Text
                    glbvar.temp_suppdesc = Me.tb_sledesc.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    sl_item_driv_load()
                ElseIf cb_inouttype.Text = "O" Then
                    glbvar.temp_suppcode = Me.cb_sledcode.Text
                    glbvar.temp_suppdesc = Me.tb_sledesc.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    cust_item_driv_load()
                ElseIf cb_inouttype.Text = "T" Then
                    glbvar.temp_suppcode = Me.cb_fritem.Text
                    glbvar.temp_suppdesc = Me.tb_fritemdesc.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    tran_item_driv_load()
                End If


                'If cb_inouttype.Text = "I" Then
                '    cmbloading()
                'ElseIf cb_inouttype.Text = "O" Then
                '    cmbloading1()
                'ElseIf cb_inouttype.Text = "T" Then
                '    cmbloading2()
                'End If

            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try
        Else
            MsgBox("Please select New or edit or cancel")
        End If 'tmode enddif

    End Sub

    Private Sub b_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_save.Click
        If tmode = 1 Then
            If Me.Tb_intdocno.Text = "" Then
                'Dim constr As String = My.Settings.Item("ConnString")
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                'Me.tb_ticketno.Text = 61000005
                'Me.tb_FIRSTQTY.Text = 1234
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd.gen_wbms_i"
                cmd.CommandType = CommandType.StoredProcedure
                Try
                    cmd.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.SelectedValue
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                    cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                    'If IsDBNull(Me.tb_container.Text) Then
                    'Me.tb_container.Text = ""
                    'Else
                    '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                    'End If
                    cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = Me.tb_transporter.Text
                    If cb_inouttype.SelectedValue = "T" Then
                        cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = "224010 001"
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = "224010 001 0554"
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = "Other Supplier"
                    Else
                        cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                    End If
                    cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                    cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                    cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

                    If Me.cb_inouttype.SelectedValue <> "T" Then
                        cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
                        cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = "Dummy"
                        cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = "00000"
                    ElseIf Me.cb_inouttype.SelectedValue = "T" Then

                        cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
                        cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
                        cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()
                    End If
                    cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = Me.tb_numberofpcs.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                    cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = Me.tb_NATIONALITY.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = Me.tb_DRIVINGLICNO.Text
                    cmd.Parameters.Add(New OracleParameter("pFIRSTQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_FIRSTQTY.Text)
                    Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
                    cmd.Parameters.Add(New OracleParameter("pDATEIN", OracleDbType.Date)).Value = dtin 'Convert.ToDateTime(Me.tb_DATEIN.Text)
                    cmd.Parameters.Add(New OracleParameter("pTIMEIN", OracleDbType.Varchar2)).Value = Me.tb_TIMEIN.Text
                    cmd.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
                    cmd.Parameters.Add(New OracleParameter("pAPPDATE0", OracleDbType.Date)).Value = Today
                    cmd.Parameters.Add(New OracleParameter("pFIELD1", OracleDbType.Varchar2)).Value = glbvar.userid
                    cmd.Parameters.Add(New OracleParameter("pSTATUS", OracleDbType.Varchar2)).Value = 1
                    cmd.Parameters.Add(New OracleParameter("pDEDUCTIONWT", OracleDbType.Int32)).Value = CInt(Me.tb_DEDUCTIONWT.Text)
                    cmd.Parameters.Add(New OracleParameter("pPACKDED", OracleDbType.Int32)).Value = CInt(Me.tb_packded.Text)
                    cmd.Parameters.Add(New OracleParameter("pDED", OracleDbType.Int32)).Value = CInt(Me.tb_ded.Text)
                    cmd.Parameters.Add(New OracleParameter("pprice", OracleDbType.Int32)).Value = CDec(Me.tb_PRICETON.Text)
                    cmd.Parameters.Add(New OracleParameter("ptotprice", OracleDbType.Int32)).Value = CDec(Me.tb_TOTALPRICE.Text)
                    cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Direction = ParameterDirection.Output
                    'Try
                    cmd.ExecuteNonQuery()
                    'Dim vint As Decimal
                    'vint = cmd.Parameters("pINTDOCNO").Value.ToString  'CDec(cmd.Parameters("pINTDOCNO").Value)
                    Me.Tb_intdocno.Text = cmd.Parameters("pINTDOCNO").Value.ToString
                    conn.Close()
                    Me.b_firstwt.Enabled = False
                    MsgBox("Record Saved")
                    'clear_scr()
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try
            End If
        ElseIf tmode = 2 Then
            'Dim constr As String = My.Settings.Item("ConnString")
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_u"
            cmd.CommandType = CommandType.StoredProcedure
            Try
                cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = Me.tb_transporter.Text
                cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

                cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
                cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
                cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()

                cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = CInt(Me.tb_numberofpcs.Text)

                cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = Me.tb_NATIONALITY.Text
                cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = Me.tb_DRIVINGLICNO.Text
                cmd.Parameters.Add(New OracleParameter("pSECONDQTY", OracleDbType.Decimal)).Value = CDec(Trim(Me.tb_SECONDQTY.Text))
                If Me.tb_SECONDQTY.Text = 0 Then
                    Me.tb_DATEOUT.Text = Today.Date
                End If
                Dim dto As Date = FormatDateTime(Me.tb_DATEOUT.Text, DateFormat.GeneralDate)
                cmd.Parameters.Add(New OracleParameter("pDATEOUT", OracleDbType.Date)).Value = dto
                cmd.Parameters.Add(New OracleParameter("pTIMOUT", OracleDbType.Varchar2)).Value = Me.tb_TIMOUT.Text
                If Me.tb_DEDUCTIONWT.Text <> "" Then
                    cmd.Parameters.Add(New OracleParameter("pDEDUCTIONWT", OracleDbType.Decimal)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                Else
                    cmd.Parameters.Add(New OracleParameter("pDEDUCTIONWT", OracleDbType.Decimal)).Value = 0.0
                End If
                If Me.tb_packded.Text <> "" Then
                    cmd.Parameters.Add(New OracleParameter("pPACKDED", OracleDbType.Decimal)).Value = CDec(Me.tb_packded.Text)
                Else
                    cmd.Parameters.Add(New OracleParameter("pPACKDED", OracleDbType.Decimal)).Value = 0.0
                End If
                If Me.tb_ded.Text <> "" Then
                    cmd.Parameters.Add(New OracleParameter("pDED", OracleDbType.Decimal)).Value = CDec(Me.tb_ded.Text)
                Else
                    cmd.Parameters.Add(New OracleParameter("pDED", OracleDbType.Decimal)).Value = 0.0
                End If
                cmd.Parameters.Add(New OracleParameter("pQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_QTY.Text)
                cmd.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
                cmd.Parameters.Add(New OracleParameter("pAPPDATE1", OracleDbType.Date)).Value = Now
                cmd.Parameters.Add(New OracleParameter("pFIELD2", OracleDbType.Varchar2)).Value = glbvar.userid
                If Me.tb_PRICETON.Text <> "" Then
                    cmd.Parameters.Add(New OracleParameter("pPRICE", OracleDbType.Decimal)).Value = CDec(Me.tb_PRICETON.Text)
                Else
                    cmd.Parameters.Add(New OracleParameter("pPRICE", OracleDbType.Decimal)).Value = 0.0
                End If
                If Me.tb_TOTALPRICE.Text <> "" Then
                    cmd.Parameters.Add(New OracleParameter("pTOTPRICE", OracleDbType.Decimal)).Value = CDec(Me.tb_TOTALPRICE.Text)
                Else
                    cmd.Parameters.Add(New OracleParameter("pTOTALPRICE", OracleDbType.Decimal)).Value = 0.0
                End If
                cmd.ExecuteNonQuery()
                conn.Close()
                If itmalloc = True Then
                    ReDim glbvar.pindocn(glbvar.intiem.Count - 1)
                    ReDim glbvar.ptktno(glbvar.intiem.Count - 1)
                    ReDim glbvar.pino(glbvar.intiem.Count - 1)
                    For i = 0 To glbvar.intiem.Count - 1
                        glbvar.pindocn(i) = CInt(Me.Tb_intdocno.Text)
                        glbvar.ptktno(i) = CInt(Me.tb_ticketno.Text)
                        glbvar.pino(i) = Me.cb_inouttype.Text
                    Next
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    Try
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.gen_wbms_uArr"
                        cmd.CommandType = CommandType.StoredProcedure
                        'cmd.ArrayBindCount = glbvar.intiem.Count
                        Dim pINTDOCNO As OracleParameter = New OracleParameter(":p1", OracleDbType.Int32)
                        pINTDOCNO.Direction = ParameterDirection.Input
                        pINTDOCNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pINTDOCNO.Value = glbvar.pindocn

                        Dim pINOUTTYPE As OracleParameter = New OracleParameter("p2:", OracleDbType.Char)
                        pINOUTTYPE.Direction = ParameterDirection.Input
                        pINOUTTYPE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pINOUTTYPE.Value = glbvar.pino

                        Dim pTICKETNO As OracleParameter = New OracleParameter(":p3", OracleDbType.Int32)
                        pTICKETNO.Direction = ParameterDirection.Input
                        pTICKETNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pTICKETNO.Value = glbvar.ptktno

                        Dim pINTITEMCODE As OracleParameter = New OracleParameter("p4", OracleDbType.Int32)
                        pINTITEMCODE.Direction = ParameterDirection.Input
                        pINTITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pINTITEMCODE.Value = glbvar.intiem

                        Dim pITEMCODE As OracleParameter = New OracleParameter("p5", OracleDbType.Varchar2)
                        pITEMCODE.Direction = ParameterDirection.Input
                        pITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pITEMCODE.Value = glbvar.itmcde

                        Dim pITEMDESC As OracleParameter = New OracleParameter(":p6", OracleDbType.Varchar2)
                        pITEMDESC.Direction = ParameterDirection.Input
                        pITEMDESC.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pITEMDESC.Value = glbvar.itemdes

                        Dim pFIRSTQTY As OracleParameter = New OracleParameter(":p7", OracleDbType.Decimal)
                        pFIRSTQTY.Direction = ParameterDirection.Input
                        pFIRSTQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pFIRSTQTY.Value = glbvar.pfswt

                        Dim pSECONDQTY As OracleParameter = New OracleParameter("p8", OracleDbType.Decimal)
                        pSECONDQTY.Direction = ParameterDirection.Input
                        pSECONDQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pSECONDQTY.Value = glbvar.pscwt

                        Dim pQTY As OracleParameter = New OracleParameter(":p9", OracleDbType.Decimal)
                        pQTY.Direction = ParameterDirection.Input
                        pQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pQTY.Value = glbvar.pqty

                        Dim pprice As OracleParameter = New OracleParameter(":p10", OracleDbType.Decimal)
                        pprice.Direction = ParameterDirection.Input
                        pprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pprice.Value = glbvar.ppricekg

                        Dim ptotprice As OracleParameter = New OracleParameter(":p11", OracleDbType.Decimal)
                        ptotprice.Direction = ParameterDirection.Input
                        ptotprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        ptotprice.Value = glbvar.prate

                        cmd.Parameters.Add(pINTDOCNO)
                        cmd.Parameters.Add(pINOUTTYPE)
                        cmd.Parameters.Add(pTICKETNO)
                        cmd.Parameters.Add(pINTITEMCODE)
                        cmd.Parameters.Add(pITEMCODE)
                        cmd.Parameters.Add(pITEMDESC)
                        cmd.Parameters.Add(pFIRSTQTY)
                        cmd.Parameters.Add(pSECONDQTY)
                        cmd.Parameters.Add(pQTY)
                        cmd.Parameters.Add(pprice)
                        cmd.Parameters.Add(ptotprice)
                        cmd.ExecuteNonQuery()
                        'multi_itm.DataGridView1.Rows.Clear()
                        'cmd.Parameters.Clear()
                        'clear_scr()
                    Catch ex As Exception
                        MsgBox(ex.Message.ToString)
                    End Try
                    'conn.Close()
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
                conn.Close()
            Finally
                MsgBox("Record Saved")
                conn.Close()
            End Try
        End If 'tmode
        itmalloc = False
        'glbvar.intiem.Initialize()
        'glbvar.itemdes.Initialize()
        'glbvar.itmcde.Initialize()
        'glbvar.ptktno.Initialize()
        'glbvar.pfswt.Initialize()
        'glbvar.pscwt.Initialize()
        'glbvar.pqty.Initialize()
    End Sub

    Private Sub b_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Disconnect.Click
        Try
            comm.ClosePort()
            b_Disconnect.Visible = False
            b_connect.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cb_sledcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_sledcode.LostFocus
        cb_itemcode.Focus()
    End Sub

    Private Sub cb_sledcode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_sledcode.SelectedIndexChanged
        If Me.cb_sledcode.SelectedIndex <> -1 Then
            Me.tb_sledesc.Text = Me.cb_sledcode.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.cb_sledcode.Text & "'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            If foundrow.Count > 1 Then
                MsgBox("More number of records found for the supplier")
            Else
                For i = 0 To foundrow.Count - 1
                    Me.Tb_accountcode.Text = foundrow(0).Item("ACCOUNTCODE").ToString
                Next
            End If
        End If
    End Sub

    Private Sub cb_itemcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_itemcode.LostFocus
        cb_dcode.Focus()
    End Sub

    Private Sub cb_itemcode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_itemcode.SelectedIndexChanged
        Try
            Me.tb_TOTALPRICE.Text = 0
            If Me.cb_itemcode.SelectedIndex <> -1 Then
                Me.tb_itemdesc.Text = Me.cb_itemcode.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "ITEMCODE = '" & Me.tb_itemdesc.Text & "'" & ""
                foundrow = dsitm.Tables("itm").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the item")
                Else
                    For i = 0 To foundrow.Count - 1
                        Me.Tb_intitemcode.Text = foundrow(0).Item("INTITEMCODE").ToString
                    Next
                End If
                If Tb_intitemcode.Text <> "" Then
                    sql = " select nvl(min_price/1000,0) price,nvl(M.INCLPRILST,'N') INCLPRILST,nvl(M.INACTIVE,'Y') inactive," _
                        & " NVL(M.BPRICE/1000,0) bprice,NVL(M.PRICEPCT/100,0) pct,nvl(min_price/1000,0)*NVL(M.PRICEPCT/100,0) addn " _
                        & " FROM   stitmratehd h, stitmrate t,smitem m " _
                        & " WHERE   h.intrateno = t.intrateno " _
                        & " and h.intrateno = (select max(intrateno) from stitmratehd) " _
                        & " and m.intitemcode = t.intitemcode " _
                        & " AND t.intitemcode = " & Me.Tb_intitemcode.Text
                    dpr = New OracleDataAdapter(sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)
                    'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")
                    If dp.Tables(0).Rows.Count > 0 Then
                        If dp.Tables(0).Rows(0).Item("inclprilst") = "Y" Then
                            If dp.Tables(0).Rows(0).Item("price") = 0 Then
                                Me.tb_TOTALPRICE.Text = 0
                            Else
                                Me.tb_TOTALPRICE.Text = dp.Tables(0).Rows(0).Item("price")
                                Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")
                                Me.Tb_perc.Text = Convert.ToDecimal(Me.Tb_perc.Text) + Convert.ToDecimal(Me.tb_TOTALPRICE.Text)
                            End If
                        ElseIf dp.Tables(0).Rows(0).Item("inclprilst") = "N" Then

                            If dp.Tables(0).Rows(0).Item("bprice") = 0 Then
                                Me.tb_TOTALPRICE.Text = 0
                            Else
                                Me.tb_TOTALPRICE.Text = dp.Tables(0).Rows(0).Item("bprice")
                                Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")
                                Me.Tb_perc.Text = Me.Tb_perc.Text + Me.tb_TOTALPRICE.Text
                            End If
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub
    Private Sub cb_fritem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_fritem.SelectedIndexChanged
        Try
            If Me.cb_fritem.SelectedIndex <> -1 Then
                Me.tb_fritemdesc.Text = Me.cb_fritem.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "ITEMCODE = '" & Me.tb_fritemdesc.Text & "'" & ""
                foundrow = dfitm.Tables("itm").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the item")
                Else
                    For i = 0 To foundrow.Count - 1
                        Me.tb_frintitem.Text = foundrow(0).Item("INTITEMCODE").ToString
                    Next
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub

    Private Sub b_genis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_genis.Click
        'Check all required fileds for IB are filled
        check_cancel()
        check_item()
        If Me.tb_status1.Text = 3 Then
            MsgBox("This is a cancelled Ticket")
        ElseIf Me.tb_intit.Text = 141325 Then
            MsgBox("Save the Record First")
        Else
            If Me.tb_ticketno.Text = "" Then
                MsgBox("Ticket number must not be blank")
                Me.tb_ticketno.Focus()
            ElseIf Me.tb_sledesc.Text = "" Then
                MsgBox("Select a vendor")
                Me.tb_sledesc.Focus()
            ElseIf Me.cb_itemcode.Text = "" Then
                MsgBox("Select an itemcode")
                Me.cb_itemcode.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
                Me.b_edit.Focus()
                'ElseIf Me.tb_QTY.Text = "" Then
            Else
                'Dim constr As String = My.Settings.Item("ConnString")
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                ' Check if it has got multiple items.
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.GEN_MATERIAL_RECEIPT_MULTI"
                        cmd.CommandType = CommandType.StoredProcedure
                        'ReDim intiem(CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString))
                        'connparam.dev_set()
                        'constrd = "Data Source=" + connparam.datasource & _
                        '      ";User Id=" + connparam.username & _
                        '      ";Password=" + connparam.paswwd
                        'Dim connd As New OracleConnection(constrd)
                        'If connd.State = ConnectionState.Closed Then
                        '    connd.Open()
                        'End If
                        'Dim cmdd As New OracleCommand
                        'cmdd.Connection = connd
                        'cmdd.Parameters.Clear()
                        'cmdd.CommandText = "gen_iwb_ds.GEN_MATERIAL_RECEIPT_MULTI"
                        'cmdd.CommandType = CommandType.StoredProcedure
                        'cmdd.ArrayBindCount = ds1.Tables(0).Rows.Count
                        'cmdd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        'cmdd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        'cmdd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        'cmdd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        'cmdd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        'cmdd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        'cmdd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        'cmdd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        'cmdd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                        'cmdd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                        'cmdd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                        'cmdd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                        'cmdd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                        'cmdd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                        'For i = 0 To ds1.Tables(0).Rows.Count - 1
                        '    intiem(i) = ds1.Tables(0).Rows(i).Item("INTITEMCODE").ToString
                        '    itemdes(i) = ds1.Tables(0).Rows(i).Item("ITEMDESC").ToString
                        '    pqty(i) = ds1.Tables(0).Rows(i).Item("QTY").ToString
                        '    pfswt(i) = ds1.Tables(0).Rows(i).Item("FIRSTQTY").ToString
                        '    pscwt(i) = ds1.Tables(0).Rows(i).Item("SECONDQTY").ToString
                        'Next
                        'Dim intitm As OracleParameter = New OracleParameter("d1", OracleDbType.Int32)
                        'intitm.Direction = ParameterDirection.Input
                        'intitm.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        'intitm.Value = glbvar.intiem
                        'cmdd.Parameters.Add(intitm)
                        'Dim pitmdesc As OracleParameter = New OracleParameter(":d2", OracleDbType.Varchar2)
                        'pitmdesc.Direction = ParameterDirection.Input
                        'pitmdesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        'pitmdesc.Value = glbvar.itemdes
                        'cmdd.Parameters.Add(pitmdesc)
                        'Dim fstwt As OracleParameter = New OracleParameter(":d3", OracleDbType.Decimal)
                        'fstwt.Direction = ParameterDirection.Input
                        'fstwt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        'fstwt.Value = glbvar.pfswt
                        'cmdd.Parameters.Add(fstwt)
                        'Dim secwt As OracleParameter = New OracleParameter(":d4", OracleDbType.Decimal)
                        'secwt.Direction = ParameterDirection.Input
                        'secwt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        'secwt.Value = glbvar.pscwt
                        'cmdd.Parameters.Add(secwt)
                        'Dim ntwt As OracleParameter = New OracleParameter(":d5", OracleDbType.Decimal)
                        'ntwt.Direction = ParameterDirection.Input
                        'ntwt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        'ntwt.Value = glbvar.pqty
                        'cmdd.Parameters.Add(ntwt)
                        'cmdd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        'cmdd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                        'cmdd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                        'cmdd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                        'cmdd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                        'cmdd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                        cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                        cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(Me.tb_SECONDQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                        cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(Me.tb_QTY.Text)
                        cmd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                        cmd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                        cmd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                        cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                        Try
                            cmd.ExecuteNonQuery()
                            conn.Close()
                            If Not IsDBNull(cmd.Parameters("errormsg").Value) Then
                                MsgBox(cmd.Parameters("errormsg").Value.ToString)
                            End If
                            Me.tb_IBDSNO.Text = cmd.Parameters("docn").Value.ToString
                            Me.tb_INTIBDSNO.Text = cmd.Parameters("pintdel").Value.ToString
                            MsgBox("Record Saved")
                            b_genis.Hide()
                        Catch ex As Exception
                            MsgBox(ex.Message.ToString)
                            conn.Close()
                        End Try
                    Else
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.GEN_MATERIAL_RECEIPT"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                        cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text
                        cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(Me.tb_SECONDQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                        cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(Me.tb_QTY.Text)
                        cmd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                        cmd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                        cmd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                        cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                        Try
                            cmd.ExecuteNonQuery()
                            conn.Close()
                            'If Not IsDBNull(cmd.Parameters("errormsg").Value) Then
                            If cmd.Parameters("errormsg").Value.ToString <> "null" Then
                                MsgBox(cmd.Parameters("errormsg").Value.ToString)
                            Else
                                Me.tb_IBDSNO.Text = cmd.Parameters("docn").Value.ToString
                                Me.tb_INTIBDSNO.Text = cmd.Parameters("pintdel").Value.ToString
                                MsgBox("Record Saved")
                                b_genis.Hide()
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message.ToString)
                            conn.Close()
                        End Try

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try

            End If 'validation
        End If
    End Sub

    Private Sub b_exit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_exit.Click
        comm.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        compselect.Show()
        Me.Close()
    End Sub

    Private Sub b_print1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_print1.Click
        Try
            glbvar.vintdocno = Me.Tb_intdocno.Text
            If Me.cb_inouttype.Text = "T" Then
                'STFSTWT.Show()
                'STFSTWT.Close()
            Else
                'Fstwt.Show()
                'Fstwt.Close()
            End If


        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub b_print2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_print2.Click
        Try
            glbvar.vintdocno = CInt(Me.Tb_intdocno.Text)
            If Me.cb_inouttype.Text = "T" Then
                '    STSCNDWT.Show()
                '    STSCNDWT.Close()
            Else
                'secwt2.Show()
                'secwt2.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub b_printall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_printall.Click
        Try
            glbvar.vintdocno = CInt(Me.Tb_intdocno.Text)
            If Me.cb_inouttype.Text = "T" Then
                'STRANBOTH.Show()
                'STRANBOTH.Close()
            Else
                'bothh.Show()
                'bothh.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_multi.Click
        Try
            If tmode = 2 Then
                glbvar.vntwt = CInt(Me.tb_QTY.Text)
                glbvar.vfwt = CInt(Me.tb_FIRSTQTY.Text)
                glbvar.vswt = CInt(Me.tb_SECONDQTY.Text)
                glbvar.inout = Me.cb_inouttype.Text
                Dim frm As New multi_itm
                frm.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub clear_scr()
        b_genis.Visible = False
        b_gends.Visible = False
        b_genst.Visible = False
        tb_edittktn.Hide()
        b_edittktn.Hide()
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        Me.cb_inouttype.Text = ""
        Me.tb_ticketno.Text = ""
        Me.tb_container.Text = ""
        Me.tb_vehicleno.Text = ""
        Me.tb_transporter.Text = ""
        Me.tb_sledesc.Text = ""
        Me.tb_itemdesc.Text = ""
        Me.tb_operatorid.Text = ""
        Me.tb_numberofpcs.Text = 0
        Me.tb_DRIVERNAM.Text = ""
        Me.tb_NATIONALITY.Text = ""
        Me.tb_DRIVINGLICNO.Text = ""
        Me.tb_FIRSTQTY.Text = ""
        Me.tb_DATEIN.Text = ""
        Me.tb_ticketno.Text = ""
        Me.tb_SECONDQTY.Text = 0
        Me.tb_DATEOUT.Text = ""
        Me.tb_TIMOUT.Text = ""
        Me.tb_DEDUCTIONWT.Text = 0
        Me.tb_packded.Text = 0
        Me.tb_ded.Text = 0
        Me.tb_QTY.Text = 0
        Me.tb_PRICETON.Text = 0
        Me.tb_TOTALPRICE.Text = 0
        Me.tb_comments.Text = ""
        Me.tb_dsno.Text = ""
        Me.tb_orderno.Text = ""
        Me.tb_IBDSNO.Text = ""
        Me.Tb_accountcode.Text = ""
        Me.Tb_intitemcode.Text = ""
        Me.Tb_intdocno.Text = ""
        Me.tb_INTIBDSNO.Text = ""
        Me.tb_STATUS.Text = ""
    End Sub

    Private Sub b_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_clear.Click
        Try
            clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub

    'Private Sub tb_DEDUCTIONWT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_DEDUCTIONWT.LostFocus
    Private Sub tb_DEDUCTIONWT_LostFocus() Handles tb_DEDUCTIONWT.LostFocus

        If tmode = 1 Then
            If cb_inouttype.SelectedValue = "I" Then
                If tb_QTY.Text <> CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.SelectedValue = "O" Then
                If tb_QTY.Text <> CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        ElseIf tmode = 2 Then
            If cb_inouttype.Text = "I" Then
                If tb_QTY.Text <> CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "O" Then
                If tb_QTY.Text <> CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
                        'Me.tb_QTY.Text = tq - CDec(Me.tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub b_gends_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_gends.Click
        'If DateTime.Now.ToString("HH:mm:ss") > #4:50:00 PM# Or DateTime.Now.ToString("HH:mm:ss") < #8:00:00 AM# Then
        'MsgBox("DS cannot be generated at this time, please try in between 8 AM and 4.50 PM")
        'Else
        check_cancel()
        check_item()
        If Me.tb_status1.Text = 3 Then
            MsgBox("This is a cancelled Ticket")
        ElseIf Me.tb_intit.Text = 141325 Then
            MsgBox("Save the Record First")
        Else
            If Me.tb_ticketno.Text = "" Then
                MsgBox("Ticket number must not be blank")
                Me.tb_ticketno.Focus()
            ElseIf Me.tb_sledesc.Text = "" Then
                MsgBox("Select a vendor")
                Me.tb_sledesc.Focus()
            ElseIf Me.cb_itemcode.Text = "" Then
                MsgBox("Select an itemcode")
                Me.cb_itemcode.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
                Me.b_edit.Focus()
                'ElseIf Me.tb_QTY.Text = "" Then
            Else
                'Dim constr As String = My.Settings.Item("ConnString")
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                ' Check if it has got multiple items.
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        'connparam.dev_set()
                        'constrd = "Data Source=" + connparam.datasource & _
                        '     ";User Id=" + connparam.username & _
                        '    ";Password=" + connparam.paswwd
                        'Dim connd As New OracleConnection(constrd)
                        'If connd.State = ConnectionState.Closed Then
                        'connd.Open()
                        'End If
                        'Dim cmdd As New OracleCommand
                        'cmdd.Connection = connd
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.GEN_Delivery_note_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                        cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                        cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(Me.tb_SECONDQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                        cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(Me.tb_QTY.Text)
                        cmd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                        cmd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                        cmd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output

                        Try
                            cmd.ExecuteNonQuery()
                            conn.Close()
                            If Not IsDBNull(cmd.Parameters("errormsg").Value) Then
                                MsgBox(cmd.Parameters("errormsg").Value.ToString)
                            End If
                            Me.tb_IBDSNO.Text = cmd.Parameters("docn").Value.ToString
                            Me.tb_INTIBDSNO.Text = cmd.Parameters("pintdel").Value.ToString
                            MsgBox("Record Saved")
                            b_genis.Hide()
                        Catch ex As Exception
                            MsgBox(ex.Message.ToString)
                            conn.Close()
                        End Try
                    Else
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.GEN_Delivery_note"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                        cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                        cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(Me.tb_SECONDQTY.Text)
                        cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                        cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(Me.tb_QTY.Text)
                        cmd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                        cmd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                        cmd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output
                        Try
                            cmd.ExecuteNonQuery()
                            conn.Close()
                            ' If Not IsDBNull(cmd.Parameters("errormsg").Value) Then
                            If Not cmd.Parameters("errormsg").Value.ToString <> "null" Then
                                MsgBox(cmd.Parameters("errormsg").Value.ToString)
                            Else
                                Me.tb_IBDSNO.Text = cmd.Parameters("docn").Value.ToString
                                Me.tb_INTIBDSNO.Text = cmd.Parameters("pintdel").Value.ToString
                                MsgBox("Record Saved")
                                b_gends.Hide()
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message.ToString)
                            conn.Close()
                        End Try

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try
            End If
        End If 'validation
        'End If
    End Sub

    Private Sub cb_dcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_dcode.LostFocus
        b_save.Focus()
    End Sub




    Private Sub cb_dcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_dcode.SelectedIndexChanged
        Try
            If Me.cb_dcode.SelectedIndex <> -1 Then
                Me.tb_DRIVERNAM.Text = Me.cb_dcode.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "EMPCODE = '" & Me.tb_DRIVERNAM.Text & "'" & ""
                foundrow = dsdr.Tables("drv").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the driver")

                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub



    Private Sub b_genst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_genst.Click
        check_cancel()
        check_item()
        If Me.tb_status1.Text = 3 Then
            MsgBox("This is a cancelled Ticket")
        ElseIf Me.tb_intit.Text = 141325 Then
            MsgBox("Save the Record First")
        Else
            If Me.tb_ticketno.Text = "" Then
                MsgBox("Ticket number must not be blank")
                Me.tb_ticketno.Focus()
            ElseIf Me.cb_itemcode.Text = "" Then
                MsgBox("Select To Item")
                Me.cb_itemcode.Focus()
            ElseIf Me.cb_fritem.Text = "" Then
                MsgBox("Select From Item")
                Me.cb_itemcode.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
                Me.b_edit.Focus()
                'ElseIf Me.tb_QTY.Text = "" Then
            Else
                'Dim constr As String = My.Settings.Item("ConnString")
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                ' Check if it has got multiple items.
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd.GEN_STOCK_TRANSFER"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                cmd.Parameters.Add(New OracleParameter("frintitm", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
                cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
                cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(Me.tb_SECONDQTY.Text)
                cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(Me.tb_DEDUCTIONWT.Text)
                cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(Me.tb_QTY.Text)
                cmd.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                cmd.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value = glbvar.userid
                cmd.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2, 20)).Direction = ParameterDirection.Output
                cmd.Parameters.Add(New OracleParameter("pintdel", OracleDbType.Int32)).Direction = ParameterDirection.Output
                cmd.Parameters.Add(New OracleParameter("errormsg", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output
                cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                Try
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    'If Not IsDBNull(cmd.Parameters("errormsg").Value) Then
                    If cmd.Parameters("errormsg").Value.ToString <> "null" Then
                        MsgBox(cmd.Parameters("errormsg").Value.ToString)
                    Else

                        Me.tb_IBDSNO.Text = cmd.Parameters("docn").Value.ToString
                        Me.tb_INTIBDSNO.Text = cmd.Parameters("pintdel").Value.ToString
                        MsgBox("Record Saved")
                        b_genst.Hide()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try

            End If
        End If
    End Sub

    Private Sub b_scaleonly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_scaleonly.Click
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS WBM WHERE INOUTTYPE = 'S' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Try
            If cb_sledcode.Visible = False Then
                cb_sledcode.Show()
            End If
            If tb_sledesc.Visible = False Then
                tb_sledesc.Show()
            End If
            If cb_fritem.Visible = True Then
                cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                tb_fritemdesc.Hide()
            End If
            Label6.Text = "Supplier"
            Label7.Text = "Product"
            cmbloading()
            Me.cb_sledcode.Text = "224010 001 0554"
            Me.tb_sledesc.Text = "Other Supplier"
            Me.tb_itemdesc.Text = "00000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            cb_inouttype.SelectedValue = "S"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub b_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_delete.Click

        If cb_inouttype.Text = "O" Then
            Try
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_delete"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
            Try
                cmd.ExecuteNonQuery()
                conn.Close()
                'If Not IsDBNull(cmd.Parameters("errormsg").Value) Then

                MsgBox("Record Deleted")

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
                conn.Close()
            End Try
        Else
            MsgBox("Only Outgoing Tickets can be deleted")
        End If
    End Sub
    Private Sub b_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_cancel.Click
        If CInt(Me.tb_ticketno.Text) <> 0 Then


            check_cancel()
            If Me.tb_status1.Text = 3 Then
                MsgBox("This is a cancelled ticket")
            Else
                tb_edittktn.Show()
                b_edittktn.Show()
                tb_edittktn.Focus()
            End If

        Else
            MsgBox("Enter Details")
        End If
    End Sub

    Private Sub b_edittktn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_edittktn.Click
        Dim emode As Integer
        emode = 2
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        sql = "SELECT   WBM.TICKETNO" _
            & "  FROM   STWBMIBDS WBM" _
            & " WHERE WBM.TICKETNO = " & Me.tb_edittktn.Text _
            & " and status in (1,2,3)"

        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try

            da.Fill(dstk)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

        If dstk.Tables(0).Rows.Count > 0 Then
            emode = 1
            MsgBox("Ticket number Already used")
        End If
        conn.Close()
        'check the ticketnumber for whther within range
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.tktrng"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        If cb_inouttype.Text = "I" Then
            cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "IWB"
        ElseIf cb_inouttype.SelectedValue = "O" Then
            cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "DLS"
        ElseIf cb_inouttype.SelectedValue = "T" Then
            cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SNA"
        ElseIf cb_inouttype.SelectedValue = "S" Then
            cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCL"
        End If
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            Dim dsrng As New DataSet
            Dim darng As New OracleDataAdapter(cmd)
            darng.TableMappings.Add("Table", "tktrng")
            darng.Fill(dsrng)
            If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                Me.tb_container.Focus()
            Else
                emode = 1
                MsgBox("Ticket number not in range should be within " & dsrng.Tables("tktrng").Rows(0).Item("STARTNO") & " - " & dsrng.Tables("tktrng").Rows(0).Item("ENDNO"))
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        If tb_IBDSNO.Text <> "" Then
            emode = 1
            MsgBox("IB DS SN Generated, Cannot edit")
        End If
        If emode = 2 Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            'Me.tb_ticketno.Text = 61000005
            'Me.tb_FIRSTQTY.Text = 1234
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "gen_iwb_dsd.gen_wbms_edittkt"
            cmd1.CommandType = CommandType.StoredProcedure
            Try
                cmd1.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.Text
                cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd1.Parameters.Add(New OracleParameter("pNEWTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_edittktn.Text)
                cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                cmd1.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                'If IsDBNull(Me.tb_container.Text) Then
                'Me.tb_container.Text = ""
                'Else
                '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                'End If
                cmd1.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = Me.tb_transporter.Text
                If cb_inouttype.SelectedValue = "T" Then
                    cmd1.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = "224010 001"
                    cmd1.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = "224010 001 0554"
                    cmd1.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = "Other Supplier"
                Else
                    cmd1.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                    cmd1.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                    cmd1.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                End If
                cmd1.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                cmd1.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                cmd1.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

                If Me.cb_inouttype.SelectedValue <> "T" Then
                    cmd1.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
                    cmd1.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = "Dummy"
                    cmd1.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = "00000"
                ElseIf Me.cb_inouttype.SelectedValue = "T" Then

                    cmd1.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
                    cmd1.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
                    cmd1.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()
                End If
                cmd1.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = Me.tb_numberofpcs.Text
                cmd1.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                cmd1.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                cmd1.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = Me.tb_NATIONALITY.Text
                cmd1.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = Me.tb_DRIVINGLICNO.Text
                cmd1.Parameters.Add(New OracleParameter("pFIRSTQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_FIRSTQTY.Text)
                Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
                cmd1.Parameters.Add(New OracleParameter("pDATEIN", OracleDbType.Date)).Value = dtin 'Convert.ToDateTime(Me.tb_DATEIN.Text)
                cmd1.Parameters.Add(New OracleParameter("pTIMEIN", OracleDbType.Varchar2)).Value = Me.tb_TIMEIN.Text
                cmd1.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
                cmd1.Parameters.Add(New OracleParameter("pAPPDATE0", OracleDbType.Date)).Value = Today
                cmd1.Parameters.Add(New OracleParameter("pFIELD1", OracleDbType.Varchar2)).Value = glbvar.userid
                cmd1.Parameters.Add(New OracleParameter("pSTATUS", OracleDbType.Varchar2)).Value = 1
                cmd.Parameters.Add(New OracleParameter("pDEDUCTIONWT", OracleDbType.Int32)).Value = CInt(Me.tb_DEDUCTIONWT.Text)
                cmd.Parameters.Add(New OracleParameter("pPACKDED", OracleDbType.Int32)).Value = CInt(Me.tb_packded.Text)
                cmd.Parameters.Add(New OracleParameter("pDED", OracleDbType.Int32)).Value = CInt(Me.tb_ded.Text)
                cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Direction = ParameterDirection.Output
                'Try
                cmd1.ExecuteNonQuery()
                'Dim vint As Decimal
                'vint = cmd.Parameters("pINTDOCNO").Value.ToString  'CDec(cmd.Parameters("pINTDOCNO").Value)
                Me.Tb_intdocno.Text = cmd1.Parameters("pINTDOCNO").Value.ToString
                conn.Close()
                Me.b_firstwt.Enabled = False
                MsgBox("Record Saved")
                'clear_scr()
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
                conn.Close()
            End Try
        End If
    End Sub
    Private Sub check_cancel()
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            sql = "SELECT   STATUS" _
                & "  FROM   STWBMIBDS WBM" _
                & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text

            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            da.Fill(dstk)
            If dstk.Tables(0).Rows.Count > 0 Then
                Me.tb_status1.Text = dstk.Tables(0).Rows(0).Item("status")
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub check_item()
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            sql = "SELECT   intitemcode" _
                & "  FROM   STWBMIBDS WBM" _
                & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text

            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            da.Fill(dstk)
            If dstk.Tables(0).Rows.Count > 0 Then
                Me.tb_intit.Text = dstk.Tables(0).Rows(0).Item("intitemcode")
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub tb_ded_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_ded.LostFocus
        Try
            Me.tb_DEDUCTIONWT.Text = CDec(tb_ded.Text) + CDec(tb_packded.Text)
            tb_DEDUCTIONWT_LostFocus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tb_packded_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_packded.LostFocus
        Try
            Me.tb_DEDUCTIONWT.Text = CDec(tb_ded.Text) + CDec(tb_packded.Text)
            tb_DEDUCTIONWT_LostFocus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_transporter_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_transporter.LostFocus

        If ymode = 0 Then
            cb_sledcode.Focus()
        End If


    End Sub


    Private Sub b_vehino_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_vehino.Click
        clear_scr()
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                 & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                 & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,REMARKS ,IBDSNO," _
                 & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS" _
                 & " from STWBMIBDS where VEHICLENO = '" & Me.tb_sveh.Text & "'" _
                 & " and status in (1,2,3) and wtstat = 'I'"
            da = New OracleDataAdapter(sql, conn)
            'da.TableMappings.Add("Table", "mlt")
            Dim ds As New DataSet
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then


                Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                End If
                If Me.cb_inouttype.Text = "T" Then
                    cb_sledcode.Hide()
                    tb_sledesc.Hide()
                    cb_fritem.Show()
                    tb_fritemdesc.Show()
                    Label6.Text = "From Item"
                    Label7.Text = "To Item"
                    Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                    Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                    Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                ElseIf Me.cb_inouttype.Text = "I" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Supplier"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                ElseIf Me.cb_inouttype.Text = "O" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Customer"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                ElseIf Me.cb_inouttype.Text = "S" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Supplier"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                End If
                Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")

                Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                    Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                    Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")

                    Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("FIRSTQTY"))) Then
                    Me.tb_FIRSTQTY.Text = ds.Tables(0).Rows(0).Item("FIRSTQTY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SECONDQTY"))) Then
                    Me.tb_SECONDQTY.Text = ds.Tables(0).Rows(0).Item("SECONDQTY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("QTY"))) Then
                    Me.tb_QTY.Text = ds.Tables(0).Rows(0).Item("QTY")
                End If
                Me.tb_DATEIN.Text = ds.Tables(0).Rows(0).Item("DATEIN")
                Me.tb_TIMEIN.Text = ds.Tables(0).Rows(0).Item("TIMEIN")
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                    Me.tb_DATEOUT.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TIMOUT"))) Then
                    Me.tb_TIMOUT.Text = ds.Tables(0).Rows(0).Item("TIMOUT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DEDUCTIONWT"))) Then
                    Me.tb_DEDUCTIONWT.Text = ds.Tables(0).Rows(0).Item("DEDUCTIONWT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PACKDED"))) Then
                    Me.tb_packded.Text = ds.Tables(0).Rows(0).Item("PACKDED")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DED"))) Then
                    Me.tb_ded.Text = ds.Tables(0).Rows(0).Item("DED")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                    Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TOTALPRICE"))) Then
                    Me.tb_TOTALPRICE.Text = ds.Tables(0).Rows(0).Item("TOTALPRICE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("REMARKS"))) Then
                    Me.tb_comments.Text = ds.Tables(0).Rows(0).Item("REMARKS")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IBDSNO"))) Then
                    Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("IBDSNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTIBDSNO"))) Then
                    Me.tb_INTIBDSNO.Text = ds.Tables(0).Rows(0).Item("INTIBDSNO")
                End If
                Me.tb_STATUS.Text = ds.Tables(0).Rows(0).Item("STATUS")
                conn.Close()
                If Me.tb_IBDSNO.Text = "" Then
                    If Me.cb_inouttype.Text = "I" Then
                        Me.b_genis.Visible = True
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = False
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = True
                        Me.b_genst.Visible = False
                    ElseIf Me.cb_inouttype.Text = "T" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = True
                    End If
                Else
                    Me.b_gends.Visible = False
                    Me.b_genis.Visible = False
                    Me.b_genst.Visible = False
                End If
                Me.b_firstwt.Enabled = False
                If Me.tb_SECONDQTY.Text = 0 Then
                    Me.b_secondwt.Enabled = True
                    tmode = 2
                End If
            Else
                MsgBox("No Records Found for this Vehicle #", MsgBoxStyle.Information)
                Me.tb_vehicleno.Focus()
            End If
            Me.tb_sveh.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()

        End Try
    End Sub

    Private Sub tb_vehicleno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_vehicleno.Validated
        Try
            ymode = 0
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        sql = "SELECT   WBM.VEHICLENO" _
            & "  FROM   STWBMIBDS WBM" _
            & " WHERE WBM.VEHICLENO = '" & Me.tb_vehicleno.Text & "'" _
            & " and status in (1,2) " _
            & " and wtstat = 'I'"

        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try

            da.Fill(dstk)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Try
            If dstk.Tables(0).Rows.Count > 0 Then
                ymode = 1
                MsgBox("Vehicle In")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If ymode = 1 Then
            tb_vehicleno.Focus()
        End If

        conn.Close()
    End Sub
    Private Sub b_trans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_trans.Click
        clear_scr()
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                 & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                 & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,REMARKS ,IBDSNO," _
                 & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS" _
                 & " from STWBMIBDS where INTDOCNO = '" & Me.tb_trans.Text & "'" _
                 & " and status in (1,2,3)"
            da = New OracleDataAdapter(sql, conn)
            'da.TableMappings.Add("Table", "mlt")
            Dim ds As New DataSet
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then


                Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                End If
                If Me.cb_inouttype.Text = "T" Then
                    cb_sledcode.Hide()
                    tb_sledesc.Hide()
                    cb_fritem.Show()
                    tb_fritemdesc.Show()
                    Label6.Text = "From Item"
                    Label7.Text = "To Item"
                    Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                    Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                    Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                ElseIf Me.cb_inouttype.Text = "I" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Supplier"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                ElseIf Me.cb_inouttype.Text = "O" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Customer"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                ElseIf Me.cb_inouttype.Text = "S" Then
                    cb_sledcode.Show()
                    tb_sledesc.Show()
                    cb_fritem.Hide()
                    tb_fritemdesc.Hide()
                    Label6.Text = "Supplier"
                    Label7.Text = "Product"
                    Me.tb_frintitem.Text = 0
                    Me.cb_fritem.Text = "0"
                    Me.tb_fritemdesc.Text = "0"
                End If
                Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")

                Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                    Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                    Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")

                    Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("FIRSTQTY"))) Then
                    Me.tb_FIRSTQTY.Text = ds.Tables(0).Rows(0).Item("FIRSTQTY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SECONDQTY"))) Then
                    Me.tb_SECONDQTY.Text = ds.Tables(0).Rows(0).Item("SECONDQTY")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("QTY"))) Then
                    Me.tb_QTY.Text = ds.Tables(0).Rows(0).Item("QTY")
                End If
                Me.tb_DATEIN.Text = ds.Tables(0).Rows(0).Item("DATEIN")
                Me.tb_TIMEIN.Text = ds.Tables(0).Rows(0).Item("TIMEIN")
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                    Me.tb_DATEOUT.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TIMOUT"))) Then
                    Me.tb_TIMOUT.Text = ds.Tables(0).Rows(0).Item("TIMOUT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DEDUCTIONWT"))) Then
                    Me.tb_DEDUCTIONWT.Text = ds.Tables(0).Rows(0).Item("DEDUCTIONWT")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PACKDED"))) Then
                    Me.tb_packded.Text = ds.Tables(0).Rows(0).Item("PACKDED")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DED"))) Then
                    Me.tb_ded.Text = ds.Tables(0).Rows(0).Item("DED")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                    Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TOTALPRICE"))) Then
                    Me.tb_TOTALPRICE.Text = ds.Tables(0).Rows(0).Item("TOTALPRICE")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("REMARKS"))) Then
                    Me.tb_comments.Text = ds.Tables(0).Rows(0).Item("REMARKS")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IBDSNO"))) Then
                    Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("IBDSNO")
                End If
                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTIBDSNO"))) Then
                    Me.tb_INTIBDSNO.Text = ds.Tables(0).Rows(0).Item("INTIBDSNO")
                End If
                Me.tb_STATUS.Text = ds.Tables(0).Rows(0).Item("STATUS")
                conn.Close()
                If Me.tb_IBDSNO.Text = "" Then
                    If Me.cb_inouttype.Text = "I" Then
                        Me.b_genis.Visible = True
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = False
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = True
                        Me.b_genst.Visible = False
                    ElseIf Me.cb_inouttype.Text = "T" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = True
                    End If
                Else
                    Me.b_gends.Visible = False
                    Me.b_genis.Visible = False
                    Me.b_genst.Visible = False
                End If
                Me.b_firstwt.Enabled = False
                If Me.tb_SECONDQTY.Text = 0 Then
                    Me.b_secondwt.Enabled = True
                    tmode = 2
                End If
            Else
                MsgBox("No Records Found for this Transaction #", MsgBoxStyle.Information)
                Me.cb_sledcode.Focus()
            End If
            Me.tb_trans.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()

        End Try
    End Sub

    Private Sub tb_PRICETON_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_PRICETON.LostFocus
        Try
            If Me.tb_PRICETON.Text <> "0" Then
                If Me.tb_PRICETON.Text > Me.Tb_perc.Text Then
                    MsgBox("Price not matching as the latest Pricelist")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub sl_item_driv_load()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.sledmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dssld.Clear()
            dasld = New OracleDataAdapter(cmd)
            dasld.TableMappings.Add("Table", "sled")
            dasld.Fill(dssld)
            cb_sledcode.DataSource = dssld.Tables("sled")
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsitm.Clear()
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.drmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsdr.Clear()
            dadr = New OracleDataAdapter(cmd)
            dadr.TableMappings.Add("Table", "drv")
            dadr.Fill(dsdr)
            cb_dcode.DataSource = dsdr.Tables("drv")
            cb_dcode.DisplayMember = dsdr.Tables("drv").Columns("EMPNAME").ToString
            cb_dcode.ValueMember = dsdr.Tables("drv").Columns("EMPCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.cb_sledcode.Text = glbvar.temp_suppcode
        Me.tb_sledesc.Text = glbvar.temp_suppdesc
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
    End Sub
    Private Sub cust_item_driv_load()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.custmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dssld.Clear()
            dasld = New OracleDataAdapter(cmd)
            dasld.TableMappings.Add("Table", "sled")
            dasld.Fill(dssld)
            cb_sledcode.DataSource = dssld.Tables("sled")
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dsitm.Clear()
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.cb_sledcode.Text = glbvar.temp_suppcode
        Me.tb_sledesc.Text = glbvar.temp_suppdesc
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
    End Sub
    Private Sub tran_item_driv_load()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dfitm.Clear()
            daitm.Fill(dfitm)
            cb_fritem.DataSource = dfitm.Tables("itm")
            cb_fritem.DisplayMember = dfitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_fritem.ValueMember = dfitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dsitm.Clear()
            daitm.Fill(dsitm)
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Me.cb_fritem.Text = glbvar.temp_suppcode
        Me.tb_fritemdesc.Text = glbvar.temp_suppdesc
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
    End Sub
End Class



















































































































































































































































































































































































































