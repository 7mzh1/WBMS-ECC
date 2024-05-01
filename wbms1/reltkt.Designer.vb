<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reltkt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cb_reltkt = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tb_ticketno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cb_reltkt
        '
        Me.cb_reltkt.AllowDrop = True
        Me.cb_reltkt.FormattingEnabled = True
        Me.cb_reltkt.Items.AddRange(New Object() {"XE", "DM01", "DM02", "DM03", "DM04", "DM05", "DM07", "DM08", "PR01", "JB01", "RY01", "RY02", "RY07", "YA01", "TF01", "JZ01"})
        Me.cb_reltkt.Location = New System.Drawing.Point(244, 47)
        Me.cb_reltkt.MaxDropDownItems = 10
        Me.cb_reltkt.Name = "cb_reltkt"
        Me.cb_reltkt.Size = New System.Drawing.Size(121, 21)
        Me.cb_reltkt.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Branch"
        '
        'tb_ticketno
        '
        Me.tb_ticketno.Location = New System.Drawing.Point(244, 74)
        Me.tb_ticketno.Name = "tb_ticketno"
        Me.tb_ticketno.Size = New System.Drawing.Size(121, 20)
        Me.tb_ticketno.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(192, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ticket #"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(244, 100)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Release"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(619, 100)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'reltkt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 417)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_ticketno)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cb_reltkt)
        Me.Name = "reltkt"
        Me.Text = "Release-Ticket"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cb_reltkt As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_ticketno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
