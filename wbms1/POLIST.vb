Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System.Timers
Public Class fdgv
    Dim wb_dir As New WBMS_DIR
    Public Sub fdgv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Dim wdir = New WBMS_DIR()
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim repos As RfcRepository = dest.Repository

            Dim podet As IRfcFunction = dest.Repository.CreateFunction("Z_MM_PO_LIST_QN")
            'Dim pipeimp As IRfcStructure = pipedet.GetStructure("IPIPEIMP")
            podet.SetValue("VENDOR", "0000" & g_vendor)
            'Dim a = 
            'pipeimp.SetValue("IMATNR", Me.DataGridView1.CurrentRow.Cells("Itemcode").Value)
            'pipeimp.SetValue("IPIPENO", Me.DataGridView1.CurrentRow.Cells("PIPENO").Value)
            Dim retpo As IRfcTable = podet.GetTable("POLIST")
            Dim st As TimeSpan = Now.TimeOfDay
            podet.Invoke(dest)
            Dim ed As TimeSpan = Now.TimeOfDay
            'MsgBox("time taken for Pipe FM " & Convert.ToString((ed - st)))
            If retpo.RowCount > 0 Then


                For j = 0 To retpo.RowCount - 1
                    dgv_po.Rows.Add()
                    dgv_po.Rows(j).Cells("EBELN").Value = retpo(j).Item("EBELN").GetValue
                    dgv_po.Rows(j).Cells("EBELP").Value = retpo(j).Item("EBELP").GetValue
                    dgv_po.Rows(j).Cells("LIFNR").Value = retpo(j).Item("LIFNR").GetValue
                    dgv_po.Rows(j).Cells("MATNR").Value = retpo(j).Item("MATNR").GetValue
                    dgv_po.Rows(j).Cells("TXZ01").Value = retpo(j).Item("TXZ01").GetValue
                    dgv_po.Rows(j).Cells("WERKS").Value = retpo(j).Item("WERKS").GetValue
                    dgv_po.Rows(j).Cells("LGORT").Value = retpo(j).Item("LGORT").GetValue
                    dgv_po.Rows(j).Cells("LEWED").Value = retpo(j).Item("LEWED").GetValue
                    dgv_po.Rows(j).Cells("MENGE").Value = retpo(j).Item("MENGE").GetValue * 1000
                    dgv_po.Rows(j).Cells("RMENGE").Value = retpo(j).Item("RMENGE").GetValue * 1000
                    dgv_po.Rows(j).Cells("BAL").Value = retpo(j).Item("BAL").GetValue * 1000
                Next
                'fdgv.Show()
                'Me.dgv_po.Columns("OD").ReadOnly = True
                'Me.dgv_po.Columns("THICK").ReadOnly = True
                'Me.dgv_po.Columns("LENGTH").ReadOnly = True

            Else
                MsgBox(podet.GetValue("RETURNMSG").ToString)
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " From PO List")
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_po.CellClick
        Try
            If e.ColumnIndex = 11 AndAlso g_gform <> "g" Then
                g_pono = Me.dgv_po.CurrentRow.Cells("EBELN").Value
                g_itmno = Me.dgv_po.CurrentRow.Cells("EBELP").Value
                g_printmat = Me.dgv_po.CurrentRow.Cells("TXZ01").Value
                'wb_dir.Tb_asno.Text = g_pono
                Me.Close()

                '             Me.Close()
            ElseIf e.ColumnIndex = 11 AndAlso g_gform = "g" Then
                g_gpono = Me.dgv_po.CurrentRow.Cells("EBELN").Value
                g_gitmno = Me.dgv_po.CurrentRow.Cells("EBELP").Value
                g_gform = ""
                'g_printmat = Me.dgv_po.CurrentRow.Cells("TXZ01").Value
                'wb_dir.Tb_asno.Text = g_pono
                Me.Close()
            End If
            'Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class