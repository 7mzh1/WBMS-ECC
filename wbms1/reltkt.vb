Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System.Timers
Public Class reltkt
    Dim constr, constrd As String
    Dim conn As New OracleConnection
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dpr As OracleDataAdapter
    Dim dopr As OracleDataAdapter
    Dim sql As String
    Dim vsql As String
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
    Dim tkt() As Long

    Private Sub reltkt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

        
        Me.Text = Me.Text + " - " + glbvar.gcompname
        connparam_release.setparams()
        constr = "Data Source=" + connparam_release.datasource & _
                          ";User Id=" + connparam_release.username & _
                          ";Password=" + connparam_release.paswwd &
                          ";Pooling=false"
        conn = New OracleConnection(constr)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            usermenu.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            gbcode = Me.cb_reltkt.Text
            connparam_release.setparams()
            constr = "Data Source=" + connparam_release.datasource & _
                              ";User Id=" + connparam_release.username & _
                              ";Password=" + connparam_release.paswwd &
                              ";Pooling=false"
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            ' Check if it has got multiple items.
            cmd.Parameters.Clear()
            cmd.CommandText = "update stwbmibds set sprinted = null,gprinted = null where ticketno =" & tb_ticketno.Text
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
End Class