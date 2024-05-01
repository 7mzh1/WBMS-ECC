Imports System.Data
'Imports Oracle.DataAccess.Types
Imports System.Text
Imports Oracle.DataAccess.Client


Public Class multi_itm
    Dim conn As New OracleConnection
    Dim daitm As New OracleDataAdapter
    Dim dsitm As New DataSet
    Dim constr, constrd As String
    Dim tot As Integer = 0
    Dim totprice As Integer = 0
    Dim totded As Decimal
    Dim deduction As Decimal
    Dim packdeduction As Decimal
    Dim sql As String
    Dim dpr As OracleDataAdapter
    Dim itmchar As String
    Dim inmode = 1
    
    Private Sub multi_itm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If glbvar.gded <> 0 Then
            Me.DataGridView1.Columns("Ded").ReadOnly = True
            Me.DataGridView1.Columns("packDed").ReadOnly = True
        Else
            Me.DataGridView1.Columns("Ded").ReadOnly = False
            Me.DataGridView1.Columns("packDed").ReadOnly = False
        End If
        If glbvar.gsapordno <> "" Or glbvar.gsapdocno <> "" Or glbvar.gsapinvno <> "" Then
            Me.DataGridView1.Enabled = False
            Me.Button1.Enabled = False
        End If
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
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
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
        'Multi from table
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
        cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(glbvar.multkt)
        cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

        Dim daamulti As New OracleDataAdapter(cmdc)
        daamulti.TableMappings.Add("Table", "mlt")
        Dim dsamlti As New DataSet
        daamulti.Fill(dsamlti)
        conn.Close()
        If CInt(dsamlti.Tables("mlt").Rows(0).Item("cnt").ToString) > 0 Then


            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Try
                cmdc.Connection = conn
                cmdc.Parameters.Clear()
                cmdc.CommandText = "curspkg_join.get_multi"
                cmdc.CommandType = CommandType.StoredProcedure
                cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Int32)).Value = CInt(glbvar.multkt)
                cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
                daamultitm.TableMappings.Add("Table", "mltitm")
                daamultitm.Fill(dsamltitm)
                conn.Close()
                For i = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1
                    inmode = 2
                    DataGridView1.Rows.Add()

                    Me.DataGridView1.Rows(i).Cells(0).Value = dsamltitm.Tables("mltitm").Rows(i).Item("SLNO").ToString()
                    Me.DataGridView1.Rows(i).Cells(1).Value = dsamltitm.Tables("mltitm").Rows(i).Item("ITEMCODE").ToString()
                    Me.DataGridView1.Rows(i).Cells(2).Value = dsamltitm.Tables("mltitm").Rows(i).Item("ITEMDESC").ToString()
                    Me.DataGridView1.Rows(i).Cells(4).Value = dsamltitm.Tables("mltitm").Rows(i).Item("QTY").ToString()
                    Me.DataGridView1.Rows(i).Cells(5).Value = dsamltitm.Tables("mltitm").Rows(i).Item("FIRSTQTY").ToString()
                    Me.DataGridView1.Rows(i).Cells(6).Value = dsamltitm.Tables("mltitm").Rows(i).Item("SECONDQTY").ToString()
                    Me.DataGridView1.Rows(i).Cells(7).Value = dsamltitm.Tables("mltitm").Rows(i).Item("PRICETON").ToString()
                    Me.DataGridView1.Rows(i).Cells(8).Value = dsamltitm.Tables("mltitm").Rows(i).Item("OMPRICE").ToString()
                    Me.DataGridView1.Rows(i).Cells(9).Value = dsamltitm.Tables("mltitm").Rows(i).Item("RATE").ToString()
                    Me.DataGridView1.Rows(i).Cells(10).Value = dsamltitm.Tables("mltitm").Rows(i).Item("DEDUCTION").ToString()
                    Me.DataGridView1.Rows(i).Cells(11).Value = dsamltitm.Tables("mltitm").Rows(i).Item("PACKDED").ToString()
                    Me.DataGridView1.Rows(i).Cells("pct").Value = (dsamltitm.Tables("mltitm").Rows(i).Item("QTY").ToString() / glbvar.vntwt) * 100
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        'Multi from table

        'Try
        '    If itmalloc = True Then
        '        Dim coun As Integer = 0
        '        coun = glbvar.itmcde.Count
        '        'ReDim itmcde(coun - 1)
        '        'ReDim itemdes(coun - 1)
        '        'ReDim pqty(coun - 1)
        '        'ReDim pfswt(coun - 1)
        '        'ReDim pscwt(coun - 1)
        '        'ReDim ppricekg(coun - 1)
        '        'ReDim prate(coun - 1)
        '        'ReDim pitem(coun - 1)
        '        'ReDim pmultided(coun - 1)
        '        'ReDim ppackded(coun - 1)
        '        'ReDim pomprice(coun - 1)
        '        For i = 0 To coun - 1
        '            'intiem(i) = Me.DataGridView1.Rows(i).Cells(0).Value
        '            DataGridView1.Rows.Add()
        '            Me.DataGridView1.Rows(i).Cells(0).Value = pitem(i)
        '            Me.DataGridView1.Rows(i).Cells(1).Value = itmcde(i)
        '            Me.DataGridView1.Rows(i).Cells(2).Value = itemdes(i)
        '            Me.DataGridView1.Rows(i).Cells(4).Value = pqty(i)
        '            If glbvar.inout = "I" Then
        '                Me.DataGridView1.Rows(i).Cells(5).Value = pfswt(i)
        '                Me.DataGridView1.Rows(i).Cells(6).Value = pscwt(i)
        '            ElseIf glbvar.inout = "O" Then
        '                Me.DataGridView1.Rows(i).Cells(5).Value = pscwt(i)
        '                Me.DataGridView1.Rows(i).Cells(6).Value = pfswt(i)
        '            End If
        '            Me.DataGridView1.Rows(i).Cells(7).Value = ppricekg(i)
        '            Me.DataGridView1.Rows(i).Cells(8).Value = pomprice(i)
        '            Me.DataGridView1.Rows(i).Cells(9).Value = prate(i)
        '            Me.DataGridView1.Rows(i).Cells(10).Value = pmultided(i)
        '            Me.DataGridView1.Rows(i).Cells(11).Value = ppackded(i)

        '        Next
        '    End If
        'Catch ex As Exception
        '    MsgBox("Continue")
        'End Try
        itmalloc = True
    End Sub

    

    

    
    Private Sub DataGridView1_Cellvalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated


        If e.ColumnIndex = 10 Then
            'If DataGridView1.CurrentCell.ColumnIndex = 10 Then
            'commented for deduction
            'Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
            'Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
            'Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            ''Me.DataGridView1.Rows(e.RowIndex).Cells(3).
            'Dim nwt As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
            'Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = nwt - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            'totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
            'If e.RowIndex = 0 Then
            '    If glbvar.inout = "I" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vfwt - nwt
            '    ElseIf glbvar.inout = "O" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vswt - nwt
            '    End If
            'Else
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - nwt


            'End If
            'commented for deduction
            Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
            Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            Dim nwt As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
            Dim per As Integer = ((CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue) + Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value + Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value) / glbvar.vntwt) * 100
            'Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value = per
            'Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            'Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
            Dim a = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Dim b = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value
            Dim c = Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value
            Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value - totded
            'If e.RowIndex = 0 Then
            '    If glbvar.inout = "I" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vfwt - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            '    ElseIf glbvar.inout = "O" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vswt - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            '    End If
            'Else
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            'End If

        ElseIf e.ColumnIndex = 11 Then
            'commented for deduction
            'Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
            'Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
            'Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            ''Me.DataGridView1.Rows(e.RowIndex).Cells(3).
            'Dim nwt As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
            'Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = nwt - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            'totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
            'If e.RowIndex = 0 Then
            '    If glbvar.inout = "I" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vfwt - nwt
            '    ElseIf glbvar.inout = "O" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vswt - nwt
            '    End If
            'Else
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - nwt


            'End If
            'commented for deduction
            Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
            Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            Dim nwt As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
            Dim per As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue) / glbvar.vntwt) * 100
            'Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value = per
            'Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
            totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
            Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value - totded

            'If e.RowIndex = 0 Then
            '    If glbvar.inout = "I" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vfwt - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            '    ElseIf glbvar.inout = "O" Then
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
            '        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = glbvar.vswt - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            '    End If
            'Else
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
            '    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
            'End If
        End If
        If e.ColumnIndex = 9 Then
            'Try

            '    conn = New OracleConnection(constr)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    'OLD If glbvar.inout = "I" Then 
            '    Dim count = 0
            '    'OLD For ai = 0 To DataGridView1.Rows.Count - 1
            '    sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
            '            & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
            '            & " WHERE z1.userauth_no = z2.userauth_no" _
            '            & " AND z1.username = z2.userid" _
            '            & " AND z2.userid = " & "'" & glbvar.userid & "'" _
            '            & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells(1).Value & "'" _
            '            & " and Z1.INTAUTHNO =  (SELECT   MAX (d.INTAUTHNO) " _
            '            & " FROM   ZUSER_AUTH_H d " _
            '            & " where username = " & "'" & glbvar.userid & "'" & ")"
            '    'OLD sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
            '    'OLD     & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
            '    'OLD    & " WHERE z1.userauth_no = z2.userauth_no" _
            '    'OLD     & " AND z1.username = z2.userid" _
            '    'OLD     & " AND z2.userid = " & "'" & glbvar.userid & "'" _
            '    'OLD     & " AND z2.matnr = " & "'" & tb_itemdesc.Text & "'"

            '    Dim dpct = New OracleDataAdapter(sql, conn)
            '    Dim dpc As New DataSet
            '    dpc.Clear()
            '    dpct.Fill(dpc)
            '    Dim user_tol_value As Decimal
            '    Dim user_tot_allowed As Decimal
            '    'OLD Dim pct = dpc.Tables(0).Rows(ai).Item("pct")
            '    'OLD Dim amt = dpc.Tables(0).Rows(ai).Item("amount")
            '    Dim pct = dpc.Tables(0).Rows(0).Item("pct")
            '    Dim amt = dpc.Tables(0).Rows(0).Item("amount")
            '    If glbvar.inout = "I" Then
            '        'OLD If glbvar.sapdocmulti = "QD" Or glbvar.sapdocmulti = "

            '        For ai = 0 To DataGridView1.Rows.Count - 1

            '            Dim plist = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value)
            '            user_tol_value = pct * plist
            '            user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value)
            '            If pct <> 0 Then
            '                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value) + user_tol_value
            '            ElseIf amt <> 0 Then
            '                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(7).Value) + amt / 1000
            '            End If

            '            If Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(9).Value) > user_tot_allowed Then
            '                count = count + 1
            '                Dim a = Convert.ToDecimal(Me.DataGridView1.Rows(ai).Cells(9).Value)
            '            End If
            '        Next
            '        If count > 0 Then
            '            MsgBox("Price not matching as the latest Pricelist")
            '            Me.Button1.Visible = False
            '        Else
            '            Me.Button1.Visible = True
            '            'OLD tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
            '        End If

            '        'OLD Else
            '        'OLD tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
            '        'OLD End If
            '    ElseIf glbvar.inout = "O" Then
            '        'OLD tb_TOTALPRICE.Text = Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text)
            '    End If
            '    conn.Close()
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try
        End If
    End Sub
    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        Try
            If e.ColumnIndex = 3 Then
                Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
                Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
                Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
                Dim nwt As Decimal = (CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
                Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value = nwt - Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value - Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
                totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
                Dim a = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
                Dim b = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value
                Dim c = Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value
                If e.RowIndex = 0 Then
                    If glbvar.inout = "I" Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
                        b = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value
                        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(glbvar.vfwt - a - totded)
                        c = Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value
                    ElseIf glbvar.inout = "O" Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
                        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(glbvar.vswt - a - totded)
                    End If
                Else

                    'Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Math.Round(Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value)
                    'Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue) - totded)
                    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
                    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue) - totded

                End If
            End If
            If e.ColumnIndex = 4 Then
                Dim qty = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
                Dim dedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value
                Dim pdedqty = Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value
                Dim nwt As Integer = (CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(3).EditedFormattedValue) * glbvar.vntwt) / 100
                Dim per As Decimal = ((CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue) + CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(10).EditedFormattedValue) + CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(11).EditedFormattedValue)) / glbvar.vntwt) * 100
                Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value = per
                totded = CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(10).Value) + CDec(Me.DataGridView1.Rows(e.RowIndex).Cells(11).Value)
                Dim a = CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(4).EditedFormattedValue)
                Dim b = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value
                Dim c = Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value
                If e.RowIndex = 0 Then
                    If glbvar.inout = "I" Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vfwt
                        b = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value
                        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(glbvar.vfwt - a - totded)
                        c = Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value
                    ElseIf glbvar.inout = "O" Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = glbvar.vswt
                        Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(glbvar.vswt - a - totded)
                    End If
                Else

                    'Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Math.Round(Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value)
                    'Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Math.Round(Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - a - totded)
                    Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(6).Value
                    Me.DataGridView1.Rows(e.RowIndex).Cells(6).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(5).EditedFormattedValue - a - totded
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    

    'Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
    '    If DataGridView1.CurrentRow.Cells(1).Selected = True Then
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
            'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
            Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMDESC").ToString)

            Me.ListView1.Items(i).SubItems.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
            

        Next
        Dim a = 1
    End Sub


    Private Sub ListView1_keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then

            Try


                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                If Me.ListView1.SelectedItems(0).SubItems(1).Text <> "" Then
                    If glbvar.inout <> "O" Then
                        'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                        Dim tdate = CDate(Today.Date).Day.ToString("D2")
                        Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                        Dim tyear = CDate(Today.Date).Year
                        Dim docdate = tyear & tmonth & tdate
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

                            Me.DataGridView1.CurrentRow.Cells(7).Value = dp.Tables(0).Rows(0).Item("price")

                        End If
                    End If
                    Dim abc As String
                    Me.DataGridView1.CurrentRow.Cells(1).Value = Me.ListView1.SelectedItems(0).SubItems(1).Text
                    abc = Me.DataGridView1.CurrentRow.Cells(1).Value
                    Me.DataGridView1.CurrentRow.Cells(2).Value = Me.ListView1.SelectedItems(0).SubItems(0).Text
                    abc = Me.DataGridView1.CurrentRow.Cells(2).Value
                    Me.DataGridView1.CurrentCell = Me.DataGridView1.CurrentRow.Cells(3)
                    Me.ListView1.Visible = False

                End If
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
    
        Try

      
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.ListView1.SelectedItems(0).SubItems(1).Text <> "" Then
                If glbvar.inout <> "O" Then
                    'Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    Dim tdate = CDate(Today.Date).Day.ToString("D2")
                    Dim tmonth = CDate(Today.Date).Month.ToString("D2")
                    Dim tyear = CDate(Today.Date).Year
                    Dim docdate = tyear & tmonth & tdate
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
                            & " AND d.div_code = " & "'" & glbvar.divcd & "'" _
                            & ")"

                    dpr = New OracleDataAdapter(sql, conn)
                    Dim dp As New DataSet
                    dp.Clear()
                    dpr.Fill(dp)

                    If dp.Tables(0).Rows.Count > 0 Then

                        Me.DataGridView1.CurrentRow.Cells(7).Value = dp.Tables(0).Rows(0).Item("price")
                        Me.DataGridView1.CurrentRow.Cells(9).Value = dp.Tables(0).Rows(0).Item("price")

                    End If
                End If
                Dim abc As String
                Me.DataGridView1.CurrentRow.Cells(1).Value = Me.ListView1.SelectedItems(0).SubItems(1).Text
                abc = Me.DataGridView1.CurrentRow.Cells(1).Value
                Me.DataGridView1.CurrentRow.Cells(2).Value = Me.ListView1.SelectedItems(0).SubItems(0).Text
                abc = Me.DataGridView1.CurrentRow.Cells(2).Value
                Me.DataGridView1.CurrentCell = Me.DataGridView1.CurrentRow.Cells(2)
                Me.ListView1.Visible = False

            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'DataGridView1.Rows.Clear()
        Dim cn As Integer = Me.DataGridView1.RowCount - 1

        If Me.tb_sum.Text = glbvar.vntwt - Me.tb_totalded.Text Then
            'ReDim intiem(cn - 1)
            ReDim itmcde(cn - 1)
            ReDim itemdes(cn - 1)
            ReDim pqty(cn - 1)
            ReDim pfswt(cn - 1)
            ReDim pscwt(cn - 1)
            ReDim ppricekg(cn - 1)
            ReDim prate(cn - 1)
            ReDim pitem(cn - 1)
            ReDim pmultided(cn - 1)
            ReDim ppackded(cn - 1)
            ReDim pomprice(cn - 1)
            Dim itm As Integer = 0
            For i = 0 To cn - 1
                'itm = itm + 10 'updated 30/5/2018
                'intiem(i) = Me.DataGridView1.Rows(i).Cells(0).Value
                'pitem(i) = itm 'updated 30/5/2018
                pitem(i) = Me.DataGridView1.Rows(i).Cells(0).Value
                itmcde(i) = Me.DataGridView1.Rows(i).Cells(1).Value
                itemdes(i) = Me.DataGridView1.Rows(i).Cells(2).Value
                pqty(i) = Me.DataGridView1.Rows(i).Cells(4).Value
                If glbvar.inout = "I" Then
                    pfswt(i) = Me.DataGridView1.Rows(i).Cells(5).Value
                    pscwt(i) = Me.DataGridView1.Rows(i).Cells(6).Value
                ElseIf glbvar.inout = "O" Then
                    pscwt(i) = Me.DataGridView1.Rows(i).Cells(5).Value
                    pfswt(i) = Me.DataGridView1.Rows(i).Cells(6).Value
                End If
                ppricekg(i) = Me.DataGridView1.Rows(i).Cells(7).Value
                pomprice(i) = Me.DataGridView1.Rows(i).Cells(8).Value
                prate(i) = Me.DataGridView1.Rows(i).Cells(9).Value
                pmultided(i) = Me.DataGridView1.Rows(i).Cells(10).Value
                ppackded(i) = Me.DataGridView1.Rows(i).Cells(11).Value
            Next
            Me.Close()
        Else
            MsgBox("Allocation does not match the netweight")
            Me.DataGridView1.Focus()
        End If
        If gmultival = True Then
            VALUATIONS.save_multi()
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        Try
            tot = 0
            totprice = 0
            deduction = 0
            packdeduction = 0
            For i = 0 To Me.DataGridView1.RowCount - 1
                tot = tot + Me.DataGridView1.Rows(i).Cells(4).FormattedValue
                totprice = totprice + Me.DataGridView1.Rows(i).Cells(9).FormattedValue
                deduction = deduction + Me.DataGridView1.Rows(i).Cells(10).FormattedValue
                packdeduction = packdeduction + Me.DataGridView1.Rows(i).Cells(11).FormattedValue
            Next
            Me.tb_sum.Text = tot
            Me.tb_sumprice.Text = totprice
            Me.tb_totalded.Text = deduction + packdeduction
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        glbvar.itmalloc = False
        Me.Close()
    End Sub

 

   
    
    'Private Sub DataGridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataGridView1.KeyPress
    '    Try

    '        If DataGridView1.CurrentRow.Cells(1).Value = e.ToString Then
    '            itmchar = itmchar + e.ToString
    '        End If
    '        Dim foundrow() As DataRow
    '        Dim expression As String = "ITEMDESC LIKE %'" & itmchar & "%'" & ""
    '        foundrow = dsitm.Tables("itm").Select(expression)
    '        ListView1.Items.Clear()
    '        For i = 0 To foundrow.Count - 1
    '            'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
    '            Me.ListView1.Items.Add(foundrow(0).Item("ITEMDESC").ToString)
    '            Me.ListView1.Items(i).SubItems.Add(foundrow(0).Item("ITEMCODE").ToString)
    '            ListView1.Visible = True
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave


    '    Try
    '        itmchar = ""
    '        If DataGridView1.CurrentRow.Cells(1).Value <> "" Then
    '            itmchar = itmchar + DataGridView1.CurrentRow.Cells("itmcode").Value.ToString

    '            Dim foundrow() As DataRow
    '            Dim expression As String = "ITEMDESC LIKE '" & itmchar & "%'" & ""
    '            foundrow = dsitm.Tables("itm").Select(expression)
    '            ListView1.Items.Clear()
    '            For i = 0 To foundrow.Count - 1
    '                'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
    '                Me.ListView1.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
    '                Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)

    '            Next
    '            ListView1.Visible = True

    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        Try
            If e.ColumnIndex = 1 Then
                itmchar = ""
                If DataGridView1.CurrentRow.Cells(1).Value <> "" Then
                    itmchar = itmchar + DataGridView1.CurrentRow.Cells("itmcode").Value.ToString

                    Dim foundrow() As DataRow
                    Dim expression As String = "ITEMDESC LIKE '" & itmchar & "%'" & ""
                    foundrow = dsitm.Tables("itm").Select(expression)
                    ListView1.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView1.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
                        Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)

                    Next
                    ListView1.Visible = True

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.KeyPress
    '    Try
    '         e.
    '        'itmchar = ""
    '        'If te <> "" Then
    '        itmchar = itmchar + TextBox1.Text

    '        Dim foundrow() As DataRow
    '        Dim expression As String = "ITEMDESC LIKE '" & itmchar & "%'" & ""
    '        foundrow = dsitm.Tables("itm").Select(expression)
    '        ListView1.Items.Clear()
    '        For i = 0 To foundrow.Count - 1
    '            'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
    '            Me.ListView1.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
    '            Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)

    '        Next
    '        ListView1.Visible = True

    '        'End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Try

            'itmchar = ""
            'If te <> "" Then
            'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
            If Asc(e.KeyChar) = 8 Then
                itmchar = ""
            Else
                itmchar = itmchar + e.KeyChar

                Dim foundrow() As DataRow
                Dim expression As String = "ITEMDESC LIKE '" & itmchar & "%'" & ""
                foundrow = dsitm.Tables("itm").Select(expression)
                ListView1.Items.Clear()
                For i = 0 To foundrow.Count - 1
                    'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                    Me.ListView1.Items.Add(foundrow(i).Item("ITEMDESC").ToString)
                    Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("ITEMCODE").ToString)

                Next
                ListView1.Visible = True

            End If
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

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView1.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
    Private Sub DataGridView1_CellValidated2(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        'Try
        '    If Me.DataGridView1.CurrentRow.Cells("Deduction").Selected Then
        '        Me.DataGridView1.CurrentRow.Cells("NETQTY").Value = Me.DataGridView1.CurrentRow.Cells("QTY").Value - Me.DataGridView1.CurrentRow.Cells("Deduction").EditedFormattedValue
        '        Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").Value
        '    ElseIf Me.DataGridView1.CurrentRow.Cells("RATE").Selected Then
        '        Me.DataGridView1.CurrentRow.Cells("VALUE").Value = Me.DataGridView1.CurrentRow.Cells("NETQTY").Value * Me.DataGridView1.CurrentRow.Cells("RATE").EditedFormattedValue
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        Try
            If Me.DataGridView1.CurrentRow.Cells("price").Selected Then

                conn = New OracleConnection(constr)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                sql = "SELECT   nvl(AMOUNT,0) AMOUNT, nvl(PRICE_TOLERANCE,0)/100 PCT" _
                        & " FROM   ZUSER_AUTH_H Z1, ZUSER_AUTH_IT Z2" _
                        & " WHERE z1.userauth_no = z2.userauth_no" _
                        & " AND z1.username = z2.userid" _
                        & " AND z2.userid = " & "'" & glbvar.userid & "'" _
                        & " AND z2.matnr = " & "'" & Me.DataGridView1.CurrentRow.Cells("itmcode").Value & "'" _
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
                Dim plist = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("tot_price").Value)
                user_tol_value = pct * plist
                user_sales_value = 2 * plist
                user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("tot_price").Value)
                user_sales_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("tot_price").Value) + user_sales_value
                If pct <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("tot_price").Value) + user_tol_value
                ElseIf amt <> 0 Then
                    user_tot_allowed = Convert.ToDecimal(Me.DataGridView1.CurrentRow.Cells("tot_price").Value) + amt / 1000
                End If
                If glbvar.inout = "I" Then
                    'If Me.tb_sap_doc.Text = "QD" Then
                    If Me.DataGridView1.CurrentRow.Cells("price").Value > user_tot_allowed Then

                        MsgBox("Price not matching as the latest Pricelist")
                        Me.Button1.Enabled = False
                        Me.DataGridView1.CurrentRow.Cells("price").Selected = True
                        Me.DataGridView1.CurrentRow.Cells("price").Value = 0
                    Else
                        Me.Button1.Enabled = True
                        '    tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    End If
                    'Else
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    'End If
                    'ElseIf Me.tb_inout_type.Text = "O" Then
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    'ElseIf Me.tb_inout_type.Text = "O" Then
                    '    If Me.tb_sap_doc.Text = "ZCWA" Then
                    '        If Me.DataGridView1.CurrentRow.Cells("rate").Value > user_sales_allowed Then

                    '            MsgBox("Price not matching as the latest Pricelist")
                    '            Me.tb_ok.Enabled = False
                    '            Me.DataGridView1.CurrentRow.Cells("rate").Selected = True
                    '            Me.DataGridView1.CurrentRow.Cells("rate").Value = 0

                    '        Else
                    '            Me.tb_ok.Enabled = True
                    '            '    tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                    '        End If
                    'Else
                    'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
                End If
                'ElseIf Me.tb_inout_type.Text = "O" Then
                'tb_TOTALPRICE.Text = Math.Round(Convert.ToDecimal(tb_PRICETON.Text) * Convert.ToDecimal(tb_QTY.Text), 2)
            End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class