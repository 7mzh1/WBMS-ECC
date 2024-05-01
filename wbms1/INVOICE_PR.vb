Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Public Class INVOICE_PR
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
    Dim dainv As New OracleDataAdapter
    Dim dsinv As New DataSet
    Dim dsdr As New DataSet
    Dim id() As String
    Dim typ() As String
    Dim nmbr() As Integer
    Dim mesg() As String
    Dim tkt() As Integer
    Private Sub Invoice_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

        Catch ex As Exception
            MsgBox(ex.Message)


        End Try


    End Sub
    Private Sub WBMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
    End Sub

    Private Sub INVGRID_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles INVGRID.CellContentClick
        If e.ColumnIndex = 0 Then
            Me.INVGRID.Rows.Remove(Me.INVGRID.CurrentRow)
            Dim rowchk = 0
            For i = 0 To INVGRID.Rows.Count - 1
                rowchk = rowchk + 1
                INVGRID.Rows(i).Cells("INVSLNO").Value = rowchk
            Next
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles b_display.Click
        Me.INVGRID.Rows.Clear()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd As New OracleCommand
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join_pr.getinv_det"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("psledcode", OracleDbType.Varchar2)).Value = tb_sledesc.Text
        cmd.Parameters.Add(New OracleParameter("pmonth", OracleDbType.Varchar2)).Value = CDate(Me.d_date.Text).Month.ToString("D2")
        cmd.Parameters.Add(New OracleParameter("pdate", OracleDbType.Varchar2)).Value = CDate(Me.d_date.Text).Year & CDate(Me.d_date.Text).Month.ToString("D2") & CDate(Me.d_date.Text).Day.ToString("D2")
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Try
            dsinv.Clear()
            dainv = New OracleDataAdapter(cmd)
            dainv.TableMappings.Add("Table", "invoice")
            dainv.Fill(dsinv)
            Dim c = dsinv.Tables(0).Rows.Count
            For i = 0 To c - 1
                INVGRID.Rows.Insert(rowIndex:=0)
                Me.INVGRID.Rows(0).Cells("INVSLNO").Value = dsinv.Tables(0).Rows(i).Item("INVSLNO")
                Me.INVGRID.Rows(0).Cells("SCALE").Value = dsinv.Tables(0).Rows(i).Item("SCALE")
                Me.INVGRID.Rows(0).Cells("INTDOCNO").Value = dsinv.Tables(0).Rows(i).Item("INTDOCNO")
                Me.INVGRID.Rows(0).Cells("TICKETNO").Value = dsinv.Tables(0).Rows(i).Item("TICKETNO")
                Me.INVGRID.Rows(0).Cells("SLEDCODE").Value = dsinv.Tables(0).Rows(i).Item("SLEDCODE")
                Me.INVGRID.Rows(0).Cells("SLEDDESC").Value = dsinv.Tables(0).Rows(i).Item("SLEDDESC")
                Me.INVGRID.Rows(0).Cells("SLNO").Value = dsinv.Tables(0).Rows(i).Item("SLNO")
                Me.INVGRID.Rows(0).Cells("ITEMCODE").Value = dsinv.Tables(0).Rows(i).Item("ITEMCODE")
                Me.INVGRID.Rows(0).Cells("ITEMDESC").Value = dsinv.Tables(0).Rows(i).Item("ITEMDESC")
                Me.INVGRID.Rows(0).Cells("DATEOUT").Value = dsinv.Tables(0).Rows(i).Item("DATEOUT")
                Me.INVGRID.Rows(0).Cells("FIRSTQTY").Value = dsinv.Tables(0).Rows(i).Item("FIRSTQTY")
                Me.INVGRID.Rows(0).Cells("SECONDQTY").Value = dsinv.Tables(0).Rows(i).Item("SECONDQTY")
                Me.INVGRID.Rows(0).Cells("QTY").Value = dsinv.Tables(0).Rows(i).Item("QTY")
                Me.INVGRID.Rows(0).Cells("PRICETON").Value = dsinv.Tables(0).Rows(i).Item("PRICETON")
                Me.INVGRID.Rows(0).Cells("RATE").Value = dsinv.Tables(0).Rows(i).Item("RATE")
                Me.INVGRID.Rows(0).Cells("TOTAL_PRICE").Value = dsinv.Tables(0).Rows(i).Item("TOTAL_PRICE")
                Me.INVGRID.Rows(0).Cells("VBELNS").Value = dsinv.Tables(0).Rows(i).Item("VBELNS")
                Me.INVGRID.Rows(0).Cells("VBELND").Value = dsinv.Tables(0).Rows(i).Item("VBELND")
                If Not IsDBNull(dsinv.Tables(0).Rows(i).Item("VBELNI")) Then
                    Me.INVGRID.Rows(0).Cells("VBELNI").Value = dsinv.Tables(0).Rows(i).Item("VBELNI")
                End If
                'Me.INVGRID.Rows(0).Cells("trcharge").Value = dsinv.Tables(0).Rows(i).Item("trcharge")
                'Me.INVGRID.Rows(0).Cells("penalty").Value = dsinv.Tables(0).Rows(i).Item("penalty")
                'Me.INVGRID.Rows(0).Cells("machcharge").Value = dsinv.Tables(0).Rows(i).Item("machcharge")
                'Me.INVGRID.Rows(0).Cells("labcharge").Value = dsinv.Tables(0).Rows(i).Item("labcharge")
            Next
            Me.b_save.Enabled = True
            'cb_sledcode.Tag = dssld.Tables("sled").Columns("ACCOUNTCODE").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles b_save.Click
        'Dim cn As Integer
        'cn = Me.INVGRID.RowCount
        'For i = 0 To Me.INVGRID.RowCount - 1
        '    If Me.INVGRID.Rows(i).Cells("selectitem").Value Is Nothing Then
        '        Dim ff = Me.INVGRID.Rows(i).Cells("invslno").Value
        '        Dim dd = Me.INVGRID.Rows(i).Cells("selectitem").Value
        '        Me.INVGRID.Rows.Remove(Me.INVGRID.CurrentRow)
        '        Dim aaa = Me.INVGRID.RowCount
        '        i = i - 1
        '        'cn = Me.INVGRID.RowCount
        '    End If
        'Next
        Try
            Dim cn As Integer = Me.INVGRID.RowCount
            ReDim giinvdocno(cn - 1)
            ReDim giINVSLNO(cn - 1)
            ReDim giSCALE(cn - 1)
            ReDim giINTDOCNO(cn - 1)
            ReDim giTICKETNO(cn - 1)
            ReDim giSLEDCODE(cn - 1)
            ReDim giSLEDDESC(cn - 1)
            ReDim giSLNO(cn - 1)
            ReDim giITEMCODE(cn - 1)
            ReDim giITEMDESC(cn - 1)
            ReDim giDATEOUT(cn - 1)
            ReDim giFIRSTQTY(cn - 1)
            ReDim giSECONDQTY(cn - 1)
            ReDim giQTY(cn - 1)
            ReDim giPRICETON(cn - 1)
            ReDim giRATE(cn - 1)
            ReDim giTOTAL_PRICE(cn - 1)
            ReDim giVBELNS(cn - 1)
            ReDim giVBELND(cn - 1)
            ReDim giVBELNI(cn - 1)
            ReDim giPOSTDATE(cn - 1)
            ReDim gitrcharge(cn - 1)
            ReDim gipenalty(cn - 1)
            ReDim gimacharge(cn - 1)
            ReDim gilabcharge(cn - 1)
            For i = 0 To cn - 1
                giinvdocno(i) = Me.tb_docno.Text
                giINVSLNO(i) = Me.INVGRID.Rows(i).Cells("INVSLNO").Value
                giSCALE(i) = Me.INVGRID.Rows(i).Cells("scale").Value
                giINTDOCNO(i) = Me.INVGRID.Rows(i).Cells("intdocno").Value
                giTICKETNO(i) = Me.INVGRID.Rows(i).Cells("ticketNO").Value
                giSLEDCODE(i) = Me.INVGRID.Rows(i).Cells("sledcode").Value
                giSLEDDESC(i) = Me.INVGRID.Rows(i).Cells("sleddesc").Value
                giSLNO(i) = Me.INVGRID.Rows(i).Cells("SLNO").Value
                giITEMCODE(i) = Me.INVGRID.Rows(i).Cells("itemcode").Value
                giITEMDESC(i) = Me.INVGRID.Rows(i).Cells("itemdesc").Value
                giDATEOUT(i) = Me.INVGRID.Rows(i).Cells("dateout").Value
                giFIRSTQTY(i) = Me.INVGRID.Rows(i).Cells("firstqty").Value
                giSECONDQTY(i) = Me.INVGRID.Rows(i).Cells("secondqty").Value
                giQTY(i) = Me.INVGRID.Rows(i).Cells("qty").Value
                giPRICETON(i) = Me.INVGRID.Rows(i).Cells("priceton").Value
                giRATE(i) = Me.INVGRID.Rows(i).Cells("rate").Value
                giTOTAL_PRICE(i) = Me.INVGRID.Rows(i).Cells("total_price").Value
                giVBELNS(i) = Me.INVGRID.Rows(i).Cells("vbelns").Value
                giVBELND(i) = Me.INVGRID.Rows(i).Cells("vbelnd").Value
                giVBELNI(i) = Me.INVGRID.Rows(i).Cells("vbelni").Value
                giPOSTDATE(i) = CDate(Me.d_date.Text)
                gitrcharge(i) = Me.Tb_transp.Text
                gipenalty(i) = Me.Tb_penalty.Text
                gimacharge(i) = Me.Tb_labourcharges.Text
                gilabcharge(i) = Me.Tb_eqpchrgs.Text
            Next
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_invoice"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.ArrayBindCount = glbvar.intiem.Count
            Dim ipINvdocno As OracleParameter = New OracleParameter(":p1", OracleDbType.Decimal)
            ipINvdocno.Direction = ParameterDirection.Input
            ipINvdocno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipINvdocno.Value = glbvar.giinvdocno

            Dim ipinvslno As OracleParameter = New OracleParameter("p2:", OracleDbType.Decimal)
            ipinvslno.Direction = ParameterDirection.Input
            ipinvslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipinvslno.Value = glbvar.giINVSLNO

            Dim ipscale As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
            ipscale.Direction = ParameterDirection.Input
            ipscale.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipscale.Value = glbvar.giSCALE

            Dim ipintdocno As OracleParameter = New OracleParameter("p4:", OracleDbType.Decimal)
            ipintdocno.Direction = ParameterDirection.Input
            ipintdocno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipintdocno.Value = glbvar.giINTDOCNO

            Dim ipticketno As OracleParameter = New OracleParameter("p5:", OracleDbType.Decimal)
            ipticketno.Direction = ParameterDirection.Input
            ipticketno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipticketno.Value = glbvar.giTICKETNO

            Dim ipsledcode As OracleParameter = New OracleParameter("p6:", OracleDbType.Varchar2)
            ipsledcode.Direction = ParameterDirection.Input
            ipsledcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipsledcode.Value = glbvar.giSLEDCODE

            Dim ipsleddesc As OracleParameter = New OracleParameter("p7:", OracleDbType.Varchar2)
            ipsleddesc.Direction = ParameterDirection.Input
            ipsleddesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipsleddesc.Value = glbvar.giSLEDDESC

            Dim ipslno As OracleParameter = New OracleParameter("p8:", OracleDbType.Decimal)
            ipslno.Direction = ParameterDirection.Input
            ipslno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipslno.Value = glbvar.giSLNO

            Dim ipitemcode As OracleParameter = New OracleParameter("p9:", OracleDbType.Varchar2)
            ipitemcode.Direction = ParameterDirection.Input
            ipitemcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipitemcode.Value = glbvar.giITEMCODE

            Dim ipitemdesc As OracleParameter = New OracleParameter("p10:", OracleDbType.Varchar2)
            ipitemdesc.Direction = ParameterDirection.Input
            ipitemdesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipitemdesc.Value = glbvar.giITEMDESC

            Dim ipdateout As OracleParameter = New OracleParameter("p11:", OracleDbType.Date)
            ipdateout.Direction = ParameterDirection.Input
            ipdateout.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipdateout.Value = glbvar.giDATEOUT

            Dim ipfirstqty As OracleParameter = New OracleParameter("p12:", OracleDbType.Decimal)
            ipfirstqty.Direction = ParameterDirection.Input
            ipfirstqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipfirstqty.Value = glbvar.giFIRSTQTY

            Dim ipsecondqty As OracleParameter = New OracleParameter("p13:", OracleDbType.Decimal)
            ipsecondqty.Direction = ParameterDirection.Input
            ipsecondqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipsecondqty.Value = glbvar.giSECONDQTY

            Dim ipqty As OracleParameter = New OracleParameter("p14:", OracleDbType.Decimal)
            ipqty.Direction = ParameterDirection.Input
            ipqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipqty.Value = glbvar.giQTY

            Dim ippriceton As OracleParameter = New OracleParameter("p15:", OracleDbType.Decimal)
            ippriceton.Direction = ParameterDirection.Input
            ippriceton.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ippriceton.Value = glbvar.giPRICETON

            Dim iprate As OracleParameter = New OracleParameter("p16:", OracleDbType.Decimal)
            iprate.Direction = ParameterDirection.Input
            iprate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            iprate.Value = glbvar.giRATE

            Dim iptotal_price As OracleParameter = New OracleParameter("p17:", OracleDbType.Decimal)
            iptotal_price.Direction = ParameterDirection.Input
            iptotal_price.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            iptotal_price.Value = glbvar.giTOTAL_PRICE

            Dim ipvbelns As OracleParameter = New OracleParameter("p18:", OracleDbType.Varchar2)
            ipvbelns.Direction = ParameterDirection.Input
            ipvbelns.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipvbelns.Value = glbvar.giVBELNS

            Dim ipvbelnd As OracleParameter = New OracleParameter("p19:", OracleDbType.Varchar2)
            ipvbelnd.Direction = ParameterDirection.Input
            ipvbelnd.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipvbelnd.Value = glbvar.giVBELND

            Dim ipvbelni As OracleParameter = New OracleParameter("p20:", OracleDbType.Varchar2)
            ipvbelni.Direction = ParameterDirection.Input
            ipvbelni.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipvbelni.Value = glbvar.giVBELNI

            Dim ippost_date As OracleParameter = New OracleParameter("p21:", OracleDbType.Date)
            ippost_date.Direction = ParameterDirection.Input
            ippost_date.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ippost_date.Value = glbvar.giPOSTDATE

            Dim iptrcharge As OracleParameter = New OracleParameter("p17:", OracleDbType.Decimal)
            iptrcharge.Direction = ParameterDirection.Input
            iptrcharge.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            iptrcharge.Value = glbvar.gitrcharge

            Dim ippenalty As OracleParameter = New OracleParameter("p17:", OracleDbType.Decimal)
            ippenalty.Direction = ParameterDirection.Input
            ippenalty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ippenalty.Value = glbvar.gipenalty

            Dim ipmacharge As OracleParameter = New OracleParameter("p17:", OracleDbType.Decimal)
            ipmacharge.Direction = ParameterDirection.Input
            ipmacharge.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ipmacharge.Value = glbvar.gimacharge

            Dim iplabcharge As OracleParameter = New OracleParameter("p17:", OracleDbType.Decimal)
            iplabcharge.Direction = ParameterDirection.Input
            iplabcharge.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            iplabcharge.Value = glbvar.gilabcharge

            cmd.Parameters.Add(ipINvdocno)
            cmd.Parameters.Add(ipinvslno)
            cmd.Parameters.Add(ipscale)
            cmd.Parameters.Add(ipintdocno)
            cmd.Parameters.Add(ipticketno)
            cmd.Parameters.Add(ipsledcode)
            cmd.Parameters.Add(ipsleddesc)
            cmd.Parameters.Add(ipslno)
            cmd.Parameters.Add(ipitemcode)
            cmd.Parameters.Add(ipitemdesc)
            cmd.Parameters.Add(ipdateout)
            cmd.Parameters.Add(ipfirstqty)
            cmd.Parameters.Add(ipsecondqty)
            cmd.Parameters.Add(ipqty)
            cmd.Parameters.Add(ippriceton)
            cmd.Parameters.Add(iprate)
            cmd.Parameters.Add(iptotal_price)
            cmd.Parameters.Add(ipvbelns)
            cmd.Parameters.Add(ipvbelnd)
            cmd.Parameters.Add(ipvbelni)
            cmd.Parameters.Add(ippost_date)
            cmd.Parameters.Add(iptrcharge)
            cmd.Parameters.Add(ippenalty)
            cmd.Parameters.Add(ipmacharge)
            cmd.Parameters.Add(iplabcharge)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_docno.Text
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")
            Me.cb_sledcode.Enabled = False
            Me.tb_sledesc.Enabled = False
            Me.tb_searchbyno.Enabled = False
            Me.b_display.Enabled = False
            Me.b_crinv.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles b_searchdoc.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            INVGRID.Rows.Clear()
            Me.tb_vbelni.Text = ""
            Me.Tb_transp.Text = 0
            Me.Tb_penalty.Text = 0
            Me.Tb_eqpchrgs.Text = 0
            Me.Tb_labourcharges.Text = 0
            Dim cns As Integer
            sql = " select count(itemcode) cnt from STMUL_GR_IV_PR WHERE invdocno = " & Me.tb_rdocno.Text
            Dim dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = " select * from STMUL_GR_IV_PR WHERE invdocno = " & Me.tb_rdocno.Text _
                  & "  order by invslno desc "
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")

            For i = 0 To cns - 1
                INVGRID.Rows.Insert(rowIndex:=0)
                Me.INVGRID.Rows(0).Cells("INVSLNO").Value = dp.Tables(0).Rows(i).Item("INVSLNO")
                Me.INVGRID.Rows(0).Cells("SCALE").Value = dp.Tables(0).Rows(i).Item("SCALE")
                Me.INVGRID.Rows(0).Cells("INTDOCNO").Value = dp.Tables(0).Rows(i).Item("INTDOCNO")
                Me.INVGRID.Rows(0).Cells("TICKETNO").Value = dp.Tables(0).Rows(i).Item("TICKETNO")
                Me.INVGRID.Rows(0).Cells("SLEDCODE").Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                Me.INVGRID.Rows(0).Cells("SLEDDESC").Value = dp.Tables(0).Rows(i).Item("SLEDDESC")
                Me.INVGRID.Rows(0).Cells("SLNO").Value = dp.Tables(0).Rows(i).Item("SLNO")
                Me.INVGRID.Rows(0).Cells("ITEMCODE").Value = dp.Tables(0).Rows(i).Item("ITEMCODE")
                Me.INVGRID.Rows(0).Cells("ITEMDESC").Value = dp.Tables(0).Rows(i).Item("ITEMDESC")
                Me.INVGRID.Rows(0).Cells("DATEOUT").Value = dp.Tables(0).Rows(i).Item("DATEOUT")
                Me.INVGRID.Rows(0).Cells("POST_DATE").Value = dp.Tables(0).Rows(i).Item("POST_DATE")
                Me.INVGRID.Rows(0).Cells("FIRSTQTY").Value = dp.Tables(0).Rows(i).Item("FIRSTQTY")
                Me.INVGRID.Rows(0).Cells("SECONDQTY").Value = dp.Tables(0).Rows(i).Item("SECONDQTY")
                Me.INVGRID.Rows(0).Cells("QTY").Value = dp.Tables(0).Rows(i).Item("QTY")
                Me.INVGRID.Rows(0).Cells("PRICETON").Value = dp.Tables(0).Rows(i).Item("PRICETON")
                Me.INVGRID.Rows(0).Cells("RATE").Value = dp.Tables(0).Rows(i).Item("RATE")
                Me.INVGRID.Rows(0).Cells("TOTAL_PRICE").Value = dp.Tables(0).Rows(i).Item("TOTAL_PRICE")
                Me.INVGRID.Rows(0).Cells("VBELNS").Value = dp.Tables(0).Rows(i).Item("VBELNS")
                Me.INVGRID.Rows(0).Cells("VBELND").Value = dp.Tables(0).Rows(i).Item("VBELND")
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("VBELNI")) Then
                    Me.INVGRID.Rows(0).Cells("VBELNI").Value = dp.Tables(0).Rows(i).Item("VBELNI")
                    Me.tb_vbelni.Text = dp.Tables(0).Rows(i).Item("VBELNI")
                End If
                Me.INVGRID.Rows(0).Cells("trcharge").Value = dp.Tables(0).Rows(i).Item("trcharge")
                Me.INVGRID.Rows(0).Cells("penalty").Value = dp.Tables(0).Rows(i).Item("penalty")
                Me.INVGRID.Rows(0).Cells("machcharge").Value = dp.Tables(0).Rows(i).Item("machcharge")
                Me.INVGRID.Rows(0).Cells("labcharge").Value = dp.Tables(0).Rows(i).Item("labcharge")

                'Me.DataGridView1.Rows(0).Cells("BUYER").Value = dp.Tables(0).Rows(0).Item("BUYER")
            Next
            Me.d_date.Text = dp.Tables(0).Rows(0).Item("POST_DATE")
            Me.tb_sledesc.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
            Me.cb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
            Me.tb_docno.Text = dp.Tables(0).Rows(0).Item("INVDOCNO")
            Me.Tb_transp.Text = dp.Tables(0).Rows(0).Item("trcharge")
            Me.Tb_penalty.Text = dp.Tables(0).Rows(0).Item("penalty")
            Me.Tb_eqpchrgs.Text = dp.Tables(0).Rows(0).Item("machcharge")
            Me.Tb_labourcharges.Text = dp.Tables(0).Rows(0).Item("labcharge")
            If Me.tb_vbelni.Text <> "" Then
                b_save.Enabled = False
                b_crinv.Enabled = False
                b_display.Enabled = False
            Else
                b_save.Enabled = True
            End If
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_gen_Click(sender As Object, e As EventArgs) Handles b_gen.Click
        Me.tb_vbelni.Text = ""
        Me.Tb_transp.Text = 0
        Me.Tb_penalty.Text = 0
        Me.Tb_eqpchrgs.Text = 0
        Me.Tb_labourcharges.Text = 0
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.INVDOCNO),0)+1 TKT" _
                & "  FROM   STMUL_GR_IV_PR WBM "
        da = New OracleDataAdapter(sql, conn)
        Dim dstk As New DataSet
        Try
            da.TableMappings.Add("Table", "TKTNO")
            da.Fill(dstk)
            conn.Close()
            Me.tb_docno.Text = dstk.Tables("TKTNO").Rows(0).Item("TKT")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
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
        Me.cb_sledcode.Enabled = True
        Me.tb_sledesc.Enabled = True
        Me.tb_searchbyno.Enabled = True
        Me.b_display.Enabled = True
        Me.b_crinv.Enabled = False
        Me.b_save.Enabled = False
        Me.tb_searchbyno.Text = ""
        Me.tb_vbelni.Text = ""
    End Sub


    Private Sub cb_sledcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sledcode.SelectedIndexChanged
        If Me.cb_sledcode.SelectedIndex <> -1 Then
            Me.tb_sledesc.Text = Me.cb_sledcode.SelectedValue.ToString

        End If
    End Sub

    Private Sub loadven_SelectedIndexChanged(sender As Object, e As EventArgs) Handles loadven.SelectedIndexChanged

    End Sub

    Private Sub tb_searchbyno_TextChanged(sender As Object, e As EventArgs) Handles tb_searchbyno.TextChanged
        Try


            Dim foundrow() As DataRow
            Dim expression As String = "SLEDCODE LIKE '" & Me.tb_searchbyno.Text & "%'" & ""
            foundrow = dssld.Tables("sled").Select(expression)
            loadven.Items.Clear()
            For i = 0 To foundrow.Count - 1

                Me.loadven.Items.Add(foundrow(i).Item("SLEDCODE").ToString)
                Me.loadven.Items(i).SubItems.Add(foundrow(i).Item("SLEDDESC").ToString)
            Next
            loadven.Visible = True


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

    Private Sub b_crinv_Click(sender As Object, e As EventArgs) Handles b_crinv.Click

        'Dim cmdc As New OracleCommand
        'Dim count As Integer = 0
        'Dim daamultitm As New OracleDataAdapter(cmdc)
        'Dim dsamltitm As New DataSet
        'conn = New OracleConnection(constr)
        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'cmdc.Connection = conn
        'cmdc.Parameters.Clear()
        'cmdc.CommandText = "curspkg_join.chk_multi"
        'cmdc.CommandType = CommandType.StoredProcedure
        'cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        'cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        'Dim daamulti As New OracleDataAdapter(cmdc)
        'daamulti.TableMappings.Add("Table", "mlt")
        'Dim dsamlti As New DataSet
        'daamulti.Fill(dsamlti)
        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'Try
        '    cmdc.Connection = conn
        '    cmdc.Parameters.Clear()
        '    cmdc.CommandText = "curspkg_join.get_multi"
        '    cmdc.CommandType = CommandType.StoredProcedure
        '    cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
        '    cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        '    daamultitm.TableMappings.Add("Table", "mltitm")
        '    daamultitm.Fill(dsamltitm)
        '    conn.Close()
        '    For a = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
        '        If dsamltitm.Tables("mltitm").Rows(a).Item("RATE").ToString() = "0" Then
        '            count = count + 1
        '        End If
        '    Next
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        ''Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString


        '' This call is required by the designer.

        'If Me.Tb_intdocno.Text = "" Then
        '    MsgBox("Please save the record first")
        'ElseIf Me.tb_sledesc.Text = "" Then
        '    MsgBox("Select a vendor")
        '    Me.tb_sledesc.Focus()
        'ElseIf Me.cb_itemcode.Text = "" Then
        '    MsgBox("Select an itemcode")
        '    Me.cb_itemcode.Focus()
        'ElseIf Me.tb_FIRSTQTY.Text = "" Then
        '    MsgBox(" First Qty cannot be blank")
        '    Me.b_newveh.Focus()
        'ElseIf Me.tb_SECONDQTY.Text = "" Then
        '    MsgBox(" Second Qty cannot be blank")
        '    Me.b_edit.Focus()
        'ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) = 0 And Me.tb_PRICETON.Text = "0" Then
        '    MsgBox("Please enter a price")
        'ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
        '    MsgBox("Please enter a price")
        'Else
        Dim cmd As New OracleCommand
        Try
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim repos As RfcRepository = dest.Repository
            Dim pogrir As IRfcFunction

            pogrir = dest.Repository.CreateFunction("Z_MM_IR_MUL_GR")

            Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_INVHEADER")
            pohdrin.SetValue("INVOICE_IND", "X")
            pohdrin.SetValue("DOC_TYPE", "RE")
            pohdrin.SetValue("COMP_CODE", "1000")
            pohdrin.SetValue("CURRENCY", "SAR")
            pohdrin.SetValue("REF_DOC_NO", glbvar.divcd)
            'pohdrin.SetValue("DOC_TYPE", "RE")
            pohdrin.SetValue("DOC_DATE", CDate(Me.d_date.Text).Year & CDate(Me.d_date.Text).Month.ToString("D2") & CDate(Me.d_date.Text).Day.ToString("D2"))
            pohdrin.SetValue("PSTNG_DATE", CDate(Me.d_date.Text).Year & CDate(Me.d_date.Text).Month.ToString("D2") & CDate(Me.d_date.Text).Day.ToString("D2"))
            pohdrin.SetValue("BLINE_DATE", CDate(Me.d_date.Text).Year & CDate(Me.d_date.Text).Month.ToString("D2") & CDate(Me.d_date.Text).Day.ToString("D2"))
            pohdrin.SetValue("PERSON_EXT", glbvar.userid)

            'new addition
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

            'new addition end

            conn = New OracleConnection(constr)


            Try


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "curspkg_join_pr.get_invoice"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("vdocno", OracleDbType.Decimal)).Value = CDec(Me.tb_docno.Text)
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                Dim damultitm As New OracleDataAdapter(cmd)
                damultitm.TableMappings.Add("Table", "mltitm")
                Dim dsmltitm As New DataSet
                damultitm.Fill(dsmltitm)
                conn.Close()
                For a = 0 To dsmltitm.Tables("mltitm").Rows.Count - 1
                    Dim poitm As IRfcTable = pogrir.GetTable("T_INVITEM")
                    Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                    'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                    poitmu.SetValue("INVOICE_DOC_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("INVSLNO").ToString)
                    poitmu.SetValue("PO_NUMBER", dsmltitm.Tables("mltitm").Rows(a).Item("VBELNS").ToString())
                    poitmu.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                    poitmu.SetValue("TAX_CODE", "V0")
                    poitm.Append(poitmu)

                    Dim pozf As IRfcTable = pogrir.GetTable("T_MTDOC")
                    Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                    pozfstru.SetValue("MTDOC", dsmltitm.Tables("mltitm").Rows(a).Item("VBELND").ToString())
                    pozfstru.SetValue("PONO", dsmltitm.Tables("mltitm").Rows(a).Item("VBELNS").ToString())
                    pozfstru.SetValue("ITEMNO", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
                    pozfstru.SetValue("POST_YEAR", CDate(Me.d_date.Text).Year)
                    pozf.Append(pozfstru)


                Next

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
                tkt(j) = Me.tb_docno.Text
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

                MsgBox("Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)


                Me.tb_vbelni.Text = pogrir.GetValue("E_INVOICENO").ToString
            End If
            Me.b_save.Enabled = False
            Me.b_crinv.Enabled = False
            Me.b_display.Enabled = False
            'freeze_scr()
            'Write an update procedure for updating the documnt numbers in STWBMIBDS
            'gen_wbms_sap_U
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_inv"
            cmd.CommandType = CommandType.StoredProcedure
            Try


                cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pogrir.GetValue("E_INVOICENO").ToString
                cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_docno.Text)
                cmd.ExecuteNonQuery()
                conn.Close()



            Catch ex As Exception
                MsgBox(ex.Message & " From Updating")
            End Try

        Catch ex As Exception
            MsgBox(ex.Message & " From QD")
        End Try




        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Sub b_exit_Click(sender As Object, e As EventArgs) Handles b_exit.Click
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()
    End Sub

    Private Sub d_date_ValueChanged() Handles d_date.Validated
        If d_date.Text > Today.Date Then
            MsgBox("Date cannot be greater than today")
            d_date.Text = Today.Date
        End If
    End Sub


End Class