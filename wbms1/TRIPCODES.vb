Imports System.Data
Imports System.IO.Ports
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports SAP.Middleware.Connector
Imports System.Timers

Public Class tripcode
    Dim wb_dir As New WBMS_DIR

    
    Public Sub tr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Dim wdir = New WBMS_DIR()
            If RfcDestinationManager.IsDestinationConfigurationRegistered = False Then
                RfcDestinationManager.RegisterDestinationConfiguration(New sap_cfg)
            End If
            Dim dest As RfcDestination = RfcDestinationManager.GetDestination("AGD")

            ' create connection to the RFC repository
            Dim repos As RfcRepository = dest.Repository

            Dim podet As IRfcFunction = dest.Repository.CreateFunction("Z_AQGNOW_TR_WBMS")
            'Dim pipeimp As IRfcStructure = pipedet.GetStructure("IPIPEIMP")
            Dim a = "0000" & g_driver
            podet.SetValue("DRIVER", "0000" & g_driver)
            'Dim a = 
            'pipeimp.SetValue("IMATNR", Me.DataGridView1.CurrentRow.Cells("Itemcode").Value)
            'pipeimp.SetValue("IPIPENO", Me.DataGridView1.CurrentRow.Cells("PIPENO").Value)
            Dim retpo As IRfcTable = podet.GetTable("TRLIST")
            Dim st As TimeSpan = Now.TimeOfDay
            podet.Invoke(dest)
            Dim ed As TimeSpan = Now.TimeOfDay
            'MsgBox("time taken for Pipe FM " & Convert.ToString((ed - st)))
            If retpo.RowCount > 0 Then


                For j = 0 To retpo.RowCount - 1
                    dgv_tr.Rows.Add()
                    dgv_tr.Rows(j).Cells("EBELN").Value = retpo(j).Item("AQGNOWREF").GetValue
                    dgv_tr.Rows(j).Cells("EBELP").Value = retpo(j).Item("DRIVER_ID").GetValue
                    dgv_tr.Rows(j).Cells("LIFNR").Value = retpo(j).Item("DRIVER_NAME").GetValue
                    dgv_tr.Rows(j).Cells("MATNR").Value = retpo(j).Item("DRIVER_CONTACT").GetValue
                    dgv_tr.Rows(j).Cells("TXZ01").Value = retpo(j).Item("VEHICLE_PLATE_NO").GetValue
                    dgv_tr.Rows(j).Cells("WERKS").Value = retpo(j).Item("VEHICLE_ID").GetValue
                    dgv_tr.Rows(j).Cells("LGORT").Value = retpo(j).Item("CUSTOMER").GetValue
                    dgv_tr.Rows(j).Cells("LEWED").Value = retpo(j).Item("MATERIAL").GetValue
                Next
                'fdgv.Show()
                'Me.dgv_po.Columns("OD").ReadOnly = True
                'Me.dgv_po.Columns("THICK").ReadOnly = True
                'Me.dgv_po.Columns("LENGTH").ReadOnly = True

            Else
                MsgBox(podet.GetValue("RETURNMSG").ToString)
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " From Trip List")
        End Try
    End Sub
    Public Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_tr.CellClick
        Try
            If e.ColumnIndex = 8 Then
                g_refno = Me.dgv_tr.CurrentRow.Cells("EBELN").Value
                WBMS_DIR.transfertr()
                'Me.Close()

                '             Me.Close()

            End If
            'Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    
End Class