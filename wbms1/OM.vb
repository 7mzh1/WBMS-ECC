Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

Public Class OM
    Private comm As New CommManager()
    Private comm2 As New CommManager2()
    Private comm3 As New CommManager3()
    Dim conn As New OracleConnection
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim constr, constrd As String
    Dim tot As Integer = 0
    Dim totprice As Integer = 0
    Dim sql As String
    Dim dpr As OracleDataAdapter
    Dim dpcc As OracleDataAdapter
    Dim tmode = 1
    Dim vmode As Integer
    Private transType As String = String.Empty
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Public ds As New DataSet
    Dim ds1 As New DataSet
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim omdasld As New OracleDataAdapter
    Dim omdssld As New DataSet
    Dim dadoc As New OracleDataAdapter
    Dim dsdoc As New DataSet
    Dim dfitm As New DataSet
    Dim dadr As New OracleDataAdapter
    Dim dsdr As New DataSet
    Dim id() As String
    Dim typ() As String
    Dim nmbr() As Integer
    Dim mesg() As String
    Dim tkt() As Decimal
    Dim rowchk As Integer
    Dim itmchar As String



    Private Sub wbmsom_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DataSet2.ACMSLEDGER' table. You can move, or remove it, as needed.

        Me.Text = Me.Text + " - " + glbvar.gcompname
        Me.tb_save.Visible = False
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
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
            dsitm.Clear()
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            daitm.Fill(dsitm)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        itmalloc = True

        DataGridView1.Rows.Clear()
        Me.tb_datein.Text = Today.Date
        glbvar.scaletype = "2"
        tmode = 1
    End Sub

    

    
   

    Private Sub listload()
        Me.ListView1.Items.Clear()
        For i = 0 To dsitm.Tables("itm").Rows.Count - 1
            Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
            Me.ListView1.Items(i).SubItems.Add(dsitm.Tables("itm").Rows(i).Item("ITEMDESC").ToString)
        Next
    End Sub
    Private Sub suplist()
        Me.loadven.Items.Clear()
        For i = 0 To dsitm.Tables("dssld").Rows.Count - 1
            Me.loadven.Items.Add(dsitm.Tables("dssld").Rows(i).Item("SLEDCODE").ToString)
            Me.loadven.Items(i).SubItems.Add(dsitm.Tables("dssld").Rows(i).Item("SLEDDESC").ToString)
        Next
    End Sub
 

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.ListView1.SelectedItems(0).SubItems(0).Text <> "" Then
                If tb_inout_type.Text <> "O" Then
                    Dim edate = Me.DataGridView1.CurrentRow.Cells("DATEOUT").Value
                    Dim format() = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy"}
                    Dim expenddt As Date = Date.ParseExact(edate, format,
                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                        Globalization.DateTimeStyles.None)
                    Dim tdate = expenddt.Day.ToString("D2")
                    Dim tmonth = expenddt.Month.ToString("D2")
                    Dim tyear = expenddt.Year
                    'Dim tdate = CDate(Me.DataGridView1.CurrentRow.Cells("DATEOUT").EditedFormattedValue).Day.ToString("D2")
                    'Dim tmonth = CDate(Me.DataGridView1.CurrentRow.Cells("DATEOUT").EditedFormattedValue).Month.ToString("D2")
                    'Dim tyear = CDate(Me.DataGridView1.CurrentRow.Cells("DATEOUT").EditedFormattedValue).Year
                    Dim docdate = tyear & tmonth & tdate
                    Dim it = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    'sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                    '        & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                    '        & " FROM   stitmratehd h, stitmrate t, smitem m" _
                    '        & " WHERE h.comp_code = t.comp_code" _
                    '        & " AND h.div_code = t.div_code" _
                    '        & " AND h.intrateno = t.intrateno" _
                    '        & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                    '        & " AND t.itemcode = " & "'" & it & "'" _
                    '        & " AND m.itemcode = t.itemcode" _
                    '        & " AND m.div_code = t.div_code" _
                    '        & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                    '        & " FROM   stitmratehd d where " _
                    '        & " to_number(to_char(d.witheffdt,'YYYYMMDD')) <= to_number(" & "'" & docdate & "')" _
                    '        & ")"
                    If Me.tb_custcategory.Text = "OST" Then
                        sql = " select z1.custlt,z1.kunnr,matnr,buy_price/1000 price,spl_price/1000 sellprice from ZCUST_PRICE_H z1,ZCUST_PRICE_I z2" _
                                & " where z1.custlt = z2.custlt" _
                                & " and z1.intprno = z2.intprno" _
                                & " and z2.kunnr = " & "'" & tb_osledcode.Text.PadLeft(10, "0") & "'" _
                                & " and z2.matnr = " & "'" & it & "'" _
                                & " AND z1.intprno = (SELECT   MAX (d.intprno)" _
                                & " FROM   ZCUST_PRICE_H d where" _
                                & " to_number(to_char(d.pricelist_date,'YYYYMMDD')) <= to_number(" & "'" & docdate & "'))"
                    Else
                        sql = " select z1.custlt,z1.kunnr,matnr,buy_price/1000 price,spl_price/1000 sellprice from ZCUST_PRICE_H z1,ZCUST_PRICE_I z2" _
                                & " where z1.custlt = z2.custlt" _
                                & " and z1.intprno = z2.intprno" _
                                & " and z2.kunnr = " & "'" & tb_omcustcode.Text.PadLeft(10, "0") & "'" _
                                & " and z2.matnr = " & "'" & it & "'" _
                                & " AND z1.intprno = (SELECT   MAX (d.intprno)" _
                                & " FROM   ZCUST_PRICE_H d where" _
                                & " to_number(to_char(d.pricelist_date,'YYYYMMDD')) <= to_number(" & "'" & docdate & "'))"
                    End If

                    dpr = New OracleDataAdapter(sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)
                    If dp.Tables(0).Rows.Count > 0 Then
                        Me.DataGridView1.CurrentRow.Cells("price").Value = dp.Tables(0).Rows(0).Item("price")
                        Me.DataGridView1.CurrentRow.Cells("OMPRICE").Value = dp.Tables(0).Rows(0).Item("sellprice")
                        Me.DataGridView1.CurrentRow.Cells("OMRATE").Value = dp.Tables(0).Rows(0).Item("sellprice")
                    Else
                        Me.DataGridView1.CurrentRow.Cells("price").Value = 0
                        Me.DataGridView1.CurrentRow.Cells("OMPRICE").Value = 0
                    End If
                End If
                    Me.DataGridView1.CurrentRow.Cells("itemname").Value = Me.ListView1.SelectedItems(0).SubItems(0).Text

                    Me.DataGridView1.CurrentRow.Cells("Itemcode").Value = Me.ListView1.SelectedItems(0).SubItems(1).Text

                    Me.ListView1.Visible = False

                End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub venlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadven.DoubleClick
        Try
           
            If Me.loadven.SelectedItems(0).SubItems(0).Text <> "" Then
                

                Me.tb_sledcode.Text = Me.loadven.SelectedItems(0).SubItems(0).Text

                Me.cb_sleddesc.Text = Me.loadven.SelectedItems(0).SubItems(1).Text

                Me.loadven.Visible = False

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_ok.Click
        If Me.DataGridView1.Rows.Count = 0 Then
            MsgBox("Enter Details")
        Else
            Try
                Dim cn As Integer = Me.DataGridView1.RowCount
                ReDim pitem(cn - 1)
                ReDim itmcde(cn - 1)
                ReDim itemdes(cn - 1)
                ReDim pqty(cn - 1)
                ReDim pmultided(cn - 1)
                ReDim ppricekg(cn - 1)
                ReDim prate(cn - 1)
                ReDim fwtp(cn - 1)
                ReDim swtp(cn - 1)
                ReDim gnetqty(cn - 1)
                ReDim gvalue(cn - 1)
                ReDim pomprice(cn - 1)
                ReDim gomrate(cn - 1)
                ReDim gsvalue(cn - 1)
                ReDim gdate(cn - 1)
                ReDim gomcusttkt(cn - 1)
                'ReDim ptktno(cn - 1)
                For i = 0 To cn - 1
                    pitem(i) = Me.DataGridView1.Rows(i).Cells("itemno").Value
                    itmcde(i) = Me.DataGridView1.Rows(i).Cells("Itemcode").Value
                    itemdes(i) = Me.DataGridView1.Rows(i).Cells("Itemname").Value
                    pqty(i) = Me.DataGridView1.Rows(i).Cells("qty").Value
                    pmultided(i) = Me.DataGridView1.Rows(i).Cells("deduction").Value
                    ppricekg(i) = Me.DataGridView1.Rows(i).Cells("price").Value
                    prate(i) = Me.DataGridView1.Rows(i).Cells("rate").Value
                    fwtp(i) = Me.DataGridView1.Rows(i).Cells("fwt").Value
                    swtp(i) = Me.DataGridView1.Rows(i).Cells("swt").Value
                    gnetqty(i) = Me.DataGridView1.Rows(i).Cells("netqty").Value
                    gvalue(i) = Me.DataGridView1.Rows(i).Cells("value").Value
                    pomprice(i) = Me.DataGridView1.Rows(i).Cells("omprice").Value
                    gomrate(i) = Me.DataGridView1.Rows(i).Cells("omrate").Value
                    gsvalue(i) = Me.DataGridView1.Rows(i).Cells("svalue").Value
                    Dim edate = Me.DataGridView1.Rows(i).Cells("DATEOUT").Value
                    Dim format() = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy"}
                    Dim expenddt As Date = Date.ParseExact(edate, format,
                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                        Globalization.DateTimeStyles.None)
                    gdate(i) = expenddt
                    gomcusttkt(i) = Me.DataGridView1.Rows(i).Cells("CTKT").Value
                    'ptktno(i) = Me.DataGridView1.Rows(i).Cells("TKTNO").Value

                Next
                'Me.tb_save.Visible = True
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        tb_save_Click()
    End Sub
    Private Sub DataGridView1_RowEnter() Handles DataGridView1.RowValidated
        Try
            Dim tot = 0
            Dim totprice = 0
            Dim totsal = 0
            For i = 0 To Me.DataGridView1.RowCount - 1
                tot = tot + Me.DataGridView1.Rows(i).Cells("NETQTY").FormattedValue
                totprice = totprice + Me.DataGridView1.Rows(i).Cells("VALUE").FormattedValue
                totsal = totsal + Me.DataGridView1.Rows(i).Cells("SVALUE").FormattedValue
            Next
            Me.tb_totqty.Text = tot
            Me.tb_totval.Text = totprice
            Me.tb_totsval.Text = totsal
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub tb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_retrieve.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            tmode = 2
            DataGridView1.Rows.Clear()
            Dim cns As Integer
            sql = " select count(itemcode) cnt from STWBMOM WHERE ticketno = " & Me.tb_rticketno.Text
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = " SELECT   INTDOCNO,INOUTTYPE,TICKETNO,VEHICLENO,CONTAINERNO,TRANSPORTER,ACCOUNTCODE,SLEDCODE,SLEDDESC,INTITEMCODE,ITEMCODE," _
         & "ITEMDESC,DCODE,DRIVERNAM,QTY,DEDUCTIONWT,PACKDED,DED,STATUS,DATEIN,TIMEIN,to_char(DATEOUT,'DD/MM/RRRR') dateout,TIMOUT,PRICETON,TOTALPRICE,RATE," _
         & "AUART,BSART,EKORG,EKGRP,VBELNS,VBELND,VBELNI,VKORG,VTWEG,SPART,SLNO,TRANS_CHARGE,PENALTY,MACHINE_CHARGE,LABOUR_CHARGE," _
         & "OMPRICE,OMSLEDCODE,OMSLEDDESC,REMARKS,TIME,FWT,SWT,NETQTY,VALUE,EBELN,MAT_DOC,BELNR,SVALUE,OMRATE,CUSTTKTNO,CUSTTYPE,TYPECODE,TYPECATG_PT from STWBMOM WHERE ticketno = " & Me.tb_rticketno.Text _
                  & "  order by slno desc "
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")

            For i = 0 To cns - 1
                DataGridView1.Rows.Insert(rowIndex:=0)
                Me.DataGridView1.Rows(0).Cells(0).Value = dp.Tables(0).Rows(i).Item("slno")
                Me.DataGridView1.Rows(0).Cells("itemcode").Value = dp.Tables(0).Rows(i).Item("Itemcode")
                Me.DataGridView1.Rows(0).Cells("itemname").Value = dp.Tables(0).Rows(i).Item("Itemdesc")
                Me.DataGridView1.Rows(0).Cells("qty").Value = dp.Tables(0).Rows(i).Item("qty")
                Me.DataGridView1.Rows(0).Cells("deduction").Value = dp.Tables(0).Rows(i).Item("DEDUCTIONWT")
                Me.DataGridView1.Rows(0).Cells("price").Value = dp.Tables(0).Rows(i).Item("priceton")
                Me.DataGridView1.Rows(0).Cells("rate").Value = dp.Tables(0).Rows(i).Item("rate")
                Me.DataGridView1.Rows(0).Cells("fwt").Value = dp.Tables(0).Rows(i).Item("fwt")
                Me.DataGridView1.Rows(0).Cells("swt").Value = dp.Tables(0).Rows(i).Item("swt")
                Me.DataGridView1.Rows(0).Cells("docno").Value = dp.Tables(0).Rows(i).Item("intdocno")
                Me.DataGridView1.Rows(0).Cells("tktno").Value = dp.Tables(0).Rows(i).Item("ticketno")
                Me.DataGridView1.Rows(0).Cells("inout").Value = dp.Tables(0).Rows(i).Item("INOUTTYPE")
                Me.DataGridView1.Rows(0).Cells("vcode").Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                Me.DataGridView1.Rows(0).Cells("vname").Value = dp.Tables(0).Rows(i).Item("SLEDDESC")
                Me.DataGridView1.Rows(0).Cells("sapdoc").Value = dp.Tables(0).Rows(i).Item("BSART")
                Me.DataGridView1.Rows(0).Cells("datein").Value = dp.Tables(0).Rows(i).Item("DATEIN")
                Me.DataGridView1.Columns("DATEOUT").DefaultCellStyle.Format = "dd/MM/yyyy"
                Me.DataGridView1.Rows(0).Cells("DATEOUT").Value = dp.Tables(0).Rows(i).Item("DATEOUT")
                Me.DataGridView1.Rows(0).Cells("TIMEIN").Value = dp.Tables(0).Rows(0).Item("TIMEIN")
                Me.DataGridView1.Rows(0).Cells("TIMOUT").Value = dp.Tables(0).Rows(0).Item("TIMOUT")
                'Me.DataGridView1.Rows(0).Cells("NUMBEROFPCS").Value = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
                Me.DataGridView1.Rows(0).Cells("LABOUR_CHARGE").Value = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                Me.DataGridView1.Rows(0).Cells("PENALTY").Value = dp.Tables(0).Rows(0).Item("PENALTY")
                Me.DataGridView1.Rows(0).Cells("MACHINE_CHARGE").Value = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                Me.DataGridView1.Rows(0).Cells("TRANS_CHARGE").Value = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")
                Me.DataGridView1.Rows(0).Cells("VEHICLENO").Value = dp.Tables(0).Rows(0).Item("VEHICLENO")
                Me.DataGridView1.Rows(0).Cells("REMARKS").Value = dp.Tables(0).Rows(0).Item("REMARKS")
                Me.DataGridView1.Rows(0).Cells("DRIVERNAM").Value = dp.Tables(0).Rows(0).Item("DRIVERNAM")
                Me.DataGridView1.Rows(0).Cells("DCODE").Value = dp.Tables(0).Rows(0).Item("DCODE")
                Me.DataGridView1.Rows(0).Cells("netqty").Value = dp.Tables(0).Rows(i).Item("netqty")
                Me.DataGridView1.Rows(0).Cells("value").Value = dp.Tables(0).Rows(i).Item("value")
                Me.DataGridView1.Rows(0).Cells("OMPRICE").Value = dp.Tables(0).Rows(i).Item("OMPRICE")
                Me.DataGridView1.Rows(0).Cells("OMRATE").Value = dp.Tables(0).Rows(i).Item("OMRATE")
                Me.DataGridView1.Rows(0).Cells("SVALUE").Value = dp.Tables(0).Rows(i).Item("SVALUE")
                Me.DataGridView1.Rows(0).Cells("CCODE").Value = dp.Tables(0).Rows(i).Item("OMSLEDCODE")
                Me.DataGridView1.Rows(0).Cells("CNAME").Value = dp.Tables(0).Rows(i).Item("OMSLEDDESC")
                Me.DataGridView1.Rows(0).Cells("CTKT").Value = dp.Tables(0).Rows(i).Item("CUSTTKTNO")
                Me.DataGridView1.Rows(0).Cells("DATEOUT").Value = dp.Tables(0).Rows(i).Item("DATEOUT")
                Me.DataGridView1.Rows(0).Cells("CUSTTYPE").Value = dp.Tables(0).Rows(i).Item("CUSTTYPE")
                Me.DataGridView1.Rows(0).Cells("TYPECODE").Value = dp.Tables(0).Rows(i).Item("TYPECODE")
                Me.DataGridView1.Rows(0).Cells("TYPECATG_PT").Value = dp.Tables(0).Rows(i).Item("TYPECATG_PT")
                'Me.DataGridView1.Rows(0).Cells("BUYER").Value = dp.Tables(0).Rows(0).Item("BUYER")
            Next
            Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("ticketno")
            Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("intdocno")
            Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
            Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
            Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
            Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
            Me.tb_DATEIN.Text = dp.Tables(0).Rows(0).Item("DATEIN")
            'Me.tb_dateout.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
            'Me.tb_timein.Text = dp.Tables(0).Rows(0).Item("TIMEIN")
            'If dp.Tables(0).Rows(0).Item("TIMOUT").ToString <> "" Then
            'Me.tb_timeout.Text = dp.Tables(0).Rows(0).Item("TIMOUT")
            ' End If

            Me.Tb_labourcharges.Text = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
            Me.Tb_penalty.Text = dp.Tables(0).Rows(0).Item("PENALTY")
            Me.Tb_eqpchrgs.Text = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
            Me.Tb_transp.Text = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")

            Me.Tb_vehicleno.Text = dp.Tables(0).Rows(0).Item("VEHICLENO").ToString

            Me.tb_comments.Text = dp.Tables(0).Rows(0).Item("REMARKS").ToString
            Me.tb_DRIVERNAM.Text = dp.Tables(0).Rows(0).Item("DRIVERNAM").ToString
            Me.cb_dcode.Text = dp.Tables(0).Rows(0).Item("DCODE").ToString
            Me.tb_omcustcode.Text = dp.Tables(0).Rows(0).Item("OMSLEDCODE").ToString
            Me.cb_omcustdesc.Text = dp.Tables(0).Rows(0).Item("OMSLEDDESC").ToString
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                Me.tb_CUSTTYPE.Text = dp.Tables(0).Rows(0).Item("CUSTTYPE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECODE"))) Then
                Me.tb_typecode.Text = dp.Tables(0).Rows(0).Item("TYPECODE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                Me.tb_typecatg_pt.Text = dp.Tables(0).Rows(0).Item("TYPECATG_PT")
            End If
            'Me.Tb_cust_ticket_no.Text = dp.Tables(0).Rows(0).Item("CUSTTKTNO").ToString
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNS"))) Then
                Me.tb_sapord.Text = dp.Tables(0).Rows(0).Item("VBELNS")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELND"))) Then
                Me.tb_sapdocno.Text = dp.Tables(0).Rows(0).Item("VBELND")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNI"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("VBELNI")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("EBELN"))) Then
                Me.tb_sono.Text = dp.Tables(0).Rows(0).Item("EBELN")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("MAT_DOC"))) Then
                Me.tb_delino.Text = dp.Tables(0).Rows(0).Item("MAT_DOC")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("BELNR"))) Then
                Me.tb_billno.Text = dp.Tables(0).Rows(0).Item("BELNR")
            End If
            If Me.tb_sapord.Text <> "" Then
                freeze_scr()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub tb_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_save.Click
    Private Sub tb_save_Click() ' Handles tb_save.Click
        'Private Sub tb_save_Click() Handles tb_save.Click
        'Dim cmd1 As New OracleCommand
        'Dim cmd2 As New OracleCommand
        'Dim cmd3 As New OracleCommand
        Try

            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If tmode = 1 Then
                sql = " select STWBMIBDSSEQ.nextval val from dual"
                dpr = New OracleDataAdapter(sql, conn)
                Dim dp As New DataSet
                dp.Clear()
                dpr.Fill(dp)
                If dp.Tables(0).Rows.Count > 0 Then
                    Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("val")
                End If
                'Else

                'cmd1.Connection = conn
                'cmd2.Connection = conn
                'cmd1.CommandText = " delete from STWBMOM where intdocno = " & Me.Tb_intdocno.Text
                'cmd2.CommandText = "commit"
                'cmd1.CommandType = CommandType.Text
                'cmd2.CommandType = CommandType.Text
                'cmd1.ExecuteNonQuery()


            End If
            'cmd3.Connection = conn
            'cmd3.CommandText = "rollback"
            'cmd3.CommandType = CommandType.Text
            Dim coun As Integer = Me.DataGridView1.RowCount

            'ReDim glbvar.pitem(coun - 1)
            ReDim glbvar.pindocn(coun - 1)

            ReDim glbvar.pino(coun - 1)
            ReDim glbvar.pvencode(coun - 1)
            ReDim glbvar.pvendesc(coun - 1)
            ReDim glbvar.psapdoccode(coun - 1)
            ReDim glbvar.p_DATEIN(coun - 1)
            ReDim glbvar.p_dateout(coun - 1)
            ReDim glbvar.p_timein(coun - 1)
            ReDim glbvar.p_timeout(coun - 1)
            ReDim glbvar.p_numberofpcs(coun - 1)
            ReDim glbvar.p_labourcharges(coun - 1)
            ReDim glbvar.p_penalty(coun - 1)
            ReDim glbvar.p_eqpchrgs(coun - 1)
            ReDim glbvar.p_transp(coun - 1)
            ReDim glbvar.p_cons_sen_branch(coun - 1)
            ReDim glbvar.p_orderno(coun - 1)
            ReDim glbvar.p_dsno(coun - 1)
            ReDim glbvar.p_asno(coun - 1)
            ReDim glbvar.p_IBDSNO(coun - 1)
            ReDim glbvar.p_ccic(coun - 1)
            ReDim glbvar.p_vehicleno(coun - 1)
            ReDim glbvar.p_oth_ven_cust(coun - 1)
            ReDim glbvar.p_comments(coun - 1)
            ReDim glbvar.p_DRIVERNAM(coun - 1)
            ReDim glbvar.p_dcode(coun - 1)
            ReDim glbvar.p_buyer(coun - 1)
            ReDim glbvar.ptktno(coun - 1)
            'ReDim glbvar.gdate(coun - 1)
            ReDim glbvar.gomcustcode(coun - 1)
            ReDim glbvar.gomcustname(coun - 1)
            ReDim glbvar.gcusttype(coun - 1)
            ReDim glbvar.gtypecode(coun - 1)
            ReDim glbvar.gtypecatg_pt(coun - 1)
            'ReDim glbvar.gomcusttkt(coun - 1)
            For i = 0 To coun - 1
                glbvar.pindocn(i) = CInt(Me.Tb_intdocno.Text)
                glbvar.ptktno(i) = CDec(Me.tb_ticketno.Text)
                glbvar.pino(i) = Me.tb_inout_type.Text
                glbvar.pvencode(i) = Me.tb_sledcode.Text
                glbvar.pvendesc(i) = Me.cb_sleddesc.Text
                glbvar.psapdoccode(i) = Me.tb_sap_doc.Text
                Dim dtin As Date = FormatDateTime(Me.tb_datein.Text, DateFormat.GeneralDate)
                glbvar.p_DATEIN(i) = dtin 'CDate(tb_DATEIN.Text)
                'Dim dtout As Date = FormatDateTime(Me.tb_dateout.Text, DateFormat.GeneralDate)
                'glbvar.p_dateout(i) = dtout
                glbvar.p_timein(i) = Me.tb_timein.Text
                glbvar.p_timeout(i) = Me.tb_timeout.Text
                'glbvar.p_numberofpcs(i) = Me.tb_numberofpcs.Text
                glbvar.p_labourcharges(i) = Me.Tb_labourcharges.Text
                glbvar.p_penalty(i) = Me.Tb_penalty.Text
                glbvar.p_eqpchrgs(i) = Me.Tb_eqpchrgs.Text
                glbvar.p_transp(i) = Me.Tb_transp.Text
                'glbvar.p_cons_sen_branch(i) = Me.Tb_cons_sen_branch.Text
                'glbvar.p_orderno(i) = Me.tb_orderno.Text
                'glbvar.p_dsno(i) = Me.tb_dsno.Text
                'glbvar.p_asno(i) = Me.Tb_asno.Text
                'glbvar.p_IBDSNO(i) = Me.tb_IBDSNO.Text
                'glbvar.p_ccic(i) = Me.Tb_ccic.Text
                glbvar.p_vehicleno(i) = Me.Tb_vehicleno.Text
                glbvar.p_oth_ven_cust(i) = Me.tb_oth_ven_cust.Text
                glbvar.p_comments(i) = Me.tb_comments.Text
                glbvar.p_DRIVERNAM(i) = Me.tb_DRIVERNAM.Text
                glbvar.p_dcode(i) = Me.cb_dcode.Text
                glbvar.p_buyer(i) = Me.tb_buyer.Text
                glbvar.gomcustcode(i) = Me.tb_omcustcode.Text
                glbvar.gomcustname(i) = Me.cb_omcustdesc.Text
                glbvar.gcusttype(i) = Me.tb_CUSTTYPE.Text
                glbvar.gtypecode(i) = Me.tb_typecode.Text
                glbvar.gtypecatg_pt(i) = Me.tb_typecatg_pt.Text
                'glbvar.gomcusttkt(i) = Me.Tb_cust_ticket_no.Text
            Next
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_om"
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

            Dim pTICKETNO As OracleParameter = New OracleParameter(":p3", OracleDbType.Decimal)
            pTICKETNO.Direction = ParameterDirection.Input
            pTICKETNO.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pTICKETNO.Value = glbvar.ptktno

            Dim ppvencode As OracleParameter = New OracleParameter("p4:", OracleDbType.Varchar2)
            ppvencode.Direction = ParameterDirection.Input
            ppvencode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppvencode.Value = glbvar.pvencode

            Dim ppvendesc As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            ppvendesc.Direction = ParameterDirection.Input
            ppvendesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppvendesc.Value = glbvar.pvendesc

            Dim ppsapdoc As OracleParameter = New OracleParameter("p6:", OracleDbType.Varchar2)
            ppsapdoc.Direction = ParameterDirection.Input
            ppsapdoc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppsapdoc.Value = glbvar.psapdoccode

            Dim ppdatein As OracleParameter = New OracleParameter("p7:", OracleDbType.Date)
            ppdatein.Direction = ParameterDirection.Input
            ppdatein.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdatein.Value = glbvar.p_DATEIN

            Dim ppdateout As OracleParameter = New OracleParameter("p8:", OracleDbType.Date)
            ppdateout.Direction = ParameterDirection.Input
            ppdateout.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdateout.Value = glbvar.gdate

            Dim pptimein As OracleParameter = New OracleParameter("p9:", OracleDbType.Varchar2)
            pptimein.Direction = ParameterDirection.Input
            pptimein.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pptimein.Value = glbvar.p_timein

            Dim pptimout As OracleParameter = New OracleParameter("p10:", OracleDbType.Varchar2)
            pptimout.Direction = ParameterDirection.Input
            pptimout.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pptimout.Value = glbvar.p_timeout

            Dim pplab As OracleParameter = New OracleParameter("p12:", OracleDbType.Decimal)
            pplab.Direction = ParameterDirection.Input
            pplab.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pplab.Value = glbvar.p_labourcharges

            Dim pppenalty As OracleParameter = New OracleParameter("p13:", OracleDbType.Decimal)
            pppenalty.Direction = ParameterDirection.Input
            pppenalty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pppenalty.Value = glbvar.p_penalty

            Dim ppeqp As OracleParameter = New OracleParameter("p14:", OracleDbType.Decimal)
            ppeqp.Direction = ParameterDirection.Input
            ppeqp.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppeqp.Value = glbvar.p_eqpchrgs

            Dim pptrans As OracleParameter = New OracleParameter("p15:", OracleDbType.Decimal)
            pptrans.Direction = ParameterDirection.Input
            pptrans.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pptrans.Value = glbvar.p_transp

            Dim ppvehicle As OracleParameter = New OracleParameter("p22:", OracleDbType.Varchar2)
            ppvehicle.Direction = ParameterDirection.Input
            ppvehicle.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppvehicle.Value = glbvar.p_vehicleno

            Dim ppothvc As OracleParameter = New OracleParameter("p23:", OracleDbType.Varchar2)
            ppothvc.Direction = ParameterDirection.Input
            ppothvc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppothvc.Value = glbvar.p_oth_ven_cust

            Dim ppcomm As OracleParameter = New OracleParameter("p24:", OracleDbType.Varchar2)
            ppcomm.Direction = ParameterDirection.Input
            ppcomm.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppcomm.Value = glbvar.p_comments

            Dim ppdrname As OracleParameter = New OracleParameter("p25:", OracleDbType.Varchar2)
            ppdrname.Direction = ParameterDirection.Input
            ppdrname.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdrname.Value = glbvar.p_DRIVERNAM

            Dim ppdcode As OracleParameter = New OracleParameter("p26:", OracleDbType.Varchar2)
            ppdcode.Direction = ParameterDirection.Input
            ppdcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdcode.Value = glbvar.p_dcode

            Dim ppbuyer As OracleParameter = New OracleParameter("p26:", OracleDbType.Varchar2)
            ppbuyer.Direction = ParameterDirection.Input
            ppbuyer.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppbuyer.Value = glbvar.p_buyer

            Dim pslno As OracleParameter = New OracleParameter("p27", OracleDbType.Int32)
            pslno.Direction = ParameterDirection.Input
            pslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pslno.Value = glbvar.pitem

            Dim pITEMCODE As OracleParameter = New OracleParameter("p28", OracleDbType.Varchar2)
            pITEMCODE.Direction = ParameterDirection.Input
            pITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pITEMCODE.Value = glbvar.itmcde

            Dim pITEMDESC As OracleParameter = New OracleParameter(":p29", OracleDbType.Varchar2)
            pITEMDESC.Direction = ParameterDirection.Input
            pITEMDESC.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pITEMDESC.Value = glbvar.itemdes

            Dim pQTY As OracleParameter = New OracleParameter(":p30", OracleDbType.Decimal)
            pQTY.Direction = ParameterDirection.Input
            pQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pQTY.Value = glbvar.pqty

            Dim pdedQTY As OracleParameter = New OracleParameter(":p31", OracleDbType.Decimal)
            pdedQTY.Direction = ParameterDirection.Input
            pdedQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pdedQTY.Value = glbvar.pmultided

            Dim pprice As OracleParameter = New OracleParameter(":p32", OracleDbType.Decimal)
            pprice.Direction = ParameterDirection.Input
            pprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pprice.Value = glbvar.ppricekg

            Dim ptotprice As OracleParameter = New OracleParameter(":p33", OracleDbType.Decimal)
            ptotprice.Direction = ParameterDirection.Input
            ptotprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ptotprice.Value = glbvar.prate

            Dim pfwt As OracleParameter = New OracleParameter(":p38", OracleDbType.Decimal)
            pfwt.Direction = ParameterDirection.Input
            pfwt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pfwt.Value = glbvar.fwtp

            Dim pswt As OracleParameter = New OracleParameter(":p39", OracleDbType.Decimal)
            pswt.Direction = ParameterDirection.Input
            pswt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pswt.Value = glbvar.swtp

            Dim pnetqty As OracleParameter = New OracleParameter(":p40", OracleDbType.Decimal)
            pnetqty.Direction = ParameterDirection.Input
            pnetqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pnetqty.Value = glbvar.gnetqty

            Dim pvalue As OracleParameter = New OracleParameter(":p41", OracleDbType.Decimal)
            pvalue.Direction = ParameterDirection.Input
            pvalue.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pvalue.Value = glbvar.gvalue

            Dim ppomprice As OracleParameter = New OracleParameter(":p39", OracleDbType.Decimal)
            ppomprice.Direction = ParameterDirection.Input
            ppomprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppomprice.Value = glbvar.pomprice

            Dim pomrate As OracleParameter = New OracleParameter(":p40", OracleDbType.Decimal)
            pomrate.Direction = ParameterDirection.Input
            pomrate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomrate.Value = glbvar.gomrate

            Dim psvalue As OracleParameter = New OracleParameter(":p41", OracleDbType.Decimal)
            psvalue.Direction = ParameterDirection.Input
            psvalue.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            psvalue.Value = glbvar.gsvalue

            Dim pomcustcode As OracleParameter = New OracleParameter(":p42", OracleDbType.Varchar2)
            pomcustcode.Direction = ParameterDirection.Input
            pomcustcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomcustcode.Value = glbvar.gomcustcode

            Dim pomcustname As OracleParameter = New OracleParameter(":p43", OracleDbType.Varchar2)
            pomcustname.Direction = ParameterDirection.Input
            pomcustname.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomcustname.Value = glbvar.gomcustname

            Dim pcusttktno As OracleParameter = New OracleParameter(":p44", OracleDbType.Varchar2)
            pcusttktno.Direction = ParameterDirection.Input
            pcusttktno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pcusttktno.Value = glbvar.gomcusttkt

            Dim pomcusttype As OracleParameter = New OracleParameter(":p45", OracleDbType.Varchar2)
            pomcusttype.Direction = ParameterDirection.Input
            pomcusttype.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomcusttype.Value = glbvar.gcusttype

            Dim pomtypecode As OracleParameter = New OracleParameter(":p46", OracleDbType.Varchar2)
            pomtypecode.Direction = ParameterDirection.Input
            pomtypecode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomtypecode.Value = glbvar.gtypecode

            Dim pomtypecatg_pt As OracleParameter = New OracleParameter(":p47", OracleDbType.Varchar2)
            pomtypecatg_pt.Direction = ParameterDirection.Input
            pomtypecatg_pt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomtypecatg_pt.Value = glbvar.gtypecatg_pt

            cmd.Parameters.Add(pINTDOCNO)
            cmd.Parameters.Add(pINOUTTYPE)
            cmd.Parameters.Add(pTICKETNO)
            cmd.Parameters.Add(pslno)
            cmd.Parameters.Add(ppvencode)
            cmd.Parameters.Add(ppvendesc)
            cmd.Parameters.Add(ppsapdoc)
            cmd.Parameters.Add(ppdatein)
            cmd.Parameters.Add(ppdateout)
            cmd.Parameters.Add(pptimein)
            cmd.Parameters.Add(pptimout)
            cmd.Parameters.Add(pplab)
            cmd.Parameters.Add(pppenalty)
            cmd.Parameters.Add(ppeqp)
            cmd.Parameters.Add(pptrans)
            cmd.Parameters.Add(ppvehicle)
            'cmd.Parameters.Add(ppothvc)
            cmd.Parameters.Add(ppcomm)
            cmd.Parameters.Add(ppdrname)
            cmd.Parameters.Add(ppdcode)
            'cmd.Parameters.Add(ppbuyer)
            cmd.Parameters.Add(pITEMCODE)
            cmd.Parameters.Add(pITEMDESC)
            cmd.Parameters.Add(pQTY)
            cmd.Parameters.Add(pdedQTY)
            cmd.Parameters.Add(pprice)
            cmd.Parameters.Add(ptotprice)
            cmd.Parameters.Add(pfwt)
            cmd.Parameters.Add(pswt)
            cmd.Parameters.Add(pnetqty)
            cmd.Parameters.Add(pvalue)
            cmd.Parameters.Add(ppomprice)
            cmd.Parameters.Add(pomrate)
            cmd.Parameters.Add(psvalue)
            cmd.Parameters.Add(pomcustcode)
            cmd.Parameters.Add(pomcustname)
            cmd.Parameters.Add(pcusttktno)
            cmd.Parameters.Add(pomcusttype)
            cmd.Parameters.Add(pomtypecode)
            cmd.Parameters.Add(pomtypecatg_pt)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Decimal)).Value = Me.tb_ticketno.Text
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")
            'cmd2.ExecuteNonQuery()
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()
        Catch ex As Exception
            'cmd3.ExecuteNonQuery()
            MsgBox(ex.Message.ToString)
        End Try
        'DataGridView1.Rows.Clear()
        Me.tb_save.Visible = False
        tmode = 2
        'conn.Close()


    End Sub

    Private Sub b_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            glbvar.vintdocno = Me.Tb_intdocno.Text
            'Scaleprint.Show()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    Private Sub b_exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_exit.Click
        comm.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()
    End Sub




    Private Sub b_new_Click(sender As Object, e As EventArgs) Handles b_new.Click
        clr_scr()
        unfreeze_scr()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMOM WBM WHERE INOUTTYPE = 'I' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If


            cmbloading()
            Me.tb_sap_doc.Text = "QO"
            Me.cb_sap_docu_type.Text = "OM Process"
            tmode = 1
            tb_inout_type.Text = "I"
            tb_inout_desc.Text = "Incoming Goods"

            Me.cb_sleddesc.Text = "One Time Vendor"
            Me.tb_sledcode.Text = "0000050004"
            Me.cb_omcustdesc.Text = "Other Customer"
            Me.tb_omcustcode.Text = "0001000000"
            Me.tb_ticketno.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub cmbloading()

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
            cb_sleddesc.DataSource = dssld.Tables("sled")
            cb_sleddesc.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sleddesc.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'itemcode
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
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        'Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.custmstom"
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
    End Sub
    Private Sub cmbloading1()

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
            cb_sleddesc.DataSource = dssld.Tables("sled")
            cb_sleddesc.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sleddesc.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
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
    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

        If Me.DataGridView1.CurrentCell.ColumnIndex = 1 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)

            RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress

        End If
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DataGridView1.CurrentCell.ColumnIndex = 3 Then
                Dim tb1 As TextBox = CType(sender, TextBox)
                'itmchar = ""
                'If te <> "" Then
                'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
                If tb1.Text.Length > 0 Then

                    Dim foundrow() As DataRow
                    Dim expression As String = "ITEMDESC LIKE '" & tb1.Text & "%'" & ""
                    foundrow = dsitm.Tables("itm").Select(expression)
                    ListView1.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView1.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
                        Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView1.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    
    'Private Sub sledcode_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles tb_sledcode.TextChanged
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
    'Private Sub tb_sledcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_sledcode.KeyPress
    '    Dim tb As TextBox = CType(e.Control, TextBox)

    '    RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress
    '    AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
    'End Sub
    'Private Sub cb_sleddesc_LostFocus(sender As Object, e As EventArgs) Handles cb_sleddesc.SelectedIndexChanged
    '    If Me.cb_sleddesc.SelectedIndex <> -1 Then
    '        Me.tb_sledcode.Text = Me.cb_sleddesc.SelectedValue.ToString
    '        Dim foundrow() As DataRow
    '        Dim expression As String = "SLEDCODE = '" & Me.tb_sledcode.Text & "'" & ""
    '        foundrow = dssld.Tables("sled").Select(expression)
    '        If foundrow.Count > 1 Then
    '            MsgBox("More number of records found for the supplier")
    '        End If
    '    End If
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles b_add.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                DataGridView1.Rows.Insert(rowIndex:=0)
                DataGridView1.Rows(0).Cells(0).Value = 10
                rowchk = 10
            ElseIf DataGridView1.Rows.Count > 0 Then
                DataGridView1.Rows.Insert(rowIndex:=DataGridView1.Rows.Count)
                rowchk = rowchk + 10
                DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(0).Value = rowchk
                DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles b_delete.Click
        Try

            Me.DataGridView1.Rows.Remove(Me.DataGridView1.CurrentRow)
            rowchk = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                rowchk = rowchk + 10
                DataGridView1.Rows(i).Cells(0).Value = rowchk
            Next
        Catch ex As Exception
            MsgBox("Add rows to delete")
        End Try
    End Sub
    Private Sub cb_sleddesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sleddesc.SelectedIndexChanged
        Try
            If Me.cb_sleddesc.SelectedIndex <> -1 Then
                Me.tb_sledcode.Text = Me.cb_sleddesc.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "SLEDCODE = '" & Me.tb_sledcode.Text & "'" & ""
                foundrow = dssld.Tables("sled").Select(expression)
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the supplier")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cb_omcustdesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_omcustdesc.SelectedIndexChanged
        Try
            Me.tb_CUSTTYPE.Text = ""
            Me.tb_typecode.Text = ""
            Me.tb_typecatg_pt.Text = ""
            If Me.cb_omcustdesc.SelectedIndex <> -1 Then
                Me.tb_omcustcode.Text = Me.cb_omcustdesc.SelectedValue.ToString
                Me.DataGridView1.Rows.Clear()
                Me.tb_dateout.Text = ""
                Dim foundrow() As DataRow
                Dim expression As String = "SLEDCODE = '" & Me.tb_omcustcode.Text & "'" & ""
                foundrow = omdssld.Tables("sled").Select(expression)
                Me.tb_custcategory.Text = ""
                If foundrow.Count > 0 Then
                    If Not IsDBNull(foundrow(0).ItemArray(3)) Then
                        Me.tb_custcategory.Text = foundrow(0).ItemArray(3)
                        Me.tb_osledcode.Text = foundrow(0).ItemArray(4)
                        'Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(5)
                        'Me.tb_typecode.Text = foundrow(0).ItemArray(6)
                        'Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(7)
                    End If
                    If Not IsDBNull(foundrow(0).ItemArray(5)) Then
                        Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(5)
                        Me.tb_typecode.Text = foundrow(0).ItemArray(6)
                        Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(7)
                    End If
                End If
                If foundrow.Count > 1 Then
                    MsgBox("More number of records found for the supplier")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub b_purchase_Click(sender As Object, e As EventArgs) Handles b_purchase.Click
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
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        conn.Close()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_multi"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            conn.Close()
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
        ElseIf Me.cb_sleddesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.tb_sledcode.Focus()
        ElseIf Me.cb_omcustdesc.Text = "" Then
            MsgBox("Select a Customer")
            Me.cb_omcustdesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '   MsgBox("Select an itemcode")
            '  Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '   MsgBox(" First Qty cannot be blank")
            '  Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '   MsgBox(" Second Qty cannot be blank")
            '  Me.b_edit.Focus()
            'ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
            '   MsgBox("Please enter a price")
            'ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            '   MsgBox("Please enter a price")
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
                pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                pohdrin.SetValue("CREAT_DATE", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))

                Dim pohdrinx As IRfcStructure = pogrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrinx.SetValue("CREATED_BY", "X")
                pohdrinx.SetValue("DOC_DATE", "X")
                pohdrinx.SetValue("CREAT_DATE", "X")



                pogrir.SetValue("I_CUSTNO", Me.tb_omcustcode.Text.PadLeft(10, "0"))
                Dim cc = Me.tb_omcustcode.Text.PadLeft(10, "0")
                'pogrir.SetValue("I_OMCUSTPRICE", Me.tb_omcustprice.Text)

                Dim pocst As IRfcStructure = pogrir.GetStructure("I_POHEADERCUST")
                ' Create field in transaction taable and bring from hremployee table
                pocst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                'pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_datein.Text).Year & CDate(tb_datein.Text).Month.ToString("D2") & CDate(tb_datein.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
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
                
                Try
                    
                        cmd.Connection = conn
                        cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_om"
                        cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000
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
                        pozfstru.SetValue("PO_ITEM", CInt(dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString))
                        pozfstru.SetValue("ZZCTKT", dsmltitm.Tables("mltitm").Rows(a).Item("CUSTTKTNO").ToString())
                        Dim d = CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Year & CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Month.ToString("D2") & CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Day.ToString("D2")
                        pozfstru.SetValue("ZZCTKT_D", CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Year & CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Month.ToString("D2") & CDate(dsmltitm.Tables("mltitm").Rows(a).Item("DATEOUT").ToString()).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                            'pozfstru.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                            'pozfstru.SetValue("ZZDNAME", Me.cb_dcode.SelectedValue.ToString)
                            'pozfstru.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
                            'pozfstru.SetValue("ZZBNAME", "JAWED")
                            'pozfstru.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
                        'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()) / 1000)
                        'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()) / 1000)
                        'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text) / 1000)
                        ' pozfstru.SetValue("ZZFTUOM", "MT")
                        ' pozfstru.SetValue("ZZSECUOM", "MT")
                            pozf.Append(pozfstru)
                            Dim omcustmult As IRfcTable = pogrir.GetTable("T_OM_ITEM_PRICE")
                            Dim omcustmultu As IRfcStructure = omcustmult.Metadata.LineType.CreateStructure
                            omcustmultu.SetValue("POSNR", itm)
                        Dim omval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("OMRATE").ToString() * 1000)
                            omcustmultu.SetValue("NETPR", omval)
                            omcustmult.Append(omcustmultu)



                        Next
                  

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
                DataGridView2.Refresh()
                For j = 0 To poerr.RowCount - 1
                    DataGridView2.Rows.Add()
                    DataGridView2.Rows(j).Cells("TYPE").Value = poerr(j).Item("Type").GetString()
                    If poerr(j).Item("Type").GetString() = "E" Then
                        poercnt = poercnt + 1
                    End If
                    DataGridView2.Rows(j).Cells("I_D").Value = poerr(j).Item("ID").GetString() 'err.GetValue("I_D")
                    DataGridView2.Rows(j).Cells("NUMBER").Value = poerr(j).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView2.Rows(j).Cells("MESAGE").Value = poerr(j).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
                    typ(j) = poerr(j).Item("Type").GetString()
                    id(j) = poerr(j).Item("ID").GetString()
                    nmbr(j) = poerr(j).Item("NUMBER").GetString()
                    mesg(j) = poerr(j).Item("MESSAGE").GetString()
                    tkt(j) = CDec(Me.tb_ticketno.Text)
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

                    Dim ptkt As OracleParameter = New OracleParameter(":n3", OracleDbType.Decimal)
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
                    Me.tb_sono.Text = pogrir.GetValue("E_SALEORDER").ToString
                    Me.tb_delino.Text = pogrir.GetValue("E_DELIVERY").ToString
                    Me.tb_billno.Text = pogrir.GetValue("E_BILLINGNO").ToString
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_om"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                       
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = pogrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pEBELN", OracleDbType.Int64)).Value = pogrir.GetValue("E_SALEORDER").ToString
                        cmd.Parameters.Add(New OracleParameter("pMAT_DOC", OracleDbType.Char)).Value = pogrir.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pBELNR", OracleDbType.Char)).Value = pogrir.GetValue("E_BILLINGNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_ticketno.Text)
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

        ' Add any initialization after the InitializeComponent() call.'    tb_save_Click()

    End Sub
    

    Private Sub tb_ticketno_LostFocus(sender As Object, e As EventArgs) Handles tb_ticketno.LostFocus

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
                & "  FROM   STWBMOM WBM" _
                & " WHERE WBM.TICKETNO = " & Me.tb_ticketno.Text _
                & " and status in (1,2,3)"

            Dim da = New OracleDataAdapter(sql, conn)
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
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'Dim cmd As New OracleCommand
            'cmd.Connection = conn
            'cmd.Parameters.Clear()
            'cmd.CommandText = "curspkg_join.tktrng"
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
            'cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
            'If tb_inout_type.Text = "I" Then
            '    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "IWS"
            'ElseIf tb_inout_type.Text = "O" Then
            '    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "DSS"
            'ElseIf tb_inout_type.Text = "T" Then
            '    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SNA"
            'ElseIf tb_inout_type.Text = "S" Then
            '    cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCL"
            'End If
            'cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            'Try
            '    Dim dsrng As New DataSet
            '    Dim darng As New OracleDataAdapter(cmd)
            '    darng.TableMappings.Add("Table", "tktrng")
            '    darng.Fill(dsrng)
            '    If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
            '        Me.DataGridView1.Focus()
            '    Else
            '        MsgBox("Ticket number not in range should be within " & dsrng.Tables("tktrng").Rows(0).Item("STARTNO") & " - " & dsrng.Tables("tktrng").Rows(0).Item("ENDNO"))
            '        Me.tb_ticketno.Focus()
            '    End If
            '    conn.Close()
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            '    conn.Close()
            'End Try


        End If 'tmode enddif

    End Sub

    Private Sub b_clear_Click(sender As Object, e As EventArgs) Handles b_clear.Click
        clr_scr()
    End Sub
    Private Sub clr_scr()
        Try
           
            Me.Tb_transp.Text = 0
            Me.Tb_labourcharges.Text = 0
            Me.Tb_eqpchrgs.Text = 0
            Me.Tb_penalty.Text = 0
            Me.cb_sleddesc.Text = ""
            Me.tb_sledcode.Text = ""
            Me.cb_omcustdesc.Text = ""
            Me.tb_omcustcode.Text = ""
            Me.tb_dateout.Text = ""
            Me.Tb_cust_ticket_no.Text = 0
            Me.tb_ticketno.Text = 0
            Me.tb_totqty.Text = 0
            Me.tb_totval.Text = 0
            Me.tb_totsval.Text = 0
            Me.tb_searchbyno.Text = ""
            Me.Tb_vehicleno.Text = ""
            Me.tb_buyer.Text = ""
            Me.tb_DRIVERNAM.Text = ""
            Me.cb_dcode.Text = ""
            Me.tb_datein.Text = Today.Date
            Me.tb_dateout.Text = ""
            Me.tb_timein.Text = ""
            Me.tb_timeout.Text = ""
            Me.tb_comments.Text = ""
            Me.Tb_intdocno.Text = ""
            Me.cb_sap_docu_type.Text = ""
            Me.tb_sap_doc.Text = ""
            Me.tb_oth_ven_cust.Text = ""
            Me.tb_inout_type.Text = ""
            Me.tb_inout_desc.Text = ""
            Me.b_purchase.Visible = True
            tmode = 1
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView2.Rows.Clear()
            Me.tb_sapord.Text = ""
            Me.tb_sapdocno.Text = ""
            Me.tb_sapinvno.Text = ""
            Me.tb_sono.Text = ""
            Me.tb_delino.Text = ""
            Me.tb_billno.Text = ""
            Me.tb_CUSTTYPE.Text = ""
            Me.tb_typecode.Text = ""
            Me.tb_typecatg_pt.Text = ""
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated

        Try
            If Me.DataGridView1.CurrentRow.Cells("Deduction").Selected Or Me.DataGridView1.CurrentRow.Cells("QTY").Selected Then
                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").EditedFormattedValue
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
                Me.DataGridView1.CurrentRow.Cells("SVALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("OMRATE").Value
            ElseIf Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
            ElseIf Me.DataGridView1.CurrentRow.Cells("OMRATE").Selected Then
                Me.DataGridView1.CurrentRow.Cells("SVALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("OMRATE").EditedFormattedValue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            If Me.DataGridView1.CurrentRow.Cells("DATEOUT").Selected Then
                Dim edate = Me.DataGridView1.CurrentRow.Cells("DATEOUT").Value.ToString
                Dim format() = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy"}
                Dim expenddt As Date = Date.ParseExact(edate, format,
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    Globalization.DateTimeStyles.None)
                If expenddt.Month.ToString("D2") <> CDate(Me.tb_DATEIN.Text).Month.ToString("D2") AndAlso Me.tb_custcategory.Text <> "OST" Then
                    Me.cb_omcustdesc.Text = ""
                    Me.tb_omcustcode.Text = ""
                    Me.cb_omcustdesc.Focus()
                    MsgBox("Customer Cleared, Select Outstanding customer for this date")
                End If
            End If
            If Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                        & " WHERE z1.userauth_no = z2.userauth_no" _
                        & " AND z1.username = z2.userid" _
                        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells("Itemcode").Value & "'" _
                        & " and Z1.INTAUTHNO =  (SELECT   MAX (d.INTAUTHNO) " _
                        & " FROM   ZUSER_AUTH_H d " _
                        & " where username = " & "'" & glbvar.userid & "'" & ")"

                Dim dpct = New OracleDataAdapter(sql, conn)
                Dim dpc As New DataSet
                dpc.Clear()
                dpct.Fill(dpc)
                Dim user_tol_value As Decimal
                Dim user_tot_allowed As Decimal
                Dim pct = dpc.Tables(0).Rows(0).Item("pct")
                Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                user_tol_value = pct * plist
                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                If pct <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + user_tol_value
                ElseIf amt <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + amt / 1000
                End If
                If Me.tb_inout_type.Text = "I" Then
                    If Me.tb_sap_doc.Text = "QO" Then
                        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_tot_allowed Then

                            MsgBox("Price not matching as the latest Pricelist")
                            Me.tb_ok.Enabled = False
                            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True

                        Else
                            Me.tb_ok.Enabled = True
                            '    tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                        End If
                        'Else
                        'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                    'ElseIf Me.tb_inout_type.Text = "O" Then
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_dateout_ValueChanged(sender As Object, e As EventArgs) Handles tb_dateout.Validated
        If CDate(Me.tb_dateout.Text) > Today.Date Then
            MsgBox("Date should not be greater than Todays Date")
            Me.tb_dateout.Text = Today.Date
        End If
        If CDate(Me.tb_dateout.Text).Month.ToString("D2") <> CDate(Today.Date).Month.ToString("D2") And Me.tb_custcategory.Text <> "OST" Then
            Me.cb_omcustdesc.Text = ""
            Me.tb_omcustcode.Text = ""
            Me.cb_omcustdesc.Focus()
        End If
    End Sub

    Private Sub tb_DATEIN_lostfocus(sender As Object, e As EventArgs) Handles tb_datein.Validated
        If CDate(Me.tb_datein.Text) > Today.Date Then
            MsgBox("Date should not be greater than Todays Date")
            Me.tb_datein.Text = Today.Date
        End If
        'Dim tdate = Me.tb_datein.Text
        'Dim format() = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy"}
        'Dim texpenddt As Date = Date.ParseExact(tdate, format,
        '    System.Globalization.DateTimeFormatInfo.InvariantInfo,
        '    Globalization.DateTimeStyles.None)

        'Dim c = texpenddt - Today.Date.AddDays(-30)
        If Me.tb_datein.Text < Today.Date.AddDays(-30) Then
            MsgBox("Document Date cannot be less than 30 days from today")
            Me.tb_datein.Text = Today.Date
            Me.tb_datein.Focus()
        End If


    End Sub

    Private Sub freeze_scr()
        Me.tb_ok.Enabled = False
        Me.tb_save.Enabled = False
        Me.b_purchase.Enabled = False
    End Sub
    Private Sub unfreeze_scr()
        Me.tb_ok.Enabled = True
        Me.tb_save.Enabled = True
        Me.b_purchase.Enabled = True
    End Sub
   
    
    
    
    
    
    
    
    
    
    
    
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
'Public Class DataGridViewDisableButtonColumn
'    Inherits DataGridViewButtonColumn

'    Public Sub New()
'        Me.CellTemplate = New DataGridViewDisableButtonCell()
'    End Sub
'End Class

'Public Class DataGridViewDisableButtonCell
'    Inherits DataGridViewButtonCell

'    Private enabledValue As Boolean
'    Public Property Enabled() As Boolean
'        Get
'            Return enabledValue
'        End Get
'        Set(ByVal value As Boolean)
'            enabledValue = value
'        End Set
'    End Property

'    ' Override the Clone method so that the Enabled property is copied. 
'    Public Overrides Function Clone() As Object
'        Dim Cell As DataGridViewDisableButtonCell = _
'            CType(MyBase.Clone(), DataGridViewDisableButtonCell)
'        Cell.Enabled = Me.Enabled
'        Return Cell
'    End Function

'    ' By default, enable the button cell. 
'    Public Sub New()
'        Me.enabledValue = True
'    End Sub

'    Protected Overrides Sub Paint(ByVal graphics As Graphics, _
'        ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, _
'        ByVal rowIndex As Integer, _
'        ByVal elementState As DataGridViewElementStates, _
'        ByVal value As Object, ByVal formattedValue As Object, _
'        ByVal errorText As String, _
'        ByVal cellStyle As DataGridViewCellStyle, _
'        ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, _
'        ByVal paintParts As DataGridViewPaintParts)

'        ' The button cell is disabled, so paint the border,   
'        ' background, and disabled button for the cell. 
'        If Not Me.enabledValue Then

'            ' Draw the background of the cell, if specified. 
'            If (paintParts And DataGridViewPaintParts.Background) = _
'                DataGridViewPaintParts.Background Then

'                Dim cellBackground As New SolidBrush(cellStyle.BackColor)
'                graphics.FillRectangle(cellBackground, cellBounds)
'                cellBackground.Dispose()
'            End If

'            ' Draw the cell borders, if specified. 
'            If (paintParts And DataGridViewPaintParts.Border) = _
'                DataGridViewPaintParts.Border Then

'                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, _
'                    advancedBorderStyle)
'            End If

'            ' Calculate the area in which to draw the button. 
'            Dim buttonArea As Rectangle = cellBounds
'            Dim buttonAdjustment As Rectangle = _
'                Me.BorderWidths(advancedBorderStyle)
'            buttonArea.X += buttonAdjustment.X
'            buttonArea.Y += buttonAdjustment.Y
'            buttonArea.Height -= buttonAdjustment.Height
'            buttonArea.Width -= buttonAdjustment.Width

'            ' Draw the disabled button.                
'            ButtonRenderer.DrawButton(graphics, buttonArea, _
'                PushButtonState.Disabled)

'            ' Draw the disabled button text.  
'            If TypeOf Me.FormattedValue Is String Then
'                TextRenderer.DrawText(graphics, CStr(Me.FormattedValue), _
'                    Me.DataGridView.Font, buttonArea, SystemColors.GrayText)
'            End If

'        Else
'            ' The button cell is enabled, so let the base class  
'            ' handle the painting. 
'            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, _
'                elementState, value, formattedValue, errorText, _
'                cellStyle, advancedBorderStyle, paintParts)
'        End If
'    End Sub

'End Class