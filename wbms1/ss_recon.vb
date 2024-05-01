Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector

Public Class ss_recon
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
    Dim tkt() As Integer
    Dim rowchk As Integer

    Private Sub b_display_Click(sender As Object, e As EventArgs) Handles b_display.Click
        Try
            Dim tdate = CDate(d_postdate.Text).Day.ToString("D2")
            Dim tmonth = CDate(d_postdate.Text).Month.ToString("D2")
            Dim tyear = CDate(d_postdate.Text).Year
            Dim docdate = tyear & tmonth & tdate
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            tmode = 2
            DGV_SML.Rows.Clear()
            Dim cns As Integer
            sql = " select count(itemdesc) cnt from VW_SS_RECON WHERE " _
            & " to_number(to_char(post_date,'YYYYMMDD')) = to_number(" & "'" & docdate & "')"
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = " select * from VW_SS_RECON WHERE " _
            & " to_number(to_char(post_date,'YYYYMMDD')) = to_number(" & "'" & docdate & "')"

            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")

            For i = 0 To cns - 1
                DGV_SML.Rows.Insert(rowIndex:=0)
                Me.DGV_SML.Rows(0).Cells("itemcode").Value = dp.Tables(0).Rows(i).Item("itemcode")
                Me.DGV_SML.Rows(0).Cells("itemdesc").Value = dp.Tables(0).Rows(i).Item("itemdesc")
                Me.DGV_SML.Rows(0).Cells("post_date").Value = dp.Tables(0).Rows(i).Item("post_date")
                Me.DGV_SML.Rows(0).Cells("purqty").Value = dp.Tables(0).Rows(i).Item("purqty")
                Me.DGV_SML.Rows(0).Cells("pur_ded").Value = dp.Tables(0).Rows(i).Item("purded")
                Me.DGV_SML.Rows(0).Cells("salqty").Value = dp.Tables(0).Rows(i).Item("salqty")
                Me.DGV_SML.Rows(0).Cells("salded").Value = dp.Tables(0).Rows(i).Item("salded")
                Me.DGV_SML.Rows(0).Cells("Recon_qty").Value = dp.Tables(0).Rows(i).Item("bigsclwt")
                Me.DGV_SML.Rows(0).Cells("Recon_ded").Value = dp.Tables(0).Rows(i).Item("bigsclded")
                Me.DGV_SML.Rows(0).Cells("Diff").Value = dp.Tables(0).Rows(i).Item("diff")
            Next
            'DataGridView1_RowEnter()
            'Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("ticketno")
            'Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("intdocno")
            'Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
            'Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
            'Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
            'Me.tb_DRIVERNAM.Text = dp.Tables(0).Rows(0).Item("DCODE")
            'Me.cb_dcode.Text = dp.Tables(0).Rows(0).Item("DRIVERNAM")
            'Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
            'Me.tb_DATE.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
            'Me.d_newdate.Text = dp.Tables(0).Rows(0).Item("POST_DATE")
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("REMARKS"))) Then
            '    Me.tb_comments.Text = dp.Tables(0).Rows(0).Item("REMARKS")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("gpremarks"))) Then
            '    Me.rtb_gprem.Text = dp.Tables(0).Rows(0).Item("gpremarks")
            'End If
            ''If CDate(Me.tb_DATE.Text).Month < Today.Month Then
            ''    Me.d_newdate.Enabled = True
            ''Else
            ''    Me.d_newdate.Enabled = False
            ''End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
            '    Me.tb_CUSTTYPE.Text = dp.Tables(0).Rows(0).Item("CUSTTYPE")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECODE"))) Then
            '    Me.tb_typecode.Text = dp.Tables(0).Rows(0).Item("TYPECODE")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
            '    Me.tb_typecatg_pt.Text = dp.Tables(0).Rows(0).Item("TYPECATG_PT")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNS"))) Then
            '    Me.tb_sapord.Text = dp.Tables(0).Rows(0).Item("VBELNS")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELND"))) Then
            '    Me.tb_sapdocno.Text = dp.Tables(0).Rows(0).Item("VBELND")
            'End If
            'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNI"))) Then
            '    Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("VBELNI")
            'End If
            'If tb_sapord.Text <> "" Or tb_sapdocno.Text <> "" Or tb_sapinvno.Text <> "" Then
            '    'Me.B_PO.Visible = False
            '    'Me.Button1.Visible = False
            '    freeze_scr()
            'Else
            '    'If Me.tb_inout_type.Text = "I" Then
            '    '    b_purchase.Enabled = True
            '    '    b_purchase.Visible = True
            '    'ElseIf Me.tb_inout_type.Text = "O" Then
            '    '    b_deliver.Enabled = True
            '    '    b_deliver.Visible = True
            '    'End If
            'End If
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ss_recon_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text + " - " + glbvar.gcompname
        'Me.tb_save.Visible = False
        Try
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
            dsitm.Clear()
            daitm = New OracleDataAdapter(cmd)
            daitm.TableMappings.Add("Table", "itm")
            daitm.Fill(dsitm)
            conn.Close()
            'listload()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_exit_Click(sender As Object, e As EventArgs) Handles b_exit.Click
        comm.ClosePort()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.ListView1.SelectedItems(0).SubItems(0).Text <> "" Then

                Me.DGV_SML.CurrentRow.Cells("trans_mat").Value = Me.ListView1.SelectedItems(0).SubItems(1).Text

                Me.DGV_SML.CurrentRow.Cells("transfer_mat").Value = Me.ListView1.SelectedItems(0).SubItems(0).Text

                'Me.DataGridView1.CurrentRow.Cells("uom").Value = Me.ListView1.SelectedItems(0).SubItems(2).Text

                Me.ListView1.Visible = False

            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ListView1_DoubleClick1(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.ListView2.SelectedItems(0).SubItems(0).Text <> "" Then

                Me.DGV_SML.CurrentRow.Cells("itemcode").Value = Me.ListView2.SelectedItems(0).SubItems(1).Text

                Me.DGV_SML.CurrentRow.Cells("itemdesc").Value = Me.ListView2.SelectedItems(0).SubItems(0).Text

                'Me.DataGridView1.CurrentRow.Cells("uom").Value = Me.ListView1.SelectedItems(0).SubItems(2).Text

                Me.ListView2.Visible = False

            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV_SML.EditingControlShowing

        If Me.DGV_SML.CurrentCell.ColumnIndex = 10 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)

            RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress

        End If
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DGV_SML.CurrentCell.ColumnIndex = 10 Then
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
    Private Sub DataGridView1_EditingControlShowing1(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV_SML.EditingControlShowing

        If Me.DGV_SML.CurrentCell.ColumnIndex = 0 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)

            RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress1
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress1

        End If
    End Sub
    Private Sub TextBox_KeyPress1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DGV_SML.CurrentCell.ColumnIndex = 0 Then
                Dim tb1 As TextBox = CType(sender, TextBox)
                'itmchar = ""
                'If te <> "" Then
                'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
                If tb1.Text.Length > 0 Then

                    Dim foundrow() As DataRow
                    Dim expression As String = "ITEMDESC LIKE '" & tb1.Text & "%'" & ""
                    foundrow = dsitm.Tables("itm").Select(expression)
                    ListView2.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView2.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
                        Me.ListView2.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)
                        Me.ListView2.Items(i).SubItems.Add(foundrow(i).Item("BASEUOMCODE").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView2.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_SML.CellContentClick

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles b_add.Click
        Try
            If DGV_SML.Rows.Count = 0 Then
                DGV_SML.Rows.Insert(rowIndex:=0)
                DGV_SML.Rows(0).Cells(0).Value = 10
                rowchk = 10
            ElseIf DGV_SML.Rows.Count > 0 Then
                DGV_SML.Rows.Insert(rowIndex:=DGV_SML.Rows.Count)
                rowchk = rowchk + 10
                'DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(0).Value = rowchk
                DGV_SML.Rows(DGV_SML.Rows.Count - 1).Selected = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_SML.CellContentClick
        Try
            If Me.DGV_SML.CurrentRow.Cells("btrans_loc").Selected And Me.DGV_SML.CurrentRow.Cells("docno").Value = "" Then
                Z_MAT_TRANSFER()
            ElseIf Me.DGV_SML.CurrentRow.Cells("btrans_mat").Selected Then
            Else
                MsgBox("Posted Already")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Z_MAT_TRANSFER()

        Try
            Dim cmd As New OracleCommand
            If Me.d_postdate.Text = "" Then
                MsgBox("Please save the record first")
                Me.d_postdate.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox("Select a vendor")
                Me.d_postdate.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox("Select document type")
                Me.d_postdate.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox("Select document type")
                Me.d_postdate.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox("Select an itemcode")
                Me.d_postdate.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox(" First Qty cannot be blank")
                'Me.b_newveh.Focus()
            ElseIf Me.d_postdate.Text = "" Then
                MsgBox(" Second Qty cannot be blank")
            ElseIf Me.d_postdate.Text = "0000000000" Then
                MsgBox("Project should be selected")
                Me.d_postdate.Focus()
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



                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("Z_LOC_LOC_TRANSFER_SML")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("GOODSMVT_HEADER")
                ohdrin.SetValue("PSTNG_DATE", CDate(Me.d_postdate.Text).Year & CDate(Me.d_postdate.Text).Month.ToString("D2") & CDate(Me.d_postdate.Text).Day.ToString("D2"))
                ohdrin.SetValue("DOC_DATE", CDate(Me.d_postdate.Text).Year & CDate(Me.d_postdate.Text).Month.ToString("D2") & CDate(Me.d_postdate.Text).Day.ToString("D2"))

                Dim scltyp As IRfcStructure = sodnbil.GetStructure("GOODSMVT_CODE") 'DLCUST_FIELD 
                scltyp.SetValue("GM_CODE", "04")
                'sodnbil.SetValue("ZSUPPTCNO", Tb_asno.Text)
                'sodnbil.SetValue("ZRTCNO", tb_ticketno.Text)
                'sodnbil.SetValue("ZZTCNO", tb_ticketno.Text)

                Dim sl As Integer = 0
                Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure

                itmstru.SetValue("MATERIAL", Me.DGV_SML.CurrentRow.Cells("itemcode").Value)
                itmstru.SetValue("PLANT", glbvar.divcd)
                itmstru.SetValue("STGE_LOC", glbvar.SSLGORT)
                itmstru.SetValue("MOVE_TYPE", "311")
                Dim qt As Decimal = Convert.ToDecimal(Me.DGV_SML.CurrentRow.Cells("purqty").Value) / 1000 - Convert.ToDecimal(Me.DGV_SML.CurrentRow.Cells("pur_ded").Value) / 1000
                'Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString())
                itmstru.SetValue("ENTRY_QNT", Math.Round(qt, 3))
                itmstru.SetValue("ENTRY_UOM", "TO")
                'itmstru.SetValue("MOVE_MAT", Me.DGV_SML.CurrentRow.Cells("trans_mat").Value)

                itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                itmstru.SetValue("MOVE_STLOC", glbvar.LGORT)
                itmstru.SetValue("LINE_ID", sl)

                oitmin.Append(itmstru)











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
                    tkt(l) = "1234"
                Next
                'write the code for inserting tcket number.

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.Connection = conn

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

                If soercnt > 0 Then
                    MsgBox("There is some error in processing" _
                            & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                            & vbCrLf & soercnt & " error(s)"
                         )
                Else
                    MsgBox("Material Doc: # " & sodnbil.GetValue("MATERIALDOCUMENT").ToString _
                    & vbCrLf & "Delivery Note # " & sodnbil.GetValue("PRICE_DOC").ToString)
                    '& vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    'Me.tb_sapdocno.Text = sodnbil.GetValue("PRICE_DOC").ToString
                    'freeze_scr()
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_U"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value
                    cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("PRICE_DOC").ToString
                    cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                    cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = "1234"
                    cmd.ExecuteNonQuery()
                    conn.Close()

                    Dim endtime = DateTime.Now.ToString()


                End If

                conn.Close()
            End If ' main end if
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tb_ok_Click(sender As Object, e As EventArgs) Handles b_save.Click
        Try
            Dim cn As Integer = Me.DGV_SML.RowCount

            ReDim rnitemcode(cn - 1)
            ReDim rnitemdesc(cn - 1)
            ReDim rndate(cn - 1)
            ReDim rnpurqty(cn - 1)
            ReDim rnpurded(cn - 1)
            ReDim rnsalqty(cn - 1)
            ReDim rnsalded(cn - 1)
            ReDim rnrecqty(cn - 1)
            ReDim rnrecded(cn - 1)
            ReDim rndiff(cn - 1)
            For i = 0 To cn - 1

                rnitemcode(i) = Me.DGV_SML.Rows(i).Cells("itemcode").Value
                rnitemdesc(i) = Me.DGV_SML.Rows(i).Cells("itemdesc").Value
                rndate(i) = Me.DGV_SML.Rows(i).Cells("post_date").Value
                rnpurqty(i) = Me.DGV_SML.Rows(i).Cells("purqty").Value
                rnpurded(i) = Me.DGV_SML.Rows(i).Cells("pur_ded").Value
                rnsalqty(i) = Me.DGV_SML.Rows(i).Cells("salqty").Value
                rnsalded(i) = Me.DGV_SML.Rows(i).Cells("salded").Value
                rnrecqty(i) = Me.DGV_SML.Rows(i).Cells("Recon_qty").Value
                rnrecded(i) = Me.DGV_SML.Rows(i).Cells("Recon_ded").Value
                rndiff(i) = Me.DGV_SML.Rows(i).Cells("Diff").Value

            Next
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        tb_save_Click()
    End Sub
    Private Sub tb_save_Click() 'Handles tb_save.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            'If tmode = 1 Then
            '    sql = " select STWBMIBDSSEQ.nextval val from dual"
            '    dpr = New OracleDataAdapter(sql, conn)
            '    Dim dp As New DataSet
            '    dp.Clear()
            '    dpr.Fill(dp)
            '    If dp.Tables(0).Rows.Count > 0 Then
            '        'Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("val")
            '    End If
            'Else
            '    Dim cmd1 As New OracleCommand()
            '    Dim cmd2 As New OracleCommand()
            '    cmd1.Connection = conn
            '    cmd2.Connection = conn
            '    cmd1.CommandText = " delete from STWBMIS where intdocno = " & Me.Tb_intdocno.Text
            '    cmd2.CommandText = "commit"
            '    cmd1.CommandType = CommandType.Text
            '    cmd2.CommandType = CommandType.Text
            '    cmd1.ExecuteNonQuery()
            '    cmd2.ExecuteNonQuery()

            'End If
            'Try
            '    Dim cn As Integer = Me.DataGridView1.RowCount

            '    ReDim glbvar.rnitemcode(cn - 1)
            '    ReDim glbvar.rnitemdesc(cn - 1)
            '    ReDim glbvar.rndate(cn - 1)
            '    ReDim glbvar.rnpurqty(cn - 1)
            '    ReDim glbvar.rnpurded(cn - 1)
            '    ReDim glbvar.rnsalqty(cn - 1)
            '    ReDim glbvar.rnsalded(cn - 1)
            '    ReDim glbvar.rnrecqty(cn - 1)
            '    ReDim glbvar.rnrecdedqty(cn - 1)
            '    ReDim glbvar.rndiff(cn - 1)
            '    For i = 0 To cn - 1

            '        rnitemcode(i) = Me.DataGridView1.Rows(i).Cells("itemcode").Value
            '        rnitemdesc(i) = Me.DataGridView1.Rows(i).Cells("itemdesc").Value
            '        rndate(i) = Me.DataGridView1.Rows(i).Cells("post_date").Value
            '        rnpurqty(i) = Me.DataGridView1.Rows(i).Cells("purqty").Value
            '        rnpurded(i) = Me.DataGridView1.Rows(i).Cells("pur_ded").Value
            '        rnsalqty(i) = Me.DataGridView1.Rows(i).Cells("salqty").Value
            '        rnsalded(i) = Me.DataGridView1.Rows(i).Cells("salded").Value
            '        rnrecqty(i) = Me.DataGridView1.Rows(i).Cells("Recon_qty").Value
            '        rnrecdedqty(i) = Me.DataGridView1.Rows(i).Cells("Recon_ded").Value
            '        rndiff(i) = Me.DataGridView1.Rows(i).Cells("Diff").Value

            '    Next
            '    'Me.tb_save.Visible = True
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try
            conn.Close()
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_recon"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.ArrayBindCount = glbvar.intiem.Count
            Dim pitemcode As OracleParameter = New OracleParameter(":p1", OracleDbType.Varchar2)
            pitemcode.Direction = ParameterDirection.Input
            pitemcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pitemcode.Value = glbvar.rnitemcode

            Dim pitemdesc As OracleParameter = New OracleParameter("p2:", OracleDbType.Varchar2)
            pitemdesc.Direction = ParameterDirection.Input
            pitemdesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pitemdesc.Value = glbvar.rnitemdesc

            Dim pdate As OracleParameter = New OracleParameter(":p3", OracleDbType.Date)
            pdate.Direction = ParameterDirection.Input
            pdate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pdate.Value = glbvar.rndate

            Dim ppurqty As OracleParameter = New OracleParameter("p4:", OracleDbType.Int32)
            ppurqty.Direction = ParameterDirection.Input
            ppurqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppurqty.Value = glbvar.rnpurqty

            Dim ppurded As OracleParameter = New OracleParameter("p5:", OracleDbType.Int32)
            ppurded.Direction = ParameterDirection.Input
            ppurded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppurded.Value = glbvar.rnpurded

            Dim psalqty As OracleParameter = New OracleParameter("p6:", OracleDbType.Int32)
            psalqty.Direction = ParameterDirection.Input
            psalqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            psalqty.Value = glbvar.rnsalqty

            Dim psalded As OracleParameter = New OracleParameter("p7:", OracleDbType.Int32)
            psalded.Direction = ParameterDirection.Input
            psalded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            psalded.Value = glbvar.rnsalded

            Dim precqty As OracleParameter = New OracleParameter("p8", OracleDbType.Int32)
            precqty.Direction = ParameterDirection.Input
            precqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            precqty.Value = glbvar.rnrecqty

            Dim precded As OracleParameter = New OracleParameter("p9", OracleDbType.Int32)
            precded.Direction = ParameterDirection.Input
            precded.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            precded.Value = glbvar.rnrecded

            Dim pdiff As OracleParameter = New OracleParameter(":p10", OracleDbType.Int32)
            pdiff.Direction = ParameterDirection.Input
            pdiff.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pdiff.Value = glbvar.rndiff

            cmd.Parameters.Add(pitemcode)
            cmd.Parameters.Add(pitemdesc)
            cmd.Parameters.Add(pdate)
            cmd.Parameters.Add(ppurqty)
            cmd.Parameters.Add(ppurded)
            cmd.Parameters.Add(psalqty)
            cmd.Parameters.Add(psalded)
            cmd.Parameters.Add(precqty)
            cmd.Parameters.Add(precded)
            cmd.Parameters.Add(pdiff)

            'cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")
            'If Me.tb_inout_type.Text = "I" Then
            '    Me.b_purchase.Visible = True
            '    Me.b_purchase.Enabled = True
            'ElseIf Me.tb_inout_type.Text = "O" Then
            '    Me.b_deliver.Visible = True
            '    Me.b_deliver.Enabled = True
            'End If
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        'DataGridView1.Rows.Clear()
        'Me.tb_save.Visible = False
        tmode = 2
    End Sub


    Private Sub b_post_Click(sender As Object, e As EventArgs) Handles b_post.Click
        Try
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim saprfcrepos As RfcRepository = saprfcdest.Repository

            Dim pgibi As IRfcFunction = saprfcdest.Repository.CreateFunction("Z_MM_SSRECON_FM")

            Dim pozf As IRfcTable = pgibi.GetTable("T_ZMM_SSRECON_TAB")

            Dim con As Integer = Me.DGV_SML.RowCount
            For i = 0 To con - 1
                Dim pozfstru As IRfcStructure = pozf.Metadata.LineType.CreateStructure
                pozfstru.SetValue("MATNR", Me.DGV_SML.Rows(i).Cells("itemcode").Value)
                pozfstru.SetValue("MAKTX", Me.DGV_SML.Rows(i).Cells("itemdesc").Value)
                pozfstru.SetValue("BUDAT", Me.DGV_SML.Rows(i).Cells("post_date").Value)
                pozfstru.SetValue("PUR_QTY", Me.DGV_SML.Rows(i).Cells("purqty").Value)
                pozfstru.SetValue("PUR_DED", Me.DGV_SML.Rows(i).Cells("pur_ded").Value)
                pozfstru.SetValue("SAL_QTY", Me.DGV_SML.Rows(i).Cells("salqty").Value)
                pozfstru.SetValue("SAL_DED", Me.DGV_SML.Rows(i).Cells("salded").Value)
                pozfstru.SetValue("REC_QTY", Me.DGV_SML.Rows(i).Cells("Recon_qty").Value)
                pozfstru.SetValue("REC_DED", Me.DGV_SML.Rows(i).Cells("Recon_ded").Value)
                pozfstru.SetValue("DIFF", Me.DGV_SML.Rows(i).Cells("Diff").Value)
                pozf.Append(pozfstru)

            Next
            'pozf.Append(pozfstru)
            pgibi.Invoke(saprfcdest)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            conn.Close()
        End Try
    End Sub
End Class