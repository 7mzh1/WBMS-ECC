Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector

Public Class prwbmsis
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
    Dim daprld As New OracleDataAdapter
    Dim dsprld As New DataSet
    Dim dapsld As New OracleDataAdapter
    Dim dspsld As New DataSet
    Dim dacsld As New OracleDataAdapter
    Dim dscsld As New DataSet
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
    Dim tkt() As Integer
    Dim rowchk As Integer


    Private Sub wbmsis_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim constr As String = My.Settings.Item("ConnString")
        'Array.Clear(pindocn, 0, pindocn.Length)
        'Array.Clear(ptktno, 0, ptktno.Length)
        'Array.Clear(pino, 0, pino.Length)
        'Array.Clear(intiem, 0, intiem.Length)
        'Array.Clear(itmcde, 0, itmcde.Length)
        'Array.Clear(itemdes, 0, itemdes.Length)
        'Array.Clear(pqty, 0, pqty.Length)
        'Array.Clear(pfswt, 0, pfswt.Length)
        'Array.Clear(pscwt, 0, pscwt.Length)
        'intiem = Nothing
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
            'listload()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        itmalloc = True
        'If DataGridView1.Rows.Count > 0 Then
        '    For i = 0 To DataGridView1.Rows.Count - 1
        '        DataGridView1.Rows
        '    Next
        'End If
        DataGridView1.Rows.Clear()
        Me.tb_DATE.Text = Today.Date
        glbvar.scaletype = "1"
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If Me.DataGridView1.CurrentRow.Cells("W1").Selected Then
                Me.DataGridView1.CurrentRow.Cells("QTY").Value = Me.rtbDisplay.Text
                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").Value
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
            ElseIf Me.DataGridView1.CurrentRow.Cells("W2").Selected Then
                Me.DataGridView1.CurrentRow.Cells("QTY").Value = Me.rtbDisplay2.Text
                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").Value
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
            ElseIf Me.DataGridView1.CurrentRow.Cells("W3").Selected Then
                Me.DataGridView1.CurrentRow.Cells("QTY").Value = Me.rtbDisplay3.Text
                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").Value
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
    'Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
    Private Sub DataGridView1_RowEnter() Handles DataGridView1.RowValidated
        Try
            Dim tot = 0
            Dim totprice = 0
            For i = 0 To Me.DataGridView1.RowCount - 1
                tot = tot + Me.DataGridView1.Rows(i).Cells("NETQTY").FormattedValue
                totprice = totprice + Me.DataGridView1.Rows(i).Cells("VALUE").FormattedValue
            Next
            Me.tb_totqty.Text = tot
            Me.tb_totval.Text = totprice
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
    '    Try
    '        If Me.DataGridView1.CurrentRow.Cells("Deduction").Selected Then
    '            Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").EditedFormattedValue
    '            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
    '        ElseIf Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
    '            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        'If DataGridView1.CurrentRow.Cells(1).Selected = True Then
        '    'DataGridView1.Rows.Insert(rowIndex:=0)
        '    ListView1.Visible = True
        '    listload()
        'End If
        'If DataGridView1.CurrentRow.Cells(1).Selected = False Then
        '    ListView1.Visible = False
        'End If
    End Sub

    Private Sub listload()
        Me.ListView1.Items.Clear()
        For i = 0 To dsitm.Tables("itm").Rows.Count - 1
            Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
            Me.ListView1.Items(i).SubItems.Add(dsitm.Tables("itm").Rows(i).Item("ITEMDESC").ToString)
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
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    Dim tdate = CDate(tb_DATE.Text).Day.ToString("D2")
                    Dim tmonth = CDate(tb_DATE.Text).Month.ToString("D2")
                    Dim tyear = CDate(tb_DATE.Text).Year
                    Dim docdate = tyear & tmonth & tdate
                    'Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    ''& " 'AND t.itemcode = " & "'" & 'Me.ListView1.SelectedItems(0).SubItems(1).Text & "'" _
                    Dim it = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                            & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                            & " FROM   stitmratehd h, stitmrate t, smitem_pr m" _
                            & " WHERE h.comp_code = t.comp_code" _
                            & " AND h.div_code = t.div_code" _
                            & " AND h.intrateno = t.intrateno" _
                            & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                            & " AND t.itemcode = " & "'" & it & "'" _
                            & " AND m.itemcode = t.itemcode" _
                            & " AND m.div_code = t.div_code" _
                            & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                            & " FROM   stitmratehd d where " _
                            & " to_number(to_char(d.witheffdt,'YYYYMMDD')) <= to_number(" & "'" & docdate & "')" _
                            & " and d.div_code = h.div_code" _
                            & ")"

                    dpr = New OracleDataAdapter(sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)
                    If dp.Tables(0).Rows.Count > 0 Then
                        Me.DataGridView1.CurrentRow.Cells("price").Value = dp.Tables(0).Rows(0).Item("price")
                    End If
                ElseIf tb_inout_type.Text = "O" Then
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    Dim tdate = CDate(tb_DATE.Text).Day.ToString("D2")
                    Dim tmonth = CDate(tb_DATE.Text).Month.ToString("D2")
                    Dim tyear = CDate(tb_DATE.Text).Year
                    Dim docdate = tyear & tmonth & tdate
                    'Dim expenddt As Date = Date.ParseExact(docdate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    ''& " 'AND t.itemcode = " & "'" & 'Me.ListView1.SelectedItems(0).SubItems(1).Text & "'" _
                    Dim it = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                            & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                            & " FROM   stitmratehd h, stitmrate t, smitem_pr m" _
                            & " WHERE h.comp_code = t.comp_code" _
                            & " AND h.div_code = t.div_code" _
                            & " AND h.intrateno = t.intrateno" _
                            & " AND h.div_code = " & "'" & glbvar.divcd & "'" _
                            & " AND t.itemcode = " & "'" & it & "'" _
                            & " AND m.itemcode = t.itemcode" _
                            & " AND m.div_code = t.div_code" _
                            & " AND h.intrateno = (SELECT   MAX (d.intrateno)" _
                            & " FROM   stitmratehd d where " _
                            & " to_number(to_char(d.witheffdt,'YYYYMMDD')) <= to_number(" & "'" & docdate & "')" _
                            & ")"

                    dpr = New OracleDataAdapter(sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)
                    If dp.Tables(0).Rows.Count > 0 Then
                        Me.DataGridView1.CurrentRow.Cells("price").Value = dp.Tables(0).Rows(0).Item("price")
                    End If
                End If
                Me.DataGridView1.CurrentRow.Cells("itemname").Value = Me.ListView1.SelectedItems(0).SubItems(0).Text

                Me.DataGridView1.CurrentRow.Cells("Itemcode").Value = Me.ListView1.SelectedItems(0).SubItems(1).Text

                Me.DataGridView1.CurrentRow.Cells("uom").Value = Me.ListView1.SelectedItems(0).SubItems(2).Text

                Me.ListView1.Visible = False

            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_connect.Click
        Try
            comm.Parity = "None"
            comm.StopBits = 1
            comm.DataBits = 8
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
    Private Sub b_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Disconnect.Click
        Try
            comm.ClosePort()
            b_Disconnect.Visible = False
            b_connect.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_ok.Click
        Try
            Dim cn As Integer = Me.DataGridView1.RowCount
            ReDim pitem(cn - 1)
            ReDim itmcde(cn - 1)
            ReDim itemdes(cn - 1)
            ReDim pqty(cn - 1)
            ReDim pmultided(cn - 1)
            ReDim ppricekg(cn - 1)
            ReDim prate(cn - 1)
            ReDim gnetqty(cn - 1)
            ReDim gvalue(cn - 1)
            ReDim guom(cn - 1)
            For i = 0 To cn - 1
                pitem(i) = Me.DataGridView1.Rows(i).Cells("intitemcode").Value
                itmcde(i) = Me.DataGridView1.Rows(i).Cells("itemcode").Value
                itemdes(i) = Me.DataGridView1.Rows(i).Cells("ItemName").Value
                pqty(i) = Me.DataGridView1.Rows(i).Cells("qty").Value
                pmultided(i) = Me.DataGridView1.Rows(i).Cells("deduction").Value
                ppricekg(i) = Me.DataGridView1.Rows(i).Cells("price").Value
                prate(i) = Me.DataGridView1.Rows(i).Cells("rate").Value
                gnetqty(i) = Me.DataGridView1.Rows(i).Cells("netqty").Value
                gvalue(i) = Me.DataGridView1.Rows(i).Cells("value").Value
                guom(i) = Me.DataGridView1.Rows(i).Cells("uom").Value
            Next
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        tb_save_Click()
    End Sub



    Private Sub tb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_retrieve.Click
        Try
            tmode = 2
            DataGridView1.Rows.Clear()
            Dim cns As Integer
            sql = " select count(itemcode) cnt from STWBMIS_PR WHERE ticketno = " & Me.tb_searchtkt.Text
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = " select * from STWBMIS_PR WHERE ticketno = " & Me.tb_searchtkt.Text _
                  & "  order by slno desc "
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")

            For i = 0 To cns - 1
                DataGridView1.Rows.Insert(rowIndex:=0)
                Me.DataGridView1.Rows(0).Cells("intitemcode").Value = dp.Tables(0).Rows(i).Item("slno")
                Me.DataGridView1.Rows(0).Cells("itemcode").Value = dp.Tables(0).Rows(i).Item("Itemcode")
                Me.DataGridView1.Rows(0).Cells("itemname").Value = dp.Tables(0).Rows(i).Item("Itemdesc")
                Me.DataGridView1.Rows(0).Cells("qty").Value = dp.Tables(0).Rows(i).Item("qty")
                Me.DataGridView1.Rows(0).Cells("deduction").Value = dp.Tables(0).Rows(i).Item("DEDUCTIONWT")
                Me.DataGridView1.Rows(0).Cells("price").Value = dp.Tables(0).Rows(i).Item("priceton")
                Me.DataGridView1.Rows(0).Cells("rate").Value = dp.Tables(0).Rows(i).Item("rate")
                Me.DataGridView1.Rows(0).Cells("netqty").Value = dp.Tables(0).Rows(i).Item("netqty")
                Me.DataGridView1.Rows(0).Cells("value").Value = dp.Tables(0).Rows(i).Item("value")
                Me.DataGridView1.Rows(0).Cells("docno").Value = dp.Tables(0).Rows(i).Item("intdocno")
                Me.DataGridView1.Rows(0).Cells("TKTNO").Value = dp.Tables(0).Rows(i).Item("ticketno")
                Me.DataGridView1.Rows(0).Cells("INOUT").Value = dp.Tables(0).Rows(i).Item("INOUTTYPE")
                Me.DataGridView1.Rows(0).Cells("VCODE").Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                Me.DataGridView1.Rows(0).Cells("VName").Value = dp.Tables(0).Rows(i).Item("SLEDDESC")
                Me.DataGridView1.Rows(0).Cells("drcode").Value = dp.Tables(0).Rows(i).Item("DCODE")
                Me.DataGridView1.Rows(0).Cells("drname").Value = dp.Tables(0).Rows(i).Item("DRIVERNAM")
                Me.DataGridView1.Rows(0).Cells("SAPDOC").Value = dp.Tables(0).Rows(i).Item("BSART")
                Me.DataGridView1.Rows(0).Cells("SAPDATE").Value = dp.Tables(0).Rows(i).Item("DATEOUT")
                Me.DataGridView1.Rows(0).Cells("CUSTTYPE").Value = dp.Tables(0).Rows(i).Item("CUSTTYPE")
                Me.DataGridView1.Rows(0).Cells("TYPECODE").Value = dp.Tables(0).Rows(i).Item("TYPECODE")
                Me.DataGridView1.Rows(0).Cells("TYPECATG_PT").Value = dp.Tables(0).Rows(i).Item("TYPECATG_PT")
                Me.DataGridView1.Rows(0).Cells("UOM").Value = dp.Tables(0).Rows(i).Item("UOM")
                Me.DataGridView1.Rows(0).Cells("post_date").Value = dp.Tables(0).Rows(i).Item("post_date")
                Me.DataGridView1.Rows(0).Cells("remarks").Value = dp.Tables(0).Rows(i).Item("remarks")
                Me.DataGridView1.Rows(0).Cells("divdesc").Value = dp.Tables(0).Rows(i).Item("divdesc")
                Me.DataGridView1.Rows(0).Cells("gpremarks").Value = dp.Tables(0).Rows(i).Item("gpremarks")
                Me.DataGridView1.Rows(0).Cells("(PRSLEDCODE ").Value = dp.Tables(0).Rows(i).Item("(PRSLEDCODE ")
                Me.DataGridView1.Rows(0).Cells("(PRSLEDDESC ").Value = dp.Tables(0).Rows(i).Item("(PRSLEDDESC ")
                Me.DataGridView1.Rows(0).Cells("(PRSUPPCODE ").Value = dp.Tables(0).Rows(i).Item("(PRSUPPCODE ")
                Me.DataGridView1.Rows(0).Cells("(PRSUPPDESC ").Value = dp.Tables(0).Rows(i).Item("(PRSUPPDESC ")
            Next
            DataGridView1_RowEnter()
            Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("ticketno")
            Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("intdocno")
            Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
            Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
            Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
            Me.tb_DRIVERNAM.Text = dp.Tables(0).Rows(0).Item("DCODE")
            Me.cb_dcode.Text = dp.Tables(0).Rows(0).Item("DRIVERNAM")
            Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
            Me.tb_DATE.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
            Me.d_newdate.Text = dp.Tables(0).Rows(0).Item("POST_DATE")
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("REMARKS"))) Then
                Me.tb_comments.Text = dp.Tables(0).Rows(0).Item("REMARKS")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("gpremarks"))) Then
                Me.rtb_gprem.Text = dp.Tables(0).Rows(0).Item("gpremarks")
            End If
            'If CDate(Me.tb_DATE.Text).Month < Today.Month Then
            '    Me.d_newdate.Enabled = True
            'Else
            '    Me.d_newdate.Enabled = False
            'End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                Me.tb_CUSTTYPE.Text = dp.Tables(0).Rows(0).Item("CUSTTYPE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECODE"))) Then
                Me.tb_typecode.Text = dp.Tables(0).Rows(0).Item("TYPECODE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                Me.tb_typecatg_pt.Text = dp.Tables(0).Rows(0).Item("TYPECATG_PT")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNS"))) Then
                Me.tb_sapord.Text = dp.Tables(0).Rows(0).Item("VBELNS")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELND"))) Then
                Me.tb_sapdocno.Text = dp.Tables(0).Rows(0).Item("VBELND")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNI"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("VBELNI")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("(PRSLEDCODE"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("(PRSLEDCODE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("(PRSLEDDESC"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("(PRSLEDDESC")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("(PRSUPPCODE"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("(PRSUPPCODE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("(PRSUPPDESC"))) Then
                Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("(PRSUPPDESC")
            End If
            If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
                'Me.B_PO.Visible = False
                'Me.Button1.Visible = False
                freeze_scr()
            Else
                'If Me.tb_inout_type.Text = "I" Then
                '    b_purchase.Enabled = True
                '    b_purchase.Visible = True
                'ElseIf Me.tb_inout_type.Text = "O" Then
                '    b_deliver.Enabled = True
                '    b_deliver.Visible = True
                'End If
            End If
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub tb_save_Click() 'Handles tb_save.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If tmode = 1 Then
                sql = " select STWBMIBDSSEQ_PR.nextval val from dual"
                dpr = New OracleDataAdapter(sql, conn)
                Dim dp As New DataSet
                dp.Clear()
                dpr.Fill(dp)
                If dp.Tables(0).Rows.Count > 0 Then
                    Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("val")
                End If
            Else
                Dim cmd1 As New OracleCommand()
                Dim cmd2 As New OracleCommand()
                cmd1.Connection = conn
                cmd2.Connection = conn
                cmd1.CommandText = " delete from STWBMIS_PR where intdocno = " & Me.Tb_intdocno.Text
                cmd2.CommandText = "commit"
                cmd1.CommandType = CommandType.Text
                cmd2.CommandType = CommandType.Text
                cmd1.ExecuteNonQuery()
                cmd2.ExecuteNonQuery()

            End If
            Dim coun As Integer = Me.DataGridView1.RowCount
            'ReDim glbvar.pitem(coun - 1)
            ReDim glbvar.pindocn(coun - 1)
            ReDim glbvar.ptktno(coun - 1)
            ReDim glbvar.pino(coun - 1)
            ReDim glbvar.pvencode(coun - 1)
            ReDim glbvar.pvendesc(coun - 1)
            ReDim glbvar.psapdoccode(coun - 1)
            ReDim glbvar.p_DATEIN(coun - 1)
            ReDim glbvar.gcusttype(coun - 1)
            ReDim glbvar.gtypecode(coun - 1)
            ReDim glbvar.gtypecatg_pt(coun - 1)
            ReDim glbvar.giPOSTDATE(coun - 1)
            ReDim glbvar.sscomments(coun - 1)
            ReDim glbvar.gcompnamegp(coun - 1)
            ReDim glbvar.ggpremarks(coun - 1)
            ReDim glbvar.pdcode(coun - 1)
            ReDim glbvar.pdname(coun - 1)
            ReDim glbvar.PRSLEDCODE(coun - 1)
            ReDim glbvar.PRSLEDDESC(coun - 1)
            ReDim glbvar.PRSUPPCODE(coun - 1)
            ReDim glbvar.PRSUPPDESC(coun - 1)
            For i = 0 To coun - 1
                glbvar.pindocn(i) = CInt(Me.Tb_intdocno.Text)
                glbvar.ptktno(i) = CInt(Me.tb_ticketno.Text)
                glbvar.pino(i) = Me.tb_inout_type.Text
                glbvar.pvencode(i) = Me.tb_sledcode.Text
                glbvar.pvendesc(i) = Me.cb_sleddesc.Text
                glbvar.psapdoccode(i) = Me.tb_sap_doc.Text
                glbvar.p_DATEIN(i) = CDate(tb_DATE.Text)
                glbvar.gcusttype(i) = Me.tb_CUSTTYPE.Text
                glbvar.gtypecode(i) = Me.tb_typecode.Text
                glbvar.gtypecatg_pt(i) = Me.tb_typecatg_pt.Text
                glbvar.giPOSTDATE(i) = Me.d_newdate.Text
                glbvar.sscomments(i) = Me.tb_comments.Text
                glbvar.gcompnamegp(i) = glbvar.gcompname
                glbvar.ggpremarks(i) = rtb_gprem.Text
                glbvar.pdcode(i) = Me.tb_DRIVERNAM.Text
                glbvar.pdname(i) = Me.cb_dcode.Text
                glbvar.PRSLEDCODE(i) = Me.tb_prjsledesc.Text
                glbvar.PRSLEDDESC(i) = Me.cb_prjsledcode.Text
                glbvar.PRSUPPCODE(i) = Me.tb_crcode.Text
                glbvar.PRSUPPDESC(i) = Me.cb_crsldc.Text
            Next
            conn.Close()
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_isarr"
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

            Dim ppdocdate As OracleParameter = New OracleParameter("p7:", OracleDbType.Date)
            ppdocdate.Direction = ParameterDirection.Input
            ppdocdate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdocdate.Value = glbvar.p_DATEIN

            Dim pslno As OracleParameter = New OracleParameter("p8", OracleDbType.Int32)
            pslno.Direction = ParameterDirection.Input
            pslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pslno.Value = glbvar.pitem

            Dim pITEMCODE As OracleParameter = New OracleParameter("p9", OracleDbType.Varchar2)
            pITEMCODE.Direction = ParameterDirection.Input
            pITEMCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pITEMCODE.Value = glbvar.itmcde

            Dim pITEMDESC As OracleParameter = New OracleParameter(":p10", OracleDbType.Varchar2)
            pITEMDESC.Direction = ParameterDirection.Input
            pITEMDESC.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pITEMDESC.Value = glbvar.itemdes

            Dim pQTY As OracleParameter = New OracleParameter(":p11", OracleDbType.Decimal)
            pQTY.Direction = ParameterDirection.Input
            pQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pQTY.Value = glbvar.pqty

            Dim pdedQTY As OracleParameter = New OracleParameter(":p12", OracleDbType.Decimal)
            pdedQTY.Direction = ParameterDirection.Input
            pdedQTY.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pdedQTY.Value = glbvar.pmultided

            Dim pprice As OracleParameter = New OracleParameter(":p13", OracleDbType.Decimal)
            pprice.Direction = ParameterDirection.Input
            pprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pprice.Value = glbvar.ppricekg

            Dim ptotprice As OracleParameter = New OracleParameter(":p14", OracleDbType.Decimal)
            ptotprice.Direction = ParameterDirection.Input
            ptotprice.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ptotprice.Value = glbvar.prate

            Dim pnetqty As OracleParameter = New OracleParameter(":p15", OracleDbType.Decimal)
            pnetqty.Direction = ParameterDirection.Input
            pnetqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pnetqty.Value = glbvar.gnetqty

            Dim pvalue As OracleParameter = New OracleParameter(":p16", OracleDbType.Decimal)
            pvalue.Direction = ParameterDirection.Input
            pvalue.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pvalue.Value = glbvar.gvalue

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

            Dim ppuom As OracleParameter = New OracleParameter(":p48", OracleDbType.Varchar2)
            ppuom.Direction = ParameterDirection.Input
            ppuom.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppuom.Value = glbvar.guom

            Dim ppostdate As OracleParameter = New OracleParameter(":p49", OracleDbType.Date)
            ppostdate.Direction = ParameterDirection.Input
            ppostdate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppostdate.Value = glbvar.giPOSTDATE

            Dim ppcomments As OracleParameter = New OracleParameter(":p50", OracleDbType.Varchar2)
            ppcomments.Direction = ParameterDirection.Input
            ppcomments.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppcomments.Value = glbvar.sscomments

            Dim pdivdesc As OracleParameter = New OracleParameter(":p51", OracleDbType.Varchar2)
            pdivdesc.Direction = ParameterDirection.Input
            pdivdesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pdivdesc.Value = glbvar.gcompnamegp

            Dim pgprem As OracleParameter = New OracleParameter(":p52", OracleDbType.Varchar2)
            pgprem.Direction = ParameterDirection.Input
            pgprem.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pgprem.Value = glbvar.ggpremarks

            Dim ppdcode As OracleParameter = New OracleParameter("p4:", OracleDbType.Varchar2)
            ppdcode.Direction = ParameterDirection.Input
            ppdcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdcode.Value = glbvar.pdcode

            Dim ppdname As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            ppdname.Direction = ParameterDirection.Input
            ppdname.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdname.Value = glbvar.pdname

            Dim PPRSLEDCODE As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            PPRSLEDCODE.Direction = ParameterDirection.Input
            PPRSLEDCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            PPRSLEDCODE.Value = glbvar.PRSLEDCODE

            Dim PPRSLEDDESC As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            PPRSLEDDESC.Direction = ParameterDirection.Input
            PPRSLEDDESC.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            PPRSLEDDESC.Value = glbvar.PRSLEDDESC

            Dim PPRSUPPCODE As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            PPRSUPPCODE.Direction = ParameterDirection.Input
            PPRSUPPCODE.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            PPRSUPPCODE.Value = glbvar.PRSUPPCODE

            Dim PPRSUPPDESC As OracleParameter = New OracleParameter("p5:", OracleDbType.Varchar2)
            PPRSUPPDESC.Direction = ParameterDirection.Input
            PPRSUPPDESC.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            PPRSUPPDESC.Value = glbvar.PRSUPPDESC

            cmd.Parameters.Add(pINTDOCNO)
            cmd.Parameters.Add(pINOUTTYPE)
            cmd.Parameters.Add(pTICKETNO)
            cmd.Parameters.Add(pslno)
            cmd.Parameters.Add(ppvencode)
            cmd.Parameters.Add(ppvendesc)
            cmd.Parameters.Add(ppsapdoc)
            cmd.Parameters.Add(ppdocdate)
            cmd.Parameters.Add(pITEMCODE)
            cmd.Parameters.Add(pITEMDESC)
            cmd.Parameters.Add(pQTY)
            cmd.Parameters.Add(pdedQTY)
            cmd.Parameters.Add(pprice)
            cmd.Parameters.Add(ptotprice)
            cmd.Parameters.Add(pnetqty)
            cmd.Parameters.Add(pvalue)
            cmd.Parameters.Add(pomcusttype)
            cmd.Parameters.Add(pomtypecode)
            cmd.Parameters.Add(pomtypecatg_pt)
            cmd.Parameters.Add(ppuom)
            cmd.Parameters.Add(ppostdate)
            cmd.Parameters.Add(ppcomments)
            cmd.Parameters.Add(pdivdesc)
            cmd.Parameters.Add(pgprem)
            cmd.Parameters.Add(ppdcode)
            cmd.Parameters.Add(ppdname)
            cmd.Parameters.Add(PPRSLEDCODE)
            cmd.Parameters.Add(PPRSLEDDESC)
            cmd.Parameters.Add(PPRSUPPCODE)
            cmd.Parameters.Add(PPRSUPPDESC)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")
            If Me.tb_inout_type.Text = "I" Then
                Me.b_purchase.Visible = True
                Me.b_purchase.Enabled = True
            ElseIf Me.tb_inout_type.Text = "O" Then
                Me.b_deliver.Visible = True
                Me.b_deliver.Enabled = True
            End If
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        'DataGridView1.Rows.Clear()
        Me.tb_save.Visible = False
        tmode = 2
    End Sub
    Private Sub cb_prjsledcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_prjsledcode.SelectedIndexChanged
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        If Me.cb_prjsledcode.SelectedIndex <> -1 Then
            Me.tb_prjsledesc.Text = Me.cb_prjsledcode.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_prjsledesc.Text & "'" & ""
            foundrow = dspsld.Tables("sledprj").Select(expression)
            If foundrow.Count > 0 Then
                If Not IsDBNull(foundrow(0).ItemArray(5)) Then
                    Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(3)
                    Me.tb_typecode.Text = foundrow(0).ItemArray(4)
                    Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(5)
                End If
            End If
        End If
    End Sub
    Private Sub cb_crjsledcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_crsldc.SelectedIndexChanged
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        If Me.cb_crsldc.SelectedIndex <> -1 Then
            Me.tb_crcode.Text = Me.cb_crsldc.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_prjsledesc.Text & "'" & ""
            foundrow = dscsld.Tables("sledcrj").Select(expression)
            If foundrow.Count > 0 Then
                If Not IsDBNull(foundrow(0).ItemArray(5)) Then
                    Me.tb_CUSTTYPE.Text = foundrow(0).ItemArray(3)
                    Me.tb_typecode.Text = foundrow(0).ItemArray(4)
                    Me.tb_typecatg_pt.Text = foundrow(0).ItemArray(5)
                End If
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
            foundrow = dspsld.Tables("sledprj").Select(expression)
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
    Private Sub tb_crjsrchbyno_TextChanged(sender As Object, e As EventArgs) Handles tb_csr.TextChanged
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
            Dim expression As String = "SLEDCODE LIKE '" & Me.tb_csr.Text & "%'" & ""
            'foundrow = dspsld.Tables("sledcrj").Select(expression)
            foundrow = dscsld.Tables("sledcrj").Select(expression)
            loadcr.Items.Clear()
            For i = 0 To foundrow.Count - 1
                'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                Me.loadcr.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
                Me.loadcr.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)
            Next
            loadcr.Visible = True
            'End If
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loadcr_DoubleClick(sender As Object, e As EventArgs) Handles loadcr.DoubleClick
        Try

            If Me.loadcr.SelectedItems(0).SubItems(0).Text <> "" Then


                Me.tb_crcode.Text = Me.loadcr.SelectedItems(0).SubItems(0).Text

                Me.cb_crsldc.Text = Me.loadcr.SelectedItems(0).SubItems(1).Text

                Me.loadcr.Visible = False

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub b_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_print.Click
        Try
            Dim apr = ""
            glbvar.vintdocno = Me.Tb_intdocno.Text
            glbvar.gdoccode = Me.tb_sap_doc.Text
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "select gpremarks from stwbmis_pr where ticketno = " & tb_ticketno.Text
            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            Try
                da.TableMappings.Add("Table", "prt")
                da.Fill(dstk)
                conn.Close()
                If Not (IsDBNull(dstk.Tables("prt").Rows(0).Item("gpremarks"))) Then
                    apr = dstk.Tables("prt").Rows(0).Item("gpremarks")
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
            If apr = "X" Then
                MsgBox("Ticket is printed already")
            Else
                Form5.Show()
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim cmd As New OracleCommand
                cmd.Connection = conn
                ' Check if it has got multiple items.
                cmd.Parameters.Clear()
                cmd.CommandText = "update stwbmis_pr set gpremarks = 'X' where ticketno =" & tb_ticketno.Text
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                conn.Close()
                'Form5.Close()

            End If

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

    Private Sub tb_ticketno_LostFocus(sender As Object, e As EventArgs) Handles tb_ticketno.LostFocus
        If Me.tb_inout_type.Text <> "E" Then


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
                & "  FROM   STWBMIS_pr WBM" _
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
            If tb_inout_type.Text = "I" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "IWS"
            ElseIf tb_inout_type.Text = "O" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "DSS"
            End If
            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                Dim dsrng As New DataSet
                Dim darng As New OracleDataAdapter(cmd)
                darng.TableMappings.Add("Table", "tktrng")
                darng.Fill(dsrng)
                'If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                '    Me.cb_sleddesc.Focus()
                'Else
                '    MsgBox("Ticket number not in range should be within " & dsrng.Tables("tktrng").Rows(0).Item("STARTNO") & " - " & dsrng.Tables("tktrng").Rows(0).Item("ENDNO"))
                '    Me.tb_ticketno.Focus()
                'End If
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try
            conn.Close()
        End If
        'tmode enddif
    End Sub

    Private Sub b_new_Click(sender As Object, e As EventArgs) Handles b_new.Click
        clr_scr()
        unfreeze_scr()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIS_pr WBM WHERE INOUTTYPE = 'I' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If


            cmbloading()
            Me.tb_sap_doc.Text = "QD"
            tmode = 1
            tb_inout_type.Text = "I"
            tb_inout_desc.Text = "Incoming Goods"

            Me.cb_sleddesc.Text = "Dummy Supplier"
            Me.tb_sledcode.Text = "0000000000"
            Me.tb_DRIVERNAM.Text = "OTH"
            Me.cb_dcode.Text = "Other Driver"
            Me.cb_prjsledcode.Text = "Dummy Supplier"
            Me.tb_prjsledesc.Text = "0000000000"
            Me.cb_crsldc.Text = "Dummy Supplier"
            Me.tb_crcode.Text = "0000000000"
            Me.tb_ticketno.Focus()
            Me.tb_DATE.Text = Today.Date
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
            If Me.DataGridView1.CurrentCell.ColumnIndex = 1 Then
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
                        Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("BASEUOMCODE").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView1.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles b_deliv.Click
        clr_scr()
        unfreeze_scr()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIS_PR WBM WHERE INOUTTYPE = 'O' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If
            cmbloading1()
            Me.tb_sap_doc.Text = "ZTBV"
            Me.cb_sap_docu_type.Text = "Cash Sales"
            tmode = 1
            tb_inout_type.Text = "O"
            tb_inout_desc.Text = "Outgoing Goods"
            Me.cb_sleddesc.Text = "Dummy Customer"
            Me.tb_sledcode.Text = "0000000000"
            Me.cb_prjsledcode.Text = "Dummy Supplier"
            Me.tb_prjsledesc.Text = "0000000000"
            Me.cb_crsldc.Text = "Dummy Supplier"
            Me.tb_crcode.Text = "0000000000"
            Me.tb_ticketno.Focus()
            Me.tb_DATE.Text = Today.Date
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
            cb_sleddesc.DataSource = dssld.Tables("sled")
            cb_sleddesc.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sleddesc.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            conn.Close()
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
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
        'itemcode
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
            cb_sap_docu_type.DataSource = dsdoc.Tables("doc")
            cb_sap_docu_type.DisplayMember = dsdoc.Tables("doc").Columns("DOCDESC").ToString
            cb_sap_docu_type.ValueMember = dsdoc.Tables("doc").Columns("DOCCODE").ToString
            conn.Close()
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.sledmstprj"
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
            dscsld.Clear()
            dacsld = New OracleDataAdapter(cmd)
            dacsld.TableMappings.Add("Table", "sledcrj")
            dacsld.Fill(dscsld)
            cb_crsldc.DataSource = dscsld.Tables("sledcrj")
            cb_crsldc.DisplayMember = dscsld.Tables("sledcrj").Columns("SLEDDESC").ToString
            cb_crsldc.ValueMember = dscsld.Tables("sledcrj").Columns("SLEDCODE").ToString
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
            cb_sleddesc.DataSource = dssld.Tables("sled")
            cb_sleddesc.DisplayMember = dssld.Tables("sled").Columns("SLEDDESC").ToString
            cb_sleddesc.ValueMember = dssld.Tables("sled").Columns("SLEDCODE").ToString
            conn.Close()
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
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
            conn.Close()
            'cb_itemcode.Tag = dsitm.Tables("itm").Columns("INTITEMCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.sledmstprj"
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
            dscsld.Clear()
            dacsld = New OracleDataAdapter(cmd)
            dacsld.TableMappings.Add("Table", "sledcrj")
            dacsld.Fill(dscsld)
            cb_crsldc.DataSource = dscsld.Tables("sledcrj")
            cb_crsldc.DisplayMember = dscsld.Tables("sledcrj").Columns("SLEDDESC").ToString
            cb_crsldc.ValueMember = dscsld.Tables("sledcrj").Columns("SLEDCODE").ToString
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
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
            Dim expression As String = "ID LIKE '" & Me.TextBox1.Text & "%'" & ""
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

    Private Sub cb_sleddesc_LostFocus(sender As Object, e As EventArgs)
        If Me.cb_sleddesc.SelectedIndex <> -1 Then
            Me.tb_sledcode.Text = Me.cb_sleddesc.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_sledcode.Text & "'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            If foundrow.Count > 1 Then
                MsgBox("More number of records found for the supplier")
            End If
        End If
    End Sub

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


    Private Sub b_connect2_Click(sender As Object, e As EventArgs) Handles b_connect2.Click
        Try
            comm2.Parity = "None"
            comm2.StopBits = 1
            comm2.DataBits = 8
            comm2.BaudRate = 9600
            comm2.DisplayWindow = rtbDisplay2
            comm2.OpenPort()
            b_disconnect2.Visible = True
            b_connect2.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub

    Private Sub b_connect3_Click(sender As Object, e As EventArgs) Handles b_connect3.Click
        Try
            comm3.Parity = "None"
            comm3.StopBits = 1
            comm3.DataBits = 8
            comm3.BaudRate = 9600
            comm3.DisplayWindow = rtbDisplay3
            comm3.OpenPort()
            b_disconnect3.Visible = True
            b_connect3.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'comm.OpenPort()
        End Try
    End Sub

    Private Sub b_disconnect2_Click(sender As Object, e As EventArgs) Handles b_disconnect2.Click
        Try
            comm2.ClosePort()
            b_disconnect2.Visible = False
            b_connect2.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_disconnect3_Click(sender As Object, e As EventArgs) Handles b_disconnect3.Click
        Try
            comm3.ClosePort()
            b_disconnect3.Visible = False
            b_connect3.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub cb_sleddesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sleddesc.SelectedIndexChanged
        Me.tb_CUSTTYPE.Text = ""
        Me.tb_typecode.Text = ""
        Me.tb_typecatg_pt.Text = ""
        If Me.cb_sleddesc.SelectedIndex <> -1 Then
            Me.tb_sledcode.Text = Me.cb_sleddesc.SelectedValue.ToString
            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE = '" & Me.tb_sledcode.Text & "'" & ""
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
            End If
        End If
    End Sub

    Private Sub b_purchase_Click(sender As Object, e As EventArgs) Handles b_purchase.Click
        tb_ok_Click(sender, e)
        If tb_sap_doc.Text = "QD" Then
            ZMMPOGRPROCESSIS() 'Direct Purchase
        End If
    End Sub
    Public Sub ZMMPOGRPROCESSIS()


        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.cb_sleddesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
        ElseIf Me.tb_crcode.Text = "0000000000" Then
            MsgBox("Internal Order should be selected")
        Else
            Dim cmd As New OracleCommand
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim repos As RfcRepository = dest.Repository

            Dim pogrir As IRfcFunction
            If cb_crinv.Checked = True Then
                pogrir = dest.Repository.CreateFunction("Z_MM_PO_GR_PROCESS_PRJ")
            Else
                pogrir = dest.Repository.CreateFunction("Z_MM_PO_GR_PROCESS_PRJ")
            End If
            pogrir.SetValue("ZPRJN", tb_sledcode.Text)
            pogrir.SetValue("ZPRJS", tb_crcode.Text)
            pogrir.SetValue("ZREFPO", "")
            Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_POHEADER")
            pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
            pohdrin.SetValue("DOC_TYPE", "QPJ")
            pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text.PadLeft(10, "0"))
            pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
            pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
            pohdrin.SetValue("CURRENCY", "SAR")
            'pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATE.Text).Year & CDate(Me.tb_DATE.Text).Month.ToString("D2") & CDate(Me.tb_DATE.Text).Day.ToString("D2"))
            pohdrin.SetValue("DOC_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))
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
            'pocst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name
            'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
            'pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
            'pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
            'pocst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            'pocst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
            'pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
            pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
            'pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
            'pocst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
            'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

            Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
            ' Create field in transaction taable and bring from hremployee table
            grcst.SetValue("ZZINDS", glbvar.scaletype)
            'grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

            'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
            ' grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
            'grcst.SetValue("ZZDATEX", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            'grcst.SetValue("ZZTIEN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
            'grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
            grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
            'grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
            'grcst.SetValue("ZZVEHINO", Me.tb_vehicleno.Text)
            'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
            grcst.SetValue("ZZVENDOR", Me.tb_oth_ven_cust.Text)
            grcst.SetValue("ZZREMARKS", Me.tb_comments.Text)


            Dim condition As IRfcTable = pogrir.GetTable("T_POCONDHEADER")
            Dim conditionx As IRfcTable = pogrir.GetTable("T_POCONDHEADERX")

            'ZTR1 POSITIVE
            'Dim pztr1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
            'pztr1u.SetValue("COND_TYPE", "ZTR1")
            'pztr1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_transp.Text))
            'pztr1u.SetValue("CURRENCY", "SAR")
            'pztr1u.SetValue("CHANGE_ID", "I")

            'condition.Append(pztr1u)


            'Dim pztr1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
            'pztr1xu.SetValue("COND_TYPE", "X")
            'pztr1xu.SetValue("COND_VALUE", "X")
            'pztr1xu.SetValue("CURRENCY", "X")
            'pztr1xu.SetValue("CHANGE_ID", "X")

            'conditionx.Append(pztr1xu)

            ''ZTR2 NEGATIVE
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

            ''ZPT1 POSITIVE
            'Dim pzpt1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
            'pzpt1u.SetValue("COND_TYPE", "ZPT1")
            'pzpt1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_penalty.Text))
            'pzpt1u.SetValue("CURRENCY", "SAR")
            'pzpt1u.SetValue("CHANGE_ID", "I")

            'condition.Append(pzpt1u)


            'Dim pzpt1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
            'pzpt1xu.SetValue("COND_TYPE", "X")
            'pzpt1xu.SetValue("COND_VALUE", "X")
            'pzpt1xu.SetValue("CURRENCY", "X")
            'pzpt1xu.SetValue("CHANGE_ID", "X")

            'conditionx.Append(pzpt1xu)

            ''ZPT2 NEGATIVE
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

            ''ZMH1 POSITIVE
            'Dim pzmh1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
            'pzmh1u.SetValue("COND_TYPE", "ZMH1")
            'pzmh1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_eqpchrgs.Text))
            'pzmh1u.SetValue("CURRENCY", "SAR")
            'pzmh1u.SetValue("CHANGE_ID", "I")

            'condition.Append(pzmh1u)


            'Dim pzmh1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
            'pzmh1xu.SetValue("COND_TYPE", "X")
            'pzmh1xu.SetValue("COND_VALUE", "X")
            'pzmh1xu.SetValue("CURRENCY", "X")
            'pzmh1xu.SetValue("CHANGE_ID", "X")

            'conditionx.Append(pzmh1xu)

            ''ZMH2 NEGATIVE
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

            ''ZLB1 POSITIVE
            'Dim pzlb1u As IRfcStructure = condition.Metadata.LineType.CreateStructure
            'pzlb1u.SetValue("COND_TYPE", "ZLB1")
            'pzlb1u.SetValue("COND_VALUE", Convert.ToDecimal(Me.Tb_labourcharges.Text))
            'pzlb1u.SetValue("CURRENCY", "SAR")
            'pzlb1u.SetValue("CHANGE_ID", "I")

            'condition.Append(pzlb1u)


            'Dim pzlb1xu As IRfcStructure = conditionx.Metadata.LineType.CreateStructure
            'pzlb1xu.SetValue("COND_TYPE", "X")
            'pzlb1xu.SetValue("COND_VALUE", "X")
            'pzlb1xu.SetValue("CURRENCY", "X")
            'pzlb1xu.SetValue("CHANGE_ID", "X")

            'conditionx.Append(pzlb1xu)

            ''ZLB2 NEGATIVE
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


            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "curspkg_join_pr.get_is"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
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
                    Dim qt As Decimal
                    Dim cval As Decimal
                    Dim poitm As IRfcTable = pogrir.GetTable("T_POITEM")
                    Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                    'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                    poitmu.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                    poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                    poitmu.SetValue("PLANT", glbvar.divcd)
                    poitmu.SetValue("STGE_LOC", glbvar.LGORT)
                    poitmu.SetValue("MATL_GROUP", "01")
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        qt = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString())
                    Else
                        qt = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000
                    End If
                    poitmu.SetValue("QUANTITY", qt)
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        poitmu.SetValue("PO_UNIT", "ST")
                    Else
                        poitmu.SetValue("PO_UNIT", "TO")
                    End If
                    'poitmu.SetValue("PO_UNIT_ISO", "KGM")
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        cval = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
                    Else
                        cval = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                    End If
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
                    pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()) / 1000)
                    pozf.Append(pozfstru)
                Next
                'Dim poacc As IRfcTable = pogrir.GetTable("POACCOUNT")
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
                    tkt(j) = Me.tb_ticketno.Text
                Next

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
                    'If cb_crinv.Checked = True Then
                    MsgBox("Purchase Order # " & pogrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    'Else
                    '    MsgBox("Purchase Order # " & pogrir.GetValue("E_PONUMBER").ToString _
                    '          & vbCrLf & "Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString)
                    '    '                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString

                    '    Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    '    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    'End If
                    Me.b_purchase.Visible = False

                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_is"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        'If cb_crinv.Checked = True Then
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = pogrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        'Else
                        '    cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = pogrir.GetValue("E_PONUMBER").ToString
                        '    cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = pogrir.GetValue("E_MATERIALDOCNO").ToString
                        '    cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = DBNull.Value 'pogrir.GetValue("E_INVOICENO").ToString
                        '    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CInt(Me.tb_ticketno.Text)
                        '    cmd.ExecuteNonQuery()
                        '    conn.Close()
                        'End If
                        freeze_scr()
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
    Public Sub ZSDSOPROCESSNEWIS()

        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            Me.tb_save.Focus()
        ElseIf Me.tb_prjsledesc.Text = "0000000000" Then
            MsgBox("Project should be selected")
            Me.tb_prjsrchbyno.Focus()
        ElseIf Me.tb_crcode.Text = "0000000000" Then
            MsgBox("Internal Order should be selected")
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
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CASH_SALES_PRJ") 'ZSD_CASH_SALES
                sodnbil.SetValue("ZPRJN", tb_prjsledesc.Text)
                sodnbil.SetValue("ZPRJS", tb_crcode.Text)
                sodnbil.SetValue("ZREFPO", "")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZTBV")
                ohdrin.SetValue("SALES_ORG", Me.tb_CUSTTYPE.Text)
                ohdrin.SetValue("DISTR_CHAN", Me.tb_typecode.Text)
                ohdrin.SetValue("DIVISION", Me.tb_typecatg_pt.Text)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                'ohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATE.Text).Year & CDate(Me.tb_DATE.Text).Month.ToString("D2") & CDate(Me.tb_DATE.Text).Day.ToString("D2"))
                ohdrin.SetValue("DOC_DATE", CDate(Me.d_newdate.Text).Year & CDate(Me.d_newdate.Text).Month.ToString("D2") & CDate(Me.d_newdate.Text).Day.ToString("D2"))


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
                'dlcust.SetValue("ZZVEHI", Me.tb_vehicleno.Text)
                'dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'dlcust.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'dlcust.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                'dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
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


                'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.

                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join_pr.get_is"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
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
                    Dim qt As Decimal
                    Dim rqty As Decimal
                    Dim cval As Decimal
                    itmstru.SetValue("ITM_NUMBER", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                    itmstru.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                    itmstru.SetValue("PLANT", glbvar.divcd)
                    itmstru.SetValue("STORE_LOC", glbvar.LGORT)
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        qt = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString())
                    Else
                        qt = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000
                    End If
                    itmstru.SetValue("TARGET_QTY", qt)
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        itmstru.SetValue("SALES_UNIT", "ST")
                    Else
                        itmstru.SetValue("SALES_UNIT", "TO")
                    End If
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
                    itminxstru.SetValue("REF_DOC", "X")
                    itminxstru.SetValue("REF_DOC_IT", "X")
                    itminxstru.SetValue("REF_DOC_CA", "X")
                    oitminx.Append(itminxstru)
                    'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                    Dim orsistru As IRfcStructure = orsi.Metadata.LineType.CreateStructure
                    orsistru.SetValue("ITM_NUMBER", itm)
                    orsistru.SetValue("SCHED_LINE", sl)
                    'Dim dt As Date = FormatDateTime(Convert.ToDateTime(ORDER_SCHEDULES_IN.Item("REQ_DATE", 0).FormattedValue), DateFormat.ShortDate)
                    'orsistru.SetValue("REQ_DATE", dt)
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        rqty = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString())
                    Else
                        rqty = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000
                    End If
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
                    If dsmltitm.Tables("mltitm").Rows(a).Item("UOM").ToString() = "PC" Then
                        cval = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
                    Else
                        cval = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString()) * 1000
                    End If

                    ocinstru.SetValue("COND_VALUE", cval)
                    ocinstru.SetValue("CURRENCY", "SAR")
                    ocin.Append(ocinstru)
                    Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                    'tdlcfstru.SetValue("ZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                    'tdlcfstru.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                    'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                    'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                    'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                    'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                    'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                    tdlcf.Append(tdlcfstru)

                    Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                    orpstru.SetValue("PARTN_ROLE", "AG")
                    orpstru.SetValue("PARTN_NUMB", Me.tb_sledcode.Text.PadLeft(10, "0"))
                    'check if the customer is a one time customer then add the test else no need.
                    orpstru.SetValue("NAME", Me.cb_sleddesc.Text)
                    orpstru.SetValue("NAME_2", Me.tb_oth_ven_cust.Text)
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
                DataGridView2.Refresh()
                For l = 0 To rttbl.RowCount - 1
                    DataGridView2.Rows.Add()
                    DataGridView2.Rows(l).Cells("TYPE").Value = rttbl(l).Item("Type").GetString() 'err.GetValue("TYPE")
                    If rttbl(l).Item("Type").GetString() = "E" Then
                        soercnt = soercnt + 1
                    End If
                    DataGridView2.Rows(l).Cells("i_d").Value = rttbl(l).Item("ID").GetString() 'err.GetValue("ID")
                    DataGridView2.Rows(l).Cells("NUMBER").Value = rttbl(l).Item("NUMBER").GetString() 'err.GetValue("NUMBER")
                    DataGridView2.Rows(l).Cells("MESAGE").Value = rttbl(l).Item("MESSAGE").GetString() 'err.GetValue("MESSAGE")
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
                    Me.b_deliver.Visible = False
                    freeze_scr()
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_is"
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


    Private Sub b_deliver_Click(sender As Object, e As EventArgs) Handles b_deliver.Click
        tb_ok_Click(sender, e)
        If tb_sap_doc.Text = "ZTBV" Then
            ZSDSOPROCESSNEWIS()
        End If
    End Sub

    Private Sub b_clear_Click(sender As Object, e As EventArgs) Handles b_clear.Click
        clr_scr()
        unfreeze_scr()
    End Sub
    Private Sub clr_scr()
        Try
            Me.b_purchase.Enabled = False
            Me.b_purchase.Visible = False
            Me.b_deliver.Enabled = False
            Me.b_deliver.Visible = False
            'Me.Tb_asno.Text = "0"
            'Me.tb_orderno.Text = "0"
            'Me.tb_IBDSNO.Text = "0"
            'Me.tb_orderno.Text = "0"
            'Me.tb_dsno.Text = "0"
            'Me.Tb_transp.Text = 0
            'Me.Tb_labourcharges.Text = 0
            'Me.Tb_eqpchrgs.Text = 0
            'Me.Tb_penalty.Text = 0
            Me.cb_sleddesc.Text = ""
            Me.tb_sledcode.Text = ""
            Me.tb_ticketno.Text = 0
            'Me.Tb_vehicleno.Text = ""
            'Me.tb_buyer.Text = ""
            'Me.tb_DRIVERNAM.Text = ""
            'Me.cb_dcode.Text = ""
            'Me.tb_DATEIN.Text = ""
            'Me.tb_dateout.Text = ""
            'Me.tb_timein.Text = ""
            'Me.tb_timeout.Text = ""
            'Me.Tb_ccic.Text = ""
            Me.tb_comments.Text = ""
            Me.Tb_intdocno.Text = ""
            Me.cb_sap_docu_type.Text = ""
            Me.tb_sap_doc.Text = ""
            Me.tb_oth_ven_cust.Text = ""
            Me.tb_inout_type.Text = ""
            Me.tb_inout_desc.Text = ""
            Me.tb_sapord.Text = ""
            Me.tb_sapdocno.Text = ""
            Me.tb_sapinvno.Text = ""
            Me.tb_searchtkt.Text = ""
            tmode = 1
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView2.Rows.Clear()
            Me.tb_totqty.Text = "0"
            Me.tb_totval.Text = "0"
            Me.tb_CUSTTYPE.Text = ""
            Me.tb_typecode.Text = ""
            Me.tb_typecatg_pt.Text = ""
            Me.d_newdate.Text = Today.Date
            Me.cb_crinv.Checked = False
            Me.rtb_gprem.Text = ""
            Me.tb_prjsledesc.Text = ""
            Me.tb_prjaccountcode.Text = ""
            Me.cb_prjsledcode.Text = "Dummy Supplier"
            Me.tb_prjsledesc.Text = "0000000000"
            Me.cb_crsldc.Text = "Dummy Supplier"
            Me.tb_crcode.Text = "0000000000"
        Catch ex As Exception
        End Try
    End Sub
    Private Sub freeze_scr()
        Me.cb_sleddesc.Enabled = False
        Me.tb_sledcode.Enabled = False
        Me.tb_ticketno.Enabled = False
        Me.tb_DATE.Enabled = False
        Me.d_newdate.Enabled = False
        Me.b_purchase.Enabled = False
        Me.b_purchase.Visible = False
        Me.b_deliver.Enabled = False
        Me.b_deliver.Visible = False
        Me.tb_ok.Enabled = False
        'Me.Tb_vehicleno.Text = ""
        'Me.tb_buyer.Text = ""
        'Me.tb_DRIVERNAM.Text = ""
        'Me.cb_dcode.Text = ""
        'Me.tb_DATEIN.Text = ""
        'Me.tb_dateout.Text = ""
        'Me.tb_timein.Text = ""
        'Me.tb_timeout.Text = ""
        'Me.Tb_ccic.Text = ""
        Me.tb_comments.Enabled = False
        Me.Tb_intdocno.Enabled = False
        Me.cb_sap_docu_type.Enabled = False
        Me.tb_sap_doc.Enabled = False
        Me.tb_oth_ven_cust.Enabled = False
        Me.tb_inout_type.Enabled = False
        Me.tb_inout_desc.Enabled = False
        tmode = 1
        Me.DataGridView1.Enabled = False
        Me.cb_crinv.Checked = False
        Me.tb_prjsledesc.Enabled = False
        Me.cb_prjsledcode.Enabled = False
        Me.tb_prjsrchbyno.Enabled = False
        Me.cb_crsldc.Enabled = False
        Me.tb_crcode.Enabled = False
    End Sub
    Private Sub unfreeze_scr()
        Me.cb_sleddesc.Enabled = True
        Me.tb_sledcode.Enabled = True
        Me.tb_ticketno.Enabled = True
        'Me.tb_DATE.Enabled = True
        Me.d_newdate.Enabled = True
        Me.tb_ok.Enabled = True
        'Me.Tb_vehicleno.Text = ""
        'Me.tb_buyer.Text = ""
        'Me.tb_DRIVERNAM.Text = ""
        'Me.cb_dcode.Text = ""
        'Me.tb_DATEIN.Text = ""
        'Me.tb_dateout.Text = ""
        'Me.tb_timein.Text = ""
        'Me.tb_timeout.Text = ""
        'Me.Tb_ccic.Text = ""
        Me.tb_comments.Enabled = True
        Me.Tb_intdocno.Enabled = True
        Me.cb_sap_docu_type.Enabled = False
        Me.tb_sap_doc.Enabled = False
        Me.tb_oth_ven_cust.Enabled = True
        Me.tb_inout_type.Enabled = True
        Me.tb_inout_desc.Enabled = True
        tmode = 1
        Me.DataGridView1.Enabled = True
        Me.cb_crinv.Checked = False
        Me.tb_prjsledesc.Enabled = True
        Me.cb_prjsledcode.Enabled = True
        Me.tb_prjsrchbyno.Enabled = True
        Me.cb_crsldc.Enabled = True
        Me.tb_crcode.Enabled = True
    End Sub

    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Try
            If Me.DataGridView1.CurrentRow.Cells("Deduction").Selected Then
                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").EditedFormattedValue
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
            ElseIf Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
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
                        & " where username  = " & "'" & glbvar.userid & "'" & ")"

                Dim dpct = New OracleDataAdapter(sql, conn)
                Dim dpc As New DataSet
                dpc.Clear()
                dpct.Fill(dpc)
                conn.Close()
                Dim user_tol_value As Decimal
                Dim user_tot_allowed As Decimal
                Dim user_sales_value As Decimal
                Dim user_sales_allowed As Decimal
                Dim pct = dpc.Tables(0).Rows(0).Item("pct")
                Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                user_tol_value = pct * plist
                user_sales_value = 2 * plist
                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                user_sales_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + user_sales_value
                If pct <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + user_tol_value
                ElseIf amt <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + amt / 1000
                End If
                If Me.tb_inout_type.Text = "I" Then
                    If Me.tb_sap_doc.Text = "QD" Then
                        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_tot_allowed Then

                            MsgBox("Price not matching as the latest Pricelist")
                            Me.tb_ok.Enabled = False
                            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True
                            Me.DataGridView1.CurrentRow.Cells("rate").Value = 0
                        Else
                            Me.tb_ok.Enabled = True
                            '    tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                        End If
                        'Else
                        'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                    'ElseIf Me.tb_inout_type.Text = "O" Then
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                ElseIf Me.tb_inout_type.Text = "O" Then
                    If Me.tb_sap_doc.Text = "ZCWA" Then
                        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_sales_allowed Then

                            MsgBox("Price not matching as the latest Pricelist")
                            Me.tb_ok.Enabled = False
                            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True
                            Me.DataGridView1.CurrentRow.Cells("rate").Value = 0

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
    Private Sub d_newdate_ValueChanged() Handles d_newdate.Validated
        'If d_newdate.Text < CDate(tb_DATE.Text) Then
        '    MsgBox("Posting date cannot be less than dateout")
        '    d_newdate.Text = Today.Date
        'Else
        If d_newdate.Text > Today.Date Then
            MsgBox("Posting date cannot be greater than today")
            d_newdate.Text = Today.Date
        End If
    End Sub





    Private Sub tb_save_Click(sender As Object, e As EventArgs) Handles tb_save.Click

    End Sub

    Private Sub tb_comments_TextChanged(sender As Object, e As EventArgs) Handles tb_comments.TextChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        clr_scr()
        unfreeze_scr()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMIS_PR WBM WHERE INOUTTYPE = 'E' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If
            cmbloading1()
            Me.tb_sap_doc.Text = "EQ"
            Me.cb_sap_docu_type.Text = "Equipment Delivery"
            tmode = 1
            tb_inout_type.Text = "E"
            tb_inout_desc.Text = "Equipment Delivery"
            Me.cb_sleddesc.Text = "Dummy Customer"
            Me.tb_sledcode.Text = "0000000000"
            Me.tb_ticketno.Focus()
            Me.tb_DATE.Text = Today.Date
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub tb_searchtkt_TextChanged(sender As Object, e As EventArgs) Handles tb_searchtkt.LostFocus
        Me.tb_retrieve.Focus()

    End Sub

    Private Sub rtbDisplay_TextChanged(sender As Object, e As EventArgs) Handles rtbDisplay.LostFocus
        rtbDisplay2.Focus()
    End Sub

    Private Sub rtbDisplay2_TextChanged(sender As Object, e As EventArgs) Handles rtbDisplay2.LostFocus
        rtbDisplay3.Focus()
    End Sub

    Private Sub rtbDisplay3_TextChanged(sender As Object, e As EventArgs) Handles rtbDisplay3.LostFocus
        cb_sleddesc.Focus()
    End Sub
    Private Sub cb_dcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_dcode.SelectedIndexChanged
        Try
            If Me.cb_dcode.SelectedIndex <> -1 Then
                Me.tb_DRIVERNAM.Text = Me.cb_dcode.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "EMPCODE = '" & Me.tb_DRIVERNAM.Text & "'" & ""
                foundrow = dsdr.Tables("drv").Select(expression)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub

End Class