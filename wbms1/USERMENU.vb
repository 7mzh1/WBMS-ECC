'Imports System.Data.OracleClient
Imports Oracle.DataAccess.Client

Public Class usermenu
    Dim conn As New OracleConnection
    Dim constr, constrd As String
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim dat As OracleDataAdapter
    Dim dab As OracleDataAdapter
    Dim sql As String
    Dim tsql As String
    Dim bsql As String
    Public ds As New DataSet

    Private Sub usermenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        compselect.Show()
    End Sub
    Private Sub usermenu_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Dim TreeView1 As TreeView
        'TreeView1 = New TreeView()
        'TreeView1.Location = New Point(10, 10)
        'TreeView1.Size = New Size(300, 300)
        'Me.Controls.Add(TreeView1)
        Try
            TreeView1.Nodes.Clear()
            Dim root = New TreeNode("WBMS")
            TreeView1.Nodes.Add(root)
            connparam.setparams()

            constr = "Data Source=" + connparam.datasource & _
                              ";User Id=" + connparam.username & _
                              ";Password=" + connparam.paswwd & _
                              ";Pooling=false"
            conn = New OracleConnection(constr)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            sql = "SELECT usercode,gp.MENUCODE menucode,gm.menuname menuname FROM GUSERMENUPRIV gp,gmenu gm " & _
            "where gp.menucode = gm.menucode and usercode= '" & glbvar.userid.ToUpper & "'and gm.div_CODE = '" & glbvar.divcd & "'"
            da = New OracleDataAdapter(sql, conn)
            Dim ds As New DataSet
            da.Fill(ds)
            conn.Close()
            Dim c = ds.Tables(0).Rows.Count
            For i = 0 To c - 1
                TreeView1.Nodes(0).Nodes.Add(New  _
                    TreeNode(ds.Tables(0).Rows(i).Item("menuname")))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.DoubleClick
        Try
            Dim c = Me.TreeView1.SelectedNode.Text
            TextBox1.Text = c
            sql = "SELECT gm.menuitem FROM GUSERMENUPRIV gp,gmenu gm " & _
            "where gp.menucode = gm.menucode and usercode= '" & glbvar.userid.ToUpper & "'" & _
            " and upper(menuname) = '" & TextBox1.Text.ToUpper & "'and gm.div_code = '" & glbvar.divcd & "'"
            da = New OracleDataAdapter(sql, conn)
            Dim ds As New DataSet
            da.Fill(ds)
            conn.Close()
            Dim menuname As String
            menuname = ds.Tables(0).Rows(0).Item("menuitem")
            Dim t As Type = Type.GetType(menuname) ', True, True)
            If t Is Nothing Then
                Dim Fullname As String = Application.ProductName & "." & menuname
                t = Type.GetType(Fullname, True, True)
            End If
            Dim k = CType(Activator.CreateInstance(t), Form)
            If t.Name = ("WBMS") Or t.Name = "WBMS_PR" Then
                'sss

                tsql = "SELECT   WBM.VEHICLENO" _
                    & "  FROM   PEND_TRN WBM"
                bsql = "SELECT   tclose" _
                    & "  FROM   mdivision where div_code ='" & glbvar.divcd & "'"



                dat = New OracleDataAdapter(tsql, conn)
                dab = New OracleDataAdapter(bsql, conn)
                Dim dstk As New DataSet
                Dim bstk As New DataSet
                Try

                    dat.Fill(dstk)
                    dab.Fill(bstk)
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
                Try
                    If bstk.Tables(0).Rows(0).Item("TCLOSE") = "X" And dstk.Tables(0).Rows.Count > 0 Then
                        MsgBox("Please close the pending transactions")
                    Else
                        k.Show()
                        Me.Visible = False
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                'sss
            Else
                k.Show()
                Me.Visible = False
            End If
            'If TextBox1.Text.ToUpper = "SCALING" Then
            '    WBMS.Show()
            '    Me.Visible = False
            'ElseIf TextBox1.Text.ToUpper = "VALUATION" Then
            '    VALUATION.Show()
            'conn.Dispose()
            'conn.Close()


            'MsgBox("Welcome to " & glbvar.gcompname)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    
End Class