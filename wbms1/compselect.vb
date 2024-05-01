'Imports System.Data.OracleClient
Imports Oracle.DataAccess.Client

Public Class compselect
    Dim conn As New OracleConnection
    Dim sql As String
    Dim da As OracleDataAdapter
    Dim constr As String
    Dim ds As New DataSet
    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See ht tp://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            glbvar.vyrcd = Me.cb_year.Text
            Dim frm As New usermenu
            frm.Show()
            Me.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        End
    End Sub

    Private Sub compselect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' fill the logged in user assigned company
        'Dim constr As String = My.Settings.Item("ConnString")
        connparam.setparams()

        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "Select comp_code,compname from vw_usr_comp where usercode = '" & glbvar.userid & "'"
        da = New OracleDataAdapter(sql, conn)
        da.TableMappings.Add("Table", "comp")
        da.Fill(ds)
        conn.Close()
        Me.cb_comp.DataSource = ds.Tables("comp")
        Me.cb_comp.DisplayMember = ds.Tables("comp").Columns("compname").ToString
        Me.cb_comp.ValueMember = ds.Tables("comp").Columns("comp_code").ToString
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub cb_comp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_comp.SelectedIndexChanged
        glbvar.cmpcd = Me.cb_comp.SelectedValue.ToString
        glbvar.BUKRS = glbvar.cmpcd
        'Dim constr As String = My.Settings.Item("ConnString")
        conn = New OracleConnection(constr)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        sql = "Select div_code,divdesc,LGORT,VSTEL,VKORG,VTWEG,EKORG,EKGRP,SPART,INDTYPE,SERVER,CLIENT,SYSID,VANLOC,SSLGORT,PRINT from vw_usr_div where comp_code = '" & Me.cb_comp.SelectedValue.ToString & "'" _
              & " and usercode = '" & glbvar.userid & "'"
        da = New OracleDataAdapter(sql, conn)
        da.TableMappings.Add("Table", "div")
        da.Fill(ds)
        Me.cb_div.DataSource = ds.Tables("div")
        Me.cb_div.DisplayMember = ds.Tables("div").Columns("divdesc").ToString
        Me.cb_div.ValueMember = ds.Tables("div").Columns("div_code").ToString
        sql = "select yearcode from acmfinyear where comp_code= '" & Me.cb_comp.SelectedValue.ToString & "' order by yearcode desc"
        da = New OracleDataAdapter(sql, conn)
        da.TableMappings.Add("Table", "yrcd")
        da.Fill(ds)
        conn.Close()
        Me.cb_year.DataSource = ds.Tables("yrcd")
        Me.cb_year.DisplayMember = ds.Tables("yrcd").Columns("yearcode").ToString
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub cb_div_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_div.SelectedIndexChanged
        Try
            glbvar.divcd = Me.cb_div.SelectedValue.ToString
            Dim exp As String = "div_code = " & "'" & glbvar.divcd & "'"
            Dim foundRows() As DataRow
            foundRows = ds.Tables("div").Select(exp)
            glbvar.gcompname = foundRows(0).ItemArray(1)
            For i = 0 To foundRows.Count - 1
                glbvar.LGORT = foundRows(i)("LGORT")
                glbvar.VSTEL = foundRows(i)("VSTEL")
                glbvar.VKORG = foundRows(i)("VKORG")
                glbvar.VTWEG = foundRows(i)("VTWEG")
                glbvar.EKORG = foundRows(i)("EKORG")
                glbvar.EKGRP = foundRows(i)("EKGRP")
                glbvar.SPART = foundRows(i)("SPART")
                glbvar.INDTYPE = foundRows(i)("INDTYPE")
                glbvar.SERVER = foundRows(i)("SERVER")
                glbvar.CLIENT = foundRows(i)("CLIENT")
                glbvar.SYSID = foundRows(i)("SYSID")
                glbvar.VANLOC = foundRows(i)("VANLOC")
                glbvar.SSLGORT = foundRows(i)("SSLGORT")
                glbvar.PRINT = foundRows(i)("PRINT")

            Next
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cb_year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_year.SelectedIndexChanged
        glbvar.vyrcd = Me.cb_year.SelectedValue.ToString
    End Sub
End Class
