Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System.Timers

Public Class TRANSFER_PR
    'Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
    '    Try
    '        MyBase.OnLoad(e)
    '        Dim tmr As New System.Timers.Timer()
    '        tmr.Interval = 21600000
    '        tmr.Enabled = True
    '        tmr.Start()
    '        AddHandler tmr.Elapsed, AddressOf OnTimedEvent
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Delegate Sub CloseFormCallback()

    'Private Sub CloseForm()
    '    Try
    '        If InvokeRequired Then
    '            Dim d As New CloseFormCallback(AddressOf CloseForm)
    '            Invoke(d, Nothing)
    '        Else
    '            'b_exit_Click()
    '            fexit()
    '            Close()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub OnTimedEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
    '    Try
    '        'b_exit_Click()
    '        CloseForm()
    '        'usermenu.Show()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
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
    Dim dsql As String
    Dim dadm As OracleDataAdapter
    Dim da As OracleDataAdapter
    Dim dpr As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Dim sql As String
    Dim recbr As String
    Public ds As New DataSet
    Dim sledfillup As String
    Dim suppfillup As String
    Dim tb_mixtkt = New TextBox
    Dim tb_cfillup = New TextBox
    Dim ds1 As New DataSet
    Dim tmode As Integer
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim dapsld As New OracleDataAdapter
    Dim dspsld As New DataSet
    Dim omdasld As New OracleDataAdapter
    Dim omdssld As New DataSet
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim dafitm As New OracleDataAdapter
    Dim dsfitm As New DataSet
    Dim dadoc As New OracleDataAdapter
    Dim dsdoc As New DataSet
    Dim dfitm As New DataSet
    Dim dadr As New OracleDataAdapter
    Dim dsdr As New DataSet
    Dim id() As String
    Dim typ() As String
    Dim nmbr() As Integer
    Dim mesg() As String
    Dim tkt() As Long

    Private Sub WBMS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message)
            comm.ClosePort()
            comm1.ClosePort()
            commett.ClosePort()
            commett1.ClosePort()
            commetty.ClosePort()
            commetty1.ClosePort()
        End Try
        clear_scr()
        comm.ClosePort()
        comm1.ClosePort()
        commett.ClosePort()
        commett1.ClosePort()
        commetty.ClosePort()
        commetty1.ClosePort()

    End Sub
    Private Sub fexit()
        'ByVal sender As Object, ByVal e As System.EventArgs
        clear_scr()
        comm.ClosePort()
        comm1.ClosePort()
        commett.ClosePort()
        commett1.ClosePort()
        commetty.ClosePort()
        commetty1.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        'usermenu.Show()
        'Me.Close()
    End Sub
    Private Sub WBMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd &
                          ";Pooling=false"
        'cmbloading()
        tmode = 0
        comm.CurrentTransmissionType = CommManager.TransmissionType.Text
        comm1.CurrentTransmissionType = CommManager2.TransmissionType.Text
        commett.CurrentTransmissionType = CommManagerMet1.TransmissionType.Text
        commett1.CurrentTransmissionType = CommManagerMet2.TransmissionType.Text
        commetty.CurrentTransmissionType = CommManagerYNB1.TransmissionType.Text
        commetty1.CurrentTransmissionType = CommManagerYNB2.TransmissionType.Text
        Me.tb_FIELD1.Text = glbvar.userid
        tb_edittktn.Hide()
        b_edittktn.Hide()
        glbvar.scaletype = "2"
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
    End Sub
    Private Sub b_newveh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'For m = 0 To wbms1.MIX.MIXGRID.RowCount - 1
        '    wbms1.MIX.MIXGRID.Rows.Clear()
        'Next
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'T' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
            conn.Close()
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
                'cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                'tb_fritemdesc.Hide()
            End If
            l_Project.Text = "Supplier"
            l_tomat.Text = "Product"
            Me.tb_sap_doc.Text = "QD"
            cmbloading()
            Me.tb_sap_doc.Text = "QD"
            Me.cb_sledcode.Text = "Dummy Supplier"
            Me.tb_sledesc.Text = "0000000000"
            Me.cb_itemcode.Text = "SCRAP"
            Me.tb_itemdesc.Text = "000000000000000000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"
            Me.tb_docprint.Text = "BIG SCALE MATERIAL RECEIPT"
            tmode = 1
            b_firstwt.Enabled = True
            b_firstwt2.Enabled = True
            Me.b_secondwt.Enabled = False
            Me.b_secondwt2.Enabled = False
            cb_inouttype.SelectedValue = "I"
            'Me.cb_sledcode.Text = "224010 00"
            b_genis.Visible = False
            b_gends.Visible = False
            b_genst.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim inot As String = Me.cb_inouttype.SelectedValue.ToString

        ' Loading the document type

        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'Dim cmd As New OracleCommand
        'cmd.Connection = conn
        'cmd.Parameters.Clear()
        'cmd.CommandText = "curspkg_join.docmst"
        'cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        'cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        'cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = inot
        'cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        'Try
        '    cb_sap_docu_type.SelectedIndex = -1
        '    tb_sap_doc.Text = "QD"
        '    dadoc = New OracleDataAdapter(cmd)
        '    dadoc.TableMappings.Add("Table", "doc")
        '    dsdoc.Clear()
        '    dadoc.Fill(dsdoc)
        '    cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
        '    cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
        '    cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString

        '    'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub


    Private Sub venlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadven.DoubleClick
        Try

            If Me.loadven.SelectedItems(0).SubItems(0).Text <> "" Then


                Me.tb_sledesc.Text = Me.loadven.SelectedItems(0).SubItems(0).Text

                Me.cb_sledcode.Text = Me.loadven.SelectedItems(0).SubItems(1).Text

                Me.loadven.Visible = False

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tb_sledcode_Validated(sender As Object, e As EventArgs) Handles tb_searchbyno.TextChanged
        Try

            'Dim tb2 As TextBox = CType(sender, TextBox)
            'If tb2.Text.Length > 0 Then
            '    Dim foundrow() As DataRow
            '    Dim expression As String = "SLEDCODE LIKE '" & tb2.Text & "%'" & ""
            '    foundrow = dsitm.Tables("dssld").Select(expression)
            '    loadven.Items.Clear()
            '    For i = 0 To foundrow.Count - 1

            '        Me.loadven.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
            '        Me.loadven.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)

            '    Next

            '    loadven.Visible = True
            'End If
            ' Try
            'If Asc(e.KeyChar) = 8 Then
            ' itmchar = ""
            'Else
            'itmchar = itmchar + e.KeyChar
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE LIKE '" & Me.tb_searchbyno.Text & "%'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            loadven.Items.Clear()
            For i = 0 To foundrow.Count - 1
                'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                Me.loadven.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
                Me.loadven.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)
            Next
            loadven.Visible = True
            'End If
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Private Sub tb_sledcode_Validated(sender As Object, e As EventArgs) Handles tb_sledcode.Validated

        'End Sub
    End Sub
    Private Sub b_outveh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'O' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
            conn.Close()
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
                'cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                'tb_fritemdesc.Hide()
            End If
            l_Project.Text = "Customer"
            l_tomat.Text = "Product"
            Me.tb_sap_doc.Text = "ZTBV"
            cmbloading1()
            Me.tb_sap_doc.Text = "ZTBV"
            Me.cb_sledcode.Text = "Dummy Customer"
            Me.tb_sledesc.Text = "0000000000"
            Me.cb_itemcode.Text = "SCRAP"
            Me.tb_itemdesc.Text = "000000000000000000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"

            Me.cb_sap_docu_type.Text = "Cash Sales"
            Me.tb_docprint.Text = "DELIVERY NOTE/GATEPASS"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            b_firstwt2.Enabled = True
            Me.b_secondwt2.Enabled = False
            cb_inouttype.SelectedValue = "O"
            b_genis.Visible = False
            b_gends.Visible = False
            b_genst.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim inot As String = Me.cb_inouttype.SelectedValue.ToString
        ' Loading the document type

        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'Dim cmd As New OracleCommand
        'cmd.Connection = conn
        'cmd.Parameters.Clear()
        'cmd.CommandText = "curspkg_join.docmst"
        'cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        'cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        'cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = inot
        'cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        'Try
        '    cb_sap_docu_type.SelectedIndex = -1
        '    tb_sap_doc.Text = "ZCOR"
        '    dadoc = New OracleDataAdapter(cmd)
        '    dadoc.TableMappings.Add("Table", "doc")
        '    dsdoc.Clear()
        '    dadoc.Fill(dsdoc)
        '    cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
        '    cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
        '    cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString
        '    'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub
    Private Sub b_stransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_stransfer.Click
        'conn = New OracleConnection(constr)
        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
        '        & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'T' "
        'da = New OracleDataAdapter(sql, conn)
        'Dim dstk1 As New DataSet
        'Try
        '    da.TableMappings.Add("Table", "TKTNO")
        '    da.Fill(dstk1)
        '    conn.Close()
        '    Me.tb_ticketno.Text = dstk1.Tables("TKTNO").Rows(0).Item("TKT")
        'Catch ex As Exception
        '    MsgBox(ex.Message.ToString)
        'End Try
        'Try
        '    cb_sledcode.Hide()
        '    tb_sledesc.Hide()
        '    If cb_fritem.Visible = False Then
        '        cb_fritem.Show()
        '    End If
        '    If tb_fritemdesc.Visible = False Then
        '        tb_fritemdesc.Show()
        '    End If
        '    Label6.Text = "From Item"
        '    Label7.Text = "To Item"
        '    cmbloading2()
        '    tmode = 1
        '    b_firstwt.Enabled = True
        '    Me.b_secondwt.Enabled = False
        '    b_firstwt2.Enabled = True
        '    Me.b_secondwt2.Enabled = False
        '    cb_inouttype.SelectedValue = "T"
        '    b_genis.Visible = False
        '    b_gends.Visible = False
        '    b_genst.Visible = False
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'T' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
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
                'cb_fritem.Hide()
            End If
            If tb_fritemdesc.Visible = True Then
                'tb_fritemdesc.Hide()
            End If
            'Label6.Text = "Supplier"
            'Label7.Text = "Product"
            cmbloading()
            Me.tb_docprint.Text = "STOCK TRANSFER"
            Me.tb_sap_doc.Text = "ST"
            Me.cb_sap_docu_type.Text = "Stock Transfer"
            Me.cb_sledcode.Text = "Dummy Supplier"
            Me.tb_sledesc.Text = "0000000000"
            Me.cb_prjsledcode.Text = "Dummy Supplier"
            Me.tb_prjsledesc.Text = "0000000000"
            Me.cb_itemcode.Text = "SCRAP"
            Me.cb_fritem.Text = "SCRAP"
            Me.tb_itemdesc.Text = "000000000000000000"
            Me.tb_fritemdesc.Text = "000000000000000000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_frintitem.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            b_firstwt2.Enabled = True
            Me.b_secondwt2.Enabled = False
            cb_inouttype.SelectedValue = "T"
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
    Private Sub b_firstwt2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_firstwt2.Click
        Me.tb_FIRSTQTY.Text = Me.rtbDisplay2.Text
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
            'cb_itemcode_SelectedIndexChanged(sender, e)
            Me.tb_SECONDQTY.Text = Me.rtbDisplay.Text
            Me.tb_DATEOUT.Text = Today.Date
            Me.tb_TIMOUT.Text = Now.ToShortTimeString
            Dim sq As Integer = Convert.ToDecimal(Trim(Me.tb_SECONDQTY.Text))
            If cb_inouttype.Text = "I" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "O" Then
                If Me.tb_sap_doc.Text <> "ZTRE" Then
                    Me.tb_QTY.Text = Math.Abs(sq - CDec(Me.tb_FIRSTQTY.Text) - CDec(Me.tb_DEDUCTIONWT.Text))
                Else
                    Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
                End If
            ElseIf cb_inouttype.Text = "T" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "S" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "W" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_secondwt2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_secondwt2.Click
        Try
            'cb_itemcode_SelectedIndexChanged(sender, e)
            Me.tb_SECONDQTY.Text = Me.rtbDisplay2.Text
            Me.tb_DATEOUT.Text = Today.Date
            Me.tb_TIMOUT.Text = Now.ToShortTimeString
            Dim sq As Integer = Convert.ToDecimal(Trim(Me.tb_SECONDQTY.Text))
            If cb_inouttype.Text = "I" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "O" Then
                If Me.tb_sap_doc.Text <> "ZTRE" Then
                    Me.tb_QTY.Text = Math.Abs(sq - CDec(Me.tb_FIRSTQTY.Text) - CDec(Me.tb_DEDUCTIONWT.Text))
                Else
                    Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
                End If
            ElseIf cb_inouttype.Text = "T" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "S" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
            ElseIf cb_inouttype.Text = "W" Then
                Me.tb_QTY.Text = Math.Abs(CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text))
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
            .Add(New cmbload("Scale Outside Tickets", "W"))
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
        cmd.CommandText = "curspkg_join_pr.sledmst"
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
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.intordmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dspsld.Clear()
            dapsld = New OracleDataAdapter(cmd)
            dapsld.TableMappings.Add("Table", "sledprj")
            dapsld.Fill(dspsld)
            cb_prjsledcode.DataSource = dspsld.Tables("sledprj")
            cb_prjsledcode.DisplayMember = dspsld.Tables("sledprj").Columns("SLEDDESC").ToString
            cb_prjsledcode.ValueMember = dspsld.Tables("sledprj").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsitm.Clear()
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            daitm.Fill(dsitm)
            conn.Close()
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
        cmd.CommandText = "curspkg_join_pr.fitmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsfitm.Clear()
            dafitm = New OracleDataAdapter(cmd)
            dafitm.TableMappings.Add("Table", "fitm")
            dafitm.Fill(dsfitm)
            conn.Close()
            cb_fritem.DataSource = dsfitm.Tables("fitm")
            cb_fritem.DisplayMember = dsfitm.Tables("fitm").Columns("itmdsc").ToString
            cb_fritem.ValueMember = dsfitm.Tables("fitm").Columns("itmcde").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.drmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsdr.Clear()
            dadr = New OracleDataAdapter(cmd)
            dadr.TableMappings.Add("Table", "drv")
            dadr.Fill(dsdr)
            'conn.Close()
            cb_dcode.DataSource = dsdr.Tables("drv")
            cb_dcode.DisplayMember = dsdr.Tables("drv").Columns("EMPNAME").ToString
            cb_dcode.ValueMember = dsdr.Tables("drv").Columns("EMPCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.docmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = "I"
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            cb_sap_docu_type.SelectedIndex = -1
            dadoc = New OracleDataAdapter(cmd)
            dadoc.TableMappings.Add("Table", "doc")
            dsdoc.Clear()
            dadoc.Fill(dsdoc)
            conn.Close()
            cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
            cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
            cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString

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
            .Add(New cmbload("Scale Outside Tickets", "W"))
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
        cmd.CommandText = "curspkg_join_pr.custmst"
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
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.itmmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.drmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.docmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = "O"
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            cb_sap_docu_type.SelectedIndex = -1
            dadoc = New OracleDataAdapter(cmd)
            dadoc.TableMappings.Add("Table", "doc")
            dsdoc.Clear()
            dadoc.Fill(dsdoc)
            cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
            cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
            cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString

            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub
    Private Sub cmbloading2()
        Dim arry As New ArrayList
        With arry
            .Add(New cmbload("", ""))
            .Add(New cmbload("Incoming Goods", "I"))
            .Add(New cmbload("Outgoing Goods", "O"))
            .Add(New cmbload("Stock Transfer", "T"))
            .Add(New cmbload("Scale Only Tickets", "S"))
            .Add(New cmbload("Scale Outside Tickets", "W"))
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
        cmd.CommandText = "curspkg_join_pr.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dfitm.Clear()
            daitm.Fill(dfitm)
            conn.Close()
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
        cmd.CommandText = "curspkg_join_pr.itmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            dsitm.Clear()
            daitm.Fill(dsitm)
            conn.Close()
            cb_itemcode.DataSource = dsitm.Tables("itm")
            cb_itemcode.DisplayMember = dsitm.Tables("itm").Columns("ITEMDESC").ToString
            cb_itemcode.ValueMember = dsitm.Tables("itm").Columns("ITEMCODE").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


        conn.Close()
    End Sub
    Private Sub b_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_connect.Click
        Try
            If glbvar.INDTYPE = "D" Then
                comm.Parity = "None"
                comm.StopBits = 1
                comm.DataBits = 8
                comm.BaudRate = 9600
                comm.DisplayWindow = rtbDisplay
                comm.OpenPort()
            ElseIf glbvar.INDTYPE = "M" Then
                commett.Parity = "None"
                commett.StopBits = 1
                commett.DataBits = 8
                commett.BaudRate = 9600
                commett.DisplayWindow = rtbDisplay
                commett.OpenPort()
            ElseIf glbvar.INDTYPE = "Y" Then
                commetty.Parity = "None"
                commetty.StopBits = 1
                commetty.DataBits = 8
                commetty.BaudRate = 9600
                commetty.DisplayWindow = rtbDisplay
                commetty.OpenPort()
            End If
            b_Disconnect.Visible = True
            b_connect.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub
    Private Sub b_connect2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_connect2.Click
        Try
            If glbvar.INDTYPE = "D" Then
                comm1.Parity = "None"
                comm1.StopBits = 1
                comm1.DataBits = 8
                comm1.BaudRate = 9600
                comm1.DisplayWindow = rtbDisplay2
                comm1.OpenPort()
            ElseIf glbvar.INDTYPE = "M" Then
                commett1.Parity = "None"
                commett1.StopBits = 1
                commett1.DataBits = 8
                commett1.BaudRate = 9600
                commett1.DisplayWindow = rtbDisplay2
                commett1.OpenPort()
            ElseIf glbvar.INDTYPE = "Y" Then
                commetty1.Parity = "None"
                commetty1.StopBits = 1
                commetty1.DataBits = 8
                commetty1.BaudRate = 9600
                commetty1.DisplayWindow = rtbDisplay2
                commetty1.OpenPort()
            End If
            b_Disconnect2.Visible = True
            b_connect2.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub

    Private Sub b_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_edit.Click
        Try

            'If Me.tb_ticketno.Text <> "" Then
            '    conn = New OracleConnection(constr)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    Dim cmd1 As New OracleCommand
            '    cmd1.Connection = conn
            '    cmd1.Parameters.Clear()
            '    cmd1.CommandText = "curspkg_join.delete_lock"
            '    cmd1.CommandType = CommandType.StoredProcedure
            '    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = Clng(Me.tb_ticketno.Text)
            '    cmd1.ExecuteNonQuery()
            '    conn.Close()
            'End If
            clear_scr()
            tmode = 2
            Me.tb_ticketno.Enabled = True
            Me.tb_ticketno.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_ticketno_LostFocu(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles tb_ticketno.LostFocus
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT nvl(ticketno,0) ticketno FROM WBMLOCK WHERE TICKETNO = " & Me.tb_ticketno.Text
        Dim dalk = New OracleDataAdapter(sql, conn)
        Dim dslk As New DataSet
        dalk.Fill(dslk)
        conn.Close()
        If dslk.Tables(0).Rows.Count > 0 Then
            MsgBox("Transaction Open in another screen")
            'Me.tb_ticketno.Text = "0"
            Me.tb_ticketno.Focus()
        Else
            unfreeze_scr_new()
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
                    & "  FROM   stwbmibds_pr WBM" _
                    & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text _
                    & " and status in (1,2,3)"

                da = New OracleDataAdapter(sql, conn)
                Dim dstk As New DataSet
                Try

                    da.Fill(dstk)
                    conn.Close()
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
                cmd.CommandText = "curspkg_join_pr.tktrng"
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
                ElseIf cb_inouttype.SelectedValue = "W" Then
                    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCO"
                End If
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim dsrng As New DataSet
                    Dim darng As New OracleDataAdapter(cmd)
                    darng.TableMappings.Add("Table", "tktrng")
                    darng.Fill(dsrng)
                    If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                        Me.cb_fritem.Focus()
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
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG" _
                         & " from stwbmibds_pr where TICKETNO = " & Me.tb_ticketno.Text _
                         & " and status in (1,2,3)"
                    clear_scr()
                    da = New OracleDataAdapter(sql, conn)
                    'da.TableMappings.Add("Table", "mlt")
                    Dim ds As New DataSet
                    da.Fill(ds)
                    conn.Close()
                    If ds.Tables(0).Rows.Count > 0 Then
                        'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then

                        Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                        Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                        Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                        Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                        '    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                        'End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                        '    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                        'End If
                        If Me.cb_inouttype.Text = "T" Then
                            cb_sledcode.Hide()
                            tb_sledesc.Hide()
                            cb_fritem.Show()
                            tb_fritemdesc.Show()
                            l_Project.Text = "From Item"
                            l_tomat.Text = "To Item"
                            Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                            Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                            Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                        ElseIf Me.cb_inouttype.Text = "I" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "O" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Customer"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "S" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "W" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("ACCOUNTCODE"))) Then
                            Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                        End If
                        Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")
                        Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTITEMCODE"))) Then
                            Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                        End If
                        Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                        Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                            Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                            Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")
                            Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                        '    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                        'End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                        '    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                        'End If
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
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SLNO"))) Then
                            Me.tb_itmno.Text = ds.Tables(0).Rows(0).Item("SLNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TOTALPRICE"))) Then
                            Me.tb_TOTALPRICE.Text = ds.Tables(0).Rows(0).Item("TOTALPRICE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("RATE"))) Then
                            Me.tb_prlist.Text = ds.Tables(0).Rows(0).Item("RATE")
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
                        If cb_inouttype.Text = "I" Then
                            Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("BSART")
                            Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("BSART")
                        ElseIf cb_inouttype.Text = "O" Then
                            Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("AUART")
                            Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("AUART")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SORDERNO"))) Then
                            Me.tb_orderno.Text = ds.Tables(0).Rows(0).Item("SORDERNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DELIVERYNO"))) Then
                            Me.tb_dsno.Text = ds.Tables(0).Rows(0).Item("DELIVERYNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PONO"))) Then
                            Me.Tb_asno.Text = ds.Tables(0).Rows(0).Item("PONO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("AGMIXNO"))) Then
                            Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("AGMIXNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONSNO"))) Then
                            Me.Tb_cons_sen_branch.Text = ds.Tables(0).Rows(0).Item("CONSNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_CHARGE"))) Then
                            Me.Tb_transp.Text = ds.Tables(0).Rows(0).Item("TRANS_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PENALTY"))) Then
                            Me.Tb_penalty.Text = ds.Tables(0).Rows(0).Item("PENALTY")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MACHINE_CHARGE"))) Then
                            Me.Tb_eqpchrgs.Text = ds.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("LABOUR_CHARGE"))) Then
                            Me.Tb_labourcharges.Text = ds.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CCIC"))) Then
                            Me.Tb_ccic.Text = ds.Tables(0).Rows(0).Item("CCIC")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDCODE"))) Then
                            Me.tb_omcustcode.Text = ds.Tables(0).Rows(0).Item("OMSLEDCODE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDDESC"))) Then
                            Me.cb_omcustdesc.Text = ds.Tables(0).Rows(0).Item("OMSLEDDESC")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                            Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMPRICE"))) Then
                            Me.tb_omcustprice.Text = ds.Tables(0).Rows(0).Item("OMPRICE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNS"))) Then
                            Me.tb_sapord.Text = ds.Tables(0).Rows(0).Item("VBELNS")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELND"))) Then
                            Me.tb_sapdocno.Text = ds.Tables(0).Rows(0).Item("VBELND")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNI"))) Then
                            Me.tb_sapinvno.Text = ds.Tables(0).Rows(0).Item("VBELNI")
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        '    Me.cb_ib.Checked = True
                        'ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        '    Me.cb_ib.Checked = False
                        'End If


                        'update data table in case of multiple items.
                        Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                        & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                        & " from(stwbmibds_pr_MULTI)" _
                        & " where(INTDOCNO =" & Me.Tb_intdocno.Text & ")"
                        Dim da1 As New OracleDataAdapter(sql, conn)
                        da1.Fill(ds1)
                        'If Me.tb_IBDSNO.Text = "" Then
                        If Me.cb_inouttype.Text = "I" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            Me.b_genst.Visible = False
                            Me.Button1.Visible = False
                            ' Me.B_PO.Visible = True
                        ElseIf Me.cb_inouttype.Text = "O" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            'Me.Button1.Visible = True
                            Me.b_genst.Visible = False
                            Me.B_PO.Visible = False
                        ElseIf Me.cb_inouttype.Text = "T" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            Me.Button1.Visible = False
                            Me.b_genst.Visible = False
                            Me.b_transfer.Visible = False
                        End If
                        'Else
                        '    Me.b_gends.Visible = False
                        '    Me.b_genis.Visible = False
                        '    Me.b_genst.Visible = False
                        'Me.Button1.Visible = False
                        'Me.B_PO.Visible = False
                        'End If
                        Me.b_firstwt.Enabled = False
                        Me.b_firstwt2.Enabled = False
                        If Me.tb_SECONDQTY.Text = 0 Then
                            Me.b_secondwt.Enabled = True
                            Me.b_secondwt2.Enabled = True
                        End If
                        If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
                            'Me.B_PO.Visible = False
                            'Me.Button1.Visible = False
                            freeze_scr()
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
                        conn = New OracleConnection(constr)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        Dim cmd1 As New OracleCommand
                        cmd1.Connection = conn
                        cmd1.Parameters.Clear()
                        cmd1.CommandText = "curspkg_join_pr.insert_lock"
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                        cmd1.ExecuteNonQuery()
                        conn.Close()

                    Else
                        MsgBox("No Records Found for this ticket #", MsgBoxStyle.Information)
                        'Me.tb_ticketno.Focus()
                    End If

                    If Me.tb_sap_doc.Text = "QN" Then
                        Me.Tb_asno.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QI" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        'Me.cb_ib.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIB" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        'Me.cb_ib.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIM" Then
                        Me.Tb_cons_sen_branch.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIX" Then
                        Me.Tb_cons_sen_branch.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QMX" Then
                        'Me.b_mixmat.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QO" Then
                        Me.cb_omcustdesc.Enabled = True
                        Me.tb_omcustcode.Enabled = True
                        Me.tb_omcustprice.Enabled = True
                        Me.Tb_custktdt.Visible = True
                        Me.Label46.Enabled = True
                        Me.Label47.Enabled = True
                        Me.Label41.Visible = True
                        Me.cb_omcustdesc.Visible = True
                        Me.tb_omcustcode.Visible = True
                        Me.tb_omcustprice.Visible = True
                        Me.Tb_cust_ticket_no.Visible = True
                        'Me.Label38.Visible = True
                        Me.Label46.Visible = True
                        Me.Label47.Visible = True
                        'Me.tb_IBDSNO.Visible = True
                        'ElseIf Me.tb_sap_doc.Text = "QMX" Then
                        '   Me.tb_IBDSNO.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                        Me.tb_orderno.Visible = True
                        Me.tb_dsno.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                        Me.tb_orderno.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "ZCWR" Then
                        Me.tb_orderno.Visible = True
                    Else
                        'Me.Tb_asno.Visible = False
                        Me.Tb_cons_sen_branch.Visible = False
                        Me.tb_IBDSNO.Visible = False
                        Me.tb_orderno.Visible = False
                        Me.tb_dsno.Visible = False
                        'Me.cb_ib.Visible = False
                    End If

                    If cb_inouttype.Text = "I" Then
                        glbvar.temp_suppcode = Me.tb_sledesc.Text
                        glbvar.temp_suppdesc = Me.cb_sledcode.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                        sl_item_driv_load()
                    ElseIf cb_inouttype.Text = "O" Then
                        glbvar.temp_suppcode = Me.tb_sledesc.Text
                        glbvar.temp_suppdesc = Me.cb_sledcode.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                        cust_item_driv_load()
                    ElseIf cb_inouttype.Text = "T" Then
                        glbvar.temp_suppcode = Me.cb_fritem.Text
                        glbvar.temp_suppdesc = Me.tb_fritemdesc.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
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
            ElseIf tmode = 0 Then
            Else
                MsgBox("Please select New or edit or cancel")
            End If 'tmode enddif
        End If
    End Sub
    Private Sub tb_ticketno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_tkt.Click
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT nvl(ticketno,0) ticketno FROM WBMLOCK_PR WHERE TICKETNO = " & Me.tb_ticketno.Text
        Dim dalk = New OracleDataAdapter(sql, conn)
        Dim dslk As New DataSet
        dalk.Fill(dslk)
        conn.Close()
        If dslk.Tables(0).Rows.Count > 0 Then
            MsgBox("Transaction Open in another screen")
            'Me.tb_ticketno.Text = 0
            Me.tb_ticketno.Focus()
        Else
            unfreeze_scr()
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
                    & "  FROM   stwbmibds_pr WBM" _
                    & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text _
                    & " and status in (1,2,3)"

                da = New OracleDataAdapter(sql, conn)
                Dim dstk As New DataSet
                Try

                    da.Fill(dstk)
                    conn.Close()
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
                'check the ticketnumber for whther within range
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join_pr.tktrng"
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
                ElseIf cb_inouttype.SelectedValue = "W" Then
                    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCO"
                End If
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim dsrng As New DataSet
                    Dim darng As New OracleDataAdapter(cmd)
                    darng.TableMappings.Add("Table", "tktrng")
                    darng.Fill(dsrng)
                    If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                        Me.cb_fritem.Focus()
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
                    sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC,PRACCOUNTCODE, PRSLEDCODE, PRSLEDDESC," _
                         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,CFCREATED,MIXTRFTKT,IBTKTNO,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT,custtype,typecode,typecatg_pt,post_date,gpremarks,sprinted,gprinted" _
                         & " from stwbmibds_pr where inouttype = 'T' and TICKETNO = " & Me.tb_ticketno.Text _
                         & " and status in (1,2,3) and inouttype = 'T'"
                    clear_scr()
                    da = New OracleDataAdapter(sql, conn)
                    'da.TableMappings.Add("Table", "mlt")
                    Dim ds As New DataSet
                    da.Fill(ds)
                    conn.Close()
                    If ds.Tables(0).Rows.Count > 0 Then
                        'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then

                        Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                        Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                        Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                        Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                        '    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                        'End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                        '    Me.tb_transporter.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                        'End If
                        If Me.cb_inouttype.Text = "T" Then
                            'cb_sledcode.Hide()
                            'tb_sledesc.Hide()
                            cb_fritem.Show()
                            tb_fritemdesc.Show()
                            'l_Project.Text = ""
                            'l_tomat.Text = ""
                            Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                            Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                            Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                        ElseIf Me.cb_inouttype.Text = "I" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "O" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Customer"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "S" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        ElseIf Me.cb_inouttype.Text = "W" Then
                            cb_sledcode.Show()
                            tb_sledesc.Show()
                            cb_fritem.Hide()
                            tb_fritemdesc.Hide()
                            l_Project.Text = "Supplier"
                            l_tomat.Text = "Product"
                            Me.tb_frintitem.Text = 0
                            Me.cb_fritem.Text = "0"
                            Me.tb_fritemdesc.Text = "0"
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("ACCOUNTCODE"))) Then
                            Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                        End If
                        Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")
                        Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRACCOUNTCODE"))) Then
                            Me.tb_prjaccountcode.Text = ds.Tables(0).Rows(0).Item("PRACCOUNTCODE")
                        End If
                        Me.cb_prjsledcode.Text = ds.Tables(0).Rows(0).Item("PRSLEDDESC")
                        Me.tb_prjsledesc.Text = ds.Tables(0).Rows(0).Item("PRSLEDCODE")
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTITEMCODE"))) Then
                            Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                        End If
                        Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                        Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                            Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                            Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")
                            Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                        '    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                        'End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                        '    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                        'End If
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
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("POST_DATE"))) Then
                            Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("POST_DATE")
                        Else
                            If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                                Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                            End If
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                        '    If CDate(Me.tb_DATEOUT.Text).Month < Today.Month Then
                        '        Me.d_newdate.Enabled = True
                        '    End If
                        'End If
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
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SLNO"))) Then
                            Me.tb_itmno.Text = ds.Tables(0).Rows(0).Item("SLNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TOTALPRICE"))) Then
                            Me.tb_TOTALPRICE.Text = ds.Tables(0).Rows(0).Item("TOTALPRICE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("RATE"))) Then
                            Me.tb_prlist.Text = ds.Tables(0).Rows(0).Item("RATE")
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
                        If cb_inouttype.Text = "I" Then
                            Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("BSART")
                            Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("BSART")
                        ElseIf cb_inouttype.Text = "O" Then
                            Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("AUART")
                            Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("AUART")
                            'Me.Label25.Visible = True
                            'Me.rtb_gprem.Visible = True
                            'Me.b_gp.Visible = True
                            'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPREMARKS"))) Then
                            '    Me.rtb_gprem.Text = ds.Tables(0).Rows(0).Item("GPREMARKS")
                            'End If
                        ElseIf cb_inouttype.Text = "S" Then
                            'Me.Label25.Visible = True
                            'Me.rtb_gprem.Visible = True
                            'Me.b_gp.Visible = True
                            'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPREMARKS"))) Then
                            '    Me.rtb_gprem.Text = ds.Tables(0).Rows(0).Item("GPREMARKS")
                            'End If
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SORDERNO"))) Then
                            Me.tb_orderno.Text = ds.Tables(0).Rows(0).Item("SORDERNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DELIVERYNO"))) Then
                            Me.tb_dsno.Text = ds.Tables(0).Rows(0).Item("DELIVERYNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PONO"))) Then
                            Me.Tb_asno.Text = ds.Tables(0).Rows(0).Item("PONO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("AGMIXNO"))) Then
                            Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("AGMIXNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONSNO"))) Then
                            Me.Tb_cons_sen_branch.Text = ds.Tables(0).Rows(0).Item("CONSNO")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_CHARGE"))) Then
                            Me.Tb_transp.Text = ds.Tables(0).Rows(0).Item("TRANS_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PENALTY"))) Then
                            Me.Tb_penalty.Text = ds.Tables(0).Rows(0).Item("PENALTY")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MACHINE_CHARGE"))) Then
                            Me.Tb_eqpchrgs.Text = ds.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("LABOUR_CHARGE"))) Then
                            Me.Tb_labourcharges.Text = ds.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CCIC"))) Then
                            Me.Tb_ccic.Text = ds.Tables(0).Rows(0).Item("CCIC")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDCODE"))) Then
                            Me.tb_omcustcode.Text = ds.Tables(0).Rows(0).Item("OMSLEDCODE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDDESC"))) Then
                            Me.cb_omcustdesc.Text = ds.Tables(0).Rows(0).Item("OMSLEDDESC")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                            Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMPRICE"))) Then
                            Me.tb_omcustprice.Text = ds.Tables(0).Rows(0).Item("OMPRICE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNS"))) Then
                            Me.tb_sapord.Text = ds.Tables(0).Rows(0).Item("VBELNS")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELND"))) Then
                            Me.tb_sapdocno.Text = ds.Tables(0).Rows(0).Item("VBELND")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNI"))) Then
                            Me.tb_sapinvno.Text = ds.Tables(0).Rows(0).Item("VBELNI")
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        '    Me.cb_ib.Checked = True
                        'ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        '    Me.cb_ib.Checked = False
                        'End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                            Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                            Me.tb_CUSTTYPE.Text = ds.Tables(0).Rows(0).Item("CUSTTYPE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECODE"))) Then
                            Me.tb_typecode.Text = ds.Tables(0).Rows(0).Item("TYPECODE")
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                            Me.tb_typecatg_pt.Text = ds.Tables(0).Rows(0).Item("TYPECATG_PT")
                        End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SPRINTED"))) Then
                        '    Me.tb_sprinted.Text = ds.Tables(0).Rows(0).Item("SPRINTED")
                        'End If
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPRINTED"))) Then
                        '    Me.tb_gprinted.Text = ds.Tables(0).Rows(0).Item("GPRINTED")
                        'End If


                        'update data table in case of multiple items.
                        Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                        & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                        & " from(stwbmibds_pr_MULTI)" _
                        & " where(INTDOCNO =" & Me.Tb_intdocno.Text & ")"
                        Dim da1 As New OracleDataAdapter(sql, conn)
                        da1.Fill(ds1)
                        conn.Close()
                        'If Me.tb_IBDSNO.Text = "" Then
                        If Me.cb_inouttype.Text = "I" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            Me.b_genst.Visible = False
                            Me.Button1.Visible = False
                            If Me.tb_sap_doc.Text = "QX" Or Me.tb_sap_doc.Text = "QIM" Then
                                Me.B_PO.Visible = True
                            Else
                                Me.B_PO.Visible = False
                            End If
                        ElseIf Me.cb_inouttype.Text = "O" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            If Me.tb_sap_doc.Text = "ZDCQ" Or Me.tb_sap_doc.Text = "ZTCF" Or Me.tb_sap_doc.Text = "ZCWA" Or Me.tb_sap_doc.Text = "ZTRE" Or Me.tb_sap_doc.Text = "ZCWR" Then
                                Me.Button1.Visible = True
                            Else
                                Me.Button1.Visible = False
                            End If
                            Me.b_genst.Visible = False
                            Me.B_PO.Visible = False
                            If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MIXTRFTKT"))) Then
                                Me.b_crfillup.Visible = False
                                Me.b_crfillup.Enabled = False
                            Else
                                Me.b_crfillup.Visible = True
                                Me.b_crfillup.Enabled = True
                            End If
                            If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MIXTRFTKT"))) Then
                                If Not (IsDBNull(ds.Tables(0).Rows(0).Item("IBTKTNO"))) Then
                                    Me.b_cribpur.Visible = False
                                    Me.b_cribpur.Enabled = False
                                Else
                                    Me.b_cribpur.Visible = True
                                    Me.b_cribpur.Enabled = True
                                End If
                            End If
                        ElseIf Me.cb_inouttype.Text = "T" Then
                            Me.b_genis.Visible = False
                            Me.b_gends.Visible = False
                            Me.Button1.Visible = False
                            Me.b_genst.Visible = False
                            Me.b_transfer.Visible = True
                            If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CFCREATED"))) Then
                                Me.b_crfillup.Visible = False
                                Me.b_crfillup.Enabled = False
                            Else
                                If Me.tb_sapinvno.Text <> "" Then
                                    Me.b_crfillup.Visible = True
                                    Me.b_crfillup.Enabled = True
                                End If
                            End If
                        End If
                        'Else
                        '    Me.b_gends.Visible = False
                        '    Me.b_genis.Visible = False
                        '    Me.b_genst.Visible = False
                        'Me.Button1.Visible = False
                        'Me.B_PO.Visible = False
                        'End If
                        Me.b_firstwt.Enabled = False
                        If Me.tb_SECONDQTY.Text = 0 Then
                            Me.b_secondwt.Enabled = True
                            Me.b_secondwt2.Enabled = True
                        End If
                        If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
                            'Me.B_PO.Visible = False
                            'Me.Button1.Visible = False
                            freeze_scr()
                        End If
                        If (IsDBNull(ds.Tables(0).Rows(0).Item("CFCREATED"))) Then
                            If Me.tb_SECONDQTY.Text <> 0 Then
                                If Me.tb_sapinvno.Text <> "" Then
                                    Me.b_crfillup.Visible = True
                                    Me.b_crfillup.Enabled = True
                                End If
                            Else
                                Me.b_crfillup.Visible = False
                                Me.b_crfillup.Enabled = False
                            End If
                        Else
                            Me.b_crfillup.Visible = False
                            Me.b_crfillup.Enabled = False
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
                        conn = New OracleConnection(constr)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        Dim cmd1 As New OracleCommand
                        cmd1.Connection = conn
                        cmd1.Parameters.Clear()
                        cmd1.CommandText = "curspkg_join_pr.insert_lock"
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                        cmd1.ExecuteNonQuery()
                        conn.Close()

                    Else
                        MsgBox("No Records Found for this ticket #", MsgBoxStyle.Information)
                        'Me.tb_ticketno.Focus()
                        Me.b_edit.Focus()
                    End If



                    If cb_inouttype.Text = "I" Then
                        glbvar.temp_suppcode = Me.tb_sledesc.Text
                        glbvar.temp_suppdesc = Me.cb_sledcode.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                        sl_item_driv_load()
                    ElseIf cb_inouttype.Text = "O" Then
                        glbvar.temp_suppcode = Me.tb_sledesc.Text
                        glbvar.temp_suppdesc = Me.cb_sledcode.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                        cust_item_driv_load()
                    ElseIf cb_inouttype.Text = "T" Then
                        glbvar.temp_suppcode = Me.tb_sledesc.Text
                        glbvar.temp_suppdesc = Me.cb_sledcode.Text
                        glbvar.temp_prsuppcode = Me.tb_prjsledesc.Text
                        glbvar.temp_prsuppdesc = Me.cb_prjsledcode.Text
                        glbvar.temp_fritemcode = Me.cb_fritem.Text
                        glbvar.temp_fritemdesc = Me.tb_fritemdesc.Text
                        glbvar.temp_itemcode = Me.cb_itemcode.Text
                        glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                        glbvar.temp_drcode = Me.cb_dcode.Text
                        glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                        glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                        glbvar.temp_docdesc = Me.tb_sap_doc.Text
                        glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                        glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                        tran_item_driv_load()
                    End If
                    If Me.tb_sap_doc.Text = "QN" Then
                        Me.Tb_asno.Visible = True
                        Me.l_pono.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QI" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        'Me.cb_ib.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIB" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        'Me.cb_ib.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIM" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QMX" Then
                        'Me.b_mixmat.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIX" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QO" Then
                        Me.cb_omcustdesc.Enabled = True
                        Me.tb_omcustcode.Enabled = True
                        Me.tb_omcustprice.Enabled = True
                        Me.Tb_custktdt.Visible = True
                        Me.Label46.Enabled = True
                        Me.Label47.Enabled = True
                        Me.Label41.Visible = True
                        Me.cb_omcustdesc.Visible = True
                        Me.tb_omcustcode.Visible = True
                        Me.tb_omcustprice.Visible = True
                        Me.Tb_cust_ticket_no.Visible = True
                        'Me.Label38.Visible = True
                        Me.Label46.Visible = True
                        Me.Label47.Visible = True
                        'Me.tb_IBDSNO.Visible = True
                        'ElseIf Me.tb_sap_doc.Text = "QMX" Then
                        '   Me.tb_IBDSNO.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                        Me.tb_orderno.Visible = True
                        Me.tb_dsno.Visible = True
                        Me.l_dsno.Visible = True
                        Me.l_so.Visible = True
                        Me.l_so.Text = "SO #"
                    ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                        Me.tb_orderno.Visible = True
                        Me.l_so.Visible = True
                        Me.l_so.Text = "RO #"
                    ElseIf Me.tb_sap_doc.Text = "ZCWR" Then
                        Me.tb_orderno.Visible = True
                        Me.l_so.Visible = True
                        Me.l_so.Text = "Billing #"
                    Else
                        'Me.Tb_asno.Visible = False
                        Me.Tb_cons_sen_branch.Visible = False
                        Me.tb_IBDSNO.Visible = False
                        Me.tb_orderno.Visible = False
                        Me.tb_dsno.Visible = False
                        'Me.cb_ib.Visible = False
                        l_agmix.Visible = False
                        l_cons.Visible = False
                        l_dsno.Visible = False
                        'l_pono.Visible = False
                        l_so.Visible = False
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
            ElseIf tmode = 0 Then
            Else
                MsgBox("Please select New or edit or cancel")
            End If 'tmode enddif
        End If
        conn.Close()
    End Sub
    Private Sub b_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_save.Click

        If tmode = 1 Then
            If Me.tb_vehicleno.Text = "" Then
                MsgBox("Enter Vehicle #")
                Me.tb_vehicleno.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox("Enter First Weight")
            Else
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
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_i"
                    cmd.CommandType = CommandType.StoredProcedure
                    'Try

                    cmd.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.SelectedValue
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                    cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = DBNull.Value
                    'If IsDBNull(Me.tb_container.Text) Then
                    'Me.tb_container.Text = ""
                    'Else
                    '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                    'End If
                    cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = DBNull.Value
                    If cb_inouttype.SelectedValue = "T" Then
                        cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("ppACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_prjaccountcode.Text
                        cmd.Parameters.Add(New OracleParameter("ppSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_prjsledesc.Text
                        cmd.Parameters.Add(New OracleParameter("ppSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_prjsledcode.Text
                    Else
                        cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                        cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                        cmd.Parameters.Add(New OracleParameter("ppACCOUNTCODE", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("ppSLEDCODE", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("ppSLEDDESC", OracleDbType.Varchar2)).Value = DBNull.Value
                    End If
                    cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                    cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                    cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

                    If Me.cb_inouttype.SelectedValue <> "T" Then
                        cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
                        cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = "Dummy"
                        cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = "00000"
                    ElseIf Me.cb_inouttype.SelectedValue = "T" Then

                        cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
                        cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
                        cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()
                    End If
                    cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = Me.tb_numberofpcs.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                    cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pFIRSTQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_FIRSTQTY.Text)
                    Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
                    cmd.Parameters.Add(New OracleParameter("pDATEIN", OracleDbType.Date)).Value = dtin 'Convert.ToDateTime(Me.tb_DATEIN.Text)
                    cmd.Parameters.Add(New OracleParameter("pTIMEIN", OracleDbType.Varchar2)).Value = Me.tb_TIMEIN.Text
                    cmd.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
                    cmd.Parameters.Add(New OracleParameter("pAPPDATE0", OracleDbType.Date)).Value = Today
                    cmd.Parameters.Add(New OracleParameter("pFIELD1", OracleDbType.Varchar2)).Value = glbvar.userid
                    cmd.Parameters.Add(New OracleParameter("pSTATUS", OracleDbType.Varchar2)).Value = 1
                    cmd.Parameters.Add(New OracleParameter("pDEDUCTIONWT", OracleDbType.Decimal)).Value = CInt(Me.tb_DEDUCTIONWT.Text)
                    cmd.Parameters.Add(New OracleParameter("pPACKDED", OracleDbType.Decimal)).Value = CInt(Me.tb_packded.Text)
                    cmd.Parameters.Add(New OracleParameter("pDED", OracleDbType.Decimal)).Value = CInt(Me.tb_ded.Text)
                    cmd.Parameters.Add(New OracleParameter("pprice", OracleDbType.Decimal)).Value = CDec(Me.tb_PRICETON.Text)
                    cmd.Parameters.Add(New OracleParameter("ptotprice", OracleDbType.Decimal)).Value = CDec(Me.tb_TOTALPRICE.Text)
                    cmd.Parameters.Add(New OracleParameter("pprlist", OracleDbType.Decimal)).Value = CDec(Me.tb_prlist.Text)
                    cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Direction = ParameterDirection.Output
                    If cb_inouttype.SelectedValue = "I" Then


                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
                    ElseIf cb_inouttype.SelectedValue = "O" Then

                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
                    Else
                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
                    End If
                    If cb_inouttype.SelectedValue = "I" Then


                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = glbvar.EKORG
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = glbvar.EKGRP
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
                    ElseIf cb_inouttype.SelectedValue = "O" Then


                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = glbvar.VKORG
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = glbvar.VTWEG
                    Else
                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
                    End If

                    cmd.Parameters.Add(New OracleParameter("psVBELNS", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psVBELND", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psVBELNI", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psorderno", OracleDbType.Varchar2)).Value = Me.tb_orderno.Text
                    cmd.Parameters.Add(New OracleParameter("pdeliveryno", OracleDbType.Varchar2)).Value = Me.tb_dsno.Text
                    cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
                    cmd.Parameters.Add(New OracleParameter("pitmno", OracleDbType.Varchar2)).Value = Me.tb_itmno.Text
                    cmd.Parameters.Add(New OracleParameter("ptransportcharges", OracleDbType.Varchar2)).Value = Me.Tb_transp.Text
                    cmd.Parameters.Add(New OracleParameter("ppenalty", OracleDbType.Varchar2)).Value = Me.Tb_penalty.Text
                    cmd.Parameters.Add(New OracleParameter("pmachinecharges", OracleDbType.Varchar2)).Value = Me.Tb_eqpchrgs.Text
                    cmd.Parameters.Add(New OracleParameter("plabourcharges", OracleDbType.Varchar2)).Value = Me.Tb_labourcharges.Text
                    cmd.Parameters.Add(New OracleParameter("ppono", OracleDbType.Varchar2)).Value = Me.Tb_asno.Text
                    cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
                    cmd.Parameters.Add(New OracleParameter("pconsno", OracleDbType.Varchar2)).Value = Me.Tb_cons_sen_branch.Text
                    cmd.Parameters.Add(New OracleParameter("pccic", OracleDbType.Varchar2)).Value = Me.Tb_ccic.Text
                    cmd.Parameters.Add(New OracleParameter("pomprice", OracleDbType.Varchar2)).Value = Me.tb_omcustprice.Text
                    cmd.Parameters.Add(New OracleParameter("pomsledcode", OracleDbType.Varchar2)).Value = Me.tb_omcustcode.Text
                    cmd.Parameters.Add(New OracleParameter("pomsleddesc", OracleDbType.Varchar2)).Value = Me.cb_omcustdesc.Text
                    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = ""
                    'If cb_ib.Checked = True Then
                    '    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
                    'ElseIf cb_ib.Checked = False Then

                    'End If
                    cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
                    cmd.Parameters.Add(New OracleParameter("ppcusttype", OracleDbType.Varchar2)).Value = Me.tb_CUSTTYPE.Text
                    cmd.Parameters.Add(New OracleParameter("pptypecode", OracleDbType.Varchar2)).Value = Me.tb_typecode.Text
                    cmd.Parameters.Add(New OracleParameter("pptypecatg_pt", OracleDbType.Varchar2)).Value = Me.tb_typecatg_pt.Text
                    cmd.Parameters.Add(New OracleParameter("pdivdesc", OracleDbType.Varchar2)).Value = glbvar.gcompname
                    cmd.Parameters.Add(New OracleParameter("pgprem", OracleDbType.Varchar2)).Value = DBNull.Value
                    Try
                        cmd.ExecuteNonQuery()
                        'Dim vint As Decimal
                        'vint = cmd.Parameters("pINTDOCNO").Value.ToString  'CDec(cmd.Parameters("pINTDOCNO").Value)
                        Me.Tb_intdocno.Text = cmd.Parameters("pINTDOCNO").Value.ToString
                        'glbvar.multdocno = Me.Tb_intdocno.Text
                        'glbvar.multtktno = Me.tb_ticketno.Text
                        'glbvar.multinout = Me.cb_inouttype.Text
                        conn.Close()
                        Me.b_firstwt.Enabled = False
                        Me.b_firstwt2.Enabled = False
                        MsgBox("Record Saved")
                        conn = New OracleConnection(constr)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        Dim cmd1 As New OracleCommand
                        cmd1.Connection = conn
                        cmd1.Parameters.Clear()
                        cmd1.CommandText = "curspkg_join_pr.insert_lock"
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                        cmd1.ExecuteNonQuery()
                        conn.Close()
                        'clear_scr()
                    Catch ex As Exception
                        MsgBox(ex.Message.ToString)
                        conn.Close()
                    End Try
                End If
            End If
        ElseIf tmode = 2 Then
            If Me.tb_TIMOUT.Text = "" AndAlso Me.tb_SECONDQTY.Text <> 0 Then
                MsgBox("Time Out is blank")
            Else

                'Dim constr As String = My.Settings.Item("ConnString")
                'Dim abc
                'Dim chk = 0
                'abc = glbvar.p_mitem
                'If IsNothing(abc) Then
                '    chk = 1
                'Else
                '    For i = 0 To glbvar.p_mitem.Count - 1
                '        abc = glbvar.p_mitem(i)
                '        If abc = 0 Then
                '            chk = 1
                '        End If
                '    Next
                'End If
                'If Me.tb_sap_doc.Text = "QIX" AndAlso chk > 0 Or Me.tb_sap_doc.Text = "QMX" AndAlso chk > 0 Then
                '    MsgBox("Check Mix Material Details")
                '    glbvar.vntwt = CInt(Me.tb_QTY.Text)
                '    glbvar.multdocno = Me.Tb_intdocno.Text
                '    glbvar.inout = Me.cb_inouttype.Text
                '    glbvar.multkt = Me.tb_ticketno.Text
                '    glbvar.sapdocmulti = Me.tb_sap_doc.Text
                '    Dim frm As New MIX
                '    frm.Show()
                'Else
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_u"
                cmd.CommandType = CommandType.StoredProcedure
                Try
                    cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                    cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
                    cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                    cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                    cmd.Parameters.Add(New OracleParameter("ppACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.tb_prjaccountcode.Text
                    cmd.Parameters.Add(New OracleParameter("ppSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_prjsledesc.Text
                    cmd.Parameters.Add(New OracleParameter("ppSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_prjsledcode.Text
                    cmd.Parameters.Add(New OracleParameter("pcSLEDCODE", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pcSLEDDESC", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
                    cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
                    cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

                    cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
                    cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
                    cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()

                    cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = CInt(Me.tb_numberofpcs.Text)

                    cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
                    cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
                    cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = DBNull.Value
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
                    If Me.tb_prlist.Text <> "" Then
                        cmd.Parameters.Add(New OracleParameter("pprlist", OracleDbType.Decimal)).Value = CDec(Me.tb_prlist.Text)
                    Else
                        cmd.Parameters.Add(New OracleParameter("pprlist", OracleDbType.Decimal)).Value = 0.0
                    End If

                    If cb_inouttype.Text = "I" Then


                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
                    ElseIf cb_inouttype.Text = "O" Then

                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
                    Else
                        cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
                    End If
                    If cb_inouttype.Text = "I" Then


                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = glbvar.EKORG
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = glbvar.EKGRP
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
                    ElseIf cb_inouttype.Text = "O" Then


                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = glbvar.VKORG
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = glbvar.VTWEG
                    Else
                        cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
                    End If
                    cmd.Parameters.Add(New OracleParameter("psVBELNS", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psVBELND", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psVBELNI", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("psorderno", OracleDbType.Varchar2)).Value = Me.tb_orderno.Text
                    cmd.Parameters.Add(New OracleParameter("pdeliveryno", OracleDbType.Varchar2)).Value = Me.tb_dsno.Text
                    cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
                    cmd.Parameters.Add(New OracleParameter("pitmno", OracleDbType.Varchar2)).Value = Me.tb_itmno.Text
                    cmd.Parameters.Add(New OracleParameter("ptransportcharges", OracleDbType.Varchar2)).Value = Me.Tb_transp.Text
                    cmd.Parameters.Add(New OracleParameter("ppenalty", OracleDbType.Varchar2)).Value = Me.Tb_penalty.Text
                    cmd.Parameters.Add(New OracleParameter("pmachinecharges", OracleDbType.Varchar2)).Value = Me.Tb_eqpchrgs.Text
                    cmd.Parameters.Add(New OracleParameter("plabourcharges", OracleDbType.Varchar2)).Value = Me.Tb_labourcharges.Text
                    cmd.Parameters.Add(New OracleParameter("ppono", OracleDbType.Varchar2)).Value = Me.Tb_asno.Text
                    cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
                    cmd.Parameters.Add(New OracleParameter("pconsno", OracleDbType.Varchar2)).Value = Me.Tb_cons_sen_branch.Text
                    cmd.Parameters.Add(New OracleParameter("pccic", OracleDbType.Varchar2)).Value = Me.Tb_ccic.Text
                    cmd.Parameters.Add(New OracleParameter("pomprice", OracleDbType.Varchar2)).Value = Me.tb_omcustprice.Text
                    cmd.Parameters.Add(New OracleParameter("pomsledcode", OracleDbType.Varchar2)).Value = Me.tb_omcustcode.Text
                    cmd.Parameters.Add(New OracleParameter("pomsleddesc", OracleDbType.Varchar2)).Value = Me.cb_omcustdesc.Text
                    'If cb_ib.Checked = True Then
                    '    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
                    'ElseIf cb_ib.Checked = False Then
                    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = ""
                    'End If
                    cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
                    cmd.Parameters.Add(New OracleParameter("ppcusttype", OracleDbType.Varchar2)).Value = Me.tb_CUSTTYPE.Text
                    cmd.Parameters.Add(New OracleParameter("pptypecode", OracleDbType.Varchar2)).Value = Me.tb_typecode.Text
                    cmd.Parameters.Add(New OracleParameter("pptypecatg_pt", OracleDbType.Varchar2)).Value = Me.tb_typecatg_pt.Text
                    Dim ndt As Date = FormatDateTime(Me.d_newdate.Text, DateFormat.GeneralDate)
                    cmd.Parameters.Add(New OracleParameter("ppostdate", OracleDbType.Date)).Value = ndt
                    cmd.Parameters.Add(New OracleParameter("pgprem", OracleDbType.Varchar2)).Value = DBNull.Value
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    If cb_multival.Checked = True Then
                        If itmalloc = True Then
                            ReDim glbvar.pindocn(glbvar.itmcde.Count - 1)
                            ReDim glbvar.ptktno(glbvar.itmcde.Count - 1)
                            ReDim glbvar.pino(glbvar.itmcde.Count - 1)
                            'Dim i As Integer
                            For n = 0 To glbvar.itmcde.Count - 1
                                glbvar.pindocn(n) = CInt(Me.Tb_intdocno.Text)
                                glbvar.ptktno(n) = CLng(Me.tb_ticketno.Text)
                                glbvar.pino(n) = Me.cb_inouttype.Text
                            Next
                            conn = New OracleConnection(constr)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.Connection = conn
                            Try
                                cmd.Parameters.Clear()
                                cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_uArr"
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

                                'Dim pINTITEMCODE As OracleParameter = New OracleParameter("p4", OracleDbType.Int32)
                                'pINTITEMCODE.Direction = ParameterDirection.Input
                                'pINTITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                                'pINTITEMCODE.Value = glbvar.intiem

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

                                Dim ppitem As OracleParameter = New OracleParameter(":p12", OracleDbType.Decimal)
                                ppitem.Direction = ParameterDirection.Input
                                ppitem.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                                ppitem.Value = glbvar.pitem

                                Dim pmded As OracleParameter = New OracleParameter(":p13", OracleDbType.Decimal)
                                pmded.Direction = ParameterDirection.Input
                                pmded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                                pmded.Value = glbvar.pmultided

                                Dim ppded As OracleParameter = New OracleParameter(":p14", OracleDbType.Decimal)
                                ppded.Direction = ParameterDirection.Input
                                ppded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                                ppded.Value = glbvar.ppackded

                                Dim ppomprice As OracleParameter = New OracleParameter(":p15", OracleDbType.Decimal)
                                ppomprice.Direction = ParameterDirection.Input
                                ppomprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                                ppomprice.Value = glbvar.pomprice


                                cmd.Parameters.Add(pINTDOCNO)
                                cmd.Parameters.Add(pINOUTTYPE)
                                cmd.Parameters.Add(pTICKETNO)
                                'cmd.Parameters.Add(pINTITEMCODE)
                                cmd.Parameters.Add(pITEMCODE)
                                cmd.Parameters.Add(pITEMDESC)
                                cmd.Parameters.Add(pFIRSTQTY)
                                cmd.Parameters.Add(pSECONDQTY)
                                cmd.Parameters.Add(pQTY)
                                cmd.Parameters.Add(pprice)
                                cmd.Parameters.Add(ptotprice)
                                cmd.Parameters.Add(ppitem)
                                cmd.Parameters.Add(pmded)
                                cmd.Parameters.Add(ppded)
                                cmd.Parameters.Add(ppomprice)
                                cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
                                cmd.ExecuteNonQuery()
                                'multi_itm.DataGridView1.Rows.Clear()
                                'cmd.Parameters.Clear()
                                'clear_scr()
                            Catch ex As Exception
                                MsgBox(ex.Message.ToString)
                            End Try
                            'End Try
                            conn.Close()
                        End If
                    Else
                        Try
                            conn = New OracleConnection(constr)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            Dim cmdd As New OracleCommand
                            cmdd.Connection = conn
                            cmdd.Parameters.Clear()
                            cmdd.CommandText = "delete from stwbmibds_multi_pr where ticketno = " & tb_ticketno.Text
                            cmdd.CommandType = CommandType.Text
                            cmdd.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    End If
                    'If Me.tb_sap_doc.Text = "QIX" Or Me.tb_sap_doc.Text = "QMX" Then

                    '    Dim a = glbvar.p_mitem
                    '    If IsNothing(a) Then
                    '        MsgBox("Enter Mix Material Details")
                    '        'Dim frm As New MIX
                    '        'frm.Show()
                    '    Else
                    '        ReDim glbvar.pindocn(glbvar.p_mitem.Count - 1)
                    '        ReDim glbvar.ptktno(glbvar.p_mitem.Count - 1)
                    '        ReDim glbvar.psapdoccode(glbvar.p_mitem.Count - 1)
                    '        'Dim i As Integer
                    '        For n = 0 To glbvar.p_mitem.Count - 1
                    '            glbvar.pindocn(n) = CInt(Me.Tb_intdocno.Text)
                    '            glbvar.ptktno(n) = Clng(Me.tb_ticketno.Text)
                    '            glbvar.psapdoccode(n) = Me.tb_sap_doc.Text
                    '        Next
                    '        conn = New OracleConnection(constr)
                    '        If conn.State = ConnectionState.Closed Then
                    '            conn.Open()
                    '        End If
                    '        cmd.Connection = conn
                    '        Try
                    '            cmd.Parameters.Clear()
                    '            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_mixarr"
                    '            cmd.CommandType = CommandType.StoredProcedure
                    '            'cmd.ArrayBindCount = glbvar.intiem.Count
                    '            Dim pINTDOCNO As OracleParameter = New OracleParameter(":p1", OracleDbType.Int32)
                    '            pINTDOCNO.Direction = ParameterDirection.Input
                    '            pINTDOCNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            pINTDOCNO.Value = glbvar.pindocn

                    '            Dim pTICKETNO As OracleParameter = New OracleParameter(":p3", OracleDbType.Int32)
                    '            pTICKETNO.Direction = ParameterDirection.Input
                    '            pTICKETNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            pTICKETNO.Value = glbvar.ptktno

                    '            Dim ppsapdoc As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
                    '            ppsapdoc.Direction = ParameterDirection.Input
                    '            ppsapdoc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            ppsapdoc.Value = glbvar.psapdoccode

                    '            Dim ppono As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
                    '            ppono.Direction = ParameterDirection.Input
                    '            ppono.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            ppono.Value = glbvar.p_mpono

                    '            Dim pslno As OracleParameter = New OracleParameter(":p12", OracleDbType.Decimal)
                    '            pslno.Direction = ParameterDirection.Input
                    '            pslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            pslno.Value = glbvar.p_mitem

                    '            Dim pQTY As OracleParameter = New OracleParameter(":p9", OracleDbType.Decimal)
                    '            pQTY.Direction = ParameterDirection.Input
                    '            pQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            pQTY.Value = glbvar.p_mqty

                    '            Dim pcomflg As OracleParameter = New OracleParameter(":p9", OracleDbType.Char)
                    '            pcomflg.Direction = ParameterDirection.Input
                    '            pcomflg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    '            pcomflg.Value = glbvar.p_mcomflg


                    '            cmd.Parameters.Add(pINTDOCNO)
                    '            cmd.Parameters.Add(pTICKETNO)
                    '            cmd.Parameters.Add(ppsapdoc)
                    '            cmd.Parameters.Add(ppono)
                    '            cmd.Parameters.Add(pslno)
                    '            cmd.Parameters.Add(pQTY)
                    '            cmd.Parameters.Add(pcomflg)
                    '            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
                    '            cmd.Parameters.Add(New OracleParameter("errmsg", OracleDbType.Varchar2)).Direction = ParameterDirection.Output
                    '            cmd.ExecuteNonQuery()
                    '            'multi_itm.DataGridView1.Rows.Clear()
                    '            'cmd.Parameters.Clear()
                    '            'clear_scr()

                    '            glbvar.p_mpono = Nothing
                    '            glbvar.p_mitem = Nothing
                    '            glbvar.p_mqty = Nothing
                    '            glbvar.p_mcomflg = Nothing

                    '        Catch ex As Exception
                    '            MsgBox(ex.Message.ToString)
                    '        End Try
                    '    End If
                    'End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                Finally
                    itmalloc = False
                    multi_itm.DataGridView1.Rows.Clear()
                    MIX.MIXGRID.Rows.Clear()
                    'ReDim p_mitem(0)
                    MsgBox("Record Saved")
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd1 As New OracleCommand
                    cmd1.Connection = conn
                    cmd1.Parameters.Clear()
                    cmd1.CommandText = "curspkg_join_pr.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                    cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                    cmd1.ExecuteNonQuery()
                    conn.Close()
                End Try
                'End If 'QMX
            End If ' Timout
        End If 'tmode


        'ReDim itmcde(0)
        'ReDim itemdes(0)
        'ReDim pqty(0)
        'ReDim pfswt(0)
        'ReDim pscwt(0)
        'ReDim ppricekg(0)
        'ReDim prate(0)
        'ReDim pitem(0)
        'ReDim pmultided(0)
        'ReDim ppackded(0)
        'ReDim pomprice(0)

    End Sub
    Public Sub save_multi()
        Dim cmd As New OracleCommand
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd
        conn = New OracleConnection(constr)

        'If itmalloc = True Then
        ReDim glbvar.pindocn(glbvar.itmcde.Count - 1)
        ReDim glbvar.ptktno(glbvar.itmcde.Count - 1)
        ReDim glbvar.pino(glbvar.itmcde.Count - 1)
        'Dim i As Integer
        For n = 0 To glbvar.itmcde.Count - 1
            glbvar.pindocn(n) = glbvar.multdocno
            glbvar.ptktno(n) = glbvar.multkt
            glbvar.pino(n) = glbvar.inout
        Next
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Connection = conn
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_uArr"
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

            'Dim pINTITEMCODE As OracleParameter = New OracleParameter("p4", OracleDbType.Int32)
            'pINTITEMCODE.Direction = ParameterDirection.Input
            'pINTITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            'pINTITEMCODE.Value = glbvar.intiem

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

            Dim ppitem As OracleParameter = New OracleParameter(":p12", OracleDbType.Decimal)
            ppitem.Direction = ParameterDirection.Input
            ppitem.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppitem.Value = glbvar.pitem

            Dim pmded As OracleParameter = New OracleParameter(":p13", OracleDbType.Decimal)
            pmded.Direction = ParameterDirection.Input
            pmded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pmded.Value = glbvar.pmultided

            Dim ppded As OracleParameter = New OracleParameter(":p14", OracleDbType.Decimal)
            ppded.Direction = ParameterDirection.Input
            ppded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppded.Value = glbvar.ppackded

            Dim ppomprice As OracleParameter = New OracleParameter(":p15", OracleDbType.Decimal)
            ppomprice.Direction = ParameterDirection.Input
            ppomprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppomprice.Value = glbvar.pomprice


            cmd.Parameters.Add(pINTDOCNO)
            cmd.Parameters.Add(pINOUTTYPE)
            cmd.Parameters.Add(pTICKETNO)
            'cmd.Parameters.Add(pINTITEMCODE)
            cmd.Parameters.Add(pITEMCODE)
            cmd.Parameters.Add(pITEMDESC)
            cmd.Parameters.Add(pFIRSTQTY)
            cmd.Parameters.Add(pSECONDQTY)
            cmd.Parameters.Add(pQTY)
            cmd.Parameters.Add(pprice)
            cmd.Parameters.Add(ptotprice)
            cmd.Parameters.Add(ppitem)
            cmd.Parameters.Add(pmded)
            cmd.Parameters.Add(ppded)
            cmd.Parameters.Add(ppomprice)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = glbvar.multkt
            cmd.ExecuteNonQuery()
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        'End Try
        conn.Close()
        'End If
    End Sub
    Public Sub save_mix()
        Dim cmd As New OracleCommand
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd
        conn = New OracleConnection(constr)
        ReDim glbvar.pindocn(glbvar.p_mitem.Count - 1)
        ReDim glbvar.ptktno(glbvar.p_mitem.Count - 1)
        ReDim glbvar.psapdoccode(glbvar.p_mitem.Count - 1)
        'Dim i As Integer
        For n = 0 To glbvar.p_mitem.Count - 1
            glbvar.pindocn(n) = glbvar.multdocno
            glbvar.ptktno(n) = glbvar.multkt
            glbvar.psapdoccode(n) = glbvar.sapdocmulti
        Next
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Connection = conn
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_mixarr"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.ArrayBindCount = glbvar.intiem.Count
            Dim pINTDOCNO As OracleParameter = New OracleParameter(":p1", OracleDbType.Int32)
            pINTDOCNO.Direction = ParameterDirection.Input
            pINTDOCNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pINTDOCNO.Value = glbvar.pindocn

            Dim pTICKETNO As OracleParameter = New OracleParameter(":p3", OracleDbType.Int32)
            pTICKETNO.Direction = ParameterDirection.Input
            pTICKETNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pTICKETNO.Value = glbvar.ptktno

            Dim ppsapdoc As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
            ppsapdoc.Direction = ParameterDirection.Input
            ppsapdoc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppsapdoc.Value = glbvar.psapdoccode

            Dim ppono As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
            ppono.Direction = ParameterDirection.Input
            ppono.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppono.Value = glbvar.p_mpono

            Dim pslno As OracleParameter = New OracleParameter(":p12", OracleDbType.Decimal)
            pslno.Direction = ParameterDirection.Input
            pslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pslno.Value = glbvar.p_mitem

            Dim pQTY As OracleParameter = New OracleParameter(":p9", OracleDbType.Decimal)
            pQTY.Direction = ParameterDirection.Input
            pQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pQTY.Value = glbvar.p_mqty

            Dim pcomflg As OracleParameter = New OracleParameter(":p9", OracleDbType.Char)
            pcomflg.Direction = ParameterDirection.Input
            pcomflg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pcomflg.Value = glbvar.p_mcomflg


            cmd.Parameters.Add(pINTDOCNO)
            cmd.Parameters.Add(pTICKETNO)
            cmd.Parameters.Add(ppsapdoc)
            cmd.Parameters.Add(ppono)
            cmd.Parameters.Add(pslno)
            cmd.Parameters.Add(pQTY)
            cmd.Parameters.Add(pcomflg)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = glbvar.multkt
            cmd.Parameters.Add(New OracleParameter("errmsg", OracleDbType.Varchar2)).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        conn.Close()
    End Sub
    Private Sub b_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Disconnect.Click
        Try
            Me.rtbDisplay.Text = ""
            comm.ClosePort()
            commett.ClosePort()
            commetty.ClosePort()
            b_Disconnect.Visible = False
            b_connect.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_Disconnect2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Disconnect2.Click
        Try
            Me.rtbDisplay.Text = ""
            comm1.ClosePort()
            commett1.ClosePort()
            commetty1.ClosePort()
            b_Disconnect2.Visible = False
            b_connect2.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cb_sledcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_sledcode.LostFocus
        tb_searchbyno.Focus()
    End Sub
    Private Sub tb_searchbyno_LostFocus(sender As Object, e As EventArgs) Handles tb_searchbyno.LostFocus
        If loadven.Visible = False Then
            Me.cb_itemcode.Focus()
        ElseIf loadven.Visible = True Then
            Me.loadven.Focus()
        End If
    End Sub
    Private Sub cb_sledcode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_sledcode.SelectedIndexChanged
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        If Me.cb_sledcode.SelectedIndex <> -1 Then
            Me.tb_sledesc.Text = Me.cb_sledcode.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_sledesc.Text & "'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            If foundrow.Count > 0 Then
                If Not IsDBNull(foundrow(0).ItemArray(5)) Then
                    Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(3)
                    Me.tb_typecode.Text = foundrow(0).ItemArray(4)
                    Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(5)
                End If
            End If
            If foundrow.Count > 1 Then
                MsgBox("More number of records found for the supplier")
            Else
                For j = 0 To foundrow.Count - 1
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
            Dim olditemcode = Me.tb_itemdesc.Text
            Dim temppt = Me.tb_PRICETON.Text

            Dim temppr = Me.tb_prlist.Text
            'Me.tb_TOTALPRICE.Text = 0
            If Me.cb_itemcode.SelectedIndex <> -1 Then
                Me.tb_itemdesc.Text = Me.cb_itemcode.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "ITEMCODE = '" & Me.tb_itemdesc.Text & "' and " & "DIV_CODE = '" & glbvar.divcd & "'" & ""
                foundrow = dsitm.Tables("itm").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the item")
                    'Else
                    '    For k = 0 To foundrow.Count - 1
                    '        Me.Tb_intitemcode.Text = foundrow(0).Item("INTITEMCODE").ToString
                    '    Next
                End If


                If tb_sap_doc.Text = "QD" Or tb_sap_doc.Text = "QMX" Then
                    Me.tb_prlist.Text = 0
                    Me.tb_PRICETON.Text = 0
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    If tb_itemdesc.Text <> "" Then
                        Dim docdate
                        If Me.tb_DATEOUT.Text <> "" Then
                            Dim tdate = CDate(Me.tb_DATEOUT.Text).Day.ToString("D2")
                            Dim tmonth = CDate(Me.tb_DATEOUT.Text).Month.ToString("D2")
                            Dim tyear = CDate(Me.tb_DATEOUT.Text).Year
                            docdate = tyear & tmonth & tdate
                        ElseIf Me.tb_DATEIN.Text <> "" Then
                            Dim tdate = CDate(Me.tb_DATEIN.Text).Day.ToString("D2")
                            Dim tmonth = CDate(Me.tb_DATEIN.Text).Month.ToString("D2")
                            Dim tyear = CDate(Me.tb_DATEIN.Text).Year
                            docdate = tyear & tmonth & tdate
                        Else
                            Dim tdate = CDate(Today.Date).Day.ToString("D2")
                            Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                            Dim tyear = CDate(Today.Date).Year
                            docdate = tyear & tmonth & tdate
                        End If

                        'Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                                & "t.itemcode,t.itemdesc,t.UOM,nvl(MIN_PRICE,0)/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                                & " FROM   stitmratehd h, stitmrate t, smitem m" _
                                & " WHERE h.comp_code = t.comp_code" _
                                & " AND h.div_code = t.div_code" _
                                & " AND h.intrateno = t.intrateno" _
                                & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                                & " AND t.itemcode = " & "'" & tb_itemdesc.Text & "'" _
                                & " AND m.itemcode = t.itemcode" _
                                & " AND m.div_code = t.div_code" _
                                & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                                & " FROM   stitmratehd d where " _
                                & " to_number(to_char(d.witheffdt,'YYYYMMDD')) <= to_number(" & "'" & docdate & "')" _
                                & " AND d.div_code = " & "'" & glbvar.divcd & "'" _
                                & ")"
                        dpr = New OracleDataAdapter(sql, conn)
                        Dim dp As New DataSet
                        dp.Clear()
                        dpr.Fill(dp)
                        If dp.Tables(0).Rows.Count > 0 Then
                            Me.tb_prlist.Text = dp.Tables(0).Rows(0).Item("price")
                            If Me.tb_itemdesc.Text <> olditemcode Then
                                Me.tb_PRICETON.Text = 0
                            Else
                                Me.tb_PRICETON.Text = temppt
                            End If

                        ElseIf dp.Tables(0).Rows.Count = 0 Then
                            Me.tb_prlist.Text = temppr
                            Me.tb_PRICETON.Text = temppt
                        End If
                    End If
                ElseIf tb_sap_doc.Text = "ZTBV" Or tb_sap_doc.Text = "ZCWA" Then
                    Me.tb_prlist.Text = 0
                    Me.tb_PRICETON.Text = 0
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    If tb_itemdesc.Text <> "" Then
                        Dim docdate
                        If Me.tb_DATEOUT.Text <> "" Then
                            Dim tdate = CDate(Me.tb_DATEOUT.Text).Day.ToString("D2")
                            Dim tmonth = CDate(Me.tb_DATEOUT.Text).Month.ToString("D2")
                            Dim tyear = CDate(Me.tb_DATEOUT.Text).Year
                            docdate = tyear & tmonth & tdate
                        ElseIf Me.tb_DATEIN.Text <> "" Then
                            Dim tdate = CDate(Me.tb_DATEIN.Text).Day.ToString("D2")
                            Dim tmonth = CDate(Me.tb_DATEIN.Text).Month.ToString("D2")
                            Dim tyear = CDate(Me.tb_DATEIN.Text).Year
                            docdate = tyear & tmonth & tdate
                        Else
                            Dim tdate = CDate(Today.Date).Day.ToString("D2")
                            Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                            Dim tyear = CDate(Today.Date).Year
                            docdate = tyear & tmonth & tdate
                        End If

                        'Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                        sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                                & "t.itemcode,t.itemdesc,t.UOM,nvl(MIN_PRICE,0)/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                                & " FROM   stitmratehd h, stitmrate t, smitem m" _
                                & " WHERE h.comp_code = t.comp_code" _
                                & " AND h.div_code = t.div_code" _
                                & " AND h.intrateno = t.intrateno" _
                                & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                                & " AND t.itemcode = " & "'" & tb_itemdesc.Text & "'" _
                                & " AND m.itemcode = t.itemcode" _
                                & " AND m.div_code = t.div_code" _
                                & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                                & " FROM   stitmratehd d where " _
                                & " to_number(to_char(d.witheffdt,'YYYYMMDD')) <= to_number(" & "'" & docdate & "')" _
                                & " AND d.div_code = " & "'" & glbvar.divcd & "'" _
                                & ")"
                        dpr = New OracleDataAdapter(sql, conn)
                        Dim dp As New DataSet
                        dp.Clear()
                        dpr.Fill(dp)
                        If dp.Tables(0).Rows.Count > 0 Then
                            Me.tb_prlist.Text = dp.Tables(0).Rows(0).Item("price")
                            If Me.tb_itemdesc.Text <> olditemcode Then
                                Me.tb_PRICETON.Text = 0
                            Else
                                Me.tb_PRICETON.Text = temppt
                            End If

                        ElseIf dp.Tables(0).Rows.Count = 0 Then
                            Me.tb_prlist.Text = temppr
                            Me.tb_PRICETON.Text = temppt
                        End If
                    End If
                End If
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Private Sub cb_fritem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_fritem.SelectedIndexChanged
        Try
            If Me.cb_fritem.SelectedIndex <> -1 Then
                Me.tb_fritemdesc.Text = Me.cb_fritem.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "itmcde = '" & Me.tb_fritemdesc.Text & "' and " & "dvcode = '" & glbvar.divcd & "'" & ""
                foundrow = dsfitm.Tables("fitm").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the item")
                    'Else
                    '    For k = 0 To foundrow.Count - 1
                    '        Me.Tb_intitemcode.Text = foundrow(0).Item("INTITEMCODE").ToString
                    '    Next
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
                'Me.b_newveh.Focus()
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
                cmd.CommandText = "curspkg_join_pr.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd_pr.GEN_MATERIAL_RECEIPT_MULTI"
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
                        'cmdd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = Clng(Me.tb_ticketno.Text)
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
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CLng(Me.tb_ticketno.Text)
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
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = DBNull.Value
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
                        cmd.CommandText = "gen_iwb_dsd_pr.GEN_MATERIAL_RECEIPT"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CLng(Me.tb_ticketno.Text)
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
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = DBNull.Value
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
        clear_scr()
        comm.ClosePort()
        comm1.ClosePort()
        commett.ClosePort()
        commett1.ClosePort()
        commetty.ClosePort()
        commetty1.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()
    End Sub

    Private Sub b_print1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_print1.Click
        Try
            b_save_Click(sender, e)
            Dim apr = ""
            glbvar.vintdocno = Me.Tb_intdocno.Text
            glbvar.gdoccode = Me.tb_sap_doc.Text
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "select sprinted from stwbmibds_pr where ticketno = " & tb_ticketno.Text
            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            Try
                da.TableMappings.Add("Table", "prt")
                da.Fill(dstk)
                conn.Close()
                If Not (IsDBNull(dstk.Tables("prt").Rows(0).Item("sprinted"))) Then
                    apr = dstk.Tables("prt").Rows(0).Item("sprinted")
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
            If apr = "X" Then
                MsgBox("Ticket is printed already")
            Else
                If Me.cb_inouttype.Text = "T" Then
                    PRForm2.Show()
                    PRForm2.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set sprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                ElseIf Me.cb_inouttype.Text = "W" Then
                    PRForm2.Show()
                    PRForm2.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set sprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Else
                    PRForm2.Show()
                    PRForm2.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set sprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End If
            End If
            'glbvar.vintdocno = Me.Tb_intdocno.Text
            'glbvar.gdoccode = Me.tb_sap_doc.Text
            'If Me.cb_inouttype.Text = "T" Then
            'STFSTWT.Show()
            'STFSTWT.Close()
            'Else

            'End If


        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub b_print2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_print2.Click
        Try
            b_save_Click(sender, e)
            Dim apr = ""
            Dim supp = ""
            glbvar.vintdocno = CInt(Me.Tb_intdocno.Text)
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "select gprinted from stwbmibds_pr where ticketno = " & tb_ticketno.Text
            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            Dim dsdm As New DataSet
            Try
                da.TableMappings.Add("Table", "prt")
                da.Fill(dstk)
                conn.Close()
                If Not (IsDBNull(dstk.Tables("prt").Rows(0).Item("gprinted"))) Then
                    apr = dstk.Tables("prt").Rows(0).Item("gprinted")
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
            If apr = "X" Then
                MsgBox("Ticket is printed already")
            Else
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dsql = "select sledcode from stwbmibds_pr where ticketno = " & tb_ticketno.Text
                dadm = New OracleDataAdapter(dsql, conn)
                Try
                    dadm.TableMappings.Add("Table", "dm")
                    dadm.Fill(dsdm)
                    conn.Close()
                    If Not (IsDBNull(dsdm.Tables("dm").Rows(0).Item("sledcode"))) Then
                        supp = dsdm.Tables("dm").Rows(0).Item("sledcode")
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
                'If supp = "0000000000" Then
                '    MsgBox("Change the dummy account")
                'Else

                If Me.cb_inouttype.Text = "T" Then
                    PRForm4.Show()
                    PRForm4.Close()
                    'conn = New OracleConnection(constr)
                    'If conn.State = ConnectionState.Closed Then
                    '    conn.Open()
                    'End If
                    'Dim cmd As New OracleCommand
                    'cmd.Connection = conn
                    'cmd.CommandText = "update stwbmibds set printed = 'Y' where ticketno = " & Me.tb_ticketno.Text
                    'cmd.CommandType = CommandType.Text
                    'cmd.ExecuteNonQuery()
                    'conn.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set gprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                ElseIf Me.cb_inouttype.Text = "W" Then
                    PRForm4.Show()
                    PRForm4.Close()
                    'conn = New OracleConnection(constr)
                    'If conn.State = ConnectionState.Closed Then
                    '    conn.Open()
                    'End If
                    'Dim cmd As New OracleCommand
                    'cmd.Connection = conn
                    'cmd.CommandText = "update stwbmibds set printed = 'Y' where ticketno = " & Me.tb_ticketno.Text
                    'cmd.CommandType = CommandType.Text
                    'cmd.ExecuteNonQuery()
                    'conn.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set gprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Else
                    PRForm4.Show()
                    PRForm4.Close()
                    'conn = New OracleConnection(constr)
                    'If conn.State = ConnectionState.Closed Then
                    '    conn.Open()
                    'End If
                    'Dim cmd As New OracleCommand
                    'cmd.Connection = conn
                    'cmd.CommandText = "update stwbmibds set printed = 'Y' where ticketno = " & Me.tb_ticketno.Text
                    'cmd.CommandType = CommandType.Text
                    'cmd.ExecuteNonQuery()
                    'conn.Close()
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    ' Check if it has got multiple items.
                    cmd.Parameters.Clear()
                    cmd.CommandText = "update stwbmibds_pr set gprinted = 'X' where ticketno =" & tb_ticketno.Text
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End If
            End If
            'End If
            'glbvar.vintdocno = CInt(Me.Tb_intdocno.Text)
            'If tb_sledesc.Text = "0000000000" Then
            '    MsgBox("Change the dummy account")
            'Else
            '    If Me.cb_inouttype.Text = "T" Then
            '        'STSCNDWT.Show()
            '        'STSCNDWT.Close()
            '    Else
            '        PRForm4.Show()
            '        PRForm4.Close()
            '        'Second.Close()
            '        'conn = New OracleConnection(constr)
            '        'If conn.State = ConnectionState.Closed Then
            '        '    conn.Open()
            '        'End If
            '        'Dim cmd As New OracleCommand
            '        'cmd.Connection = conn
            '        'cmd.CommandText = "update stwbmibds_pr set printed = 'Y' where ticketno = " & Me.tb_ticketno.Text
            '        'cmd.CommandType = CommandType.Text
            '        'cmd.ExecuteNonQuery()
            '        'conn.Close()
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub b_printall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If tmode = 2 Then
                glbvar.vntwt = CInt(Me.tb_QTY.Text)
                glbvar.vfwt = CInt(Me.tb_FIRSTQTY.Text)
                glbvar.vswt = CInt(Me.tb_SECONDQTY.Text)
                glbvar.multdocno = Me.Tb_intdocno.Text
                glbvar.inout = Me.cb_inouttype.Text
                glbvar.multkt = Me.tb_ticketno.Text
                glbvar.sapdocmulti = Me.tb_sap_doc.Text
                glbvar.gsapordno = Me.tb_sapord.Text
                glbvar.gsapdocno = Me.tb_sapdocno.Text
                glbvar.gsapinvno = Me.tb_sapinvno.Text
                glbvar.gded = CInt(Me.tb_DEDUCTIONWT.Text)
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
        glbvar.itmalloc = False
        b_genis.Visible = False
        b_gends.Visible = False
        b_genst.Visible = False
        'b_mixmat.Visible = False
        Button1.Visible = False
        B_PO.Visible = False
        b_crfillup.Visible = False
        b_cribpur.Visible = False
        b_transfer.Visible = False
        tb_edittktn.Hide()
        b_edittktn.Hide()
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        b_firstwt2.Enabled = False
        b_secondwt2.Enabled = False
        Me.cb_inouttype.Text = ""
        If Me.tb_ticketno.Text <> "" AndAlso Me.Tb_intdocno.Text <> "" Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.delete_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.ExecuteNonQuery()
        End If
        conn.Close()
        Me.tb_ticketno.Text = "0"
        'Me.tb_container.Text = ""
        Me.tb_vehicleno.Text = ""
        'Me.tb_transporter.Text = ""
        Me.tb_sledesc.Text = ""
        Me.tb_prjsledesc.Text = ""
        Me.tb_itemdesc.Text = ""
        Me.tb_fritemdesc.Text = ""
        'Me.tb_operatorid.Text = ""
        Me.tb_numberofpcs.Text = 0
        Me.tb_DRIVERNAM.Text = ""
        'Me.tb_NATIONALITY.Text = ""
        'Me.tb_DRIVINGLICNO.Text = ""
        Me.tb_FIRSTQTY.Text = ""
        Me.tb_DATEIN.Text = ""
        Me.tb_ticketno.Text = "0"
        Me.tb_SECONDQTY.Text = 0
        Me.tb_DATEOUT.Text = ""
        Me.tb_TIMOUT.Text = ""
        Me.tb_DEDUCTIONWT.Text = 0
        Me.tb_packded.Text = 0
        Me.tb_ded.Text = 0
        Me.tb_QTY.Text = 0
        Me.tb_PRICETON.Text = 0
        Me.tb_TOTALPRICE.Text = 0
        Me.tb_prlist.Text = 0
        Me.tb_comments.Text = ""
        Me.tb_dsno.Text = ""
        Me.tb_orderno.Text = ""
        Me.Tb_asno.Text = ""
        Me.Tb_cons_sen_branch.Text = ""
        Me.tb_IBDSNO.Text = ""
        Me.Tb_accountcode.Text = ""
        Me.Tb_intitemcode.Text = ""
        Me.tb_prjaccountcode.Text = ""
        Me.tb_frintitem.Text = ""
        Me.Tb_intdocno.Text = ""
        Me.tb_INTIBDSNO.Text = ""
        Me.tb_STATUS.Text = ""
        Me.Tb_transp.Text = 0
        Me.Tb_penalty.Text = 0
        Me.Tb_eqpchrgs.Text = 0
        Me.Tb_labourcharges.Text = 0
        Me.tb_omcustcode.Text = ""
        Me.tb_omcustprice.Text = 0
        Me.cb_omcustdesc.Text = ""
        Me.tb_sapord.Text = ""
        Me.tb_sapdocno.Text = ""
        Me.tb_sapinvno.Text = ""
        'Me.tb_oth_ven_cust.Text = ""
        Me.tb_itmno.Text = ""
        Me.cb_omcustdesc.Visible = False
        Me.tb_omcustcode.Visible = False
        Me.tb_omcustprice.Visible = False
        Me.Tb_custktdt.Visible = False
        Me.Tb_cust_ticket_no.Visible = False
        Me.Label47.Visible = False
        Me.Label46.Visible = False
        Me.Label41.Visible = False
        'Me.cb_ib.Checked = False
        'Me.cb_ib.Visible = False
        Me.tb_ticketno.Enabled = False
        Me.cb_dcode.Text = ""
        Me.l_dsno.Visible = False
        Me.l_so.Visible = False
        Me.l_agmix.Visible = False
        'Me.l_pono.Visible = False
        Me.l_cons.Visible = False
        'Me.Tb_asno.Visible = False
        Me.tb_IBDSNO.Visible = False
        Me.tb_orderno.Visible = False
        Me.tb_dsno.Visible = False
        Me.Tb_cons_sen_branch.Visible = False
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        'Me.cb_multi.Checked = False
        Me.d_newdate.Enabled = True
        'Me.Label25.Visible = False
        'Me.rtb_gprem.Visible = False
        'Me.rtb_gprem.Text = ""
        'Me.b_gp.Visible = False
        Me.d_newdate.Text = Today
        Me.cb_sledcode.Text = "Dummy Supplier"
        Me.tb_sledesc.Text = "0000000000"
        Me.cb_prjsledcode.Text = "Dummy Supplier"
        Me.tb_prjsledesc.Text = "0000000000"
        Me.cb_itemcode.Text = "SCRAP"
        Me.cb_fritem.Text = "SCRAP"
        Me.tb_itemdesc.Text = "000000000000000000"
        Me.tb_fritemdesc.Text = "000000000000000000"
        Me.Tb_intitemcode.Text = 141325
        Me.tb_frintitem.Text = 141325
        'Me.rtbDisplay.Text = ""
        'Me.rtbDisplay2.Text = ""
        'Me.tb_searchbyno.Text = ""
        Me.DataGridView1.Rows.Clear()
    End Sub
    Private Sub clear_scr_new()
        b_genis.Visible = False
        b_gends.Visible = False
        b_genst.Visible = False
        Button1.Visible = False
        B_PO.Visible = False
        tb_edittktn.Hide()
        b_edittktn.Hide()
        'b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        'b_firstwt2.Enabled = False
        b_secondwt2.Enabled = False
        Me.cb_inouttype.Text = ""
        If Me.tb_ticketno.Text <> "" Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.insert_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
            cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
            cmd1.ExecuteNonQuery()
            conn.Close()
        End If
        Me.tb_ticketno.Text = "0"
        'Me.tb_container.Text = ""
        Me.tb_vehicleno.Text = ""
        'Me.tb_transporter.Text = ""
        Me.tb_sledesc.Text = ""
        Me.tb_itemdesc.Text = ""
        'Me.tb_operatorid.Text = ""
        Me.tb_numberofpcs.Text = 0
        Me.tb_DRIVERNAM.Text = ""
        'Me.tb_NATIONALITY.Text = ""
        'Me.tb_DRIVINGLICNO.Text = ""
        Me.tb_FIRSTQTY.Text = ""
        Me.tb_DATEIN.Text = ""
        Me.tb_ticketno.Text = "0"
        Me.tb_SECONDQTY.Text = 0
        Me.tb_DATEOUT.Text = ""
        Me.tb_TIMOUT.Text = ""
        Me.tb_DEDUCTIONWT.Text = 0
        Me.tb_packded.Text = 0
        Me.tb_ded.Text = 0
        Me.tb_QTY.Text = 0
        Me.tb_PRICETON.Text = 0
        Me.tb_TOTALPRICE.Text = 0
        Me.tb_prlist.Text = 0
        Me.tb_comments.Text = ""
        Me.tb_dsno.Text = ""
        Me.tb_orderno.Text = ""
        Me.Tb_asno.Text = ""
        Me.Tb_cons_sen_branch.Text = ""
        Me.tb_IBDSNO.Text = ""
        Me.Tb_accountcode.Text = ""
        Me.Tb_intitemcode.Text = ""
        Me.Tb_intdocno.Text = ""
        Me.tb_INTIBDSNO.Text = ""
        Me.tb_STATUS.Text = ""
        Me.Tb_transp.Text = 0
        Me.Tb_penalty.Text = 0
        Me.Tb_eqpchrgs.Text = 0
        Me.Tb_labourcharges.Text = 0
        Me.tb_omcustcode.Text = ""
        Me.tb_omcustprice.Text = 0
        Me.cb_omcustdesc.Text = ""
        Me.tb_sapord.Text = ""
        Me.tb_sapdocno.Text = ""
        Me.tb_sapinvno.Text = ""
        'Me.tb_oth_ven_cust.Text = ""
        Me.tb_itmno.Text = ""
        Me.cb_omcustdesc.Visible = False
        Me.tb_omcustcode.Visible = False
        Me.tb_omcustprice.Visible = False
        Me.Tb_custktdt.Visible = False
        Me.Tb_cust_ticket_no.Visible = False
        'Me.Label38.Visible = False
        Me.Label47.Visible = False
        Me.Label46.Visible = False
        Me.Label41.Visible = False
        'Me.cb_ib.Checked = False
        Me.tb_ticketno.Enabled = False
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        'Me.cb_multi.Enabled = True
        'Me.Label25.Visible = False
        'Me.rtb_gprem.Visible = False
        'Me.b_gp.Visible = False
        Me.d_newdate.Text = Today
        Me.DataGridView1.Rows.Clear()
    End Sub
    Private Sub freeze_scr()
        glbvar.itmalloc = False
        b_genis.Enabled = False
        b_gends.Enabled = False
        b_genst.Enabled = False
        Button1.Visible = False
        'b_multi.Enabled = False
        B_PO.Visible = False
        b_transfer.Visible = False
        tb_edittktn.Enabled = False
        b_edittktn.Enabled = False
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        Me.cb_inouttype.Enabled = False
        Me.tb_ticketno.Enabled = False
        'Me.tb_container.Enabled = False
        Me.tb_vehicleno.Enabled = False
        'Me.tb_transporter.Enabled = False
        Me.tb_sledesc.Enabled = False
        Me.tb_itemdesc.Enabled = False
        Me.tb_prjsledesc.Enabled = False
        Me.tb_fritemdesc.Enabled = False
        'Me.tb_operatorid.Enabled = False
        Me.tb_numberofpcs.Enabled = False
        Me.tb_DRIVERNAM.Enabled = False
        'Me.tb_NATIONALITY.Enabled = False
        'Me.tb_DRIVINGLICNO.Enabled = False
        Me.tb_FIRSTQTY.Enabled = False
        Me.tb_DATEIN.Enabled = False
        'Me.tb_ticketno.Enabled = False
        Me.tb_SECONDQTY.Enabled = False
        Me.tb_DATEOUT.Enabled = False
        Me.tb_TIMOUT.Enabled = False
        Me.tb_DEDUCTIONWT.Enabled = False
        Me.tb_packded.Enabled = False
        Me.tb_ded.Enabled = False
        Me.tb_QTY.Enabled = False
        Me.tb_PRICETON.Enabled = False
        Me.tb_TOTALPRICE.Enabled = False
        Me.tb_prlist.Enabled = False
        Me.tb_comments.Enabled = False
        Me.tb_dsno.Enabled = False
        Me.tb_orderno.Enabled = False
        Me.Tb_asno.Enabled = False
        Me.Tb_cons_sen_branch.Enabled = False
        Me.tb_IBDSNO.Enabled = False
        Me.Tb_accountcode.Enabled = False
        Me.Tb_intitemcode.Enabled = False
        Me.Tb_intdocno.Enabled = False
        Me.tb_INTIBDSNO.Enabled = False
        Me.tb_STATUS.Enabled = False
        Me.Tb_transp.Enabled = False
        Me.Tb_penalty.Enabled = False
        Me.Tb_eqpchrgs.Enabled = False
        Me.Tb_labourcharges.Enabled = False
        Me.tb_omcustcode.Enabled = False
        Me.tb_omcustprice.Enabled = False
        Me.cb_omcustdesc.Enabled = False
        'Me.tb_sapord.Enabled = False
        'Me.tb_sapdocno.Enabled = False
        'Me.tb_sapinvno.Enabled = False
        Me.cb_inouttype.Enabled = False
        Me.cb_itemcode.Enabled = False
        Me.cb_fritem.Enabled = False
        Me.cb_omcustdesc.Enabled = False
        Me.cb_sap_docu_type.Enabled = False
        Me.cb_sledcode.Enabled = False
        Me.cb_prjsledcode.Enabled = False
        'Me.tb_oth_ven_cust.Enabled = False
        Me.tb_itmno.Enabled = False
        Me.tb_DRIVERNAM.Enabled = False
        Me.cb_dcode.Enabled = False
        'Me.Cb_buyname.Enabled = False
        'Me.Tb_buydesc.Enabled = False
        Me.Tb_ccic.Enabled = False
        'Me.DataGridView1.Enabled = False
        'Me.cb_ib.Enabled = False
        Me.tb_sap_doc.Enabled = False
        'Me.cb_multi.Enabled = False
        Me.tb_searchbyno.Enabled = False
        Me.tb_prjsrchbyno.Enabled = False
        Me.d_newdate.Enabled = False
    End Sub
    Private Sub unfreeze_scr()
        glbvar.itmalloc = False
        b_genis.Enabled = False
        b_gends.Enabled = False
        b_genst.Enabled = False
        'Button1.Enabled = True
        'b_multi.Enabled = True
        'B_PO.Enabled = True
        tb_edittktn.Enabled = True
        b_edittktn.Enabled = True
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        b_firstwt2.Enabled = False
        b_secondwt2.Enabled = False
        Me.cb_inouttype.Enabled = False
        'Me.tb_ticketno.Enabled = True
        'Me.tb_container.Enabled = True
        Me.tb_vehicleno.Enabled = True
        'Me.tb_transporter.Enabled = True
        Me.tb_sledesc.Enabled = True
        Me.tb_itemdesc.Enabled = True
        Me.tb_prjsledesc.Enabled = True
        Me.tb_fritemdesc.Enabled = True
        'Me.tb_operatorid.Enabled = True
        Me.tb_numberofpcs.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        'Me.tb_NATIONALITY.Enabled = True
        'Me.tb_DRIVINGLICNO.Enabled = True
        Me.tb_FIRSTQTY.Enabled = False
        Me.tb_DATEIN.Enabled = False
        Me.tb_ticketno.Enabled = True
        Me.tb_SECONDQTY.Enabled = False
        Me.tb_DATEOUT.Enabled = False
        Me.tb_TIMOUT.Enabled = False
        Me.tb_DEDUCTIONWT.Enabled = False
        Me.tb_packded.Enabled = True
        Me.tb_ded.Enabled = True
        Me.tb_QTY.Enabled = False
        Me.tb_PRICETON.Enabled = True
        Me.tb_TOTALPRICE.Enabled = False
        Me.tb_prlist.Enabled = False
        Me.tb_comments.Enabled = True
        Me.tb_dsno.Enabled = True
        Me.tb_orderno.Enabled = True
        Me.Tb_asno.Enabled = True
        Me.Tb_cons_sen_branch.Enabled = True
        Me.tb_IBDSNO.Enabled = True
        Me.Tb_accountcode.Enabled = True
        Me.Tb_intitemcode.Enabled = True
        Me.Tb_intdocno.Enabled = True
        Me.tb_INTIBDSNO.Enabled = True
        Me.tb_STATUS.Enabled = True
        Me.Tb_transp.Enabled = True
        Me.Tb_penalty.Enabled = True
        Me.Tb_eqpchrgs.Enabled = True
        Me.Tb_labourcharges.Enabled = True
        Me.tb_omcustcode.Enabled = True
        Me.tb_omcustprice.Enabled = True
        Me.cb_omcustdesc.Enabled = True
        'Me.tb_sapord.Enabled = True
        'Me.tb_sapdocno.Enabled = True
        'Me.tb_sapinvno.Enabled = True
        Me.cb_inouttype.Enabled = False
        Me.cb_itemcode.Enabled = True
        Me.cb_fritem.Enabled = True
        Me.cb_omcustdesc.Enabled = True
        Me.cb_sap_docu_type.Enabled = True
        Me.cb_sledcode.Enabled = True
        Me.cb_prjsledcode.Enabled = True
        'Me.tb_oth_ven_cust.Enabled = True
        Me.tb_itmno.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        Me.cb_dcode.Enabled = True
        'Me.Cb_buyname.Enabled = True
        'Me.Tb_buydesc.Enabled = True
        Me.Tb_ccic.Enabled = True
        'Me.DataGridView1.Enabled = True
        Me.tb_sap_doc.Enabled = True
        'Me.cb_ib.Enabled = True
        'Me.cb_multi.Enabled = True
        Me.tb_searchbyno.Enabled = True
        Me.tb_prjsrchbyno.Enabled = True
        Me.d_newdate.Enabled = True
        'Me.Label25.Visible = False
        'Me.rtb_gprem.Visible = False
        'Me.b_gp.Visible = False
        Me.d_newdate.Text = Today
    End Sub
    Private Sub unfreeze_scr_new()
        b_genis.Enabled = False
        b_gends.Enabled = False
        b_genst.Enabled = False
        'Button1.Enabled = True
        'b_multi.Enabled = True
        'B_PO.Enabled = True
        tb_edittktn.Enabled = True
        b_edittktn.Enabled = True
        'b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        'b_firstwt2.Enabled = False
        b_secondwt2.Enabled = False
        Me.cb_inouttype.Enabled = False
        Me.tb_ticketno.Enabled = True
        'Me.tb_container.Enabled = True
        Me.tb_vehicleno.Enabled = True
        'Me.tb_transporter.Enabled = True
        Me.tb_sledesc.Enabled = True
        Me.tb_itemdesc.Enabled = True
        'Me.tb_operatorid.Enabled = True
        Me.tb_numberofpcs.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        'Me.tb_NATIONALITY.Enabled = True
        'Me.tb_DRIVINGLICNO.Enabled = True
        Me.tb_FIRSTQTY.Enabled = False
        Me.tb_DATEIN.Enabled = False
        Me.tb_ticketno.Enabled = True
        Me.tb_SECONDQTY.Enabled = False
        Me.tb_DATEOUT.Enabled = False
        Me.tb_TIMOUT.Enabled = False
        Me.tb_DEDUCTIONWT.Enabled = False
        Me.tb_packded.Enabled = True
        Me.tb_ded.Enabled = True
        Me.tb_QTY.Enabled = False
        Me.tb_PRICETON.Enabled = True
        Me.tb_TOTALPRICE.Enabled = False
        Me.tb_prlist.Enabled = False
        Me.tb_comments.Enabled = True
        Me.tb_dsno.Enabled = True
        Me.tb_orderno.Enabled = True
        Me.Tb_asno.Enabled = True
        Me.Tb_cons_sen_branch.Enabled = True
        Me.tb_IBDSNO.Enabled = True
        Me.Tb_accountcode.Enabled = True
        Me.Tb_intitemcode.Enabled = True
        Me.Tb_intdocno.Enabled = True
        Me.tb_INTIBDSNO.Enabled = True
        Me.tb_STATUS.Enabled = True
        Me.Tb_transp.Enabled = True
        Me.Tb_penalty.Enabled = True
        Me.Tb_eqpchrgs.Enabled = True
        Me.Tb_labourcharges.Enabled = True
        Me.tb_omcustcode.Enabled = True
        Me.tb_omcustprice.Enabled = True
        Me.cb_omcustdesc.Enabled = True
        'Me.tb_sapord.Enabled = True
        'Me.tb_sapdocno.Enabled = True
        'Me.tb_sapinvno.Enabled = True
        Me.cb_inouttype.Enabled = False
        Me.cb_itemcode.Enabled = True
        Me.cb_omcustdesc.Enabled = True
        Me.cb_sap_docu_type.Enabled = True
        Me.cb_sledcode.Enabled = True
        'Me.tb_oth_ven_cust.Enabled = True
        Me.tb_itmno.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        Me.cb_dcode.Enabled = True
        'Me.Cb_buyname.Enabled = True
        'Me.Tb_buydesc.Enabled = True
        Me.Tb_ccic.Enabled = True
        'Me.DataGridView1.Enabled = True
        Me.tb_sap_doc.Enabled = True
        'Me.cb_multi.Enabled = True
        Me.tb_searchbyno.Enabled = True
        'Me.Label25.Visible = False
        'Me.rtb_gprem.Visible = False
        'Me.b_gp.Visible = False
        Me.d_newdate.Text = Today
    End Sub

    Private Sub b_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_clear.Click
        Try
            unfreeze_scr()
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
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.SelectedValue = "O" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        If Me.tb_sap_doc.Text <> "ZTRE" Then
                            Me.tb_QTY.Text = Math.Abs(CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                        ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                            Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "S" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "T" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "W" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        ElseIf tmode = 2 Then
            If cb_inouttype.Text = "I" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "O" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        If Me.tb_sap_doc.Text <> "ZTRE" Then
                            Me.tb_QTY.Text = Math.Abs(CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                        ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                            Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                        End If
                        'Me.tb_QTY.Text = tq - CDec(Me.tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "S" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "T" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf cb_inouttype.Text = "W" Then
                If tb_QTY.Text <> Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        Me.tb_QTY.Text = Math.Abs(CDec(tb_FIRSTQTY.Text) - CDec(tb_SECONDQTY.Text)) - CDec(tb_DEDUCTIONWT.Text)
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
                'Me.b_newveh.Focus()
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
                cmd.CommandText = "curspkg_join_pr.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
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
                        cmd.CommandText = "gen_iwb_dsd_pr.GEN_Delivery_note_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CLng(Me.tb_ticketno.Text)
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
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = DBNull.Value
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
                        cmd.CommandText = "gen_iwb_dsd_pr.GEN_Delivery_note"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                        cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                        cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                        cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CLng(Me.tb_ticketno.Text)
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
                        cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = DBNull.Value
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
                'Me.b_newveh.Focus()
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
                cmd.CommandText = "gen_iwb_dsd_pr.GEN_STOCK_TRANSFER"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
                cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
                cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = glbvar.vyrcd
                cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = Today
                cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CLng(Me.tb_ticketno.Text)
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
                cmd.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value = DBNull.Value
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

    Private Sub b_scaleonly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'S' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
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
            l_Project.Text = "Supplier"
            l_tomat.Text = "Product"
            cmbloading()
            Me.tb_docprint.Text = "SCALE ONLY/STOCK TRANSFER"
            Me.tb_sap_doc.Text = "SC"
            Me.cb_sap_docu_type.Text = "Scale Only"
            Me.tb_sap_doc.Enabled = False
            Me.cb_sap_docu_type.Enabled = False
            Me.cb_sledcode.Text = "Dummy Supplier"
            Me.tb_sledesc.Text = "0000000000"
            Me.tb_itemdesc.Text = "000000000000000000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            b_firstwt2.Enabled = True
            Me.b_secondwt2.Enabled = False
            cb_inouttype.SelectedValue = "S"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub b_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_delete"
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
    Private Sub b_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CLng(Me.tb_ticketno.Text) <> 0 Then


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
            & "  FROM   stwbmibds_pr WBM" _
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
        cmd.CommandText = "curspkg_join_pr.tktrng"
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
        ElseIf cb_inouttype.SelectedValue = "W" Then
            cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCO"
        End If
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            Dim dsrng As New DataSet
            Dim darng As New OracleDataAdapter(cmd)
            darng.TableMappings.Add("Table", "tktrng")
            darng.Fill(dsrng)
            If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                Me.cb_fritem.Focus()
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
            cmd1.CommandText = "gen_iwb_dsd_pr.gen_wbms_edittkt"
            cmd1.CommandType = CommandType.StoredProcedure
            Try
                cmd1.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.Text
                cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd1.Parameters.Add(New OracleParameter("pNEWTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_edittktn.Text)
                cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                cmd1.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = DBNull.Value
                'If IsDBNull(Me.tb_container.Text) Then
                'Me.tb_container.Text = ""
                'Else
                '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
                'End If
                cmd1.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = DBNull.Value
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
                cmd1.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = DBNull.Value
                cmd1.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = DBNull.Value
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
                Me.b_firstwt2.Enabled = False
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
                & "  FROM   stwbmibds_pr WBM" _
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

            sql = "SELECT   itemcode" _
                & "  FROM   stwbmibds_pr WBM" _
                & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text

            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            da.Fill(dstk)
            If dstk.Tables(0).Rows.Count > 0 Then
                Me.tb_intit.Text = dstk.Tables(0).Rows(0).Item("itemcode")
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

    Private Sub tb_transporter_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        If ymode = 0 Then
            cb_sledcode.Focus()
        End If


    End Sub


    Private Sub b_vehino_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_vehino.Click
        'clear_scr()

        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT nvl(ticketno,0) ticketno FROM WBMLOCK WHERE VEHICLENO = '" & Me.tb_sveh.Text & "'"
        Dim dalk = New OracleDataAdapter(sql, conn)
        Dim dslk As New DataSet
        dalk.Fill(dslk)
        conn.Close()
        If dslk.Tables(0).Rows.Count > 0 Then
            MsgBox("Transaction Open in another screen")
            'Me.tb_ticketno.Text = 0
            Me.tb_ticketno.Focus()
        Else
            Try
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                'sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                '         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                '         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                '         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                '         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC" _
                '         & " from stwbmibds_pr where VEHICLENO = '" & Me.tb_sveh.Text & "'" _
                '         & " and status in (1,2,3) and wtstat = 'I'"
                sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC,PRACCOUNTCODE, PRSLEDCODE, PRSLEDDESC," _
                         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,CFCREATED,MIXTRFTKT,IBTKTNO,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT,custtype,typecode,typecatg_pt,post_date,gpremarks,sprinted,gprinted" _
                         & " from stwbmibds_pr where inouttype = 'T' and VEHICLENO = '" & Me.tb_sveh.Text & "'" _
                         & " and status in (1,2,3) and wtstat = 'I'"

                clear_scr()
                da = New OracleDataAdapter(sql, conn)
                'da.TableMappings.Add("Table", "mlt")
                Dim ds As New DataSet
                da.Fill(ds)
                conn.Close()
                If ds.Tables(0).Rows.Count > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then

                    Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                    Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                    Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                    Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                    '    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                    'End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                    '    Me.tb_transporter.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                    'End If
                    If Me.cb_inouttype.Text = "T" Then
                        'cb_sledcode.Hide()
                        'tb_sledesc.Hide()
                        cb_fritem.Show()
                        tb_fritemdesc.Show()
                        'l_Project.Text = "From Item"
                        'l_tomat.Text = "To Item"
                        Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                        Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                        Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                    ElseIf Me.cb_inouttype.Text = "I" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Customer"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "S" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "W" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("ACCOUNTCODE"))) Then
                        Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                    End If
                    Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")
                    Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRACCOUNTCODE"))) Then
                        Me.tb_prjaccountcode.Text = ds.Tables(0).Rows(0).Item("PRACCOUNTCODE")
                    End If
                    Me.cb_prjsledcode.Text = ds.Tables(0).Rows(0).Item("PRSLEDDESC")
                    Me.tb_prjsledesc.Text = ds.Tables(0).Rows(0).Item("PRSLEDCODE")
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTITEMCODE"))) Then
                        Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                    End If
                    Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                    Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                        Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                        Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")
                        Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                    End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                    '    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                    'End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                    '    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                    'End If
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("POST_DATE"))) Then
                        Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("POST_DATE")
                    Else
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                            Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                        End If
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("RATE"))) Then
                        Me.tb_prlist.Text = ds.Tables(0).Rows(0).Item("RATE")
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
                    If cb_inouttype.Text = "I" Then
                        Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("BSART")
                        Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("BSART")
                    ElseIf cb_inouttype.Text = "O" Then
                        Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("AUART")
                        Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("AUART")
                        'Me.Label25.Visible = True
                        'Me.rtb_gprem.Visible = True
                        'Me.b_gp.Visible = True
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPREMARKS"))) Then
                        '    Me.rtb_gprem.Text = ds.Tables(0).Rows(0).Item("GPREMARKS")
                        'End If
                    ElseIf cb_inouttype.Text = "S" Then
                        'Me.Label25.Visible = True
                        'Me.rtb_gprem.Visible = True
                        'Me.b_gp.Visible = True
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPREMARKS"))) Then
                        '    Me.rtb_gprem.Text = ds.Tables(0).Rows(0).Item("GPREMARKS")
                        'End If
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SORDERNO"))) Then
                        Me.tb_orderno.Text = ds.Tables(0).Rows(0).Item("SORDERNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DELIVERYNO"))) Then
                        Me.tb_dsno.Text = ds.Tables(0).Rows(0).Item("DELIVERYNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PONO"))) Then
                        Me.Tb_asno.Text = ds.Tables(0).Rows(0).Item("PONO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("AGMIXNO"))) Then
                        Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("AGMIXNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONSNO"))) Then
                        Me.Tb_cons_sen_branch.Text = ds.Tables(0).Rows(0).Item("CONSNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_CHARGE"))) Then
                        Me.Tb_transp.Text = ds.Tables(0).Rows(0).Item("TRANS_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PENALTY"))) Then
                        Me.Tb_penalty.Text = ds.Tables(0).Rows(0).Item("PENALTY")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MACHINE_CHARGE"))) Then
                        Me.Tb_eqpchrgs.Text = ds.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("LABOUR_CHARGE"))) Then
                        Me.Tb_labourcharges.Text = ds.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CCIC"))) Then
                        Me.Tb_ccic.Text = ds.Tables(0).Rows(0).Item("CCIC")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDCODE"))) Then
                        Me.tb_omcustcode.Text = ds.Tables(0).Rows(0).Item("OMSLEDCODE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDDESC"))) Then
                        Me.cb_omcustdesc.Text = ds.Tables(0).Rows(0).Item("OMSLEDDESC")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                        Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMPRICE"))) Then
                        Me.tb_omcustprice.Text = ds.Tables(0).Rows(0).Item("OMPRICE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNS"))) Then
                        Me.tb_sapord.Text = ds.Tables(0).Rows(0).Item("VBELNS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELND"))) Then
                        Me.tb_sapdocno.Text = ds.Tables(0).Rows(0).Item("VBELND")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNI"))) Then
                        Me.tb_sapinvno.Text = ds.Tables(0).Rows(0).Item("VBELNI")
                    End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                    '    Me.cb_ib.Checked = True
                    'ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                    '    Me.cb_ib.Checked = False
                    'End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                        Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                        Me.tb_CUSTTYPE.Text = ds.Tables(0).Rows(0).Item("CUSTTYPE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECODE"))) Then
                        Me.tb_typecode.Text = ds.Tables(0).Rows(0).Item("TYPECODE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                        Me.tb_typecatg_pt.Text = ds.Tables(0).Rows(0).Item("TYPECATG_PT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SPRINTED"))) Then
                        Me.tb_sprinted.Text = ds.Tables(0).Rows(0).Item("SPRINTED")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPRINTED"))) Then
                        Me.tb_gprinted.Text = ds.Tables(0).Rows(0).Item("GPRINTED")
                    End If
                    'update data table in case of multiple items.
                    Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                    & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                    & " from(stwbmibds_pr_MULTI)" _
                    & " where(INTDOCNO =" & Me.Tb_intdocno.Text & ")"
                    Dim da1 As New OracleDataAdapter(sql, conn)
                    da1.Fill(ds1)
                    'If Me.tb_IBDSNO.Text = "" Then
                    If Me.cb_inouttype.Text = "I" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = False
                        Me.Button1.Visible = False
                        ' Me.B_PO.Visible = True
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        'Me.Button1.Visible = True
                        Me.b_genst.Visible = False
                        Me.B_PO.Visible = False
                    ElseIf Me.cb_inouttype.Text = "T" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.Button1.Visible = False
                        Me.b_genst.Visible = False
                        Me.b_transfer.Visible = False
                    End If
                    'Else
                    '    Me.b_gends.Visible = False
                    '    Me.b_genis.Visible = False
                    '    Me.b_genst.Visible = False
                    'Me.Button1.Visible = False
                    'Me.B_PO.Visible = False
                    'End If
                    Me.b_firstwt.Enabled = False
                    Me.b_firstwt2.Enabled = False
                    If Me.tb_SECONDQTY.Text = 0 Then
                        Me.b_secondwt.Enabled = True
                        Me.b_secondwt2.Enabled = True
                        tmode = 2
                    End If
                    If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
                        'Me.B_PO.Visible = False
                        'Me.Button1.Visible = False
                        freeze_scr()
                    End If
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd1 As New OracleCommand
                    cmd1.Connection = conn
                    cmd1.Parameters.Clear()
                    cmd1.CommandText = "curspkg_join_pr.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                    cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                    cmd1.ExecuteNonQuery()
                    conn.Close()


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

                Else
                    MsgBox("No Records Found for this vehicle #", MsgBoxStyle.Information)
                    'Me.tb_ticketno.Focus()
                End If
                Me.tb_sveh.Text = "0"


                If cb_inouttype.Text = "I" Then
                    glbvar.temp_suppcode = Me.tb_sledesc.Text
                    glbvar.temp_suppdesc = Me.cb_sledcode.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    sl_item_driv_load()
                ElseIf cb_inouttype.Text = "O" Then
                    glbvar.temp_suppcode = Me.tb_sledesc.Text
                    glbvar.temp_suppdesc = Me.cb_sledcode.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    cust_item_driv_load()
                ElseIf cb_inouttype.Text = "T" Then
                    glbvar.temp_suppcode = Me.tb_sledesc.Text
                    glbvar.temp_suppdesc = Me.cb_sledcode.Text
                    glbvar.temp_prsuppcode = Me.tb_prjsledesc.Text
                    glbvar.temp_prsuppdesc = Me.cb_prjsledcode.Text
                    glbvar.temp_fritemcode = Me.cb_fritem.Text
                    glbvar.temp_fritemdesc = Me.tb_fritemdesc.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    tran_item_driv_load()
                End If
                If Me.tb_sap_doc.Text = "QN" Then
                    Me.Tb_asno.Visible = True
                    Me.l_pono.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QI" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIB" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    'Me.b_mixmat.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIX" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QO" Then
                    Me.cb_omcustdesc.Enabled = True
                    Me.tb_omcustcode.Enabled = True
                    Me.tb_omcustprice.Enabled = True
                    Me.Tb_custktdt.Visible = True
                    Me.Label46.Enabled = True
                    Me.Label47.Enabled = True
                    Me.Label41.Visible = True
                    Me.cb_omcustdesc.Visible = True
                    Me.tb_omcustcode.Visible = True
                    Me.tb_omcustprice.Visible = True
                    Me.Tb_cust_ticket_no.Visible = True
                    'Me.Label38.Visible = True
                    Me.Label46.Visible = True
                    Me.Label47.Visible = True
                    'Me.tb_IBDSNO.Visible = True
                    'ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    '   Me.tb_IBDSNO.Visible = True
                ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                    Me.tb_orderno.Visible = True
                    Me.tb_dsno.Visible = True
                    Me.l_dsno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "SO #"
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "RO #"
                ElseIf Me.tb_sap_doc.Text = "ZCWR" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "Billing #"
                Else
                    'Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    'Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    'l_pono.Visible = False
                    l_so.Visible = False
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
            'ElseIf tmode = 0 Then
            'Else
            'MsgBox("Please select New or edit or cancel")
        End If 'lock
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
            & "  FROM   stwbmibds_pr WBM" _
            & " WHERE WBM.VEHICLENO = '" & Me.tb_vehicleno.Text & "'" _
            & " and status in (1,2) " _
            & " and wtstat = 'T'"

        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try

            da.Fill(dstk)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Try
            If dstk.Tables(0).Rows.Count > 0 And b_secondwt.Enabled = False And b_secondwt2.Enabled = False Then
                ymode = 1
                MsgBox("Vehicle In - Take the second weight")
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
        'clear_scr()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT nvl(ticketno,0) ticketno FROM WBMLOCK WHERE intdocno = " & Me.tb_trans.Text
        Dim dalk = New OracleDataAdapter(sql, conn)
        Dim dslk As New DataSet
        dalk.Fill(dslk)
        conn.Close()
        If dslk.Tables(0).Rows.Count > 0 Then
            MsgBox("Transaction Open in another screen")
            'Me.tb_ticketno.Text = 0
            Me.tb_ticketno.Focus()
        Else
            Try
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                'sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                '         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                '         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                '         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                '         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC" _
                '         & " from stwbmibds_pr where INTDOCNO = '" & Me.tb_trans.Text & "'" _
                '         & " and status in (1,2,3)"

                sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT,custtype,typecode,typecatg_pt,post_date,gpremarks" _
                         & " from stwbmibds_pr where INTDOCNO = '" & Me.tb_trans.Text & "'" _
                         & " and status in (1,2,3)"
                clear_scr()
                da = New OracleDataAdapter(sql, conn)
                'da.TableMappings.Add("Table", "mlt")
                Dim ds As New DataSet
                da.Fill(ds)
                conn.Close()
                If ds.Tables(0).Rows.Count > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                    'If CInt(ds.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then

                    Me.Tb_intdocno.Text = ds.Tables(0).Rows(0).Item("INTDOCNO")
                    Me.cb_inouttype.Text = ds.Tables(0).Rows(0).Item("INOUTTYPE")
                    Me.tb_ticketno.Text = ds.Tables(0).Rows(0).Item("TICKETNO")
                    Me.tb_vehicleno.Text = ds.Tables(0).Rows(0).Item("VEHICLENO")
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONTAINERNO"))) Then
                    '    Me.tb_container.Text = ds.Tables(0).Rows(0).Item("CONTAINERNO")
                    'End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANSPORTER"))) Then
                    '    Me.tb_transporter.Text = ds.Tables(0).Rows(0).Item("TRANSPORTER")
                    'End If
                    If Me.cb_inouttype.Text = "T" Then
                        cb_sledcode.Hide()
                        tb_sledesc.Hide()
                        cb_fritem.Show()
                        tb_fritemdesc.Show()
                        l_Project.Text = "From Item"
                        l_tomat.Text = "To Item"
                        Me.tb_frintitem.Text = ds.Tables(0).Rows(0).Item("FRINTITEMCODE")
                        Me.cb_fritem.Text = ds.Tables(0).Rows(0).Item("FRITEMDESC")
                        Me.tb_fritemdesc.Text = ds.Tables(0).Rows(0).Item("FRITEMCODE")
                    ElseIf Me.cb_inouttype.Text = "I" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Customer"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "S" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    ElseIf Me.cb_inouttype.Text = "W" Then
                        cb_sledcode.Show()
                        tb_sledesc.Show()
                        cb_fritem.Hide()
                        tb_fritemdesc.Hide()
                        l_Project.Text = "Supplier"
                        l_tomat.Text = "Product"
                        Me.tb_frintitem.Text = 0
                        Me.cb_fritem.Text = "0"
                        Me.tb_fritemdesc.Text = "0"
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("ACCOUNTCODE"))) Then
                        Me.Tb_accountcode.Text = ds.Tables(0).Rows(0).Item("ACCOUNTCODE")
                    End If
                    Me.cb_sledcode.Text = ds.Tables(0).Rows(0).Item("SLEDDESC")
                    Me.tb_sledesc.Text = ds.Tables(0).Rows(0).Item("SLEDCODE")
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("INTITEMCODE"))) Then
                        Me.Tb_intitemcode.Text = ds.Tables(0).Rows(0).Item("INTITEMCODE")
                    End If
                    Me.cb_itemcode.Text = ds.Tables(0).Rows(0).Item("ITEMDESC")
                    Me.tb_itemdesc.Text = ds.Tables(0).Rows(0).Item("ITEMCODE")

                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NUMBEROFPCS"))) Then
                        Me.tb_numberofpcs.Text = ds.Tables(0).Rows(0).Item("NUMBEROFPCS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVERNAM"))) Then
                        Me.cb_dcode.Text = ds.Tables(0).Rows(0).Item("DRIVERNAM")
                        Me.tb_DRIVERNAM.Text = ds.Tables(0).Rows(0).Item("DCODE")
                    End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("NATIONALITY"))) Then
                    '    Me.tb_NATIONALITY.Text = ds.Tables(0).Rows(0).Item("NATIONALITY")
                    'End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DRIVINGLICNO"))) Then
                    '    Me.tb_DRIVINGLICNO.Text = ds.Tables(0).Rows(0).Item("DRIVINGLICNO")
                    'End If
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("POST_DATE"))) Then
                        Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("POST_DATE")
                    Else
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DATEOUT"))) Then
                            Me.d_newdate.Text = ds.Tables(0).Rows(0).Item("DATEOUT")
                        End If
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("RATE"))) Then
                        Me.tb_prlist.Text = ds.Tables(0).Rows(0).Item("RATE")
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
                    If cb_inouttype.Text = "I" Then
                        Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("BSART")
                        Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("BSART")
                    ElseIf cb_inouttype.Text = "O" Then
                        Me.cb_sap_docu_type.Text = ds.Tables(0).Rows(0).Item("AUART")
                        Me.tb_sap_doc.Text = ds.Tables(0).Rows(0).Item("AUART")
                        'Me.Label25.Visible = True
                        'Me.rtb_gprem.Visible = True
                        'Me.b_gp.Visible = True
                        'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("GPREMARKS"))) Then
                        '    Me.rtb_gprem.Text = ds.Tables(0).Rows(0).Item("GPREMARKS")
                        'End If
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("SORDERNO"))) Then
                        Me.tb_orderno.Text = ds.Tables(0).Rows(0).Item("SORDERNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DELIVERYNO"))) Then
                        Me.tb_dsno.Text = ds.Tables(0).Rows(0).Item("DELIVERYNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PONO"))) Then
                        Me.Tb_asno.Text = ds.Tables(0).Rows(0).Item("PONO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("AGMIXNO"))) Then
                        Me.tb_IBDSNO.Text = ds.Tables(0).Rows(0).Item("AGMIXNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CONSNO"))) Then
                        Me.Tb_cons_sen_branch.Text = ds.Tables(0).Rows(0).Item("CONSNO")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_CHARGE"))) Then
                        Me.Tb_transp.Text = ds.Tables(0).Rows(0).Item("TRANS_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PENALTY"))) Then
                        Me.Tb_penalty.Text = ds.Tables(0).Rows(0).Item("PENALTY")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("MACHINE_CHARGE"))) Then
                        Me.Tb_eqpchrgs.Text = ds.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("LABOUR_CHARGE"))) Then
                        Me.Tb_labourcharges.Text = ds.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CCIC"))) Then
                        Me.Tb_ccic.Text = ds.Tables(0).Rows(0).Item("CCIC")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDCODE"))) Then
                        Me.tb_omcustcode.Text = ds.Tables(0).Rows(0).Item("OMSLEDCODE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMSLEDDESC"))) Then
                        Me.cb_omcustdesc.Text = ds.Tables(0).Rows(0).Item("OMSLEDDESC")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("PRICETON"))) Then
                        Me.tb_PRICETON.Text = ds.Tables(0).Rows(0).Item("PRICETON")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("OMPRICE"))) Then
                        Me.tb_omcustprice.Text = ds.Tables(0).Rows(0).Item("OMPRICE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNS"))) Then
                        Me.tb_sapord.Text = ds.Tables(0).Rows(0).Item("VBELNS")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELND"))) Then
                        Me.tb_sapdocno.Text = ds.Tables(0).Rows(0).Item("VBELND")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("VBELNI"))) Then
                        Me.tb_sapinvno.Text = ds.Tables(0).Rows(0).Item("VBELNI")
                    End If
                    'If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                    '    Me.cb_ib.Checked = True
                    'ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                    '    Me.cb_ib.Checked = False
                    'End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                        Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                        Me.tb_CUSTTYPE.Text = ds.Tables(0).Rows(0).Item("CUSTTYPE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECODE"))) Then
                        Me.tb_typecode.Text = ds.Tables(0).Rows(0).Item("TYPECODE")
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                        Me.tb_typecatg_pt.Text = ds.Tables(0).Rows(0).Item("TYPECATG_PT")
                    End If
                    'update data table in case of multiple items.
                    Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                    & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                    & " from(stwbmibds_pr_MULTI)" _
                    & " where(INTDOCNO =" & Me.Tb_intdocno.Text & ")"
                    Dim da1 As New OracleDataAdapter(sql, conn)
                    da1.Fill(ds1)
                    'If Me.tb_IBDSNO.Text = "" Then
                    If Me.cb_inouttype.Text = "I" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.b_genst.Visible = False
                        Me.Button1.Visible = False
                        Me.B_PO.Visible = True
                    ElseIf Me.cb_inouttype.Text = "O" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.Button1.Visible = True
                        Me.b_genst.Visible = False
                        Me.B_PO.Visible = False
                    ElseIf Me.cb_inouttype.Text = "T" Then
                        Me.b_genis.Visible = False
                        Me.b_gends.Visible = False
                        Me.Button1.Visible = False
                        Me.b_genst.Visible = False
                        Me.b_transfer.Visible = True
                    End If
                    'Else
                    '    Me.b_gends.Visible = False
                    '    Me.b_genis.Visible = False
                    '    Me.b_genst.Visible = False
                    'Me.Button1.Visible = False
                    'Me.B_PO.Visible = False
                    'End If
                    Me.b_firstwt.Enabled = False
                    Me.b_firstwt2.Enabled = False
                    If Me.tb_SECONDQTY.Text = 0 Then
                        Me.b_secondwt.Enabled = True
                        Me.b_secondwt2.Enabled = True
                        tmode = 2
                    End If
                    If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
                        'Me.B_PO.Visible = False
                        'Me.Button1.Visible = False
                        freeze_scr()
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
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmd1 As New OracleCommand
                    cmd1.Connection = conn
                    cmd1.Parameters.Clear()
                    cmd1.CommandText = "curspkg_join_pr.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                    cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                    cmd1.ExecuteNonQuery()
                    conn.Close()

                Else
                    MsgBox("No Records Found for this transaction #", MsgBoxStyle.Information)
                    'Me.tb_ticketno.Focus()
                End If
                Me.tb_trans.Text = "0"


                If cb_inouttype.Text = "I" Then
                    glbvar.temp_suppcode = Me.tb_sledesc.Text
                    glbvar.temp_suppdesc = Me.cb_sledcode.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    sl_item_driv_load()
                ElseIf cb_inouttype.Text = "O" Then
                    glbvar.temp_suppcode = Me.tb_sledesc.Text
                    glbvar.temp_suppdesc = Me.cb_sledcode.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    cust_item_driv_load()
                ElseIf cb_inouttype.Text = "T" Then
                    glbvar.temp_suppcode = Me.cb_fritem.Text
                    glbvar.temp_suppdesc = Me.tb_fritemdesc.Text
                    glbvar.temp_itemcode = Me.cb_itemcode.Text
                    glbvar.temp_itemdesc = Me.tb_itemdesc.Text
                    glbvar.temp_drcode = Me.cb_dcode.Text
                    glbvar.temp_drdesc = Me.tb_DRIVERNAM.Text
                    glbvar.temp_doctype = Me.cb_sap_docu_type.Text
                    glbvar.temp_docdesc = Me.tb_sap_doc.Text
                    glbvar.temp_omsledcode = Me.tb_omcustcode.Text
                    glbvar.temp_omsleddesc = Me.cb_omcustdesc.Text
                    tran_item_driv_load()
                End If
                If Me.tb_sap_doc.Text = "QN" Then
                    Me.Tb_asno.Visible = True
                    Me.l_pono.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QI" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIB" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    'Me.b_mixmat.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIX" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QO" Then
                    Me.cb_omcustdesc.Enabled = True
                    Me.tb_omcustcode.Enabled = True
                    Me.tb_omcustprice.Enabled = True
                    Me.Tb_custktdt.Visible = True
                    Me.Label46.Enabled = True
                    Me.Label47.Enabled = True
                    Me.Label41.Visible = True
                    Me.cb_omcustdesc.Visible = True
                    Me.tb_omcustcode.Visible = True
                    Me.tb_omcustprice.Visible = True
                    Me.Tb_cust_ticket_no.Visible = True
                    'Me.Label38.Visible = True
                    Me.Label46.Visible = True
                    Me.Label47.Visible = True
                    'Me.tb_IBDSNO.Visible = True
                    'ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    '   Me.tb_IBDSNO.Visible = True
                ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                    Me.tb_orderno.Visible = True
                    Me.tb_dsno.Visible = True
                    Me.l_dsno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "SO #"
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "RO #"
                ElseIf Me.tb_sap_doc.Text = "ZCWR" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "Billing #"
                Else
                    'Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    'Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    'l_pono.Visible = False
                    l_so.Visible = False
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
            'ElseIf tmode = 0 Then
            'Else
            'MsgBox("Please select New or edit or cancel")
        End If 'lock
    End Sub

    Private Sub tb_PRICETON_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_PRICETON.LostFocus
        Try

            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            sql = "SELECT   to_number(nvl(AMOUNT,0)) AMOUNT, to_number(nvl(PRICE_TOLERANCE,0)/100) PCT" _
                    & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                    & " WHERE z1.userauth_no = z2.userauth_no" _
                    & " AND z1.username = z2.userid" _
                    & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                    & " AND z2.matnr = " & "'" & tb_itemdesc.Text & "'" _
                    & " and Z1.INTAUTHNO =  (SELECT   MAX (d.INTAUTHNO) " _
                    & " FROM   ZUSER_AUTH_H d " _
                    & " where username  = " & "'" & glbvar.userid & "'" & ")"


            Dim dpct = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpct.Fill(dpc)
            Dim user_tol_value As Decimal
            Dim user_sales_value As Decimal
            Dim user_tot_allowed As Decimal
            Dim user_sales_allowed As Decimal
            Dim pct As Decimal
            Dim amt As Decimal
            Dim a = dpc.Tables(0).Rows.Count
            If dpc.Tables(0).Rows.Count > 0 Then
                pct = dpc.Tables(0).Rows(0).Item("pct")
                amt = dpc.Tables(0).Rows(0).Item("amount")

                Dim plist = Convert.ToDecimal(Me.tb_prlist.Text)
                user_tol_value = pct * plist
                user_sales_value = 2 * plist
                user_sales_allowed = Convert.ToDecimal(Me.tb_prlist.Text) + user_sales_value
                user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text)
                If pct <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text) + user_tol_value
                ElseIf amt <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text) + amt / 1000
                End If
                If Me.cb_inouttype.Text = "I" Then
                    'If Me.tb_sap_doc.Text = "QD" Or Me.tb_sap_doc.Text = "QMX" Then
                    If Me.tb_PRICETON.Text > user_tot_allowed Then
                        Me.tb_PRICETON.Text = 0
                        MsgBox("Price not matching as the latest Pricelist")
                        tb_PRICETON.Focus()
                    Else
                        tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                    'Else
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    'End If
                ElseIf Me.cb_inouttype.Text = "O" Then
                    If Me.tb_sap_doc.Text = "ZCWA" Then
                        If Me.tb_PRICETON.Text > user_sales_allowed Then
                            Me.tb_PRICETON.Text = 0
                            MsgBox("Price not matching as the latest Pricelist")
                            tb_PRICETON.Focus()
                        Else
                            tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                        End If
                    Else
                        tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                End If
            Else
                Me.tb_PRICETON.Text = 0
                MsgBox("You are not authorized to enter price for this material")
            End If
            conn.Close()
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
        cmd.CommandText = "curspkg_join_pr.sledmst"
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
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.custmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            omdssld.Clear()
            omdasld = New OracleDataAdapter(cmd)
            omdasld.TableMappings.Add("Table", "sled")
            omdasld.Fill(omdssld)
            cb_omcustdesc.DataSource = omdssld.Tables("sled")
            cb_omcustdesc.DisplayMember = omdssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_omcustdesc.ValueMember = omdssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.itmmst"
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
        cmd.CommandText = "curspkg_join_pr.drmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.docmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = "I"
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dadoc = New OracleDataAdapter(cmd)
            dadoc.TableMappings.Add("Table", "doc")
            dsdoc.Clear()
            dadoc.Fill(dsdoc)
            cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
            cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
            cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString
            conn.Close()
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.cb_sledcode.Text = glbvar.temp_suppdesc
        Me.tb_sledesc.Text = glbvar.temp_suppcode
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
        Me.cb_sap_docu_type.Text = glbvar.temp_doctype
        Me.tb_sap_doc.Text = glbvar.temp_docdesc
        Me.tb_omcustcode.Text = glbvar.temp_omsledcode
        Me.cb_omcustdesc.Text = glbvar.temp_omsleddesc

    End Sub
    Private Sub cust_item_driv_load()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.custmst"
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
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.itmmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.docmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("modl", OracleDbType.Varchar2)).Value = "O"
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dadoc = New OracleDataAdapter(cmd)
            dadoc.TableMappings.Add("Table", "doc")
            dsdoc.Clear()
            dadoc.Fill(dsdoc)
            cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
            cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
            cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString
            conn.Close()
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.cb_sledcode.Text = glbvar.temp_suppdesc
        Me.tb_sledesc.Text = glbvar.temp_suppcode
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
        Me.cb_sap_docu_type.Text = glbvar.temp_doctype
        Me.tb_sap_doc.Text = glbvar.temp_docdesc
        Me.tb_omcustcode.Text = glbvar.temp_omsledcode
        Me.cb_omcustdesc.Text = glbvar.temp_omsleddesc

    End Sub
    Private Sub tran_item_driv_load()


        conn = New OracleConnection(constr)

        Dim cmd As New OracleCommand
        cmd.Connection = conn
        'itemcode
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.itmmst"
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
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.fitmmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.grpdivcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsfitm.Clear()
            dafitm = New OracleDataAdapter(cmd)
            dafitm.TableMappings.Add("Table", "fitm")
            dafitm.Fill(dsfitm)
            conn.Close()
            cb_fritem.DataSource = dsfitm.Tables("fitm")
            cb_fritem.DisplayMember = dsfitm.Tables("fitm").Columns("itmdsc").ToString
            cb_fritem.ValueMember = dsfitm.Tables("fitm").Columns("itmcde").ToString
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.sledmst"
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
            cb_sledcode.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sledcode.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.intordmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dspsld.Clear()
            dapsld = New OracleDataAdapter(cmd)
            dapsld.TableMappings.Add("Table", "sledprj")
            dapsld.Fill(dspsld)
            cb_prjsledcode.DataSource = dspsld.Tables("sledprj")
            cb_prjsledcode.DisplayMember = dspsld.Tables("sledprj").Columns("SLEDDESC").ToString
            cb_prjsledcode.ValueMember = dspsld.Tables("sledprj").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.tb_sledesc.Text = glbvar.temp_suppcode
        Me.cb_sledcode.Text = glbvar.temp_suppdesc
        Me.tb_prjsledesc.Text = glbvar.temp_prsuppcode
        Me.cb_prjsledcode.Text = glbvar.temp_prsuppdesc
        Me.cb_fritem.Text = glbvar.temp_fritemcode
        Me.tb_fritemdesc.Text = glbvar.temp_fritemdesc
        Me.cb_itemcode.Text = glbvar.temp_itemcode
        Me.tb_itemdesc.Text = glbvar.temp_itemdesc
        Me.cb_dcode.Text = glbvar.temp_drcode
        Me.tb_DRIVERNAM.Text = glbvar.temp_drdesc
        Me.cb_sap_docu_type.Text = glbvar.temp_doctype
        Me.tb_omcustcode.Text = glbvar.temp_omsledcode
        Me.cb_omcustdesc.Text = glbvar.temp_omsleddesc
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'first check if the current record is saved into the databse --> completed
        'Get all the records which do not have so,dn,biling number
        'send these records to sap for generating the so,dn,billing
        'get from sap the so number based on the ticke number 
        'update it wbms table with the returne so/dn/illing based on ticket number.




        'for Document type ZCOR the tb_dsno is mandatory
        'Outside Materials the customer ticket # and date to be made mandatory is manatory ZOMO
        'Inter Branch Consignemet Number from SAP to be stored in. This will become the refernce for receiving branch
        b_save_Click(sender, e)
        If tb_sap_doc.Text = "ZTBV" Then
            'ZSDSOPROCESSNEW()
            'Button1.Visible = False

        ElseIf tb_sap_doc.Text = "ZDCQ" Then

            'ZSDDIRECTCONTRACT()
            'Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZTCF" Then

            'ZSDCONSIGNFILLUP02()
            'Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZCWA" Then
            'ZSDCWASALES()
            'Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZTRE" Then
            'ZSDRETURNORDER()
            'Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZCWR" Then
            'ZSDCWARETURN()
            'Button1.Visible = False
        End If 'document checking endif



    End Sub


    Private Sub B_PO_Click(sender As Object, e As EventArgs) Handles B_PO.Click
        b_save_Click(sender, e)
        If tb_sap_doc.Text = "QD" Then
            'ZMMPOGRPROCESS() 'Direct Purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QN" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            'ZMMGENPOPROCESS() 'Against PO Purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QI" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            'ZINTERBRANCHDETAILSUPD() 'Interbranch complete purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QIB" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            'ZINTERBRANCHRET() 'Interbranch complete purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QX" Then
            'ZMMMIXMATPROCESS() 'Mixmaterial purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QMX" Then
            'ZMMMIXINMATPROCESS() ' against mix material purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QIM" Then
            'ZMMINTMIXMATPROCESS() ' interbranch mix material purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QIX" Then
            'ZMIXINTERBRANCHDETAILSUPD() ' interbranch against mix material purchase
            'B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QO" Then
            'ZMMOMAUTOPROCESS() 'OM purchase and sales
            'B_PO.Visible = False
        End If  'Document 

        'End If 'Main


    End Sub



    Private Sub cb_sap_docu_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sap_docu_type.SelectedIndexChanged
        Try
            If Me.cb_sap_docu_type.SelectedIndex <> -1 Then
                Me.tb_sap_doc.Text = Me.cb_sap_docu_type.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "DOCCODE = '" & Me.tb_sap_doc.Text & "'" & ""
                foundrow = dsdoc.Tables("doc").Select(expression)
                If foundrow.Count > 0 Then
                    Me.tb_docprint.Text = foundrow(0).ItemArray(2)
                End If
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the document")
                End If
            End If
            If tb_sap_doc.Text = "QO" Then
                Me.Tb_asno.Visible = False
                Me.tb_orderno.Visible = False
                Me.tb_IBDSNO.Visible = False
                Me.tb_dsno.Visible = False
                Me.Tb_cons_sen_branch.Visible = False
                'Me.b_mixmat.Visible = False
                'Me.cb_ib.Visible = False
                'Me.tb_FIRSTQTY.Enabled = False
                'Me.tb_SECONDQTY.Enabled = False
                Me.cb_omcustdesc.Enabled = True
                Me.tb_omcustcode.Enabled = True
                Me.tb_omcustprice.Enabled = True
                Me.Tb_custktdt.Visible = True
                Me.Label46.Enabled = True
                Me.Label47.Enabled = True
                Me.Label41.Visible = True
                Me.cb_omcustdesc.Visible = True
                Me.tb_omcustcode.Visible = True
                Me.tb_omcustprice.Visible = True
                Me.Tb_cust_ticket_no.Visible = True
                'Me.Label38.Visible = True
                Me.Label46.Visible = True
                Me.Label47.Visible = True
                omcustload()
                Me.cb_omcustdesc.Text = "Other Customer"
                Me.tb_omcustcode.Text = "0001000000"

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If


                Dim tdate = CDate(Me.tb_DATEIN.Text).Day.ToString("D2")
                Dim tmonth = CDate(Me.tb_DATEIN.Text).Month.ToString("D2")
                Dim tyear = CDate(Me.tb_DATEIN.Text).Year
                Dim docdate = tdate & "/" & tmonth & "/" & tyear
                Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                'sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                '        & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE price,MAX_PRICE,BUYPRICE" _
                '        & " FROM   stitmratehd h, stitmrate t, smitem m" _
                '        & " WHERE h.comp_code = t.comp_code" _
                '        & " AND h.div_code = t.div_code" _
                '        & " AND h.intrateno = t.intrateno" _
                '        & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                '        & " AND t.itemcode = " & "'" & tb_itemdesc.Text & "'" _
                '        & " AND m.itemcode = t.itemcode" _
                '        & " AND m.div_code = t.div_code" _
                '        & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                '        & " FROM   stitmratehd d where " _
                '        & " to_date(d.witheffdt,'DD/MM/RRRR') <= to_date(" & "'" & expenddt & "'" & ",'MM/DD/RRRR')" _
                '        & ")"
                'Try
                '    sql = " select z1.custlt,z1.kunnr,matnr,buy_price price,spl_price sellprice from ZCUST_PRICE_H z1,ZCUST_PRICE_I z2" _
                '        & " where z1.custlt = z2.custlt" _
                '        & " and z1.kunnr = z2.kunnr" _
                '        & " and z1.intprno = z2.intprno" _
                '        & " and z1.kunnr = '0000000099'" _
                '        & " and z2.matnr = '000000000000000016'" _
                '        & " AND z1.intprno = (SELECT   MAX (d.intprno)" _
                '        & " FROM   ZCUST_PRICE_H d where" _
                '        & " to_date(d.pricelist_date,'DD/MM/RRRR') <= to_date('29/11/2014','DD/MM/RRRR'))"

                '    dopr = New OracleDataAdapter(sql, conn)
                '    Dim dop As New DataSet
                '    dop.Clear()
                '    dopr.Fill(dop)

                '    If dop.Tables(0).Rows.Count > 0 Then

                '        Me.tb_prlist.Text = dop.Tables(0).Rows(0).Item("price")
                '        Me.tb_PRICETON.Text = dop.Tables(0).Rows(0).Item("price")
                '        Me.tb_omcustprice.Text = dop.Tables(0).Rows(0).Item("sellprice")

                '    End If
                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try



            ElseIf tb_sap_doc.Text <> "QO" Then
                'Me.tb_FIRSTQTY.Enabled = False
                'Me.tb_SECONDQTY.Enabled = False
                Me.cb_omcustdesc.Visible = False
                Me.tb_omcustcode.Visible = False
                Me.tb_omcustprice.Visible = False
                Me.Tb_cust_ticket_no.Visible = False
                'Me.Label38.Visible = False
                Me.Label46.Visible = False
                Me.Label47.Visible = False
                Me.Label41.Visible = False
                Me.Tb_custktdt.Visible = False
                If Me.tb_sap_doc.Text = "QN" Then
                    Me.Tb_asno.Visible = True
                    Me.l_pono.Visible = True
                    Me.Tb_cons_sen_branch.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_cons.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "QI" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                    'Me.cb_ib.Checked = True
                ElseIf Me.tb_sap_doc.Text = "QIB" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    'Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                    'Me.cb_ib.Checked = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIX" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QO" Then
                    Me.cb_omcustdesc.Enabled = True
                    Me.tb_omcustcode.Enabled = True
                    Me.tb_omcustprice.Enabled = True
                    Me.Tb_custktdt.Visible = True
                    Me.Label46.Enabled = True
                    Me.Label47.Enabled = True
                    Me.Label41.Visible = True
                    Me.cb_omcustdesc.Visible = True
                    Me.tb_omcustcode.Visible = True
                    Me.tb_omcustprice.Visible = True
                    Me.Tb_cust_ticket_no.Visible = True
                    Me.Label46.Visible = True
                    Me.Label47.Visible = True
                    'Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                    Me.tb_orderno.Visible = True
                    Me.tb_dsno.Visible = True
                    Me.l_dsno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "SO #"
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "RO #"
                    Me.tb_dsno.Visible = False
                    Me.l_dsno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "ZCWR" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.l_so.Text = "Billing #"
                    Me.tb_dsno.Visible = False
                    Me.l_dsno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    'Me.cb_ib.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_agmix.Visible = False
                    'Me.b_mixmat.Visible = False
                Else
                    'Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    'Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    'l_pono.Visible = False
                    l_so.Visible = False
                    'Me.b_mixmat.Visible = False
                End If




                conn.Close()
            End If  'Document 

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub












    Public Sub omcustload()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.custmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            omdssld.Clear()
            omdasld = New OracleDataAdapter(cmd)
            omdasld.TableMappings.Add("Table", "sled")
            omdasld.Fill(omdssld)
            cb_omcustdesc.DataSource = omdssld.Tables("sled")
            cb_omcustdesc.DisplayMember = omdssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_omcustdesc.ValueMember = omdssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
        conn.Close()
    End Sub

    Private Sub cb_omcustdesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Me.cb_omcustdesc.SelectedIndex <> -1 Then
            Me.tb_omcustcode.Text = Me.cb_omcustdesc.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_omcustcode.Text & "'" & ""
            foundrow = omdssld.Tables("sled").Select(expression)
            If foundrow.Count > 1 Then
                MsgBox("More number of records found for the supplier")
            Else
                For j = 0 To foundrow.Count - 1
                    Me.Tb_accountcode.Text = foundrow(0).Item("ACCOUNTCODE").ToString
                Next
            End If
        End If
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If


        'Dim tdate = CDate(Me.tb_DATEIN.Text).Day.ToString("D2")
        'Dim tmonth = CDate(Me.tb_DATEIN.Text).Month.ToString("D2")
        'Dim tyear = CDate(Me.tb_DATEIN.Text).Year
        'Dim docdate = tdate & "/" & tmonth & "/" & tyear
        'Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim ddate
        If Me.Tb_custktdt.Text = "" Then
            ddate = Today.Date
        Else
            ddate = Me.Tb_custktdt.Text
        End If
        Dim tdate = CDate(ddate).Day.ToString("D2")
        Dim tmonth = CDate(ddate).Month.ToString("D2")
        Dim tyear = CDate(ddate).Year
        Dim docdate = tyear & tmonth & tdate
        'sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
        '        & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE price,MAX_PRICE,BUYPRICE" _
        '        & " FROM   stitmratehd h, stitmrate t, smitem m" _
        '        & " WHERE h.comp_code = t.comp_code" _
        '        & " AND h.div_code = t.div_code" _
        '        & " AND h.intrateno = t.intrateno" _
        '        & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
        '        & " AND t.itemcode = " & "'" & tb_itemdesc.Text & "'" _
        '        & " AND m.itemcode = t.itemcode" _
        '        & " AND m.div_code = t.div_code" _
        '        & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
        '        & " FROM   stitmratehd d where " _
        '        & " to_date(d.witheffdt,'DD/MM/RRRR') <= to_date(" & "'" & expenddt & "'" & ",'MM/DD/RRRR')" _
        '        & ")"
        Try
            sql = " select z1.custlt,z1.kunnr,matnr,buy_price/1000 price,spl_price/1000 sellprice from ZCUST_PRICE_H z1,ZCUST_PRICE_I z2" _
                & " where z1.custlt = z2.custlt" _
                & " and z1.intprno = z2.intprno" _
                & " and z2.kunnr = " & "'" & tb_omcustcode.Text & "'" _
                & " and z2.matnr = " & "'" & tb_itemdesc.Text & "'" _
                & " AND z1.intprno = (SELECT   MAX (d.intprno)" _
                & " FROM   ZCUST_PRICE_H d where" _
                & " to_number(to_char(d.pricelist_date,'YYYYMMDD')) <= to_number(" & "'" & docdate & "'))"

            dopr = New OracleDataAdapter(sql, conn)
            Dim dop As New DataSet
            dop.Clear()
            dopr.Fill(dop)

            If dop.Tables(0).Rows.Count > 0 Then

                Me.tb_prlist.Text = dop.Tables(0).Rows(0).Item("price")
                Me.tb_PRICETON.Text = dop.Tables(0).Rows(0).Item("price")
                Me.tb_omcustprice.Text = dop.Tables(0).Rows(0).Item("sellprice")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub







    Private Sub tb_orderno_LostFocus(sender As Object, e As EventArgs)
        tb_dsno.Focus()
    End Sub


    Private Sub TextBox1_KeyPress(sender As Object, _
                              e As KeyPressEventArgs) Handles tb_itmno.KeyPress
        e.Handled = Not Char.IsNumber(e.KeyChar)
    End Sub

    Private Sub tb_ticketno_LostFocus1(sender As Object, e As EventArgs) Handles tb_ticketno.LostFocus
        Me.b_tkt.Focus()
    End Sub

    Private Sub tb_sveh_LostFocus(sender As Object, e As EventArgs) Handles tb_sveh.LostFocus
        Me.b_vehino.Focus()
    End Sub

    Private Sub tb_trans_LostFocus(sender As Object, e As EventArgs) Handles tb_trans.LostFocus
        Me.b_trans.Focus()
    End Sub




    Private Sub d_newdate_ValueChanged() Handles d_newdate.Validated
        'If d_newdate.Text < CDate(tb_DATEOUT.Text) Then
        '    MsgBox("Posting date cannot be less than dateout")
        '    d_newdate.Text = Today.Date
        'Else
        If d_newdate.Text > Today.Date Then
            MsgBox("Posting date cannot be greater than today")
            d_newdate.Text = Today.Date
        End If

    End Sub

    Private Sub OpenVendorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenVendorToolStripMenuItem.Click
        venlist_DoubleClick(sender, e)
    End Sub

    Private Sub SelectVendorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectVendorToolStripMenuItem.Click
        Me.loadven.Focus()
        Me.loadven.Select()
    End Sub

    Private Sub DisableLoadVendorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisableLoadVendorToolStripMenuItem.Click
        loadven.Visible = False
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        b_save_Click(sender, e)
    End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles b_gp.Click
    '    Try
    '        glbvar.vintdocno = Me.Tb_intdocno.Text
    '        glbvar.gdoccode = Me.tb_sap_doc.Text
    '        If Me.cb_inouttype.Text = "T" Then
    '            'STFSTWT.Show()
    '            'STFSTWT.Close()
    '        Else
    '            GP.Show()
    '            'GP.Close()
    '        End If


    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)
    '        'MsgBox(ex.InnerException)
    '        Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
    '        Console.WriteLine("Inner Exception is {0}", ex.InnerException)
    '    End Try
    'End Sub





    Private Sub b_scout_Click(sender As Object, e As EventArgs)
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'W' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
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
            l_Project.Text = "Supplier"
            l_tomat.Text = "Product"
            cmbloading()
            Me.tb_docprint.Text = "SCALE OUTSIDE TICKET"
            Me.tb_sap_doc.Text = "SW"
            Me.cb_sap_docu_type.Text = "Scale Outside"
            Me.tb_sap_doc.Enabled = False
            Me.cb_sap_docu_type.Enabled = False
            Me.cb_sledcode.Text = "Dummy Supplier"
            Me.tb_sledesc.Text = "0000000000"
            Me.tb_itemdesc.Text = "000000000000000000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"
            tmode = 1
            b_firstwt.Enabled = True
            Me.b_secondwt.Enabled = False
            b_firstwt2.Enabled = True
            Me.b_secondwt2.Enabled = False
            cb_inouttype.SelectedValue = "W"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = " select stwbmibds_prSEQ.nextval val from dual"
        dpr = New OracleDataAdapter(sql, conn)
        Dim dp As New DataSet
        dp.Clear()
        dpr.Fill(dp)
        If dp.Tables(0).Rows.Count > 0 Then
            Me.Tb_dmy_intd.Text = dp.Tables(0).Rows(0).Item("val")
        End If
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   stwbmibds_pr WBM WHERE INOUTTYPE = 'I' "
        dpr = New OracleDataAdapter(sql, conn)
        Dim dpt As New DataSet
        dpt.Clear()
        dpr.Fill(dpt)
        If dpt.Tables(0).Rows.Count > 0 Then
            Me.tb_dmy_tkt.Text = dpt.Tables(0).Rows(0).Item("tkt")
        End If
        Try
            Dim cmd1 As New OracleCommand()
            Dim cmd2 As New OracleCommand()
            cmd1.Connection = conn
            cmd2.Connection = conn
            cmd1.CommandText = " INSERT INTO stwbmibds_pr_PR (" _
            & " INTDOCNO,ticketno,INOUTTYPE,VEHICLENO, " _
            & "SLEDCODE, SLEDDESC, ITEMCODE, ITEMDESC, DCODE, DRIVERNAM" _
            & " )" _
            & " values (" _
            & Me.Tb_dmy_intd.Text _
            & "," _
            & Me.tb_dmy_tkt.Text _
            & "," _
            & "I" _
            & "," _
            & Me.tb_vehicleno.Text _
            & "," _
            & Me.cb_sledcode.Text _
            & "," _
            & Me.tb_sledesc.Text _
            & "," _
            & Me.tb_itemdesc.Text _
            & "," _
            & Me.cb_itemcode.Text _
            & "," _
            & Me.tb_DRIVERNAM.Text _
            & "," _
            & Me.cb_dcode.Text _
            & " )"
            cmd2.CommandText = "commit"
            cmd1.CommandType = CommandType.Text
            cmd2.CommandType = CommandType.Text
            cmd1.ExecuteNonQuery()
            cmd2.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub b_transfer_Click(sender As Object, e As EventArgs) Handles b_transfer.Click
        Try
            b_save_Click(sender, e)
            Dim cmd As New OracleCommand
            If Me.Tb_intdocno.Text = "" Then
                MsgBox("Please save the record first")
                Me.b_save.Focus()
            ElseIf Me.tb_sledesc.Text = "" Then
                MsgBox("Select a vendor")
                Me.tb_sledesc.Focus()
            ElseIf Me.cb_itemcode.Text = "" Then
                MsgBox("Select an itemcode")
                Me.cb_itemcode.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                'Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
            ElseIf Me.tb_sledesc.Text = "0000000000" Then
                MsgBox("Project should be selected")
                Me.tb_searchbyno.Focus()
            ElseIf Me.tb_prjsledesc.Text = "0000000000" Then
                MsgBox("Internal Order should be selected")
            ElseIf cb_fritem.Text.Substring(0, 3).ToUpper <> "MIX" Then
                MsgBox("Mix Material Should be selected in from")
                'ElseIf Me.tb_PRICETON.Text = "0" Then
                '    MsgBox(" Price must be entered ")
            Else

                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim saprfcrepos As RfcRepository = saprfcdest.Repository



                'for Document type ZCOR the tb_dsno is mandatory
                'Outside Materials the customer ticket # and date to be made mandatory is manatory ZOMO
                'Inter Branch Consignemet Number from SAP to be stored in. This will become the refernce for receiving branch



                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("Z_STOCK_TRF_PRJ")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("GOODSMVT_HEADER")
                ohdrin.SetValue("PSTNG_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))
                ohdrin.SetValue("DOC_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))

                Dim scltyp As IRfcStructure = sodnbil.GetStructure("GOODSMVT_CODE") 'DLCUST_FIELD 
                scltyp.SetValue("GM_CODE", "04")
                sodnbil.SetValue("ZPRJN", tb_sledesc.Text)
                sodnbil.SetValue("ZPRJS", tb_prjsledesc.Text)
                sodnbil.SetValue("ZREFPO", Tb_asno.Text)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join_pr.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    conn.Close()
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join_pr.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        conn.Close()
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1
                            Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                            Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure

                            itmstru.SetValue("MATERIAL", Me.tb_fritemdesc.Text)
                            itmstru.SetValue("PLANT", glbvar.divcd)
                            itmstru.SetValue("STGE_LOC", glbvar.LGORT)
                            itmstru.SetValue("MOVE_TYPE", "309")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            itmstru.SetValue("ENTRY_QNT", Math.Round(qt, 3))
                            itmstru.SetValue("ENTRY_UOM", "TO")
                            itmstru.SetValue("MOVE_MAT", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                            itmstru.SetValue("MOVE_STLOC", glbvar.LGORT)
                            itmstru.SetValue("LINE_ID", sl)

                            oitmin.Append(itmstru)

                        Next
                    Else
                        Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                        Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                        Dim itm As UInteger = Convert.ToUInt64("10")

                        'ensure the material number is left padded with zeros.
                        itmstru.SetValue("MATERIAL", Me.tb_fritemdesc.Text)
                        itmstru.SetValue("PLANT", glbvar.divcd)
                        itmstru.SetValue("STGE_LOC", glbvar.LGORT)
                        itmstru.SetValue("MOVE_TYPE", "309")
                        Dim qt As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        itmstru.SetValue("ENTRY_QNT", qt)
                        itmstru.SetValue("ENTRY_UOM", "TO")
                        itmstru.SetValue("MOVE_MAT", Me.tb_itemdesc.Text)
                        itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                        itmstru.SetValue("MOVE_STLOC", glbvar.LGORT)
                        oitmin.Append(itmstru)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try


                Dim rttbl As IRfcTable = sodnbil.GetTable("RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                sodnbil.Invoke(saprfcdest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))
                ReDim id(rttbl.RowCount - 1)
                ReDim typ(rttbl.RowCount - 1)
                ReDim nmbr(rttbl.RowCount - 1)
                ReDim mesg(rttbl.RowCount - 1)
                ReDim tkt(rttbl.RowCount - 1)
                Dim soercnt As Integer = 0
                DataGridView1.Refresh()
                For l = 0 To rttbl.RowCount - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(l).Cells("TYPE").Value = rttbl(l).Item("Type").GetString() 'err.GetValue("TYPE")
                    If rttbl(l).Item("Type").GetString() = "E" Then
                        soercnt = soercnt + 1
                    End If
                    DataGridView1.Rows(l).Cells("i_d").Value = rttbl(l).Item("ID").GetString() 'err.GetValue("ID")
                    DataGridView1.Rows(l).Cells("NUMBER").Value = rttbl(l).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView1.Rows(l).Cells("MESAGE").Value = rttbl(l).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
                    typ(l) = rttbl(l).Item("Type").GetString()
                    id(l) = rttbl(l).Item("ID").GetString()
                    nmbr(l) = rttbl(l).Item("NUMBER").GetString()
                    mesg(l) = rttbl(l).Item("MESSAGE").GetString()
                    tkt(l) = Me.tb_ticketno.Text
                Next
                'write the code for inserting tcket number.

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn

                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_errsap_uarr"
                cmd.CommandType = CommandType.StoredProcedure
                Dim ptyp As OracleParameter = New OracleParameter(":n1", OracleDbType.Char)
                ptyp.Direction = ParameterDirection.Input
                ptyp.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                ptyp.Value = typ

                Dim pid As OracleParameter = New OracleParameter(":n2", OracleDbType.Varchar2)
                pid.Direction = ParameterDirection.Input
                pid.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pid.Value = id

                Dim pnbr As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                pnbr.Direction = ParameterDirection.Input
                pnbr.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pnbr.Value = nmbr

                Dim pmesg As OracleParameter = New OracleParameter(":n3", OracleDbType.Varchar2)
                pmesg.Direction = ParameterDirection.Input
                pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pmesg.Value = mesg

                Dim ptkt As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                ptkt.Direction = ParameterDirection.Input
                ptkt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                ptkt.Value = tkt

                cmd.Parameters.Add(ptyp)
                cmd.Parameters.Add(pid)
                cmd.Parameters.Add(pnbr)
                cmd.Parameters.Add(pmesg)
                cmd.Parameters.Add(ptkt)
                cmd.ExecuteNonQuery()

                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Material Doc: # " & sodnbil.GetValue("MATERIALDOCUMENT").ToString _
                    & vbCrLf & "Delivery Note # " & sodnbil.GetValue("PRICE_DOC").ToString)
                    '& vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                    Me.tb_sapinvno.Text = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    Me.tb_sapdocno.Text = sodnbil.GetValue("PRICE_DOC").ToString
                    freeze_scr()
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("PRICE_DOC").ToString
                    cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CLng(Me.tb_ticketno.Text)
                    cmd.ExecuteNonQuery()
                    conn.Close()

                    Dim endtime = DateTime.Now.ToString()
                    'If glbvar.LGORT = "PR01" Then
                    '    b_transfer_Loc(sodnbil.GetValue("PRICE_DOC").ToString, sodnbil.GetValue("MATERIALDOCUMENT").ToString)
                    'Else
                    Me.b_crfillup.Visible = True
                    Me.b_crfillup.Enabled = True
                    'End If



                End If

                conn.Close()
            End If ' main end if
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_transfer_Loc(a, b)
        Try
            Dim cmd As New OracleCommand
            If Me.Tb_intdocno.Text = "" Then
                MsgBox("Please save the record first")
                Me.b_save.Focus()
            ElseIf Me.tb_sledesc.Text = "" Then
                MsgBox("Select a vendor")
                Me.tb_sledesc.Focus()
            ElseIf Me.cb_itemcode.Text = "" Then
                MsgBox("Select an itemcode")
                Me.cb_itemcode.Focus()
            ElseIf Me.tb_FIRSTQTY.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                'Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
            ElseIf Me.tb_sledesc.Text = "0000000000" Then
                MsgBox("Project should be selected")
                Me.tb_searchbyno.Focus()
                'ElseIf Me.tb_PRICETON.Text = "0" Then
                '    MsgBox(" Price must be entered ")
            Else

                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim saprfcrepos As RfcRepository = saprfcdest.Repository



                'for Document type ZCOR the tb_dsno is mandatory
                'Outside Materials the customer ticket # and date to be made mandatory is manatory ZOMO
                'Inter Branch Consignemet Number from SAP to be stored in. This will become the refernce for receiving branch



                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("Z_STOCK_TRF_PRJ")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("GOODSMVT_HEADER")
                ohdrin.SetValue("PSTNG_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))
                ohdrin.SetValue("DOC_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))

                Dim scltyp As IRfcStructure = sodnbil.GetStructure("GOODSMVT_CODE") 'DLCUST_FIELD 
                scltyp.SetValue("GM_CODE", "04")
                sodnbil.SetValue("ZPRJN", tb_sledesc.Text)
                sodnbil.SetValue("ZPRJS", tb_prjsledesc.Text)
                sodnbil.SetValue("ZREFPO", Tb_asno.Text)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join_pr.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    conn.Close()
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join_pr.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        conn.Close()
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1
                            Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                            Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure

                            itmstru.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            itmstru.SetValue("PLANT", glbvar.divcd)
                            itmstru.SetValue("STGE_LOC", glbvar.LGORT)
                            itmstru.SetValue("MOVE_TYPE", "311")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            itmstru.SetValue("ENTRY_QNT", Math.Round(qt, 3))
                            itmstru.SetValue("ENTRY_UOM", "TO")
                            itmstru.SetValue("MOVE_MAT", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                            itmstru.SetValue("MOVE_STLOC", "1000")
                            itmstru.SetValue("LINE_ID", sl)

                            oitmin.Append(itmstru)

                        Next
                    Else
                        Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                        Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                        Dim itm As UInteger = Convert.ToUInt64("10")

                        'ensure the material number is left padded with zeros.
                        itmstru.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        itmstru.SetValue("PLANT", glbvar.divcd)
                        itmstru.SetValue("STGE_LOC", glbvar.LGORT)
                        itmstru.SetValue("MOVE_TYPE", "311")
                        Dim qt As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        itmstru.SetValue("ENTRY_QNT", qt)
                        itmstru.SetValue("ENTRY_UOM", "TO")
                        itmstru.SetValue("MOVE_MAT", Me.tb_itemdesc.Text)
                        itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                        itmstru.SetValue("MOVE_STLOC", "1000")
                        oitmin.Append(itmstru)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try


                Dim rttbl As IRfcTable = sodnbil.GetTable("RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                sodnbil.Invoke(saprfcdest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))
                ReDim id(rttbl.RowCount - 1)
                ReDim typ(rttbl.RowCount - 1)
                ReDim nmbr(rttbl.RowCount - 1)
                ReDim mesg(rttbl.RowCount - 1)
                ReDim tkt(rttbl.RowCount - 1)
                Dim soercnt As Integer = 0
                DataGridView1.Refresh()
                For l = 0 To rttbl.RowCount - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(l).Cells("TYPE").Value = rttbl(l).Item("Type").GetString() 'err.GetValue("TYPE")
                    If rttbl(l).Item("Type").GetString() = "E" Then
                        soercnt = soercnt + 1
                    End If
                    DataGridView1.Rows(l).Cells("i_d").Value = rttbl(l).Item("ID").GetString() 'err.GetValue("ID")
                    DataGridView1.Rows(l).Cells("NUMBER").Value = rttbl(l).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView1.Rows(l).Cells("MESAGE").Value = rttbl(l).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
                    typ(l) = rttbl(l).Item("Type").GetString()
                    id(l) = rttbl(l).Item("ID").GetString()
                    nmbr(l) = rttbl(l).Item("NUMBER").GetString()
                    mesg(l) = rttbl(l).Item("MESSAGE").GetString()
                    tkt(l) = Me.tb_ticketno.Text
                Next
                'write the code for inserting tcket number.

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn

                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_errsap_uarr"
                cmd.CommandType = CommandType.StoredProcedure
                Dim ptyp As OracleParameter = New OracleParameter(":n1", OracleDbType.Char)
                ptyp.Direction = ParameterDirection.Input
                ptyp.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                ptyp.Value = typ

                Dim pid As OracleParameter = New OracleParameter(":n2", OracleDbType.Varchar2)
                pid.Direction = ParameterDirection.Input
                pid.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pid.Value = id

                Dim pnbr As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                pnbr.Direction = ParameterDirection.Input
                pnbr.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pnbr.Value = nmbr

                Dim pmesg As OracleParameter = New OracleParameter(":n3", OracleDbType.Varchar2)
                pmesg.Direction = ParameterDirection.Input
                pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                pmesg.Value = mesg

                Dim ptkt As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                ptkt.Direction = ParameterDirection.Input
                ptkt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                ptkt.Value = tkt

                cmd.Parameters.Add(ptyp)
                cmd.Parameters.Add(pid)
                cmd.Parameters.Add(pnbr)
                cmd.Parameters.Add(pmesg)
                cmd.Parameters.Add(ptkt)
                cmd.ExecuteNonQuery()

                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Material Doc: # " & sodnbil.GetValue("MATERIALDOCUMENT").ToString _
                    & vbCrLf & "Delivery Note # " & sodnbil.GetValue("PRICE_DOC").ToString)
                    '& vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                    Me.tb_sapinvno.Text = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    Me.tb_sapdocno.Text = sodnbil.GetValue("PRICE_DOC").ToString
                    freeze_scr()
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = a
                    cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = b
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CLng(Me.tb_ticketno.Text)
                    cmd.ExecuteNonQuery()
                    conn.Close()

                    Dim endtime = DateTime.Now.ToString()
                    'Me.b_crfillup.Visible = True
                    'Me.b_crfillup.Enabled = True

                End If

                conn.Close()
            End If ' main end if
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cb_prjsledcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_prjsledcode.SelectedIndexChanged
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        If Me.cb_prjsledcode.SelectedIndex <> -1 Then
            Me.tb_prjsledesc.Text = Me.cb_prjsledcode.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_prjsledesc.Text & "'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            If foundrow.Count > 0 Then
                If Not IsDBNull(foundrow(0).ItemArray(5)) Then
                    Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(3)
                    Me.tb_typecode.Text = foundrow(0).ItemArray(4)
                    Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(5)
                End If
            End If
            If foundrow.Count > 1 Then
                MsgBox("More number of records found for the supplier")
            Else
                For j = 0 To foundrow.Count - 1
                    Me.Tb_accountcode.Text = foundrow(0).Item("ACCOUNTCODE").ToString
                Next
            End If
        End If
    End Sub

    Private Sub tb_prjsrchbyno_TextChanged(sender As Object, e As EventArgs) Handles tb_prjsrchbyno.TextChanged
        Try

            'Dim tb2 As TextBox = CType(sender, TextBox)
            'If tb2.Text.Length > 0 Then
            '    Dim foundrow() As DataRow
            '    Dim expression As String = "SLEDCODE LIKE '" & tb2.Text & "%'" & ""
            '    foundrow = dsitm.Tables("dssld").Select(expression)
            '    loadven.Items.Clear()
            '    For i = 0 To foundrow.Count - 1

            '        Me.loadven.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
            '        Me.loadven.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)

            '    Next

            '    loadven.Visible = True
            'End If
            ' Try
            'If Asc(e.KeyChar) = 8 Then
            ' itmchar = ""
            'Else
            'itmchar = itmchar + e.KeyChar
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE LIKE '" & Me.tb_prjsrchbyno.Text & "%'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            loadven1.Items.Clear()
            For i = 0 To foundrow.Count - 1
                'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                Me.loadven1.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
                Me.loadven1.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)
            Next
            loadven1.Visible = True
            'End If
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub loadven1_DoubleClick(sender As Object, e As EventArgs) Handles loadven1.DoubleClick
        Try

            If Me.loadven1.SelectedItems(0).SubItems(0).Text <> "" Then


                Me.tb_prjsledesc.Text = Me.loadven1.SelectedItems(0).SubItems(0).Text

                Me.cb_prjsledcode.Text = Me.loadven1.SelectedItems(0).SubItems(1).Text

                Me.loadven1.Visible = False

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_crfillup_Click(sender As Object, e As EventArgs) Handles b_crfillup.Click
        sledfillup = Me.tb_sledesc.Text
        suppfillup = Me.tb_prjsledesc.Text
        tb_mixtkt.Text = tb_ticketno.Text
        Me.DataGridView1.Rows.Clear()
        If Me.tb_ticketno.Text <> "" AndAlso Me.Tb_intdocno.Text <> "" Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.delete_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.ExecuteNonQuery()
            conn.Close()
        End If
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "Select div_code,divdesc from mdivision where DIVTYPE = 'BR'"
        da = New OracleDataAdapter(sql, conn)
        Dim ddiv As New DataSet
        Try
            da.TableMappings.Add("Table", "div")
            da.Fill(ddiv)
            conn.Close()
            Me.tb_sledesc.Text = ddiv.Tables("div").Rows(0).Item("div_code") + "S"
            Me.cb_sledcode.Text = ddiv.Tables("div").Rows(0).Item("divdesc")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS_PR WBM WHERE INOUTTYPE = 'O' "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        'Me.tb_ticketno.Text = 61000005
        'Me.tb_FIRSTQTY.Text = 1234
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_iprcf"
        cmd.CommandType = CommandType.StoredProcedure
        'Try
        Me.cb_inouttype.Text = "O"
        Me.tb_sap_doc.Text = "ZTCF"
        Me.cb_sap_docu_type.Text = "ZTCF"

        cmd.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.Text
        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
        cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
        cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = DBNull.Value
        'If IsDBNull(Me.tb_container.Text) Then
        'Me.tb_container.Text = ""
        'Else
        '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
        'End If
        cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = DBNull.Value
        If cb_inouttype.Text = "T" Then
            cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = "224010 001"
            cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = "224010 001 0554"
            cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = "Other Supplier"
        Else
            cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
            cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
            cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
            cmd.Parameters.Add(New OracleParameter("ppSLEDCODE", OracleDbType.Varchar2)).Value = sledfillup
            cmd.Parameters.Add(New OracleParameter("ppSLEDDESC", OracleDbType.Varchar2)).Value = suppfillup
        End If
        cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
        cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
        cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

        If Me.cb_inouttype.Text <> "T" Then
            cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
            cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = "Dummy"
            cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = "00000"
        ElseIf Me.cb_inouttype.Text = "T" Then

            cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
            cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
            cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()
        End If
        cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = Me.tb_numberofpcs.Text
        cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
        cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
        cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("pFIRSTQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_FIRSTQTY.Text)
        cmd.Parameters.Add(New OracleParameter("pSECONDQTY", OracleDbType.Decimal)).Value = CDec(Trim(Me.tb_SECONDQTY.Text))

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
        Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("pDATEIN", OracleDbType.Date)).Value = dtin 'Convert.ToDateTime(Me.tb_DATEIN.Text)
        cmd.Parameters.Add(New OracleParameter("pTIMEIN", OracleDbType.Varchar2)).Value = Me.tb_TIMEIN.Text

        Dim dto As Date = FormatDateTime(Me.tb_DATEOUT.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("pDATEOUT", OracleDbType.Date)).Value = dto
        cmd.Parameters.Add(New OracleParameter("pTIMOUT", OracleDbType.Varchar2)).Value = Me.tb_TIMOUT.Text
        cmd.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
        cmd.Parameters.Add(New OracleParameter("pAPPDATE0", OracleDbType.Date)).Value = Today
        cmd.Parameters.Add(New OracleParameter("pAPPDATE1", OracleDbType.Date)).Value = Today
        cmd.Parameters.Add(New OracleParameter("pFIELD1", OracleDbType.Varchar2)).Value = glbvar.userid
        cmd.Parameters.Add(New OracleParameter("pSTATUS", OracleDbType.Varchar2)).Value = 1
        cmd.Parameters.Add(New OracleParameter("pFIELD2", OracleDbType.Varchar2)).Value = glbvar.userid
        cmd.Parameters.Add(New OracleParameter("pprice", OracleDbType.Decimal)).Value = CDec(Me.tb_PRICETON.Text)
        cmd.Parameters.Add(New OracleParameter("ptotprice", OracleDbType.Decimal)).Value = CDec(Me.tb_TOTALPRICE.Text)
        cmd.Parameters.Add(New OracleParameter("pprlist", OracleDbType.Decimal)).Value = CDec(Me.tb_prlist.Text)
        cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Direction = ParameterDirection.Output
        If cb_inouttype.Text = "I" Then


            cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
            cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "O" Then

            cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
        End If
        If cb_inouttype.Text = "I" Then


            cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = glbvar.EKORG
            cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = glbvar.EKGRP
            cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "O" Then


            cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = glbvar.VKORG
            cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = glbvar.VTWEG
        End If

        cmd.Parameters.Add(New OracleParameter("psVBELNS", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psVBELND", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psVBELNI", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psorderno", OracleDbType.Varchar2)).Value = Me.tb_orderno.Text
        cmd.Parameters.Add(New OracleParameter("pdeliveryno", OracleDbType.Varchar2)).Value = Me.tb_dsno.Text
        cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
        cmd.Parameters.Add(New OracleParameter("pitmno", OracleDbType.Varchar2)).Value = Me.tb_itmno.Text
        cmd.Parameters.Add(New OracleParameter("ptransportcharges", OracleDbType.Varchar2)).Value = Me.Tb_transp.Text
        cmd.Parameters.Add(New OracleParameter("ppenalty", OracleDbType.Varchar2)).Value = Me.Tb_penalty.Text
        cmd.Parameters.Add(New OracleParameter("pmachinecharges", OracleDbType.Varchar2)).Value = Me.Tb_eqpchrgs.Text
        cmd.Parameters.Add(New OracleParameter("plabourcharges", OracleDbType.Varchar2)).Value = Me.Tb_labourcharges.Text
        cmd.Parameters.Add(New OracleParameter("ppono", OracleDbType.Varchar2)).Value = Me.Tb_asno.Text
        cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
        cmd.Parameters.Add(New OracleParameter("pconsno", OracleDbType.Varchar2)).Value = Me.Tb_cons_sen_branch.Text
        cmd.Parameters.Add(New OracleParameter("pccic", OracleDbType.Varchar2)).Value = Me.Tb_ccic.Text
        cmd.Parameters.Add(New OracleParameter("pomprice", OracleDbType.Varchar2)).Value = Me.tb_omcustprice.Text
        cmd.Parameters.Add(New OracleParameter("pomsledcode", OracleDbType.Varchar2)).Value = Me.tb_omcustcode.Text
        cmd.Parameters.Add(New OracleParameter("pomsleddesc", OracleDbType.Varchar2)).Value = Me.cb_omcustdesc.Text
        'If cb_ib.Checked = True Then
        '    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
        'ElseIf cb_ib.Checked = False Then
        cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = ""
        'End If
        cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
        cmd.Parameters.Add(New OracleParameter("ppcusttype", OracleDbType.Varchar2)).Value = Me.tb_CUSTTYPE.Text
        cmd.Parameters.Add(New OracleParameter("pptypecode", OracleDbType.Varchar2)).Value = Me.tb_typecode.Text
        cmd.Parameters.Add(New OracleParameter("pptypecatg_pt", OracleDbType.Varchar2)).Value = Me.tb_typecatg_pt.Text
        Dim ndt As Date = FormatDateTime(Me.d_newdate.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("ppostdate", OracleDbType.Date)).Value = ndt
        cmd.Parameters.Add(New OracleParameter("pdivdesc", OracleDbType.Varchar2)).Value = glbvar.gcompname
        cmd.Parameters.Add(New OracleParameter("pgprem", OracleDbType.Varchar2)).Value = DBNull.Value
        Try
            cmd.ExecuteNonQuery()
            'Dim vint As Decimal
            'vint = cmd.Parameters("pINTDOCNO").Value.ToString  'CDec(cmd.Parameters("pINTDOCNO").Value)
            Me.Tb_intdocno.Text = cmd.Parameters("pINTDOCNO").Value.ToString
            'glbvar.multdocno = Me.Tb_intdocno.Text
            'glbvar.multtktno = Me.tb_ticketno.Text
            'glbvar.multinout = Me.cb_inouttype.Text
            conn.Close()
            'Me.b_firstwt.Enabled = False
            MsgBox("Record Saved")
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmdupdcr As New OracleCommand()
            Dim cmdupd As New OracleCommand()
            Dim cmdcmt As New OracleCommand()
            cmdupd.Connection = conn
            cmdupdcr.Connection = conn
            cmdcmt.Connection = conn
            cmdupd.CommandText = " update stwbmibds_pr set CFCREATED = " & Me.tb_ticketno.Text & " where ticketno = " & Me.tb_mixtkt.Text
            cmdupdcr.CommandText = " update stwbmibds_pr set MIXTRFTKT = " & Me.tb_mixtkt.Text & " where ticketno = " & Me.tb_ticketno.Text
            cmdcmt.CommandText = " commit"
            cmdupd.CommandType = CommandType.Text
            cmdupdcr.CommandType = CommandType.Text
            cmdcmt.CommandType = CommandType.Text
            cmdupd.ExecuteNonQuery()
            cmdupdcr.ExecuteNonQuery()
            cmdcmt.ExecuteNonQuery()
            conn.Close()
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.insert_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
            cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
            cmd1.ExecuteNonQuery()
            conn.Close()
            ZSDCONSIGNFILLUPPR()
            Button1.Visible = False
            b_crfillup.Visible = False
            b_cribpur_Click(sender, e)
            'b_cribpur.Visible = True
            'clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            conn.Close()
        End Try
    End Sub
    Private Sub b_cribpur_Click(sender As Object, e As EventArgs) Handles b_cribpur.Click
        'sledfillup = Me.tb_prjsledesc.Text
        'suppfillup = cb_prjsledcode.Text
        tb_cfillup.Text = tb_ticketno.Text
        Me.Tb_cons_sen_branch.Text = Me.tb_sapord.Text
        Me.DataGridView1.Rows.Clear()
        Me.Tb_cons_sen_branch.Visible = True

        If Me.tb_ticketno.Text <> "" AndAlso Me.Tb_intdocno.Text <> "" Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.delete_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.ExecuteNonQuery()
            conn.Close()
        End If
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "Select div_code,divdesc from mdivision where DIVTYPE = 'PR'"
        da = New OracleDataAdapter(sql, conn)
        Dim ddiv As New DataSet
        Try
            da.TableMappings.Add("Table", "div")
            da.Fill(ddiv)
            conn.Close()
            Me.tb_sledesc.Text = ddiv.Tables("div").Rows(0).Item("div_code")
            Me.cb_sledcode.Text = ddiv.Tables("div").Rows(0).Item("divdesc")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIBDS_PR WBM WHERE INOUTTYPE = 'A'"
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
            Me.tb_ticketno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        'Me.tb_ticketno.Text = 61000005
        'Me.tb_FIRSTQTY.Text = 1234
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_iprib"
        cmd.CommandType = CommandType.StoredProcedure
        'Try
        Me.cb_inouttype.Text = "A"
        Me.tb_sap_doc.Text = "QI"
        Me.cb_sap_docu_type.Text = "QI"

        cmd.Parameters.Add(New OracleParameter("pINOUTTYPE", OracleDbType.Varchar2)).Value = Me.cb_inouttype.Text
        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
        cmd.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
        cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = DBNull.Value
        'If IsDBNull(Me.tb_container.Text) Then
        'Me.tb_container.Text = ""
        'Else
        '   cmd.Parameters.Add(New OracleParameter("pCONTAINERNO", OracleDbType.Varchar2)).Value = Me.tb_container.Text
        'End If
        cmd.Parameters.Add(New OracleParameter("pTRANSPORTER", OracleDbType.Varchar2)).Value = DBNull.Value
        If cb_inouttype.Text = "T" Then
            cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = "224010 001"
            cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = "224010 001 0554"
            cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = "Other Supplier"
        Else
            cmd.Parameters.Add(New OracleParameter("pACCOUNTCODE", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text
            cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
            cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
            cmd.Parameters.Add(New OracleParameter("ppSLEDCODE", OracleDbType.Varchar2)).Value = sledfillup
            cmd.Parameters.Add(New OracleParameter("ppSLEDDESC", OracleDbType.Varchar2)).Value = suppfillup
        End If
        cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = CInt(Me.Tb_intitemcode.Text)
        cmd.Parameters.Add(New OracleParameter("pITEMCODE", OracleDbType.Varchar2)).Value = Me.tb_itemdesc.Text
        cmd.Parameters.Add(New OracleParameter("pITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text

        If Me.cb_inouttype.Text <> "T" Then
            cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt("141325")
            cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = "Dummy"
            cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = "00000"
        ElseIf Me.cb_inouttype.Text = "T" Then

            cmd.Parameters.Add(New OracleParameter("pFRINTITEM", OracleDbType.Int32)).Value = CInt(Me.tb_frintitem.Text)
            cmd.Parameters.Add(New OracleParameter("pFRITEM", OracleDbType.Varchar2)).Value = Me.tb_fritemdesc.Text
            cmd.Parameters.Add(New OracleParameter("pFRITEMDESC", OracleDbType.Varchar2)).Value = Me.cb_fritem.Text()
        End If
        cmd.Parameters.Add(New OracleParameter("pNUMBEROFPCS", OracleDbType.Int32)).Value = Me.tb_numberofpcs.Text
        cmd.Parameters.Add(New OracleParameter("pDRIVERCODE", OracleDbType.Varchar2)).Value = Me.tb_DRIVERNAM.Text
        cmd.Parameters.Add(New OracleParameter("pDRIVERNAM", OracleDbType.Varchar2)).Value = Me.cb_dcode.Text
        cmd.Parameters.Add(New OracleParameter("pNATIONALITY", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("pDRIVINGLICNO", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("pFIRSTQTY", OracleDbType.Decimal)).Value = CDec(Me.tb_FIRSTQTY.Text)
        cmd.Parameters.Add(New OracleParameter("pSECONDQTY", OracleDbType.Decimal)).Value = CDec(Trim(Me.tb_SECONDQTY.Text))

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
        Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("pDATEIN", OracleDbType.Date)).Value = dtin 'Convert.ToDateTime(Me.tb_DATEIN.Text)
        cmd.Parameters.Add(New OracleParameter("pTIMEIN", OracleDbType.Varchar2)).Value = Me.tb_TIMEIN.Text

        Dim dto As Date = FormatDateTime(Me.tb_DATEOUT.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("pDATEOUT", OracleDbType.Date)).Value = dto
        cmd.Parameters.Add(New OracleParameter("pTIMOUT", OracleDbType.Varchar2)).Value = Me.tb_TIMOUT.Text
        cmd.Parameters.Add(New OracleParameter("pREMARKS", OracleDbType.Varchar2)).Value = Me.tb_comments.Text
        cmd.Parameters.Add(New OracleParameter("pAPPDATE0", OracleDbType.Date)).Value = Today
        cmd.Parameters.Add(New OracleParameter("pAPPDATE1", OracleDbType.Date)).Value = Today
        cmd.Parameters.Add(New OracleParameter("pFIELD1", OracleDbType.Varchar2)).Value = glbvar.userid
        cmd.Parameters.Add(New OracleParameter("pSTATUS", OracleDbType.Varchar2)).Value = 1
        cmd.Parameters.Add(New OracleParameter("pFIELD2", OracleDbType.Varchar2)).Value = glbvar.userid
        cmd.Parameters.Add(New OracleParameter("pprice", OracleDbType.Decimal)).Value = CDec(Me.tb_PRICETON.Text)
        cmd.Parameters.Add(New OracleParameter("ptotprice", OracleDbType.Decimal)).Value = CDec(Me.tb_TOTALPRICE.Text)
        cmd.Parameters.Add(New OracleParameter("pprlist", OracleDbType.Decimal)).Value = CDec(Me.tb_prlist.Text)
        cmd.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Direction = ParameterDirection.Output
        If cb_inouttype.Text = "I" Then


            cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
            cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "A" Then


            cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
            cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "O" Then

            cmd.Parameters.Add(New OracleParameter("psdocintype", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psdocouttype", OracleDbType.Varchar2)).Value = Me.tb_sap_doc.Text
        End If
        If cb_inouttype.Text = "I" Then


            cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = glbvar.EKORG
            cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = glbvar.EKGRP
            cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "A" Then


            cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = glbvar.EKORG
            cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = glbvar.EKGRP
            cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = DBNull.Value
        ElseIf cb_inouttype.Text = "O" Then


            cmd.Parameters.Add(New OracleParameter("psEKORG", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psEKGRP", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("psVKORG", OracleDbType.Varchar2)).Value = glbvar.VKORG
            cmd.Parameters.Add(New OracleParameter("psVTWEG", OracleDbType.Varchar2)).Value = glbvar.VTWEG
        End If

        cmd.Parameters.Add(New OracleParameter("psVBELNS", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psVBELND", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psVBELNI", OracleDbType.Varchar2)).Value = DBNull.Value
        cmd.Parameters.Add(New OracleParameter("psorderno", OracleDbType.Varchar2)).Value = Me.tb_orderno.Text
        cmd.Parameters.Add(New OracleParameter("pdeliveryno", OracleDbType.Varchar2)).Value = Me.tb_dsno.Text
        cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
        cmd.Parameters.Add(New OracleParameter("pitmno", OracleDbType.Varchar2)).Value = Me.tb_itmno.Text
        cmd.Parameters.Add(New OracleParameter("ptransportcharges", OracleDbType.Varchar2)).Value = Me.Tb_transp.Text
        cmd.Parameters.Add(New OracleParameter("ppenalty", OracleDbType.Varchar2)).Value = Me.Tb_penalty.Text
        cmd.Parameters.Add(New OracleParameter("pmachinecharges", OracleDbType.Varchar2)).Value = Me.Tb_eqpchrgs.Text
        cmd.Parameters.Add(New OracleParameter("plabourcharges", OracleDbType.Varchar2)).Value = Me.Tb_labourcharges.Text
        cmd.Parameters.Add(New OracleParameter("ppono", OracleDbType.Varchar2)).Value = Me.Tb_asno.Text
        cmd.Parameters.Add(New OracleParameter("pagmixno", OracleDbType.Varchar2)).Value = Me.tb_IBDSNO.Text
        cmd.Parameters.Add(New OracleParameter("pconsno", OracleDbType.Varchar2)).Value = Me.Tb_cons_sen_branch.Text
        cmd.Parameters.Add(New OracleParameter("pccic", OracleDbType.Varchar2)).Value = Me.Tb_ccic.Text
        cmd.Parameters.Add(New OracleParameter("pomprice", OracleDbType.Varchar2)).Value = Me.tb_omcustprice.Text
        cmd.Parameters.Add(New OracleParameter("pomsledcode", OracleDbType.Varchar2)).Value = Me.tb_omcustcode.Text
        cmd.Parameters.Add(New OracleParameter("pomsleddesc", OracleDbType.Varchar2)).Value = Me.cb_omcustdesc.Text
        'If cb_ib.Checked = True Then
        '    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
        'ElseIf cb_ib.Checked = False Then
        cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
        'End If
        cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
        cmd.Parameters.Add(New OracleParameter("ppcusttype", OracleDbType.Varchar2)).Value = Me.tb_CUSTTYPE.Text
        cmd.Parameters.Add(New OracleParameter("pptypecode", OracleDbType.Varchar2)).Value = Me.tb_typecode.Text
        cmd.Parameters.Add(New OracleParameter("pptypecatg_pt", OracleDbType.Varchar2)).Value = Me.tb_typecatg_pt.Text
        Dim ndt As Date = FormatDateTime(Me.d_newdate.Text, DateFormat.GeneralDate)
        cmd.Parameters.Add(New OracleParameter("ppostdate", OracleDbType.Date)).Value = ndt
        cmd.Parameters.Add(New OracleParameter("pdivdesc", OracleDbType.Varchar2)).Value = glbvar.gcompname
        cmd.Parameters.Add(New OracleParameter("pgprem", OracleDbType.Varchar2)).Value = DBNull.Value
        Try
            cmd.ExecuteNonQuery()
            'Dim vint As Decimal
            'vint = cmd.Parameters("pINTDOCNO").Value.ToString  'CDec(cmd.Parameters("pINTDOCNO").Value)
            Me.Tb_intdocno.Text = cmd.Parameters("pINTDOCNO").Value.ToString
            'glbvar.multdocno = Me.Tb_intdocno.Text
            'glbvar.multtktno = Me.tb_ticketno.Text
            'glbvar.multinout = Me.cb_inouttype.Text
            conn.Close()
            Me.b_firstwt.Enabled = False
            MsgBox("Record Saved")
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmdupdcr As New OracleCommand()
            Dim cmdupd As New OracleCommand()
            Dim cmdcmt As New OracleCommand()
            cmdupd.Connection = conn
            cmdupdcr.Connection = conn
            cmdcmt.Connection = conn
            cmdupd.CommandText = " update stwbmibds_pr set IBTKTNO = " & Me.tb_ticketno.Text & " where ticketno = " & Me.tb_cfillup.Text
            cmdupdcr.CommandText = " update stwbmibds_pr set CFCREATED = " & Me.tb_cfillup.Text & " where ticketno = " & Me.tb_ticketno.Text
            cmdcmt.CommandText = " commit"
            cmdupd.CommandType = CommandType.Text
            cmdupdcr.CommandType = CommandType.Text
            cmdcmt.CommandType = CommandType.Text
            cmdupd.ExecuteNonQuery()
            cmdupdcr.ExecuteNonQuery()
            cmdcmt.ExecuteNonQuery()
            conn.Close()
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join_pr.insert_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
            cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
            cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
            cmd1.ExecuteNonQuery()
            conn.Close()
            ZINTERBRANCHDETAILSUPDPR()
            Button1.Visible = False
            b_cribpur.Visible = False
            'clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            conn.Close()
        End Try

    End Sub
    Public Sub ZSDCONSIGNFILLUPPR()

        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            Me.b_save.Focus()
        ElseIf Me.tb_sledesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.tb_sledesc.Focus()
        ElseIf Me.cb_itemcode.Text = "" Then
            MsgBox("Select an itemcode")
            Me.cb_itemcode.Focus()
        ElseIf Me.tb_FIRSTQTY.Text = "" Then
            MsgBox(" First Qty cannot be blank")
            'Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '    MsgBox(" Price must be entered ")
        Else
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim saprfcrepos As RfcRepository = saprfcdest.Repository



            'for Document type ZCOR the tb_dsno is mandatory
            'Outside Materials the customer ticket # and date to be made mandatory is manatory ZOMO
            'Inter Branch Consignemet Number from SAP to be stored in. This will become the refernce for receiving branch


            Try
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CONSIGN_FILLUP_PRJ")
                sodnbil.SetValue("ZPRJN", sledfillup)
                sodnbil.SetValue("ZPRJS", suppfillup)
                sodnbil.SetValue("ZREFPO", Tb_asno.Text)
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZTCF")
                ohdrin.SetValue("SALES_ORG", Me.tb_CUSTTYPE.Text)
                ohdrin.SetValue("DISTR_CHAN", Me.tb_typecode.Text)
                ohdrin.SetValue("DIVISION", Me.tb_typecatg_pt.Text)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))
                ohdrin.SetValue("CREATED_BY", glbvar.userid)

                Dim ohdrinx As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_INX")
                ohdrinx.SetValue("DOC_TYPE", "X")
                ohdrinx.SetValue("SALES_ORG", "X")
                ohdrinx.SetValue("DISTR_CHAN", "X")
                ohdrinx.SetValue("DIVISION", "X")
                ohdrinx.SetValue("PURCH_NO_C", "X")
                ohdrinx.SetValue("DOC_DATE", "X")
                Dim scltyp As IRfcStructure = sodnbil.GetStructure("SOCUST_HEAD") 'DLCUST_FIELD 
                scltyp.SetValue("ZZINDS", glbvar.scaletype)

                Dim dlcust As IRfcStructure = sodnbil.GetStructure("DLCUST_FIELD") 'DLCUST_FIELD 
                dlcust.SetValue("ZZTICKET", CLng(Me.tb_ticketno.Text))
                dlcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text)
                dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZINDS", glbvar.scaletype)








                Dim oitmin As IRfcTable = sodnbil.GetTable("ORDER_ITEMS_IN")
                'Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                Dim oitminx As IRfcTable = sodnbil.GetTable("ORDER_ITEMS_INX")
                'Dim itminxstru As IRfcStructure = oitminx.Metadata.LineType.CreateStructure
                Dim orsi As IRfcTable = sodnbil.GetTable("ORDER_SCHEDULES_IN")
                'Dim orsistru As IRfcStructure = orsi.Metadata.LineType.CreateStructure
                Dim orsinx As IRfcTable = sodnbil.GetTable("ORDER_SCHEDULES_INX")
                'Dim orsinxstru As IRfcStructure = orsinx.Metadata.LineType.CreateStructure
                Dim ocin As IRfcTable = sodnbil.GetTable("ORDER_CONDITIONS_IN")
                'Dim ocinstru As IRfcStructure = ocin.Metadata.LineType.CreateStructure
                Dim tdlcf As IRfcTable = sodnbil.GetTable("T_DELCUST_FIELD") 'T_DELCUST_FIELD
                'Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                Dim orp As IRfcTable = sodnbil.GetTable("ORDER_PARTNERS")
                'Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure


                'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    conn.Close()
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        conn.Close()
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                            itmstru.SetValue("ITM_NUMBER", itm)
                            itmstru.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            itmstru.SetValue("PLANT", glbvar.divcd)
                            itmstru.SetValue("STORE_LOC", glbvar.LGORT)
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            itmstru.SetValue("TARGET_QTY", qt)
                            itmstru.SetValue("SALES_UNIT", "TO")
                            itmstru.SetValue("SHIP_POINT", glbvar.VSTEL)
                            oitmin.Append(itmstru)


                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            Dim itminxstru As IRfcStructure = oitminx.Metadata.LineType.CreateStructure
                            itminxstru.SetValue("ITM_NUMBER", itm)
                            itminxstru.SetValue("MATERIAL", "X")
                            itminxstru.SetValue("PLANT", "X")
                            itminxstru.SetValue("STORE_LOC", "X")
                            itminxstru.SetValue("TARGET_QTY", "X")
                            itminxstru.SetValue("SALES_UNIT", "X")
                            itminxstru.SetValue("SHIP_POINT", "X")

                            'itminxstru.SetValue("REF_DOC", "X")
                            'itminxstru.SetValue("REF_DOC_IT", "X")
                            'itminxstru.SetValue("REF_DOC_CA", "X")
                            oitminx.Append(itminxstru)
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            Dim orsistru As IRfcStructure = orsi.Metadata.LineType.CreateStructure
                            orsistru.SetValue("ITM_NUMBER", itm)
                            orsistru.SetValue("SCHED_LINE", sl)
                            'Dim dt As Date = FormatDateTime(Convert.ToDateTime(ORDER_SCHEDULES_IN.Item("REQ_DATE", 0).FormattedValue), DateFormat.ShortDate)
                            'orsistru.SetValue("REQ_DATE", dt)
                            Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            orsistru.SetValue("REQ_QTY", rqty)
                            orsi.Append(orsistru)

                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            Dim orsinxstru As IRfcStructure = orsinx.Metadata.LineType.CreateStructure
                            orsinxstru.SetValue("ITM_NUMBER", itm)
                            'hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                            orsinxstru.SetValue("SCHED_LINE", sl)
                            'orsinxstru.SetValue("UPDATEFLAG", ORDER_SCHEDULES_INX.Item("UPDATEFLAGnx", 0).ToString)
                            'orsinxstru.SetValue("REQ_DATE", ORDER_SCHEDULES_INX.Item("REQ_DATEnx", 0).ToString)
                            orsinxstru.SetValue("REQ_QTY", "X")
                            orsinx.Append(orsinxstru)

                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            Dim ocinstru As IRfcStructure = ocin.Metadata.LineType.CreateStructure
                            ocinstru.SetValue("ITM_NUMBER", itm)
                            'hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                            ocinstru.SetValue("COND_ST_NO", sl)
                            Dim cocn As UInteger = Convert.ToUInt64("00")
                            ocinstru.SetValue("COND_COUNT", cocn)
                            ocinstru.SetValue("COND_TYPE", "ZPR0")
                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            ocinstru.SetValue("COND_VALUE", cval)
                            ocinstru.SetValue("CURRENCY", "SAR")
                            ocin.Append(ocinstru)
                            Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                            tdlcfstru.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            tdlcfstru.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                            'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text)/1000)
                            'tdlcfstru.SetValue("ZZPIPE", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZOM", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZTHICK", 0) ' CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZLEN", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            tdlcfstru.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            tdlcfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTION").ToString()) / 1000)
                            tdlcfstru.SetValue("ZZPACKD", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString()) / 1000)
                            'tdlcfstru.SetValue("ZZUOMOD", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZUOMT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZUOML", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'tdlcfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            tdlcf.Append(tdlcfstru)

                            Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                            orpstru.SetValue("PARTN_ROLE", "AG")
                            'orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text)
                            orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.Remove(4, 1))
                            'check if the customer is a one time customer then add the test else no need.
                            orpstru.SetValue("NAME", Me.cb_sledcode.Text)
                            'orpstru.SetValue("STREET", ORDER_PARTNERS.Rows(0).Cells("STREET").FormattedValue)
                            orpstru.SetValue("COUNTRY", "SA")
                            ''orpstru.SetValue("PO_BOX", ORDER_PARTNERS.Item("PO_BOX", 0).ToString)
                            'orpstru.SetValue("POSTL_CODE", ORDER_PARTNERS.Rows(0).Cells("POSTL_CODE").FormattedValue)
                            orpstru.SetValue("CITY", "Dammam")
                            'orpstru.SetValue("TELEPHONE", ORDER_PARTNERS.Rows(0).Cells("TELEPHONE").FormattedValue)
                            'orpstru.SetValue("FAX_NUMBER", ORDER_PARTNERS.Rows(0).Cells("FAX_NUMBER").FormattedValue)
                            orp.Append(orpstru)


                        Next

                        'oitmin.Append(itmstru)
                        'oitminx.Append(itminxstru)
                        'orsi.Append(orsistru)
                        'orsinx.Append(orsinxstru)
                        'ocin.Append(ocinstru)
                        'tdlcf.Append(tdlcfstru)
                        'orp.Append(orpstru)
                        'oitmin.Append(itmstru)
                        'oitminx.Append(itminxstru)
                    Else
                        Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                        Dim itm As UInteger = Convert.ToUInt64("10")
                        itmstru.SetValue("ITM_NUMBER", itm)
                        'ensure the material number is left padded with zeros.
                        itmstru.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        itmstru.SetValue("PLANT", glbvar.divcd)
                        itmstru.SetValue("STORE_LOC", glbvar.LGORT)
                        Dim qt As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        itmstru.SetValue("TARGET_QTY", qt)
                        itmstru.SetValue("SALES_UNIT", "TO")
                        itmstru.SetValue("SHIP_POINT", glbvar.VSTEL)
                        oitmin.Append(itmstru)

                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        Dim itminxstru As IRfcStructure = oitminx.Metadata.LineType.CreateStructure
                        itminxstru.SetValue("ITM_NUMBER", 10)
                        itminxstru.SetValue("MATERIAL", "X")
                        itminxstru.SetValue("PLANT", "X")
                        itminxstru.SetValue("STORE_LOC", "X")
                        itminxstru.SetValue("TARGET_QTY", "X")
                        itminxstru.SetValue("SALES_UNIT", "X")
                        itminxstru.SetValue("SHIP_POINT", "X")
                        'itminxstru.SetValue("REF_DOC", "X")
                        'itminxstru.SetValue("REF_DOC_IT", "X")
                        'itminxstru.SetValue("REF_DOC_CA", "X")
                        oitminx.Append(itminxstru)
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        Dim orsistru As IRfcStructure = orsi.Metadata.LineType.CreateStructure
                        orsistru.SetValue("ITM_NUMBER", 10)
                        orsistru.SetValue("SCHED_LINE", 1)
                        'Dim dt As Date = FormatDateTime(Convert.ToDateTime(ORDER_SCHEDULES_IN.Item("REQ_DATE", 0).FormattedValue), DateFormat.ShortDate)
                        'orsistru.SetValue("REQ_DATE", dt)
                        Dim rqty As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        orsistru.SetValue("REQ_QTY", rqty)
                        orsi.Append(orsistru)

                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        Dim itn As UInteger = Convert.ToUInt64("10")
                        Dim orsinxstru As IRfcStructure = orsinx.Metadata.LineType.CreateStructure
                        orsinxstru.SetValue("ITM_NUMBER", itn)
                        'hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                        Dim itsl As UInteger = Convert.ToUInt64("001")
                        orsinxstru.SetValue("SCHED_LINE", itsl)
                        'orsinxstru.SetValue("UPDATEFLAG", ORDER_SCHEDULES_INX.Item("UPDATEFLAGnx", 0).ToString)
                        'orsinxstru.SetValue("REQ_DATE", ORDER_SCHEDULES_INX.Item("REQ_DATEnx", 0).ToString)
                        orsinxstru.SetValue("REQ_QTY", "X")
                        orsinx.Append(orsinxstru)

                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        Dim itmoci As UInteger = Convert.ToUInt64("000010")
                        'Dim ocinstru As IRfcStructure = ocin.Metadata.LineType.CreateStructure
                        'ocinstru.SetValue("ITM_NUMBER", itmoci)
                        ''hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                        'Dim cstn As UInteger = Convert.ToUInt64("0001")
                        'ocinstru.SetValue("COND_ST_NO", cstn)
                        'Dim cocn As UInteger = Convert.ToUInt64("00")
                        'ocinstru.SetValue("COND_COUNT", cocn)
                        'ocinstru.SetValue("COND_TYPE", "ZPR0")
                        'Dim cval As Decimal = Convert.ToDecimal(tb_PRICETON.Text)
                        'ocinstru.SetValue("COND_VALUE", cval)
                        'ocinstru.SetValue("CURRENCY", "SAR")
                        'ocin.Append(ocinstru)
                        Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                        tdlcfstru.SetValue("ZZFWGT", CDec(Me.tb_FIRSTQTY.Text) / 1000) 'ZZFWGT
                        tdlcfstru.SetValue("ZZSWGT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        tdlcfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        'tdlcfstru.SetValue("ZZPIPE", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZOM", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZTHICK", 0) ' CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZLEN", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        tdlcfstru.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        tdlcfstru.SetValue("ZZPACKD", CDec(Me.tb_packded.Text) / 1000)
                        'tdlcfstru.SetValue("ZZUOMOD", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZUOMT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZUOML", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'tdlcfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        tdlcf.Append(tdlcfstru)
                        Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                        'Dim orp As IRfcTable = sodnbil.GetTable("ORDER_PARTNERS")
                        'Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                        orpstru.SetValue("PARTN_ROLE", "AG")
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.Remove(4, 1))
                        'check if the customer is a one time customer then add the test else no need.
                        orpstru.SetValue("NAME", Me.cb_sledcode.Text)
                        'orpstru.SetValue("STREET", ORDER_PARTNERS.Rows(0).Cells("STREET").FormattedValue)
                        orpstru.SetValue("COUNTRY", "SA")
                        ''orpstru.SetValue("PO_BOX", ORDER_PARTNERS.Item("PO_BOX", 0).ToString)
                        'orpstru.SetValue("POSTL_CODE", ORDER_PARTNERS.Rows(0).Cells("POSTL_CODE").FormattedValue)
                        orpstru.SetValue("CITY", "Dammam")
                        'orpstru.SetValue("TELEPHONE", ORDER_PARTNERS.Rows(0).Cells("TELEPHONE").FormattedValue)
                        'orpstru.SetValue("FAX_NUMBER", ORDER_PARTNERS.Rows(0).Cells("FAX_NUMBER").FormattedValue)
                        orp.Append(orpstru)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try




                Dim rttbl As IRfcTable = sodnbil.GetTable("RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                sodnbil.Invoke(saprfcdest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))
                ReDim id(rttbl.RowCount - 1)
                ReDim typ(rttbl.RowCount - 1)
                ReDim nmbr(rttbl.RowCount - 1)
                ReDim mesg(rttbl.RowCount - 1)
                ReDim tkt(rttbl.RowCount - 1)
                Dim soercnt As Integer = 0
                DataGridView1.Refresh()
                For l = 0 To rttbl.RowCount - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(l).Cells("TYPE").Value = rttbl(l).Item("Type").GetString() 'err.GetValue("TYPE")
                    If rttbl(l).Item("Type").GetString() = "E" Then
                        soercnt = soercnt + 1
                    End If
                    DataGridView1.Rows(l).Cells("i_d").Value = rttbl(l).Item("ID").GetString() 'err.GetValue("ID")
                    DataGridView1.Rows(l).Cells("NUMBER").Value = rttbl(l).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView1.Rows(l).Cells("MESAGE").Value = rttbl(l).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
                    typ(l) = rttbl(l).Item("Type").GetString()
                    id(l) = rttbl(l).Item("ID").GetString()
                    nmbr(l) = rttbl(l).Item("NUMBER").GetString()
                    mesg(l) = rttbl(l).Item("MESSAGE").GetString()
                    tkt(l) = Me.tb_ticketno.Text
                Next
                'write the code for inserting tcket number.

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                Try
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_errsap_uarr"
                    cmd.CommandType = CommandType.StoredProcedure
                    Dim ptyp As OracleParameter = New OracleParameter(":n1", OracleDbType.Char)
                    ptyp.Direction = ParameterDirection.Input
                    ptyp.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    ptyp.Value = typ

                    Dim pid As OracleParameter = New OracleParameter(":n2", OracleDbType.Varchar2)
                    pid.Direction = ParameterDirection.Input
                    pid.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pid.Value = id

                    Dim pnbr As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                    pnbr.Direction = ParameterDirection.Input
                    pnbr.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pnbr.Value = nmbr

                    Dim pmesg As OracleParameter = New OracleParameter(":n3", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                    ptkt.Direction = ParameterDirection.Input
                    ptkt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    ptkt.Value = tkt

                    cmd.Parameters.Add(ptyp)
                    cmd.Parameters.Add(pid)
                    cmd.Parameters.Add(pnbr)
                    cmd.Parameters.Add(pmesg)
                    cmd.Parameters.Add(ptkt)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message & "From insering into SO Error Table")
                End Try
                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Sales Order # " & sodnbil.GetValue("SALESDOCUMENT").ToString _
                          & vbCrLf & "Delivery Note # " & sodnbil.GetValue("E_DELIVERY").ToString)
                    '& vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                    Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
                    Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
                    freeze_scr()
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    Dim endtime = DateTime.Now.ToString()



                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From Main ZTCF")
            End Try

        End If ' main end if

        ' Add any initialization after the InitializeComponent() call.



    End Sub
    Private Sub ZINTERBRANCHDETAILSUPDPR()

        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.tb_sledesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.tb_sledesc.Focus()
        ElseIf Me.cb_itemcode.Text = "" Then
            MsgBox("Select an itemcode")
            Me.cb_itemcode.Focus()
        ElseIf Me.tb_FIRSTQTY.Text = "" Then
            MsgBox(" First Qty cannot be blank")
            'Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.Tb_cons_sen_branch.Text = "" Then
            MsgBox(" Consignment # is compulsory")
            Me.Tb_cons_sen_branch.Focus()
            'ElseIf Me.tb_itmno.Text = "" Then
            '    MsgBox(" Item # is compulsory")
            '    Me.tb_itmno.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '    MsgBox("Please enter a price")
        Else

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_INTER_BRANCH_UPDATE_PRJ")
                'Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_INTER_BRANCH_UPDATE")


                Dim grcst As IRfcStructure = pogrir.GetStructure("I_INTERBRANCH_HEAD")
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                sql = "Select div_code,divdesc from mdivision where DIVTYPE = 'BR'"
                da = New OracleDataAdapter(sql, conn)
                Dim ddiv As New DataSet

                Try
                    da.TableMappings.Add("Table", "div")
                    da.Fill(ddiv)
                    conn.Close()
                    recbr = ddiv.Tables("div").Rows(0).Item("div_code")
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
                ' Create field in transaction taable and bring from hremployee table
                'grcst.SetValue("ZZINDS", "2") 'Buyer Name
                'grcst.SetValue("MANDT", "200")
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                'Commented Praveen 17/03/2015
                'grcst.SetValue("VBELN", Me.Tb_cons_sen_branch.Text) 'SO #
                'grcst.SetValue("MBLNR", "0000000455") 'Material Doc# - Blank in QI
                grcst.SetValue("SENDING_PLANT", tb_sledesc.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("RECEIVING_PLANT", recbr) 'Material Doc# - Blank in QI
                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("BUKRS", glbvar.cmpcd) 'Material Doc# - Blank in QI
                grcst.SetValue("BSART", "QI") 'Material Doc# - Blank in QI
                'grcst.SetValue("AEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'grcst.SetValue("ERNAM", "AKMENON") 'Name of Person who Created the Object
                grcst.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("LIFNR", tb_sledesc.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("EKORG", glbvar.EKORG) 'Material Doc# - Blank in QI
                grcst.SetValue("EKGRP", glbvar.EKGRP) 'Material Doc# - Blank in QI
                'grcst.SetValue("BEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTRANSCHR", CDec(Me.Tb_transp.Text))
                grcst.SetValue("ZZPENALTY", CDec(Me.Tb_penalty.Text))
                grcst.SetValue("ZZMACHARGE", CDec(Me.Tb_eqpchrgs.Text))
                grcst.SetValue("ZZLABCHAR", CDec(Me.Tb_labourcharges.Text))
                grcst.SetValue("ZREMARKS", Me.tb_comments.Text)
                grcst.SetValue("LGORT", glbvar.LGORT)
                'grcst.SetValue("CREATED_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("CREATED_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))
                grcst.SetValue("ZPRJN", sledfillup)
                grcst.SetValue("ZPRJS", suppfillup)
                grcst.SetValue("ZREFPO", Me.Tb_asno.Text)
                'Commented Praveen 17/03/2015
                'If cb_ib.Checked = True Then
                '    grcst.SetValue("VBELN_COMPLETE", "X")
                'ElseIf cb_ib.Checked = False Then
                '    grcst.SetValue("VBELN_COMPLETE", "")
                'End If
                'grcst.SetValue("ZZLABCHAR", Me.Tb_labourcharges.Text) for store charges
                'commented on 10.03.2015 after discussion with vignesh
                'Changed Praveen 17/03/2015 item level for inter branch direct purchase
                Dim mixtab As IRfcTable = pogrir.GetTable("T_INTERBRANCH_CONSIG")
                Dim mixstr As IRfcStructure = mixtab.Metadata.LineType.CreateStructure
                mixstr.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                'mixstr.SetValue("EBELN", Me.Tb_cons_sen_branch.Text)
                'mixstr.SetValue("EBELP", 10)
                'mixstr.SetValue("MENGE", Convert.ToDecimal(tb_QTY.Text) / 1000)

                mixstr.SetValue("VBELN", Me.Tb_cons_sen_branch.Text)
                If cb_ib.Checked = True Then
                    mixstr.SetValue("COMPLETE", "X")
                ElseIf cb_ib.Checked = False Then
                    mixstr.SetValue("COMPLETE", "")
                End If

                mixtab.Append(mixstr)
                'Next
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.chk_multi"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    conn.Close()
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Long)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        conn.Close()
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            'Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                            'Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            ''hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            'poitmu.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                            'poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            'poitmu.SetValue("PLANT", glbvar.divcd)
                            'poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                            'poitmu.SetValue("MATL_GROUP", "01")
                            'Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())/1000
                            ''poitmu.SetValue("QUANTITY", qt)
                            'poitmu.SetValue("PO_UNIT", "TO")
                            'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                            'Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
                            'poitmu.SetValue("NET_PRICE", cval)
                            'poitm.Append(poitmu)

                            'Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                            'Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            'poitmuX.SetValue("PO_ITEM", "X")
                            'poitmuX.SetValue("MATERIAL", "X")
                            'poitmuX.SetValue("PLANT", "X")
                            'poitmuX.SetValue("STGE_LOC", "X")
                            'poitmuX.SetValue("MATL_GROUP", "X")
                            ''poitmuX.SetValue("QUANTITY", "X")
                            'poitmuX.SetValue("PO_UNIT", "X")
                            'poitmuX.SetValue("PO_UNIT_ISO", "X")
                            'poitmuX.SetValue("NET_PRICE", "X")
                            'poitmx.Append(poitmuX)

                            'Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                            'Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            'pozfstru.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                            'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString())/1000)
                            'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text)/1000)
                            'pozf.Append(pozfstru)

                            'Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                            'Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                            'gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                            'gpozfstru.SetValue("EBELP", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString()) ' Item Number of Purchasing Document
                            'gpozfstru.SetValue("MATNR", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())) 'Material Number
                            'Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())/1000
                            'gpozfstru.SetValue("MENGE", gt) 'Quantity
                            'gpozf.Append(gpozfstru)
                            Dim pozf As IRfcTable = pogrir.GetTable("T_INTERBRANCH_ITEM")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            'pozfstru.SetValue("MANDT", "200")
                            pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                            pozfstru.SetValue("EBELP", itm)
                            pozfstru.SetValue("MATNR", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            pozfstru.SetValue("WERKS", glbvar.divcd)
                            pozfstru.SetValue("LGORT", glbvar.LGORT)
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            pozfstru.SetValue("MENGE", qt)
                            pozfstru.SetValue("MATKL", "01")
                            pozfstru.SetValue("MEINS", "TO")
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            Dim sapded As Decimal = 0.0
                            sapded = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTION").ToString()) / 1000 + CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString()) / 1000
                            pozfstru.SetValue("ZZDECT", sapded)
                            Dim sapgrwt As Decimal = 0.0
                            sapgrwt = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000 + sapded
                            pozfstru.SetValue("ZZGROSSWT", sapgrwt)
                            pozfstru.SetValue("ZZFTUOM", "TO")
                            pozfstru.SetValue("ZZSECUOM", "TO")
                            'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                            'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                            'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                            'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                            'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                            'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                            'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                            'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
                            pozfstru.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
                            pozf.Append(pozfstru)

                        Next
                    Else
                        'Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                        'Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        ''hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        'poitmu.SetValue("PO_ITEM", 10) 'CDec(tb_itmno.Text))
                        'poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        'poitmu.SetValue("PLANT", glbvar.divcd)
                        'poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                        'poitmu.SetValue("MATL_GROUP", "01")
                        ''poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text)/1000)
                        'poitmu.SetValue("PO_UNIT", "MT")
                        'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        'poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text))
                        'poitm.Append(poitmu)

                        'Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                        'Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        'poitmuX.SetValue("PO_ITEM", 10)
                        'poitmuX.SetValue("MATERIAL", "X")
                        'poitmuX.SetValue("PLANT", "X")
                        'poitmuX.SetValue("STGE_LOC", "X")
                        'poitmuX.SetValue("MATL_GROUP", "X")
                        ''poitmuX.SetValue("QUANTITY", "X")
                        'poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        'poitmuX.SetValue("NET_PRICE", "X")
                        'poitmx.Append(poitmuX)

                        Dim pozf As IRfcTable = pogrir.GetTable("T_INTERBRANCH_ITEM")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        'pozfstru.SetValue("MANDT", "200")
                        pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                        pozfstru.SetValue("EBELP", 10)
                        pozfstru.SetValue("MATNR", Me.tb_itemdesc.Text)
                        pozfstru.SetValue("WERKS", recbr)
                        pozfstru.SetValue("LGORT", "1000")
                        pozfstru.SetValue("MENGE", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        pozfstru.SetValue("MATKL", "01")
                        pozfstru.SetValue("MEINS", "TO")
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        Dim saphgrwt As Decimal = 0.0
                        saphgrwt = Convert.ToDecimal(tb_QTY.Text) / 1000 + CDec(Me.tb_DEDUCTIONWT.Text) / 1000
                        pozfstru.SetValue("ZZGROSSWT", saphgrwt)
                        pozfstru.SetValue("ZZFTUOM", "TO")
                        pozfstru.SetValue("ZZSECUOM", "TO")
                        'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                        'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                        'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                        'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                        'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                        'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                        'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                        'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
                        pozfstru.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
                        pozf.Append(pozfstru)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try
                '                Dim poacc As IRfcTable = pogrir.GetTable("POACCOUNT")
                Dim poerr As IRfcTable = pogrir.GetTable("T_RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                pogrir.Invoke(dest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Purchase FM " & Convert.ToString((ed - st)))

                ReDim id(poerr.RowCount - 1)
                ReDim typ(poerr.RowCount - 1)
                ReDim nmbr(poerr.RowCount - 1)
                ReDim mesg(poerr.RowCount - 1)
                ReDim tkt(poerr.RowCount - 1)

                Dim poercnt As Integer = 0
                DataGridView1.Refresh()
                For j = 0 To poerr.RowCount - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(j).Cells("TYPE").Value = poerr(j).Item("Type").GetString()
                    If poerr(j).Item("Type").GetString() = "E" Then
                        poercnt = poercnt + 1
                    End If
                    DataGridView1.Rows(j).Cells("I_D").Value = poerr(j).Item("ID").GetString() 'err.GetValue("I_D")
                    DataGridView1.Rows(j).Cells("NUMBER").Value = poerr(j).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView1.Rows(j).Cells("MESAGE").Value = poerr(j).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
                    typ(j) = poerr(j).Item("Type").GetString()
                    id(j) = poerr(j).Item("ID").GetString()
                    nmbr(j) = poerr(j).Item("NUMBER").GetString()
                    mesg(j) = poerr(j).Item("MESSAGE").GetString()
                    tkt(j) = Me.tb_ticketno.Text
                Next

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                Try
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_errsap_uarr"
                    cmd.CommandType = CommandType.StoredProcedure
                    Dim ptyp As OracleParameter = New OracleParameter(":n1", OracleDbType.Char)
                    ptyp.Direction = ParameterDirection.Input
                    ptyp.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    ptyp.Value = typ

                    Dim pid As OracleParameter = New OracleParameter(":n2", OracleDbType.Varchar2)
                    pid.Direction = ParameterDirection.Input
                    pid.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pid.Value = id

                    Dim pnbr As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                    pnbr.Direction = ParameterDirection.Input
                    pnbr.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pnbr.Value = nmbr

                    Dim pmesg As OracleParameter = New OracleParameter(":n3", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n3", OracleDbType.Int64)
                    ptkt.Direction = ParameterDirection.Input
                    ptkt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    ptkt.Value = tkt

                    cmd.Parameters.Add(ptyp)
                    cmd.Parameters.Add(pid)
                    cmd.Parameters.Add(pnbr)
                    cmd.Parameters.Add(pmesg)
                    cmd.Parameters.Add(ptkt)
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Ticket # " & Me.tb_ticketno.Text & " Updated")
                    '     & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    Me.tb_sapord.Text = Me.tb_ticketno.Text
                    freeze_scr()
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = Me.tb_ticketno.Text
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CLng(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QI")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Sub b_multi_Click(sender As Object, e As EventArgs) Handles b_multi.Click
        Try
            If tmode = 2 Then
                glbvar.vntwt = CInt(Me.tb_QTY.Text)
                glbvar.vfwt = CInt(Me.tb_FIRSTQTY.Text)
                glbvar.vswt = CInt(Me.tb_SECONDQTY.Text)
                glbvar.multdocno = Me.Tb_intdocno.Text
                glbvar.inout = Me.cb_inouttype.Text
                glbvar.multkt = Me.tb_ticketno.Text
                glbvar.sapdocmulti = Me.tb_sap_doc.Text
                glbvar.gsapordno = Me.tb_sapord.Text
                glbvar.gsapdocno = Me.tb_sapdocno.Text
                glbvar.gsapinvno = Me.tb_sapinvno.Text
                glbvar.gmultival = Me.cb_multival.Checked
                Dim frm As New multi_itm_pr
                frm.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub
    ReadOnly ValidChars As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"

    Private Sub txtOLDBuildingName_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) _
    Handles tb_vehicleno.KeyPress

        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
                    OrElse e.KeyChar = Convert.ToChar(Keys.Back))

    End Sub

   
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If CLng(Me.tb_ticketno.Text) <> 0 Then
            If Me.Tb_intdocno.Text = "" Then
                MsgBox("Ticket not saved")
            Else
                If tb_sapord.Text = "" AndAlso tb_sapdocno.Text = "" AndAlso tb_sapinvno.Text = "" Then
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim cmdupd As New OracleCommand()
                    Dim cmdcmt As New OracleCommand()
                    cmdupd.Connection = conn
                    cmdcmt.Connection = conn
                    cmdupd.CommandText = " update stwbmibds_pr set VBELNS = 'Cancelled',VBELND = 'Cancelled',VBELNI = 'Cancelled' where ticketno = " & Me.tb_ticketno.Text
                    cmdcmt.CommandText = " commit"
                    cmdupd.CommandType = CommandType.Text
                    cmdcmt.CommandType = CommandType.Text
                    cmdupd.ExecuteNonQuery()
                    cmdcmt.ExecuteNonQuery()
                    Me.tb_sapord.Text = "Cancelled"
                    Me.tb_sapdocno.Text = "Cancelled"
                    Me.tb_sapinvno.Text = "Cancelled"
                    freeze_scr()
                    conn.Close()
                    MsgBox("Ticket Cancelled")
                Else
                    MsgBox("Documents Already created, cannot cancel")
                End If
            End If
        Else
            MsgBox("Enter Ticket Number")
        End If
    End Sub
End Class




















































































































































































































































































































































































































