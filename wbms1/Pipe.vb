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
Public Class Pipe

    Private comm As New CommManager()
    Private comm2 As New CommManager2()
    Private comm3 As New CommManager3()
    Dim conn As New OracleConnection
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim laitm As New OracleDataAdapter
    Dim lsitm As New DataSet
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
    Private Sub Pipe_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim cmd, cmd1 As New OracleCommand
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
            'listload()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cmd1.Connection = conn
        cmd1.Parameters.Clear()
        cmd1.CommandText = "curspkg_join.slocmst"
        cmd1.CommandType = CommandType.StoredProcedure
        cmd1.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
        cmd1.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd1.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            lsitm.Clear()
            laitm = New OracleDataAdapter(cmd1)
            laitm.TableMappings.Add("Table", "loc")
            laitm.Fill(lsitm)
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
        Me.tb_DATEIN.Text = Today.Date
        glbvar.scaletype = "2"
        tmode = 1
        Me.Tb_asno.Visible = False
        Me.Tb_cons_sen_branch.Visible = False
        Me.tb_IBDSNO.Visible = False
        Me.tb_orderno.Visible = False
        Me.tb_dsno.Visible = False
        Me.Label25.Visible = False
        Me.Label26.Visible = False
        Me.Label27.Visible = False
        Me.Label34.Visible = False
        Me.Label35.Visible = False
        'Dim uc As UniqueConstraint

    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try

            If Me.DataGridView1.CurrentRow.Cells("Weight").Selected Then
                If tmode = 1 Then
                    'Me.DataGridView1.CurrentRow.Cells("Fwt").Value = Me.rtbDisplay.Text
                    Me.tb_DATEIN.Text = Today.Date
                    Me.tb_timein.Text = Now.ToShortTimeString
                    Me.tb_ticketno.Focus()


                    '    Dim buttonCell As DataGridViewDisableButtonCell = _
                    'CType(DataGridView1.Rows(e.RowIndex).Cells("Buttons"),  _
                    'DataGridViewDisableButtonCell)

                    'Dim checkCell As DataGridViewCheckBoxCell = _
                    '    CType(DataGridView1.Rows(e.RowIndex).Cells("CheckBoxes"),  _
                    '    DataGridViewCheckBoxCell)
                    'buttonCell.Enabled = Not CType(checkCell.Value, [Boolean])
                    'buttonCell.Enabled = False
                    '   Me.DataGridView1.CurrentRow.Cells("Weight") = New DataGridViewDisableButtonCell
                    '   Dim buttonCell As DataGridViewDisableButtonCell = _
                    'CType(DataGridView1.Rows(e.RowIndex).Cells("Weight"),  _
                    'DataGridViewDisableButtonCell)
                    '   buttonCell.Enabled = False
                ElseIf tmode = 2 Then
                    Try
                        'cb_itemcode_SelectedIndexChanged(sender, e)
                        'Me.DataGridView1.CurrentRow.Cells("Swt").Value = Me.rtbDisplay.Text
                        Me.tb_dateout.Text = Today.Date
                        Me.tb_timeout.Text = Now.ToShortTimeString
                        Dim sq As Integer = Convert.ToDecimal(Trim(Me.DataGridView1.CurrentRow.Cells("Swt").Value))
                        If tb_inout_type.Text = "I" Then
                            Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
                            Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value
                            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
                        ElseIf tb_inout_type.Text = "O" Then
                            If Me.tb_sap_doc.Text <> "ZTRE" Then
                                Me.DataGridView1.CurrentRow.Cells("QTY").Value = sq - CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
                                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value
                                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
                            Else
                                Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
                                Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value
                                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
                            End If

                        ElseIf tb_inout_type.Text = "S" Then
                            Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
                            Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            ElseIf Me.DataGridView1.CurrentRow.Cells("CHKBTN").Selected Then
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim pipedet As IRfcFunction = dest.Repository.CreateFunction("Z_FM_PIPE_DET_RET")
                Dim pipeimp As IRfcStructure = pipedet.GetStructure("IPIPEIMP")
                pipeimp.SetValue("IPLANT", glbvar.divcd)
                pipeimp.SetValue("IMATNR", Me.DataGridView1.CurrentRow.Cells("Itemcode").Value)
                pipeimp.SetValue("IPIPENO", Me.DataGridView1.CurrentRow.Cells("PIPENO").Value)
                Dim retpipe As IRfcTable = pipedet.GetTable("PIPERET_STR")
                Dim st As TimeSpan = Now.TimeOfDay
                pipedet.Invoke(dest)
                Dim ed As TimeSpan = Now.TimeOfDay
                'MsgBox("time taken for Pipe FM " & Convert.ToString((ed - st)))
                If retpipe.RowCount > 0 Then


                    For j = 0 To retpipe.RowCount - 1
                        Me.DataGridView1.CurrentRow.Cells("OD").Value = retpipe(j).Item("PIPE_OD").GetValue
                        Me.DataGridView1.CurrentRow.Cells("THICK").Value = retpipe(j).Item("PIPE_THK").GetValue
                        Me.DataGridView1.CurrentRow.Cells("LENGTH").Value = retpipe(j).Item("PIPE_LEN").GetValue
                        Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = retpipe(j).Item("PIPE_QTY").GetValue * 1000
                    Next
                    'Me.DataGridView1.Columns("OD").ReadOnly = True
                    'Me.DataGridView1.Columns("THICK").ReadOnly = True
                    'Me.DataGridView1.Columns("LENGTH").ReadOnly = True

                Else
                    MsgBox(pipedet.GetValue("RETURNMSG").ToString)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            If Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
                Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
           If Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then

                'conn = New OracleConnection(constr)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If

                'sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                '        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                '        & " WHERE z1.userauth_no = z2.userauth_no" _
                '        & " AND z1.username = z2.userid" _
                '        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                '        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells("Itemcode").Value & "'"

                'Dim dpct = New OracleDataAdapter(sql, conn)
                'Dim dpc As New DataSet
                'dpc.Clear()
                'dpct.Fill(dpc)
                'Dim user_tol_value As Decimal
                'Dim user_tot_allowed As Decimal
                'Dim pct = dpc.Tables(0).Rows(0).Item("pct")
                'Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                'Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                'user_tol_value = pct * plist
                'user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                'If pct <> 0 Then
                '    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + user_tol_value
                'ElseIf amt <> 0 Then
                '    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + amt
                'End If
                'If Me.tb_inout_type.Text = "I" Then
                '    If Me.tb_sap_doc.Text = "QO" Then
                '        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_tot_allowed Then

                '            MsgBox("Price not matching as the latest Pricelist")
                '            Me.tb_ok.Enabled = False
                '            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True

                '        Else
                '            Me.tb_ok.Enabled = True

                '        End If

                '    End If

                'End If
                'conn = New OracleConnection(constr)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If

                'sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                '        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                '        & " WHERE z1.userauth_no = z2.userauth_no" _
                '        & " AND z1.username = z2.userid" _
                '        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                '        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells(1).Value & "'"
                ''sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                ''    & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                ''    & " WHERE z1.userauth_no = z2.userauth_no" _
                ''    & " AND z1.username = z2.userid" _
                ''    & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                ''    & " AND z2.matnr = " & "'" & tb_itemdesc.Text & "'"

                'Dim dpct = New OracleDataAdapter(sql, conn)
                'Dim dpc As New DataSet
                'dpc.Clear()
                'dpct.Fill(dpc)
                'Dim user_tol_value As Decimal
                'Dim user_tot_allowed As Decimal
                'Dim pct = dpc.Tables(0).Rows(0).Item("pct")
                'Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                'If Me.tb_inout_type.Text = "I" Then
                '    If Me.tb_sap_doc.Text = "QMX" Then
                '        Dim count = 0
                '        For ai = 0 To DataGridView1.Rows.Count - 1
                '            'Dim plist = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value)
                '            Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                '            user_tol_value = pct * plist
                '            user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value)
                '            If pct <> 0 Then
                '                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + user_tol_value
                '            ElseIf amt <> 0 Then
                '                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + amt
                '            End If

                '            If Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value) > user_tot_allowed Then
                '                count = count + 1
                '                Dim a = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value)
                '            End If
                '        Next
                '        If count > 0 Then
                '            MsgBox("Price not matching as the latest Pricelist")
                '            Me.Button1.Visible = False
                '        Else
                '            Me.Button1.Visible = True
                '            'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                '        End If

                '    Else
                '        'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                '    End If
                'ElseIf glbvar.inout = "O" Then
                '    'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            'If Me.DataGridView1.CurrentRow.Cells(9).Selected Then
            '    'Dim ems As String
            '    'ems = DataGridView1.CurrentRow.Index
            '    'DataGridView1.Rows.Insert(rowIndex:=0)
            'End If
            'If Me.DataGridView1.CurrentRow.Cells(10).Selected Then
            '    Me.DataGridView1.Rows.Remove(Me.DataGridView1.CurrentRow)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
    '    If DataGridView1.CurrentRow.Cells(1).Selected = True Then
    '        'DataGridView1.Rows.Insert(rowIndex:=0)
    '        ListView1.Visible = True
    '        listload()
    '    End If
    '    If DataGridView1.CurrentRow.Cells(1).Selected = False Then
    '        ListView1.Visible = False
    '    End If
    'End Sub

    Private Sub listload()
        Me.ListView1.Items.Clear()
        For i = 0 To dsitm.Tables("itm").Rows.Count - 1
            Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
            Me.ListView1.Items(i).SubItems.Add(dsitm.Tables("itm").Rows(i).Item("ITEMDESC").ToString)
        Next
    End Sub
    Private Sub locload()
        Me.ListView2.Items.Clear()
        For i = 0 To dsitm.Tables("loc").Rows.Count - 1
            Me.ListView2.Items.Add(dsitm.Tables("loc").Rows(i).Item("LGORT").ToString)
            Me.ListView2.Items(i).SubItems.Add(dsitm.Tables("loc").Rows(i).Item("LGORTDESC").ToString)
        Next
    End Sub

    'Private Sub b_firstwt_Click(sender As Object, e As EventArgs) Handles b_firstwt.Click
    '    Try
    '        Me.DataGridView1.Rows(0).Cells(3).Value = Me.rtbDisplay.Text
    '        Me.DataGridView1.CurrentRow.Cells("Fwt").Value = Me.rtbDisplay.Text
    '        Me.tb_DATE.Text = Today.Date
    '        Me.tb_timein.Text = Now.ToShortTimeString
    '        Me.tb_ticketno.Focus()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub b_secondwt_Click(sender As Object, e As EventArgs) Handles b_secondwt.Click
    '    Try
    '        'cb_itemcode_SelectedIndexChanged(sender, e)
    '        Me.DataGridView1.CurrentRow.Cells("Swt").Value = Me.rtbDisplay.Text
    '        Me.tb_dateout.Text = Today.Date
    '        Me.tb_timeout.Text = Now.ToShortTimeString
    '        Dim sq As Integer = Convert.ToDecimal(Trim(Me.DataGridView1.CurrentRow.Cells("Swt").Value))
    '        If tb_inout_type.Text = "I" Then
    '            Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
    '        ElseIf tb_inout_type.Text = "O" Then
    '            If Me.tb_sap_doc.Text <> "ZTRE" Then
    '                Me.DataGridView1.CurrentRow.Cells("QTY").Value = sq - CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
    '            Else
    '                Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value)
    '            End If

    '        ElseIf tb_inout_type.Text = "S" Then
    '            Me.DataGridView1.CurrentRow.Cells("QTY").Value = CDec(Me.DataGridView1.CurrentRow.Cells("Fwt").Value) - sq - CDec(Me.DataGridView1.CurrentRow.Cells("Deduction").Value)
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.ListView1.SelectedItems(0).SubItems(0).Text <> "" Then
                If tb_inout_type.Text <> "O" Then
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    'Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                    'Dim tyear = CDate(Today.Date).Year
                    'Dim docdate = tyear & tmonth & tdate
                    Dim docdate
                    If Me.tb_dateout.Text <> "" Then
                        Dim tdate = CDate(Me.tb_dateout.Text).Day.ToString("D2")
                        Dim tmonth = CDate(Me.tb_dateout.Text).Month.ToString("D2")
                        Dim tyear = CDate(Me.tb_dateout.Text).Year
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
                    ''& " 'AND t.itemcode = " & "'" & 'Me.ListView1.SelectedItems(0).SubItems(1).Text & "'" _
                    Dim it = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    Sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                            & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                            & " FROM   stitmratehd h, stitmrate t, smitem m" _
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

                    dpr = New OracleDataAdapter(Sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)
                    If dp.Tables(0).Rows.Count > 0 Then
                        Me.DataGridView1.CurrentRow.Cells("price").Value = dp.Tables(0).Rows(0).Item("price")
                    End If
                ElseIf tb_inout_type.Text = "O" Then
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    'Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                    'Dim tyear = CDate(Today.Date).Year
                    'Dim docdate = tyear & tmonth & tdate
                    Dim docdate
                    If Me.tb_dateout.Text <> "" Then
                        Dim tdate = CDate(Me.tb_dateout.Text).Day.ToString("D2")
                        Dim tmonth = CDate(Me.tb_dateout.Text).Month.ToString("D2")
                        Dim tyear = CDate(Me.tb_dateout.Text).Year
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
                    ''& " 'AND t.itemcode = " & "'" & 'Me.ListView1.SelectedItems(0).SubItems(1).Text & "'" _
                    Dim it = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    sql = " SELECT   h.div_code,h.yearcode,h.intrateno,h.rateno,h.witheffdt,h.withefftime," _
                            & "t.itemcode,t.itemdesc,t.UOM,MIN_PRICE/1000 price,MAX_PRICE/1000,BUYPRICE/1000" _
                            & " FROM   stitmratehd h, stitmrate t, smitem m" _
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

                Me.ListView1.Visible = False
                Me.DataGridView1.CurrentRow.Cells("itemname").Selected = True

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        Try
           
            Me.DataGridView1.CurrentRow.Cells("location").Value = Me.ListView2.SelectedItems(0).SubItems(0).Text



            Me.ListView2.Visible = False




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub b_connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        comm.Parity = "None"
    '        comm.StopBits = 1
    '        comm.DataBits = 7
    '        comm.BaudRate = 9600
    '        'comm.DisplayWindow = rtbDisplay
    '        comm.OpenPort()
    '        'b_Disconnect.Visible = True
    '        'b_connect.Visible = False
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        'comm.OpenPort()
    '    End Try
    'End Sub
    'Private Sub b_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        comm.ClosePort()
    '        'b_Disconnect.Visible = False
    '        'b_connect.Visible = True
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub tb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_ok.Click
        'If Me.tb_netqty.Text <> Me.tb_totqty.Text Then
        '    MsgBox("Quantity is not matching")
        Dim cntr = 0
        For i = 0 To Me.DataGridView1.RowCount - 1
            If Me.DataGridView1.Rows(i).Cells("Location").Value = "" Then
                'MsgBox("Location should be filled")
                cntr = 1
            End If
        Next
        'Else
        If Me.DataGridView1.Rows.Count = 0 Then
            MsgBox("Enter Details")
        ElseIf cntr = 1 Then
            MsgBox("Location Should be filled")
        ElseIf tb_netqty.Text <> tb_actqty.Text Then
            MsgBox("Quantity not matching")
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
                ReDim odp(cn - 1)
                ReDim thickp(cn - 1)
                ReDim lengthp(cn - 1)
                ReDim pipenop(cn - 1)
                ReDim fwtp(cn - 1)
                ReDim swtp(cn - 1)
                ReDim gnetqty(cn - 1)
                ReDim gvalue(cn - 1)
                ReDim glgort(cn - 1)
                ReDim gactqty(cn - 1)
                For i = 0 To cn - 1
                    pitem(i) = Me.DataGridView1.Rows(i).Cells(0).Value
                    itmcde(i) = Me.DataGridView1.Rows(i).Cells(1).Value
                    itemdes(i) = Me.DataGridView1.Rows(i).Cells(2).Value
                    pqty(i) = Me.DataGridView1.Rows(i).Cells("qty").Value
                    pmultided(i) = Me.DataGridView1.Rows(i).Cells("deduction").Value
                    ppricekg(i) = Me.DataGridView1.Rows(i).Cells("price").Value
                    prate(i) = Me.DataGridView1.Rows(i).Cells("rate").Value
                    odp(i) = Me.DataGridView1.Rows(i).Cells("od").Value
                    thickp(i) = Me.DataGridView1.Rows(i).Cells("thick").Value
                    lengthp(i) = Me.DataGridView1.Rows(i).Cells("length").Value
                    Dim a = Me.DataGridView1.Rows(i).Cells("pipeno").Value
                    If Not IsDBNull(Me.DataGridView1.Rows(i).Cells("pipeno").Value) Then
                        pipenop(i) = Me.DataGridView1.Rows(i).Cells("pipeno").Value
                    End If
                    fwtp(i) = Me.DataGridView1.Rows(i).Cells("fwt").Value
                    swtp(i) = Me.DataGridView1.Rows(i).Cells("swt").Value
                    gnetqty(i) = Me.DataGridView1.Rows(i).Cells("netqty").Value
                    gvalue(i) = Me.DataGridView1.Rows(i).Cells("value").Value
                    glgort(i) = Me.DataGridView1.Rows(i).Cells("location").Value
                    gactqty(i) = Me.DataGridView1.Rows(i).Cells("actqty").Value
                Next
                'Me.tb_save.Visible = True
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            glbvar.vntwt = CInt(Me.tb_actqty.Text)
            glbvar.multdocno = Me.Tb_intdocno.Text
            glbvar.inout = Me.tb_sap_doc.Text
            glbvar.multkt = Me.tb_ticketno.Text
            glbvar.sapdocmulti = Me.tb_sap_doc.Text
            ReDim p_mpono(0)
            ReDim p_mitem(0)
            ReDim p_mqty(0)
            ReDim p_mcomflg(0)
            For i = 0 To 0
                p_mpono(i) = Me.tb_mixpo.Text
                p_mitem(i) = 10
                p_mqty(i) = Me.tb_actqty.Text
                p_mcomflg(i) = ""
            Next
            VALUATIONS.save_mix()
            tb_save_Click()
            'End If
        End If
    End Sub
    Private Sub DataGridView1_RowEnter() Handles DataGridView1.RowValidated
        Try
            Dim tot = 0
            Dim totprice = 0
            Dim acttot = 0
            For i = 0 To Me.DataGridView1.RowCount - 1
                tot = tot + Me.DataGridView1.Rows(i).Cells("NETQTY").FormattedValue
                acttot = acttot + Me.DataGridView1.Rows(i).Cells("ACTQTY").FormattedValue
                totprice = totprice + Me.DataGridView1.Rows(i).Cells("VALUE").FormattedValue
            Next
            Me.tb_totqty.Text = tot
            Me.tb_actqty.Text = acttot
            Me.tb_totval.Text = totprice
        
            'For i = 0 To Me.DataGridView1.RowCount - 1
            '    If Me.DataGridView1.Rows(i).Cells("Location").Value = "" Then
            '        MsgBox("Location should be filled")
            '    End If
            'Next
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
            sql = " select count(itemcode) cnt from STWBMPIPE WHERE ticketno = " & Me.tb_rticketno.Text
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = " select * from STWBMPIPE WHERE ticketno = " & Me.tb_rticketno.Text _
                  & "  order by slno desc "
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")

            For i = 0 To cns - 1
                DataGridView1.Rows.Insert(rowIndex:=0)
                Me.DataGridView1.Rows(0).Cells(0).Value = dp.Tables(0).Rows(i).Item("slno")
                Me.DataGridView1.Rows(0).Cells(1).Value = dp.Tables(0).Rows(i).Item("Itemcode")
                Me.DataGridView1.Rows(0).Cells(2).Value = dp.Tables(0).Rows(i).Item("Itemdesc")
                Me.DataGridView1.Rows(0).Cells("qty").Value = dp.Tables(0).Rows(i).Item("qty")
                Me.DataGridView1.Rows(0).Cells("deduction").Value = dp.Tables(0).Rows(i).Item("DEDUCTIONWT")
                Me.DataGridView1.Rows(0).Cells("price").Value = dp.Tables(0).Rows(i).Item("priceton")
                Me.DataGridView1.Rows(0).Cells("rate").Value = dp.Tables(0).Rows(i).Item("rate")
                Me.DataGridView1.Rows(0).Cells("od").Value = dp.Tables(0).Rows(i).Item("od")
                Me.DataGridView1.Rows(0).Cells("thick").Value = dp.Tables(0).Rows(i).Item("thick")
                Me.DataGridView1.Rows(0).Cells("length").Value = dp.Tables(0).Rows(i).Item("length")
                Dim a = dp.Tables(0).Rows(i).Item("pipeno")
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("pipeno")) Then
                    Me.DataGridView1.Rows(0).Cells("pipeno").Value = dp.Tables(0).Rows(i).Item("pipeno")
                End If
                Me.DataGridView1.Rows(0).Cells("fwt").Value = dp.Tables(0).Rows(i).Item("fwt")
                Me.DataGridView1.Rows(0).Cells("swt").Value = dp.Tables(0).Rows(i).Item("swt")
                Me.DataGridView1.Rows(0).Cells("docno").Value = dp.Tables(0).Rows(i).Item("intdocno")
                Me.DataGridView1.Rows(0).Cells("tktno").Value = dp.Tables(0).Rows(i).Item("ticketno")
                Me.DataGridView1.Rows(0).Cells("inout").Value = dp.Tables(0).Rows(i).Item("INOUTTYPE")
                Me.DataGridView1.Rows(0).Cells("vcode").Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                Me.DataGridView1.Rows(0).Cells("vname").Value = dp.Tables(0).Rows(i).Item("SLEDDESC")
                Me.DataGridView1.Rows(0).Cells("sapdoc").Value = dp.Tables(0).Rows(i).Item("BSART")
                Me.DataGridView1.Rows(0).Cells("datein").Value = dp.Tables(0).Rows(i).Item("DATEIN")
                Me.DataGridView1.Rows(0).Cells("DATEOUT").Value = dp.Tables(0).Rows(0).Item("DATEOUT")
                Me.DataGridView1.Rows(0).Cells("TIMEIN").Value = dp.Tables(0).Rows(0).Item("TIMEIN")
                Me.DataGridView1.Rows(0).Cells("TIMOUT").Value = dp.Tables(0).Rows(0).Item("TIMOUT")
                Me.DataGridView1.Rows(0).Cells("NUMBEROFPCS").Value = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
                Me.DataGridView1.Rows(0).Cells("LABOUR_CHARGE").Value = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                Me.DataGridView1.Rows(0).Cells("PENALTY").Value = dp.Tables(0).Rows(0).Item("PENALTY")
                Me.DataGridView1.Rows(0).Cells("MACHINE_CHARGE").Value = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                Me.DataGridView1.Rows(0).Cells("TRANS_CHARGE").Value = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")
                Me.DataGridView1.Rows(0).Cells("CONSNO").Value = dp.Tables(0).Rows(0).Item("CONSNO")
                Me.DataGridView1.Rows(0).Cells("SORDERNO").Value = dp.Tables(0).Rows(0).Item("SORDERNO")
                Me.DataGridView1.Rows(0).Cells("DELIVERYNO").Value = dp.Tables(0).Rows(0).Item("DELIVERYNO")
                Me.DataGridView1.Rows(0).Cells("PONO").Value = dp.Tables(0).Rows(0).Item("PONO")
                Me.DataGridView1.Rows(0).Cells("AGMIXNO").Value = dp.Tables(0).Rows(0).Item("AGMIXNO")
                Me.DataGridView1.Rows(0).Cells("CCIC").Value = dp.Tables(0).Rows(0).Item("CCIC")
                Me.DataGridView1.Rows(0).Cells("VEHICLENO").Value = dp.Tables(0).Rows(0).Item("VEHICLENO")
                Me.DataGridView1.Rows(0).Cells("OTHVENCUST").Value = dp.Tables(0).Rows(0).Item("OTHVENCUST")
                Me.DataGridView1.Rows(0).Cells("REMARKS").Value = dp.Tables(0).Rows(0).Item("REMARKS")
                Me.DataGridView1.Rows(0).Cells("DRIVERNAM").Value = dp.Tables(0).Rows(0).Item("DRIVERNAM")
                Me.DataGridView1.Rows(0).Cells("DCODE").Value = dp.Tables(0).Rows(0).Item("DCODE")
                Me.DataGridView1.Rows(0).Cells("netqty").Value = dp.Tables(0).Rows(i).Item("netqty")
                Me.DataGridView1.Rows(0).Cells("value").Value = dp.Tables(0).Rows(i).Item("value")
                Me.DataGridView1.Rows(0).Cells("CUSTTYPE").Value = dp.Tables(0).Rows(i).Item("CUSTTYPE")
                Me.DataGridView1.Rows(0).Cells("TYPECODE").Value = dp.Tables(0).Rows(i).Item("TYPECODE")
                Me.DataGridView1.Rows(0).Cells("TYPECATG_PT").Value = dp.Tables(0).Rows(i).Item("TYPECATG_PT")
                Me.DataGridView1.Rows(0).Cells("MIXPO").Value = dp.Tables(0).Rows(i).Item("MIXPO")
                Me.DataGridView1.Rows(0).Cells("LOCATION").Value = dp.Tables(0).Rows(i).Item("LGORT")
                Me.DataGridView1.Rows(0).Cells("actqty").Value = dp.Tables(0).Rows(i).Item("actqty")
                Me.DataGridView1.Rows(0).Cells("werks").Value = dp.Tables(0).Rows(i).Item("WERKS")
                'Me.DataGridView1.Rows(0).Cells("BUYER").Value = dp.Tables(0).Rows(0).Item("BUYER")
            Next
            DataGridView1_RowEnter()
            Me.tb_netqty.Text = Me.tb_actqty.Text
            Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("ticketno")
            Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("intdocno")
            Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
            Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
            Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
            Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
            Me.tb_DATEIN.Text = dp.Tables(0).Rows(0).Item("DATEIN")
            Me.tb_dateout.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
            Me.tb_timein.Text = dp.Tables(0).Rows(0).Item("TIMEIN")
            If dp.Tables(0).Rows(0).Item("TIMOUT").ToString <> "" Then
                Me.tb_timeout.Text = dp.Tables(0).Rows(0).Item("TIMOUT")
            End If
            Me.tb_numberofpcs.Text = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
            Me.Tb_labourcharges.Text = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
            Me.Tb_penalty.Text = dp.Tables(0).Rows(0).Item("PENALTY")
            Me.Tb_eqpchrgs.Text = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
            Me.Tb_transp.Text = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")
            Me.Tb_cons_sen_branch.Text = dp.Tables(0).Rows(0).Item("CONSNO")
            Me.tb_orderno.Text = dp.Tables(0).Rows(0).Item("SORDERNO")
            Me.tb_dsno.Text = dp.Tables(0).Rows(0).Item("DELIVERYNO")
            Me.Tb_asno.Text = dp.Tables(0).Rows(0).Item("PONO")
            Me.tb_IBDSNO.Text = dp.Tables(0).Rows(0).Item("AGMIXNO")
            Me.Tb_ccic.Text = dp.Tables(0).Rows(0).Item("CCIC").ToString
            Me.Tb_vehicleno.Text = dp.Tables(0).Rows(0).Item("VEHICLENO").ToString
            Me.tb_oth_ven_cust.Text = dp.Tables(0).Rows(0).Item("OTHVENCUST").ToString
            Me.tb_comments.Text = dp.Tables(0).Rows(0).Item("REMARKS").ToString
            Me.tb_DRIVERNAM.Text = dp.Tables(0).Rows(0).Item("DRIVERNAM").ToString
            Me.cb_dcode.Text = dp.Tables(0).Rows(0).Item("DCODE").ToString
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("CUSTTYPE"))) Then
                Me.tb_CUSTTYPE.Text = dp.Tables(0).Rows(0).Item("CUSTTYPE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECODE"))) Then
                Me.tb_typecode.Text = dp.Tables(0).Rows(0).Item("TYPECODE")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("TYPECATG_PT"))) Then
                Me.tb_typecatg_pt.Text = dp.Tables(0).Rows(0).Item("TYPECATG_PT")
            End If
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("MIXPO"))) Then
                Me.tb_mixpo.Text = dp.Tables(0).Rows(0).Item("MIXPO")
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
            If Not (IsDBNull(dp.Tables(0).Rows(0).Item("WERKS"))) Then
                Me.tb_werks.Text = dp.Tables(0).Rows(0).Item("WERKS")
            End If
            Me.Tb_asno.Visible = False
            Me.Tb_cons_sen_branch.Visible = False
            Me.tb_IBDSNO.Visible = False
            Me.tb_orderno.Visible = False
            Me.tb_dsno.Visible = False
            Me.Label25.Visible = False
            Me.Label26.Visible = False
            Me.Label27.Visible = False
            Me.Label34.Visible = False
            Me.Label35.Visible = False
            'Me.tb_buyer.Text = dp.Tables(0).Rows(0).Item("BUYER").ToString
            If Me.tb_sap_doc.Text = "QN" Then
                Me.Tb_asno.Visible = True
                Me.Label34.Visible = True
            ElseIf Me.tb_sap_doc.Text = "QPX" Then
                Me.Tb_asno.Visible = True
                Me.Label34.Visible = True
            ElseIf Me.tb_sap_doc.Text = "QI" Then
                Me.Tb_cons_sen_branch.Visible = True
                Me.Label35.Visible = True
            ElseIf Me.tb_sap_doc.Text = "QIM" Then
                Me.Tb_cons_sen_branch.Visible = True
                Me.Label35.Visible = True
            ElseIf Me.tb_sap_doc.Text = "QIX" Then
                Me.Tb_cons_sen_branch.Visible = True
                Me.tb_IBDSNO.Visible = True
                Me.Label35.Visible = True
                Me.Label27.Visible = True
            ElseIf Me.tb_sap_doc.Text = "QMX" Then
                'Me.tb_IBDSNO.Visible = True
                'Me.Label27.Visible = True
            ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                Me.tb_orderno.Visible = True
                Me.tb_dsno.Visible = True
                Me.Label25.Visible = True
                Me.Label26.Visible = True
            ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                Me.tb_orderno.Visible = True
                Me.Label26.Visible = True
            Else
                Me.Tb_asno.Visible = False
                Me.Tb_cons_sen_branch.Visible = False
                Me.tb_IBDSNO.Visible = False
                Me.tb_orderno.Visible = False
                Me.tb_dsno.Visible = False
                Me.Label25.Visible = False
                Me.Label26.Visible = False
                Me.Label27.Visible = False
                Me.Label34.Visible = False
                Me.Label35.Visible = False
            End If
            Me.p_mix.Visible = False
            glbvar.mix = False
            If Me.tb_sapord.Text <> "" Or Me.tb_sapdocno.Text <> "" Or Me.tb_sapinvno.Text <> "" Then
                Me.b_purchase.Enabled = False
                Me.b_deliver.Enabled = False
                Me.tb_ok.Enabled = False
            Else
                Me.b_purchase.Enabled = True
                Me.b_deliver.Enabled = True
                Me.tb_ok.Enabled = True
            End If
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub tb_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_save.Click
    Private Sub tb_save_Click() 'ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles tb_save.Click
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
                '    Dim cmd1 As New OracleCommand
                '    Dim cmd2 As New OracleCommand
                '    cmd1.Connection = conn
                '    cmd2.Connection = conn
                '    cmd1.CommandText = " delete from STWBMPIPE where intdocno = " & Me.Tb_intdocno.Text
                '    cmd2.CommandText = "commit"
                '    cmd1.CommandType = CommandType.Text
                '    cmd2.CommandType = CommandType.Text
                '    cmd1.ExecuteNonQuery()
                '    cmd2.ExecuteNonQuery()

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
            ReDim glbvar.gcusttype(coun - 1)
            ReDim glbvar.gtypecode(coun - 1)
            ReDim glbvar.gtypecatg_pt(coun - 1)
            ReDim glbvar.gmixpo(coun - 1)
            ReDim glbvar.gwerks(coun - 1)
            'ReDim glbvar.glgort(coun - 1)
            For i = 0 To coun - 1
                glbvar.pindocn(i) = CInt(Me.Tb_intdocno.Text)
                glbvar.ptktno(i) = CDec(Me.tb_ticketno.Text)
                glbvar.pino(i) = Me.tb_inout_type.Text
                glbvar.pvencode(i) = Me.tb_sledcode.Text
                glbvar.pvendesc(i) = Me.cb_sleddesc.Text
                glbvar.psapdoccode(i) = Me.tb_sap_doc.Text
                Dim dtin As Date = FormatDateTime(Me.tb_DATEIN.Text, DateFormat.GeneralDate)
                glbvar.p_DATEIN(i) = dtin 'CDate(tb_DATEIN.Text)
                If Me.tb_dateout.Text <> "" Then
                    Dim dtout As Date = FormatDateTime(Me.tb_dateout.Text, DateFormat.GeneralDate)
                    glbvar.p_dateout(i) = dtout 'CDate(tb_DATEIN.Text) 'CDate(tb_dateout.Text)
                End If
                glbvar.p_timein(i) = Me.tb_timein.Text
                glbvar.p_timeout(i) = Me.tb_timeout.Text
                glbvar.p_numberofpcs(i) = Me.tb_numberofpcs.Text
                glbvar.p_labourcharges(i) = Me.Tb_labourcharges.Text
                glbvar.p_penalty(i) = Me.Tb_penalty.Text
                glbvar.p_eqpchrgs(i) = Me.Tb_eqpchrgs.Text
                glbvar.p_transp(i) = Me.Tb_transp.Text
                glbvar.p_cons_sen_branch(i) = Me.Tb_cons_sen_branch.Text
                glbvar.p_orderno(i) = Me.tb_orderno.Text
                glbvar.p_dsno(i) = Me.tb_dsno.Text
                glbvar.p_asno(i) = Me.Tb_asno.Text
                glbvar.p_IBDSNO(i) = Me.tb_IBDSNO.Text
                glbvar.p_ccic(i) = Me.Tb_ccic.Text
                glbvar.p_vehicleno(i) = Me.Tb_vehicleno.Text
                glbvar.p_oth_ven_cust(i) = Me.tb_oth_ven_cust.Text
                glbvar.p_comments(i) = Me.tb_comments.Text
                glbvar.p_DRIVERNAM(i) = Me.tb_DRIVERNAM.Text
                glbvar.p_dcode(i) = Me.cb_dcode.Text
                glbvar.p_buyer(i) = Me.tb_buyer.Text
                glbvar.gcusttype(i) = Me.tb_CUSTTYPE.Text
                glbvar.gtypecode(i) = Me.tb_typecode.Text
                glbvar.gtypecatg_pt(i) = Me.tb_typecatg_pt.Text
                glbvar.gmixpo(i) = Me.tb_mixpo.Text
                glbvar.gwerks(i) = Me.tb_werks.Text
            Next
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If


            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_pipearr"
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
            ppdateout.Value = glbvar.p_dateout

            Dim pptimein As OracleParameter = New OracleParameter("p9:", OracleDbType.Varchar2)
            pptimein.Direction = ParameterDirection.Input
            pptimein.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pptimein.Value = glbvar.p_timein

            Dim pptimout As OracleParameter = New OracleParameter("p10:", OracleDbType.Varchar2)
            pptimout.Direction = ParameterDirection.Input
            pptimout.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pptimout.Value = glbvar.p_timeout

            Dim ppnopcs As OracleParameter = New OracleParameter("p11:", OracleDbType.Decimal)
            ppnopcs.Direction = ParameterDirection.Input
            ppnopcs.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppnopcs.Value = glbvar.p_numberofpcs

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

            Dim ppconsign As OracleParameter = New OracleParameter("p16:", OracleDbType.Varchar2)
            ppconsign.Direction = ParameterDirection.Input
            ppconsign.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppconsign.Value = glbvar.p_cons_sen_branch

            Dim pporderno As OracleParameter = New OracleParameter("p17:", OracleDbType.Varchar2)
            pporderno.Direction = ParameterDirection.Input
            pporderno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pporderno.Value = glbvar.p_orderno

            Dim ppdsno As OracleParameter = New OracleParameter("p18:", OracleDbType.Varchar2)
            ppdsno.Direction = ParameterDirection.Input
            ppdsno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppdsno.Value = glbvar.p_dsno

            Dim ppasno As OracleParameter = New OracleParameter("p19:", OracleDbType.Varchar2)
            ppasno.Direction = ParameterDirection.Input
            ppasno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppasno.Value = glbvar.p_asno

            Dim ppibdsno As OracleParameter = New OracleParameter("p20:", OracleDbType.Varchar2)
            ppibdsno.Direction = ParameterDirection.Input
            ppibdsno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppibdsno.Value = glbvar.p_IBDSNO

            Dim ppccic As OracleParameter = New OracleParameter("p21:", OracleDbType.Varchar2)
            ppccic.Direction = ParameterDirection.Input
            ppccic.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppccic.Value = glbvar.p_ccic

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

            Dim pod As OracleParameter = New OracleParameter(":p34", OracleDbType.Decimal)
            pod.Direction = ParameterDirection.Input
            pod.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pod.Value = glbvar.odp

            Dim pthick As OracleParameter = New OracleParameter(":p35", OracleDbType.Decimal)
            pthick.Direction = ParameterDirection.Input
            pthick.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pthick.Value = glbvar.thickp

            Dim plength As OracleParameter = New OracleParameter(":p36", OracleDbType.Decimal)
            plength.Direction = ParameterDirection.Input
            plength.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            plength.Value = glbvar.lengthp

            Dim ppipeno As OracleParameter = New OracleParameter(":p37", OracleDbType.Varchar2)
            ppipeno.Direction = ParameterDirection.Input
            ppipeno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            ppipeno.Value = glbvar.pipenop

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

            Dim pomcusttype As OracleParameter = New OracleParameter(":p42", OracleDbType.Varchar2)
            pomcusttype.Direction = ParameterDirection.Input
            pomcusttype.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomcusttype.Value = glbvar.gcusttype

            Dim pomtypecode As OracleParameter = New OracleParameter(":p43", OracleDbType.Varchar2)
            pomtypecode.Direction = ParameterDirection.Input
            pomtypecode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomtypecode.Value = glbvar.gtypecode

            Dim pomtypecatg_pt As OracleParameter = New OracleParameter(":p44", OracleDbType.Varchar2)
            pomtypecatg_pt.Direction = ParameterDirection.Input
            pomtypecatg_pt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pomtypecatg_pt.Value = glbvar.gtypecatg_pt

            Dim pmixpo As OracleParameter = New OracleParameter(":p45", OracleDbType.Varchar2)
            pmixpo.Direction = ParameterDirection.Input
            pmixpo.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pmixpo.Value = glbvar.gmixpo

            Dim plgort As OracleParameter = New OracleParameter(":p46", OracleDbType.Varchar2)
            plgort.Direction = ParameterDirection.Input
            plgort.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            plgort.Value = glbvar.glgort

            Dim pactqty As OracleParameter = New OracleParameter(":p47", OracleDbType.Decimal)
            pactqty.Direction = ParameterDirection.Input
            pactqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pactqty.Value = glbvar.gactqty

            Dim pwerks As OracleParameter = New OracleParameter(":p48", OracleDbType.Varchar2)
            pwerks.Direction = ParameterDirection.Input
            pwerks.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            pwerks.Value = glbvar.gwerks

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
            cmd.Parameters.Add(ppnopcs)
            cmd.Parameters.Add(pplab)
            cmd.Parameters.Add(pppenalty)
            cmd.Parameters.Add(ppeqp)
            cmd.Parameters.Add(pptrans)
            cmd.Parameters.Add(ppconsign)
            cmd.Parameters.Add(pporderno)
            cmd.Parameters.Add(ppdsno)
            cmd.Parameters.Add(ppasno)
            cmd.Parameters.Add(ppibdsno)
            cmd.Parameters.Add(ppccic)
            cmd.Parameters.Add(ppvehicle)
            cmd.Parameters.Add(ppothvc)
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
            cmd.Parameters.Add(pod)
            cmd.Parameters.Add(pthick)
            cmd.Parameters.Add(plength)
            cmd.Parameters.Add(ppipeno)
            cmd.Parameters.Add(pfwt)
            cmd.Parameters.Add(pswt)
            cmd.Parameters.Add(pnetqty)
            cmd.Parameters.Add(pvalue)
            cmd.Parameters.Add(pomcusttype)
            cmd.Parameters.Add(pomtypecode)
            cmd.Parameters.Add(pomtypecatg_pt)
            cmd.Parameters.Add(pmixpo)
            cmd.Parameters.Add(plgort)
            cmd.Parameters.Add(pactqty)
            cmd.Parameters.Add(pwerks)
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Varchar2)).Value = Me.tb_ticketno.Text
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")
            'multi_itm.DataGridView1.Rows.Clear()
            'cmd.Parameters.Clear()
            'clear_scr()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        'DataGridView1.Rows.Clear()
        Me.tb_save.Visible = False
        'conn.Close()


    End Sub

    Private Sub b_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_print.Click
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
        unfreeze()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMPIPE WBM WHERE INOUTTYPE = 'I' "
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
            Me.tb_sap_doc.Text = "QD"
            tmode = 1
            tb_inout_type.Text = "I"
            tb_inout_desc.Text = "Incoming Goods"

            Me.cb_sleddesc.Text = "One Time Vendor"
            Me.tb_sledcode.Text = "0000050004"
            Me.p_mix.Visible = True
            'Me.tb_ticketno.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr_scr()
        unfreeze()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMPIPE WBM WHERE INOUTTYPE = 'O' "
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
            Me.p_mix.Visible = True
            'Me.DataGridView1.Columns("OD").ReadOnly = True
            'Me.DataGridView1.Columns("THICK").ReadOnly = True
            'Me.DataGridView1.Columns("LENGTH").ReadOnly = True
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
    Private Sub DataGridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

        If Me.DataGridView1.CurrentCell.ColumnIndex = 51 And Not e.Control Is Nothing Then
            Dim lb As TextBox = CType(e.Control, TextBox)

            RemoveHandler lb.KeyPress, AddressOf locBox_KeyPress
            AddHandler lb.KeyPress, AddressOf locBox_KeyPress

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

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView1.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub locBox_KeyPress(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DataGridView1.CurrentCell.ColumnIndex = 51 Then
                Dim lb1 As TextBox = CType(sender, TextBox)
                'itmchar = ""
                'If te <> "" Then
                'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
                If lb1.Text.Length > 0 Then

                    Dim foundrow() As DataRow
                    Dim expression As String = "LGORTDESC LIKE '" & lb1.Text & "%'" & ""
                    foundrow = lsitm.Tables("loc").Select(expression)
                    ListView2.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView2.Items.Add(foundrow(i).Item("LGORT").ToString)
                        Me.ListView2.Items(i).SubItems.Add(foundrow(i).Item("LGORTDESC").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView2.Visible = True
                End If
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
        '    tb_save_Click()
        If tb_sap_doc.Text = "QD" Then
            ZMMPOGRPROCESSPIPE() 'Direct Purchase

        ElseIf tb_sap_doc.Text = "QN" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            ZMMGENPOPROCESSPIPE() 'Against PO Purchase

        ElseIf tb_sap_doc.Text = "QI" Then
            'Against PO FM Z_MM_GEN_PO_PROCESS ZMMGENPOPROCESS
            ZINTERBRANCHDETAILSUPDPIPE() 'Interbranch complete purchase

        ElseIf tb_sap_doc.Text = "QX" Then
            ZMMMIXMATPROCESSPIPE() 'Mixmaterial purchase

        ElseIf tb_sap_doc.Text = "QMX" Then
            'If glbvar.mix = True Then
            ZMMMIXINMATPROCESSPIPE() ' against mix material purchase
        ElseIf tb_sap_doc.Text = "QPX" Then
            'If glbvar.mix = True Then
            ZMMMIXPOPROCESS() ' against mix material purchase
            'ElseIf glbvar.mix = False Then
            '   MsgBox("Check Mix Material Details")
            'End If

        ElseIf tb_sap_doc.Text = "QIM" Then
            ZMMINTMIXMATPROCESSPIPE() ' interbranch mix material purchase

        ElseIf tb_sap_doc.Text = "QIX" Then
            ZMIXINTERBRANCHDETAILSUPDPIPE() ' interbranch against mix material purchase

            'ElseIf tb_sap_doc.Text = "QO" Then
            '    ZMMOMAUTOPROCESS() 'OM purchase and sales
            '    B_PO.Visible = False
        End If  'Document 
    End Sub
    Public Sub ZMMPOGRPROCESSPIPE()


        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.cb_sleddesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
        Else
            Dim cmd As New OracleCommand
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
            pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text.PadLeft(10, "0"))
            pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
            pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
            pohdrin.SetValue("CURRENCY", "SAR")
            pohdrin.SetValue("DOC_DATE", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
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
            'pocst.SetValue("ZZERDAT", Me.tb_dateout.Text) 'Date on Which Record Was Created
            'pocst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
            pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
            pocst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
            pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
            pocst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
            pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
            pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
            pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
            'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

            Dim grcst As IRfcStructure = pogrir.GetStructure("I_GR_HEADER_CUST")
            ' Create field in transaction taable and bring from hremployee table
            grcst.SetValue("ZZINDS", glbvar.scaletype)
            'grcst.SetValue("ZZBNAME", Me.Cb_buyname.Text) 'Buyer Name

            'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
            'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
            grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
            grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
            grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
            grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
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


            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "curspkg_join.get_pipe"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(Me.tb_ticketno.Text)
            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
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
                    poitmu.SetValue("PO_ITEM", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())
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
                    poitmuX.SetValue("PO_UNIT_ISO", "X")
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
                    pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()) / 1000)
                    pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()) / 1000)
                    pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()) / 1000)
                    pozfstru.SetValue("ZZFTUOM", "MT")
                    pozfstru.SetValue("ZZSECUOM", "MT")
                    pozfstru.SetValue("ZZPIPE", dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString()) 'Pipe Number
                    pozfstru.SetValue("ZZOUTN", dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()) 'Pipe OD
                    pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                    pozfstru.SetValue("ZZTHICK", CInt(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                    pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                    pozfstru.SetValue("ZZLEN", dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()) 'LENGTH
                    pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                    pozfstru.SetValue("ZZNOPIPE", CInt(Me.tb_numberofpcs.Text)) 'No: of PIPES
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
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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


    Private Sub b_deliver_Click(sender As Object, e As EventArgs) Handles b_deliver.Click
        If tb_sap_doc.Text = "ZTBV" Then
            ZSDSOPROCESSNEWPIPE()
            'Button1.Visible = False

        ElseIf tb_sap_doc.Text = "ZDCQ" Then

            ZSDDIRECTCONTRACTPIPE()

        ElseIf tb_sap_doc.Text = "ZTCF" Then

            ZSDCONSIGNFILLUP02PIPE()

        ElseIf tb_sap_doc.Text = "ZCWA" Then
            ZSDCWASALESPIPE()

        ElseIf tb_sap_doc.Text = "ZTRE" Then
            ZSDRETURNORDERPIPE()

        End If
    End Sub

    Private Sub tb_ticketno_LostFocus(sender As Object, e As EventArgs) 'Handles tb_ticketno.LostFocus

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
                & "  FROM   STWBMPIPE WBM" _
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
            cmd.CommandText = "curspkg_join.tktrng"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New OracleParameter("pcomp_code", OracleDbType.Varchar2)).Value = glbvar.cmpcd
            cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
            If tb_inout_type.Text = "I" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "IWS"
            ElseIf tb_inout_type.Text = "O" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "DSS"
            ElseIf tb_inout_type.Text = "T" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SNA"
            ElseIf tb_inout_type.Text = "S" Then
                cmd.Parameters.Add(New OracleParameter("poccode", OracleDbType.Varchar2)).Value = "SCL"
            End If
            cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                Dim dsrng As New DataSet
                Dim darng As New OracleDataAdapter(cmd)
                darng.TableMappings.Add("Table", "tktrng")
                darng.Fill(dsrng)
                If Me.tb_ticketno.Text <= dsrng.Tables("tktrng").Rows(0).Item("ENDNO") And Me.tb_ticketno.Text >= dsrng.Tables("tktrng").Rows(0).Item("STARTNO") Then
                    Me.DataGridView1.Focus()
                Else
                    MsgBox("Ticket number not in range should be within " & dsrng.Tables("tktrng").Rows(0).Item("STARTNO") & " - " & dsrng.Tables("tktrng").Rows(0).Item("ENDNO"))
                    Me.tb_ticketno.Focus()
                End If
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try


        End If 'tmode enddif

    End Sub


  
    Private Sub ZMMGENPOPROCESSPIPE()
        'Make ASN Number mandatory
        'Price field to be disabled
        'update wbms table VBELNS - ASN entered by the user, VBELND - GRno returned from FM, VBELNI - IR no returned from FM
        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.cb_sleddesc.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
            'ElseIf Me.Tb_asno.Text = "" Then
            '    MsgBox(" PO # is compulsory")
            '    Me.Tb_asno.Focus()
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

                Dim pogrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_GEN_PO_PROCESS")
                Dim pohdrin As IRfcStructure = pogrir.GetStructure("I_POHEADER")
                'pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QN")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
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
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
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
                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        'Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        ''poitmu.SetValue("QUANTITY", qt)
                        'poitmu.SetValue("PO_UNIT", "KG")
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
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                        pozfstru.SetValue("ZZFTUOM", "KG")
                        pozfstru.SetValue("ZZSECUOM", "KG")
                        pozfstru.SetValue("ZZPIPE", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString())) 'Pipe Number
                        pozfstru.SetValue("ZZOUTN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString())) 'Pipe OD
                        pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                        pozfstru.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                        pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                        pozfstru.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString())) 'LENGTH
                        pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                        pozfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'No: of PIPES
                        pozf.Append(pozfstru)

                        Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                        Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                        gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                        gpozfstru.SetValue("EBELP", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString()) ' Item Number of Purchasing Document
                        gpozfstru.SetValue("MATNR", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())) 'Material Number
                        Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        gpozfstru.SetValue("MENGE", gt) 'Quantity
                        gpozf.Append(gpozfstru)





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
                    MsgBox("Error", MsgBoxStyle.Critical, "There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & "errors"
                           )
                Else
                    MsgBox("Purchase Order # " & pogrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & pogrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    'Me.tb_sapord.Text = pogrir.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = pogrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = pogrir.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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
    Private Sub ZINTERBRANCHDETAILSUPDPIPE()

        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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
                grcst.SetValue("VBELN", Me.Tb_cons_sen_branch.Text) 'SO #
                'grcst.SetValue("MBLNR", "0000000455") 'Material Doc# - Blank in QI
                grcst.SetValue("SENDING_PLANT", tb_sledcode.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("RECEIVING_PLANT", glbvar.divcd) 'Material Doc# - Blank in QI
                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("BUKRS", glbvar.cmpcd) 'Material Doc# - Blank in QI
                grcst.SetValue("BSART", "QI") 'Material Doc# - Blank in QI
                'grcst.SetValue("AEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'grcst.SetValue("ERNAM", "AKMENON") 'Name of Person who Created the Object
                grcst.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("LIFNR", tb_sledcode.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("EKORG", glbvar.EKORG) 'Material Doc# - Blank in QI
                grcst.SetValue("EKGRP", glbvar.EKGRP) 'Material Doc# - Blank in QI
                'grcst.SetValue("BEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTRANSCHR", CDec(Me.Tb_transp.Text))
                grcst.SetValue("ZZPENALTY", CDec(Me.Tb_penalty.Text))
                grcst.SetValue("ZZMACHARGE", CDec(Me.Tb_eqpchrgs.Text))
                grcst.SetValue("ZZLABCHAR", CDec(Me.Tb_labourcharges.Text))
                grcst.SetValue("ZREMARKS", Me.tb_comments.Text)
                grcst.SetValue("CREATED_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZLABCHAR", Me.Tb_labourcharges.Text) for store charges

                conn = New OracleConnection(constr)

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        'Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        ''poitmu.SetValue("QUANTITY", qt)
                        'poitmu.SetValue("PO_UNIT", "KG")
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
                        'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()))
                        'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                        'pozf.Append(pozfstru)

                        'Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                        'Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                        'gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                        'gpozfstru.SetValue("EBELP", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString()) ' Item Number of Purchasing Document
                        'gpozfstru.SetValue("MATNR", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())) 'Material Number
                        'Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        pozfstru.SetValue("MENGE", qt)
                        pozfstru.SetValue("MATKL", "01")
                        pozfstru.SetValue("MEINS", "KG")
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        'Dim sapded As Decimal = 0.0
                        'sapded = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTION").ToString()) + CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString())
                        pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                        Dim sapgrwt As Decimal = 0.0
                        sapgrwt = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) + CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString())
                        pozfstru.SetValue("ZZGROSSWT", sapgrwt)
                        pozfstru.SetValue("ZZFTUOM", "KG")
                        pozfstru.SetValue("ZZSECUOM", "KG")
                        pozfstru.SetValue("ZZPIPE", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString())) 'Pipe Number
                        pozfstru.SetValue("ZZOUTN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString())) 'Pipe OD
                        pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                        pozfstru.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                        pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                        pozfstru.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString())) 'LENGTH
                        pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                        pozfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs) 'No: of PIPES
                        pozfstru.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object

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
                    MsgBox("Error", MsgBoxStyle.Critical, "There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & "errors"
                           )
                Else
                    MsgBox("Ticket # " & Me.tb_ticketno.Text & " Updated")
                    '     & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    'Me.tb_sapord.Text = Me.tb_ticketno.Text
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value
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
    Public Sub ZMMMIXMATPROCESSPIPE()

        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MAT_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QX")
                pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text)
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))

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
                pocst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        poitmu.SetValue("QUANTITY", qt)
                        poitmu.SetValue("PO_UNIT", "KG")
                        poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
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
                        poitmuX.SetValue("PO_UNIT_ISO", "X")
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
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                        pozfstru.SetValue("ZZFTUOM", "KG")
                        pozfstru.SetValue("ZZSECUOM", "KG")
                        pozf.Append(pozfstru)


                    Next

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
                    'Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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
    Public Sub ZMMMIXPOPROCESS()
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_pipe"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
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
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
        ElseIf Me.tb_IBDSNO.Text = "" Then
            MsgBox(" Ag:Mix Material # is compulsory")
            Me.tb_IBDSNO.Focus()
        Else
            Dim cmd As New OracleCommand

            Try
                If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                    RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                End If
                Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                ' create connection to the RFC repository
                Dim repos As RfcRepository = dest.Repository

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MATERIAL_PO_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QX")
                pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))

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
                pocst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
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
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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

                        Dim gpozf As IRfcTable = mmgrir.GetTable("T_GENPO_ITEM")
                        Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                        gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                        gpozfstru.SetValue("EBELP", itm) 'Convert.ToDecimal(tb_itmno.Text))  Item Number of Purchasing Document
                        gpozfstru.SetValue("MATNR", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                        gpozfstru.SetValue("WERKS", glbvar.divcd) 'Material Number
                        'Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())/1000
                        Dim qtt As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000, 3)
                        gpozfstru.SetValue("MENGE", qtt) 'Quantity
                        gpozf.Append(gpozfstru)
                        Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", itm)
                        poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)

                        Dim qt As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000, 3)
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
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "TO")
                        pozfstru.SetValue("ZZSECUOM", "TO")
                        pozfstru.SetValue("ZZPIPE", dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString()) 'Pipe Number
                        pozfstru.SetValue("ZZOUTN", dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()) 'Pipe OD
                        pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                        pozfstru.SetValue("ZZTHICK", CInt(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                        pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                        pozfstru.SetValue("ZZLEN", dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()) 'LENGTH
                        pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                        pozfstru.SetValue("ZZNOPIPE", CInt(Me.tb_numberofpcs.Text)) 'No: of PIPES
                        pozf.Append(pozfstru)


                    Next

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
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value 'mmgrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = mmgrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    glbvar.mix = False
                    Me.b_purchase.Enabled = False
                    freeze()
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QX")
            End Try


        End If 'Main
    End Sub
    Public Sub ZMMMIXINMATPROCESSPIPE()
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
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join.get_pipe"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
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
        ElseIf CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 And count > 0 Then
            MsgBox("Please enter a price")
        ElseIf Me.tb_IBDSNO.Text = "" Then
            MsgBox(" Ag:Mix Material # is compulsory")
            Me.tb_IBDSNO.Focus()
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
                pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text.PadLeft(10, "0"))
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)
                pohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))

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
                pocst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'pocst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                'grcst.SetValue("ZZTIEX", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)
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
                cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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


                        Dim poitm As IRfcTable = mmgrir.GetTable("T_POITEM")
                        Dim poitmu As IRfcStructure = poitm.Metadata.LineType.CreateStructure
                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.
                        poitmu.SetValue("PO_ITEM", itm)
                        poitmu.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                        poitmu.SetValue("PLANT", glbvar.divcd)
                        poitmu.SetValue("STGE_LOC", glbvar.LGORT)

                        Dim qt As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000, 3)
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
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()) / 1000)
                        pozfstru.SetValue("ZZFTUOM", "TO")
                        pozfstru.SetValue("ZZSECUOM", "TO")
                        pozfstru.SetValue("ZZPIPE", dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString()) 'Pipe Number
                        pozfstru.SetValue("ZZOUTN", dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()) 'Pipe OD
                        pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                        pozfstru.SetValue("ZZTHICK", CInt(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                        pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                        pozfstru.SetValue("ZZLEN", dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()) 'LENGTH
                        pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                        pozfstru.SetValue("ZZNOPIPE", CInt(Me.tb_numberofpcs.Text)) 'No: of PIPES
                        pozf.Append(pozfstru)


                    Next

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
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = mmgrir.GetValue("E_PONUMBER").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = mmgrir.GetValue("E_INVOICENO").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    glbvar.mix = False
                    Me.b_purchase.Enabled = False
                    freeze()
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From QX")
            End Try


        End If 'Main
    End Sub
    Public Sub ZMMINTMIXMATPROCESSPIPE()

        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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

                Dim mmgrir As IRfcFunction = dest.Repository.CreateFunction("Z_MM_MIX_MAT_PROCESS")
                Dim pohdrin As IRfcStructure = mmgrir.GetStructure("I_POHEADER")
                pohdrin.SetValue("COMP_CODE", glbvar.BUKRS)
                pohdrin.SetValue("DOC_TYPE", "QI")
                pohdrin.SetValue("VENDOR", Me.tb_sledcode.Text)
                pohdrin.SetValue("PURCH_ORG", glbvar.EKORG)
                pohdrin.SetValue("PUR_GROUP", glbvar.EKGRP)
                pohdrin.SetValue("CURRENCY", "SAR")
                pohdrin.SetValue("CREATED_BY", glbvar.userid)

                Dim pohdrinx As IRfcStructure = mmgrir.GetStructure("I_POHEADERX")
                pohdrinx.SetValue("COMP_CODE", "X")
                pohdrinx.SetValue("DOC_TYPE", "X")
                pohdrinx.SetValue("VENDOR", "X")
                pohdrinx.SetValue("PURCH_ORG", "X")
                pohdrinx.SetValue("PUR_GROUP", "X")
                pohdrinx.SetValue("CURRENCY", "X")
                pohdrin.SetValue("CREATED_BY", "X")

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
                pocst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                'pocst.SetValue("ZZERDAT", Me.tb_DATEOUT.Text) 'Date on Which Record Was Created
                pocst.SetValue("ZZERDAT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                pocst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                pocst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                pocst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                pocst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                pocst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                pocst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'pocst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

                Dim grcst As IRfcStructure = mmgrir.GetStructure("I_GR_HEADER_CUST")
                ' Create field in transaction taable and bring from hremployee table
                grcst.SetValue("ZZINDS", glbvar.scaletype) 'Buyer Name
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name

                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZERNAM", glbvar.userid) 'Name of Person who Created the Object
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTCNO", Me.tb_ticketno.Text)
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                'grcst.SetValue("ZZDLINC", Me.tb_DRIVINGLICNO.Text)

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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        poitmu.SetValue("QUANTITY", qt)
                        poitmu.SetValue("PO_UNIT", "KG")
                        poitmu.SetValue("PO_UNIT_ISO", "KGM")
                        Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
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
                        poitmuX.SetValue("PO_UNIT_ISO", "X")
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
                        'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()))
                        'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                        'pozf.Append(pozfstru)






                    Next

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
                    MsgBox("Error", MsgBoxStyle.Critical, "There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & "errors"
                           )
                Else
                    MsgBox("Purchase Order # " & mmgrir.GetValue("E_PONUMBER").ToString _
                          & vbCrLf & "Goods Receipt  # " & mmgrir.GetValue("E_MATERIALDOCNO").ToString _
                          & vbCrLf & "Invoice        # " & mmgrir.GetValue("E_INVOICENO").ToString)
                    'Me.tb_sapord.Text = mmgrir.GetValue("E_PONUMBER").ToString
                    'Me.tb_sapdocno.Text = mmgrir.GetValue("E_MATERIALDOCNO").ToString
                    'Me.tb_sapinvno.Text = mmgrir.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ZMIXINTERBRANCHDETAILSUPDPIPE()
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
        ElseIf Me.Tb_cons_sen_branch.Text = "" Then
            MsgBox(" Consignment # is compulsory")
            Me.Tb_cons_sen_branch.Focus()
        ElseIf Me.tb_IBDSNO.Text = "" Then
            MsgBox(" Ag:Mix Material # is compulsory")
            Me.tb_IBDSNO.Focus()
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
                grcst.SetValue("VBELN", Me.Tb_cons_sen_branch.Text) 'SO #
                grcst.SetValue("MBLNR", Me.tb_IBDSNO.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("SENDING_PLANT", tb_sledcode.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("RECEIVING_PLANT", glbvar.divcd) 'Material Doc# - Blank in QI
                'grcst.SetValue("ZZERDAT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                grcst.SetValue("BUKRS", glbvar.cmpcd) 'Material Doc# - Blank in QI
                grcst.SetValue("BSART", "QI") 'Material Doc# - Blank in QI
                grcst.SetValue("AEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'grcst.SetValue("ERNAM", "AKMENON") 'Name of Person who Created the Object
                'grcst.SetValue("CREATED_BY", "AKMENON") 'Name of Person who Created the Object
                grcst.SetValue("LIFNR", tb_sledcode.Text) 'Material Doc# - Blank in QI
                grcst.SetValue("EKORG", glbvar.EKORG) 'Material Doc# - Blank in QI
                grcst.SetValue("EKGRP", glbvar.EKGRP) 'Material Doc# - Blank in QI
                grcst.SetValue("BEDAT", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZBNAME", Me.tb_buyer.Text) 'Buyer Name
                grcst.SetValue("ZZDATEN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDATEX", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                grcst.SetValue("ZZDNAME", Me.tb_DRIVERNAM.Text)
                grcst.SetValue("ZZVEHINO", Me.Tb_vehicleno.Text)
                grcst.SetValue("ZZTIEN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTIEX", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                grcst.SetValue("ZZTRANSCHR", CDec(Me.Tb_transp.Text))
                grcst.SetValue("ZZPENALTY", CDec(Me.Tb_penalty.Text))
                grcst.SetValue("ZZMACHARGE", CDec(Me.Tb_eqpchrgs.Text))
                grcst.SetValue("ZZLABCHAR", CDec(Me.Tb_labourcharges.Text))
                grcst.SetValue("ZREMARKS", Me.tb_comments.Text)
                grcst.SetValue("CREATED_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                'grcst.SetValue("ZZLABCHAR", Me.Tb_labourcharges.Text) for store charges

                conn = New OracleConnection(constr)

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        'Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        ''poitmu.SetValue("QUANTITY", qt)
                        'poitmu.SetValue("PO_UNIT", "KG")
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
                        'pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()))
                        'pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        'pozfstru.SetValue("ZZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                        'pozf.Append(pozfstru)

                        'Dim gpozf As IRfcTable = pogrir.GetTable("T_GENPO_ITEM")
                        'Dim gpozfstru As IRfcStructure = gpozf.Metadata.LineType.CreateStructure
                        'gpozfstru.SetValue("EBELN", Me.Tb_asno.Text) 'Purchasing Document Number
                        'gpozfstru.SetValue("EBELP", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString()) ' Item Number of Purchasing Document
                        'gpozfstru.SetValue("MATNR", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())) 'Material Number
                        'Dim gt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        pozfstru.SetValue("MENGE", qt)
                        pozfstru.SetValue("MATKL", "01")
                        pozfstru.SetValue("MEINS", "KG")
                        pozfstru.SetValue("ZZFTWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        pozfstru.SetValue("ZZSECWT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        Dim sapded As Decimal = 0.0
                        sapded = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()) ' + CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString())
                        pozfstru.SetValue("ZZDECT", sapded)
                        Dim sapgrwt As Decimal = 0.0
                        sapgrwt = CDec(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString()) + sapded
                        pozfstru.SetValue("ZZGROSSWT", sapgrwt)
                        pozfstru.SetValue("ZZFTUOM", "KG")
                        pozfstru.SetValue("ZZSECUOM", "KG")
                        pozfstru.SetValue("ZZPIPE", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString())) 'Pipe Number
                        pozfstru.SetValue("ZZOUTN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString())) 'Pipe OD
                        pozfstru.SetValue("ZZOUTUOM", "IN") 'OD UOM
                        pozfstru.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString())) 'THICKNESS
                        pozfstru.SetValue("ZZTHICKUOM", "MM") 'THICKNESS UOM
                        pozfstru.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString())) 'LENGTH
                        pozfstru.SetValue("ZZLENUOM", "M") 'LENGTH UOM
                        pozfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs) 'No: of PIPES
                        pozfstru.SetValue("CREATED_BY", glbvar.userid) 'Name of Person who Created the Object
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
                    MsgBox("Error", MsgBoxStyle.Critical, "There is some error in processing" _
                           & vbCrLf & "Please contact SAP Support Center with Ticket Number " _
                           & vbCrLf & poercnt & "errors"
                           )
                Else
                    MsgBox("Ticket # " & Me.tb_ticketno.Text & " Updated")
                    '     & vbCrLf & "Invoice        # " & pogrir.GetValue("E_INVOICENO").ToString)
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    'gen_wbms_sap_U
                    'Me.tb_sapord.Text = Me.tb_ticketno.Text
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value
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
                MsgBox(ex.Message & " From QD")
            End Try


        End If 'Main

        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Public Sub ZSDSOPROCESSNEWPIPE()
        Dim sty = ""
        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            'Me.b_save.Focus()
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CASH_SALES_DPP") 'ZSD_CASH_SALES
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZTBV")
                ohdrin.SetValue("SALES_ORG", Me.tb_CUSTTYPE.Text)
                ohdrin.SetValue("DISTR_CHAN", Me.tb_typecode.Text)
                ohdrin.SetValue("DIVISION", Me.tb_typecatg_pt.Text)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
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
                dlcust.SetValue("ZZTICKET", (Me.tb_ticketno.Text))
                dlcust.SetValue("ZZVEHI", Me.Tb_vehicleno.Text)
                dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZDATOUT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZTIMIN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZINDS", glbvar.scaletype)
                'dlcust.SetValue("ZZCNTNO", Me.tb_container.Text)



                If rb_met.Checked = True Then
                    sty = "ME"
                ElseIf rb_mt.Checked = True Then
                    sty = "MT"
                ElseIf rb_pc.Checked = True Then
                    sty = "PC"

                End If
                
        sodnbil.SetValue("REMARKS_1", Me.tb_comments.Text)
                sodnbil.SetValue("ZDPP_IND", sty)


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

        Try
            'Dim damulti As New OracleDataAdapter(cmd)
            'damulti.TableMappings.Add("Table", "mlt")
            'Dim dsmlti As New DataSet
            'damulti.Fill(dsmlti)
            'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "curspkg_join.get_pipe"
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


                Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure
                itmstru.SetValue("ITM_NUMBER", itm)
                itmstru.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                If tb_werks.Text = "" Then
                    itmstru.SetValue("PLANT", glbvar.divcd)
                    itmstru.SetValue("STORE_LOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                Else
                    itmstru.SetValue("PLANT", Me.tb_werks.Text)
                    itmstru.SetValue("STORE_LOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                End If
                'itmstru.SetValue("STORE_LOC", glbvar.LGORT)
                Dim qt As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("ACTQTY").ToString()) / 1000, 3)
                itmstru.SetValue("TARGET_QTY", qt)
                itmstru.SetValue("SALES_UNIT", "TO")
                If tb_werks.Text = "" Then
                    itmstru.SetValue("SHIP_POINT", glbvar.VSTEL)
                Else
                    itmstru.SetValue("SHIP_POINT", Me.tb_werks.Text)
                End If
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
                Dim rqty As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("ACTQTY").ToString()) / 1000, 3)
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
                tdlcfstru.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()) / 1000)
                tdlcfstru.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()) / 1000)
                'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                tdlcfstru.SetValue("ZZPIPE", dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString())
                tdlcfstru.SetValue("ZZOM", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()))
                tdlcfstru.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString()))
                tdlcfstru.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()))
                tdlcfstru.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                tdlcfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                'tdlcfstru.SetValue("ZZPACKD", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString()))
                tdlcfstru.SetValue("ZZUOMOD", "IN") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                tdlcfstru.SetValue("ZZUOMT", "MM") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                tdlcfstru.SetValue("ZZUOML", "M") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                tdlcfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                tdlcf.Append(tdlcfstru)

                Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                orpstru.SetValue("PARTN_ROLE", "AG")
                orpstru.SetValue("PARTN_NUMB", Me.tb_sledcode.Text.PadLeft(10, "0"))
                'check if the customer is a one time customer then add the test else no need.
                orpstru.SetValue("NAME", Me.cb_sleddesc.Text)
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

            Dim ptkt As OracleParameter = New OracleParameter(":n5", OracleDbType.Decimal)
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
            'Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
            'Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
            'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICE").ToString
            'Write an update procedure for updating the documnt numbers in STWBMIBDS
            cmd.Parameters.Clear()
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
            cmd.CommandType = CommandType.StoredProcedure
            Try
                cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = sodnbil.GetValue("SALESDOCUMENT").ToString
                cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = sodnbil.GetValue("E_DELIVERY").ToString
                cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("E_INVOICE").ToString
                cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_ticketno.Text)
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message & " From Updating")
            End Try
            Dim endtime = DateTime.Now.ToString()

            freeze()

        End If
            Catch ex As Exception
            MsgBox(ex.Message & " From Main ZTBV")
        End Try

        End If ' main end if

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub ZSDRETURNORDERPIPE()
        Dim cmd As New OracleCommand
        ' This call is required by the designer.
        ' Add any initialization after the InitializeComponent() call.

        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            'Me.b_save.Focus()
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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
                dcust.SetValue("ZZVEHI", Me.Tb_vehicleno.Text) 'done
                'dcust.SetValue("ZZVNAME", Me.tb_vehicleno.Text) 'done
                dcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dcust.SetValue("ZZDATOUT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                dcust.SetValue("ZZTIMIN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                dcust.SetValue("ZZTIMOUT", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                dcust.SetValue("ZZINDS", glbvar.scaletype) 'done
                'dcust.SetValue("ZZCNTNO", tb_container.Text) 'done



                pgibi.SetValue("I_RETURNORDER", Me.tb_orderno.Text)

                pgibi.SetValue("I_UNAME", glbvar.userid)

                'dcn1.SetValue("I_DELIVERY", )





                'Dim dpqty As IRfcStructure = pgibil.GetStructure("I_PICKQUANTITY")
                'Dim pqty As Decimal = Convert.ToDecimal(tb_QTY.Text)
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        pqtystr.SetValue("ITM_NUMBER", itm)
                        pqtystr.SetValue("PICK_QTY", rqty)
                        pqtystr.SetValue("PICK_UOM", "KG")
                        pqty.Append(pqtystr)

                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.


                        'Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                        'itcuststr.SetValue("ZZCCIC", "1234")
                        ''hardcoded because of no field 

                        'itcuststr.SetValue("ZZDECT", CDec(Me.tb_ded.Text))

                        ''itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) commented since not found in FM
                        'itcuststr.SetValue("ZZFWGT", CDec(Me.tb_FIRSTQTY.Text))
                        'itcuststr.SetValue("ZZSWGT", CDec(Me.tb_SECONDQTY.Text))
                        'itcuststr.SetValue("ZZPACKD", CDec(Me.tb_packded.Text))
                        'itcust.Append(itcuststr)



                    Next



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
                    'Me.tb_sapinvno.Text = pgibi.GetValue("E_BILLINGDOC").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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
    Public Sub ZSDDIRECTCONTRACTPIPE()
        Dim cmd As New OracleCommand
        ' This call is required by the designer.
        ' Add any initialization after the InitializeComponent() call.

        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            'Me.b_save.Focus()
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a customer")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
        ElseIf Me.tb_orderno.Text = "" Then
            MsgBox(" SO # is compulsory")
            Me.tb_orderno.Focus()
        ElseIf Me.tb_dsno.Text = "" Then
            MsgBox(" Delivery Note # is compulsory")
            Me.tb_dsno.Focus()
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

                Dim pgibi As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_DIRECT_CONTRACT_DPP")
                Dim dcust As IRfcStructure = pgibi.GetStructure("CUST_FIELDS") 'CUST_FIELDS 
                dcust.SetValue("ZZTICKET", Me.tb_ticketno.Text) ' done
                dcust.SetValue("ZZVEHI", Me.Tb_vehicleno.Text) 'done
                'dcust.SetValue("ZZVNAME", Me.tb_vehicleno.Text) 'done
                dcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dcust.SetValue("ZZDATOUT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                dcust.SetValue("ZZTIMIN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                dcust.SetValue("ZZTIMOUT", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
                dcust.SetValue("ZZINDS", glbvar.scaletype) 'done
                'dcust.SetValue("ZZCNTNO", tb_container.Text) 'done



                pgibi.SetValue("I_DELIVERY", tb_dsno.Text)
                'dcn1.SetValue("I_DELIVERY", )

                pgibi.SetValue("I_SALESORDER", tb_orderno.Text)

                pgibi.SetValue("I_UNAME", glbvar.userid)
                pgibi.SetValue("REMARKS_1", Me.tb_comments.Text)

                'Dim dpqty As IRfcStructure = pgibil.GetStructure("I_PICKQUANTITY")
                'Dim pqty As Decimal = Convert.ToDecimal(tb_QTY.Text)
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

                Try
                    'Dim damulti As New OracleDataAdapter(cmd)
                    'damulti.TableMappings.Add("Table", "mlt")
                    'Dim dsmlti As New DataSet
                    'damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(Me.tb_ticketno.Text)
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


                        Dim pqtystr As IRfcStructure = pqty.Metadata.LineType.CreateStructure
                        Dim rqty As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("ACTQTY").ToString()) / 1000, 3)
                        'Dim rrqt As Decimal = Math.Round(Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("NETQTY").ToString()) / 1000, 3)
                        pqtystr.SetValue("ITM_NUMBER", itm)
                        pqtystr.SetValue("PICK_QTY", rqty)
                        pqtystr.SetValue("PICK_UOM", "TO")
                        pqtystr.SetValue("MATERIAL", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                        If tb_werks.Text = "" Then
                            pqtystr.SetValue("PLANT", glbvar.divcd)
                        Else
                            pqtystr.SetValue("PLANT", Me.tb_werks.Text)
                        End If
                        pqty.Append(pqtystr)

                        'hardcoded to 10 if single item else in the multi item start with 10 and increase by 10.


                        Dim itcuststr As IRfcStructure = itcust.Metadata.LineType.CreateStructure





                        itcuststr.SetValue("ZZCCIC", Me.Tb_ccic.Text)
                        'itcuststr.SetValue("ZZCNTNO", Me.tb_container.Text) 'commented since not found in FM
                        itcuststr.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        itcuststr.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        itcuststr.SetValue("ZZPIPE", dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString())
                        itcuststr.SetValue("ZZOM", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()))
                        itcuststr.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString()))
                        itcuststr.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()))
                        itcuststr.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        itcuststr.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                        'itcuststr.SetValue("ZZPACKD", CDec(tb_packded.Text))
                        itcuststr.SetValue("ZZUOMOD", "IN") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        itcuststr.SetValue("ZZUOMT", "MM") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        itcuststr.SetValue("ZZUOML", "M") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        itcuststr.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))

                        itcust.Append(itcuststr)

                        Dim sremarksd As IRfcStructure = sremarks.Metadata.LineType.CreateStructure
                        sremarksd.SetValue("TDLINE", Me.tb_comments.Text)
                        sremarks.Append(sremarksd)


                    Next



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
                    'Me.tb_sapinvno.Text = pgibi.GetValue("E_BILLINGDOC").ToString
                    '& vbCrLf & "Delivery Note # " & pgibi.GetValue("E_DELIVERY").ToString _
                    '& vbCrLf & "Invoice # " & pgibi.GetValue("E_INVOICE").ToString _

                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    Me.tb_sapinvno.Text = pgibi.GetValue("E_BILLINGDOC").ToString
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
                    cmd.CommandType = CommandType.StoredProcedure
                    Try
                        cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value 'pgibi.GetValue("SALESDOCUMENT").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value 'pgibi.GetValue("E_DELIVERY").ToString
                        cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = pgibi.GetValue("E_BILLINGDOC").ToString
                        cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CDec(Me.tb_ticketno.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & " From Updating")
                    End Try
                    Dim endtime = DateTime.Now.ToString()

                    freeze()

                End If
            Catch ex As Exception
                MsgBox(ex.Message & " From Main ZDCQ")
            End Try

        End If
        'End if

    End Sub
    Public Sub ZSDCWASALESPIPE()


        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            'Me.b_save.Focus()
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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
                Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("ZSD_CWA_SALES")
                Dim ohdrin As IRfcStructure = sodnbil.GetStructure("ORDER_HEADER_IN")
                ohdrin.SetValue("DOC_TYPE", "ZCWA")
                ohdrin.SetValue("SALES_ORG", Me.tb_CUSTTYPE.Text)
                ohdrin.SetValue("DISTR_CHAN", Me.tb_typecode.Text)
                ohdrin.SetValue("DIVISION", Me.tb_typecatg_pt.Text)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
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
                dlcust.SetValue("ZZVEHI", Me.Tb_vehicleno.Text)
                dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZDATOUT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZTIMIN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        itmstru.SetValue("TARGET_QTY", qt)
                        itmstru.SetValue("SALES_UNIT", "KG")
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
                        Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
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
                        Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
                        ocinstru.SetValue("COND_VALUE", cval)
                        ocinstru.SetValue("CURRENCY", "SAR")
                        ocin.Append(ocinstru)
                        Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                        tdlcfstru.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FWT").ToString()))
                        tdlcfstru.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SWT").ToString()))
                        'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                        tdlcfstru.SetValue("ZZPIPE", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PIPENO").ToString()))
                        tdlcfstru.SetValue("ZZOM", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("OD").ToString()))
                        tdlcfstru.SetValue("ZZTHICK", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("THICK").ToString()))
                        tdlcfstru.SetValue("ZZLEN", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("LENGTH").ToString()))
                        'tdlcfstru.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTIONWT").ToString()))
                        'tdlcfstru.SetValue("ZZPACKD", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString()))
                        tdlcfstru.SetValue("ZZUOMOD", "IN") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZUOMT", "MM") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZUOML", "M") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))

                        tdlcf.Append(tdlcfstru)

                        Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                        orpstru.SetValue("PARTN_ROLE", "SP")
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledcode.Text)
                        'check if the customer is a one time customer then add the test else no need.
                        orpstru.SetValue("NAME", Me.cb_sleddesc.Text)
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
                    'Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
                    'Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICE").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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
    Public Sub ZSDCONSIGNFILLUP02PIPE()

        ' This call is required by the designer.
        Dim cmd As New OracleCommand
        If Me.Tb_intdocno.Text = "" Then
            MsgBox("Please save the record first")
            'Me.b_save.Focus()
        ElseIf Me.tb_sledcode.Text = "" Then
            MsgBox("Select a vendor")
            Me.cb_sleddesc.Focus()
            'ElseIf Me.cb_itemcode.Text = "" Then
            '    MsgBox("Select an itemcode")
            '    Me.cb_itemcode.Focus()
            'ElseIf Me.tb_FIRSTQTY.Text = "" Then
            '    MsgBox(" First Qty cannot be blank")
            '    Me.b_newveh.Focus()
            'ElseIf Me.tb_SECONDQTY.Text = "" Then
            '    MsgBox(" Second Qty cannot be blank")
            '    Me.b_edit.Focus()
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
                ohdrin.SetValue("SALES_ORG", Me.tb_CUSTTYPE.Text)
                ohdrin.SetValue("DISTR_CHAN", Me.tb_typecode.Text)
                ohdrin.SetValue("DIVISION", Me.tb_typecatg_pt.Text)
                ohdrin.SetValue("PURCH_NO_C", Me.Tb_intdocno.Text)
                ohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
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
                dlcust.SetValue("ZZVEHI", Me.Tb_vehicleno.Text)
                dlcust.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZDATOUT", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
                dlcust.SetValue("ZZTIMIN", CDate(Me.tb_timein.Text).Hour.ToString("D2") & CDate(Me.tb_timein.Text).Minute.ToString("D2") & CDate(Me.tb_timein.Text).Second.ToString("D2"))
                dlcust.SetValue("ZZTIMOUT", CDate(Me.tb_timeout.Text).Hour.ToString("D2") & CDate(Me.tb_timeout.Text).Minute.ToString("D2") & CDate(Me.tb_timeout.Text).Second.ToString("D2"))
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

                Try
                    Dim damulti As New OracleDataAdapter(cmd)
                    damulti.TableMappings.Add("Table", "mlt")
                    Dim dsmlti As New DataSet
                    damulti.Fill(dsmlti)
                    'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "curspkg_join.get_pipe"
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
                        Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
                        itmstru.SetValue("TARGET_QTY", qt)
                        itmstru.SetValue("SALES_UNIT", "KG")
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
                        Dim rqty As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("QTY").ToString())
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
                        Dim cval As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("RATE").ToString())
                        ocinstru.SetValue("COND_VALUE", cval)
                        ocinstru.SetValue("CURRENCY", "SAR")
                        ocin.Append(ocinstru)
                        Dim tdlcfstru As IRfcStructure = tdlcf.Metadata.LineType.CreateStructure
                        tdlcfstru.SetValue("ZZFWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("FIRSTQTY").ToString()))
                        tdlcfstru.SetValue("ZZSWGT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        'tdlcfstru.SetValue("ZZDATIN", CDate(Me.tb_DATEIN.Text).Year & CDate(Me.tb_DATEIN.Text).Month.ToString("D2") & CDate(Me.tb_DATEIN.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZDATOUT", CDate(Me.tb_DATEOUT.Text).Year & CDate(Me.tb_DATEOUT.Text).Month.ToString("D2") & CDate(Me.tb_DATEOUT.Text).Day.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMIN", CDate(Me.tb_TIMEIN.Text).Hour.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Minute.ToString("D2") & CDate(Me.tb_TIMEIN.Text).Second.ToString("D2"))
                        'tdlcfstru.SetValue("ZZTIMOUT", CDate(Me.tb_TIMOUT.Text).Hour.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Minute.ToString("D2") & CDate(Me.tb_TIMOUT.Text).Second.ToString("D2"))
                        'tdlcfstru.SetValue("ZDECT", CDec(Me.tb_DEDUCTIONWT.Text))
                        tdlcfstru.SetValue("ZZPIPE", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZOM", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZTHICK", 0) ' CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZLEN", 0) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZCTKT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZDECT", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("DEDUCTION").ToString()))
                        tdlcfstru.SetValue("ZZPACKD", CDec(dsmltitm.Tables("mltitm").Rows(a).Item("PACKDED").ToString()))
                        tdlcfstru.SetValue("ZZUOMOD", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZUOMT", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZUOML", "") 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcfstru.SetValue("ZZNOPIPE", Me.tb_numberofpcs.Text) 'CDec(dsmltitm.Tables("mltitm").Rows(a).Item("SECONDQTY").ToString()))
                        tdlcf.Append(tdlcfstru)

                        Dim orpstru As IRfcStructure = orp.Metadata.LineType.CreateStructure
                        orpstru.SetValue("PARTN_ROLE", "AG")
                        orpstru.SetValue("PARTN_NUMB", Me.tb_sledcode.Text)
                        'check if the customer is a one time customer then add the test else no need.
                        orpstru.SetValue("NAME", Me.cb_sleddesc.Text)
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
                    'Me.tb_sapord.Text = sodnbil.GetValue("SALESDOCUMENT").ToString
                    'Me.tb_sapdocno.Text = sodnbil.GetValue("E_DELIVERY").ToString
                    'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                    'Write an update procedure for updating the documnt numbers in STWBMIBDS
                    cmd.Parameters.Clear()
                    cmd.Connection = conn
                    cmd.Parameters.Clear()
                    cmd.CommandText = "gen_iwb_dsd.gen_wbms_sap_pipe"
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

    Private Sub b_clear_Click(sender As Object, e As EventArgs) Handles b_clear.Click
        clr_scr()
    End Sub
    Private Sub clr_scr()
        Try
            Me.Tb_asno.Text = "0"
            Me.tb_orderno.Text = "0"
            Me.tb_IBDSNO.Text = "0"
            Me.tb_orderno.Text = "0"
            Me.tb_dsno.Text = "0"
            Me.Tb_transp.Text = 0
            Me.Tb_labourcharges.Text = 0
            Me.Tb_eqpchrgs.Text = 0
            Me.Tb_penalty.Text = 0
            Me.cb_sleddesc.Text = ""
            Me.tb_sledcode.Text = ""
            Me.tb_ticketno.Text = 0
            Me.Tb_vehicleno.Text = ""
            Me.tb_buyer.Text = ""
            Me.tb_DRIVERNAM.Text = ""
            Me.cb_dcode.Text = ""
            Me.tb_DATEIN.Text = ""
            Me.tb_dateout.Text = ""
            Me.tb_timein.Text = ""
            Me.tb_timeout.Text = ""
            Me.Tb_ccic.Text = ""
            Me.tb_comments.Text = ""
            Me.Tb_intdocno.Text = ""
            Me.cb_sap_docu_type.Text = ""
            Me.tb_sap_doc.Text = ""
            Me.tb_oth_ven_cust.Text = ""
            Me.tb_inout_type.Text = ""
            Me.tb_inout_desc.Text = ""
            Me.b_purchase.Visible = True
            Me.tb_totqty.Text = 0
            Me.tb_actqty.Text = 0
            Me.tb_netqty.Text = 0
            Me.tb_sapord.Text = ""
            Me.tb_sapdocno.Text = ""
            Me.tb_sapinvno.Text = ""
            Me.tb_CUSTTYPE.Text = ""
            Me.tb_typecode.Text = ""
            Me.tb_typecatg_pt.Text = ""
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView2.Rows.Clear()
            Me.tb_mixpo.Text = ""
            Me.tb_totqty.Text = 0
            Me.tb_actqty.Text = 0
            Me.tb_totval.Text = 0
            Me.p_mix.Visible = True
            glbvar.mix = False
            MIX.MIXGRID.Rows.Clear()
            Me.b_purchase.Enabled = False
            Me.b_deliver.Enabled = False
            'Me.DataGridView1.Columns("OD").ReadOnly = False
            'Me.DataGridView1.Columns("THICK").ReadOnly = False
            'Me.DataGridView1.Columns("LENGTH").ReadOnly = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub freeze()
        Try
            MIX.MIXGRID.Enabled = False
            Me.b_purchase.Enabled = False
            Me.b_deliver.Enabled = False
            Me.tb_ok.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub unfreeze()
        Try
            MIX.MIXGRID.Enabled = True
            Me.b_purchase.Enabled = True
            Me.b_deliver.Enabled = True
            Me.tb_ok.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Try
            'If Me.DataGridView1.CurrentRow.Cells("Deduction").Selected Then
            '    Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").EditedFormattedValue
            '    Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
            'ElseIf Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
            '    Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
            'End If
            If Me.DataGridView1.CurrentRow.Cells("OD").Selected Then

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            If Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then

                'conn = New OracleConnection(constr)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If

                'sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                '        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                '        & " WHERE z1.userauth_no = z2.userauth_no" _
                '        & " AND z1.username = z2.userid" _
                '        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                '        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells("Itemcode").Value & "'"

                'Dim dpct = New OracleDataAdapter(sql, conn)
                'Dim dpc As New DataSet
                'dpc.Clear()
                'dpct.Fill(dpc)
                'Dim user_tol_value As Decimal
                'Dim user_tot_allowed As Decimal
                'Dim pct = dpc.Tables(0).Rows(0).Item("pct")
                'Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                'Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                'user_tol_value = pct * plist
                'user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                'If pct <> 0 Then
                '    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + user_tol_value
                'ElseIf amt <> 0 Then
                '    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value) + amt
                'End If
                'If Me.tb_inout_type.Text = "I" Then
                '    If Me.tb_sap_doc.Text = "QD" Or Me.tb_sap_doc.Text = "QMX" Then
                '        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_tot_allowed Then

                '            MsgBox("Price not matching as the latest Pricelist")
                '            Me.tb_ok.Enabled = False
                '            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True

                '        Else
                '            Me.tb_ok.Enabled = True

                '            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
                '        End If

                '    End If

                'End If
                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                        & " WHERE z1.userauth_no = z2.userauth_no" _
                        & " AND z1.username = z2.userid" _
                        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells(1).Value & "'" _
                        & " and Z1.INTAUTHNO =  (SELECT   MAX (d.INTAUTHNO) " _
                        & " FROM   ZUSER_AUTH_H d " _
                        & " where username = " & "'" & glbvar.userid & "'" & ")"
                'sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                '    & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                '    & " WHERE z1.userauth_no = z2.userauth_no" _
                '    & " AND z1.username = z2.userid" _
                '    & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                '    & " AND z2.matnr = " & "'" & tb_itemdesc.Text & "'"

                Dim dpct = New OracleDataAdapter(sql, conn)
                Dim dpc As New DataSet
                dpc.Clear()
                dpct.Fill(dpc)
                Dim user_tol_value As Decimal
                Dim user_tot_allowed As Decimal
                Dim user_sales_value As Decimal
                Dim user_sales_allowed As Decimal
                Dim pct = dpc.Tables(0).Rows(0).Item("pct")

                Dim amt = dpc.Tables(0).Rows(0).Item("amount")
                If Me.tb_inout_type.Text = "I" Then
                    If Me.tb_sap_doc.Text = "QMX" Then
                        Dim count As Integer = 0
                        For ai = 0 To DataGridView1.Rows.Count - 1
                            'Dim plist = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value)
                            Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                            user_tol_value = pct * plist
                            user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value)
                            If pct <> 0 Then
                                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + user_tol_value
                            ElseIf amt <> 0 Then
                                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + amt/1000
                            End If

                            If Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value) > user_tot_allowed Then
                                count = count + 1
                                Dim a = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value)
                            End If
                            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
                        Next
                        If count > 0 Then
                            MsgBox("Price not matching as the latest Pricelist")
                            For ai = 0 To DataGridView1.Rows.Count - 1
                                Me.DataGridView1.Rows(ai).Cells("rate").Value = 0
                            Next
                            Me.tb_ok.Visible = False
                        Else
                            Me.tb_ok.Visible = True
                            count = 0
                            'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                        End If

                    Else
                        'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                    End If
                ElseIf Me.tb_inout_type.Text = "O" Then
                    If Me.tb_sap_doc.Text = "ZCWA" Then
                        Dim count As Integer = 0
                        For ai = 0 To DataGridView1.Rows.Count - 1
                            'Dim plist = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value)
                            Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("price").Value)
                            user_tol_value = pct * plist
                            user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value)
                            user_sales_value = 2 * plist
                            user_sales_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + user_sales_value
                            If pct <> 0 Then
                                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + user_tol_value
                            ElseIf amt <> 0 Then
                                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("price").Value) + amt / 1000
                            End If

                            If Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value) > user_sales_allowed Then
                                count = count + 1
                                Dim a = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells("rate").Value)
                            End If
                            Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("ACTQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
                        Next
                        If count > 0 Then
                            MsgBox("Price not matching as the latest Pricelist")
                            For ai = 0 To DataGridView1.Rows.Count - 1
                                Me.DataGridView1.Rows(ai).Cells("rate").Value = 0
                            Next
                            Me.tb_ok.Visible = False
                        Else
                            Me.tb_ok.Visible = True
                            count = 0
                            'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                        End If

                    Else
                        'tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
                    End If
                End If
            End If
            If Me.DataGridView1.CurrentRow.Cells("PIPENO").Selected Then
                If Me.tb_inout_type.Text = "I" Then


                    If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                        RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
                    End If
                    Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

                    ' create connection to the RFC repository
                    Dim repos As RfcRepository = dest.Repository

                    Dim pipedet As IRfcFunction = dest.Repository.CreateFunction("Z_FM_PIPNO_DUP")
                    Dim pipeimp As IRfcStructure = pipedet.GetStructure("IPIPEIMP")
                    'Dim pipeimps As IRfcStructure = pipeimp.Metadata.LineType.CreateStructure
                    pipeimp.SetValue("IPLANT", glbvar.divcd)
                    'pipeimps.SetValue("IMATNR", Me.DataGridView1.CurrentRow.Cells("Itemcode").Value)
                    pipeimp.SetValue("IPIPENO", Me.DataGridView1.CurrentRow.Cells("PIPENO").Value)
                    Dim a = Me.DataGridView1.CurrentRow.Cells("PIPENO").Value
                    Dim retpipe As IRfcTable = pipedet.GetTable("PIPERET_STR")
                    Dim st As TimeSpan = Now.TimeOfDay
                    pipedet.Invoke(dest)
                    Dim ed As TimeSpan = Now.TimeOfDay
                    'MsgBox("time taken for Pipe FM " & Convert.ToString((ed - st)))
                    If retpipe.RowCount > 0 Then
                        MsgBox("Pipe Number Exists")
                        Me.DataGridView1.CurrentRow.Cells("PIPENO").Value = "0"
                        'For j = 0 To retpipe.RowCount - 1
                        '    Me.DataGridView1.CurrentRow.Cells("OD").Value = retpipe(j).Item("PIPE_OD").GetValue
                        '    Me.DataGridView1.CurrentRow.Cells("THICK").Value = retpipe(j).Item("PIPE_THK").GetValue
                        '    Me.DataGridView1.CurrentRow.Cells("LENGTH").Value = retpipe(j).Item("PIPE_LEN").GetValue
                        '    Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = retpipe(j).Item("PIPE_QTY").GetValue
                        'Next
                        'Else
                        '    MsgBox(pipedet.GetValue("RETURNMSG").ToString)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub cb_sap_docu_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_sap_docu_type.SelectedIndexChanged
        Try
            If Me.cb_sap_docu_type.SelectedIndex <> -1 Then
                Me.tb_sap_doc.Text = Me.cb_sap_docu_type.SelectedValue.ToString
                Dim foundrow() As DataRow
                Dim expression As String = "DOCCODE = '" & Me.cb_sap_docu_type.Text & "'" & ""
                foundrow = dsdoc.Tables("doc").Select(expression)
                

            End If
            If tb_sap_doc.Text = "QPX" Then
                Me.Tb_asno.Visible = True
                Me.Label34.Visible = True
            End If



            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'conn.Close()
        End Try
    End Sub

    Private Sub b_generate_Click(sender As Object, e As EventArgs) Handles b_generate.Click
        Try
            Dim itmx = 0
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            'tmode = 2
            DataGridView1.Rows.Clear()
            Dim cns As Integer
            sql = " select count(itemcode) cnt from STWBMPIPE WHERE ticketno = " & Me.tb_rticketno.Text
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            Dim sqlc As String
            sqlc = "select distinct vbelns from stwbmpipe where ticketno =" & Me.tb_gtktno.Text
            Dim dca As OracleDataAdapter
            dca = New OracleDataAdapter(sqlc, conn)
            Dim dcs As New DataSet
            dcs.Clear()
            dca.Fill(dcs)
            'If dcs.Tables(0).Rows.Count > 0 Then
            '    MsgBox("Ticket already utilized ")
            'Else
            If Me.tb_inout_type.Text = "I" Then
                sql = " select * from STWBMIBDS WHERE ticketno = " & Me.tb_gtktno.Text _
                      & "  and inouttype =  'I'" _
                      & " AND VBELNS is not null AND VBELND is not null and bsart = 'QX'" _
                      & "  order by slno desc "
            ElseIf Me.tb_inout_type.Text = "O" Then
                sql = " select * from STWBMIBDS WHERE ticketno = " & Me.tb_gtktno.Text _
                      & "  and inouttype =  'O'" _
                      & "  order by slno desc "
            ElseIf Me.tb_inout_type.Text = "S" Then
                sql = " select * from STWBMIBDS WHERE ticketno = " & Me.tb_gtktno.Text _
                      & "  and inouttype =  'S' and vbelni is null" _
                      & "  order by slno desc "
            ElseIf Me.tb_inout_type.Text = "T" Then
                sql = " select * from STWBMIBDS WHERE ticketno = " & Me.tb_gtktno.Text _
                      & "  and inouttype IN  ('O','S') and vbelni is null" _
                      & "  order by slno desc "
                'Dim sqlu As String
                'sqlu = "update stwbmibds set vbelns =" & Me.tb_ticketno.Text & " where ticketno = " & Me.tb_gtktno.Text
                
            End If
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            If dp.Tables(0).Rows(0).Item("NUMBEROFPCS") > 0 Then
                Me.tb_nopcs.Text = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
            End If
            If CInt(Me.tb_nopcs.Text < 1) Then
                MsgBox("Enter Number of Pieces ")
            Else
                For i = 0 To CInt(Me.tb_nopcs.Text) - 1
                    DataGridView1.Rows.Insert(rowIndex:=i)
                    Me.DataGridView1.Rows(i).Cells(0).Value = itmx + 10
                    itmx = itmx + 10
                    rowchk = itmx
                Next
                'Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("TICKETNO")
                Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("INTDOCNO")
                Me.Tb_vehicleno.Text = dp.Tables(0).Rows(0).Item("VEHICLENO")
                Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
                Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
                'Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
                Me.tb_DATEIN.Text = dp.Tables(0).Rows(0).Item("DATEIN")
                Me.tb_timein.Text = dp.Tables(0).Rows(0).Item("TIMEIN")
                Me.tb_dateout.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
                Me.tb_timeout.Text = dp.Tables(0).Rows(0).Item("TIMOUT")
                If Not IsDBNull(dp.Tables(0).Rows(0).Item("BSART")) Then
                    'Me.cb_sap_docu_type.Text = dp.Tables(0).Rows(0).Item("BSART")
                    'Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
                    Me.cb_sap_docu_type.Text = "Against Mix Material"
                    Me.tb_sap_doc.Text = "QMX"
                    Me.tb_mixpo.Text = dp.Tables(0).Rows(0).Item("VBELNS")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(0).Item("AUART")) Then
                    Me.cb_sap_docu_type.Text = dp.Tables(0).Rows(0).Item("AUART")
                    Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("AUART")
                End If
                Me.tb_netqty.Text = dp.Tables(0).Rows(0).Item("QTY")
                Me.tb_numberofpcs.Text = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")

                'For i = 0 To cns - 1
                '    DataGridView1.Rows.Insert(rowIndex:=0)
                '    Me.DataGridView1.Rows(0).Cells(0).Value = dp.Tables(0).Rows(i).Item("slno")
                '    Me.DataGridView1.Rows(0).Cells(1).Value = dp.Tables(0).Rows(i).Item("Itemcode")
                '    Me.DataGridView1.Rows(0).Cells(2).Value = dp.Tables(0).Rows(i).Item("Itemdesc")
                '    Me.DataGridView1.Rows(0).Cells("qty").Value = dp.Tables(0).Rows(i).Item("qty")
                '    Me.DataGridView1.Rows(0).Cells("deduction").Value = dp.Tables(0).Rows(i).Item("DEDUCTIONWT")
                '    Me.DataGridView1.Rows(0).Cells("price").Value = dp.Tables(0).Rows(i).Item("priceton")
                '    Me.DataGridView1.Rows(0).Cells("rate").Value = dp.Tables(0).Rows(i).Item("rate")
                '    Me.DataGridView1.Rows(0).Cells("od").Value = dp.Tables(0).Rows(i).Item("od")
                '    Me.DataGridView1.Rows(0).Cells("thick").Value = dp.Tables(0).Rows(i).Item("thick")
                '    Me.DataGridView1.Rows(0).Cells("length").Value = dp.Tables(0).Rows(i).Item("length")
                '    Dim a = dp.Tables(0).Rows(i).Item("pipeno")
                '    If Not IsDBNull(dp.Tables(0).Rows(i).Item("pipeno")) Then
                '        Me.DataGridView1.Rows(0).Cells("pipeno").Value = dp.Tables(0).Rows(i).Item("pipeno")
                '    End If
                '    Me.DataGridView1.Rows(0).Cells("fwt").Value = dp.Tables(0).Rows(i).Item("fwt")
                '    Me.DataGridView1.Rows(0).Cells("swt").Value = dp.Tables(0).Rows(i).Item("swt")
                '    Me.DataGridView1.Rows(0).Cells("docno").Value = dp.Tables(0).Rows(i).Item("intdocno")
                '    Me.DataGridView1.Rows(0).Cells("tktno").Value = dp.Tables(0).Rows(i).Item("ticketno")
                '    Me.DataGridView1.Rows(0).Cells("inout").Value = dp.Tables(0).Rows(i).Item("INOUTTYPE")
                '    Me.DataGridView1.Rows(0).Cells("vcode").Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                '    Me.DataGridView1.Rows(0).Cells("vname").Value = dp.Tables(0).Rows(i).Item("SLEDDESC")
                '    Me.DataGridView1.Rows(0).Cells("sapdoc").Value = dp.Tables(0).Rows(i).Item("BSART")
                '    Me.DataGridView1.Rows(0).Cells("datein").Value = dp.Tables(0).Rows(i).Item("DATEIN")
                '    Me.DataGridView1.Rows(0).Cells("DATEOUT").Value = dp.Tables(0).Rows(0).Item("DATEOUT")
                '    Me.DataGridView1.Rows(0).Cells("TIMEIN").Value = dp.Tables(0).Rows(0).Item("TIMEIN")
                '    Me.DataGridView1.Rows(0).Cells("TIMOUT").Value = dp.Tables(0).Rows(0).Item("TIMOUT")
                '    Me.DataGridView1.Rows(0).Cells("NUMBEROFPCS").Value = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
                '    Me.DataGridView1.Rows(0).Cells("LABOUR_CHARGE").Value = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                '    Me.DataGridView1.Rows(0).Cells("PENALTY").Value = dp.Tables(0).Rows(0).Item("PENALTY")
                '    Me.DataGridView1.Rows(0).Cells("MACHINE_CHARGE").Value = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                '    Me.DataGridView1.Rows(0).Cells("TRANS_CHARGE").Value = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")
                '    Me.DataGridView1.Rows(0).Cells("CONSNO").Value = dp.Tables(0).Rows(0).Item("CONSNO")
                '    Me.DataGridView1.Rows(0).Cells("SORDERNO").Value = dp.Tables(0).Rows(0).Item("SORDERNO")
                '    Me.DataGridView1.Rows(0).Cells("DELIVERYNO").Value = dp.Tables(0).Rows(0).Item("DELIVERYNO")
                '    Me.DataGridView1.Rows(0).Cells("PONO").Value = dp.Tables(0).Rows(0).Item("PONO")
                '    Me.DataGridView1.Rows(0).Cells("AGMIXNO").Value = dp.Tables(0).Rows(0).Item("AGMIXNO")
                '    Me.DataGridView1.Rows(0).Cells("CCIC").Value = dp.Tables(0).Rows(0).Item("CCIC")
                '    Me.DataGridView1.Rows(0).Cells("VEHICLENO").Value = dp.Tables(0).Rows(0).Item("VEHICLENO")
                '    Me.DataGridView1.Rows(0).Cells("OTHVENCUST").Value = dp.Tables(0).Rows(0).Item("OTHVENCUST")
                '    Me.DataGridView1.Rows(0).Cells("REMARKS").Value = dp.Tables(0).Rows(0).Item("REMARKS")
                '    Me.DataGridView1.Rows(0).Cells("DRIVERNAM").Value = dp.Tables(0).Rows(0).Item("DRIVERNAM")
                '    Me.DataGridView1.Rows(0).Cells("DCODE").Value = dp.Tables(0).Rows(0).Item("DCODE")
                '    Me.DataGridView1.Rows(0).Cells("netqty").Value = dp.Tables(0).Rows(i).Item("netqty")
                '    Me.DataGridView1.Rows(0).Cells("value").Value = dp.Tables(0).Rows(i).Item("value")
                '    'Me.DataGridView1.Rows(0).Cells("BUYER").Value = dp.Tables(0).Rows(0).Item("BUYER")
                'Next
                'Me.tb_ticketno.Text = dp.Tables(0).Rows(0).Item("ticketno")
                'Me.Tb_intdocno.Text = dp.Tables(0).Rows(0).Item("intdocno")
                'Me.tb_inout_type.Text = dp.Tables(0).Rows(0).Item("INOUTTYPE")
                'Me.tb_sledcode.Text = dp.Tables(0).Rows(0).Item("SLEDCODE")
                'Me.cb_sleddesc.Text = dp.Tables(0).Rows(0).Item("SLEDDESC")
                'Me.tb_sap_doc.Text = dp.Tables(0).Rows(0).Item("BSART")
                'Me.tb_DATEIN.Text = dp.Tables(0).Rows(0).Item("DATEIN")
                'Me.tb_dateout.Text = dp.Tables(0).Rows(0).Item("DATEOUT")
                'Me.tb_timein.Text = dp.Tables(0).Rows(0).Item("TIMEIN")
                'If dp.Tables(0).Rows(0).Item("TIMOUT").ToString <> "" Then
                '    Me.tb_timeout.Text = dp.Tables(0).Rows(0).Item("TIMOUT")
                'End If
                'Me.tb_numberofpcs.Text = dp.Tables(0).Rows(0).Item("NUMBEROFPCS")
                'Me.Tb_labourcharges.Text = dp.Tables(0).Rows(0).Item("LABOUR_CHARGE")
                'Me.Tb_penalty.Text = dp.Tables(0).Rows(0).Item("PENALTY")
                'Me.Tb_eqpchrgs.Text = dp.Tables(0).Rows(0).Item("MACHINE_CHARGE")
                'Me.Tb_transp.Text = dp.Tables(0).Rows(0).Item("TRANS_CHARGE")
                'Me.Tb_cons_sen_branch.Text = dp.Tables(0).Rows(0).Item("CONSNO")
                'Me.tb_orderno.Text = dp.Tables(0).Rows(0).Item("SORDERNO")
                'Me.tb_dsno.Text = dp.Tables(0).Rows(0).Item("DELIVERYNO")
                'Me.Tb_asno.Text = dp.Tables(0).Rows(0).Item("PONO")
                'Me.tb_IBDSNO.Text = dp.Tables(0).Rows(0).Item("AGMIXNO")
                'Me.Tb_ccic.Text = dp.Tables(0).Rows(0).Item("CCIC").ToString
                'Me.Tb_vehicleno.Text = dp.Tables(0).Rows(0).Item("VEHICLENO").ToString
                'Me.tb_oth_ven_cust.Text = dp.Tables(0).Rows(0).Item("OTHVENCUST").ToString
                'Me.tb_comments.Text = dp.Tables(0).Rows(0).Item("REMARKS").ToString
                'Me.tb_DRIVERNAM.Text = dp.Tables(0).Rows(0).Item("DRIVERNAM").ToString
                'Me.cb_dcode.Text = dp.Tables(0).Rows(0).Item("DCODE").ToString
                'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNS"))) Then
                '    Me.tb_sapord.Text = dp.Tables(0).Rows(0).Item("VBELNS")
                'End If
                'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELND"))) Then
                '    Me.tb_sapdocno.Text = dp.Tables(0).Rows(0).Item("VBELND")
                'End If
                'If Not (IsDBNull(dp.Tables(0).Rows(0).Item("VBELNI"))) Then
                '    Me.tb_sapinvno.Text = dp.Tables(0).Rows(0).Item("VBELNI")
                'End If
                'Me.tb_buyer.Text = dp.Tables(0).Rows(0).Item("BUYER").ToString
                If Me.tb_sap_doc.Text = "QN" Then
                    Me.Tb_asno.Visible = True
                    Me.Label34.Visible = True
                    Me.Label34.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QI" Then
                    Me.Tb_cons_sen_branch.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIM" Then
                    Me.Tb_cons_sen_branch.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QIX" Then
                    Me.Tb_cons_sen_branch.Visible = True
                    Me.tb_IBDSNO.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QMX" Then
                    'Me.tb_IBDSNO.Visible = True
                ElseIf Me.tb_sap_doc.Text = "QPX" Then
                    Me.Tb_asno.Visible = True
                    Me.Label34.Visible = True
                ElseIf Me.tb_sap_doc.Text = "ZDCQ" Then
                    Me.tb_orderno.Visible = True
                    Me.tb_dsno.Visible = True
                ElseIf Me.tb_sap_doc.Text = "ZTRE" Then
                    Me.tb_orderno.Visible = True
                Else
                    'Me.Tb_asno.Visible = False
                    Me.Tb_cons_sen_branch.Visible = False
                    Me.tb_IBDSNO.Visible = False
                    Me.tb_orderno.Visible = False
                    Me.tb_dsno.Visible = False
                    Me.Label25.Visible = False
                    Me.Label26.Visible = False
                    Me.Label27.Visible = False
                    Me.Label34.Visible = False
                    Me.Label35.Visible = False
                End If
                Me.p_mix.Visible = False
                Dim cmdu As New OracleCommand
                Dim cmdco As New OracleCommand
                cmdu.Connection = conn
                cmdco.Connection = conn
                cmdu.Parameters.Clear()
                cmdco.Parameters.Clear()
                cmdu.CommandText = "update stwbmibds set vbelni =" & Me.tb_ticketno.Text & " where ticketno = " & Me.tb_gtktno.Text
                cmdco.CommandText = "commit"
                cmdu.CommandType = CommandType.Text
                cmdco.CommandType = CommandType.Text
                cmdu.ExecuteNonQuery()
                cmdco.ExecuteNonQuery()
            End If
            'End If
            'Me.tb_save.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    
    Private Sub b_genwt_Click(sender As Object, e As EventArgs) Handles b_genwt.Click
        Me.tb_quotient.Text = 0
        Me.Tb_prolot.Text = 0

        Try
            For i = 0 To DataGridView1.RowCount - 1
                Dim a = (Me.DataGridView1.Rows(i).Cells("OD").Value * 25.4)
                Dim b = (Me.DataGridView1.Rows(i).Cells("THICK").Value)
                Dim c = (Me.DataGridView1.Rows(i).Cells("THICK").Value) * (0.0246615)
                Me.DataGridView1.Rows(i).Cells("NETQTY").Value = Math.Round((((Me.DataGridView1.Rows(i).Cells("OD").Value) - (Me.DataGridView1.Rows(i).Cells("THICK").Value)) * (Me.DataGridView1.Rows(i).Cells("THICK").Value) * (0.0246615)) * (Me.DataGridView1.Rows(i).Cells("LENGTH").Value), 2)

            Next
            'For i = 0 To DataGridView1.RowCount - 1
            '    Me.DataGridView1.Rows(i).Cells("WT").Value = Me.DataGridView1.Rows(i).Cells("OD").EditedFormattedValue * Me.DataGridView1.Rows(i).Cells("THICK").EditedFormattedValue * Me.DataGridView1.Rows(i).Cells("LENGTH").EditedFormattedValue
            '    Me.Tb_prolot.Text = Me.Tb_prolot.Text + Me.DataGridView1.Rows(i).Cells("WT").Value
            'Next
            'Me.tb_quotient.Text = Math.Round(CDec(Me.tb_netqty.Text) / CDec(Me.Tb_prolot.Text), 3)
            'For i = 0 To DataGridView1.RowCount - 1
            '    Me.DataGridView1.Rows(i).Cells("NETQTY").Value = Math.Round(Me.DataGridView1.Rows(i).Cells("WT").EditedFormattedValue * Me.tb_quotient.Text, 2)
            '    DataGridView1_RowEnter()
            'Next
            'Me.tb_quotient.Text = 0
            'Me.Tb_prolot.Text = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_mixmat_Click(sender As Object, e As EventArgs) Handles b_mixmat.Click
        Try
            glbvar.vntwt = CInt(Me.tb_actqty.Text)
            glbvar.multdocno = Me.Tb_intdocno.Text
            glbvar.inout = Me.tb_sap_doc.Text
            glbvar.multkt = Me.tb_ticketno.Text
            glbvar.sapdocmulti = Me.tb_sap_doc.Text
            Dim mix As New MIX
            mix.Show()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox(ex.InnerException)
            Console.WriteLine("In Main catch block. Caught: {0}", ex.Message)
            Console.WriteLine("Inner Exception is {0}", ex.InnerException)
        End Try
    End Sub

    
    
  
    
    
    Private Sub b_scon_Click(sender As Object, e As EventArgs) Handles b_scon.Click
        clr_scr()
        unfreeze()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMPIPE WBM WHERE INOUTTYPE = 'S' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If
            cmbloading()
            Me.tb_sap_doc.Text = "SC"
            Me.cb_sap_docu_type.Text = "Scale Only"
            tmode = 1
            tb_inout_type.Text = "S"
            tb_inout_desc.Text = "Scale Only"
            Me.p_mix.Visible = True
            'Me.DataGridView1.Columns("OD").ReadOnly = True
            'Me.DataGridView1.Columns("THICK").ReadOnly = True
            'Me.DataGridView1.Columns("LENGTH").ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub b_stk_Click(sender As Object, e As EventArgs) Handles b_stk.Click
        clr_scr()
        unfreeze()
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "SELECT   NVL(MAX(WBM.TICKETNO),0)+1 TKT" _
                & "  FROM   STWBMPIPE WBM WHERE INOUTTYPE = 'T' "
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
            If cb_sleddesc.Visible = False Then
                cb_sleddesc.Show()
            End If
            If tb_sledcode.Visible = False Then
                tb_sledcode.Show()
            End If
            cmbloading1()
            Me.tb_sap_doc.Text = "TRN"
            Me.cb_sap_docu_type.Text = "Stock Transfer"
            tmode = 1
            tb_inout_type.Text = "T"
            tb_inout_desc.Text = "Stock Transfer"
            Me.p_mix.Visible = True
            'Me.DataGridView1.Columns("OD").ReadOnly = True
            'Me.DataGridView1.Columns("THICK").ReadOnly = True
            'Me.DataGridView1.Columns("LENGTH").ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_trn_Click(sender As Object, e As EventArgs) Handles b_trn.Click
        Try
            Dim cmd As New OracleCommand
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim saprfcdest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim saprfcrepos As RfcRepository = saprfcdest.Repository



            'for Document type ZCOR the tb_dsno is mandatory 
            'Outside Materials the customer ticket # and date to be made mandatory is manatory ZOMO
            'Inter Branch Consignemet Number from SAP to be stored in. This will become the refernce for receiving branch



            Dim sodnbil As IRfcFunction = saprfcdest.Repository.CreateFunction("Z_STOCK_TRF_DPP")
            Dim ohdrin As IRfcStructure = sodnbil.GetStructure("GOODSMVT_HEADER")
            ohdrin.SetValue("PSTNG_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))
            ohdrin.SetValue("DOC_DATE", CDate(Me.tb_dateout.Text).Year & CDate(Me.tb_dateout.Text).Month.ToString("D2") & CDate(Me.tb_dateout.Text).Day.ToString("D2"))

            Dim scltyp As IRfcStructure = sodnbil.GetStructure("GOODSMVT_CODE") 'DLCUST_FIELD 
            scltyp.SetValue("GM_CODE", "04")
            sodnbil.SetValue("ZPRJN", "DPP100")
            sodnbil.SetValue("ZPRJS", "DPP100")
            sodnbil.SetValue("ZREFPO", Tb_asno.Text)



            conn = New OracleConnection(constr)


            'Dim damulti As New OracleDataAdapter(cmd)
            'damulti.TableMappings.Add("Table", "mlt")
            'Dim dsmlti As New DataSet
            'damulti.Fill(dsmlti)
            'Dim coun As String = dsmlti.Tables("mlt").Rows(0).Item("cnt").ToString

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "curspkg_join.get_pipe"
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


                Dim oitmin As IRfcTable = sodnbil.GetTable("GOODSMVT_ITEM")
                Dim itmstru As IRfcStructure = oitmin.Metadata.LineType.CreateStructure

                itmstru.SetValue("MATERIAL", "000000000102900101")
                If tb_werks.Text = "" Then
                    itmstru.SetValue("PLANT", glbvar.divcd)
                    itmstru.SetValue("STGE_LOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                Else
                    itmstru.SetValue("PLANT", Me.tb_werks.Text)
                    itmstru.SetValue("STGE_LOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                End If
                itmstru.SetValue("MOVE_TYPE", "309")
                Dim qt As Decimal = Convert.ToDecimal(dsmltitm.Tables("mltitm").Rows(a).Item("ACTQTY").ToString()) / 1000
                itmstru.SetValue("ENTRY_QNT", Math.Round(qt, 3))
                itmstru.SetValue("ENTRY_UOM", "TO")
                itmstru.SetValue("MOVE_MAT", dsmltitm.Tables("mltitm").Rows(a).Item("ITEMCODE").ToString())
                If tb_werks.Text = "" Then
                    itmstru.SetValue("MOVE_PLANT", glbvar.divcd)
                    itmstru.SetValue("MOVE_STLOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                Else
                    itmstru.SetValue("MOVE_PLANT", Me.tb_werks.Text)
                    itmstru.SetValue("MOVE_STLOC", dsmltitm.Tables("mltitm").Rows(a).Item("LGORT").ToString())
                End If

                itmstru.SetValue("LINE_ID", dsmltitm.Tables("mltitm").Rows(a).Item("SLNO").ToString())

                oitmin.Append(itmstru)


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
                & vbCrLf & "Delivery Note # " & sodnbil.GetValue("PRICE_DOC").ToString
                )
                '& vbCrLf & "Invoice # " & sodnbil.GetValue("E_INVOICE").ToString _
                Me.tb_sapinvno.Text = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                '  Me.tb_sapdocno.Text = sodnbil.GetValue("PRICE_DOC").ToString
                freeze()
                'Me.tb_sapinvno.Text = sodnbil.GetValue("E_INVOICENO").ToString
                'Write an update procedure for updating the documnt numbers in STWBMIBDS
                cmd.Parameters.Clear()
                cmd.Connection = conn
                cmd.Parameters.Clear()
                cmd.CommandText = "gen_iwb_dsd_pr.gen_wbms_sap_U"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("pVBELNS", OracleDbType.Int64)).Value = DBNull.Value
                'cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("pVBELND", OracleDbType.Char)).Value = DBNull.Value
                cmd.Parameters.Add(New OracleParameter("pVBELNI", OracleDbType.Char)).Value = sodnbil.GetValue("MATERIALDOCUMENT").ToString
                cmd.Parameters.Add(New OracleParameter("pTICKETNO", OracleDbType.Char)).Value = CLng(Me.tb_ticketno.Text)
                cmd.ExecuteNonQuery()
                conn.Close()

                Dim endtime = DateTime.Now.ToString()
                'Me.b_crfillup.Visible = True
                'Me.b_crfillup.Enabled = True

            End If

            conn.Close()
            ' End If ' main end if
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            conn.Close()
        End Try
    End Sub
End Class
