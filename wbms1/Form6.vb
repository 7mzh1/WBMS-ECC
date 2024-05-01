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
Public Class mmaster

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
    Dim itemcode = 0
    Private Sub master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
    End Sub
    Private Sub b_cr_Click(sender As Object, e As EventArgs) Handles b_cr.Click
        Try

            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "SELECT   NVL(MAX(to_number(WBM.itemcode)),0)+1 itm" _
                    & "  FROM   SMITEM WBM WHERE ITEMGRPCODE = 'PIPE'"
            da = New OracleDataAdapter(sql, conn)
            Dim dstk As New DataSet
            Try
                da.TableMappings.Add("Table", "item")
                da.Fill(dstk)
                itemcode = dstk.Tables("item").Rows(0).Item("itm")
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.Parameters.Clear()
            cmd.CommandText = "Insert into ACCTS.SMITEM (COMP_CODE, DIV_CODE,  ITEMCODE, ITEMDESC,ITEMGRPCODE ) Values (" _
                & "'" _
            & glbvar.cmpcd _
            & "'" _
            & "," _
                         & "'" _
            & glbvar.divcd _
                         & "'" _
            & "," _
                         & "'" _
            & itemcode _
                         & "'" _
           & "," _
                        & "'" _
            & Me.TextBox1.Text _
                         & "'" _
                         & "," _
                         & "'" _
                         & "PIPE" _
            & "'" _
            & ")"
            cmd.CommandType = CommandType.Text

            cmd.ExecuteNonQuery()
            MsgBox("Material Created " & itemcode)
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message & " From Creating")
        End Try
    End Sub

    Private Sub b_close_Click(sender As Object, e As EventArgs) Handles b_close.Click


        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        usermenu.Show()
        Me.Close()

    End Sub
End Class