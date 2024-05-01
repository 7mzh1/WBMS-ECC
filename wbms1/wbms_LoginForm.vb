'Imports System.Data.OracleClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports Oracle.DataAccess.Client

Public Class wbms_LoginForm
    'Private comm As New Conn
    Private transType As String = String.Empty
    Dim conn As New OracleConnection
    'Dim constr As String
    Dim constr, constrd As String
    Public dr As OracleDataReader
    Dim da As OracleDataAdapter
    Dim sql As String
    Public ds As New DataSet
    Private TheKey(7) As Byte
    Private Vector() As Byte = {&H12, &H44, &H16, &HEE, &H88, &H25, &H19, &H21, &H8}
    'Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    'Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Public str As String

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
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
            sql = "SELECT USERCODE,PWD FROM GUSER where usercode='" & Me.UsernameTextBox.Text.ToUpper & "'"
            da = New OracleDataAdapter(sql, conn)
            Dim ds As New DataSet
            da.Fill(ds)
            glbvar.userid = Me.UsernameTextBox.Text.ToUpper()
            Dim com As New OracleCommand
            'If pinLabel.Visible = False Then
            'Try
            If Me.UsernameTextBox.Text <> "" And _
             Me.UsernameTextBox.Text.ToUpper = ds.Tables(0).Rows(0).Item("USERCODE") _
             And Me.PasswordTextBox.Text.ToUpper <> "" And _
             Me.PasswordTextBox.Text.Length >= 4 And Me.PasswordTextBox.Text.Length <= 8 Then
                com.Connection = conn
                com.CommandText = "P_CHKLEN"
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Clear()
                com.Parameters.Add("P_USER_PD", OracleDbType.Varchar2, 25).Value = Me.PasswordTextBox.Text
                com.Parameters.Add("P_USER", OracleDbType.Varchar2, 25).Direction = ParameterDirection.Output
                com.Parameters.Add("ERR_MSG", OracleDbType.Varchar2, 250).Direction = ParameterDirection.Output
                com.ExecuteNonQuery()
                'con.Close()
                Me.PasswordTextBox.Text = StrConv(com.Parameters("P_USER").Value.ToString, VbStrConv.Uppercase)
                'Me.Label1.Text = com.Parameters("ERR_MSG").Value
                'MsgBox(com.Parameters("P_USER").Value.ToString)
                com.CommandText = "P_CHKPSS"
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Clear()
                com.Parameters.Add("P_USER_PD", OracleDbType.Varchar2, 25).Value = Me.PasswordTextBox.Text.ToString
                com.Parameters.Add("ENCRYP_PD", OracleDbType.Varchar2, 25).Direction = ParameterDirection.Output
                com.Parameters.Add("DECRYP_PD", OracleDbType.Varchar2, 25).Direction = ParameterDirection.Output
                com.Parameters.Add("ERROR_MSG", OracleDbType.Varchar2, 2500).Direction = ParameterDirection.Output
                com.ExecuteNonQuery()
                If com.Parameters("ERROR_MSG").Value.ToString = "0" Then
                    glbvar.userid = Me.UsernameTextBox.Text.ToUpper
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    Dim frm As New compselect
                    frm.Show()
                    Me.Visible = False
                Else
                    MsgBox(com.Parameters("ERROR_MSG").Value.ToString)
                End If
            Else
                MsgBox("Invalid Username or Password")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox(com.Parameters("ERROR_MSG").Value.ToString)
            conn.Close()
        End Try
        'End If

        'to be used for sqldatabase
        'Try
        '    If Me.PasswordTextBox.Text.Length = 8 Then
        '        'con.Open()
        '        ''com.CommandText = "P_CHKPSS"
        '        ''com.CommandType = CommandType.StoredProcedure
        '        ''com.Parameters.Clear()
        '        ''com.Parameters.Add("P_USER_PD", SqlDbType.VarChar, 16).Value = Me.PasswordTextBox.Text.ToUpper
        '        ''com.Parameters.Add("encryp_pd", SqlDbType.VarChar, 2048).Direction = ParameterDirection.Output
        '        ''com.Parameters.Add("error_msg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output
        '        ''com.ExecuteNonQuery()
        '        ' ''con.Close()
        '        ''Dim s As String = com.Parameters("encryp_pd").Value.ToString
        '        'MsgBox(s)
        '        CreateKey(Me.PasswordTextBox.Text.ToUpper)
        '        ''com.Parameters("encryp_pd").Value = "D60B6E02D03788D1"
        '        'If com.Parameters("encryp_pd").Value = ds.Tables(0).Rows(0).Item("PWD") Then
        '        If str = ds.Tables(0).Rows(0).Item("PWD") Then
        '            'MsgBox("Username and Pasword is Correct")
        '            'UsernameLabel.Visible = False
        '            'UsernameTextBox.Visible = False
        '            'PasswordLabel.Visible = False
        '            'PasswordTextBox.Visible = False
        '            'pinLabel.Visible = False
        '            'PinTextBox.Visible = False
        '            ''PinTextBox.Focus()
        '            Dim frm As New compselect
        '            frm.Show()
        '            Me.Visible = False
        '        Else
        '            MsgBox("Invalid Username or Password")
        '        End If
        '    End If
        'Dim s2 As String = com.Parameters("error_msg").Value
        'MsgBox(com.Parameters("ENCRYP_PD" + "DECRYP_PD" + "ERROR_MSG").Value)
        'Console.WriteLine(com.Parameters["P_USER"].Value);
        '    Catch ex As Exception
        '        'MsgBox(ex.Source)
        '        MsgBox(com.Parameters("ERROR_MSG").Value)
        '    End Try
        'End If

        'If pinLabel.Visible = True Then
        '    Try
        '        If Me.PinTextBox.Text <> "" And Me.PinTextBox.Text.Length >= 4 And Me.PinTextBox.Text.Length <= 8 Then
        '            com.CommandText = "P_CHKLEN"
        '            com.CommandType = CommandType.StoredProcedure
        '            com.Parameters.Clear()
        '            com.Parameters.Add("P_USER_PD", SqlDbType.VarChar, 25).Value = Me.PinTextBox.Text
        '            com.Parameters.Add("P_USER", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output
        '            com.Parameters.Add("ERR_MSG", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output
        '            com.ExecuteNonQuery()
        '            'con.Close()
        '            Me.PinTextBox.Text = com.Parameters("P_USER").Value.ToString
        '            'Me.Label1.Text = com.Parameters("ERR_MSG").Value
        '            'MsgBox(com.Parameters("ERR_MSG").Value)
        '        Else
        '            'MsgBox("Invalid Username OR Password")
        '        End If
        '    Catch ex As Exception
        '        MsgBox(com.Parameters("ERROR_MSG").Value)
        '    End Try
        '    Try
        '        If Me.PinTextBox.Text.Length = 8 Then
        '            'con.Open()
        '            ''com.CommandText = "P_CHKPSS"
        '            ''com.CommandType = CommandType.StoredProcedure
        '            ''com.Parameters.Clear()
        '            ''com.Parameters.Add("P_USER_PD", SqlDbType.VarChar, 16).Value = Me.PinTextBox.Text.ToUpper
        '            ''com.Parameters.Add("encryp_pd", SqlDbType.VarChar, 2048).Direction = ParameterDirection.Output
        '            ''com.Parameters.Add("error_msg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output
        '            ''com.ExecuteNonQuery()
        '            'con.Close()
        '            ''Dim s As String = com.Parameters("encryp_pd").Value.ToString
        '            'MsgBox(s)
        '            ''com.Parameters("encryp_pd").Value = "546D59841043DCAF"
        '            CreateKey(Me.PinTextBox.Text.ToUpper)
        '            If str = ds.Tables(0).Rows(0).Item("PINNO") Then
        '                'MsgBox("PIN No. is Correct")
        '                Dim frm As New compselect
        '                frm.Show()
        '                Me.Visible = False
        '            Else
        '                MsgBox("Invalid PIN NO.")
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox(com.Parameters("ERROR_MSG").Value)
        '    End Try
        'End If
        'conn.Close()

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub wbms_LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd
    End Sub
End Class
