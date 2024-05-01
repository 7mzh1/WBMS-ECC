Imports System.Data
'Imports Oracle.DataAccess.Types
Imports System.Text
Imports Oracle.DataAccess.Client
Public Class MIXP
    Dim constr, constrd As String
    Dim conn As New OracleConnection
    Private Sub MIX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If glbvar.gsapordno <> "" Or glbvar.gsapdocno <> "" Or glbvar.gsapinvno <> "" Then
            Me.MIXGRID.Enabled = False
        End If
        Me.Text = Me.Text + " - " + glbvar.gcompname
        Me.MIXGRID.Rows.Clear()
        'Me.MIXGRID.Columns.Clear()
        connparam.setparams()
        constr = "Data Source=" + connparam.datasource & _
                          ";User Id=" + connparam.username & _
                          ";Password=" + connparam.paswwd & _
                          ";Pooling=false"
        Dim cmdc As New OracleCommand
        Dim count As Integer = 0
        Dim daamultitm As New OracleDataAdapter(cmdc)
        Dim dsamltitm As New DataSet
        conn = New OracleConnection(constr)



        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Try
            cmdc.Connection = conn
            cmdc.Parameters.Clear()
            cmdc.CommandText = "curspkg_join_pr.get_mix"
            cmdc.CommandType = CommandType.StoredProcedure
            cmdc.Parameters.Add(New OracleParameter("vtktno", OracleDbType.Decimal)).Value = CDec(glbvar.multkt)
            cmdc.Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            daamultitm.TableMappings.Add("Table", "mltitm")
            daamultitm.Fill(dsamltitm)
            For i = 0 To dsamltitm.Tables("mltitm").Rows.Count - 1

                MIXGRID.Rows.Add()
                Me.MIXGRID.Rows(i).Cells("PONO").Value = dsamltitm.Tables("mltitm").Rows(i).Item("PONO").ToString()
                Me.MIXGRID.Rows(i).Cells("ITEMNO").Value = dsamltitm.Tables("mltitm").Rows(i).Item("SLNO").ToString()
                Me.MIXGRID.Rows(i).Cells("QTY").Value = dsamltitm.Tables("mltitm").Rows(i).Item("QTY").ToString()
                If dsamltitm.Tables("mltitm").Rows(i).Item("COMFLG").ToString() = "X" Then
                    Me.MIXGRID.Rows(i).Cells("DEL").Value = True
                ElseIf dsamltitm.Tables("mltitm").Rows(i).Item("COMFLG").ToString() = "" Then
                    Me.MIXGRID.Rows(i).Cells("DEL").Value = False
                End If



            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.tb_netwt.Text = glbvar.vntwt
        conn.Close()
    End Sub

    Private Sub b_ok_Click(sender As Object, e As EventArgs) Handles b_ok.Click
        Dim cn As Integer = Me.MIXGRID.RowCount - 1
        Dim chk = 0
        For i = 0 To cn - 1
            Try
                If Me.MIXGRID.Rows(i).Cells("PONO").Value = "" Then
                    chk = 1
                End If

                'If IsNothing(p_mpono(i)) Or IsNothing(p_mitem(i)) Then
                '    chk = 1
                'End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next
        If chk > 0 Then
            MsgBox("Enter PO #")
            Me.MIXGRID.Focus()
        Else

            If Me.tb_sum.Text = Me.tb_netwt.Text Then
                ReDim p_mpono(cn - 1)
                ReDim p_mitem(cn - 1)
                ReDim p_mqty(cn - 1)
                ReDim p_mcomflg(cn - 1)
                For i = 0 To cn - 1
                    p_mpono(i) = Me.MIXGRID.Rows(i).Cells("PONO").Value
                    p_mitem(i) = Me.MIXGRID.Rows(i).Cells("ITEMNO").Value
                    p_mqty(i) = Me.MIXGRID.Rows(i).Cells("QTY").Value
                    Dim a = Me.MIXGRID.Rows(i).Cells("DEL").Value
                    If a = "TRUE" Then
                        p_mcomflg(i) = "X"
                    Else
                        p_mcomflg(i) = ""
                    End If
                Next
                'Me.MIXGRID.Rows.Clear()

                Me.Close()
                VALUATIONS_PR.save_mix()
                glbvar.mix = True
                'ReDim p_mpono(cn - 1)
                'ReDim p_mitem(cn - 1)
                'ReDim p_mqty(cn - 1)
                'ReDim p_mcomflg(cn - 1)
                'For m = 0 To Me.MIXGRID.RowCount - 1
                '    Me.MIXGRID.Rows(m).Cells.Clear()
                'Next
                'Dim ba As New MIX
                'ba.MIXGRID.Rows.Clear()
            Else
                MsgBox("Allocation does not match the netweight")
                Me.MIXGRID.Focus()
            End If
        End If

    End Sub
    Private Sub MixGrid_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MIXGRID.RowEnter
        Dim tot = 0



        For i = 0 To Me.MIXGRID.RowCount - 1
            tot = tot + Me.MIXGRID.Rows(i).Cells("QTY").EditedFormattedValue

        Next
        Me.tb_sum.Text = tot


    End Sub

    Private Sub b_cancel_Click(sender As Object, e As EventArgs) Handles b_cancel.Click

        Me.MIXGRID.Rows.Clear()
        Me.Close()

    End Sub
End Class