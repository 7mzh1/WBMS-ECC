﻿Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector

Public Class VALUATIONS
    Private comm As New CommManager()
    Private transType As String = String.Empty
    Dim constr, constrd As String
    Dim conn As New OracleConnection
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dpr As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Dim sql As String
    Public ds As New DataSet
    Dim ds1 As New DataSet
    Dim tmode As Integer
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim omdasld As New OracleDataAdapter
    Dim omdssld As New DataSet
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim dadoc As New OracleDataAdapter
    Dim dsdoc As New DataSet
    Dim dfitm As New DataSet
    Dim dadr As New OracleDataAdapter
    Dim dsdr As New DataSet
    Dim id() As String
    Dim typ() As String
    Dim nmbr() As Integer
    Dim mesg() As String
    Dim tkt() As Integer

    Private Sub WBMS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
        Catch ex As Exception
            MsgBox(ex.Message)
            comm.ClosePort()

        End Try
        comm.ClosePort()

    End Sub
    Private Sub WBMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd
        'cmbloading()
        tmode = 0
        comm.CurrentTransmissionType = CommManager.TransmissionType.Text
        Me.tb_FIELD1.Text = glbvar.userid
        tb_edittktn.Hide()
        b_edittktn.Hide()
        glbvar.scaletype = "2"
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
    End Sub
    Private Sub b_newveh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_newveh.Click
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
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
            Me.cb_sledcode.Text = "Other Supplier"
            Me.tb_sledesc.Text = "0001000000"
            Me.tb_itemdesc.Text = "00000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.tb_sap_doc.Text = "QD"
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

    Private Sub b_outveh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_outveh.Click
        unfreeze_scr()
        clear_scr()
        Me.tb_DATEIN.Text = Today.Date
        Me.tb_DATEOUT.Text = Today.Date
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
            Me.cb_sledcode.Text = "Other Customer"
            Me.tb_sledesc.Text = "0001000000"
            Me.tb_itemdesc.Text = "00000"
            Me.Tb_intitemcode.Text = 141325
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.tb_sap_doc.Text = "ZTBV"
            Me.cb_sap_docu_type.Text = "Cash Sales"
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
            'cb_itemcode_SelectedIndexChanged(sender, e)
            Me.tb_SECONDQTY.Text = Me.rtbDisplay.Text
            Me.tb_DATEOUT.Text = Today.Date
            Me.tb_TIMOUT.Text = Now.ToShortTimeString
            Dim sq As Integer = Convert.ToDecimal(Trim(Me.tb_SECONDQTY.Text))
            If cb_inouttype.Text = "I" Then
                Me.tb_QTY.Text = CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text)
            ElseIf cb_inouttype.Text = "O" Then
                If Me.tb_sap_doc.Text <> "ZTRE" Then
                    Me.tb_QTY.Text = sq - CDec(Me.tb_FIRSTQTY.Text) - CDec(Me.tb_DEDUCTIONWT.Text)
                Else
                    Me.tb_QTY.Text = CDec(Me.tb_FIRSTQTY.Text) - sq - CDec(Me.tb_DEDUCTIONWT.Text)
                End If
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.docmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.docmst"
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
            '    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
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
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT" _
                         & " from STWBMIBDS where TICKETNO = " & Me.tb_ticketno.Text _
                         & " and status in (1,2,3)"
                    clear_scr()
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
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                            Me.cb_ib.Checked = True
                        ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                            Me.cb_ib.Checked = False
                        End If
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                            Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                        End If

                        'update data table in case of multiple items.
                        Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                        & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                        & " from(STWBMIBDS_MULTI)" _
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
                            Me.B_PO.Visible = False
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
                        cmd1.CommandText = "curspkg_join.insert_lock"
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                        cmd1.ExecuteNonQuery()
                        'conn.Close()
                        conn.Close()

                    Else
                        MsgBox("No Records Found for this ticket #", MsgBoxStyle.Information)
                        'Me.tb_ticketno.Focus()
                        Me.b_edit.Focus()
                    End If

                    If Me.tb_sap_doc.Text = "QN" Then
                        Me.Tb_asno.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QI" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        Me.cb_ib.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIM" Then
                        Me.Tb_cons_sen_branch.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIX" Then
                        Me.Tb_cons_sen_branch.Visible = True
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
                    Else
                        Me.Tb_asno.Visible = False
                        Me.Tb_cons_sen_branch.Visible = False
                        Me.tb_IBDSNO.Visible = False
                        Me.tb_orderno.Visible = False
                        Me.tb_dsno.Visible = False
                        Me.cb_ib.Visible = False
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
        sql = "SELECT nvl(ticketno,0) ticketno FROM WBMLOCK WHERE TICKETNO = " & Me.tb_ticketno.Text
        Dim dalk = New OracleDataAdapter(sql, conn)
        Dim dslk As New DataSet
        dalk.Fill(dslk)
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
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG" _
                         & " from STWBMIBDS where TICKETNO = " & Me.tb_ticketno.Text _
                         & " and status in (1,2,3)"
                    clear_scr()
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
                        If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                            Me.cb_ib.Checked = True
                        ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                            Me.cb_ib.Checked = False
                        End If


                        'update data table in case of multiple items.
                        Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                        & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                        & " from(STWBMIBDS_MULTI)" _
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
                            Me.B_PO.Visible = False
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
                        cmd1.CommandText = "curspkg_join.insert_lock"
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                        cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                        cmd1.ExecuteNonQuery()
                        'conn.Close()
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
                        Me.cb_ib.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QIM" Then
                        Me.Tb_cons_sen_branch.Visible = True
                        Me.l_cons.Visible = True
                    ElseIf Me.tb_sap_doc.Text = "QMX" Then
                        Me.b_mixmat.Visible = True
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
                    ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                        Me.tb_orderno.Visible = True
                        Me.l_so.Visible = True
                    Else
                        Me.Tb_asno.Visible = False
                        Me.Tb_cons_sen_branch.Visible = False
                        Me.tb_IBDSNO.Visible = False
                        Me.tb_orderno.Visible = False
                        Me.tb_dsno.Visible = False
                        Me.cb_ib.Visible = False
                        l_agmix.Visible = False
                        l_cons.Visible = False
                        l_dsno.Visible = False
                        l_pono.Visible = False
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
                'Try
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
                    cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                    cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
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
                If cb_ib.Checked = True Then
                    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
                ElseIf cb_ib.Checked = False Then
                    cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = ""
                End If
                cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
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
                    Dim cmd1 As New OracleCommand
                    cmd1.Connection = conn
                    cmd1.Parameters.Clear()
                    cmd1.CommandText = "curspkg_join.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
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
        ElseIf tmode = 2 Then
            'Dim constr As String = My.Settings.Item("ConnString")
            Dim abc
            Dim chk = 0
            abc = glbvar.p_mitem
            If IsNothing(abc) Then
                chk = 1
            Else
                For i = 0 To glbvar.p_mitem.Count - 1
                    abc = glbvar.p_mitem(i)
                    If abc = 0 Then
                        chk = 1
                    End If
                Next
            End If
            If Me.tb_sap_doc.Text = "QIX" And chk > 0 Or Me.tb_sap_doc.Text = "QMX" And chk > 0 Then
                MsgBox("Check Mix Material Details")
                glbvar.vntwt = CInt(Me.tb_QTY.Text)
                glbvar.multdocno = Me.Tb_intdocno.Text
                glbvar.inout = Me.cb_inouttype.Text
                glbvar.multkt = Me.tb_ticketno.Text
                glbvar.sapdocmulti = Me.tb_sap_doc.Text
                Dim frm As New MIX
                frm.Show()
            Else
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
                    cmd.Parameters.Add(New OracleParameter("pSLEDCODE", OracleDbType.Varchar2)).Value = Me.tb_sledesc.Text
                    cmd.Parameters.Add(New OracleParameter("pSLEDDESC", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text
                    cmd.Parameters.Add(New OracleParameter("pINTITEMCODE", OracleDbType.Int32)).Value = Me.Tb_intitemcode.Text
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
                    If cb_ib.Checked = True Then
                        cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = "X"
                    ElseIf cb_ib.Checked = False Then
                        cmd.Parameters.Add(New OracleParameter("pcomflg", OracleDbType.Varchar2)).Value = ""
                    End If
                    cmd.Parameters.Add(New OracleParameter("pdocprint", OracleDbType.Varchar2)).Value = Me.tb_docprint.Text
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    If itmalloc = True Then
                        ReDim glbvar.pindocn(glbvar.itmcde.Count - 1)
                        ReDim glbvar.ptktno(glbvar.itmcde.Count - 1)
                        ReDim glbvar.pino(glbvar.itmcde.Count - 1)
                        'Dim i As Integer
                        For n = 0 To glbvar.itmcde.Count - 1
                            glbvar.pindocn(n) = CInt(Me.Tb_intdocno.Text)
                            glbvar.ptktno(n) = CInt(Me.tb_ticketno.Text)
                            glbvar.pino(n) = Me.cb_inouttype.Text
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
                        'conn.Close()
                    End If
                    If Me.tb_sap_doc.Text = "QIX" Or Me.tb_sap_doc.Text = "QMX" Then

                        Dim a = glbvar.p_mitem
                        If IsNothing(a) Then
                            MsgBox("Enter Mix Material Details")
                            'Dim frm As New MIX
                            'frm.Show()
                        Else
                            ReDim glbvar.pindocn(glbvar.p_mitem.Count - 1)
                            ReDim glbvar.ptktno(glbvar.p_mitem.Count - 1)
                            ReDim glbvar.psapdoccode(glbvar.p_mitem.Count - 1)
                            'Dim i As Integer
                            For n = 0 To glbvar.p_mitem.Count - 1
                                glbvar.pindocn(n) = CInt(Me.Tb_intdocno.Text)
                                glbvar.ptktno(n) = CInt(Me.tb_ticketno.Text)
                                glbvar.psapdoccode(n) = Me.tb_sap_doc.Text
                            Next
                            conn = New OracleConnection(constr)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.Connection = conn
                            Try
                                cmd.Parameters.Clear()
                                cmd.CommandText = "gen_iwb_dsd.gen_wbms_mixarr"
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
                                cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
                                cmd.Parameters.Add(New OracleParameter("errmsg", OracleDbType.Varchar2)).Direction = ParameterDirection.Output
                                cmd.ExecuteNonQuery()
                                'multi_itm.DataGridView1.Rows.Clear()
                                'cmd.Parameters.Clear()
                                'clear_scr()
                                glbvar.p_mpono = Nothing
                                glbvar.p_mitem = Nothing
                                glbvar.p_mqty = Nothing
                                glbvar.p_mcomflg = Nothing
                            Catch ex As Exception
                                MsgBox(ex.Message.ToString)
                            End Try
                        End If
                    End If
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
                    cmd1.CommandText = "curspkg_join.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                    cmd1.Parameters.Add(New OracleParameter("pVEHICLENO", OracleDbType.Varchar2)).Value = Me.tb_vehicleno.Text
                    cmd1.Parameters.Add(New OracleParameter("pINTDOCNO", OracleDbType.Int32)).Value = CInt(Me.Tb_intdocno.Text)
                    cmd1.ExecuteNonQuery()
                    conn.Close()
                End Try
            End If 'QMX
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
        'conn.Close()
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
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_mixarr"
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
        tb_searchbyno.Focus()
    End Sub

    Private Sub cb_sledcode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_sledcode.SelectedIndexChanged
        If Me.cb_sledcode.SelectedIndex <> -1 Then
            Me.tb_sledesc.Text = Me.cb_sledcode.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_sledesc.Text & "'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
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
                        Dim tdate = CDate(Today.Date).Day.ToString("D2")
                        Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                        Dim tyear = CDate(Today.Date).Year
                        Dim docdate = tyear & tmonth & tdate
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
                            Me.tb_PRICETON.Text = temppt
                            Me.tb_prlist.Text = temppr
                        End If
                    End If
                End If
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
                Dim expression As String = "ITEMCODE = '" & Me.tb_fritemdesc.Text & "'" & ""
                foundrow = dfitm.Tables("itm").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the item")
                Else
                    For m = 0 To foundrow.Count - 1
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
        clear_scr()
        comm.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()
    End Sub

    Private Sub b_print1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b_print1.Click
        Try
            glbvar.vintdocno = Me.Tb_intdocno.Text
            If Me.cb_inouttype.Text = "T" Then
                'STFSTWT.Show()
                'STFSTWT.Close()
            Else
                'frstwt.Show()
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
                'STSCNDWT.Show()
                'STSCNDWT.Close()
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
                glbvar.multdocno = Me.Tb_intdocno.Text
                glbvar.inout = Me.cb_inouttype.Text
                glbvar.multkt = Me.tb_ticketno.Text
                glbvar.sapdocmulti = Me.tb_sap_doc.Text
                glbvar.gsapordno = Me.tb_sapord.Text
                glbvar.gsapdocno = Me.tb_sapdocno.Text
                glbvar.gsapinvno = Me.tb_sapinvno.Text
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
        b_mixmat.Visible = False
        Button1.Visible = False
        B_PO.Visible = False
        tb_edittktn.Hide()
        b_edittktn.Hide()
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        Me.cb_inouttype.Text = ""
        If Me.tb_ticketno.Text <> "" AndAlso Me.Tb_intdocno.Text <> "" Then
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.Parameters.Clear()
            cmd1.CommandText = "curspkg_join.delete_lock"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmd1.ExecuteNonQuery()
        End If
        conn.Close()
        Me.tb_ticketno.Text = "0"
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
        Me.tb_oth_ven_cust.Text = ""
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
        Me.cb_ib.Checked = False
        Me.tb_ticketno.Enabled = False
        Me.l_dsno.Visible = False
        Me.l_so.Visible = False
        Me.l_agmix.Visible = False
        Me.l_pono.Visible = False
        Me.l_cons.Visible = False
        Me.Tb_asno.Visible = False
        Me.tb_IBDSNO.Visible = False
        Me.tb_orderno.Visible = False
        Me.tb_dsno.Visible = False
        Me.Tb_cons_sen_branch.Visible = False
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
        tb_edittktn.Enabled = False
        b_edittktn.Enabled = False
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        Me.cb_inouttype.Enabled = False
        Me.tb_ticketno.Enabled = False
        Me.tb_container.Enabled = False
        Me.tb_vehicleno.Enabled = False
        Me.tb_transporter.Enabled = False
        Me.tb_sledesc.Enabled = False
        Me.tb_itemdesc.Enabled = False
        Me.tb_operatorid.Enabled = False
        Me.tb_numberofpcs.Enabled = False
        Me.tb_DRIVERNAM.Enabled = False
        Me.tb_NATIONALITY.Enabled = False
        Me.tb_DRIVINGLICNO.Enabled = False
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
        Me.cb_omcustdesc.Enabled = False
        Me.cb_sap_docu_type.Enabled = False
        Me.cb_sledcode.Enabled = False
        Me.tb_oth_ven_cust.Enabled = False
        Me.tb_itmno.Enabled = False
        Me.tb_DRIVERNAM.Enabled = False
        Me.cb_dcode.Enabled = False
        Me.Cb_buyname.Enabled = False
        Me.Tb_buydesc.Enabled = False
        Me.Tb_ccic.Enabled = False
        Me.DataGridView1.Enabled = False
        Me.cb_ib.Enabled = False
        Me.tb_sap_doc.Enabled = False

    End Sub
    Private Sub unfreeze_scr()
        glbvar.itmalloc = False
        b_genis.Enabled = True
        b_gends.Enabled = True
        b_genst.Enabled = True
        Button1.Enabled = True
        b_multi.Enabled = True
        B_PO.Enabled = True
        tb_edittktn.Enabled = True
        b_edittktn.Enabled = True
        b_firstwt.Enabled = False
        b_secondwt.Enabled = False
        Me.cb_inouttype.Enabled = False
        'Me.tb_ticketno.Enabled = True
        Me.tb_container.Enabled = True
        Me.tb_vehicleno.Enabled = True
        Me.tb_transporter.Enabled = True
        Me.tb_sledesc.Enabled = True
        Me.tb_itemdesc.Enabled = True
        Me.tb_operatorid.Enabled = True
        Me.tb_numberofpcs.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        Me.tb_NATIONALITY.Enabled = True
        Me.tb_DRIVINGLICNO.Enabled = True
        Me.tb_FIRSTQTY.Enabled = False
        Me.tb_DATEIN.Enabled = False
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
        Me.cb_inouttype.Enabled = True
        Me.cb_itemcode.Enabled = True
        Me.cb_omcustdesc.Enabled = True
        Me.cb_sap_docu_type.Enabled = True
        Me.cb_sledcode.Enabled = True
        Me.tb_oth_ven_cust.Enabled = True
        Me.tb_itmno.Enabled = True
        Me.tb_DRIVERNAM.Enabled = True
        Me.cb_dcode.Enabled = True
        Me.Cb_buyname.Enabled = True
        Me.Tb_buydesc.Enabled = True
        Me.Tb_ccic.Enabled = True
        Me.DataGridView1.Enabled = True
        Me.tb_sap_doc.Enabled = True
        Me.cb_ib.Enabled = True

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
                If tb_QTY.Text <> CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        If Me.tb_sap_doc.Text <> "ZTRE" Then
                            Me.tb_QTY.Text = CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
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
                If tb_QTY.Text <> CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text) Then
                    Try
                        Dim tq As Decimal = CDec(Me.tb_QTY.Text)
                        If Me.tb_sap_doc.Text <> "ZTRE" Then
                            Me.tb_QTY.Text = CDec(tb_SECONDQTY.Text) - CDec(tb_FIRSTQTY.Text) - CDec(tb_DEDUCTIONWT.Text)
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

            sql = "SELECT   itemcode" _
                & "  FROM   STWBMIBDS WBM" _
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

    Private Sub tb_transporter_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_transporter.LostFocus

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
                '         & " from STWBMIBDS where VEHICLENO = '" & Me.tb_sveh.Text & "'" _
                '         & " and status in (1,2,3) and wtstat = 'I'"
                sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT" _
                         & " from STWBMIBDS where VEHICLENO = '" & Me.tb_sveh.Text & "'" _
                         & " and status in (1,2,3) and wtstat = 'I'"

                clear_scr()
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        Me.cb_ib.Checked = True
                    ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        Me.cb_ib.Checked = False
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                        Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                    End If

                    'update data table in case of multiple items.
                    Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                    & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                    & " from(STWBMIBDS_MULTI)" _
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
                        Me.B_PO.Visible = False
                    End If
                    'Else
                    '    Me.b_gends.Visible = False
                    '    Me.b_genis.Visible = False
                    '    Me.b_genst.Visible = False
                    'Me.Button1.Visible = False
                    'Me.B_PO.Visible = False
                    'End If
                    Me.b_firstwt.Enabled = False
                    'Me.b_firstwt2.Enabled = False
                    If Me.tb_SECONDQTY.Text = 0 Then
                        Me.b_secondwt.Enabled = True
                        'Me.b_secondwt2.Enabled = True
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
                    cmd1.CommandText = "curspkg_join.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
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
                    Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    Me.b_mixmat.Visible = True
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
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                Else
                    Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    l_pono.Visible = False
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
            If dstk.Tables(0).Rows.Count > 0 And b_secondwt.Enabled = False Then
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
                '         & " from STWBMIBDS where INTDOCNO = '" & Me.tb_trans.Text & "'" _
                '         & " and status in (1,2,3)"

                sql = "Select INTDOCNO ,INOUTTYPE, TICKETNO, VEHICLENO, CONTAINERNO, TRANSPORTER, ACCOUNTCODE, SLEDCODE, SLEDDESC," _
                         & " INTITEMCODE ,ITEMCODE ,ITEMDESC ,NUMBEROFPCS ,DCODE,DRIVERNAM ,NATIONALITY ,DRIVINGLICNO ,FIRSTQTY," _
                         & " SECONDQTY ,QTY ,DATEIN ,TIMEIN ,DATEOUT ,TIMOUT ,DEDUCTIONWT ,PACKDED,DED,PRICETON ,TOTALPRICE ,RATE,REMARKS ,IBDSNO," _
                         & " FRINTITEMCODE,FRITEMCODE,FRITEMDESC,INTIBDSNO ,STATUS,AUART,BSART,SORDERNO,DELIVERYNO,SLNO,TRANS_CHARGE,PENALTY," _
                         & " MACHINE_CHARGE,LABOUR_CHARGE,PONO,AGMIXNO,CONSNO,CCIC,OMPRICE,OMSLEDCODE,OMSLEDDESC,VBELNS,VBELND,VBELNI,COMFLG,DOCPRINT" _
                         & " from STWBMIBDS where INTDOCNO = '" & Me.tb_trans.Text & "'" _
                         & " and status in (1,2,3)"
                clear_scr()
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
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        Me.cb_ib.Checked = True
                    ElseIf (IsDBNull(ds.Tables(0).Rows(0).Item("COMFLG"))) Then
                        Me.cb_ib.Checked = False
                    End If
                    If Not (IsDBNull(ds.Tables(0).Rows(0).Item("DOCPRINT"))) Then
                        Me.tb_docprint.Text = ds.Tables(0).Rows(0).Item("DOCPRINT")
                    End If

                    'update data table in case of multiple items.
                    Dim sqlmulti As String = "Select  INTDOCNO ,INOUTTYPE ,TICKETNO ,INTITEMCODE ,ITEMCODE ,ITEMDESC ," _
                    & "FIRSTQTY, SECONDQTY, QTY,SLNO" _
                    & " from(STWBMIBDS_MULTI)" _
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
                        Me.B_PO.Visible = False
                    End If
                    'Else
                    '    Me.b_gends.Visible = False
                    '    Me.b_genis.Visible = False
                    '    Me.b_genst.Visible = False
                    'Me.Button1.Visible = False
                    'Me.B_PO.Visible = False
                    'End If
                    Me.b_firstwt.Enabled = False
                    'Me.b_firstwt2.Enabled = False
                    If Me.tb_SECONDQTY.Text = 0 Then
                        Me.b_secondwt.Enabled = True
                        'Me.b_secondwt2.Enabled = True
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
                    cmd1.CommandText = "curspkg_join.insert_lock"
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
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
                    Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    Me.b_mixmat.Visible = True
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
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                Else
                    Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    l_pono.Visible = False
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
                    & " AND z2.matnr = " & "'" & tb_itemdesc.Text & "'"

            Dim dpct = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpct.Fill(dpc)
            Dim user_tol_value As Decimal
            Dim user_tot_allowed As Decimal
            Dim pct As Decimal
            Dim amt As Decimal
            Dim a = dpc.Tables(0).Rows.Count
            If dpc.Tables(0).Rows.Count > 0 Then
                pct = dpc.Tables(0).Rows(0).Item("pct")
                amt = dpc.Tables(0).Rows(0).Item("amount")

                Dim plist = Convert.ToDecimal(Me.tb_prlist.Text)
                user_tol_value = pct * plist
                user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text)
                If pct <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text) + user_tol_value
                ElseIf amt <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.tb_prlist.Text) + amt
                End If
                If Me.cb_inouttype.Text = "I" Then
                    If Me.tb_sap_doc.Text = "QD" Or Me.tb_sap_doc.Text = "QMX" Then
                        If Me.tb_PRICETON.Text > user_tot_allowed Then
                            MsgBox("Price not matching as the latest Pricelist")
                            tb_PRICETON.Focus()
                        Else
                            tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                        End If
                    Else
                        tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                ElseIf Me.cb_inouttype.Text = "O" Then
                    tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                End If
            Else
                Me.tb_PRICETON.Text = 0
                MsgBox("You are not authorized to enter price for this material")
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
        cmd.CommandText = "curspkg_join.custmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.docmst"
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.docmst"
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
        Me.cb_fritem.Text = glbvar.temp_suppdesc
        Me.tb_fritemdesc.Text = glbvar.temp_suppcode
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
            ZSDSOPROCESSNEW()
            Button1.Visible = False

        ElseIf tb_sap_doc.Text = "ZDCQ" Then

            ZSDDIRECTCONTRACT()
            Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZTCF" Then

            ZSDCONSIGNFILLUP02()
            Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZCWA" Then
            ZSDCWASALES()
            Button1.Visible = False
        ElseIf tb_sap_doc.Text = "ZTRE" Then
            ZSDRETURNORDER()
            Button1.Visible = False
        End If 'document checking endif



    End Sub


    Private Sub B_PO_Click(sender As Object, e As EventArgs) Handles B_PO.Click
        b_save_Click(sender, e)
        Dim divcd As String
        Dim actcd As String 'sledcode
        If tb_sap_doc.Text = "QD" Then
            'ZMMPOGRPROCESS() 'Direct Purchase
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim das As OracleDataAdapter
            Dim dss As New DataSet
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "Select div_code from MAPPLANT where div_code = '" & glbvar.divcd & "'"
            das = New OracleDataAdapter(sql, conn)
            das.TableMappings.Add("Table", "divs")
            das.Fill(dss)
            divcd = dss.Tables("divs").Columns("div_code").ToString

            Dim dact As OracleDataAdapter
            Dim dsac As New DataSet
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "Select ORAACC from MAPSAPORA_VENDOR_ALQ where div_code = '" & glbvar.divcd & "'"
            dact = New OracleDataAdapter(sql, conn)
            dact.TableMappings.Add("Table", "act")
            dact.Fill(dsac)
            actcd = dsac.Tables("act").Columns("ORAACC").ToString 'sledcode
            'TODO Sleddesc and accountcode


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.GEN_MATERIAL_RECEIPT"
            cmd.CommandType = CommandType.StoredProcedure
            
            cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = "ALQ"
            cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = divcd
            cmd.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Int32)).Value = FormatDateTime(Me.tb_DATEIN.Text, Year)
            cmd.Parameters.Add(New OracleParameter("docdt", OracleDbType.Date)).Value = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
            cmd.Parameters.Add(New OracleParameter("tktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmd.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value = Me.Tb_accountcode.Text 'TODO
            cmd.Parameters.Add(New OracleParameter("psledcode", OracleDbType.Varchar2)).Value = actcd
            cmd.Parameters.Add(New OracleParameter("psleddesc", OracleDbType.Varchar2)).Value = Me.cb_sledcode.Text 'TODO
            cmd.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("intitm", OracleDbType.Varchar2)).Value = CInt(Me.Tb_intitemcode.Text)
            cmd.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value = Me.cb_itemcode.Text
            cmd.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Int32)).Value = 1
            cmd.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Int32)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Int32)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Int32)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("vpipgrd", OracleDbType.Varchar2)).Value = DBNull.Value
            cmd.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Int32)).Value = CDec(Me.tb_FIRSTQTY.Text)
            cmd.Parameters.Add(New OracleParameter("secwt", OracleDbType.Int32)).Value = CDec(tb_SECONDQTY.Text)
            cmd.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Int32)).Value = CDec(tb_DEDUCTIONWT.Text)
            cmd.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Int32)).Value = CDec(tb_QTY.Text)
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
                MsgBox("Record Saved")
                'Generate Purchase Valuation.
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                'get 
                Dim cmd1 As New OracleCommand
                cmd1.Connection = conn
                cmd1.Parameters.Clear()
                cmd1.CommandText = "gen_iwb_dsd.GEN_Purch_Valuation"
                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = "ALQ"
                cmd1.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = divcd
                cmd1.Parameters.Add(New OracleParameter("pyearcode", OracleDbType.Varchar2)).Value = FormatDateTime(Me.tb_DATEIN.Text, Year)
                cmd1.Parameters.Add(New OracleParameter("docdt", OracleDbType.Varchar2)).Value = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
                cmd1.Parameters.Add(New OracleParameter("tktno", OracleDbType.Varchar2)).Value       'pass the intreqno
                cmd1.Parameters.Add(New OracleParameter("acctcode", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("psledcode", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("psleddesc", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vsupcode", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("intitm", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("pitmdesc", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vprnslno", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vpipthk", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vpiplen", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vpipod", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("fstwt", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("secwt", OracleDbType.Varchar2)).Value      'pass the rate
                cmd1.Parameters.Add(New OracleParameter("dedwt", OracleDbType.Varchar2)).Value     'pass the value of the
                cmd1.Parameters.Add(New OracleParameter("ntwt", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("vehicle", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("containo", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("usrnm", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.Varchar2)).Value
                cmd1.Parameters.Add(New OracleParameter("docn", OracleDbType.Varchar2)).Value
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
                conn.Close()
            End Try
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QN" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            ZMMGENPOPROCESS() 'Against PO Purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QI" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            ZINTERBRANCHDETAILSUPD() 'Interbranch complete purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QX" Then
            ZMMMIXMATPROCESS() 'Mixmaterial purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QMX" Then
            ZMMMIXINMATPROCESS() ' against mix material purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QIM" Then
            ZMMINTMIXMATPROCESS() ' interbranch mix material purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QIX" Then
            ZMIXINTERBRANCHDETAILSUPD() ' interbranch against mix material purchase
            B_PO.Visible = False
        ElseIf tb_sap_doc.Text = "QO" Then
            ZMMOMAUTOPROCESS() 'OM purchase and sales
            B_PO.Visible = False
        End If  'Document 

        'End If 'Main


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

    Private Sub tb_searchbyno_LostFocus(sender As Object, e As EventArgs) Handles tb_searchbyno.LostFocus
        Me.cb_itemcode.Focus()
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

    Private Sub cb_sap_docu_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sap_docu_type.SelectedIndexChanged
        Try
            If Me.cb_sap_docu_type.SelectedIndex <> -1 Then
                Me.tb_sap_doc.Text = Me.cb_sap_docu_type.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "DOCCODE = '" & Me.cb_sap_docu_type.Text & "'" & ""
                foundrow = dsdoc.Tables("doc").Select(expression)
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
                Me.b_mixmat.Visible = False
                Me.cb_ib.Visible = False
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
                    Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_cons.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "QI" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.cb_ib.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = False
                    Me.cb_ib.Checked = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIX" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.l_cons.Visible = True
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.cb_ib.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_so.Visible = False
                    Me.l_dsno.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = True
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
                    Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                    Me.tb_orderno.Visible = True
                    Me.tb_dsno.Visible = True
                    Me.l_dsno.Visible = True
                    Me.l_so.Visible = True
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.cb_ib.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = False
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                    Me.l_so.Visible = True
                    Me.tb_dsno.Visible = False
                    Me.l_dsno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.l_cons.Visible = False
                    Me.Tb_asno.Visible = False
                    Me.l_pono.Visible = False
                    Me.cb_ib.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.l_agmix.Visible = False
                    Me.b_mixmat.Visible = False
                Else
                    Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.cb_ib.Visible = False
                    l_agmix.Visible = False
                    l_cons.Visible = False
                    l_dsno.Visible = False
                    l_pono.Visible = False
                    l_so.Visible = False
                    Me.b_mixmat.Visible = False
                End If





            End If  'Document 

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub

    Private Sub ZMMGENPOPROCESS()
        'Make ASN Number mandatory
        'Price field to be disabled
        'update wbms table VBELNS - ASN entered by the user, VBELND - GRno returned from FM, VBELNI - IR no returned from FM
        ' This call is required by the designer.
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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.Tb_asno.Text = "" Then
            MsgBox(" PO # is compulsory")
            Me.Tb_asno.Focus()
        ElseIf Me.tb_itmno.Text = "" Then
            MsgBox(" Item # is compulsory")
            Me.tb_itmno.Focus()
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

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_GEN_PO_PROCESS")
                Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_POHEADER")
                'pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QN")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'pohdrin.SetValue("VENDOR", Me.cb_sledcode.Text)
                'pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                'pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                'pohdrin.SetValue("CURRENCY", "SAR")

                ''
                'Dim pohdrinx As IRfcStructure = pogrir.GetStructure("I_POHEADERX")
                'pohdrinx.SetValue("COMP_CODE", "X")
                'pohdrinx.SetValue("DOC_TYPE", "X")
                'pohdrinx.SetValue("VENDOR", "X")
                'pohdrinx.SetValue("PURCH_ORG", "X")
                'pohdrinx.SetValue("PUR_GROUP", "X")
                'pohdrinx.SetValue("CURRENCY", "X")

                'Dim pocst As IRfcStructure = pogrir.GetStructure("I_POHEADERCUST")
                '' Create field in transaction taable and bring from hremployee table
                ''pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                ''pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                'pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                'pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                'pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                'pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                'pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                'pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                'grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)
                Dim condition As IRfcTable = pogrir.GetTable("T_POCONDHEADER")
                'Dim conditionx As IRfcTable = pogrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                'Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'pztr1xu.SetValue("COND_TYPE", "X")
                'pztr1xu.SetValue("COND_VALUE", "X")
                'pztr1xu.SetValue("CURRENCY", "X")
                'pztr1xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                'Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                'nztr2u.SetValue("COND_TYPE", "ZTR2")
                'nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                'nztr2u.SetValue("CURRENCY", "SAR")
                'nztr2u.SetValue("CHANGE_ID", "I")

                'condition.Append(nztr2u)

                'Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'nztr2xu.SetValue("COND_TYPE", "X")
                'nztr2xu.SetValue("COND_VALUE", "X")
                'nztr2xu.SetValue("CURRENCY", "X")
                'nztr2xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                'Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'pzpt1xu.SetValue("COND_TYPE", "X")
                'pzpt1xu.SetValue("COND_VALUE", "X")
                'pzpt1xu.SetValue("CURRENCY", "X")
                'pzpt1xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                'Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                'nzpt1u.SetValue("COND_TYPE", "ZPT2")
                'nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                'nzpt1u.SetValue("CURRENCY", "SAR")
                'nzpt1u.SetValue("CHANGE_ID", "I")

                'condition.Append(nzpt1u)

                'Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'nzpt2xu.SetValue("COND_TYPE", "X")
                'nzpt2xu.SetValue("COND_VALUE", "X")
                'nzpt2xu.SetValue("CURRENCY", "X")
                'nzpt2xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                'Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'pzmh1xu.SetValue("COND_TYPE", "X")
                'pzmh1xu.SetValue("COND_VALUE", "X")
                'pzmh1xu.SetValue("CURRENCY", "X")
                'pzmh1xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                'Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                'nzmh2u.SetValue("COND_TYPE", "ZMH2")
                'nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                'nzmh2u.SetValue("CURRENCY", "SAR")
                'nzmh2u.SetValue("CHANGE_ID", "I")

                'condition.Append(nzmh2u)

                'Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'nzmh2xu.SetValue("COND_TYPE", "X")
                'nzmh2xu.SetValue("COND_VALUE", "X")
                'nzmh2xu.SetValue("CURRENCY", "X")
                'nzmh2xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                'Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'pzlb1xu.SetValue("COND_TYPE", "X")
                'pzlb1xu.SetValue("COND_VALUE", "X")
                'pzlb1xu.SetValue("CURRENCY", "X")
                'pzlb1xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                'Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                'nzlb2u.SetValue("COND_TYPE", "ZLB2")
                'nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                'nzlb2u.SetValue("CURRENCY", "SAR")
                'nzlb2u.SetValue("CHANGE_ID", "I")

                'condition.Append(nzlb2u)

                'Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                'nzlb2xu.SetValue("COND_TYPE", "X")
                'nzlb2xu.SetValue("COND_VALUE", "X")
                'nzlb2xu.SetValue("CURRENCY", "X")
                'nzlb2xu.SetValue("CHANGE_ID", "X")

                'conditionx.Append(nzlb2xu)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
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

                            Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            pozfstru.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
                            'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                            'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                            'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                            'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                            'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                            'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                            'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                            'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
                            pozfstru.SetValue("ZZCNNUM", Me.tb_container.Text) 'Container No
                            pozf.Append(pozfstru)

                            Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                            Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                            gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                            gpozfstru.SetValue("EBELP", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString()) ' Item Number of Purchasing Document
                            'gpozfstru.SetValue("MATNR", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())) 'Material Number
                            gpozfstru.SetValue("WERKS", glbvar.divcd) 'Material Number
                            Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            gpozfstru.SetValue("MENGE", gt) 'Quantity
                            gpozf.Append(gpozfstru)
                           




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
                        ''poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text))
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

                        
                        Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        pozfstru.SetValue("PO_ITEM", Me.tb_itmno.Text) 'Convert.ToDecimal(tb_itmno.Text))
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
                        'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                        'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                        'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                        'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                        'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                        'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                        'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                        'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
                        pozfstru.SetValue("ZZCNNUM", Me.tb_container.Text)
                        pozf.Append(pozfstru)

                        Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                        Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                        gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                        gpozfstru.SetValue("EBELP", Me.tb_itmno.Text) 'Convert.ToDecimal(tb_itmno.Text))  Item Number of Purchasing Document
                        'gpozfstru.SetValue("MATNR", Me.tb_itemdesc.Text) 'Material Number
                        gpozfstru.SetValue("WERKS", glbvar.divcd) 'Material Number
                        'Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())/1000
                        gpozfstru.SetValue("MENGE", Convert.ToDecimal(tb_QTY.Text) / 1000) 'Quantity
                        gpozf.Append(gpozfstru)

                        
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

                    Dim pmesg As OracleParameter = New OracleParameter(":n4", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Int64)
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
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    'Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value 'pogrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QN")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.


    End Sub

   

   
    Public Sub ZSDDIRECTCONTRACT()
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmdc.Connection = conn
        cmdc.Parameters.Clear()
        cmdc.CommandText = "curspkg_join.chk_multi"
        cmdc.CommandType = CommandType.StoredProcedure
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                If dsamltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString() = "0" Then
                    count = count + 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim cmd As New OracleCommand
        ' This call is required by the designer.
        ' Add any initialization after the InitializeComponent() call.

        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            Me.b_save.Focus()
        ElseIf Me.tb_sledesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.tb_sledesc.Focus()
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 AndAlso Me.tb_itmno.Text = "" Or Me.tb_itmno.Text = "0" Then
            MsgBox("Please enter Item #")
            Me.tb_itmno.Focus()
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 AndAlso count > 0 Then
            MsgBox("Please enter Item #")
        ElseIf Me.cb_itemcode.Text = "" Then
            MsgBox("Select an itemcode")
            Me.cb_itemcode.Focus()
        ElseIf Me.tb_FIRSTQTY.Text = "" Then
            MsgBox(" First Qty cannot be blank")
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.tb_orderno.Text = "" Then
            MsgBox(" SO # is compulsory")
            Me.tb_orderno.Focus()
        ElseIf Me.tb_dsno.Text = "" Then
            MsgBox(" Delivery Note # is compulsory")
            Me.tb_dsno.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '    MsgBox(" Price must be entered ")
        Else
            Dim a = CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString)
            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim saprfcrepos As RfcRepository = saprfcdest.Repository

                Dim pgibi As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_DIRECT_CONTRACT")
                Dim dcust As IRfcStructure = pgibi.GetStructure("CUST_FIELDS") 'CUST_FIELDS 
                dcust.SetValue("ZZTICKET", CInt(Me.tb_ticketno.Text)) ' done
                dcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text) 'done
                'dcust.SetValue("ZZVNAME", Me.tb_vehicleno.Text) 'done
                dcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                dcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                dcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                dcust.SetValue("ZZINDS", glbvar.scaletype) 'done
                'dcust.SetValue("ZZCNTNO", tb_container.Text) 'done



                pgibi.SetValue("I_DELIVERY", tb_dsno.Text)
                'dcn1.SetValue("I_DELIVERY", )

                pgibi.SetValue("I_SALESORDER", tb_orderno.Text)

                pgibi.SetValue("I_UNAME", glbvar.userid)
                pgibi.SetValue("REMARKS_1", Me.tb_comments.Text)

                'Dim dpqty As IRfcStructure = pgibil.GetStructure("I_PICKQUANTITY")
                'Dim pqty As Decimal = Convert.ToDecimal(tb_QTY.Text)/1000
                'dpqty.SetValue("I_PICKQUANTITY", pqty)
                'ohdrin.SetValue("DOC_TYPE", "ZDCQ")
                'ohdrin.SetValue("SALES_ORG", glbvar.VKORG)
                'ohdrin.SetValue("DISTR_CHAN", glbvar.VTWEG)
                'ohdrin.SetValue("DIVISION", "11")
                'ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                'ohdrin.SetValue("I_DELIVERY", Me.tb_dsno.Text)
                'ohdrin.SetValue("I_SALESORDER", Me.tb_orderno.Text)
                'ohdrin.SetValue("I_PICKQUANTITY", Me.tb_orderno.Text)

                'Dim ohdrinx As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_INX")
                'ohdrinx.SetValue("DOC_TYPE", "X")
                'ohdrinx.SetValue("SALES_ORG", "X")
                'ohdrinx.SetValue("DISTR_CHAN", "X")
                'ohdrinx.SetValue("DIVISION", "X")
                'ohdrinx.SetValue("PURCH_NO_C", "X")
                'ohdrin.SetValue("I_DELIVERY", "X")
                'ohdrin.SetValue("I_SALESORDER", "X")
                'ohdrin.SetValue("I_PICKQUANTITY", "X")



                Dim pqty As IRfcTable = pgibi.GetTable("PICK_QTY") 'T_DELCUST_FIELD

                Dim itcust As IRfcTable = pgibi.GetTable("CUST_FIELDS_ITEM")

                Dim sremarks As IRfcTable = pgibi.GetTable("REMARKS")


                'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim pqtystr As IRfcStructure = pqty.Metadata.LineType.CreateStructure
                            Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            pqtystr.SetValue("ITM_NUMBER", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                            pqtystr.SetValue("PICK_QTY", rqty)
                            pqtystr.SetValue("PICK_UOM", "TO")
                            pqty.Append(pqtystr)

                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.


                            Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                            itcuststr.SetValue("ZZCCIC", Me.Tb_ccic.Text)
                            itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) 'commented since not found in FM
                            itcuststr.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            itcuststr.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            'itcuststr.SetValue("ZZPIPE", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZOM", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZTHICK", 0) ' CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZLEN", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            itcuststr.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            itcuststr.SetValue("ZZDECT", CDec(tb_ded.Text))
                            itcuststr.SetValue("ZZPACKD", CDec(tb_packded.Text))
                            'itcuststr.SetValue("ZZUOMOD", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZUOMT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZUOML", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'itcuststr.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)

                            itcust.Append(itcuststr)
                            Dim sremarksd As IRfcStructure = sremarks.Metadata.LineType.CreateStructure
                            sremarksd.SetValue("TDLINE", Me.tb_comments.Text)
                            sremarks.Append(sremarksd)

                        Next


                    Else
                        Dim pqtystr As IRfcStructure = pqty.Metadata.LineType.CreateStructure
                        Dim rqty As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        'pqtystr.SetValue("ITM_NUMBER", 10)
                        pqtystr.SetValue("ITM_NUMBER", CInt(Me.tb_itmno.Text))
                        pqtystr.SetValue("PICK_QTY", rqty)
                        pqtystr.SetValue("PICK_UOM", "TO")
                        pqty.Append(pqtystr)





                        Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                        itcuststr.SetValue("ZZCCIC", Me.Tb_ccic.Text)
                        itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) 'commented since not found in FM
                        itcuststr.SetValue("ZZFWGT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        itcuststr.SetValue("ZZSWGT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        'itcuststr.SetValue("ZZPIPE", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZOM", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZTHICK", 0) ' CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZLEN", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        itcuststr.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        itcuststr.SetValue("ZZDECT", CDec(tb_ded.Text) / 1000)
                        itcuststr.SetValue("ZZPACKD", CDec(tb_packded.Text) / 1000)
                        ' itcuststr.SetValue("ZZUOMOD", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZUOMT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZUOML", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        'itcuststr.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                        itcust.Append(itcuststr)
                        Dim sremarksd As IRfcStructure = sremarks.Metadata.LineType.CreateStructure
                        sremarksd.SetValue("TDLINE", Me.tb_comments.Text)
                        sremarks.Append(sremarksd)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try




                Dim rttbl As IRfcTable = pgibi.GetTable("RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                pgibi.Invoke(saprfcdest)
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
                Catch ex As Exception
                    MsgBox(ex.Message & "From insering into SO Error Table")
                End Try
                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Billing Document # " & pgibi.GetValue("E_BILLINGDOC").ToString)
                    'Me.tb_sapord.Text = sodnbil.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = sodnbil.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pgibi.GetValue("E_BILLINGDOC").ToString
                    freeze_scr()
                    '& vbCrLf & "Delivery Note # " & pgibi.GetValue("E_DELIVERY").ToString _
                    '& vbCrLf & "Invoice # " & pgibi.GetValue("E_INVOICE").ToString _

                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value 'pgibi.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value 'pgibi.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pgibi.GetValue("E_BILLINGDOC").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    Dim endtime = DateTime.Now.ToString()



                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From Main ZDCQ")
            End Try

        End If
        'End if

    End Sub

    Public Sub ZMMMIXMATPROCESS()

        ' This call is required by the designer.
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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '   MsgBox("Please enter a price")
        Else

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MATERIAL_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QX")
                pohdrin.SetValue("VENDOR", Me.tb_sledesc.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))

                Dim pohdrinx As IRfcStructure = mmgrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("CREATED_BY", "X")
                pohdrinx.SetValue("DOC_DATE", "X")

                'Dim pocst As IRfcStructure = mmgrir.GetStructure("I_POHEADERCUST")
                '' Create field in transaction taable and bring from hremployee table
                'pocst.SetValue("ZZBNAME", "JAWED") 'Buyer Name
                ''pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                'pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object

                Dim pocst As IRfcStructure = mmgrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                grcst.SetValue("ZZVENDOR", Me.tb_oth_ven_cust.Text)
                grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)

                Dim condition As IRfcTable = mmgrir.GetTable("T_POCONDHEADER")
                Dim conditionx As IRfcTable = mmgrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pztr1xu.SetValue("COND_TYPE", "X")
                pztr1xu.SetValue("COND_VALUE", "X")
                pztr1xu.SetValue("CURRENCY", "X")
                pztr1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nztr2u.SetValue("COND_TYPE", "ZTR2")
                nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                nztr2u.SetValue("CURRENCY", "SAR")
                nztr2u.SetValue("CHANGE_ID", "I")

                condition.Append(nztr2u)

                Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nztr2xu.SetValue("COND_TYPE", "X")
                nztr2xu.SetValue("COND_VALUE", "X")
                nztr2xu.SetValue("CURRENCY", "X")
                nztr2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzpt1xu.SetValue("COND_TYPE", "X")
                pzpt1xu.SetValue("COND_VALUE", "X")
                pzpt1xu.SetValue("CURRENCY", "X")
                pzpt1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzpt1u.SetValue("COND_TYPE", "ZPT2")
                nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                nzpt1u.SetValue("CURRENCY", "SAR")
                nzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(nzpt1u)

                Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzpt2xu.SetValue("COND_TYPE", "X")
                nzpt2xu.SetValue("COND_VALUE", "X")
                nzpt2xu.SetValue("CURRENCY", "X")
                nzpt2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzmh1xu.SetValue("COND_TYPE", "X")
                pzmh1xu.SetValue("COND_VALUE", "X")
                pzmh1xu.SetValue("CURRENCY", "X")
                pzmh1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzmh2u.SetValue("COND_TYPE", "ZMH2")
                nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                nzmh2u.SetValue("CURRENCY", "SAR")
                nzmh2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzmh2u)

                Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzmh2xu.SetValue("COND_TYPE", "X")
                nzmh2xu.SetValue("COND_VALUE", "X")
                nzmh2xu.SetValue("CURRENCY", "X")
                nzmh2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzlb1xu.SetValue("COND_TYPE", "X")
                pzlb1xu.SetValue("COND_VALUE", "X")
                pzlb1xu.SetValue("CURRENCY", "X")
                pzlb1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzlb2u.SetValue("COND_TYPE", "ZLB2")
                nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                nzlb2u.SetValue("CURRENCY", "SAR")
                nzlb2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzlb2u)

                Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzlb2xu.SetValue("COND_TYPE", "X")
                nzlb2xu.SetValue("COND_VALUE", "X")
                nzlb2xu.SetValue("CURRENCY", "X")
                nzlb2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzlb2xu)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                            Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            poitmu.SetValue("PO_ITEM", itm)
                            poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            poitmu.SetValue("PLANT", glbvar.divcd)
                            poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                            poitmu.SetValue("MATL_GROUP", "01")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            poitmu.SetValue("QUANTITY", qt)
                            poitmu.SetValue("PO_UNIT", "TO")
                            'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            poitmu.SetValue("NET_PRICE", cval)
                            poitmu.SetValue("ITEM_CAT", "K")
                            poitm.Append(poitmu)

                            Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                            Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            poitmuX.SetValue("PO_ITEM", itm)
                            poitmuX.SetValue("MATERIAL", "X")
                            poitmuX.SetValue("PLANT", "X")
                            poitmuX.SetValue("STGE_LOC", "X")
                            poitmuX.SetValue("MATL_GROUP", "X")
                            poitmuX.SetValue("QUANTITY", "X")
                            poitmuX.SetValue("PO_UNIT", "X")
                            'poitmuX.SetValue("PO_UNIT_ISO", "X")
                            poitmuX.SetValue("NET_PRICE", "X")
                            poitmuX.SetValue("ITEM_CAT", "X")
                            poitmx.Append(poitmuX)
                            Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            pozfstru.SetValue("PO_ITEM", itm)
                            'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                            'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                            'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                            'pozfstru.SetValue("ZZBNAME", Me.Cb_buyname.Text)
                            'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTION").ToString()) / 1000)
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
                            pozf.Append(pozfstru)


                        Next
                    Else
                        Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", 10)
                        poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                        poitmu.SetValue("MATL_GROUP", "01")
                        poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        poitmu.SetValue("PO_UNIT", "TO")
                        'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text) * 1000)
                        poitmu.SetValue("ITEM_CAT", "K")

                        poitm.Append(poitmu)
                        Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        poitmuX.SetValue("PO_ITEM", 10)
                        poitmuX.SetValue("MATERIAL", "X")
                        poitmuX.SetValue("PLANT", "X")
                        poitmuX.SetValue("STGE_LOC", "X")
                        poitmuX.SetValue("MATL_GROUP", "X")
                        poitmuX.SetValue("QUANTITY", "X")
                        poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        poitmuX.SetValue("NET_PRICE", "X")
                        poitmuX.SetValue("ITEM_CAT", "X")
                        poitmx.Append(poitmuX)
                        'Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        'Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        'poitmuX.SetValue("PO_ITEM", 10)
                        'poitmuX.SetValue("MATERIAL", "X")
                        'poitmuX.SetValue("PLANT", "X")
                        'poitmuX.SetValue("STGE_LOC", "X")
                        'poitmuX.SetValue("MATL_GROUP", "X")
                        'poitmuX.SetValue("QUANTITY", "X")
                        'poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        'poitmuX.SetValue("NET_PRICE", "X")
                        'poitmx.Append(poitmuX)

                        Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        pozfstru.SetValue("PO_ITEM", 10)
                        'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                        'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                        'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                        'pozfstru.SetValue("ZZBNAME", "JAWED")
                        'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
                        pozf.Append(pozfstru)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try

                Dim poerr As IRfcTable = mmgrir.GetTable("T_RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                mmgrir.Invoke(dest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))

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

                Catch ex As Exception
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString) ' _
                    '& vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = mmgrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value 'mmgrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QX")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ZMMPOGRPROCESS()
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmdc.Connection = conn
        cmdc.Parameters.Clear()
        cmdc.CommandText = "curspkg_join.chk_multi"
        cmdc.CommandType = CommandType.StoredProcedure
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                If dsamltitm.Tables("mltitm").Rows(a).Item("RATE").ToString() = "0" Then
                    count = count + 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
        

            ' This call is required by the designer.

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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
            MsgBox("Please enter a price")
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
        Else
            Dim cmd As New OracleCommand
            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_PO_GR_PROCESS")
                Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QD")
                pohdrin.SetValue("VENDOR", Me.tb_sledesc.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pohdrin.SetValue("CREATED_BY", glbvar.userid)

                Dim pohdrinx As IRfcStructure = pogrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("DOC_DATE", "X")
                pohdrinx.SetValue("CREATED_BY", "X")

                Dim pocst As IRfcStructure = pogrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype)
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                grcst.SetValue("ZZVENDOR", Me.tb_oth_ven_cust.Text)
                grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)

                Dim condition As IRfcTable = pogrir.GetTable("T_POCONDHEADER")
                Dim conditionx As IRfcTable = pogrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pztr1xu.SetValue("COND_TYPE", "X")
                pztr1xu.SetValue("COND_VALUE", "X")
                pztr1xu.SetValue("CURRENCY", "X")
                pztr1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nztr2u.SetValue("COND_TYPE", "ZTR2")
                nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                nztr2u.SetValue("CURRENCY", "SAR")
                nztr2u.SetValue("CHANGE_ID", "I")

                condition.Append(nztr2u)

                Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nztr2xu.SetValue("COND_TYPE", "X")
                nztr2xu.SetValue("COND_VALUE", "X")
                nztr2xu.SetValue("CURRENCY", "X")
                nztr2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzpt1xu.SetValue("COND_TYPE", "X")
                pzpt1xu.SetValue("COND_VALUE", "X")
                pzpt1xu.SetValue("CURRENCY", "X")
                pzpt1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzpt1u.SetValue("COND_TYPE", "ZPT2")
                nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                nzpt1u.SetValue("CURRENCY", "SAR")
                nzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(nzpt1u)

                Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzpt2xu.SetValue("COND_TYPE", "X")
                nzpt2xu.SetValue("COND_VALUE", "X")
                nzpt2xu.SetValue("CURRENCY", "X")
                nzpt2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzmh1xu.SetValue("COND_TYPE", "X")
                pzmh1xu.SetValue("COND_VALUE", "X")
                pzmh1xu.SetValue("CURRENCY", "X")
                pzmh1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzmh2u.SetValue("COND_TYPE", "ZMH2")
                nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                nzmh2u.SetValue("CURRENCY", "SAR")
                nzmh2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzmh2u)

                Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzmh2xu.SetValue("COND_TYPE", "X")
                nzmh2xu.SetValue("COND_VALUE", "X")
                nzmh2xu.SetValue("CURRENCY", "X")
                nzmh2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzlb1xu.SetValue("COND_TYPE", "X")
                pzlb1xu.SetValue("COND_VALUE", "X")
                pzlb1xu.SetValue("CURRENCY", "X")
                pzlb1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzlb2u.SetValue("COND_TYPE", "ZLB2")
                nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                nzlb2u.SetValue("CURRENCY", "SAR")
                nzlb2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzlb2u)

                Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzlb2xu.SetValue("COND_TYPE", "X")
                nzlb2xu.SetValue("COND_VALUE", "X")
                nzlb2xu.SetValue("CURRENCY", "X")
                nzlb2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzlb2xu)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1

                            Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                            Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            poitmu.SetValue("PO_ITEM", itm)
                            poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            poitmu.SetValue("PLANT", glbvar.divcd)
                            poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                            poitmu.SetValue("MATL_GROUP", "01")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            poitmu.SetValue("QUANTITY", Math.Round(qt, 3))
                            poitmu.SetValue("PO_UNIT", "TO")
                            'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            poitmu.SetValue("NET_PRICE", cval)
                            poitm.Append(poitmu)

                            Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                            Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            poitmuX.SetValue("PO_ITEM", itm)
                            poitmuX.SetValue("MATERIAL", "X")
                            poitmuX.SetValue("PLANT", "X")
                            poitmuX.SetValue("STGE_LOC", "X")
                            poitmuX.SetValue("MATL_GROUP", "X")
                            poitmuX.SetValue("QUANTITY", "X")
                            poitmuX.SetValue("PO_UNIT", "X")
                            'poitmuX.SetValue("PO_UNIT_ISO", "X")
                            poitmuX.SetValue("NET_PRICE", "X")
                            poitmx.Append(poitmuX)

                            Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            pozfstru.SetValue("PO_ITEM", itm)
                            'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                            'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                            'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                            'pozfstru.SetValue("ZZBNAME", "JAWED")
                            'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
                            'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                            'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                            'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                            'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                            'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                            'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                            'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                            'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
                            pozf.Append(pozfstru)


                        Next
                    Else

                        Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", 10)
                        poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                        poitmu.SetValue("MATL_GROUP", "01")
                        poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        poitmu.SetValue("PO_UNIT", "TO")
                        'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text) * 1000)
                        poitm.Append(poitmu)

                        Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                        Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        poitmuX.SetValue("PO_ITEM", 10)
                        poitmuX.SetValue("MATERIAL", "X")
                        poitmuX.SetValue("PLANT", "X")
                        poitmuX.SetValue("STGE_LOC", "X")
                        poitmuX.SetValue("MATL_GROUP", "X")
                        poitmuX.SetValue("QUANTITY", "X")
                        poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        poitmuX.SetValue("NET_PRICE", "X")
                        poitmx.Append(poitmuX)

                        Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        pozfstru.SetValue("PO_ITEM", 10)
                        'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                        'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                        'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                        'pozfstru.SetValue("ZZBNAME", "JAWED")
                        'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
                        'pozfstru.SetValue("ZZPIPE", "") 'Pipe Number
                        'pozfstru.SetValue("ZZOUTN", "") 'Pipe OD
                        'pozfstru.SetValue("ZZOUTUOM", "") 'OD UOM
                        'pozfstru.SetValue("ZZTHICK", "") 'THICKNESS
                        'pozfstru.SetValue("ZZTHICKUOM", "") 'THICKNESS UOM
                        'pozfstru.SetValue("ZZLEN", "") 'LENGTH
                        'pozfstru.SetValue("ZZLENUOM", "") 'LENGTH UOM
                        'pozfstru.SetValue("ZZNOPIPE", "") 'No: of PIPES
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
                Me.Cursor = Cursors.WaitCursor
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Purchase FM " & Convert.ToString((ed - st)))
                Me.Cursor = Cursors.Default
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

                    Dim pmesg As OracleParameter = New OracleParameter(":n4", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Int64)
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
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Purchase Order # " & pogrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = pogrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QD")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ZSDSOPROCESSNEW()
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmdc.Connection = conn
        cmdc.Parameters.Clear()
        cmdc.CommandText = "curspkg_join.chk_multi"
        cmdc.CommandType = CommandType.StoredProcedure
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                If dsamltitm.Tables("mltitm").Rows(a).Item("RATE").ToString() = "0" Then
                    count = count + 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString


        ' This call is required by the designer.

        'If Me.Tb_intdocno.Text = "" Then
        'MsgBox("Please save the record first")
        'ElseIf Me.tb_sledesc.Text = "" Then
        '    MsgBox("Select a vendor")
        '   Me.tb_sledesc.Focus()
        ' ElseIf Me.cb_itemcode.Text = "" Then
        '     MsgBox("Select an itemcode")
        '     Me.cb_itemcode.Focus()
        ' ElseIf Me.tb_FIRSTQTY.Text = "" Then
        '     MsgBox(" First Qty cannot be blank")
        '     Me.b_newveh.Focus()
        ' ElseIf Me.tb_SECONDQTY.Text = "" Then
        '     MsgBox(" Second Qty cannot be blank")
        '    Me.b_edit.Focus()
        

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
                Me.b_newveh.Focus()
            ElseIf Me.tb_SECONDQTY.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
                Me.b_edit.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            'MsgBox(" Price must be entered ")
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
            MsgBox("Please enter a price")
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
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
                    Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CASH_SALES") 'ZSD_CASH_SALES
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZTBV")
                    ohdrin.SetValue("SALES_ORG", glbvar.VKORG)
                    ohdrin.SetValue("DISTR_CHAN", glbvar.VTWEG)
                    ohdrin.SetValue("DIVISION", glbvar.SPART)
                    ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                    ohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                    ohdrin.SetValue("CREATED_BY", glbvar.userid)

                    Dim ohdrinx As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_INX")
                    ohdrinx.SetValue("DOC_TYPE", "X")
                    ohdrinx.SetValue("SALES_ORG", "X")
                    ohdrinx.SetValue("DISTR_CHAN", "X")
                    ohdrinx.SetValue("DIVISION", "X")
                    ohdrinx.SetValue("PURCH_NO_C", "X")
                    ohdrinx.SetValue("DOC_DATE", "X")
                    'ohdrinx.SetValue("CREATED_BY", "X")

                    Dim scltyp As IRfcStructure = sodnbil.GetStructure("SOCUST_HEAD") 'DLCUST_FIELD 
                    scltyp.SetValue("ZZINDS", glbvar.scaletype)
                    Dim dlcust As IRfcStructure = sodnbil.GetStructure("DLCUST_FIELD") 'DLCUST_FIELD 
                    dlcust.SetValue("ZZTICKET", CInt(Me.tb_ticketno.Text))
                    dlcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text)
                    dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                    dlcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                    dlcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                    dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                    dlcust.SetValue("ZZINDS", glbvar.scaletype)
                    'dlcust.SetValue("ZZCNTNO", Me.tb_container.Text)
                sodnbil.SetValue("REMARKS_1", Me.tb_comments.Text)






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

                'Dim sremarks As IRfcTable = sodnbil.GetTable("REMARKS")

                
                    'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                    conn = New OracleConnection(constr)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
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
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.Connection = conn
                            cmd.Parameters.Clear()
                            cmd.CommandText = "curspkg_join.get_multi"
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                            Dim damultitm As New OracleDataAdapter(cmd)
                            damultitm.TableMappings.Add("Table", "mltitm")
                            Dim dsmltitm As New DataSet
                            damultitm.Fill(dsmltitm)
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
                                'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
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
                            orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.PadLeft(10, "0"))
                                'check if the customer is a one time customer then add the test else no need.
                            orpstru.SetValue("NAME", Me.cb_sledcode.Text)
                            orpstru.SetValue("NAME_2", Me.tb_oth_ven_cust.Text)
                                'orpstru.SetValue("STREET", ORDER_PARTNERS.Rows(0).Cells("STREET").FormattedValue)
                                orpstru.SetValue("COUNTRY", "SA")
                                ''orpstru.SetValue("PO_BOX", ORDER_PARTNERS.Item("PO_BOX", 0).ToString)
                                'orpstru.SetValue("POSTL_CODE", ORDER_PARTNERS.Rows(0).Cells("POSTL_CODE").FormattedValue)
                                orpstru.SetValue("CITY", "Dammam")
                                'orpstru.SetValue("TELEPHONE", ORDER_PARTNERS.Rows(0).Cells("TELEPHONE").FormattedValue)
                                'orpstru.SetValue("FAX_NUMBER", ORDER_PARTNERS.Rows(0).Cells("FAX_NUMBER").FormattedValue)
                                orp.Append(orpstru)
                            'Dim sremarksd As IRfcStructure = sremarks.Metadata.LineType.CreateStructure
                            'sremarksd.SetValue("TDLINE", Me.tb_comments.Text)
                            'sremarks.Append(sremarksd)

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
                            Dim ocinstru As IRfcStructure = ocin.Metadata.LineType.CreateStructure
                            ocinstru.SetValue("ITM_NUMBER", itmoci)
                            'hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                            Dim cstn As UInteger = Convert.ToUInt64("0001")
                            ocinstru.SetValue("COND_ST_NO", cstn)
                            Dim cocn As UInteger = Convert.ToUInt64("00")
                            ocinstru.SetValue("COND_COUNT", cocn)
                            ocinstru.SetValue("COND_TYPE", "ZPR0")
                            Dim cval As Decimal = Convert.ToDecimal(tb_PRICETON.Text) * 1000
                            ocinstru.SetValue("COND_VALUE", cval)
                            ocinstru.SetValue("CURRENCY", "SAR")
                            ocin.Append(ocinstru)
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
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.PadLeft(10, "0"))
                            'check if the customer is a one time customer then add the test else no need.
                            'write an if here to pass the one time customer description.

                            orpstru.SetValue("NAME", Me.cb_sledcode.Text)
                            orpstru.SetValue("NAME_2", Me.tb_oth_ven_cust.Text)
                            'orpstru.SetValue("STREET", ORDER_PARTNERS.Rows(0).Cells("STREET").FormattedValue)
                            orpstru.SetValue("COUNTRY", "SA")
                            ''orpstru.SetValue("PO_BOX", ORDER_PARTNERS.Item("PO_BOX", 0).ToString)
                            'orpstru.SetValue("POSTL_CODE", ORDER_PARTNERS.Rows(0).Cells("POSTL_CODE").FormattedValue)
                            orpstru.SetValue("CITY", "Dammam")
                            'orpstru.SetValue("TELEPHONE", ORDER_PARTNERS.Rows(0).Cells("TELEPHONE").FormattedValue)
                            'orpstru.SetValue("FAX_NUMBER", ORDER_PARTNERS.Rows(0).Cells("FAX_NUMBER").FormattedValue)
                            orp.Append(orpstru)
                        'Dim sremarksd As IRfcStructure = sremarks.Metadata.LineType.CreateStructure
                        ''sremarksd.SetValue("TDLINE", Me.tb_comments.Text)
                        'sremarksd.SetValue("TDLINE", "ABCDEFGHIJK")
                        'sremarks.Append(sremarksd)
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

                        Dim pmesg As OracleParameter = New OracleParameter(":n4", OracleDbType.Varchar2)
                        pmesg.Direction = ParameterDirection.Input
                        pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        pmesg.Value = mesg

                        Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Int64)
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
                              & vbCrLf & "Delivery Note # " & sodnbil.GetValue("E_DELIVERY").ToString _
                              & vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                              )
                        Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
                        Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
                        Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICE").ToString
                        freeze_scr()
                        'Write an update procedure for updating the documnt numbers in STWBMIBDS
                        cmd.Parameters.Clear()
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                        cmd.CommandType = CommandType.StoredProcedure
                        Try
                            cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("SALESDOCUMENT").ToString
                            cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("E_DELIVERY").ToString
                            cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("E_INVOICE").ToString
                            cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                            cmd.ExecuteNonQuery()
                            conn.Close()
                        Catch ex As Exception
                            MsgBox(ex.Message & " From Updating")
                        End Try
                        Dim endtime = DateTime.Now.ToString()



                    End If
                Catch ex As Exception
                    MsgBox(ex.Message & " From Main ZTBV")
                End Try

            End If ' main end if

            ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ZMMMIXINMATPROCESS()
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmdc.Connection = conn
        cmdc.Parameters.Clear()
        cmdc.CommandText = "curspkg_join.chk_multi"
        cmdc.CommandType = CommandType.StoredProcedure
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                If dsamltitm.Tables("mltitm").Rows(a).Item("RATE").ToString() = "0" Then
                    count = count + 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString


        ' This call is required by the designer.

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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
            MsgBox("Please enter a price")
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
            'ElseIf Me.tb_IBDSNO.Text = "" Then
            '    MsgBox(" Ag:Mix Material # is compulsory")
            '    Me.tb_IBDSNO.Focus()
        Else
            Dim cmd As New OracleCommand

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MATERIAL_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QX")
                pohdrin.SetValue("VENDOR", Me.tb_sledesc.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))

                Dim pohdrinx As IRfcStructure = mmgrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("CREATED_BY", "X")
                pohdrinx.SetValue("DOC_DATE", "X")
                

                'Dim pagmix As IRfcStructure = mmgrir.GetStructure("I_MIXMATERIAL")
                'pagmix.SetValue("MAT_DOC", Me.tb_IBDSNO.Text)

                Dim pocst As IRfcStructure = mmgrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                grcst.SetValue("ZZVENDOR", Me.tb_oth_ven_cust.Text)
                grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)

                Dim condition As IRfcTable = mmgrir.GetTable("T_POCONDHEADER")
                Dim conditionx As IRfcTable = mmgrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pztr1xu.SetValue("COND_TYPE", "X")
                pztr1xu.SetValue("COND_VALUE", "X")
                pztr1xu.SetValue("CURRENCY", "X")
                pztr1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nztr2u.SetValue("COND_TYPE", "ZTR2")
                nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                nztr2u.SetValue("CURRENCY", "SAR")
                nztr2u.SetValue("CHANGE_ID", "I")

                condition.Append(nztr2u)

                Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nztr2xu.SetValue("COND_TYPE", "X")
                nztr2xu.SetValue("COND_VALUE", "X")
                nztr2xu.SetValue("CURRENCY", "X")
                nztr2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzpt1xu.SetValue("COND_TYPE", "X")
                pzpt1xu.SetValue("COND_VALUE", "X")
                pzpt1xu.SetValue("CURRENCY", "X")
                pzpt1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzpt1u.SetValue("COND_TYPE", "ZPT2")
                nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                nzpt1u.SetValue("CURRENCY", "SAR")
                nzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(nzpt1u)

                Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzpt2xu.SetValue("COND_TYPE", "X")
                nzpt2xu.SetValue("COND_VALUE", "X")
                nzpt2xu.SetValue("CURRENCY", "X")
                nzpt2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzmh1xu.SetValue("COND_TYPE", "X")
                pzmh1xu.SetValue("COND_VALUE", "X")
                pzmh1xu.SetValue("CURRENCY", "X")
                pzmh1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzmh2u.SetValue("COND_TYPE", "ZMH2")
                nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                nzmh2u.SetValue("CURRENCY", "SAR")
                nzmh2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzmh2u)

                Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzmh2xu.SetValue("COND_TYPE", "X")
                nzmh2xu.SetValue("COND_VALUE", "X")
                nzmh2xu.SetValue("CURRENCY", "X")
                nzmh2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzlb1xu.SetValue("COND_TYPE", "X")
                pzlb1xu.SetValue("COND_VALUE", "X")
                pzlb1xu.SetValue("CURRENCY", "X")
                pzlb1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzlb2u.SetValue("COND_TYPE", "ZLB2")
                nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                nzlb2u.SetValue("CURRENCY", "SAR")
                nzlb2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzlb2u)

                Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzlb2xu.SetValue("COND_TYPE", "X")
                nzlb2xu.SetValue("COND_VALUE", "X")
                nzlb2xu.SetValue("CURRENCY", "X")
                nzlb2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzlb2xu)

                Dim mixtab As IRfcTable = mmgrir.GetTable("T_CONSIGNMENT_PO")


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.get_mix"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Dim damix As New OracleDataAdapter(cmd)
                damix.TableMappings.Add("Table", "mix")
                Dim dsmix As New DataSet
                damix.Fill(dsmix)
                For a = 0 To dsmix.Tables("mix").Rows.Count - 1
                    Dim mixstr As IRfcStructure = mixtab.Metadata.LineType.CreateStructure
                    mixstr.SetValue("EBELN", dsmix.Tables("mix").Rows(a).Item("PONO").ToString())
                    mixstr.SetValue("EBELP", CInt(dsmix.Tables("mix").Rows(a).Item("SLNO").ToString()))
                    mixstr.SetValue("MENGE", CDec(dsmix.Tables("mix").Rows(a).Item("QTY").ToString()) / 1000)
                    mixstr.SetValue("COMPLETE", dsmix.Tables("mix").Rows(a).Item("COMFLG").ToString())
                    mixtab.Append(mixstr)
                Next
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                            Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            poitmu.SetValue("PO_ITEM", itm)
                            poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            poitmu.SetValue("PLANT", glbvar.divcd)
                            poitmu.SetValue("STGE_LOC", glbvar.LGORT)

                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            poitmu.SetValue("QUANTITY", qt)
                            poitmu.SetValue("PO_UNIT", "TO")

                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            poitmu.SetValue("NET_PRICE", cval)

                            poitm.Append(poitmu)

                            Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                            Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            poitmuX.SetValue("PO_ITEM", itm)
                            poitmuX.SetValue("MATERIAL", "X")
                            poitmuX.SetValue("PLANT", "X")
                            poitmuX.SetValue("STGE_LOC", "X")

                            poitmuX.SetValue("QUANTITY", "X")
                            poitmuX.SetValue("PO_UNIT", "X")

                            poitmuX.SetValue("NET_PRICE", "X")

                            poitmx.Append(poitmuX)
                            Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            pozfstru.SetValue("PO_ITEM", itm)
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
                            pozf.Append(pozfstru)


                        Next
                    Else


                        Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", 10)
                        poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)

                        poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        poitmu.SetValue("PO_UNIT", "TO")

                        poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text) * 1000)


                        poitm.Append(poitmu)
                        Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        poitmuX.SetValue("PO_ITEM", 10)
                        poitmuX.SetValue("MATERIAL", "X")
                        poitmuX.SetValue("PLANT", "X")
                        poitmuX.SetValue("STGE_LOC", "X")

                        poitmuX.SetValue("QUANTITY", "X")
                        poitmuX.SetValue("PO_UNIT", "X")

                        poitmuX.SetValue("NET_PRICE", "X")

                        poitmx.Append(poitmuX)
                        'Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        'Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        'poitmuX.SetValue("PO_ITEM", 10)
                        'poitmuX.SetValue("MATERIAL", "X")
                        'poitmuX.SetValue("PLANT", "X")
                        'poitmuX.SetValue("STGE_LOC", "X")
                        'poitmuX.SetValue("MATL_GROUP", "X")
                        'poitmuX.SetValue("QUANTITY", "X")
                        'poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        'poitmuX.SetValue("NET_PRICE", "X")
                        'poitmx.Append(poitmuX)

                        Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        pozfstru.SetValue("PO_ITEM", 10)
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
                        pozf.Append(pozfstru)
                    End If
                    
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try

                Dim poerr As IRfcTable = mmgrir.GetTable("T_RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                mmgrir.Invoke(dest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Mix in FM " & Convert.ToString((ed - st)))

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

                Catch ex As Exception
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = mmgrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = mmgrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QX")
            End Try


        End If 'Main
    End Sub




    ' This call is required by the designer.


    ' Add any initialization after the InitializeComponent() call.

    Public Sub ZSDCONSIGNFILLUP02()

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
            Me.b_newveh.Focus()
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
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CONSIGN_FILLUP02")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZTCF")
                ohdrin.SetValue("SALES_ORG", glbvar.VKORG)
                ohdrin.SetValue("DISTR_CHAN", glbvar.VTWEG)
                ohdrin.SetValue("DIVISION", glbvar.SPART)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
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
                dlcust.SetValue("ZZTICKET", CInt(Me.tb_ticketno.Text))
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
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
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
                            orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text)
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
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text)
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
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
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

    Private Sub ZINTERBRANCHDETAILSUPD()

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
            Me.b_newveh.Focus()
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

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_INTER_BRANCH_UPDATE")
           

                Dim grcst As IRfcStructure = pogrir.GetStructure("I_INTERBRANCH_HEAD")
                ' Create field in transaction taable and bring from hremployee table
                'grcst.SetValue("ZZINDS", "2") 'Buyer Name
                'grcst.SetValue("MANDT", "200")
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                'Commented Praveen 17/03/2015
                'grcst.SetValue("VBELN", Me.Tb_cons_sen_branch.Text) 'SO #
                'grcst.SetValue("MBLNR", "0000000455") 'Material Doc# - Blank in QI
                grcst.SetValue("SENDING_PLANT", tb_sledesc.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("RECEIVING_PLANT", glbvar.divcd) 'Material Doc# - Blank in QI
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
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
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
                grcst.SetValue("CREATED_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
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
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
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
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
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
                        pozfstru.SetValue("WERKS", glbvar.divcd)
                        pozfstru.SetValue("LGORT", glbvar.LGORT)
                        pozfstru.SetValue("MENGE", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        pozfstru.SetValue("MATKL", "01")
                        pozfstru.SetValue("MEINS", "TO")
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        Dim saphgrwt As Decimal = 0.0
                        saphgrwt = Convert.ToDecimal(tb_QTY.Text) / 1000 + CDec(Me.tb_DEDUCTIONWT.Text) / 1000
                        pozfstru.SetValue("ZZGROSSWT", saphgrwt)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
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
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
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

    Public Sub ZMMINTMIXMATPROCESS()

        ' This call is required by the designer.
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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.Tb_cons_sen_branch.Text = "" Then
            MsgBox(" Consignment # is compulsory")
            Me.Tb_cons_sen_branch.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '   MsgBox("Please enter a price")
        Else

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MATERIAL_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QI")
                pohdrin.SetValue("VENDOR", Me.tb_sledesc.Text)
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("REF_1", Me.Tb_cons_sen_branch.Text)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                Dim pohdrinx As IRfcStructure = mmgrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("CREATED_BY", "X")
                pohdrinx.SetValue("REF_1", "X")
                pohdrinx.SetValue("DOC_DATE", "X")
                Dim poconsin As IRfcStructure = mmgrir.GetStructure("I_CONSIG_FILL_REF")
                poconsin.SetValue("VBELN", Me.Tb_cons_sen_branch.Text)



                'Dim pocst As IRfcStructure = mmgrir.GetStructure("I_POHEADERCUST")
                '' Create field in transaction taable and bring from hremployee table
                'pocst.SetValue("ZZBNAME", "JAWED") 'Buyer Name
                ''pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                'pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object

                Dim pocst As IRfcStructure = mmgrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim condition As IRfcTable = mmgrir.GetTable("T_POCONDHEADER")
                Dim conditionx As IRfcTable = mmgrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pztr1xu.SetValue("COND_TYPE", "X")
                pztr1xu.SetValue("COND_VALUE", "X")
                pztr1xu.SetValue("CURRENCY", "X")
                pztr1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nztr2u.SetValue("COND_TYPE", "ZTR2")
                nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                nztr2u.SetValue("CURRENCY", "SAR")
                nztr2u.SetValue("CHANGE_ID", "I")

                condition.Append(nztr2u)

                Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nztr2xu.SetValue("COND_TYPE", "X")
                nztr2xu.SetValue("COND_VALUE", "X")
                nztr2xu.SetValue("CURRENCY", "X")
                nztr2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzpt1xu.SetValue("COND_TYPE", "X")
                pzpt1xu.SetValue("COND_VALUE", "X")
                pzpt1xu.SetValue("CURRENCY", "X")
                pzpt1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzpt1u.SetValue("COND_TYPE", "ZPT2")
                nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                nzpt1u.SetValue("CURRENCY", "SAR")
                nzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(nzpt1u)

                Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzpt2xu.SetValue("COND_TYPE", "X")
                nzpt2xu.SetValue("COND_VALUE", "X")
                nzpt2xu.SetValue("CURRENCY", "X")
                nzpt2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzmh1xu.SetValue("COND_TYPE", "X")
                pzmh1xu.SetValue("COND_VALUE", "X")
                pzmh1xu.SetValue("CURRENCY", "X")
                pzmh1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzmh2u.SetValue("COND_TYPE", "ZMH2")
                nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                nzmh2u.SetValue("CURRENCY", "SAR")
                nzmh2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzmh2u)

                Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzmh2xu.SetValue("COND_TYPE", "X")
                nzmh2xu.SetValue("COND_VALUE", "X")
                nzmh2xu.SetValue("CURRENCY", "X")
                nzmh2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzlb1xu.SetValue("COND_TYPE", "X")
                pzlb1xu.SetValue("COND_VALUE", "X")
                pzlb1xu.SetValue("CURRENCY", "X")
                pzlb1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzlb2u.SetValue("COND_TYPE", "ZLB2")
                nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                nzlb2u.SetValue("CURRENCY", "SAR")
                nzlb2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzlb2u)

                Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzlb2xu.SetValue("COND_TYPE", "X")
                nzlb2xu.SetValue("COND_VALUE", "X")
                nzlb2xu.SetValue("CURRENCY", "X")
                nzlb2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzlb2xu)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                            Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            poitmu.SetValue("PO_ITEM", itm)
                            poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            poitmu.SetValue("PLANT", glbvar.divcd)
                            poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                            poitmu.SetValue("MATL_GROUP", "01")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            poitmu.SetValue("QUANTITY", qt)
                            poitmu.SetValue("PO_UNIT", "TO")
                            'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            poitmu.SetValue("NET_PRICE", cval)
                            poitmu.SetValue("ITEM_CAT", "K")
                            poitm.Append(poitmu)

                            Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                            Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            poitmuX.SetValue("PO_ITEM", 10)
                            poitmuX.SetValue("MATERIAL", "X")
                            poitmuX.SetValue("PLANT", "X")
                            poitmuX.SetValue("STGE_LOC", "X")
                            poitmuX.SetValue("MATL_GROUP", "X")
                            poitmuX.SetValue("QUANTITY", "X")
                            poitmuX.SetValue("PO_UNIT", "X")
                            'poitmuX.SetValue("PO_UNIT_ISO", "X")
                            poitmuX.SetValue("NET_PRICE", "X")
                            poitmuX.SetValue("ITEM_CAT", "X")
                            poitmx.Append(poitmuX)
                            'Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                            'Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            'pozfstru.SetValue("PO_ITEM", 10)
                            'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                            'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                            'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                            'pozfstru.SetValue("ZZBNAME", "JAWED")
                            'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                            'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString())/1000)
                            'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString())/1000)
                            'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text)/1000)
                            'pozf.Append(pozfstru)


                            
                            


                        Next
                    Else
                        Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", 10)
                        poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                        poitmu.SetValue("MATL_GROUP", "01")
                        poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        poitmu.SetValue("PO_UNIT", "TO")
                        'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text) * 1000)
                        poitmu.SetValue("ITEM_CAT", "K")

                        poitm.Append(poitmu)
                        Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        poitmuX.SetValue("PO_ITEM", 10)
                        poitmuX.SetValue("MATERIAL", "X")
                        poitmuX.SetValue("PLANT", "X")
                        poitmuX.SetValue("STGE_LOC", "X")
                        poitmuX.SetValue("MATL_GROUP", "X")
                        poitmuX.SetValue("QUANTITY", "X")
                        poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        poitmuX.SetValue("NET_PRICE", "X")
                        poitmuX.SetValue("ITEM_CAT", "X")
                        poitmx.Append(poitmuX)

                        
                        'Dim poitmx As IRfcTable = mmgrir.GetTable("T_POITEMX")
                        'Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        'poitmuX.SetValue("PO_ITEM", 10)
                        'poitmuX.SetValue("MATERIAL", "X")
                        'poitmuX.SetValue("PLANT", "X")
                        'poitmuX.SetValue("STGE_LOC", "X")
                        'poitmuX.SetValue("MATL_GROUP", "X")
                        'poitmuX.SetValue("QUANTITY", "X")
                        'poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        'poitmuX.SetValue("NET_PRICE", "X")
                        'poitmx.Append(poitmuX)

                        'Dim pozf As IRfcTable = mmgrir.GetTable("T_POCUST_EXT")
                        'Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        'pozfstru.SetValue("PO_ITEM", 10)
                        'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                        'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                        'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                        'pozfstru.SetValue("ZZBNAME", "JAWED")
                        'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                        'pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text)/1000)
                        'pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text)/1000)
                        'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text)/1000)
                        'pozf.Append(pozfstru)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try

                Dim poerr As IRfcTable = mmgrir.GetTable("T_RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                mmgrir.Invoke(dest)
                Dim ed As TimeSpan = Now.TimeOfDay
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))

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

                Catch ex As Exception
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString) ' _
                    '& vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = mmgrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QX")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ZMMOMAUTOPROCESS()
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmdc.Connection = conn
        cmdc.Parameters.Clear()
        cmdc.CommandText = "curspkg_join.chk_multi"
        cmdc.CommandType = CommandType.StoredProcedure
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                If dsamltitm.Tables("mltitm").Rows(a).Item("RATE").ToString() = "0" Then
                    count = count + 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString


        ' This call is required by the designer.

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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
            MsgBox("Please enter a price")
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
        Else
            Dim cmd As New OracleCommand

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_OM_AUTO_PROCESS")
                Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QO")
                pohdrin.SetValue("VENDOR", Me.tb_sledesc.Text)
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))

                Dim pohdrinx As IRfcStructure = pogrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("CREATED_BY", "X")
                pohdrinx.SetValue("DOC_DATE", "X")



                pogrir.SetValue("I_CUSTNO", Me.tb_omcustcode.Text)
                'pogrir.SetValue("I_OMCUSTPRICE", Me.tb_omcustprice.Text)

                Dim pocst As IRfcStructure = pogrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                grcst.SetValue("ZZVENDOR", Me.tb_oth_ven_cust.Text)
                grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)

                Dim condition As IRfcTable = pogrir.GetTable("T_POCONDHEADER")
                Dim conditionx As IRfcTable = pogrir.GetTable("T_POCONDHEADERX")

                'ZTR1 POSITIVE
                Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pztr1u.SetValue("COND_TYPE", "ZTR1")
                pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
                pztr1u.SetValue("CURRENCY", "SAR")
                pztr1u.SetValue("CHANGE_ID", "I")

                condition.Append(pztr1u)


                Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pztr1xu.SetValue("COND_TYPE", "X")
                pztr1xu.SetValue("COND_VALUE", "X")
                pztr1xu.SetValue("CURRENCY", "X")
                pztr1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pztr1xu)

                'ZTR2 NEGATIVE
                Dim nztr2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nztr2u.SetValue("COND_TYPE", "ZTR2")
                nztr2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_transp.Text))
                nztr2u.SetValue("CURRENCY", "SAR")
                nztr2u.SetValue("CHANGE_ID", "I")

                condition.Append(nztr2u)

                Dim nztr2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nztr2xu.SetValue("COND_TYPE", "X")
                nztr2xu.SetValue("COND_VALUE", "X")
                nztr2xu.SetValue("CURRENCY", "X")
                nztr2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nztr2xu)

                'ZPT1 POSITIVE
                Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzpt1u.SetValue("COND_TYPE", "ZPT1")
                pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
                pzpt1u.SetValue("CURRENCY", "SAR")
                pzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzpt1u)


                Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzpt1xu.SetValue("COND_TYPE", "X")
                pzpt1xu.SetValue("COND_VALUE", "X")
                pzpt1xu.SetValue("CURRENCY", "X")
                pzpt1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzpt1xu)

                'ZPT2 NEGATIVE
                Dim nzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzpt1u.SetValue("COND_TYPE", "ZPT2")
                nzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_penalty.Text))
                nzpt1u.SetValue("CURRENCY", "SAR")
                nzpt1u.SetValue("CHANGE_ID", "I")

                condition.Append(nzpt1u)

                Dim nzpt2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzpt2xu.SetValue("COND_TYPE", "X")
                nzpt2xu.SetValue("COND_VALUE", "X")
                nzpt2xu.SetValue("CURRENCY", "X")
                nzpt2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzpt2xu)

                'ZMH1 POSITIVE
                Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzmh1u.SetValue("COND_TYPE", "ZMH1")
                pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
                pzmh1u.SetValue("CURRENCY", "SAR")
                pzmh1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzmh1u)


                Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzmh1xu.SetValue("COND_TYPE", "X")
                pzmh1xu.SetValue("COND_VALUE", "X")
                pzmh1xu.SetValue("CURRENCY", "X")
                pzmh1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzmh1xu)

                'ZMH2 NEGATIVE
                Dim nzmh2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzmh2u.SetValue("COND_TYPE", "ZMH2")
                nzmh2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_eqpchrgs.Text))
                nzmh2u.SetValue("CURRENCY", "SAR")
                nzmh2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzmh2u)

                Dim nzmh2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzmh2xu.SetValue("COND_TYPE", "X")
                nzmh2xu.SetValue("COND_VALUE", "X")
                nzmh2xu.SetValue("CURRENCY", "X")
                nzmh2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzmh2xu)

                'ZLB1 POSITIVE
                Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                pzlb1u.SetValue("COND_TYPE", "ZLB1")
                pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
                pzlb1u.SetValue("CURRENCY", "SAR")
                pzlb1u.SetValue("CHANGE_ID", "I")

                condition.Append(pzlb1u)


                Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                pzlb1xu.SetValue("COND_TYPE", "X")
                pzlb1xu.SetValue("COND_VALUE", "X")
                pzlb1xu.SetValue("CURRENCY", "X")
                pzlb1xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(pzlb1xu)

                'ZLB2 NEGATIVE
                Dim nzlb2u As IRfcStructure = condition.Metadata.LineType.CreateStructure
                nzlb2u.SetValue("COND_TYPE", "ZLB2")
                nzlb2u.SetValue("COND_VALUE", Convert.ToDecimal(-Me.Tb_labourcharges.Text))
                nzlb2u.SetValue("CURRENCY", "SAR")
                nzlb2u.SetValue("CHANGE_ID", "I")

                condition.Append(nzlb2u)

                Dim nzlb2xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
                nzlb2xu.SetValue("COND_TYPE", "X")
                nzlb2xu.SetValue("COND_VALUE", "X")
                nzlb2xu.SetValue("CURRENCY", "X")
                nzlb2xu.SetValue("CHANGE_ID", "X")

                conditionx.Append(nzlb2xu)

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                            Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                            poitmu.SetValue("PO_ITEM", itm)
                            poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                            poitmu.SetValue("PLANT", glbvar.divcd)
                            poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                            poitmu.SetValue("MATL_GROUP", "01")
                            Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            poitmu.SetValue("QUANTITY", Math.Round(qt, 3))
                            poitmu.SetValue("PO_UNIT", "TO")
                            'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                            Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                            poitmu.SetValue("NET_PRICE", cval)
                            poitm.Append(poitmu)

                            Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                            Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                            poitmuX.SetValue("PO_ITEM", itm)
                            poitmuX.SetValue("MATERIAL", "X")
                            poitmuX.SetValue("PLANT", "X")
                            poitmuX.SetValue("STGE_LOC", "X")
                            poitmuX.SetValue("MATL_GROUP", "X")
                            poitmuX.SetValue("QUANTITY", "X")
                            poitmuX.SetValue("PO_UNIT", "X")
                            'poitmuX.SetValue("PO_UNIT_ISO", "X")
                            poitmuX.SetValue("NET_PRICE", "X")
                            poitmx.Append(poitmuX)

                            Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                            Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                            pozfstru.SetValue("PO_ITEM", itm)
                            'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                            'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                            'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                            'pozfstru.SetValue("ZZBNAME", "JAWED")
                            'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                            pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                            pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
                            pozf.Append(pozfstru)
                            Dim omcustmult As IRfcTable = pogrir.GetTable("T_OM_ITEM_PRICE")
                            Dim omcustmultu As IRfcStructure = omcustmult.Metadata.LineType.CreateStructure
                            omcustmultu.SetValue("POSNR", itm)
                            Dim omval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("OMPRICE").ToString() * 1000)
                            omcustmultu.SetValue("NETPR", omval)
                            omcustmult.Append(omcustmultu)



                        Next
                    Else


                        Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", 10)
                        poitmu.SetValue("MATERIAL", Me.tb_itemdesc.Text)
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                        poitmu.SetValue("MATL_GROUP", "01")
                        poitmu.SetValue("QUANTITY", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        poitmu.SetValue("PO_UNIT", "TO")
                        'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        poitmu.SetValue("NET_PRICE", Convert.ToDecimal(tb_PRICETON.Text) * 1000)
                        poitm.Append(poitmu)

                        Dim poitmx As IRfcTable = pogrir.GetTable("T_POITEMX")
                        Dim poitmuX As IRfcStructure = poitmx.Metadata.LineType.CreateStructure
                        poitmuX.SetValue("PO_ITEM", 10)
                        poitmuX.SetValue("MATERIAL", "X")
                        poitmuX.SetValue("PLANT", "X")
                        poitmuX.SetValue("STGE_LOC", "X")
                        poitmuX.SetValue("MATL_GROUP", "X")
                        poitmuX.SetValue("QUANTITY", "X")
                        poitmuX.SetValue("PO_UNIT", "X")
                        'poitmuX.SetValue("PO_UNIT_ISO", "X")
                        poitmuX.SetValue("NET_PRICE", "X")
                        poitmx.Append(poitmuX)

                        Dim pozf As IRfcTable = pogrir.GetTable("T_POCUST_EXT")
                        Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                        pozfstru.SetValue("PO_ITEM", 10)
                        'pozfstru.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                        'pozfstru.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                        'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                        'pozfstru.SetValue("ZZBNAME", "JAWED")
                        'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
                        pozf.Append(pozfstru)
                        Dim omcustmult As IRfcTable = pogrir.GetTable("T_OM_ITEM_PRICE")
                        Dim omcustmultu As IRfcStructure = omcustmult.Metadata.LineType.CreateStructure
                        omcustmultu.SetValue("POSNR", 10)
                        omcustmultu.SetValue("NETPR", Convert.ToDecimal(tb_omcustprice.Text) * 1000)
                        omcustmult.Append(omcustmultu)
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
                MsgBox("time taken for Sales FM " & Convert.ToString((ed - st)))

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

                Catch ex As Exception
                    MsgBox(ex.Message & " From Inserting into PO Error Table")
                End Try
                If poercnt > 0 Then
                    MsgBox("There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & " errors"
                           )
                Else
                    MsgBox("Purchase Order # " & pogrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString _
                          & vbCrLf & "Order  # " & pogrir.GetValue("E_SALEORDER").ToString _
                          & vbCrLf & "Delivery  # " & pogrir.GetValue("E_DELIVERY").ToString _
                          & vbCrLf & "Billing  # " & pogrir.GetValue("E_BILLINGNO").ToString)
                    Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = pogrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QO")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub omcustload()
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
       
    End Sub

    Private Sub cb_omcustdesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_omcustdesc.SelectedIndexChanged
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
    End Sub

    Public Sub ZSDCWASALES()


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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.tb_PRICETON.Text = "0" Then
            MsgBox(" Price must be entered ")
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
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CWA_SALES")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZCWA")
                ohdrin.SetValue("SALES_ORG", glbvar.VKORG)
                ohdrin.SetValue("DISTR_CHAN", glbvar.VTWEG)
                ohdrin.SetValue("DIVISION", glbvar.SPART)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                ohdrin.SetValue("CREATED_BY", glbvar.userid)

                'ORDER_HEADER_IN()
                'ORDER_HEADER_INX()
                'SOCUST_HEAD()
                'SOCUST_DOC()
                'DLCUST_FIELD()

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
                dlcust.SetValue("ZZTICKET", CInt(Me.tb_ticketno.Text))
                dlcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text)
                dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZINDS", glbvar.scaletype)
                'dlcust.SetValue("ZZCNTNO", Me.tb_container.Text)







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
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString
                    If CInt(dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
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
                            orpstru.SetValue("PARTN_ROLE", "SP")
                            orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.PadLeft(10, "0"))
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
                        Dim ocinstru As IRfcStructure = ocin.Metadata.LineType.CreateStructure
                        ocinstru.SetValue("ITM_NUMBER", itmoci)
                        'hardcoded to 1 if single item else in the multi item start with 1 and increase by 1.
                        Dim cstn As UInteger = Convert.ToUInt64("0001")
                        ocinstru.SetValue("COND_ST_NO", cstn)
                        Dim cocn As UInteger = Convert.ToUInt64("00")
                        ocinstru.SetValue("COND_COUNT", cocn)
                        ocinstru.SetValue("COND_TYPE", "ZPR0")
                        Dim cval As Decimal = Convert.ToDecimal(tb_PRICETON.Text) * 1000
                        ocinstru.SetValue("COND_VALUE", cval)
                        ocinstru.SetValue("CURRENCY", "SAR")
                        ocin.Append(ocinstru)
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
                        orpstru.SetValue("PARTN_ROLE", "SP")
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledesc.Text.PadLeft(10, "0"))
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

                    Dim pmesg As OracleParameter = New OracleParameter(":n4", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Int64)
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
                          & vbCrLf & "Delivery Note # " & sodnbil.GetValue("E_DELIVERY").ToString _
                          & vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                          )
                    Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
                    Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
                    Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICE").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("E_INVOICE").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    Dim endtime = DateTime.Now.ToString()



                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From Main ZTBV")
            End Try

        End If ' main end if

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    
    

    Private Sub ZMIXINTERBRANCHDETAILSUPD()
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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
            'ElseIf Me.Tb_cons_sen_branch.Text = "" Then
            '   MsgBox(" Consignment # is compulsory")
            '  Me.Tb_cons_sen_branch.Focus()
            'ElseIf Me.tb_IBDSNO.Text = "" Then
            '   MsgBox(" Ag:Mix Material # is compulsory")
            '  Me.tb_IBDSNO.Focus()
        Else
            'Dim cmd As New OracleCommand

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_INTER_BRANCH_UPDATE")


                Dim grcst As IRfcStructure = pogrir.GetStructure("I_INTERBRANCH_HEAD")
                ' Create field in transaction taable and bring from hremployee table
                'grcst.SetValue("ZZINDS", "2") 'Buyer Name
                'grcst.SetValue("MANDT", "200")
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                'Commented Praveen 17/03/2015
                'grcst.SetValue("VBELN", Me.Tb_cons_sen_branch.Text) 'SO #
                'Commented Praveen 17/03/2015
                'grcst.SetValue("MBLNR", Me.tb_IBDSNO.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("SENDING_PLANT", tb_sledesc.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("RECEIVING_PLANT", glbvar.divcd) 'Material Doc# - Blank in QI
                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("BUKRS", glbvar.cmpcd) 'Material Doc# - Blank in QI
                grcst.SetValue("BSART", "QI") 'Material Doc# - Blank in QI
                'grcst.SetValue("AEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'grcst.SetValue("ERNAM", "AKMENON") 'Name of Person who Created the Object
                'grcst.SetValue("CREATED_BY", "AKMENON") 'Name of Person who Created the Object
                grcst.SetValue("LIFNR", tb_sledesc.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("EKORG", glbvar.EKORG) 'Material Doc# - Blank in QI
                grcst.SetValue("EKGRP", glbvar.EKGRP) 'Material Doc# - Blank in QI
                'grcst.SetValue("BEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
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
                grcst.SetValue("CREATED_DATE", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZLABCHAR", Me.Tb_labourcharges.Text) for store charges
                Dim mixtab As IRfcTable = pogrir.GetTable("T_INTERBRANCH_CONSIG")

                pogrir.SetValue("I_IND", "X")
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join.get_mix"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Dim damix As New OracleDataAdapter(cmd)
                damix.TableMappings.Add("Table", "mix")
                Dim dsmix As New DataSet
                damix.Fill(dsmix)
                For a = 0 To dsmix.Tables("mix").Rows.Count - 1
                    Dim mixstr As IRfcStructure = mixtab.Metadata.LineType.CreateStructure
                    mixstr.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                    Dim cd = dsmix.Tables("mix").Rows(a).Item("PONO").ToString()
                    mixstr.SetValue("EBELN", dsmix.Tables("mix").Rows(a).Item("PONO").ToString())
                    'Commented Praveen 17/03/2015
                    'mixstr.SetValue("VBELN", Me.Tb_cons_sen_branch.Text)
                    mixstr.SetValue("EBELP", CInt(dsmix.Tables("mix").Rows(a).Item("SLNO").ToString()))
                    Dim ab = CDec(dsmix.Tables("mix").Rows(a).Item("QTY").ToString()) / 1000
                    mixstr.SetValue("MENGE", CDec(dsmix.Tables("mix").Rows(a).Item("QTY").ToString()) / 1000)
                    mixstr.SetValue("COMPLETE", dsmix.Tables("mix").Rows(a).Item("COMFLG").ToString())
                    mixtab.Append(mixstr)
                Next
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
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
                            pozfstru.SetValue("ZZFTUOM", "MT")
                            pozfstru.SetValue("ZZSECUOM", "MT")
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
                        pozfstru.SetValue("WERKS", glbvar.divcd)
                        pozfstru.SetValue("LGORT", glbvar.LGORT)
                        pozfstru.SetValue("MENGE", Convert.ToDecimal(tb_QTY.Text) / 1000)
                        pozfstru.SetValue("MATKL", "01")
                        pozfstru.SetValue("MEINS", "TO")
                        pozfstru.SetValue("ZZFTWT", CDec(Me.tb_FIRSTQTY.Text) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(Me.tb_SECONDQTY.Text) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        Dim saphgrwt As Decimal = 0.0
                        saphgrwt = Convert.ToDecimal(tb_QTY.Text) / 1000 + CDec(Me.tb_DEDUCTIONWT.Text) / 1000
                        pozfstru.SetValue("ZZGROSSWT", saphgrwt)
                        pozfstru.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
                        pozfstru.SetValue("ZZFTUOM", "MT")
                        pozfstru.SetValue("ZZSECUOM", "MT")
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

                    Dim pmesg As OracleParameter = New OracleParameter(":n4", OracleDbType.Varchar2)
                    pmesg.Direction = ParameterDirection.Input
                    pmesg.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    pmesg.Value = mesg

                    Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Int64)
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
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QIX")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    
    Public Sub ZSDRETURNORDER()
        Dim cmd As New OracleCommand
        ' This call is required by the designer.
        ' Add any initialization after the InitializeComponent() call.

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
            Me.b_newveh.Focus()
        ElseIf Me.tb_SECONDQTY.Text = "" Then
            MsgBox(" Second Qty cannot be blank")
            Me.b_edit.Focus()
        ElseIf Me.tb_orderno.Text = "" Then
            MsgBox(" SO # is compulsory")
            Me.tb_orderno.Focus()
            'ElseIf Me.tb_PRICETON.Text = "0" Then
            '    MsgBox(" Price must be entered ")
        Else
            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim saprfcrepos As RfcRepository = saprfcdest.Repository

                Dim pgibi As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_RETURN_ORDER")
                Dim dcust As IRfcStructure = pgibi.GetStructure("CUST_FIELDS") 'CUST_FIELDS 
                dcust.SetValue("ZZTICKET", CInt(Me.tb_ticketno.Text)) ' done
                dcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text) 'done
                'dcust.SetValue("ZZVNAME", Me.tb_vehicleno.Text) 'done
                dcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                dcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                dcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                dcust.SetValue("ZZINDS", glbvar.scaletype) 'done
                dcust.SetValue("ZZCNTNO", tb_container.Text) 'done



                pgibi.SetValue("I_RETURNORDER", Me.tb_orderno.Text)

                pgibi.SetValue("I_UNAME", glbvar.userid)

                'dcn1.SetValue("I_DELIVERY", )





                'Dim dpqty As IRfcStructure = pgibil.GetStructure("I_PICKQUANTITY")
                'Dim pqty As Decimal = Convert.ToDecimal(tb_QTY.Text)/1000
                'dpqty.SetValue("I_PICKQUANTITY", pqty)
                'ohdrin.SetValue("DOC_TYPE", "ZDCQ")
                'ohdrin.SetValue("SALES_ORG", glbvar.VKORG)
                'ohdrin.SetValue("DISTR_CHAN", glbvar.VTWEG)
                'ohdrin.SetValue("DIVISION", "11")
                'ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                'ohdrin.SetValue("I_DELIVERY", Me.tb_dsno.Text)
                'ohdrin.SetValue("I_SALESORDER", Me.tb_orderno.Text)
                'ohdrin.SetValue("I_PICKQUANTITY", Me.tb_orderno.Text)

                'Dim ohdrinx As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_INX")
                'ohdrinx.SetValue("DOC_TYPE", "X")
                'ohdrinx.SetValue("SALES_ORG", "X")
                'ohdrinx.SetValue("DISTR_CHAN", "X")
                'ohdrinx.SetValue("DIVISION", "X")
                'ohdrinx.SetValue("PURCH_NO_C", "X")
                'ohdrin.SetValue("I_DELIVERY", "X")
                'ohdrin.SetValue("I_SALESORDER", "X")
                'ohdrin.SetValue("I_PICKQUANTITY", "X")



                Dim pqty As IRfcTable = pgibi.GetTable("PICK_QTY") 'T_DELCUST_FIELD

                'Dim itcust As IRfcTable = pgibi.GetTable("CUST_FIELDS_ITEM")


                'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
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
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                        cmd.CommandText = "curspkg_join.get_multi"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                        Dim damultitm As New OracleDataAdapter(cmd)
                        damultitm.TableMappings.Add("Table", "mltitm")
                        Dim dsmltitm As New DataSet
                        damultitm.Fill(dsmltitm)
                        Dim itm As Integer = 0

                        Dim sl As Integer = 0

                        For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1


                            itm = itm + 10
                            sl = sl + 1


                            Dim pqtystr As IRfcStructure = pqty.Metadata.LineType.CreateStructure
                            Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) / 1000
                            pqtystr.SetValue("ITM_NUMBER", itm)
                            pqtystr.SetValue("PICK_QTY", rqty)
                            pqtystr.SetValue("PICK_UOM", "TO")
                            pqty.Append(pqtystr)

                            'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.


                            'Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                            'itcuststr.SetValue("ZZCCIC", "1234")
                            ''hardcoded because of no field 

                            'itcuststr.SetValue("ZZDECT", CDec(Me.tb_ded.Text))

                            ''itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) commented since not found in FM
                            'itcuststr.SetValue("ZZFWGT", CDec(Me.tb_FIRSTQTY.Text)/1000)
                            'itcuststr.SetValue("ZZSWGT", CDec(Me.tb_SECONDQTY.Text)/1000)
                            'itcuststr.SetValue("ZZPACKD", CDec(Me.tb_packded.Text))
                            'itcust.Append(itcuststr)



                        Next


                    Else
                        Dim pqtystr As IRfcStructure = pqty.Metadata.LineType.CreateStructure
                        Dim rqty As Decimal = Convert.ToDecimal(tb_QTY.Text) / 1000
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        pqtystr.SetValue("ITM_NUMBER", 10)
                        pqtystr.SetValue("PICK_QTY", rqty)
                        pqtystr.SetValue("PICK_UOM", "TO")
                        pqty.Append(pqtystr)





                        'Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                        'itcuststr.SetValue("ZZCCIC", "1234")
                        ''hardcoded because of no field 

                        'itcuststr.SetValue("ZZDECT", CDec(Me.tb_ded.Text))

                        ''itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) commented since not found in FM
                        'itcuststr.SetValue("ZZFWGT", CDec(Me.tb_FIRSTQTY.Text)/1000)
                        'itcuststr.SetValue("ZZSWGT", CDec(Me.tb_SECONDQTY.Text)/1000)
                        'itcuststr.SetValue("ZZPACKD", CDec(Me.tb_packded.Text))
                        'itcust.Append(itcuststr)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    conn.Close()
                End Try




                Dim rttbl As IRfcTable = pgibi.GetTable("RETURN")
                Dim st As TimeSpan = Now.TimeOfDay
                pgibi.Invoke(saprfcdest)
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
                Catch ex As Exception
                    MsgBox(ex.Message & "From insering into SO Error Table")
                End Try
                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Billing Document # " & pgibi.GetValue("E_BILLINGDOC").ToString)
                    '& vbCrLf & "Delivery Note # " & pgibi.GetValue("E_DELIVERY").ToString _
                    '& vbCrLf & "Invoice # " & pgibi.GetValue("E_INVOICE").ToString _
                    'Me.tb_sapord.Text = sodnbil.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = sodnbil.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pgibi.GetValue("E_BILLINGDOC").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value 'pgibi.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value 'pgibi.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pgibi.GetValue("E_BILLINGDOC").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    Dim endtime = DateTime.Now.ToString()



                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From Main Sales Return")
            End Try

        End If
        'End if

    End Sub

    Private Sub ck_manual_CheckedChanged(sender As Object, e As EventArgs) Handles ck_manual.CheckedChanged
        If ck_manual.Checked = True Then
            Me.rtbDisplay.ReadOnly = False
        ElseIf ck_manual.Checked = True Then
            Me.rtbDisplay.ReadOnly = True
        End If
    End Sub

    
    Private Sub tb_orderno_LostFocus(sender As Object, e As EventArgs) Handles tb_orderno.LostFocus
        tb_dsno.Focus()
    End Sub
    
    Private Sub b_mixmat_Click(sender As Object, e As EventArgs) Handles b_mixmat.Click
        Try
            glbvar.vntwt = CInt(Me.tb_QTY.Text)
            glbvar.multdocno = Me.Tb_intdocno.Text
            glbvar.inout = Me.cb_inouttype.Text
            glbvar.multkt = Me.tb_ticketno.Text
            glbvar.sapdocmulti = Me.tb_sap_doc.Text
            glbvar.gsapordno = Me.tb_sapord.Text
            glbvar.gsapdocno = Me.tb_sapdocno.Text
            glbvar.gsapinvno = Me.tb_sapinvno.Text
            Dim mix As New MIX
            mix.Show()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
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

    Private Sub cb_inouttype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_inouttype.SelectedIndexChanged

    End Sub
End Class




















































































































































































































































































































































































































