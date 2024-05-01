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

Public Class f_truck_income
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
    Dim dpr As OracleDataAdapter
    Dim dpcc As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Dim sql As String
    Public ds As New DataSet
    Dim ds1 As New DataSet
    Dim tmode As Integer
    Dim ymode As Integer
    Dim dasld As New OracleDataAdapter
    Dim dssld As New DataSet
    Dim darou As New OracleDataAdapter
    Dim dsrou As New DataSet
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
    Dim rowchk As Integer

    'Private Sub f_truck_income_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    usermenu.Show()
    '    Me.Close()
    'End Sub
    Private Sub f_truck_income_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Sled.ACMSLEDGER' table. You can move, or remove it, as needed.
        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd &
                          ";Pooling=false"
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
            dasld.TableMappings.Add("Table", "sledc")
            dasld.Fill(dssld)
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
        cmd.Parameters.Add(New OracleParameter("pdiv_code", OracleDbType.Varchar2)).Value = glbvar.divcd
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsdr.Clear()
            dadr = New OracleDataAdapter(cmd)
            dadr.TableMappings.Add("Table", "drvr")
            dadr.Fill(dsdr)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'If conn.State = ConnectionState.Closed Then
        '    conn.Open()
        'End If
        'cmd.Parameters.Clear()
        'cmd.CommandText = "curspkg_join.routemst"
        'cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        'Try
        '    dsrou.Clear()
        '    darou = New OracleDataAdapter(cmd)
        '    darou.TableMappings.Add("Table", "route")
        '    darou.Fill(dsrou)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "curspkg_join.trkmst"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Try
            dsrou.Clear()
            darou = New OracleDataAdapter(cmd)
            darou.TableMappings.Add("Table", "truck")
            darou.Fill(dsrou)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub b_save_Click(sender As Object, e As EventArgs) Handles b_save.Click
        If Me.trk_income_entry.Rows.Count = 0 Then
            MsgBox("Enter Details")
        Else
            Try
                Dim cn As Integer = Me.trk_income_entry.RowCount
                
                ReDim pslnotr(cn - 1)
                ReDim pdocdatetr(cn - 1)
                ReDim ptrailernotr(cn - 1)
                ReDim ptrailer_codetr(cn - 1)
                ReDim psledcodetr(cn - 1)
                ReDim psleddesctr(cn - 1)
                ReDim proutetr(cn - 1)
                ReDim pdrivernotr(cn - 1)
                ReDim pdriver_nametr(cn - 1)
                ReDim pnooftripstr(cn - 1)
                ReDim ptripratetr(cn - 1)
                ReDim pnetamounttr(cn - 1)
                ReDim premarks(cn - 1)
                Dim edate = Me.d_docdate.Text
                Dim format() = {"dd/MM/yyyy"}

                Dim expenddt As Date = Date.ParseExact(edate, format,
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    Globalization.DateTimeStyles.None)
                For i = 0 To cn - 1
                    pslnotr(i) = Me.trk_income_entry.Rows(i).Cells(0).Value
                    pdocdatetr(i) = expenddt 'Me.trk_income_entry.Rows(i).Cells(1).Value
                    ptrailernotr(i) = Me.trk_income_entry.Rows(i).Cells(2).Value
                    ptrailer_codetr(i) = Me.trk_income_entry.Rows(i).Cells(3).Value
                    psledcodetr(i) = Me.trk_income_entry.Rows(i).Cells(4).Value
                    psleddesctr(i) = Me.trk_income_entry.Rows(i).Cells(5).Value
                    proutetr(i) = Me.trk_income_entry.Rows(i).Cells(6).Value
                    pdrivernotr(i) = Me.trk_income_entry.Rows(i).Cells(7).Value
                    pdriver_nametr(i) = Me.trk_income_entry.Rows(i).Cells(8).Value
                    pnooftripstr(i) = Me.trk_income_entry.Rows(i).Cells(9).Value
                    ptripratetr(i) = Me.trk_income_entry.Rows(i).Cells(10).Value
                    pnetamounttr(i) = Me.trk_income_entry.Rows(i).Cells(11).Value
                    premarks(i) = Me.trk_income_entry.Rows(i).Cells(12).Value
                Next
                save_income()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Public Sub save_income()
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim coun As Integer = Me.trk_income_entry.RowCount
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "gen_iwb_dsd.gen_tr_income_i"
            cmd.CommandType = CommandType.StoredProcedure

            Dim p_sysdocno As OracleParameter = New OracleParameter(":p1", OracleDbType.Int32)
            p_sysdocno.Direction = ParameterDirection.Input
            p_sysdocno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_sysdocno.Value = glbvar.pslnotr

            cmd.Parameters.Add(p_sysdocno)

            Dim p_slno As OracleParameter = New OracleParameter(":p2", OracleDbType.Date)
            p_slno.Direction = ParameterDirection.Input
            p_slno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_slno.Value = glbvar.pdocdatetr

            cmd.Parameters.Add(p_slno)

            Dim p_htypes As OracleParameter = New OracleParameter(":p3", OracleDbType.Varchar2)
            p_htypes.Direction = ParameterDirection.Input
            p_htypes.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_htypes.Value = glbvar.ptrailernotr

            cmd.Parameters.Add(p_htypes)

            Dim p_sono As OracleParameter = New OracleParameter(":p4", OracleDbType.Varchar2)
            p_sono.Direction = ParameterDirection.Input
            p_sono.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_sono.Value = glbvar.ptrailer_codetr

            cmd.Parameters.Add(p_sono)

            Dim p_delno As OracleParameter = New OracleParameter(":p5", OracleDbType.Varchar2)
            p_delno.Direction = ParameterDirection.Input
            p_delno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_delno.Value = glbvar.psledcodetr

            cmd.Parameters.Add(p_delno)

            Dim p_invno As OracleParameter = New OracleParameter(":p6", OracleDbType.Varchar2)
            p_invno.Direction = ParameterDirection.Input
            p_invno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_invno.Value = glbvar.psleddesctr

            cmd.Parameters.Add(p_invno)

            Dim p_itemcode As OracleParameter = New OracleParameter(":p7", OracleDbType.Varchar2)
            p_itemcode.Direction = ParameterDirection.Input
            p_itemcode.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_itemcode.Value = glbvar.proutetr

            cmd.Parameters.Add(p_itemcode)

            Dim p_itemdesc As OracleParameter = New OracleParameter(":p8", OracleDbType.Varchar2)
            p_itemdesc.Direction = ParameterDirection.Input
            p_itemdesc.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_itemdesc.Value = glbvar.pdrivernotr

            cmd.Parameters.Add(p_itemdesc)

            Dim p_hedgtype As OracleParameter = New OracleParameter(":p9", OracleDbType.Varchar2)
            p_hedgtype.Direction = ParameterDirection.Input
            p_hedgtype.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_hedgtype.Value = glbvar.pdriver_nametr

            cmd.Parameters.Add(p_hedgtype)

            Dim p_partqty As OracleParameter = New OracleParameter(":p10", OracleDbType.Int32)
            p_partqty.Direction = ParameterDirection.Input
            p_partqty.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_partqty.Value = glbvar.pnooftripstr

            cmd.Parameters.Add(p_partqty)

            Dim p_lmerate As OracleParameter = New OracleParameter(":p11", OracleDbType.Int32)
            p_lmerate.Direction = ParameterDirection.Input
            p_lmerate.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_lmerate.Value = glbvar.ptripratetr

            cmd.Parameters.Add(p_lmerate)

            Dim p_partamt As OracleParameter = New OracleParameter(":p12", OracleDbType.Int32)
            p_partamt.Direction = ParameterDirection.Input
            p_partamt.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_partamt.Value = glbvar.pnetamounttr
            cmd.Parameters.Add(p_partamt)

            Dim p_remarks As OracleParameter = New OracleParameter(":p13", OracleDbType.Varchar2)
            p_remarks.Direction = ParameterDirection.Input
            p_remarks.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p_remarks.Value = glbvar.premarks
            cmd.Parameters.Add(p_remarks)

            Dim edate = Me.d_docdate.Text
            Dim format() = {"dd/MM/yyyy"}

            Dim expenddt As Date = Date.ParseExact(edate, format,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                Globalization.DateTimeStyles.None)
            Dim tdate = expenddt.Day.ToString("D2")
            Dim tmonth = expenddt.Month.ToString("D2")
            Dim tyear = expenddt.Year
            Dim docdate = tyear & tmonth & tdate
            cmd.Parameters.Add(New OracleParameter("delticket", OracleDbType.Decimal)).Value = docdate
            cmd.ExecuteNonQuery()
            MsgBox("Record Saved")

        Catch ex As Exception

            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub trk_income_entry_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles trk_income_entry.CellEndEdit
        If e.ColumnIndex = 10 Then
            Me.trk_income_entry.CurrentRow.Cells(11).Value = Me.trk_income_entry.CurrentRow.Cells(9).Value * Me.trk_income_entry.CurrentRow.Cells(10).Value
        End If
        If e.ColumnIndex = 9 Then
            Me.trk_income_entry.CurrentRow.Cells(11).Value = Me.trk_income_entry.CurrentRow.Cells(9).Value * Me.trk_income_entry.CurrentRow.Cells(10).Value
        End If
    End Sub
    Private Sub b_add_Click(sender As Object, e As EventArgs) Handles b_add.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            If Me.trk_income_entry.Rows.Count = 0 Then
                Me.trk_income_entry.Rows.Clear()
            End If
            Dim cns As Integer
            Dim c As Integer
            Dim edate = Me.d_docdate.Text
            Dim format() = {"dd/MM/yyyy"}
            'edate = "25/1/2017"
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
            sql = " select max(slno) cnt from TRUCK_INCOME WHERE" _
            & " to_number(to_char(docdate,'YYYYMMDD')) = to_number(" & "'" & docdate & "')"
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                If Not (IsDBNull(dpc.Tables(0).Rows(0).Item("cnt"))) Then
                    cns = dpc.Tables(0).Rows(0).Item("cnt")
                Else
                    cns = 0
                End If
            End If
            Dim a = Me.trk_income_entry.Rows.Count
            For i = 0 To a - 1
                c = trk_income_entry.Rows(i).Cells(0).Value
            Next
            If c = 0 Then
                trk_income_entry.Rows.Insert(rowIndex:=0)
                trk_income_entry.Rows(0).Cells(0).Value = 10
                rowchk = 10
                trk_income_entry.CurrentCell = trk_income_entry.Rows(trk_income_entry.Rows.Count - 1).Cells(1)
            ElseIf c > 0 Then
                trk_income_entry.Rows.Insert(rowIndex:=trk_income_entry.Rows.Count)
                rowchk = c
                rowchk = rowchk + 10
                trk_income_entry.Rows(trk_income_entry.Rows.Count - 1).Cells(0).Value = rowchk
                trk_income_entry.Rows(trk_income_entry.Rows.Count - 2).Selected = False
                'trk_income_entry.Rows(trk_income_entry.Rows.Count - 1).Selected = True
                trk_income_entry.CurrentCell = trk_income_entry.Rows(trk_income_entry.Rows.Count - 1).Cells(1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles b_delete.Click
        Try

            Me.trk_income_entry.Rows.Remove(Me.trk_income_entry.CurrentRow)
            rowchk = 0
            For i = 0 To trk_income_entry.Rows.Count - 1
                rowchk = rowchk + 10
                trk_income_entry.Rows(i).Cells(0).Value = rowchk
            Next
        Catch ex As Exception
            MsgBox("Add rows to delete")
        End Try
    End Sub

    Private Sub b_search_Click(sender As Object, e As EventArgs) Handles b_search.Click
        Try
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Me.trk_income_entry.Rows.Clear()
            Dim cns As Integer
            Dim edate = Me.d_docdate.Text
            Dim format() = {"dd/MM/yyyy"}
            'edate = "25/1/2017"
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
            sql = " select count(slno) cnt from TRUCK_INCOME WHERE" _
            & " to_number(to_char(docdate,'YYYYMMDD')) = to_number(" & "'" & docdate & "')"
            dpcc = New OracleDataAdapter(sql, conn)
            Dim dpc As New DataSet
            dpc.Clear()
            dpcc.Fill(dpc)
            If dpc.Tables(0).Rows.Count > 0 Then
                cns = dpc.Tables(0).Rows(0).Item("cnt")
            End If
            sql = "SELECT T.SLNO,T.DOCDATE,T.TRAILER_NO,T.TRAILER_CODE,T.SLEDCODE,T.fromroute,T.toROUTE,T.DRIVER_NO,T.DRIVER_NAME,T.NO_OF_TRIPS,T.TRIP_RATE,T.NETAMOUNT,t.remarks FROM ACCTS.TRUCK_INCOME T where" _
                & " to_number(to_char(docdate,'YYYYMMDD')) = to_number(" & "'" & docdate & "') order by slno desc"
            dpr = New OracleDataAdapter(sql, conn)
            Dim dp As New DataSet
            dp.Clear()
            dpr.Fill(dp)
            'Me.Tb_perc.Text = dp.Tables(0).Rows(0).Item("addn")
            Me.trk_income_entry.Rows.Clear()
            For i = 0 To cns - 1
                Me.trk_income_entry.Rows.Insert(rowIndex:=0)
                Me.trk_income_entry.Rows(0).Cells(0).Value = dp.Tables(0).Rows(i).Item("slno")
                Me.trk_income_entry.Rows(0).Cells(1).Value = dp.Tables(0).Rows(i).Item("DOCDATE")
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("TRAILER_NO")) Then
                    Me.trk_income_entry.Rows(0).Cells(2).Value = dp.Tables(0).Rows(i).Item("TRAILER_NO")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("TRAILER_CODE")) Then
                    Me.trk_income_entry.Rows(0).Cells(3).Value = dp.Tables(0).Rows(i).Item("TRAILER_CODE")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("SLEDCODE")) Then
                    Me.trk_income_entry.Rows(0).Cells(4).Value = dp.Tables(0).Rows(i).Item("SLEDCODE")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("fromRoute")) Then
                    Me.trk_income_entry.Rows(0).Cells(5).Value = dp.Tables(0).Rows(i).Item("fromRoute")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("toROUTE")) Then
                    Me.trk_income_entry.Rows(0).Cells(6).Value = dp.Tables(0).Rows(i).Item("toROUTE")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("DRIVER_NO")) Then
                    Me.trk_income_entry.Rows(0).Cells(7).Value = dp.Tables(0).Rows(i).Item("DRIVER_NO")
                End If
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("DRIVER_NAME")) Then
                    Me.trk_income_entry.Rows(0).Cells(8).Value = dp.Tables(0).Rows(i).Item("DRIVER_NAME")
                End If
                Me.trk_income_entry.Rows(0).Cells(9).Value = dp.Tables(0).Rows(i).Item("NO_OF_TRIPS")
                Me.trk_income_entry.Rows(0).Cells(10).Value = dp.Tables(0).Rows(i).Item("TRIP_RATE")
                Me.trk_income_entry.Rows(0).Cells(11).Value = dp.Tables(0).Rows(i).Item("NETAMOUNT")
                If Not IsDBNull(dp.Tables(0).Rows(i).Item("remarks")) Then
                    Me.trk_income_entry.Rows(0).Cells(12).Value = dp.Tables(0).Rows(i).Item("remarks")
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.b_add.Enabled = True
    End Sub
    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles trk_income_entry.EditingControlShowing

        If Me.trk_income_entry.CurrentCell.ColumnIndex = 2 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)

            RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
        ElseIf Me.trk_income_entry.CurrentCell.ColumnIndex = 7 And Not e.Control Is Nothing Then
                Dim tb As TextBox = CType(e.Control, TextBox)

                RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress1
                AddHandler tb.KeyPress, AddressOf TextBox_KeyPress1
                'ElseIf Me.trk_income_entry.CurrentCell.ColumnIndex = 6 And Not e.Control Is Nothing Then
                '    Dim tb As TextBox = CType(e.Control, TextBox)

                '    RemoveHandler tb.KeyPress, AddressOf TextBox_KeyPress2
                '    AddHandler tb.KeyPress, AddressOf TextBox_KeyPress2

            End If
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.trk_income_entry.CurrentCell.ColumnIndex = 2 Then
                Dim tb1 As TextBox = CType(sender, TextBox)
                'itmchar = ""
                'If te <> "" Then
                'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
                If tb1.Text.Length > 0 Then

                    Dim foundrow() As DataRow
                    Dim expression As String = "truckno LIKE '" & tb1.Text & "%'" & ""
                    foundrow = dsrou.Tables("truck").Select(expression)
                    ListView1.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView1.Items.Add(foundrow(i).Item("truckno").ToString)
                        Me.ListView1.Items(i).SubItems.Add(foundrow(i).Item("trcode").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView1.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Try

            Me.trk_income_entry.CurrentRow.Cells("trailer_no").Value = Me.ListView1.SelectedItems(0).SubItems(0).Text

            Me.trk_income_entry.CurrentRow.Cells("trailer_code").Value = Me.ListView1.SelectedItems(0).SubItems(1).Text

            Me.ListView1.Visible = False



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    
    Private Sub TextBox_KeyPress1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.trk_income_entry.CurrentCell.ColumnIndex = 7 Then
                Dim tb1 As TextBox = CType(sender, TextBox)
                'itmchar = ""
                'If te <> "" Then
                'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
                If tb1.Text.Length > 0 Then

                    Dim foundrow() As DataRow
                    Dim expression As String = "EMPNAME LIKE '" & tb1.Text & "%'" & ""
                    foundrow = dsdr.Tables("drvr").Select(expression)
                    ListView2.Items.Clear()
                    For i = 0 To foundrow.Count - 1
                        'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
                        Me.ListView2.Items.Add(foundrow(i).Item("EMPNAME").ToString)
                        Me.ListView2.Items(i).SubItems.Add(foundrow(i).Item("EMPCODE").ToString)

                    Next
                    'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
                    ListView2.Visible = True
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ListView1_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        Try

            Me.trk_income_entry.CurrentRow.Cells("DRIVER_NamE").Value = Me.ListView2.SelectedItems(0).SubItems(0).Text

            Me.trk_income_entry.CurrentRow.Cells("DRIVER_COdE").Value = Me.ListView2.SelectedItems(0).SubItems(1).Text

            Me.ListView2.Visible = False



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub TextBox_KeyPress2(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.trk_income_entry.CurrentCell.ColumnIndex = 6 Then
    '            Dim tb1 As TextBox = CType(sender, TextBox)
    '            'itmchar = ""
    '            'If te <> "" Then
    '            'If Asc(e.KeyChar) > 64 And Asc(e.KeyChar) < 91 Or Asc(e.KeyChar) > 96 And Asc(e.KeyChar) < 123 Then
    '            If tb1.Text.Length > 0 Then

    '                Dim foundrow() As DataRow
    '                Dim expression As String = "ZROUTE LIKE '" & tb1.Text & "%'" & ""
    '                foundrow = dsrou.Tables("route").Select(expression)
    '                ListView3.Items.Clear()
    '                For i = 0 To foundrow.Count - 1
    '                    'Me.ListView1.Items.Add(dsitm.Tables("itm").Rows(i).Item("ITEMCODE").ToString)
    '                    Me.ListView3.Items.Add(foundrow(i).Item("ZROUTE").ToString)
    '                    Me.ListView3.Items(i).SubItems.Add(foundrow(i).Item("RATE").ToString)

    '                Next
    '                'ListView1.SetBounds(Me.DataGridView1.CurrentRow.Cells.)
    '                ListView3.Visible = True
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    'Private Sub ListView1_DoubleClick2(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '        Me.trk_income_entry.CurrentRow.Cells("route").Value = Me.ListView3.SelectedItems(0).SubItems(0).Text
    '        Me.trk_income_entry.CurrentRow.Cells("trip_rate").Value = Me.ListView3.SelectedItems(0).SubItems(1).Text
    '        Me.ListView3.Visible = False



    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    

  
  

    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        trk_income_entry.Rows.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        usermenu.Show()
    End Sub
End Class